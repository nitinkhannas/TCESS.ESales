using System;

public partial class Reports_BookingPendingReport : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucBookingPendingData.Event_LoadReport += ucBookingPendingData_Event_LoadReport;
        ucBookingPendingDataReport.Event_CloseScreen += ucBookingPendingDataReport_Event_CloseScreen;
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
        pnlBookingPendingData.Visible = true;
        pnlBookingPendingDataReport.Visible = false;
    }

    public void ucBookingPendingData_Event_LoadReport(int agentId,DateTime fromDate, DateTime toDate)
    {
        pnlBookingPendingData.Visible = false;
        pnlBookingPendingDataReport.Visible = true;
        ucBookingPendingDataReport.LoadReport(agentId,fromDate, toDate);
    }

    public void ucBookingPendingDataReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}