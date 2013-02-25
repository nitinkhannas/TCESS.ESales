using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CustomerRegistration_TruckVerification_: BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucVerifyTrucks.Event_LoadReport += ucLoadingSMSBookingData_Event_LoadReport;

         ucPrintBookingSlip.Event_CloseScreen += ucLoadingSMSBookingReport_Event_CloseScreen;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
		base.CheckIsUserAuthenticated();
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
        pnlManageTrucks.Visible = true;
        pnlManageTrucksReport.Visible = false;
      
    }
    public void ucLoadingSMSBookingData_Event_LoadReport(string TruckNo)
    {
        pnlManageTrucks.Visible = false;
        pnlManageTrucksReport.Visible = true;

        ucPrintBookingSlip.LoadReport(TruckNo);
    }
    public void ucLoadingSMSBookingReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}