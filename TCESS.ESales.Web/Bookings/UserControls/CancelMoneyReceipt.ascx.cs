#region Using directives

using System;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using System.Web.UI.WebControls;
using Resources;

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
        }
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