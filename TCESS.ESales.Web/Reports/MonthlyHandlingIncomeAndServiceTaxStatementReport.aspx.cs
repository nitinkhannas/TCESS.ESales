using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_MonthlyHandlingIncomeAndServiceTaxStatementReport : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucMonthlyHandlingIncomeAndServiceTaxStatementData.Event_LoadReport += ucMonthlyHandlingIncomeAndServiceTaxStatementData_Event_LoadReport;
        ucMonthlyHandlingIncomeAndServiceTaxStatementReport.Event_CloseScreen += ucMonthlyHandlingIncomeAndServiceTaxStatementReport_Event_CloseScreen;
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
        pnlMonthlyHandlingIncomeAndServiceTaxStatementData.Visible = true;
        pnlMonthlyHandlingIncomeAndServiceTaxStatementReport.Visible = false;
    }

    public void ucMonthlyHandlingIncomeAndServiceTaxStatementData_Event_LoadReport(int agentId, int month, int year)
    {
        pnlMonthlyHandlingIncomeAndServiceTaxStatementData.Visible = false;
        pnlMonthlyHandlingIncomeAndServiceTaxStatementReport.Visible = true;
        ucMonthlyHandlingIncomeAndServiceTaxStatementReport.LoadReport(agentId, month, year);
    }

    public void ucMonthlyHandlingIncomeAndServiceTaxStatementReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}