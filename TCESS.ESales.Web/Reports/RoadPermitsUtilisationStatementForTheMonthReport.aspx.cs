using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_RoadPermitsUtilisationStatementForTheMonthReport : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucRoadPermitsUtilisationStatementForTheMonthData.Event_LoadReport += ucRoadPermitsUtilisationStatementForTheMonthData_Event_LoadReport;
        ucRoadPermitsUtilisationStatementForTheMonthReport.Event_CloseScreen += ucRoadPermitsUtilisationStatementForTheMonthReport_Event_CloseScreen;
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
        pnlRoadPermitsUtilisationStatementForTheMonthData.Visible = true;
        pnlRoadPermitsUtilisationStatementForTheMonthReport.Visible = false;
    }

    public void ucRoadPermitsUtilisationStatementForTheMonthData_Event_LoadReport(int agentId, int month, int year)
    {
        pnlRoadPermitsUtilisationStatementForTheMonthData.Visible = false;
        pnlRoadPermitsUtilisationStatementForTheMonthReport.Visible = true;
        ucRoadPermitsUtilisationStatementForTheMonthReport.LoadReport(month, year);
    }

    public void ucRoadPermitsUtilisationStatementForTheMonthReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}