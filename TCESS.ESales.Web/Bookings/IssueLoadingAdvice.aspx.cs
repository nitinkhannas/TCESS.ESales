#region Using directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Exception;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.BusinessLayer.Interfaces.GhatoCollection;

#endregion

public partial class Bookings_IssueLoadingAdvice : BasePage
{
    /// <summary>
    /// Page_Init event to register ok button event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        ucViewImage.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }
    /// <summary>
    /// Page load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckIsUserAuthenticated();

        if (!IsPostBack)
        {
            ViewState[Globals.StateMgmtVariables.DOCID] = null;
            ViewState[Globals.StateMgmtVariables.CUSTOMERID] = null;
            ViewState[Globals.StateMgmtVariables.TRUCKID] = null;

            ddlTruck.Items.Insert(0, new ListItem(Messages.SelectTruck, "0"));
            ddlMaterial.Items.Insert(0, new ListItem(Messages.SelectMaterialType, "0"));

            //Get booking mode details from database for current date
            GetBookingModeDetails();

            if (!String.IsNullOrEmpty(Convert.ToString(Session[Globals.StateMgmtVariables.BOOKINGID])))
            {
                PopulateLoadingAdviceDetails(Convert.ToInt32(Session[Globals.StateMgmtVariables.BOOKINGID]));
            }
        }
    }

    /// <summary>
    /// Get booking mode details from database for current date
    /// </summary>
    private void GetBookingModeDetails()
    {
        //Get booking mode details from database for current date
        IList<BookingModeDetailDTO> lstBookingModeDetail = ESalesUnityContainer.Resolve<IBookingModeService>()
            .GetBookingModeDetails();

        int currentHour = DateTime.Now.Hour;

        //Gets the booking mode for current hour
        BookingModeDetailDTO bookingModeDetail = (from F in lstBookingModeDetail
                                                  where F.BookingDetails_StartTime.Hours <= currentHour &&
                                                  F.BookingDetails_EndTime.Hours > currentHour
                                                  select F).FirstOrDefault();

        //If booking mode not exists for current hour, set "Current Open" as default booking mode
        if (bookingModeDetail == null)
        {
            //Gets default booking mode details from database
            BookingModeDTO bookingModeDetails = ESalesUnityContainer.Resolve<IBookingModeService>().GetDefaultBookingMode();
            txtBookingMode.Text = bookingModeDetails.BookingMode_Name;
            ViewState[Globals.StateMgmtVariables.BOOKINGMODEID] = bookingModeDetails.BookingMode_Id;
            ViewState[Globals.StateMgmtVariables.BOOKINGGROUP] = bookingModeDetails.BookingMode_Group;

            //Gets total bookings completed for default booking mode
            int bookingCompleted = GetTotalBookingsCompleted(Convert.ToInt32(bookingModeDetails.BookingMode_Id),
                DateTime.Now.Date);
            txtBookingDate.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            txtTotalBookings.Text = bookingCompleted.ToString();
            txtAllowedBooking.Text = Messages.NoLimit;
            btnSave.Enabled = true;
        }
        else
        {
            //Adds number of days to booking date with respect to booking mode selected
            int totalDays = bookingModeDetail.BookingDetails_Mode_AddDays;
            DateTime bookingDate = totalDays != 0 ? DateTime.Now.Date.AddDays(totalDays) : DateTime.Now.Date;

            //Gets total bookings completed for default booking mode
            int bookingCompleted = GetTotalBookingsCompleted(Convert.ToInt32(bookingModeDetail.BookingDetails_Mode_Id),
                bookingDate);
            txtBookingMode.Text = bookingModeDetail.BookingDetails_Mode_Name;

            //If booking completed for current booking matches with total trucks allowed for current booking mode then
            //message prompts for current booking mode closed and disables save button
            if (bookingCompleted <= bookingModeDetail.BookingDetails_Trucks)
            {
                ViewState[Globals.StateMgmtVariables.BOOKINGMODEID] = bookingModeDetail.BookingDetails_Mode_Id;
                ViewState[Globals.StateMgmtVariables.BOOKINGGROUP] = bookingModeDetail.BookingDetails_Mode_Group;
                txtBookingDate.Text = bookingDate.ToString("dd-MMM-yyyy");
                txtTotalBookings.Text = bookingCompleted.ToString();
                txtAllowedBooking.Text = bookingModeDetail.BookingDetails_Trucks.ToString();
                btnSave.Enabled = true;
            }
            else
            {
                //Show notification message for booking closed
                ucMessageBox.ShowMessage(Messages.BookingClosedFor + bookingModeDetail.BookingDetails_Mode_Name);
                btnSave.Enabled = false;
            }
        }
    }

    /// <summary>
    /// Get Total bookings completed for current booking mode
    /// </summary>
    /// <param name="bookingModeId">Int32: selected booking mode</param>
    /// <param name="bookingDate">DateTime: booking date</param>
    /// <returns>Returns booking completed</returns>
    private int GetTotalBookingsCompleted(int bookingModeId, DateTime bookingDate)
    {
        int bookingCompleted = ESalesUnityContainer.Resolve<IBookingService>()
            .GetTodaysBookingCountByMode(bookingModeId, bookingDate);
        return bookingCompleted;
    }
    /// <summary>
    /// To validate the customer detail and sms info
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        lblCounterNo.Visible = false;

        //Get customer details by customer code
        GetCustomerDetails(txtCustomerCode.Text.Trim());
        smsValidate.Enabled = false;
    }
    /// <summary>
    /// Event for the selected Index Change of dropdown list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlTruck_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTruck.SelectedValue != "0")
        {
            //Get truck details based on the type of truck selected
            GetTruckDetail(ddlTruck.SelectedItem.Value, false);
        }
    }
    /// <summary>
    /// Event for Sign Button 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSign_Click(object sender, EventArgs e)
    {
        if (ViewState[Globals.StateMgmtVariables.DOCID] != null)
        {
            ucViewImage.ShowMessage(Convert.ToString(ViewState[Globals.StateMgmtVariables.DOCID]));
        }
    }
    /// <summary>
    /// Event for the save Button.It will used for save current booking option
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (chkSignValid.Checked)
            {
                //If current booking mode group is current booking
                if (Convert.ToInt32(ViewState[Globals.StateMgmtVariables.BOOKINGGROUP]) == 1)
                {
                    //Save current bookings
                    SaveCurrentBooking();
                }
                else
                {
                    //Save advance bookings
                    SaveAdvanceBooking();
                }
            }
            else
            {
                ucMessageBox.ShowMessage(ErrorMessages.SignatureFailed);
            }

        }
    }

    /// <summary>
    /// Save advance bookings
    /// </summary>
    private void SaveAdvanceBooking()
    {
        try
        {
            ExceptionHandler.AppExceptionManager.Process(() =>
            {
                if (rdBookingStatus.SelectedItem.Value == "1")
                {

                    //Initialize advance booking details if booking is accepted
                    BookingDTO advBookingDetails = InitializeBookingDetails(0);
                    advBookingDetails.Booking_IsAdvanced = true;
                    ESalesUnityContainer.Container.Resolve<IBookingService>().SaveAndUpdateBookingDetail(advBookingDetails);
                }
                else
                {
                    //Initialize advance booking details if booking is rejected
                    BookingDTO rejBookingDetails = InitializeBookingDetails(0);
                    rejBookingDetails.Booking_IsAdvanced = true;
                    ESalesUnityContainer.Container.Resolve<IBookingService>().SaveAndUpdateBookingDetail(rejBookingDetails);
                }

                //Reset controls on page to default state
                ResetFields();
            }, Globals.ExceptionTypes.AssistingAdministrators.ToString());
        }
        catch (Exception ex)
        {
        }
    }

    /// <summary>
    /// Save current bookings
    /// </summary>
    private void SaveCurrentBooking()
    {
        try
        {
            ExceptionHandler.AppExceptionManager.Process(() =>
            {
                if (rdBookingStatus.SelectedItem.Value == "1")
                {
                    AcceptSave();
                }
                else
                {
                    BookingDTO rejBookingDetails = InitializeBookingDetails(0);
                    rejBookingDetails.Booking_IsDeleted = true;
                    ESalesUnityContainer.Container.Resolve<IBookingService>().SaveAllRejectedBookingInfo(rejBookingDetails, Convert.ToInt32(txtSmsRegNo.Text.Trim()));
                }

                //Reset controls on page to default state
                ResetFields();
            }, Globals.ExceptionTypes.ExceptionShielding.ToString());
        }
        catch (Exception ex)
        {
        }
    }
    /// <summary>
    /// Method for Accept Save
    /// </summary>
    private void AcceptSave()
    {
        Boolean flagMonth = false;
        Boolean flagSave = false;
        Boolean flagTSLAcceptedDate = false;
        int currentMonth = DateTime.Now.Month;
        int currentYear = DateTime.Now.Year;
        string truckNumber = string.Empty;

        DateTime now = DateTime.Now;
        string month = now.ToString("MMMM");


        string Form27CCheck = ConfigurationManager.AppSettings["Form27CCheck"].ToLower();
        string Form27CActive = ConfigurationManager.AppSettings["Form27CActive"].ToLower();

        if (Form27CCheck == "booking" && Form27CActive == "true")
        {
            IList<Form27CDTO> form27CList = ESalesUnityContainer.Container.Resolve<IForm27CService>()
            .GetForm27CDetailsByCustIdList(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]));

            if (ddlTruck.SelectedIndex > 0)
            {
                truckNumber = ddlTruck.SelectedItem.Text;
            }
            else
            {
                truckNumber = txtStandaloneTruck.Text;
            }


            if (form27CList.Count == 0)
            {
                SmsUtility.SendSMSForBookings(ViewState["MobileNo"].ToString(), "Aapke code mein form 27C jama nahin kiya gaya hai. Isliye Truck " + truckNumber + " ki booking sweekar nahin ki ja saki. Agli booking se pahle form 27C jama karen. DCA Ghato");
                ucMessageBox.ShowMessage("Please submit Form 27C for the current Month.");
            }
            else
            {
                foreach (Form27CDTO item in form27CList)
                {
                    if (item.ValidMonthCount == null)
                    {
                        item.ValidMonthCount = 0;
                    }

                    if (currentMonth >= item.CurrentMonth && currentMonth <= item.ValidMonthCount + item.CurrentMonth)
                    {
                        if (item.AcceptedByTSLDate == null)
                        {
                            flagTSLAcceptedDate = false;
                        }
                        else
                        {
                            flagTSLAcceptedDate = true;
                        }
                        if (currentMonth == 12)
                        {
                            int endYear = +currentYear + 1;
                            if (currentYear > Convert.ToInt32(item.ValidYear) && currentYear < endYear)
                            {
                                flagMonth = true;
                            }
                        }
                        else
                        {
                            if (currentYear == Convert.ToInt32(item.ValidYear))
                                flagMonth = true;
                        }
                    }

                }

                if (flagMonth && flagSave == false && flagTSLAcceptedDate)
                {
                    SaveBooking();
                }
                else if (!flagMonth)
                {
                    SmsUtility.SendSMSForBookings(ViewState["MobileNo"].ToString(), "Aapke code mein form 27C jama nahin kiya gaya hai. Isliye Truck " + truckNumber + " ki booking sweekar nahin ki ja saki. Agli booking se pahle form 27C jama karen. DCA Ghato");
                    ucMessageBox.ShowMessage("Form 27C for the current Month is not submitted.");
                }
                else if (flagMonth && !flagTSLAcceptedDate)
                {
                    ucMessageBox.ShowMessage("Form 27C for the current Month is not Activated.");
                    SaveBooking();
                }
            }
        }
        else
        {
            SaveBooking();
            flagSave = true;
        }
    }

    private void SaveBooking()
    {
        int smsRegId = 0;
        string counterId = string.Empty;
        int agentId = 0;

        IList<DcaMaterialAllocationDTO> lstMaterialAllocations = ESalesUnityContainer.Container
            .Resolve<IDcaMaterialAllocationService>()
            .GetMaterialAllocationDetails(Convert.ToInt32(ddlMaterial.SelectedItem.Value), DateTime.Now.Date);

        if (txtSmsRegNo.Text.Trim() != "")
        {
            smsRegId = Convert.ToInt32(txtSmsRegNo.Text.Trim());
        }

        if (lstMaterialAllocations.Count > 0)
        {
            if (ViewState[Globals.StateMgmtVariables.AGENTID] == null)
            {
                agentId = (from F in lstMaterialAllocations
                           where F.DCAMA_AllocatedQty == 0
                           select F.DCAMA_Agent_Id).FirstOrDefault();

                if (agentId == 0)
                {
                    agentId = (from F in lstMaterialAllocations
                               orderby F.DCAMA_CurrentVariance descending
                               select F.DCAMA_Agent_Id).FirstOrDefault();
                }
            }
            else
            {
                agentId = (from F in lstMaterialAllocations
                           where F.DCAMA_Agent_Id == Convert.ToInt32(ViewState[Globals.StateMgmtVariables.AGENTID])
                           select F.DCAMA_Agent_Id).FirstOrDefault();
            }

            if (agentId != 0)
            {
                int qtySum = (from F in lstMaterialAllocations
                              select F.DCAMA_AllocatedQty).Sum() + Convert.ToInt32(txtCurrentQty.Text.Trim());

                foreach (DcaMaterialAllocationDTO item in lstMaterialAllocations)
                {
                    if (item.DCAMA_Agent_Id == agentId)
                    {
                        item.DCAMA_AllocatedQty += Convert.ToInt32(txtCurrentQty.Text.Trim());
                        item.DCAMA_LastQty = Convert.ToInt32(txtCurrentQty.Text.Trim());
                    }
                    item.DCAMA_CurrentPercentage = (Convert.ToDecimal(item.DCAMA_AllocatedQty) / Convert.ToDecimal(qtySum)) * 100;
                    item.DCAMA_CurrentVariance = item.DCAMA_TodayPercentage - item.DCAMA_CurrentPercentage;
                }

                counterId = ESalesUnityContainer.Container.Resolve<IBookingService>()
                    .SaveAllBookingInfo(lstMaterialAllocations, InitializeBookingDetails(agentId), GetCounterID(agentId), smsRegId);

                CounterDTO counterDetails = ESalesUnityContainer.Container.Resolve<ICounterService>().GetCounterDetailsById(Convert.ToInt32(counterId));

                string truckNo = txtStandaloneTruck.Text.Trim();
                if (rdStandAlone.SelectedItem.Value == "1")
                {
                    truckNo = ddlTruck.SelectedItem.Text;
                }

                string englishMessage = Messages.BookingConfirmation.FormatWith(truckNo, txtCurrentQty.Text.Trim());
                //string hindiMessage = " ?? ";
                //+truckNo + " ?? " + txtCurrentQty.Text.Trim() + "??  Tailings ??? ????  ?? .??  ??????  ?? ???  ???????? .";

                SmsUtility.SendSMS(txtMobileNo.Text.Trim(), englishMessage + " .DCA Ghato");
                lblCounterNo.Visible = true;
                lblCounterNo.Text = Messages.GoToCounterNo + counterDetails.Counter_Name;
            }
            else
            {
                ucMessageBox.ShowMessage(Messages.AgentNotActive);
            }
        }
        else
        {
            ucMessageBox.ShowMessage(Messages.AgentNotActive);
        }
    }


    /// <summary>
    /// To Reset field
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        //Reset controls on page to default state
        ResetFields();
    }
    /// <summary>
    /// To Get StandAlone truck Detail
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGetStandAlone_Click(object sender, EventArgs e)
    {
        //Checks if standalone truck field is empty
        if (!string.IsNullOrEmpty(txtStandaloneTruck.Text.Trim()))
        {
            //Get truck details based on the type of truck selected
            GetTruckDetail(txtStandaloneTruck.Text.Trim(), true);
        }
    }
    /// <summary>
    /// Slected Index change for dropdown list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rdStandAlone_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtNumberofWheel.Text = string.Empty;
        txtCarryCapacity.Text = string.Empty;
        txtDriverDetails.Text = string.Empty;
        txtStandaloneTruck.Text = string.Empty;

        if (ddlTruck.Items.Count > 0)
        {
            ddlTruck.SelectedIndex = 0;
        }

        if (rdStandAlone.SelectedItem.Value == "1")
        {
            ddlTruck.Enabled = true;
            txtStandaloneTruck.Enabled = false;
            btnGetStandAlone.Enabled = false;
        }
        else
        {
            txtStandaloneTruck.Enabled = true;
            btnGetStandAlone.Enabled = true;
            ddlTruck.Enabled = false;
        }
    }
    /// <summary>
    /// Event for the selected Index change for dropdown list ddlMaterial
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlMaterial_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState[Globals.StateMgmtVariables.CUSTOMERID] != null && Convert.ToInt32(ddlMaterial.SelectedItem.Value) != 0)
        {
            //Gets customer material details by customer and material id
            CustomerMaterialMapDTO customerMatDetails = ESalesUnityContainer.Container.Resolve<ICustomerMaterialService>()
                .GetCustomerMaterialByCustomerAndMaterialId(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]),
                Convert.ToInt32(ddlMaterial.SelectedItem.Value));

            //Gets current booking date
            DateTime bookingModeDate = Convert.ToDateTime(txtBookingDate.Text).Date;

            //Gets total quantity issued for current booking mode
            IList<object> quantityDetails = ESalesUnityContainer.Container.Resolve<IBookingService>()
               .GetTotalIssuedQty(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]),
               Convert.ToInt32(ddlMaterial.SelectedItem.Value), bookingModeDate).ToList();

            //If customer is allotted some quantity && lifting limit for customer is not exceeded
            if (customerMatDetails.Cust_Mat_AllotedQuantityId != 1 && customerMatDetails.Cust_Mat_LiftingLimit > Convert.ToInt32(quantityDetails[1]) && Convert.ToInt32(customerMatDetails.Cust_Mat_AllotedQuantity) > (Convert.ToInt32(quantityDetails[0]) + Convert.ToInt32(txtCarryCapacity.Text == "" ? "0" : txtCarryCapacity.Text)))
            {
                txtMaxLiftQty.Text = customerMatDetails.Cust_Mat_AllotedQuantity.ToString();
                txtTotalIssuedQty.Text = Convert.ToString(quantityDetails[0]);
                CheckCurrentBalance();
            }
            else
            {
                ddlMaterial.SelectedIndex = 0;
                if (customerMatDetails.Cust_Mat_AllotedQuantityId == 1)
                {
                    ucMessageBox.ShowMessage(Messages.QuantityNotAllotted);
                }
                else
                {
                    ucMessageBox.ShowMessage(Messages.LiftingLimitExceeded);
                }
            }
        }
    }
    /// <summary>
    /// event for the selected Index Change of booking Status
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rdBookingStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdBookingStatus.SelectedValue == "1")
        {
            txtRejectionReason.Text = string.Empty;
            txtRejectionReason.ReadOnly = true;
        }
        else
        {
            txtRejectionReason.ReadOnly = false;
        }
    }
    /// <summary>
    /// Event to validate the Sms
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void smsValidate_Click(object sender, EventArgs e)
    {
        string CustomerBusinessType = string.Empty;
        string CustomerTruckType = string.Empty;
        string customerTruckTypeName = string.Empty;
        txtTotalBookingAdvance.Text = string.Empty;
        txtBalanceAdvance.Text = string.Empty;
        txtAdvanceAmount.Text = string.Empty;

        List<CounterDTO> lstCounterData = ESalesUnityContainer.Container.Resolve<ICounterService>().GetCounterList().ToList();
        List<CounterDetailsDTO> lstCounterDetailsData = ESalesUnityContainer.Container.Resolve<ICounterService>().GetCounterDetailsListForCurrentDate().ToList();

        IList<int> lstMaterialAllocations = ESalesUnityContainer.Container.Resolve<IDcaMaterialAllocationService>()
                    .GetAllMaterialAllocationAgentIDList(DateTime.Now.Date);

        var query = from counter in lstCounterData
                    join material in lstMaterialAllocations.Select(k => k) on
                    counter.Counter_Agent_Id equals material
                    select counter.Counter_Id;

        var loggedCounters = lstCounterDetailsData.FindAll(T => query.ToList().Contains(T.CounterDetail_Counter_ID));

        ViewState["MobileNo"] = txtPhoneNumber.Text.Trim();

        if (query.ToList().Count == loggedCounters.ToList().Count)
        {
            IList<CustomerDTO> lstCustomer = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetCustomerDetailsByMobileNumber(txtPhoneNumber.Text.Trim());

            SMSRegistrationDTO smsRegDetails = ESalesUnityContainer.Container.Resolve<ISMSService>().GetTodaysSMSDetailsById(Convert.ToInt32(txtSmsRegNo.Text), DateTime.Now.Date.AddDays(-1));
            CustomerDTO customer = new CustomerDTO();
            if (lstCustomer.Count > 0 && smsRegDetails.SMSReg_Id > 0)
            {
                customer = (from F in lstCustomer
                            where F.Cust_Code == smsRegDetails.SMSReg_Cust_Code
                            select F).FirstOrDefault();
            }
            if (customer.Cust_Id > 0 && smsRegDetails.SMSReg_Id > 0 && smsRegDetails.SMSReg_Booking_Id == null)
            {
                string TruckTypeCheck = ConfigurationManager.AppSettings["TruckTypeCheck"].ToLower();

                if (TruckTypeCheck == "booking")
                {
                    CustomerBusinessType = customer.Cust_Business_Name;
                    if (CustomerBusinessType != "Bricks ")
                    {
                        // Only for Non-Brick Customers

                        IList<LiftingLimitDTO> LstLimitDTO = ESalesUnityContainer.Container.Resolve<ILiftingLimit>().GetLimitList();
                        foreach (LiftingLimitDTO l in LstLimitDTO)
                        {
                            if (l.LiftingLimit_Business_Name != "Bricks ")
                            {
                                ViewState[Globals.StateMgmtVariables.CUSTOMERTRUCKTYPE] = l.LiftingLimit_TruckRegType_Id;
                                ViewState[Globals.StateMgmtVariables.CUSTOMERTRUCKTYPENAME] = l.LiftingLimit_TruckRegType_Name;
                            }

                        }

                        if (ViewState[Globals.StateMgmtVariables.CUSTOMERTRUCKTYPE] != null)
                        {
                            CustomerTruckType = ViewState[Globals.StateMgmtVariables.CUSTOMERTRUCKTYPE].ToString();
                            customerTruckTypeName = ViewState[Globals.StateMgmtVariables.CUSTOMERTRUCKTYPENAME].ToString();

                            TruckVerificationDTO truckDetails = ESalesUnityContainer.Container.Resolve<ITruckService>().GetAllTruckDetails(smsRegDetails.SMSReg_TruckNo.Trim());

                            if (truckDetails.Truck_Id > 0)
                            {
                                long truckTypes = truckDetails.type;
                                if (Convert.ToInt32(CustomerTruckType) != truckTypes)
                                {
                                    ucMessageBox.ShowMessage("Only " + customerTruckTypeName + " are allowed for Hardcoke Customers.");
                                }
                                else
                                {

                                    GetCustomerDetails(customer.Cust_Code);
                                    if (truckDetails.type == 1)
                                    {
                                        GetTruckDetail(truckDetails.Truck_Id.ToString(), false);
                                        ddlTruck.SelectedValue = truckDetails.Truck_Id.ToString();
                                        rdStandAlone.SelectedValue = "1";
                                    }
                                    else
                                    {
                                        GetTruckDetail(truckDetails.Truck_RegNo, true);
                                        rdStandAlone.SelectedValue = "2";
                                    }
                                    //Validat for avaliable balance
                                    {

                                    }

                                }
                            }
                            // Sms send on Invalid truck or truck not registered
                            else
                            {
                                ucMessageBox.ShowMessage("Truck not registered");
                            }
                        }
                    }
                    else
                    {
                        // Only for Brick Customer
                        ValidatingTruckAndCustomer(smsRegDetails.SMSReg_TruckNo.Trim(), customer.Cust_Code);
                    }
                }
                else
                {
                    ValidatingTruckAndCustomer(smsRegDetails.SMSReg_TruckNo.Trim(), customer.Cust_Code);
                }

            }
            else
            {
                //Reset controls on page to default state
                ResetFields();
                if (customer.Cust_Id > 0)
                {
                    ucMessageBox.ShowMessage("SMS ID already used");
                }
                else
                {
                    ucMessageBox.ShowMessage(Messages.CustomerNotFound);
                }
            }
        }
        else
        {
            ucMessageBox.ShowMessage("All counters are not  Active for today");
        }

    }

    # region Private Methods

    private void CheckCurrentBalance()
    {
        string BalanceAmt;
        
        //get total deposit amount Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID])
        decimal totalAmountCollected = ESalesUnityContainer.Container.Resolve<IPaymentService>().GetPaymentMadeByCustomer(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]), Convert.ToDateTime(ConfigurationManager.AppSettings["PaymentStartDate"]), Convert.ToDateTime(ConfigurationManager.AppSettings["PaymentEndDate"]));

        decimal totalRefundAmount = ESalesUnityContainer.Container.Resolve<IPaymentService>().GetCustomerPaymentRefundList(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID])).Sum(f => f.PR_Amount);

        //get Total exp amount
        decimal totalMaterialLiftedAmount = ESalesUnityContainer.Container.Resolve<ISettlementOfAccountsService>().GetMaterialAmountLiftedByCustomer(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]), Convert.ToDateTime(ConfigurationManager.AppSettings["PaymentStartDate"]), Convert.ToDateTime(ConfigurationManager.AppSettings["PaymentEndDate"]));
        //Get InTransit amount
        decimal InTransitLoad = ESalesUnityContainer.Container.Resolve<IBookingService>().GetIntransisCustomerQty(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]), Convert.ToDateTime(ConfigurationManager.AppSettings["PaymentStartDate"]), Convert.ToDateTime(ConfigurationManager.AppSettings["PaymentEndDate"])).Sum(item => item.Booking_Qty);
        InTransitLoad = InTransitLoad + (Convert.ToDecimal(InTransitLoad) * Convert.ToDecimal(ConfigurationManager.AppSettings["OverLiftingPercentage"]) / 100);
        // decimal InTransitAmount = GetAmount(ESalesUnityContainer.Container.Resolve<IBookingService>().GetIntransisCustomerQty(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]), Convert.ToDateTime(ConfigurationManager.AppSettings["PaymentStartDate"]), Convert.ToDateTime(ConfigurationManager.AppSettings["PaymentEndDate"])).Sum(item => item.Booking_Qty));
        decimal InTransitAmount = GetAmount(InTransitLoad);

        decimal currentLoad = (Convert.ToDecimal(txtCarryCapacity.Text.Trim()) + (Convert.ToDecimal(txtCarryCapacity.Text.Trim()) * Convert.ToDecimal(ConfigurationManager.AppSettings["OverLiftingPercentage"]) / 100));
        decimal currentAmount = GetAmount(currentLoad);
    
        if (totalAmountCollected >= (totalMaterialLiftedAmount + InTransitAmount + currentAmount + totalRefundAmount))
        {

            decimal balanceAvlAmount = totalAmountCollected - (totalMaterialLiftedAmount + InTransitAmount + totalRefundAmount);
            decimal balanceAmount = totalAmountCollected - (totalMaterialLiftedAmount + InTransitAmount + currentAmount + totalRefundAmount);
            txtAdvanceAmount.Text = string.Format("{0:N2}", currentAmount);
            txtTotalBookingAdvance.Text = string.Format("{0:N2}", balanceAvlAmount);
            txtBalanceAdvance.Text = string.Format("{0:N2}", balanceAmount);
            txtAdvanceAmount.ReadOnly = true;
        }
        else
        {
            decimal balanceAvlAmount = totalAmountCollected - (totalMaterialLiftedAmount + InTransitAmount + totalRefundAmount);
            decimal balanceAmount = totalAmountCollected - (totalMaterialLiftedAmount + InTransitAmount + currentAmount + totalRefundAmount);
            BalanceAmt = string.Format("{0:N2}", balanceAmount);
            //Reset controls on page to default state
            ResetFields();
            decimal msgBalanceAvlAmount = totalAmountCollected - (totalMaterialLiftedAmount + InTransitAmount + totalRefundAmount);
            decimal msgBalanceAmount = totalAmountCollected - (totalMaterialLiftedAmount + InTransitAmount + currentAmount + totalRefundAmount);
            msgBalanceAmount = msgBalanceAmount * -1;
            if (msgBalanceAvlAmount < 0)
            {
                msgBalanceAvlAmount = 0;
            }
            ucMessageBox.ShowMessage("BAL: " + string.Format("{0:N2}",msgBalanceAvlAmount) + " ;REQD: " + string.Format("{0:N2}",msgBalanceAmount) + " .Insufficent Fund.");
        }
    }

    private decimal GetAmount(decimal qty)
    {
        MaterialTypeDTO materialTypeDetails = new MaterialTypeDTO();
        materialTypeDetails = ESalesUnityContainer.Container.Resolve<IMaterialTypeService>()
            .GetMaterialTypeById(Convert.ToInt32(ddlMaterial.SelectedItem.Value));

        decimal handlingRate = Convert.ToDecimal(qty) * Convert.ToDecimal(materialTypeDetails.MaterialType_HandlingRate);
        decimal tiscoRate = Convert.ToDecimal(qty) * Convert.ToDecimal(materialTypeDetails.MaterialType_TiscoRate);
        decimal grossAmount = handlingRate + tiscoRate;
        decimal serviceTax = handlingRate * (Convert.ToDecimal(materialTypeDetails.MaterialType_ServiceTax) / 100);
        decimal educationCess = serviceTax * (Convert.ToDecimal(materialTypeDetails.MaterialType_EducationCess) / 100);
        decimal higherEducationCess = serviceTax * (Convert.ToDecimal(materialTypeDetails.MaterialType_HigherEducationCess) / 100);
        decimal netAmount = grossAmount + serviceTax + educationCess + higherEducationCess;
        return netAmount;
    }

    private void ValidatingTruckAndCustomer(string truckNo, string customerCode)
    {
        TruckVerificationDTO truckDetails = ESalesUnityContainer.Container.Resolve<ITruckService>().GetAllTruckDetails(truckNo);

        if (truckDetails.Truck_Id > 0)
        {
            lblCounterNo.Visible = false;
            //Get customer details by customer code
            GetCustomerDetails(customerCode);

            if (truckDetails.type == 1)
            {
                GetTruckDetail(truckDetails.Truck_Id.ToString(), false);
                ddlTruck.SelectedValue = truckDetails.Truck_Id.ToString();
                rdStandAlone.SelectedValue = "1";
            }
            else
            {
                GetTruckDetail(truckDetails.Truck_RegNo, true);
                rdStandAlone.SelectedValue = "2";
            }

            txtCustomerCode.ReadOnly = true;
            txtStandaloneTruck.Enabled = false;
            rdStandAlone.Enabled = false;
            btnValidate.Enabled = false;
            btnGetStandAlone.Enabled = false;
            btnValidate.Enabled = false;
        }
        else
        {
            //Reset controls on page to default state
            ResetFields();
            ucMessageBox.ShowMessage(Messages.CustomerNotFound);
        }
    }

    /// <summary>
    /// Get customer details by customer code
    /// </summary>
    /// <param name="customerCode">String: customercode</param>
    private void GetCustomerDetails(string customerCode)
    {
        //Get customer material details by customer code
        IList<CustomerMaterialMapDTO> lstCustomerMaterials = ESalesUnityContainer.Container.Resolve<ICustomerMaterialService>()
            .GetCustomerMaterialDetailsByCustomerCode(customerCode);

        if (lstCustomerMaterials.Count > 0)
        {
            txtTradeName.Text = lstCustomerMaterials[0].Cust_Mat_Customer.Cust_TradeName;
            txtFirmName.Text = lstCustomerMaterials[0].Cust_Mat_Customer.Cust_FirmName;
            txtAddress.Text = lstCustomerMaterials[0].Cust_Mat_Customer.Cust_UnitAddress;
            txtMobileNo.Text = lstCustomerMaterials[0].Cust_Mat_Customer.Cust_MobileNo;
            ViewState["MobileNo"] = lstCustomerMaterials[0].Cust_Mat_Customer.Cust_MobileNo;
            txtCustomerCode.Text = lstCustomerMaterials[0].Cust_Mat_Customer.Cust_Code;
            ViewState[Globals.StateMgmtVariables.CUSTOMERID] = lstCustomerMaterials[0].Cust_Mat_Customer.Cust_Id;
            ViewState[Globals.StateMgmtVariables.AGENTID] = lstCustomerMaterials[0].Cust_Mat_Customer.Cust_AgentId;
            ViewState[Globals.StateMgmtVariables.CUSTOMERBUSINESSTYPE] = lstCustomerMaterials[0].Cust_Mat_Customer.Cust_Business_Name;

            //if ((ViewState[Globals.StateMgmtVariables.CUSTOMERBUSINESSTYPE]).ToString() != "Bricks ")
            //{
            //txtAdvanceAmount.Text = "0";
            txtAdvanceAmount.ReadOnly = true;
            // ucMessageBox.ShowMessage("Hardcoke customer, Advance Amount is 0");
            //}

            //Get customer document details by customer id
            IList<CustomerDocDetailsDTO> listCustomerDoc = ESalesUnityContainer.Container.Resolve<ICustomerDocService>()
            .GetCustomerDocumentDetails(lstCustomerMaterials[0].Cust_Mat_Customer.Cust_Id);

            //Get customer signature from customer document details
            ViewState[Globals.StateMgmtVariables.DOCID] = (from Doc in listCustomerDoc
                                                           where Doc.Cust_Doc_DocId == 13
                                                           select Doc.Cust_Doc_Id).FirstOrDefault();

            ddlMaterial.DataSource = lstCustomerMaterials;
            ddlMaterial.DataBind();
            ddlMaterial.Items.Insert(0, new ListItem(Messages.SelectMaterialType, "0"));

            ddlTruck.DataSource = lstCustomerMaterials[0].Cust_Mat_Customer.Cust_TruckList;
            ddlTruck.DataBind();
            ddlTruck.Items.Insert(0, new ListItem(Messages.SelectTruck, "0"));
        }
        else
        {
            //Reset controls on page to default state
            ResetFields();
            ucMessageBox.ShowMessage(Messages.CustomerNotFound);
        }
    }

    /// <summary>
    /// Reset controls on page to default state
    /// </summary>
    private void ResetFields()
    {
        txtCustomerCode.Text = string.Empty;
        txtTradeName.Text = string.Empty;
        txtFirmName.Text = string.Empty;
        ddlTruck.Items.Clear();
        ddlMaterial.SelectedIndex = 0;
        txtNumberofWheel.Text = string.Empty;
        txtCarryCapacity.Text = string.Empty;
        txtTotalIssuedQty.Text = string.Empty;
        txtMaxLiftQty.Text = string.Empty;
        txtAdvanceAmount.Text = string.Empty;
        txtRejectionReason.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtDriverDetails.Text = string.Empty;
        txtCurrentQty.Text = string.Empty;
        txtStandaloneTruck.Text = string.Empty;
        txtSmsRegNo.Text = string.Empty;
        txtPhoneNumber.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        chkSignValid.Checked = false;

        rdBookingStatus.SelectedIndex = 1;
        rdStandAlone.SelectedIndex = 1;

        txtStandaloneTruck.Enabled = true;
        btnGetStandAlone.Enabled = true;
        ddlTruck.Enabled = false;
        txtAdvanceAmount.ReadOnly = false;
        txtCustomerCode.ReadOnly = false;
        rdStandAlone.Enabled = true;
        btnValidate.Enabled = true;
        btnGetStandAlone.Enabled = true;
        ddlMaterial.Enabled = true;
        smsValidate.Enabled = true;
        txtTotalBookingAdvance.Text = string.Empty;
        txtBalanceAdvance.Text = string.Empty;


        ViewState[Globals.StateMgmtVariables.DOCID] = null;
        ViewState[Globals.StateMgmtVariables.CUSTOMERID] = null;
        ViewState[Globals.StateMgmtVariables.TRUCKID] = null;
        Session[Globals.StateMgmtVariables.BOOKINGID] = null;
    }

    /// <summary>
    /// Initialize booking details for save in database
    /// </summary>
    /// <param name="agentId">Int32: agentId</param>
    /// <returns>returns booking details</returns>
    private BookingDTO InitializeBookingDetails(int agentId)
    {
        BookingDTO bookingDetails = new BookingDTO();

        bookingDetails.Booking_Cust_Id = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]);
        if (Session[Globals.StateMgmtVariables.BOOKINGID] != null)
        {
            bookingDetails.Booking_Id = Convert.ToInt32(Session[Globals.StateMgmtVariables.BOOKINGID]);
        }
        if (agentId != 0)
        {
            bookingDetails.Booking_Agent_Id = agentId;
        }
        if (rdStandAlone.SelectedItem.Value == "1")
        {
            bookingDetails.Booking_Truck_Id = Convert.ToInt32(ddlTruck.SelectedItem.Value);
        }
        else
        {
            bookingDetails.Booking_TruckType = true;
            bookingDetails.Booking_StandAlone_Truck_Id = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.TRUCKID]);
        }

        bookingDetails.Booking_MaterialType_Id = Convert.ToInt32(ddlMaterial.SelectedItem.Value);
        bookingDetails.Booking_AdvanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text.Trim());

        bookingDetails.Booking_TotalAdvanceAmount = Convert.ToDecimal(txtTotalBookingAdvance.Text.Trim());
        bookingDetails.Booking_BalanceAmount = Convert.ToDecimal(txtBalanceAdvance.Text.Trim());

        bookingDetails.Booking_TotalIssuedQty = Convert.ToInt32(txtTotalIssuedQty.Text.Trim());
        bookingDetails.Booking_Qty = Convert.ToInt32(txtCurrentQty.Text.Trim());
        bookingDetails.Booking_Status = rdBookingStatus.SelectedItem.Value == "1" ? true : false;
        bookingDetails.Booking_RejectionReson = txtRejectionReason.Text.Trim();
        bookingDetails.Booking_CreatedBy = GetCurrentUserId();
        bookingDetails.Booking_LastUpdatedDate = DateTime.Now;
        bookingDetails.Booking_CreatedDate = DateTime.Now;
        bookingDetails.Booking_Mode = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.BOOKINGMODEID]);
        bookingDetails.Booking_Date = Convert.ToDateTime(txtBookingDate.Text);
        return bookingDetails;
    }

    /// <summary>
    /// Get all active counter detail by agent Id
    /// </summary>
    /// <param name="agentID">Int32: agentId</param>
    /// <returns>return counter details</returns>
    private CounterDetailsDTO GetCounterID(int agentID)
    {
        //Get all active counters for agent on a day
        IList<CounterDetailsDTO> lstcounterDetails = ESalesUnityContainer.Container.Resolve<ICounterService>()
            .GetCounterDailyDetails(agentID);

        //Gets counter on a round robin basis
        CounterDetailsDTO counter = (from counterDetails in lstcounterDetails
                                     orderby counterDetails.CounterDetail_Count
                                         ascending
                                     select counterDetails).FirstOrDefault();
        return counter;
    }
    /// <summary>
    /// Populate Loading Advice Details by booking ID
    /// </summary>
    /// <param name="bookingId">Int32:bookingId</param>
    private void PopulateLoadingAdviceDetails(int bookingId)
    {
        BookingDTO booking = ESalesUnityContainer.Container.Resolve<IBookingService>().GetBookingDetailByBookingId(bookingId, false);

        txtCustomerCode.Text = booking.Booking_Cust_Code;
        GetCustomerDetails(booking.Booking_Cust_Code);
        ddlMaterial.SelectedValue = booking.Booking_MaterialType_Id.ToString();
        CustomerMaterialMapDTO CustomerMatDetails = ESalesUnityContainer.Container.Resolve<ICustomerMaterialService>()
            .GetCustomerMaterialByCustomerAndMaterialId(Convert.ToInt32(booking.Booking_Cust_Id), Convert.ToInt32(booking.Booking_MaterialType_Id));
        if (booking.Booking_TruckType == false)
        {
            GetTruckDetail(Convert.ToString(booking.Booking_Truck_Id), false);
            ddlTruck.SelectedValue = booking.Booking_Truck_Id.ToString();
            rdStandAlone.SelectedValue = "1";
        }
        else
        {
            GetTruckDetail(booking.Booking_StandaloneTruck_RegNo, true);
            rdStandAlone.SelectedValue = "2";
        }

        txtMaxLiftQty.Text = CustomerMatDetails.Cust_Mat_AllotedQuantity.ToString();
        txtTotalIssuedQty.Text = booking.Booking_TotalIssuedQty.ToString();
        txtAdvanceAmount.Text = booking.Booking_AdvanceAmount.ToString();

        txtTotalBookingAdvance.Text = booking.Booking_TotalAdvanceAmount.ToString();
        txtBalanceAdvance.Text = booking.Booking_BalanceAmount.ToString();

        txtRejectionReason.Text = booking.Booking_RejectionReson;

        txtAdvanceAmount.ReadOnly = true;
        txtCustomerCode.ReadOnly = true;
        txtStandaloneTruck.Enabled = false;
        rdStandAlone.Enabled = false;
        btnValidate.Enabled = false;
        btnGetStandAlone.Enabled = false;
        ddlMaterial.Enabled = false;
    }

    /// <summary>
    /// Get truck details based on the type of truck selected
    /// </summary>
    /// <param name="truckNo">String: truck number</param>
    /// <param name="isAuthorized">Boolean: Indicates if authorized truck selected</param>
    /// 
    private void GetTruckDetail(string truckNo, bool isAuthorized)
    {
        if (!isAuthorized)
        {
            TruckDetails(truckNo);
        }
        else
        {
            StandaloneTruck(truckNo);
        }
    }


    //private void GetTruckDetail(string truckNo, bool isAuthorized)
    //{
    //    string customerBusinessType = string.Empty;
    //    string customerTruckType = string.Empty;
    //    string customerTruckTypeName = string.Empty;

    //    if (ViewState[Globals.StateMgmtVariables.CUSTOMERBUSINESSTYPE] != null)
    //    {
    //        if (!isAuthorized)
    //        {
    //            if (ViewState[Globals.StateMgmtVariables.CUSTOMERBUSINESSTYPE].ToString() != "Bricks ")
    //            {
    //                IList<LiftingLimitDTO> LstLimitDTO = ESalesUnityContainer.Container.Resolve<ILiftingLimit>().GetLimitList();
    //                foreach (LiftingLimitDTO l in LstLimitDTO)
    //                {
    //                    if (l.LiftingLimit_Business_Name != "Bricks ")
    //                    {
    //                        ViewState[Globals.StateMgmtVariables.CUSTOMERTRUCKTYPE] = l.LiftingLimit_TruckRegType_Id;
    //                        ViewState[Globals.StateMgmtVariables.CUSTOMERTRUCKTYPENAME] = l.LiftingLimit_TruckRegType_Name;
    //                    }

    //                }

    //                if (ViewState[Globals.StateMgmtVariables.CUSTOMERTRUCKTYPE] != null)
    //                {
    //                    customerTruckType = ViewState[Globals.StateMgmtVariables.CUSTOMERTRUCKTYPE].ToString();
    //                    customerTruckTypeName = ViewState[Globals.StateMgmtVariables.CUSTOMERTRUCKTYPENAME].ToString();

    //                    TruckDetailsDTO TruckInfo = ESalesUnityContainer.Container.Resolve<ITruckService>()
    //                         .GetTruckDetailsByTruckId(Convert.ToInt32(truckNo));

    //                    int truckType = Convert.ToInt32(TruckInfo.Truck_RegType);

    //                    if (Convert.ToInt32(customerTruckType) != truckType)
    //                    {
    //                        ucMessageBox.ShowMessage("Only " + customerTruckTypeName + " are allowed for Hardcoke Customers.");
    //                    }
    //                    else
    //                    {
    //                        TruckDetails(truckNo);
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                TruckDetails(truckNo);
    //            }
    //        }
    //        else
    //        {
    //            customerBusinessType = ViewState[Globals.StateMgmtVariables.CUSTOMERBUSINESSTYPE].ToString();

    //            if (customerBusinessType != "Bricks ")
    //            {
    //                IList<LiftingLimitDTO> LstLimitDTO = ESalesUnityContainer.Container.Resolve<ILiftingLimit>().GetLimitList();
    //                foreach (LiftingLimitDTO l in LstLimitDTO)
    //                {
    //                    if (l.LiftingLimit_Business_Name != "Bricks ")
    //                    {
    //                        ViewState[Globals.StateMgmtVariables.CUSTOMERTRUCKTYPENAME] = l.LiftingLimit_TruckRegType_Name;
    //                        ViewState[Globals.StateMgmtVariables.CUSTOMERTRUCKTYPE] = l.LiftingLimit_TruckRegType_Id;
    //                    }
    //                }

    //                if (ViewState[Globals.StateMgmtVariables.CUSTOMERTRUCKTYPE] != null)
    //                {
    //                    customerTruckType = ViewState[Globals.StateMgmtVariables.CUSTOMERTRUCKTYPE].ToString();
    //                    customerTruckTypeName = ViewState[Globals.StateMgmtVariables.CUSTOMERTRUCKTYPENAME].ToString();

    //                    StandaloneTrucksDTO StandaloneTruckDetails = ESalesUnityContainer.Container.Resolve<IStandaloneTruckService>()
    //                    .GetStandaloneTruckByRegNo(truckNo);

    //                    int truckType = Convert.ToInt32(StandaloneTruckDetails.StandaloneTruck_RegType);

    //                    if (Convert.ToInt32(customerTruckType) != truckType)
    //                    {
    //                        ucMessageBox.ShowMessage("Only " + customerTruckTypeName + " are allowed for Hardcoke Customers.");
    //                    }
    //                    else
    //                    {
    //                        StandaloneTruck(truckNo);
    //                    }
    //                }
    //            }

    //            else
    //            {
    //                StandaloneTruck(truckNo);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        ucMessageBox.ShowMessage("Please Validate Customer First.");
    //    }
    //}

    private void TruckDetails(string truckNo)
    {
        if (ESalesUnityContainer.Container.Resolve<IBookingService>().GetTruckCountForDate(Convert.ToInt32(truckNo), Convert.ToDateTime(txtBookingDate.Text).Date, 1) == 0)
        {
            TruckDetailsDTO TruckInfo = ESalesUnityContainer.Container.Resolve<ITruckService>()
                            .GetTruckDetailsByTruckId(Convert.ToInt32(truckNo));

            int truckType = Convert.ToInt32(TruckInfo.Truck_RegType);

            txtNumberofWheel.Text = TruckInfo.TruckWheeler_Type;
            txtCarryCapacity.Text = TruckInfo.TruckCarryCapacity_Type;
            txtCurrentQty.Text = TruckInfo.TruckCarryCapacity_Type;
            txtDriverDetails.Text = TruckInfo.Truck_DriverName + Environment.NewLine + TruckInfo.Truck_Address;
            btnSave.Enabled = true;
        }
        else
        {
            btnSave.Enabled = false;
            ResetFields();
            ucMessageBox.ShowMessage(Messages.DuplicateTruck);
        }
    }

    private void StandaloneTruck(string truckNo)
    {

        //Get standalone truck details from database
        StandaloneTrucksDTO StandaloneTruckDetails = ESalesUnityContainer.Container.Resolve<IStandaloneTruckService>()
                .GetStandaloneTruckByRegNo(truckNo);

        //If standalone truck does not exist, show message box
        if (StandaloneTruckDetails.StandaloneTruck_Id > 0)
        {
            if (ESalesUnityContainer.Container.Resolve<IBookingService>().GetTruckCountForDate(StandaloneTruckDetails.StandaloneTruck_Id, Convert.ToDateTime(txtBookingDate.Text).Date, 2) == 0)
            {
                ViewState[Globals.StateMgmtVariables.TRUCKID] = StandaloneTruckDetails.StandaloneTruck_Id;
                txtNumberofWheel.Text = StandaloneTruckDetails.StandaloneTruckWheeler_Type.ToString();
                txtCarryCapacity.Text = StandaloneTruckDetails.StandaloneTruckCarryCapacity_Type.ToString();
                txtCurrentQty.Text = StandaloneTruckDetails.StandaloneTruckCarryCapacity_Type.ToString();
                txtStandaloneTruck.Text = StandaloneTruckDetails.StandaloneTruck_RegNo;
                txtDriverDetails.Text = StandaloneTruckDetails.StandaloneTruck_DriverName.ToString() + Environment.NewLine +
                    StandaloneTruckDetails.StandaloneTruck_Address.ToString();
                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
                ResetFields();
                ucMessageBox.ShowMessage(Messages.DuplicateTruck);
            }
        }
        else
        {
            ucMessageBox.ShowMessage(Messages.TransporterTruckNotExists);
        }
    }

    /// <summary>
    /// Event for Ok Button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        //check Signature checkbox to verify user has viewed customer signatures
        chkSignValid.Checked = true;
    }

    #endregion
}
