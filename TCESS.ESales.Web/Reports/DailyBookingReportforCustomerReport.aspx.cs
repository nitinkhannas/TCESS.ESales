using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_DailyBookingReportforCustomerReport :BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucDailyBookingReportforCustomerData.Event_LoadReport += ucDailyBookingReportforCustomerData_Event_LoadReport;
        ucDailyBookingReportforCustomerReport.Event_CloseScreen += ucDailyBookingReportforCustomerReport_Event_CloseScreen;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //base.CheckIsUserAuthenticated();
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
        //Sets visibility of frames that contains user        + controls
        pnlDailyBookingReportforCustomerData.Visible = true;
        pnlDailyBookingReportforCustomerReport.Visible = false;
    }


    public void ucDailyBookingReportforCustomerData_Event_LoadReport(int custId, DateTime fromDate, DateTime toDate)
    {
        pnlDailyBookingReportforCustomerData.Visible = false;
        pnlDailyBookingReportforCustomerReport.Visible = true;
        ucDailyBookingReportforCustomerReport.LoadReport(custId, fromDate, toDate);
    }

    public void ucDailyBookingReportforCustomerReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}