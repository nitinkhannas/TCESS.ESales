using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_MonthlySalesStatementandReconciliationReport : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucMonthlySalesStatementandReconciliationData.Event_LoadReport += ucMonthlySalesStatementandReconciliationData_Event_LoadReport;
        ucMonthlySalesStatementandReconciliationReport.Event_CloseScreen += ucMonthlySalesStatementandReconciliationReport_Event_CloseScreen;
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
        pnlMonthlySalesStatementandReconciliationData.Visible = true;
        pnlMonthlySalesStatementandReconciliationReport.Visible = false;
    }

    public void ucMonthlySalesStatementandReconciliationData_Event_LoadReport(DateTime fromDate, int month)
    {
        pnlMonthlySalesStatementandReconciliationData.Visible = false;
        pnlMonthlySalesStatementandReconciliationReport.Visible = true;
        ucMonthlySalesStatementandReconciliationReport.LoadReport(fromDate, month);
    }

    public void ucMonthlySalesStatementandReconciliationReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}