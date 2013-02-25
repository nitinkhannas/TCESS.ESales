using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_CashCollectionReport : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucCashCollectionData.Event_LoadReport += ucCashCollectionData_Event_LoadReport;
        ucCashCollectionReport.Event_CloseScreen += ucCashCollectionReport_Event_CloseScreen;
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
        //Sets visibility of frames that contains user controls
        pnlCashCollectionData.Visible = true;
        pnlCashCollectionDataReport.Visible = false;
    }

    public void ucCashCollectionData_Event_LoadReport(int agentId, DateTime fromDate, DateTime toDate)
    {
        pnlCashCollectionData.Visible = false;
        pnlCashCollectionDataReport.Visible = true;
        ucCashCollectionReport.LoadReport(agentId, fromDate, toDate);
    }

    public void ucCashCollectionReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}