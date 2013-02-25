using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_DailyLoadingAdviceIssuedReportForAll : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucDailyLoadingAdviceIssuedReportForAllData.Event_LoadReport += ucDailyLoadingAdviceIssuedReportForAllData_Event_LoadReport;
        ucDailyLoadingAdviceIssuedReportForAllReport.Event_CloseScreen += ucDailyLoadingAdviceIssuedReportForAllReport_Event_CloseScreen;
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
        pnlDailyLoadingAdviceIssuedReportForAllData.Visible = true;
        pnlDailyLoadingAdviceIssuedReportForAllReport.Visible = false;
    }


    public void ucDailyLoadingAdviceIssuedReportForAllData_Event_LoadReport(DateTime fromDate, DateTime toDate)
    {
        pnlDailyLoadingAdviceIssuedReportForAllData.Visible = false;
        pnlDailyLoadingAdviceIssuedReportForAllReport.Visible = true;
        ucDailyLoadingAdviceIssuedReportForAllReport.LoadReport(fromDate, toDate);
    }

    public void ucDailyLoadingAdviceIssuedReportForAllReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}