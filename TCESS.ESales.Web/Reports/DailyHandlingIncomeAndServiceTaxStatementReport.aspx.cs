using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Reports_DailyHandlingIncomeAndServiceTaxStatementReport : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucDailyHandlingIncomeAndServiceTaxStatementData.Event_LoadReport += ucDailyHandlingIncomeAndServiceTaxStatementData_Event_LoadReport;
        ucDailyHandlingIncomeAndServiceTaxStatementReport.Event_CloseScreen += ucDailyHandlingIncomeAndServiceTaxStatementReport_Event_CloseScreen;
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
        pnlDailyHandlingIncomeAndServiceTaxStatementData.Visible = true;
        pnlDailyHandlingIncomeAndServiceTaxStatementReport.Visible = false;
    }

    public void ucDailyHandlingIncomeAndServiceTaxStatementData_Event_LoadReport(int agentId, DateTime fromDate, DateTime toDate)
    {
        pnlDailyHandlingIncomeAndServiceTaxStatementData.Visible = false;
        pnlDailyHandlingIncomeAndServiceTaxStatementReport.Visible = true;
        ucDailyHandlingIncomeAndServiceTaxStatementReport.LoadReport(agentId, fromDate, toDate);
    }

    public void ucDailyHandlingIncomeAndServiceTaxStatementReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}