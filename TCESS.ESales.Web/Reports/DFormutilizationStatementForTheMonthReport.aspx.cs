using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_DFormutilizationStatementForTheMonthReport : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucDFormutilizationStatementForTheMonthData.Event_LoadReport += ucDFormutilizationStatementForTheMonthData_Event_LoadReport;
        ucDFormutilizationStatementForTheMonthReport.Event_CloseScreen += ucDFormutilizationStatementForTheMonthReport_Event_CloseScreen;
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
        pnlDFormutilizationStatementForTheMonthData.Visible = true;
        pnlDFormutilizationStatementForTheMonthReport.Visible = false;
    }

    public void ucDFormutilizationStatementForTheMonthData_Event_LoadReport(int agentId,int month,int year)
    {
        pnlDFormutilizationStatementForTheMonthData.Visible = false;
        pnlDFormutilizationStatementForTheMonthReport.Visible = true;
        ucDFormutilizationStatementForTheMonthReport.LoadReport(month, year);
    }

    public void ucDFormutilizationStatementForTheMonthReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}