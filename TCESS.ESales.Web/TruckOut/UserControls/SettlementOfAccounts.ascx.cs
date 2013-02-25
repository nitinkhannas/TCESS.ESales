#region Namespace

using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using Microsoft.Practices.Unity;
using TCESS.ESales.CommonLayer.CommonLibrary;
using System.Web.Security;
using TCESS.ESales.BusinessLayer.Interfaces.GhatoCollection;

#endregion

public partial class TruckOut_UserControls_SettlementOfAccounts : BaseUserControl
{
    public ShowDataByIdEventHandler Event_ShowHandlingBillReport;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetTotalCount();
        }
    }

    public void GetTotalCount()
    {
        int UserId = Convert.ToInt32(Membership.GetUser().ProviderUserKey);
        IList<object> dayCollection = ESalesUnityContainer.Container.Resolve<ISettlementOfAccountsService>().GetSettlementOfAccountsCount(UserId);
        lblCount.Text = dayCollection[0].ToString();
        lblTotalcashcollected.Text = dayCollection[1].ToString();
    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        Boolean flagMonth = false;        
        Boolean flagTSLAcceptedDate = false;
        int currentMonth = DateTime.Now.Month;  
        int currentYear = DateTime.Now.Year;
        DateTime now = DateTime.Now;
        string month = now.ToString("MMMM");        

        string Form27CCheck = ConfigurationManager.AppSettings["Form27CCheck"].ToLower();
        string Form27CActive = ConfigurationManager.AppSettings["Form27CActive"].ToLower();

        IList<Form27CDTO> form27CList = ESalesUnityContainer.Container.Resolve<IForm27CService>()
               .GetForm27CDetailsByCustIdList(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]));
        ViewState["form27ID"] = 0;
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
                    ViewState["form27ID"] = item.Form27C_Id;
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

        if (Form27CActive == "true" && Form27CCheck == "settlement")
        {
            //IList<Form27CDTO> form27CList = ESalesUnityContainer.Container.Resolve<IForm27CService>()
            //    .GetForm27CDetailsByCustIdList(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]));

            if (form27CList.Count == 0)
            {
                ucMessageBox.ShowMessage("Please submit Form 27C for the current Month.");
            }
            else
            {
                //foreach (Form27CDTO item in form27CList)
                //{
                //    if (item.ValidMonthCount == null)
                //    {
                //        item.ValidMonthCount = 0;
                //    }

                //    if (currentMonth >= item.CurrentMonth && currentMonth < item.ValidMonthCount + item.CurrentMonth)
                //    {
                //        if (item.AcceptedByTSLDate == null)
                //        {
                //            flagTSLAcceptedDate = false;
                //        }
                //        else
                //        {
                //            flagTSLAcceptedDate = true;
                //            ViewState["form27ID"] = item.Form27C_Id;
                //        }
                //        if (currentMonth == 12)
                //        {
                //            int endYear = +currentYear + 1;
                //            if (currentYear > Convert.ToInt32(item.ValidYear) && currentYear < endYear)
                //            {
                //                flagMonth = true;
                //            }
                //        }
                //        else
                //        {
                //            if (currentYear == Convert.ToInt32(item.ValidYear))
                //                flagMonth = true;
                //        }
                //    }

            }

            //if (flagMonth && flagSave == false && flagTSLAcceptedDate)
            //{
            //    ViewState["form27ID"] = form27ID;
            //}
            //else 
            if (!flagMonth || !flagTSLAcceptedDate)   // As per Arora ji 09/24/2012
            {
                ucMessageBox.ShowMessage("Form 27C for the current Month is not submitted.");
            }
            //else if (flagMonth && !flagTSLAcceptedDate)
            //{
            //    ucMessageBox.ShowMessage("Form 27C for the current Month is not Activated.");
            //}
        }
        //}
        CheckCurrentBalance();
        decimal handlingRate = Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtHandlingRate.Text);
        decimal tiscoRate = Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtTiscoRate.Text);
        decimal grossAmount = handlingRate + tiscoRate;
        decimal serviceTax = handlingRate * (Convert.ToDecimal(ViewState[Globals.StateMgmtVariables.SERVICETAX]) / 100);
        decimal educationCess = serviceTax * (Convert.ToDecimal(ViewState[Globals.StateMgmtVariables.EDUCATIONCESS]) / 100);
        decimal higherEducationCess = serviceTax * (Convert.ToDecimal(ViewState[Globals.StateMgmtVariables.HEDUCATIONCESS]) / 100);
        decimal netAmount = grossAmount + serviceTax + educationCess + higherEducationCess;
        decimal balance = netAmount - Convert.ToDecimal(txtAmtDeposited.Text);

        txtGrossAmount.Text = Convert.ToString(Math.Round(tiscoRate, 2));
        txtHndGrossAmount.Text = Convert.ToString(Math.Round(handlingRate, 2));
        txtTotalMatAmount.Text = Convert.ToString(Math.Round(tiscoRate, 2));
        txtTotalHndAmount.Text = Convert.ToString(Math.Round(netAmount, 2) - Math.Round(tiscoRate, 2));
        txtHndServiceTax.Text = Convert.ToString(Math.Round(serviceTax, 2));
        txtHndEducationCess.Text = Convert.ToString(Math.Round(educationCess, 2));
        txtHndHigherEducationCess.Text = Convert.ToString(Math.Round(higherEducationCess, 2));
        txtTotalAmount.Text = Convert.ToString(Math.Round(netAmount, 2));
        txtBalance.Text = Convert.ToString(Math.Round(balance, 2));

        //Set enabled property of save button to true
        btnSave.Enabled = true;
    }
    private void CheckCurrentBalance()
    {
        //get total deposit amount Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID])
        decimal totalAmountCollected = ESalesUnityContainer.Container.Resolve<IPaymentService>().GetPaymentMadeByCustomer(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]), Convert.ToDateTime(ConfigurationManager.AppSettings["PaymentStartDate"]), Convert.ToDateTime(ConfigurationManager.AppSettings["PaymentEndDate"]));
        //get Total exp amount
        decimal totalMaterialLiftedAmount = ESalesUnityContainer.Container.Resolve<ISettlementOfAccountsService>().GetMaterialAmountLiftedByCustomer(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]), Convert.ToDateTime(ConfigurationManager.AppSettings["PaymentStartDate"]), Convert.ToDateTime(ConfigurationManager.AppSettings["PaymentEndDate"]));

        //get Total refund amount
        decimal totalRefundAmount = ESalesUnityContainer.Container.Resolve<IPaymentService>().GetCustomerPaymentRefundList(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID])).Sum(f => f.PR_Amount);
        

        decimal rate = 0;
        decimal currentLoad = Convert.ToDecimal(txtQuantity.Text.Trim());
        decimal currentAmount = GetAmount(currentLoad);
        if (totalAmountCollected >= (totalMaterialLiftedAmount + currentAmount + totalRefundAmount))
        {
            txtAmtDeposited.Text = Convert.ToString(totalAmountCollected - totalMaterialLiftedAmount);
        }
        else
        {
            //Reset controls on page to default state
            ResetControls();
            ucMessageBox.ShowMessage("In adequate funds");
        }
    }
    private decimal GetAmount(decimal qty)
    {

        decimal handlingRate = Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtHandlingRate.Text);
        decimal tiscoRate = Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtTiscoRate.Text);
        decimal grossAmount = handlingRate + tiscoRate;
        decimal serviceTax = handlingRate * (Convert.ToDecimal(ViewState[Globals.StateMgmtVariables.SERVICETAX]) / 100);
        decimal educationCess = serviceTax * (Convert.ToDecimal(ViewState[Globals.StateMgmtVariables.EDUCATIONCESS]) / 100);
        decimal higherEducationCess = serviceTax * (Convert.ToDecimal(ViewState[Globals.StateMgmtVariables.HEDUCATIONCESS]) / 100);
        decimal netAmount = grossAmount + serviceTax + educationCess + higherEducationCess;
        return netAmount;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //Get booking details for settlement of account
        BookingDTO bookingDetails = MasterList.GetBookingDetailByBookingId(Convert.ToInt32(txtBookingNo.Text.Trim()), true);

        if (CheckExistence(Convert.ToInt32(txtBookingNo.Text.Trim()), Convert.ToInt32(txtSmsId.Text.Trim())) && bookingDetails.Booking_Agent_Id == Convert.ToInt32(base.GetAgentByUserId().UAM_Agent_Id))
        {
            if (bookingDetails.Booking_Id > 0)
            {
                ViewState[Globals.StateMgmtVariables.CUSTOMERID] = bookingDetails.Booking_Cust_Id;
                ViewState[Globals.StateMgmtVariables.BOOKINGID] = bookingDetails.Booking_Id;
                txtBookingDate.Text = Convert.ToDateTime(bookingDetails.Booking_Date).ToString("dd/MMM/yyyy");
                txtCustomerCode.Text = bookingDetails.Booking_Cust_Code;
                txtCustomerName.Text = bookingDetails.Booking_Cust_UnitName;
                txtDCAName.Text = bookingDetails.Booking_Agent_AgentName;
                txtMaterialType.Text = bookingDetails.Booking_MaterialType_MaterialName;
                txtTruckNo.Text = bookingDetails.Booking_TruckType == false ? bookingDetails.Booking_Truck_RegNo : bookingDetails.Booking_StandaloneTruck_RegNo;
                txtHandlingRate.Text = Convert.ToString(bookingDetails.Booking_MaterialType_HandlingRate);
                txtTiscoRate.Text = Convert.ToString(bookingDetails.Booking_MaterialType_TiscoRate);

                //Get advance amount deposited details
                MoneyReceiptDTO moneyReceiptDetails = ESalesUnityContainer.Container.Resolve<IMoneyReceiptService>()
                    .GetMoneyReceiptById(0, bookingDetails.Booking_Id);
                txtAmtDeposited.Text = Convert.ToString(moneyReceiptDetails.MoneyReceipt_AmountPaid);

                //Set values in viewstate
                ViewState[Globals.StateMgmtVariables.SERVICETAX] = bookingDetails.Booking_MaterialType_ServiceTax;
                ViewState[Globals.StateMgmtVariables.EDUCATIONCESS] = bookingDetails.Booking_MaterialType_EducationCess;
                ViewState[Globals.StateMgmtVariables.HEDUCATIONCESS] = bookingDetails.Booking_MaterialType_HigherEducationCess;

                ViewState[Globals.StateMgmtVariables.TISCORATE] = bookingDetails.Booking_MaterialType_TiscoRate;
                ViewState[Globals.StateMgmtVariables.CFORMRATE] = bookingDetails.Booking_MaterialType_CFormRate;
                ViewState[Globals.StateMgmtVariables.CSTRATE] = bookingDetails.Booking_MaterialType_CSTRate;
            }
            else
            {
                ResetControls();
                ucMessageBox.ShowMessage("ID Not Found");
            }
        }
        else
        {
            ResetControls();
            ucMessageBox.ShowMessage("ID Not Found");
        }
        //Set print button enabled property to false
        btnPrint.Enabled = false;
    }

    /// <summary>
    /// Reset page controls
    /// </summary>
    private void ResetControls()
    {
        txtBookingNo.Text = string.Empty;
        txtSmsId.Text = string.Empty;
        txtBookingDate.Text = string.Empty;
        txtCustomerName.Text = string.Empty;
        txtCustomerCode.Text = string.Empty;
        txtDCAName.Text = string.Empty;
        txtQuantity.Text = string.Empty;
        txtMaterialType.Text = string.Empty;
        txtTiscoRate.Text = string.Empty;
        txtTruckNo.Text = string.Empty;
        txtHandlingRate.Text = string.Empty;
        txtAmtDeposited.Text = string.Empty;
        txtGrossAmount.Text = string.Empty;
        txtFormDNo.Text = string.Empty;
        txtHologramNumber.Text = string.Empty;
        txtInvoiceNo.Text = string.Empty;
        txtRoadPermitNo.Text = string.Empty;
        txtTotalAmount.Text = string.Empty;
        txtGatePassNo.Text = string.Empty;
        txtBalance.Text = string.Empty;
        txtCFormNo.Text = string.Empty;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Event_ShowHandlingBillReport(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.ACCOUNTID]));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string Form27CCheck = ConfigurationManager.AppSettings["Form27CCheck"].ToLower();
        string Form27CActive = ConfigurationManager.AppSettings["Form27CActive"].ToLower();

        if (Form27CActive == "true" && Form27CCheck == "settlement")
        {
            // check for Valid Form 27 C 
            if (ViewState["form27ID"] != null && Convert.ToInt32(ViewState["form27ID"]) != 0)
            {
                //Initializes Settlement of Account
                SettlementOfAccount();
                
            }
            else
            {
                ucMessageBox.ShowMessage("Please submit Form 27C for the current month, Settlement of account cannot be done");
                
            }
        }
        else
        {
            SettlementOfAccount();
            
        }
    }

    /// <summary>
    /// Initializes Settlement of Account
    /// </summary>
    private void SettlementOfAccount()
    {
        SettlementOfAccountsDTO settlementOfAcct = InitializeSettlementDTO();

        int accountId = ESalesUnityContainer.Container.Resolve<ISettlementOfAccountsService>().SaveSettlementOfAccounts(settlementOfAcct);
        GetTotalCount();
        ViewState[Globals.StateMgmtVariables.ACCOUNTID] = accountId;

        btnPrint.Enabled = true;
        btnSave.Enabled = false;
        ucMessageBox.ShowMessage(Resources.Messages.SettlementOfAccountsSavedSuccessfully);
    }

    /// <summary>
    /// Initialize DTO propeties with page values
    /// </summary>
    private SettlementOfAccountsDTO InitializeSettlementDTO()
    {
        SettlementOfAccountsDTO settlementOfAcct = new SettlementOfAccountsDTO();
        settlementOfAcct.Account_Booking_Id = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.BOOKINGID]);
        settlementOfAcct.Account_Quantity = Convert.ToDecimal(txtQuantity.Text.Trim());
        settlementOfAcct.Account_CFormNo = txtCFormNo.Text.Trim();
        settlementOfAcct.Account_FormDNumber = txtFormDNo.Text.Trim();
        settlementOfAcct.Account_HGNumber = txtHologramNumber.Text.Trim();
        settlementOfAcct.Account_InvoiceNumber = txtInvoiceNo.Text.Trim();
        settlementOfAcct.Account_RoadPermitNo = txtRoadPermitNo.Text.Trim();
        settlementOfAcct.Account_GatePassNo = txtGatePassNo.Text.Trim();
        settlementOfAcct.Account_AdvanceReceived = Convert.ToDecimal(txtAmtDeposited.Text);
        settlementOfAcct.Account_TiscoRate = Convert.ToDecimal(txtTiscoRate.Text);
        settlementOfAcct.Account_HandlingServiceTax = Convert.ToDecimal(txtHndServiceTax.Text);
        settlementOfAcct.Account_HandlingRate = Convert.ToDecimal(txtHandlingRate.Text);
        settlementOfAcct.Account_HandlingECess = Convert.ToDecimal(txtHndEducationCess.Text);
        settlementOfAcct.Account_HandlingHECess = Convert.ToDecimal(txtHndHigherEducationCess.Text);
        settlementOfAcct.Account_TotalAmount = Convert.ToDecimal(txtTotalAmount.Text);
        settlementOfAcct.Account_Balance = Convert.ToDecimal(txtBalance.Text);
        settlementOfAcct.Account_CreatedBy = base.GetCurrentUserId();
        settlementOfAcct.Account_CreatedDate = DateTime.Now;
        settlementOfAcct.Account_Form27CId = Convert.ToInt32(ViewState["form27ID"]);
        return settlementOfAcct;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetControls();
    }

    protected void ddlRate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRate.SelectedValue == "1")
        {
            txtTiscoRate.Text = Convert.ToString(ViewState[Globals.StateMgmtVariables.TISCORATE]);
        }
        else if (ddlRate.SelectedValue == "2")
        {
            txtTiscoRate.Text = Convert.ToString(ViewState[Globals.StateMgmtVariables.CFORMRATE]);
        }
        else if (ddlRate.SelectedValue == "3")
        {
            txtTiscoRate.Text = Convert.ToString(ViewState[Globals.StateMgmtVariables.CSTRATE]);
        }
    }

    private bool CheckExistence(int bookingId, int smsId)
    {
        SMSRegistrationDTO smsDetails = new SMSRegistrationDTO();
        smsDetails = ESalesUnityContainer.Container.Resolve<ISMSService>().GetDetailsBySmsIdBookingId(bookingId, smsId);

        if (smsDetails.SMSReg_Booking_Id > 0)
            return true;
        else
            return false;
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
        BookingDTO bookingDetails = ESalesUnityContainer.Container.Resolve<IBookingService>().GetBookingDetailBySmsId(Convert.ToInt32(txtSmsId.Text.Trim()));
        CustomerDTO customerDetails = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetCustomerDetailsByCode(bookingDetails.Booking_Cust_Code);

        if (CheckExistence(Convert.ToInt32(txtBookingNo.Text.Trim()), Convert.ToInt32(txtSmsId.Text.Trim())) && bookingDetails.Booking_Agent_Id == Convert.ToInt32(base.GetAgentByUserId().UAM_Agent_Id))
        {
            if (bookingDetails.Booking_Id > 0)
            {
                ViewState[Globals.StateMgmtVariables.CUSTOMERID] = bookingDetails.Booking_Cust_Id;
                ViewState[Globals.StateMgmtVariables.BOOKINGID] = bookingDetails.Booking_Id;
                txtBookingDate.Text = Convert.ToDateTime(bookingDetails.Booking_Date).ToString("dd/MMM/yyyy");
                txtCustomerCode.Text = bookingDetails.Booking_Cust_Code;
                txtCustomerName.Text = bookingDetails.Booking_Cust_UnitName;
                txtDCAName.Text = bookingDetails.Booking_Agent_AgentName;
                txtMaterialType.Text = bookingDetails.Booking_MaterialType_MaterialName;
                txtTruckNo.Text = bookingDetails.Booking_TruckType == false ? bookingDetails.Booking_Truck_RegNo : bookingDetails.Booking_StandaloneTruck_RegNo;
                txtHandlingRate.Text = Convert.ToString(bookingDetails.Booking_MaterialType_HandlingRate);
                txtTiscoRate.Text = Convert.ToString(bookingDetails.Booking_MaterialType_TiscoRate);

                if (customerDetails.Cust_Business_Name != "Bricks ")
                {
                    ucMessageBox.ShowMessage("Hardcoke customer, Advance Amount is 0");
                }

                //Get advance amount deposited details
                MoneyReceiptDTO moneyReceiptDetails = ESalesUnityContainer.Container.Resolve<IMoneyReceiptService>()
                    .GetMoneyReceiptById(0, bookingDetails.Booking_Id);
                txtAmtDeposited.Text = Convert.ToString(moneyReceiptDetails.MoneyReceipt_AmountPaid);

                //Set values in viewstate
                ViewState[Globals.StateMgmtVariables.SERVICETAX] = bookingDetails.Booking_MaterialType_ServiceTax;
                ViewState[Globals.StateMgmtVariables.EDUCATIONCESS] = bookingDetails.Booking_MaterialType_EducationCess;
                ViewState[Globals.StateMgmtVariables.HEDUCATIONCESS] = bookingDetails.Booking_MaterialType_HigherEducationCess;

                ViewState[Globals.StateMgmtVariables.TISCORATE] = bookingDetails.Booking_MaterialType_TiscoRate;
                ViewState[Globals.StateMgmtVariables.CFORMRATE] = bookingDetails.Booking_MaterialType_CFormRate;
                ViewState[Globals.StateMgmtVariables.CSTRATE] = bookingDetails.Booking_MaterialType_CSTRate;
            }
            else
            {
                ResetControls();
                ucMessageBox.ShowMessage("ID Not Found");
            }
        }
        else
        {
            ResetControls();
            ucMessageBox.ShowMessage("ID Not Found");
        }
        //Set print button enabled property to false
        btnPrint.Enabled = false;
    }
}
