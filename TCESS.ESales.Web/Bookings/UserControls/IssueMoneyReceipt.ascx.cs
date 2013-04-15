#region Using directives

using System;
using System.Web.UI;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using System.Web.UI.WebControls;
using Resources;

#endregion

public partial class Bookings_UserControls_IssueMoneyReceipt : BaseUserControl
{
    public event CloseScreenEventHandler Event_CloseScreen;
    public event ShowDataByIdEventHandler Event_PrintLoadingAdvice;

    /// <summary>
    /// page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetPaymentModeDetails();
        }
    }

    private void GetPaymentModeDetails()
    {
        string index="0";
        ddlPaymentMode.DataSource = MasterList.GetListOfPaymentMode(false);
        ddlPaymentMode.DataBind();
        ddlPaymentMode.Items.Insert(0, new ListItem(Labels.SelectPaymentMode, "0"));
        foreach (ListItem item in ddlPaymentMode.Items)
        {
            if(item.Text=="e-Collection")
            {
                index =item.Value;
            }
        }
        ddlPaymentMode.SelectedValue = index;
            //ddlPaymentMode.Items.IndexOf(ddlPaymentMode.Items.FindByValue("e-Collection")); ;
        ddlPaymentMode.Enabled = false;
    }

    /// <summary>
    /// function to get booking detail
    /// </summary>
    /// <param name="bookingId"></param>
    public void GetBookingDetails(int bookingId)
    {
        
		//Gets Booking details by booking id
        BookingDTO bookingDetail = MasterList.GetBookingDetailByBookingId(bookingId, false);

        CustomerDTO customerDetails = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetCustomerDetailsByCode(bookingDetail.Booking_Cust_Code);

        if (customerDetails.Cust_Business_Name != "Bricks ")
        {
            txtAdvAmount.ReadOnly = true;            
        }

        ViewState[Globals.StateMgmtVariables.BOOKINGID] = bookingDetail.Booking_Id;
        txtBookingNo.Text = bookingDetail.Booking_Agent_AgentShortName + "-" + Convert.ToString(bookingDetail.Booking_Id);
        txtCustName.Text = bookingDetail.Booking_Cust_UnitName;
        txtMaterialType.Text = bookingDetail.Booking_MaterialType_MaterialName;
        txtAdvAmount.Text = bookingDetail.Booking_AdvanceAmount.ToString();
        ViewState[Globals.StateMgmtVariables.ADVANCEAMOUNT] = bookingDetail.Booking_AdvanceAmount;
        txtDCAName.Text = bookingDetail.Booking_Agent_AgentName;
        txtBookingDate.Text = Convert.ToDateTime(bookingDetail.Booking_CreatedDate).ToString("dd-MMM-yyyy");
        txtTotalBookingAdvance.Text = bookingDetail.Booking_TotalAdvanceAmount.ToString();
        txtBalanceAdvance.Text=bookingDetail.Booking_BalanceAmount.ToString();
        //If registered truck
        if (bookingDetail.Booking_TruckType == false)
        {
            txtTruckNo.Text = bookingDetail.Booking_Truck_RegNo;
            txtDriverName.Text = bookingDetail.Booking_Truck_DriverName;
            txtTruckOwner.Text = bookingDetail.Booking_Truck_OwnerName;
        }
        else
        {
            txtTruckNo.Text = bookingDetail.Booking_StandaloneTruck_RegNo;
            txtDriverName.Text = bookingDetail.Booking_StandaloneTruck_DriverName;
            txtTruckOwner.Text = bookingDetail.Booking_StandaloneTruck_OwnerName;
        }
		GetPaymentModeDetails();
		txtRemarks.Text = string.Empty;
		txtAccountName.Text = string.Empty;
    }

    /// <summary>
    /// To collect the money receipt
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCollect_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            //Initialize MoneyReceiptDTO with receipt details
            MoneyReceiptDTO moneyReceiptDetails = InitializeMoneyReceiptDetails();

            //Save money receipt details in database
            int moneyReceiptId = ESalesUnityContainer.Container.Resolve<IMoneyReceiptService>()
                .SaveAndUpdateMoneyReceipt(moneyReceiptDetails);

            //Get booking details by current selected booking id
            int bookingId = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.BOOKINGID]);
            
            BookingDTO bookingDetail = MasterList.GetBookingDetailByBookingId(bookingId, false);

            //Set money receipt issued status flag to true and save booking details in database
            bookingDetail.Booking_MoneyReceiptIssued = true;
            
            ESalesUnityContainer.Container.Resolve<IBookingService>().SaveAndUpdateBookingDetail(bookingDetail);
            btnPrint.Enabled = true;
        }
    }

    /// <summary>
    /// //Initialize MoneyReceiptDTO with receipt details
    /// </summary>
    /// <returns>returns MoneyReceiptDTO object</returns>
    private MoneyReceiptDTO InitializeMoneyReceiptDetails()
    {
        MoneyReceiptDTO moneyReceiptDetails = new MoneyReceiptDTO();
        moneyReceiptDetails.MoneyReceipt_Booking_Id = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.BOOKINGID]);
        moneyReceiptDetails.MoneyReceipt_AmountPaid = Convert.ToDecimal(txtAdvAmount.Text);
        moneyReceiptDetails.MoneyReceipt_Remarks = txtRemarks.Text.Trim();
        moneyReceiptDetails.MoneyReceipt_PaymentmodeId = Convert.ToInt32(ddlPaymentMode.SelectedItem.Value);
        moneyReceiptDetails.MoneyReceipt_InstrumentNo = txtInstrumentNo.Text.Trim();
        moneyReceiptDetails.MoneyReceipt_AccountName = txtAccountName.Text.Trim();
        moneyReceiptDetails.MoneyReceipt_CreateDate = DateTime.Now;
        moneyReceiptDetails.MoneyReceipt_CreatedBy = GetCurrentUserId();
        return moneyReceiptDetails;
    }

    /// <summary>
    /// To set the controls
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReset_Click(object sender, EventArgs e)
    {
		GetPaymentModeDetails();
		txtRemarks.Text = string.Empty;
		txtInstrumentNo.Text = string.Empty;
		txtAccountName.Text = string.Empty;
    }

    /// <summary>
    /// Event for the cancel button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Closes Issue Money Receipt Screen
        Event_CloseScreen(sender);
    }

    /// <summary>
    /// To print the Report
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Event_PrintLoadingAdvice(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.BOOKINGID]));
    }

    /// <summary>
    /// To validate advance amount
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected void AdvAmount_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        if (Convert.ToDecimal(txtAdvAmount.Text.Trim()) > Convert.ToDecimal(ViewState[Globals.StateMgmtVariables.ADVANCEAMOUNT]))
        {
            args.IsValid = false;
        }
    }

    /// <summary>
    /// To validate account name if debit/credit card is selected from payment mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected void AccountName_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        if (ddlPaymentMode.SelectedItem.Text.ToUpper().Contains("CARD"))
        {
            if (string.IsNullOrEmpty(txtAccountName.Text.Trim()))
            {
                args.IsValid = false;
            }
        }
    }
}