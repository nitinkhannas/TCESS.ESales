#region Using directives

using System;

#endregion

public partial class Reports_LoadingAdviceReport : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucLoadingAdviceData.Event_LoadReport += ucLoadingAdviceData_Event_LoadReport;
        ucLoadingAdviceReport.Event_CloseScreen += ucLoadingAdviceReport_Event_CloseScreen;
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
        pnlLoadingAdviceData.Visible = true;
        pnlLoadingAdviceReport.Visible = false;
    }

    public void ucLoadingAdviceData_Event_LoadReport(int agentId, DateTime fromDate, DateTime toDate)
    {
        pnlLoadingAdviceData.Visible = false;
        pnlLoadingAdviceReport.Visible = true;
        ucLoadingAdviceReport.LoadReport(agentId, fromDate, toDate);
    }

    public void ucLoadingAdviceReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}