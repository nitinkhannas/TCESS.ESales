using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_DailyBookingReportforall : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucDailyBookingReportforallData.Event_LoadReport += ucDailyBookingReportforallDCAsData_Event_LoadReport;
        ucDailyBookingReportforallReport.Event_CloseScreen += ucDailyBookingReportforallDCAsReport_Event_CloseScreen;
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
        pnlDailyBookingReportforallData.Visible = true;
        pnlDailyBookingReportforallReport.Visible = false;
    }


    public void ucDailyBookingReportforallDCAsData_Event_LoadReport(int agentId ,DateTime fromDate, DateTime toDate)
    {
        pnlDailyBookingReportforallData.Visible = false;
        pnlDailyBookingReportforallReport.Visible = true;
        ucDailyBookingReportforallReport.LoadReport(agentId,fromDate, toDate);
    }

    public void ucDailyBookingReportforallDCAsReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}