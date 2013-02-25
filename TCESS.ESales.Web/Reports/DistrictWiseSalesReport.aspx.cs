using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_DistrictWiseSalesReport : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucDistrictWiseSalesData.Event_LoadReport += ucDistrictWiseSalesData_Event_LoadReport;
        ucDistrictWiseSalesReport.Event_CloseScreen += ucDistrictWiseSalesReport_Event_CloseScreen;
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
        pnlDistrictWiseSalesData.Visible = true;
        pnlDistrictWiseSalesReport.Visible = false;
    }

    public void ucDistrictWiseSalesData_Event_LoadReport(int agentId, int month)
    {
        pnlDistrictWiseSalesData.Visible = false;
        pnlDistrictWiseSalesReport.Visible = true;
        ucDistrictWiseSalesReport.LoadReport(agentId, month);
    }

    public void ucDistrictWiseSalesReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }

}