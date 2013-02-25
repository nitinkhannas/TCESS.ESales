using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_ConsolidatedAdviceReport :BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucConsolidatedAdviceData.Event_LoadReport += ucConsolidatedAdviceData_Event_LoadReport;
        ucConsolidatedAdviceReport.Event_CloseScreen += ucConsolidatedAdviceReport_Event_CloseScreen;
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
        pnlConsolidatedAdviceData.Visible = true;
        pnlConsolidatedAdviceReport.Visible = false;
    }


    public void ucConsolidatedAdviceData_Event_LoadReport(int agentId, DateTime fromDate, DateTime toDate)
    
    {
        pnlConsolidatedAdviceData.Visible = false;
        pnlConsolidatedAdviceReport.Visible = true;
        ucConsolidatedAdviceReport.LoadReport(agentId, fromDate, toDate);
    }

    public void ucConsolidatedAdviceReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}