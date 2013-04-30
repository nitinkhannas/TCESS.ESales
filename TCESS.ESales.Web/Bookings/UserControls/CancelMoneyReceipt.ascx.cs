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

public partial class Bookings_UserControls_CancelMoneyReceipt : BaseUserControl
{
    public event CloseScreenEventHandler Event_CloseScreen;

    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    /// <summary>
    /// Populate Money Receipt Detail by money ReceiptId
    /// </summary>
    /// <param name="moneyReceiptId"></param>
    public void PopulateMoneyReceiptDetail(int moneyReceiptId)
    {
        ResetFields();

        MoneyReceiptDTO moneyReceiptDetails = MasterList.GetMoneyReceiptById(moneyReceiptId, 0);

        if (moneyReceiptDetails.MoneyReceipt_Id > 0)
        {
            txtReceiptNo.Text = moneyReceiptDetails.MoneyReceipt_AgentShortName + "-MR-" + moneyReceiptDetails.MoneyReceipt_Id;
            txtBookingNo.Text = moneyReceiptDetails.MoneyReceipt_InvoiceNo;
            ViewState["BOOKINGID"] = moneyReceiptDetails.MoneyReceipt_Booking_Id;
            ViewState[Globals.StateMgmtVariables.CUSTOMERID] = moneyReceiptDetails.MoneyReceipt_Cust_ID;
            ViewState[Globals.StateMgmtVariables.MONEYRECEIPTID] = moneyReceiptDetails.MoneyReceipt_Id;
            txtCustName.Text = moneyReceiptDetails.MoneyReceipt_Cust_FirmName;
            txtMaterialType.Text = moneyReceiptDetails.MoneyReceipt_MaterialName;
            txtAmountPaid.Text = moneyReceiptDetails.MoneyReceipt_AmountPaid.ToString();
            txtDCAName.Text = moneyReceiptDetails.MoneyReceipt_AgentName;
            txtBookingDate.Text = Convert.ToDateTime(moneyReceiptDetails.MoneyReceipt_Booking_Date).ToString("dd MMM yyyy");
            txtTruckNo.Text = moneyReceiptDetails.MoneyReceipt_Truck_RegNo;
            txtDriverName.Text = moneyReceiptDetails.MoneyReceipt_Truck_DriverName;
            txtTruckOwner.Text = moneyReceiptDetails.MoneyReceipt_Truck_OwnerName;
            txtRemarks.Text = moneyReceiptDetails.MoneyReceipt_Remarks;
            txtRefundAmt.Text = moneyReceiptDetails.MoneyReceipt_AmountPaid.ToString();
            txtRefundAmt.ReadOnly = true;
            CheckCurrentBalance(moneyReceiptDetails.MoneyReceipt_MaterialID);
        }
    }


    private void CheckCurrentBalance(int materialType)
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
        decimal InTransitAmount = GetAmount(InTransitLoad, materialType);

        decimal currentAmount = Convert.ToDecimal(txtRefundAmt.Text);
        {
            decimal balanceAvlAmount = totalAmountCollected - (totalMaterialLiftedAmount + InTransitAmount + totalRefundAmount);
            decimal balanceAmount = balanceAvlAmount + currentAmount;
            BalanceAmt = string.Format("{0:N2}", balanceAmount);
            txtBalRefundAmount.Text = BalanceAmt;
        }
    }

    private decimal GetAmount(decimal qty, int materialType)
    {
        MaterialTypeDTO materialTypeDetails = new MaterialTypeDTO();
        materialTypeDetails = ESalesUnityContainer.Container.Resolve<IMaterialTypeService>()
            .GetMaterialTypeById(materialType);

        decimal handlingRate = Convert.ToDecimal(qty) * Convert.ToDecimal(materialTypeDetails.MaterialType_HandlingRate);
        decimal tiscoRate = Convert.ToDecimal(qty) * Convert.ToDecimal(materialTypeDetails.MaterialType_TiscoRate);
        decimal grossAmount = handlingRate + tiscoRate;
        decimal serviceTax = handlingRate * (Convert.ToDecimal(materialTypeDetails.MaterialType_ServiceTax) / 100);
        decimal educationCess = serviceTax * (Convert.ToDecimal(materialTypeDetails.MaterialType_EducationCess) / 100);
        decimal higherEducationCess = serviceTax * (Convert.ToDecimal(materialTypeDetails.MaterialType_HigherEducationCess) / 100);
        decimal netAmount = grossAmount + serviceTax + educationCess + higherEducationCess;
        return netAmount;
    }
    /// <summary>
    /// Event for save button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int bookingId = Convert.ToInt32(ViewState["BOOKINGID"]);
        int moneyReceiptId = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.MONEYRECEIPTID]);

        if (Page.IsValid)
        {
            MoneyReceiptDTO moneyReceiptDetails = MasterList.GetMoneyReceiptById(moneyReceiptId, 0);

            if (moneyReceiptDetails.MoneyReceipt_Id > 0)
            {
                moneyReceiptDetails.MoneyReceipt_RefundAmount = Convert.ToDecimal(txtRefundAmt.Text.Trim());
                moneyReceiptDetails.MoneyReceipt_CancellationRemarks = txtCancellationRemarks.Text.Trim();
                moneyReceiptDetails.MoneyReceipt_IsDeleted = true;
                moneyReceiptDetails.MoneyReceipt_LastUpdateDate = DateTime.Now;

                //Update money receipt with cancellation details
                ESalesUnityContainer.Container.Resolve<IMoneyReceiptService>().SaveAndUpdateMoneyReceipt(moneyReceiptDetails);

                BookingDTO bookingDetails = MasterList.GetBookingDetailByBookingId(bookingId, true);
                bookingDetails.Booking_MoneyReceiptIssued = false;
                bookingDetails.Booking_LastUpdatedDate = DateTime.Now;
                bookingDetails.Booking_IsDeleted = true;

                //Update booking details to re-issue the money receipt
                ESalesUnityContainer.Container.Resolve<IBookingService>().SaveAndUpdateBookingDetail(bookingDetails);
                ucMessageBoxForGrid.ShowMessage(Messages.MoneyReceiptCancelledSuccessfully);
            }
        }
    }
    /// <summary>
    /// Event for Reset button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetFields();
    }
    /// <summary>
    /// To clear control
    /// </summary>
    private void ResetFields()
    {
        txtRefundAmt.Text = string.Empty;
        txtCancellationRemarks.Text = string.Empty;
    }
    /// <summary>
    /// Event for Ok button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        //Close the screen and return to Manage Money Receipt screen
        Event_CloseScreen(sender);
    }
    /// <summary>
    /// For cancel option
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Close the screen and return to Manage Money Receipt screen
        Event_CloseScreen(sender);
    }
    /// <summary>
    /// To validate Refund Amt
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void txtRefundAmt_Validate(object source, ServerValidateEventArgs args)
    {
        if (Convert.ToDecimal(txtAmountPaid
            .Text) != Convert.ToDecimal(txtRefundAmt.Text))
        {
            args.IsValid = false;
        }
    }
}