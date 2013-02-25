#region Directives

using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using System.Collections.Specialized;

#endregion

public partial class Bookings_SMSConfirmation : BasePage
{
    # region Global variables

    string rejectionReason = string.Empty;
    string sndSms = string.Empty;
    string btnText = "Approve Last SMS";
    #endregion

    # region Events

    protected void Page_Init(object sender, EventArgs e)
    {
        //if (Request.Form["__EVENTTARGET"] != "ctl00$MainContent$ucMessageBoxForGrid$btnOk")

        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
        //else
        //ucMessageBoxForGrid.Event_OkButton -= ucMessageBoxForGrid_Event_OkButton;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillGridWithSMSRegDetails();
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsSMSAutoApprovalMode"]))
            {
                btnApproval.Visible = true;
                if (lblSmsBalance.Text != "" && Convert.ToInt32(lblSmsBalance.Text) == 1)
                { 
                    btnApproval.Visible = true; btnApproval.Text = btnText; 
                }
                btnRejectAll.Visible = true;

            }
        }
    }
    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        if (!CheckForDailyLimit())
        {
            // Control will be redirected to SMS Limit page in order to set limit first
            Response.Redirect("~/Masters/SMSBookingLimit.aspx", true);
        }
        FillGridWithSMSRegDetails();
        ucMessageBoxForGrid.Event_OkButton -= ucMessageBoxForGrid_Event_OkButton;
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void grdSMSReg_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Boolean flagTSLAcceptedDate = false;
        Boolean flagMonth = false;
        Boolean flagSave = false;
        string Form27CCheck = ConfigurationManager.AppSettings["Form27CCheck"].ToLower();
        string Form27CActive = ConfigurationManager.AppSettings["Form27CActive"].ToLower();
        int currentMonth = DateTime.Now.AddDays(1).Month;
        int currentYear = DateTime.Now.Year;
        string CustomerBusinessType = string.Empty;

        if (e.CommandName != "Page")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            SMSRegistrationDTO smsRegDetails = ESalesUnityContainer.Container.Resolve<ISMSService>().GetTodaysSMSDetailsById(Convert.ToInt32(grdSMSReg.DataKeys[row.RowIndex]["SMSReg_Id"]), DateTime.Now.Date);
            IList<SMSLimitDTO> lstSMSLimitDTO = ESalesUnityContainer.Container.Resolve<ISMSLimitService>().GetSMSLimitList(DateTime.Now.Date.AddDays(0));
            IList<SMSRegistrationDTO> lstCustomerDTO = ESalesUnityContainer.Container.Resolve<ISMSService>().GetTotalSMSDetailsForDate(DateTime.Now.Date.AddDays(0));
            IList<CustomerMaterialMapDTO> listCustMatDetails = ESalesUnityContainer.Container.Resolve<ICustomerMaterialService>().GetCustomerMaterialDetailsByCustomerId(smsRegDetails.SMSReg_CustId).ToList();
            string _nextDate = DateTime.Now.AddDays(1).ToString("dd-MMM-yyyy");
            int limit = 0;

            if (CheckFirstValue(row))
            {
                if (Form27CCheck == "sms" && Form27CActive == "true")
                {
                    IList<Form27CDTO> form27CList = ESalesUnityContainer.Container.Resolve<IForm27CService>()
                    .GetForm27CDetailsByCustIdList(smsRegDetails.SMSReg_CustId);

                    if (form27CList.Count == 0)
                    {
                        if (e.CommandName == Globals.GridCommandEvents.EDITBOOKING)
                        {
                            rejectionReason = "Form 27C for the current Month is not submitted";
                            sndSms = "Aapke code mein form 27 C jama nahin kiya gaya hai. Isliye Truck " + smsRegDetails.SMSReg_TruckNo + " Date " + smsRegDetails.SMSReg_Date.ToString("dd-MMM-yyyy") + " ka sms sweekar nahin kiya gaya hai. Agle sms se pahle form 27C awashiye jama karen. DCA Ghato";
                            SmsDelete(smsRegDetails, rejectionReason, sndSms);
                            RemoveDuplicateSMS(smsRegDetails.SMSReg_TruckNo);
                        }
                        else if (e.CommandName == Globals.GridCommandEvents.EDIT)
                        {
                            if (lstSMSLimitDTO.Count > 0)
                            {
                                limit = (from F in lstSMSLimitDTO where F.SMSLimit_IsActive == true select F.SMSLimit_Limit).FirstOrDefault();
                            }
                            AcceptSms(e.CommandName, smsRegDetails, lstCustomerDTO, limit, row);
                        }
                    }
                    else
                    {
                        foreach (Form27CDTO item in form27CList)
                        {
                            if (item.ValidMonthCount == null)
                            {
                                item.ValidMonthCount = 0;
                            }

                            if (currentMonth >= item.CurrentMonth && currentMonth < item.ValidMonthCount + item.CurrentMonth)
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
                                    if (currentYear >= Convert.ToInt32(item.ValidYear) && currentYear < endYear)
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
                            if (lstSMSLimitDTO.Count > 0)
                            {
                                limit = (from F in lstSMSLimitDTO where F.SMSLimit_IsActive == true select F.SMSLimit_Limit).FirstOrDefault();
                            }

                            AcceptSms(e.CommandName, smsRegDetails, lstCustomerDTO, limit, row);
                        }
                        else if (!flagMonth || !flagTSLAcceptedDate)    // As per Arora ji 09/24/2012
                        {
                            if (lstSMSLimitDTO.Count > 0)
                            {
                                limit = (from F in lstSMSLimitDTO where F.SMSLimit_IsActive == true select F.SMSLimit_Limit).FirstOrDefault();
                            }
                            rejectionReason = "Form 27C for the current Month is not submitted";
                            sndSms = "Aapke code mein form 27 C jama nahin kiya gaya hai. Isliye Truck " + smsRegDetails.SMSReg_TruckNo + " Date " + smsRegDetails.SMSReg_Date.ToString("dd-MMM-yyyy") + " ka sms sweekar nahin kiya gaya hai. Agle sms se pahle form 27C awashiye jama karen. DCA Ghato";
                            SmsDelete(smsRegDetails, rejectionReason, sndSms);
                            //AcceptSms(e.CommandName, smsRegDetails, lstCustomerDTO, limit, row);
                            ucMessageBoxForGrid.ShowMessage("Your booking has been rejected for reason: " + rejectionReason);
                        }
                    }
                }
                else
                {
                    if (lstSMSLimitDTO.Count > 0)
                    {
                        limit = (from F in lstSMSLimitDTO where F.SMSLimit_IsActive == true select F.SMSLimit_Limit).FirstOrDefault();
                    }
                    AcceptSms(e.CommandName, smsRegDetails, lstCustomerDTO, limit, row);
                    flagSave = true;
                }
            }
            else
            {
                GridViewRowUpdateFunctions(-1);
                ucMessageBoxForGrid.ShowMessage("Cannot Jump the Queue");
            }
        }
    }
    protected void grdSMSReg_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewRow row = (GridViewRow)grdSMSReg.Rows[e.NewEditIndex];
        if (CheckFirstValue(row))
        {
            GridViewRowUpdateFunctions(e.NewEditIndex);
        }
        else
        {
            GridViewRowUpdateFunctions(-1);
        }
    }
    protected void grdSMSReg_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridViewRowUpdateFunctions(-1);
        grdSMSReg.Columns[8].Visible = false;
        grdSMSReg.Columns[9].Visible = true;
        grdSMSReg.Columns[10].Visible = false;
    }
    protected void grdSMSReg_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (Page.IsValid)
        {
            GridViewRow row = (GridViewRow)grdSMSReg.Rows[e.RowIndex];

            SMSRegistrationDTO smsRegDetails = new SMSRegistrationDTO();
            smsRegDetails = ESalesUnityContainer.Container.Resolve<ISMSService>().GetTodaysSMSDetailsById(Convert.ToInt32(grdSMSReg.DataKeys[e.RowIndex]["SMSReg_Id"]), DateTime.Now.Date);
            smsRegDetails.SMSReg_IsDeleted = true;
            smsRegDetails.SMSReg_RejectionReason = ((DropDownList)grdSMSReg.Rows[e.RowIndex].FindControl("ddlRejectionReason")).SelectedItem.Text;
            smsRegDetails.SMSReg_LastUpdatedDate = DateTime.Now;
            ESalesUnityContainer.Container.Resolve<ISMSService>().SaveAndUpdateSMSDetails(smsRegDetails);
            RemoveDuplicateSMS(smsRegDetails.SMSReg_TruckNo);
            grdSMSReg.EditIndex = -1;
            FillGridWithSMSRegDetails();
            grdSMSReg.Columns[8].Visible = false;
            grdSMSReg.Columns[9].Visible = true;
            grdSMSReg.Columns[10].Visible = false;
            string _nextDate = DateTime.Now.AddDays(1).ToString("dd-MMM-yyyy");
            SmsUtility.SendSMSForBookings(smsRegDetails.SMSReg_Cust_PhoneNumber, "Code" + smsRegDetails.SMSReg_Cust_Code + " mein " + _nextDate + " ke liye ki gayee booking saweekar nahin ki jaa saki. Kripya agle din ke liye phir se booking Karen. Aur jaankari ke liye ghato office se sampark Karen. DCAGhato");
            ucMessageBoxForGrid.ShowMessage("Your booking has been rejected for reason: " + smsRegDetails.SMSReg_RejectionReason);
            return;
        }
    }
    protected void grdSMSReg_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSMSReg.PageIndex = e.NewPageIndex;
        FillGridWithSMSRegDetails();
    }
    protected void grdSMSReg_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // To populate Block dropdowm list for update
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsSMSAutoApprovalMode"]))
            {
                grdSMSReg.Columns[9].Visible = false;
            }

        }
    }
    #endregion

    # region Methods
    /// <summary>
    /// Accept/Reject sms and check for hardcoke customer Trucktype  
    /// </summary>
    /// <param name="truckNumber">Truck number</param>
    /// <returns></returns>
    private void AcceptSms(string commandName, SMSRegistrationDTO smsRegDetails, IList<SMSRegistrationDTO> lstCustomerDTO, int limit, GridViewRow row)
    {

        string CustomerBusinessType = string.Empty;
        string TruckTypeCheck = ConfigurationManager.AppSettings["TruckTypeCheck"].ToLower();
        if (CheckFirstValue(row))
        {
            if ((lstCustomerDTO.Count(A => A.SMSReg_BookingStatus == true) < limit))
            {
                if (commandName == Globals.GridCommandEvents.EDITBOOKING)
                {
                    if (TruckTypeCheck == "sms")
                    {
                        CustomerDTO customerDetails = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetCustomerDetailsByCode(smsRegDetails.SMSReg_Cust_Code);
                        CustomerBusinessType = customerDetails.Cust_Business_Name;
                        if (CustomerBusinessType != "Bricks ")
                        {
                            //Only for hardcoke customers
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
                                TruckVerificationDTO truckDetails = ESalesUnityContainer.Container.Resolve<ITruckService>().GetAllTruckDetails(smsRegDetails.SMSReg_TruckNo.Trim());

                                if (truckDetails.Truck_Id > 0)
                                {
                                    long truckTypes = truckDetails.type;
                                    if (Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERTRUCKTYPE]) != truckTypes)
                                    {
                                        rejectionReason = "Only " + ViewState[Globals.StateMgmtVariables.CUSTOMERTRUCKTYPENAME].ToString() + " trucks are allowed";
                                        sndSms = "Only " + ViewState[Globals.StateMgmtVariables.CUSTOMERTRUCKTYPENAME].ToString() + " are allowed for Hardcoke Customers.";
                                        SmsDelete(smsRegDetails, rejectionReason, sndSms);
                                        if (!Convert.ToBoolean(ConfigurationManager.AppSettings["IsSMSAutoApprovalMode"]))
                                        {
                                            ucMessageBoxForGrid.ShowMessage(rejectionReason);
                                        }
                                    }
                                    else
                                    {
                                        SmsVerification(smsRegDetails);
                                    }
                                }

                                    // Sms send on Invalid truck or truck not registered
                                else
                                {
                                    rejectionReason = "Truck not registered";
                                    sndSms = "Only " + ViewState[Globals.StateMgmtVariables.CUSTOMERTRUCKTYPENAME].ToString() + " are allowed for Hardcoke Customers.";
                                    SmsDelete(smsRegDetails, rejectionReason, sndSms);
                                    if (!Convert.ToBoolean(ConfigurationManager.AppSettings["IsSMSAutoApprovalMode"]))
                                    {
                                        ucMessageBoxForGrid.ShowMessage(rejectionReason);
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Only for Bricks customers
                            SmsVerification(smsRegDetails);
                        }
                    }
                    else
                    {
                        SmsVerification(smsRegDetails);
                    }
                }
                else if (commandName == Globals.GridCommandEvents.EDIT)
                {
                    grdSMSReg.Columns[8].Visible = true;
                    grdSMSReg.Columns[9].Visible = false;
                    grdSMSReg.Columns[10].Visible = true;
                }
            }
            else if (commandName == Globals.GridCommandEvents.EDIT || commandName == Globals.GridCommandEvents.EDITBOOKING)
            {
                grdSMSReg.Columns[8].Visible = false;
                grdSMSReg.Columns[9].Visible = true;
                grdSMSReg.Columns[10].Visible = false;
                smsRegDetails.SMSReg_IsDeleted = true;
                smsRegDetails.SMSReg_RejectionReason = "Booking Closed for the Day";
                smsRegDetails.SMSReg_LastUpdatedDate = DateTime.Now;
                ESalesUnityContainer.Container.Resolve<ISMSService>().SaveAndUpdateSMSDetails(smsRegDetails);
                SmsUtility.SendSMSForBookings(smsRegDetails.SMSReg_Cust_PhoneNumber, "Aaj ke liye booking puri ho gayee hai. Code " + smsRegDetails.SMSReg_Cust_Code + " mein Truck " + smsRegDetails.SMSReg_TruckNo + " ke liye bheja SMS ab laggo nahin raha. Kripya agle din ke liye phir se booking karen. DCAGhato");
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsSMSAutoApprovalMode"]))
                {
                    ucMessageBoxForGrid.ShowMessage("Aaj aapki SMS Booking sweekar nahin karne ke liye hamen khed hai. Kripya booking ke liye phir se SMS karen- DCAGhato ");
                }
                RemoveDuplicateSMS(smsRegDetails.SMSReg_TruckNo);
                return;
            }
        }
        else
        {
            GridViewRowUpdateFunctions(-1);
            ucMessageBoxForGrid.ShowMessage("Cannot Jump the Queue");
        }
    }
    private void FillGridWithSMSRegDetails()
    {
        IList<SMSRegistrationDTO> lstSMSReg = ESalesUnityContainer.Container.Resolve<ISMSService>().GetTodaysSMSDetails();


        if (!CheckForDailyLimit())
        {
            ucMessageBoxForGrid.ShowMessage("SMS Limit not set for today. Please set the limit");
        }
        if (lstSMSReg.Count > 0)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsSMSAutoApprovalMode"]))
            {
                grdSMSReg.PageSize = lstSMSReg.Count;
            }
            grdSMSReg.DataSource = lstSMSReg;
            grdSMSReg.DataBind();
           

        }
        else
        {
            FillBlankGrid();
        }

        GetSmsDetails();
        //if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsSMSAutoApprovalMode"]))
        //{
        //    if (lblSmsBalance.Text != "" && Convert.ToInt32(lblSmsBalance.Text) == 1 || lstSMSReg.Count == 1)
        //    { btnApproval.Visible = true; btnApproval.Text = btnText; }
        //}

    }
    private void FillBlankGrid()
    {
        ShowBlankRowInGrid<SMSRegistrationDTO>(grdSMSReg);
    }
    /// <summary>
    /// Will check if the truck has got a booking for the day
    /// </summary>
    /// <param name="truckNumber">Truck number</param>
    /// <returns></returns>
    private bool DuplicityCheck(string truckNumber)
    {
        int countSMSReg = ESalesUnityContainer.Container.Resolve<ISMSService>().GetTodaysCountByTruckId(truckNumber);
        if (countSMSReg > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    /// <summary>
    /// Will check if total quantity has excised 
    /// </summary>
    /// <param name="custId">customer ID</param>
    /// <returns></returns>
    private bool QuantityCheck(int custId)
    {
        IList<CustomerMaterialMapDTO> listCustMatDetails = ESalesUnityContainer.Container.Resolve<ICustomerMaterialService>().GetCustomerMaterialDetailsByCustomerId(custId).ToList();

        CustomerMaterialMapDTO CustomerMatDetails = ESalesUnityContainer.Container.Resolve<ICustomerMaterialService>()
                .GetCustomerMaterialByCustomerAndMaterialId(Convert.ToInt32(custId),
                listCustMatDetails[0].Cust_Mat_MaterialId);

        IList<object> obj = ESalesUnityContainer.Container.Resolve<IBookingService>()
                    .GetTotalIssuedQty(custId, listCustMatDetails[0].Cust_Mat_MaterialId, DateTime.Now.Date).ToList();

        if (Convert.ToInt32(CustomerMatDetails.Cust_Mat_AllotedQuantity) > Convert.ToInt32(obj[0]))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// Will check if the number of truck for the day has not reached
    /// </summary>
    /// <param name="custId"></param>
    /// <returns></returns>
    private bool TruckCheck(int custId)
    {
        IList<CustomerMaterialMapDTO> listCustMatDetails = ESalesUnityContainer.Container.Resolve<ICustomerMaterialService>().GetCustomerMaterialDetailsByCustomerId(custId).ToList();

        CustomerMaterialMapDTO CustomerMatDetails = ESalesUnityContainer.Container.Resolve<ICustomerMaterialService>()
                .GetCustomerMaterialByCustomerAndMaterialId(Convert.ToInt32(custId),
                listCustMatDetails[0].Cust_Mat_MaterialId);
        int countSMSReg = ESalesUnityContainer.Container.Resolve<ISMSService>().GetTodaysCountByCustId(custId);

        if (CustomerMatDetails.Cust_Mat_LiftingLimit > countSMSReg)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool BookingIntervalCheck(int custId)
    {
        IList<CustomerMaterialMapDTO> listCustMatDetails = ESalesUnityContainer.Container.Resolve<ICustomerMaterialService>().GetCustomerMaterialDetailsByCustomerId(custId).ToList();
        List<SMSRegistrationDTO> listBookingDates = ESalesUnityContainer.Container.Resolve<ISMSService>().GetLastBookingDateByCustId(custId).ToList();
        if (listBookingDates.Count > 0)
        {
            DateTime lastBookingDate = (from F in listBookingDates select F.SMSReg_Date).First();
            DateTime bookingInterval = lastBookingDate.Date.AddDays(Convert.ToInt32(listCustMatDetails[0].Cust_Mat_Timeinterval));

            if (bookingInterval <= DateTime.Now.Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }
    /// <summary>
    /// Delete entry from Sms Registraion with Rejection Reason 
    /// </summary>
    /// <param></param>
    /// <returns></returns>
    private void SmsDelete(SMSRegistrationDTO smsRegDetails, string rejectionReason, string sendSms)
    {
        smsRegDetails.SMSReg_IsDeleted = true;
        smsRegDetails.SMSReg_RejectionReason = rejectionReason;
        smsRegDetails.SMSReg_LastUpdatedDate = DateTime.Now;
        ESalesUnityContainer.Container.Resolve<ISMSService>().SaveAndUpdateSMSDetails(smsRegDetails);
        if (sendSms != string.Empty)
        {
            SmsUtility.SendSMSForBookings(smsRegDetails.SMSReg_Cust_PhoneNumber, sendSms);
        }
        if (!Convert.ToBoolean(ConfigurationManager.AppSettings["IsSMSAutoApprovalMode"]))
        {
            ucMessageBoxForGrid.ShowMessage(rejectionReason);
        }
    }
    /// <summary>
    /// Check for SMS Duplicacy, Truck Validity, Quantity, Booking time Interval 
    /// </summary>
    /// <param></param>
    /// <returns></returns>
    private void SmsVerification(SMSRegistrationDTO smsRegDetails)
    {
        string _nextDate = DateTime.Now.AddDays(1).ToString("dd-MMM-yyyy");

        if (DuplicityCheck(smsRegDetails.SMSReg_TruckNo) && TruckCheck(smsRegDetails.SMSReg_CustId) && QuantityCheck(smsRegDetails.SMSReg_CustId) && BookingIntervalCheck(smsRegDetails.SMSReg_CustId))
        {
            smsRegDetails.SMSReg_BookingStatus = true;
            smsRegDetails.SMSReg_LastUpdatedDate = DateTime.Now;
            int bookingId = ESalesUnityContainer.Container.Resolve<ISMSService>().SaveAndUpdateSMSDetails(smsRegDetails);
           // SmsUtility.SendSMSForBookings(smsRegDetails.SMSReg_Cust_PhoneNumber, "Your booking ID: " + bookingId + " for " + _nextDate + " for Truck No " + smsRegDetails.SMSReg_TruckNo + " . Please show this message for booking which will be allowed if valid Form27C for the month is deposited. DCA Ghato");
            SmsUtility.SendSMSForBookings(smsRegDetails.SMSReg_Cust_PhoneNumber, "ID:" + bookingId + " T " + smsRegDetails.SMSReg_TruckNo + " Dt " + _nextDate + " Bkg allowed on valid Form27C; Truck No n RC and balance Qty in code. Pls show this SMS at counter for Booking. MMLGhato.");
            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["IsSMSAutoApprovalMode"]))
            {
                ucMessageBoxForGrid.ShowMessage("Your booking ID: " + bookingId + " for " + _nextDate + " for Truck No " + smsRegDetails.SMSReg_TruckNo + ". Please show this message for booking which will be allowed if valid Form27C for the month is deposited");
            }
            RemoveDuplicateSMS(smsRegDetails.SMSReg_TruckNo);
        }
        else
        {
            if (!(DuplicityCheck(smsRegDetails.SMSReg_TruckNo)))
            {
                rejectionReason = "Duplicate Entry";
                sndSms = string.Empty;
                SmsDelete(smsRegDetails, rejectionReason, sndSms);
                return;
            }
            else if (!(TruckCheck(smsRegDetails.SMSReg_CustId)))
            {
                rejectionReason = "Daily truck lifting limit exceeded";
                sndSms = "Daily truck lifting limit exceeded. DCA Ghato";
                SmsDelete(smsRegDetails, rejectionReason, sndSms);
                RemoveDuplicateSMS(smsRegDetails.SMSReg_TruckNo);
                return;
            }
            else if (!(QuantityCheck(smsRegDetails.SMSReg_CustId)))
            {
                rejectionReason = "You have lifted the allocated quantity";
                sndSms = "You have lifted the allocated quantity. DCA Ghato";
                SmsDelete(smsRegDetails, rejectionReason, sndSms);
                RemoveDuplicateSMS(smsRegDetails.SMSReg_TruckNo);
                return;
            }
            else if (!(BookingIntervalCheck(smsRegDetails.SMSReg_CustId)))
            {
                IList<CustomerMaterialMapDTO> listCustMatDetails = ESalesUnityContainer.Container.Resolve<ICustomerMaterialService>().GetCustomerMaterialDetailsByCustomerId(smsRegDetails.SMSReg_CustId).ToList();
                List<SMSRegistrationDTO> listBookingDates = ESalesUnityContainer.Container.Resolve<ISMSService>().GetLastBookingDateByCustId(smsRegDetails.SMSReg_CustId).ToList();
                DateTime lastBookingDate = (from F in listBookingDates select F.SMSReg_Date).First();
                DateTime bookingInterval = lastBookingDate.Date.AddDays(Convert.ToInt32(listCustMatDetails[0].Cust_Mat_Timeinterval));

                rejectionReason = "You can make next booking only after " + Convert.ToString(bookingInterval.Date.Day - DateTime.Now.Date.Day) + " days";
                sndSms = "Code" + smsRegDetails.SMSReg_Cust_Code + " mein " + _nextDate + " ke liye ki gayee booking saweekar nahin ki jaa saki. Kripya agle din ke liye phir se booking Karen. Aur jaankari ke liye ghato office se sampark Karen. DCAGhato";
                SmsDelete(smsRegDetails, rejectionReason, sndSms);
                RemoveDuplicateSMS(smsRegDetails.SMSReg_TruckNo);
                return;
            }
        }
    }
    private bool CheckFirstValue(GridViewRow row)
    {
        int id = Convert.ToInt32(((Label)row.FindControl("lblAutoColumn")).Text);
        if (id == 1)
        {
            return true;
        }
        else
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsSMSAutoApprovalMode"]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    /// <summary>
    /// Will check whether daily SMS limit is set or not
    /// </summary>
    /// <param></param>
    /// <returns></returns>
    private bool CheckForDailyLimit()
    {
        int limit = 0;
        IList<SMSLimitDTO> lstSMSLimitDTO = ESalesUnityContainer.Container.Resolve<ISMSLimitService>().GetSMSLimitList(DateTime.Now.Date.AddDays(0));
        if (lstSMSLimitDTO.Count > 0)
        {
            limit = (from F in lstSMSLimitDTO where F.SMSLimit_IsActive == true select F.SMSLimit_Limit).FirstOrDefault();
        }
        if (limit == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    /// <summary>
    /// Will get the details like SMSReceived, Daily SMS Limit, SMS Accepted and SMS Balance
    /// </summary>
    /// <param></param>
    /// <returns></returns>
    private void GetSmsDetails()
    {

        IList<SMSLimitDTO> lstSMSLimitDTO = ESalesUnityContainer.Container.Resolve<ISMSLimitService>().GetSMSLimitList(DateTime.Now.Date.AddDays(0));
        IList<SMSRegistrationDTO> lstCustomerDTO = ESalesUnityContainer.Container.Resolve<ISMSService>().GetTotalSMSDetailsForDate(DateTime.Now.Date.AddDays(0));
        bool isToUpdate = false;

        lblSmsReceived.Text = Convert.ToString(lstCustomerDTO.Count);
        lblSmsAccepted.Text = Convert.ToString(lstCustomerDTO.Count(F => F.SMSReg_BookingStatus == true));
        int limit = 0;
        if (lstSMSLimitDTO.Count > 0)
        {
            limit = (from F in lstSMSLimitDTO where F.SMSLimit_IsActive == true select F.SMSLimit_Limit).FirstOrDefault();
        }
        lblSmsLimit.Text = Convert.ToString(limit);
        lblSmsBalance.Text = Convert.ToString(limit - Convert.ToInt32(lblSmsAccepted.Text));

        isToUpdate = Convert.ToInt32(lblSmsBalance.Text) == 0;

        if (Convert.ToInt32(lblSmsBalance.Text) == 0 && Convert.ToInt32(lblSmsLimit.Text) != 0 && isToUpdate)
        {
            BulkReject();
        }
    }
    private void GridViewRowUpdateFunctions(int editIndex)
    {
        grdSMSReg.EditIndex = editIndex;
        FillGridWithSMSRegDetails();
    }
    private void BulkReject()
    {

        IList<SMSRegistrationDTO> lstSMSReg = ESalesUnityContainer.Container.Resolve<ISMSService>().GetTodaysSMSDetails();

        ArrayList customerPhoneNumber = new ArrayList();

        lstSMSReg.ToList().ForEach(k =>
        {

            k.SMSReg_IsDeleted = true;
            k.SMSReg_RejectionReason = "Booking Closed for the Day";
            k.SMSReg_LastUpdatedDate = DateTime.Now;
            SMSRegistrationDTO smsRegDetails = ESalesUnityContainer.Container.Resolve<ISMSService>().
                 GetTodaysSMSDetailsById(k.SMSReg_Id, DateTime.Now.Date);
            customerPhoneNumber.Add(smsRegDetails.SMSReg_Cust_PhoneNumber);
            SmsUtility.SendSMSForBookings(smsRegDetails.SMSReg_Cust_PhoneNumber, "Aaj ke liye booking puri ho gayee hai. Code " + smsRegDetails.SMSReg_Cust_Code + " mein Truck " + smsRegDetails.SMSReg_TruckNo + " ke liye bheja SMS ab laggo nahin raha. Kripya agle din ke liye phir se booking karen. DCAGhato");

        });

        if (lstSMSReg.Count > 0)
        {
            bool lstSmsDetail = ESalesUnityContainer.Container.Resolve<ISMSService>().SaveAndUpdateSMSDetailsList(lstSMSReg.ToList());

            if (lstSmsDetail)
            {
                if (!Convert.ToBoolean(ConfigurationManager.AppSettings["IsSMSAutoApprovalMode"]))
                {
                    ucMessageBoxForGrid.ShowMessage("Aaj aapki SMS Booking sweekar nahin karne ke liye hamen khed hai. Kripya booking ke liye phir se SMS karen- DCAGhato ");
                }
            }
        }


        //for (int i = 0; i < grdSMSReg.Rows.Count; i++)
        //{
        //    if ((Convert.ToInt32(grdSMSReg.DataKeys[i]["SMSReg_Id"])) != 0)
        //    {
        //        _rowsDeleted = _rowsDeleted + 1;
        //        SMSRegistrationDTO smsRegDetails = ESalesUnityContainer.Container.Resolve<ISMSService>().
        //            GetTodaysSMSDetailsById(Convert.ToInt32(grdSMSReg.DataKeys[i]["SMSReg_Id"]), DateTime.Now.Date);
        //        grdSMSReg.Columns[8].Visible = false;
        //        grdSMSReg.Columns[9].Visible = true;
        //        grdSMSReg.Columns[10].Visible = false;
        //        smsRegDetails.SMSReg_IsDeleted = true;
        //        smsRegDetails.SMSReg_RejectionReason = "Booking Closed for the Day";
        //        smsRegDetails.SMSReg_LastUpdatedDate = DateTime.Now;

        //        ESalesUnityContainer.Container.Resolve<ISMSService>().SaveAndUpdateSMSDetails(smsRegDetails);
        //        SmsUtility.SendSMSForBookings(smsRegDetails.SMSReg_Cust_PhoneNumber, "Aaj ke liye booking puri ho gayee hai. Code " + smsRegDetails.SMSReg_Cust_Code + " mein Truck " + smsRegDetails.SMSReg_TruckNo + " ke liye bheja SMS ab laggo nahin raha. Kripya agle din ke liye phir se booking karen. DCAGhato");
        //    }
        //}
        //if (_rowsDeleted > 0)
        //{
        //    ucMessageBoxForGrid.ShowMessage("Aaj aapki SMS Booking sweekar nahin karne ke liye hamen khed hai. Kripya booking ke liye phir se SMS karen- DCAGhato ");
        //}
    }
    private void RemoveDuplicateSMS(string truckno)
    {
        ESalesUnityContainer.Container.Resolve<ISMSService>().GetTodaysSMSDetailsByTruckNo(truckno);
    }
    #endregion

    #region AutoAccepect

    protected void btnRejectAll_Click(object sender, EventArgs e)
    {
        BulkReject();
        Response.Redirect(Request.Url.AbsoluteUri); 
    }
    protected void btnApproval_Click(object sender, EventArgs e)
    {
        IList<SMSRegistrationDTO> lstSMSReg = GetSMSAsPerPriority();

        if (CheckForDailyLimit())
        {
            if (lstSMSReg.Count > 0)
            {
                for (int i = 0; i < lstSMSReg.Count; i++)
                {
                    if (Convert.ToInt32(lblSmsBalance.Text) == 1 && btnApproval.Text != btnText)
                    {
                        btnApproval.Visible = true;
                        btnApproval.Text = btnText;
                        break;
                    }
                    else if (Convert.ToInt32(lblSmsBalance.Text) == 0 && btnApproval.Text == btnText)
                    {
                        BulkReject();
                        break;
                    }

                    foreach (GridViewRow grow in grdSMSReg.Rows)
                    {
                        if (Convert.ToInt32(grdSMSReg.DataKeys[grow.RowIndex]["SMSReg_Id"]) == Convert.ToInt32(lstSMSReg[i].SMSReg_Id))
                        {
                            AutoApproveSMS(grow, Globals.GridCommandEvents.EDITBOOKING, Convert.ToInt32(lstSMSReg[i].SMSReg_Id));
                            break;
                        }
                    }

                    FillGridWithSMSRegDetails();
                }

            }
        }
        Response.Redirect(Request.Url.AbsoluteUri);

    }

    private void AutoApproveSMS(GridViewRow row, string piCommand, int piSMSReg_Id)
    {
        {
            Boolean flagTSLAcceptedDate = false;
            Boolean flagMonth = false;
            Boolean flagSave = false;
            string Form27CCheck = ConfigurationManager.AppSettings["Form27CCheck"].ToLower();
            string Form27CActive = ConfigurationManager.AppSettings["Form27CActive"].ToLower();
            int currentMonth = DateTime.Now.AddDays(1).Month;
            int currentYear = DateTime.Now.Year;
            string CustomerBusinessType = string.Empty;
            //GridViewRow row = null;
            if (piCommand != "Page")
            {


                //GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                SMSRegistrationDTO smsRegDetails = ESalesUnityContainer.Container.Resolve<ISMSService>().GetTodaysSMSDetailsById(piSMSReg_Id, DateTime.Now.Date);
                IList<SMSLimitDTO> lstSMSLimitDTO = ESalesUnityContainer.Container.Resolve<ISMSLimitService>().GetSMSLimitList(DateTime.Now.Date.AddDays(0));
                IList<SMSRegistrationDTO> lstCustomerDTO = ESalesUnityContainer.Container.Resolve<ISMSService>().GetTotalSMSDetailsForDate(DateTime.Now.Date.AddDays(0));
                IList<CustomerMaterialMapDTO> listCustMatDetails = ESalesUnityContainer.Container.Resolve<ICustomerMaterialService>().GetCustomerMaterialDetailsByCustomerId(smsRegDetails.SMSReg_CustId).ToList();
                string _nextDate = DateTime.Now.AddDays(1).ToString("dd-MMM-yyyy");
                int limit = 0;

                //if (CheckFirstValue(row))
                //{
                if (Form27CCheck == "sms" && Form27CActive == "true")
                {
                    IList<Form27CDTO> form27CList = ESalesUnityContainer.Container.Resolve<IForm27CService>()
                    .GetForm27CDetailsByCustIdList(smsRegDetails.SMSReg_CustId);

                    if (form27CList.Count == 0)
                    {
                        if (piCommand == Globals.GridCommandEvents.EDITBOOKING)
                        {
                            rejectionReason = "Form 27C for the current Month is not submitted";
                            sndSms = "Aapke code mein form 27 C jama nahin kiya gaya hai. Isliye Truck " + smsRegDetails.SMSReg_TruckNo + " Date " + smsRegDetails.SMSReg_Date.ToString("dd-MMM-yyyy") + " ka sms sweekar nahin kiya gaya hai. Agle sms se pahle form 27C awashiye jama karen. DCA Ghato";
                            SmsDelete(smsRegDetails, rejectionReason, sndSms);
                            RemoveDuplicateSMS(smsRegDetails.SMSReg_TruckNo);
                        }
                        else if (piCommand == Globals.GridCommandEvents.EDIT)
                        {
                            if (lstSMSLimitDTO.Count > 0)
                            {
                                limit = (from F in lstSMSLimitDTO where F.SMSLimit_IsActive == true select F.SMSLimit_Limit).FirstOrDefault();
                            }
                            AcceptSms(piCommand, smsRegDetails, lstCustomerDTO, limit, row);
                        }
                    }
                    else
                    {
                        foreach (Form27CDTO item in form27CList)
                        {
                            if (item.ValidMonthCount == null)
                            {
                                item.ValidMonthCount = 0;
                            }

                            if (currentMonth >= item.CurrentMonth && currentMonth < item.ValidMonthCount + item.CurrentMonth)
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
                                    if (currentYear >= Convert.ToInt32(item.ValidYear) && currentYear < endYear)
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
                            if (lstSMSLimitDTO.Count > 0)
                            {
                                limit = (from F in lstSMSLimitDTO where F.SMSLimit_IsActive == true select F.SMSLimit_Limit).FirstOrDefault();
                            }

                            AcceptSms(piCommand, smsRegDetails, lstCustomerDTO, limit, row);
                        }
                        else if (!flagMonth || !flagTSLAcceptedDate)    // As per Arora ji 09/24/2012
                        {
                            if (lstSMSLimitDTO.Count > 0)
                            {
                                limit = (from F in lstSMSLimitDTO where F.SMSLimit_IsActive == true select F.SMSLimit_Limit).FirstOrDefault();
                            }
                            rejectionReason = "Form 27C for the current Month is not submitted";
                            sndSms = "Aapke code mein form 27 C jama nahin kiya gaya hai. Isliye Truck " + smsRegDetails.SMSReg_TruckNo + " Date " + smsRegDetails.SMSReg_Date.ToString("dd-MMM-yyyy") + " ka sms sweekar nahin kiya gaya hai. Agle sms se pahle form 27C awashiye jama karen. DCA Ghato";
                            SmsDelete(smsRegDetails, rejectionReason, sndSms);
                            //AcceptSms(e.CommandName, smsRegDetails, lstCustomerDTO, limit, row);
                            //ucMessageBoxForGrid.ShowMessage("Your booking has been rejected for reason: " + rejectionReason);
                        }
                    }
                }
                else
                {
                    if (lstSMSLimitDTO.Count > 0)
                    {
                        limit = (from F in lstSMSLimitDTO where F.SMSLimit_IsActive == true select F.SMSLimit_Limit).FirstOrDefault();
                    }
                    AcceptSms(piCommand, smsRegDetails, lstCustomerDTO, limit, row);
                    flagSave = true;
                }
                //}
                //else
                //{
                //    GridViewRowUpdateFunctions(-1);
                //    ucMessageBoxForGrid.ShowMessage("Cannot Jump the Queue");
                //}
            }
        }
    }

    private IList<SMSRegistrationDTO> GetSMSAsPerPriority()
    {
        IList<SMSRegistrationDTO> lstSMSReg = new List<SMSRegistrationDTO>();
        if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsSMSPriorityMode"]))
        {
            lstSMSReg = ESalesUnityContainer.Container.Resolve<ISMSService>().GetTodaysSMSDetails().OrderByDescending(sms => sms.SMSReg_CustomerBusinessType).ThenBy(smsreg => smsreg.SMSReg_Booking_Id).ToList<SMSRegistrationDTO>();
        }
        else
        {
            lstSMSReg = ESalesUnityContainer.Container.Resolve<ISMSService>().GetTodaysSMSDetails();

        }
        return lstSMSReg;
    }

    #endregion

}