#region Using directives

using System;

#endregion

public partial class Reports_SMSBookingsAcceptedCompletedLapsedReport : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucSMSBookingsAcceptedCompletedLapsedData.Event_LoadReport += ucSMSBookingsAcceptedCompletedLapsedData_Event_LoadReport;
        ucSMSBookingsAcceptedCompletedLapsedReport.Event_CloseScreen += ucSMSBookingsAcceptedCompletedLapsedReport_Event_CloseScreen;
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
        pnlSMSBookingsAcceptedCompletedLapsedData.Visible = true;
        pnlSMSBookingsAcceptedCompletedLapsedReport.Visible = false;
    }

    public void ucSMSBookingsAcceptedCompletedLapsedData_Event_LoadReport(int agentId, DateTime fromDate, DateTime toDate)
    {
        pnlSMSBookingsAcceptedCompletedLapsedData.Visible = false;
        pnlSMSBookingsAcceptedCompletedLapsedReport.Visible = true;
        ucSMSBookingsAcceptedCompletedLapsedReport.LoadReport(agentId, fromDate, toDate);
    }

    public void ucSMSBookingsAcceptedCompletedLapsedReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}