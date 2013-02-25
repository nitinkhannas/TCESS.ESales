using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_PendingReport : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucPendingReportData.Event_LoadReport += ucPendingReportData_Event_LoadReport;
        ucPendingReport.Event_CloseScreen += ucPendingReport_Event_CloseScreen;
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
        pnlPendingReportData.Visible = true;
        pnlPendingReport.Visible = false;
    }

    public void ucPendingReportData_Event_LoadReport(int agentId, DateTime fromDate, DateTime toDate)
    {
        pnlPendingReportData.Visible = false;
        pnlPendingReport.Visible = true;
        ucPendingReport.LoadReport(agentId,fromDate, toDate);
    }

    public void ucPendingReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}