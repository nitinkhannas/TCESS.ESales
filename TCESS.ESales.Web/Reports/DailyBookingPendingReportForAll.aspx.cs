using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_DailyBookingPendingReportForAll : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucDailyBookingPendingReportForAllData.Event_LoadReport += ucDailyBookingPendingReportForAllData_Event_LoadReport;
        ucDailyBookingPendingReportForAllReport.Event_CloseScreen += ucDailyBookingPendingReportForAllReport_Event_CloseScreen;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       // base.CheckIsUserAuthenticated();
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
        pnlDailyBookingPendingReportForAllData.Visible = true;
        pnlDailyBookingPendingReportForAllReport.Visible = false;
    }

    public void ucDailyBookingPendingReportForAllData_Event_LoadReport(int agentId, DateTime fromDate, DateTime toDate)
    {
        pnlDailyBookingPendingReportForAllData.Visible = false;
        pnlDailyBookingPendingReportForAllReport.Visible = true;
        ucDailyBookingPendingReportForAllReport.LoadReport(agentId,fromDate, toDate);
    }

    public void ucDailyBookingPendingReportForAllReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}