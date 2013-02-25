using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Bookings_TotalBooking : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucReprint.Event_LoadReportForMoneyReceipt += ucReprint_Event_LoadReportForMoneyReceipt;
        ucReprint.Event_LoadReportForHandlingBill += ucReprint_Event_LoadReportForHandlingBill;
        ucMoneyReceiptReport.Event_CloseScreen += ucMoneyReceiptReport_Event_CloseScreen;
        ucHandleBillRpt.Event_CloseScreen += ucMoneyReceiptReport_Event_CloseScreen;
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
       // CheckIsUserAuthenticated();

        if (!IsPostBack)
        {
            ShowInitialValues();
        }  
 
    }



    /// <summary>
    /// Show Page Values when it initially Loads or Refreshes
    /// </summary>
    private void ShowInitialValues()
    {
        //Sets visibility of frames that contains user controls       
        pnlReprint.Visible = true;
        PnlMoneyReceipt.Visible = false;
        PnlHandleBillReport.Visible = false;
    }


    public void ucReprint_Event_LoadReportForMoneyReceipt(int bookingID)
    {
        pnlReprint.Visible = false;
        PnlMoneyReceipt.Visible = true;
        PnlHandleBillReport.Visible = false;
        ucMoneyReceiptReport.GetBookingDetails(bookingID);
    }

    public void ucReprint_Event_LoadReportForHandlingBill(int accountID)
    {
        pnlReprint.Visible = false;
        PnlMoneyReceipt.Visible = false;
        PnlHandleBillReport.Visible = true;
        ucHandleBillRpt.GetSettlementOfAccountDetails(accountID);
    }


    public void ucMoneyReceiptReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}