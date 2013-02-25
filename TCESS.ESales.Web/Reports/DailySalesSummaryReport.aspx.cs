using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_DailySalesSummaryReport :BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucDailySalesSummaryData.Event_LoadReport += ucDailySalesSummaryData_Event_LoadReport;
        ucDailySalesSummaryReport.Event_CloseScreen += ucDailySalesSummaryReport_Event_CloseScreen;
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
        pnlDailySalesSummaryData.Visible = true;
        pnlDailySalesSummaryReport.Visible = false;
    }


    public void ucDailySalesSummaryData_Event_LoadReport(int agentId, DateTime fromDate, DateTime toDate)
    {
        pnlDailySalesSummaryData.Visible = false;
        pnlDailySalesSummaryReport.Visible = true;
        ucDailySalesSummaryReport.LoadReport(agentId, fromDate, toDate);
    }

    public void ucDailySalesSummaryReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}