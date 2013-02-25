using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_DistrictWiseReportofInactiveCustomersReport : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucDistrictWiseReportofInactiveCustomersData.Event_LoadReport += ucDistrictWiseReportofInactiveCustomersData_LoadReport;
        ucDistrictWiseReportofInactiveCustomersReport.Event_CloseScreen += ucDistrictWiseReportofInactiveCustomersReport_Event_CloseScreen;
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
        pnlDistrictWiseReportofInactiveCustomersData.Visible = true;
        pnlDistrictWiseReportofInactiveCustomersReport.Visible = false;
    }

    public void ucDistrictWiseReportofInactiveCustomersData_LoadReport(int agentId, DateTime fromDate, DateTime toDate)
    {
        pnlDistrictWiseReportofInactiveCustomersData.Visible = false;
        pnlDistrictWiseReportofInactiveCustomersReport.Visible = true;
        ucDistrictWiseReportofInactiveCustomersReport.LoadReport(agentId, fromDate, toDate);
    }

    public void ucDistrictWiseReportofInactiveCustomersReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}