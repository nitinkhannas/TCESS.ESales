#region Namespace

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using Resources;

#endregion

public partial class Bookings_IssueMoneyReceipt : BasePage
{
    /// <summary>
    /// Page init event to initaize Event's
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        ucTotalMoneyReciepts.Event_ShowMoneyReceipt += ucTotalMoneyReciepts_Event_ShowMoneyReceipt;
        ucIssueMoneyReceipt.Event_CloseScreen += ucIssueMoneyReceipt_Event_CloseScreen;
        ucIssueMoneyReceipt.Event_PrintLoadingAdvice += ucIssueMoneyReceipt_Event_PrintLoadingAdvice;
        ucLoadingAdviceReport.Event_CloseScreen += ucLoadingAdviceReport_Event_CloseScreen;
    }
    /// <summary>
    /// for Loading Advice Report
    /// </summary>
    /// <param name="sender"></param>
    void ucLoadingAdviceReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
    /// <summary>
    /// Event for Print Loading Advice by booking Id
    /// </summary>
    /// <param name="bookingId">Int32:bookingId</param>
    void ucIssueMoneyReceipt_Event_PrintLoadingAdvice(int bookingId)
    {
        //Sets visibility of frames that contains user controls
        pnlTotalMoneyReceipts.Visible = false;
        pnlIssueMoneyReceipt.Visible = false;
        pnlPrintLoadingAdviceReport.Visible = true;

        ucLoadingAdviceReport.GetBookingDetails(bookingId);
    }
    /// <summary>
    /// Event for close screen and load initail value
    /// </summary>
    /// <param name="sender"></param>
    void ucIssueMoneyReceipt_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
    /// <summary>
    /// Event for Show Money Receipt by bookingId
    /// </summary>
    /// <param name="bookingId">Int32:BookingId</param>
    void ucTotalMoneyReciepts_Event_ShowMoneyReceipt(int bookingId)
    {
        //Sets visibility of frames that contains user controls
        pnlTotalMoneyReceipts.Visible = false;
        pnlPrintLoadingAdviceReport.Visible = false;
        pnlIssueMoneyReceipt.Visible = true;
        
        ucIssueMoneyReceipt.GetBookingDetails(bookingId);
        //if(((TextBox)ucIssueMoneyReceipt.FindControl("txtAdvAmount")).ReadOnly)
          //  ucMessageBox.ShowMessage("Hardcoke customer, Advance Amount is 0");
    }
    /// <summary>
    /// Event for Page Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckIsUserAuthenticated();

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
        pnlIssueMoneyReceipt.Visible = false;
        pnlPrintLoadingAdviceReport.Visible = false;
        pnlTotalMoneyReceipts.Visible = true;

        ucTotalMoneyReciepts.GetAllMoneyReceipts();
        ucTotalMoneyReciepts.GetTotalCount();
    }
}