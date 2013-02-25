using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_CustomerWiseSalesReport : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucCustomerWiseSalesData.Event_LoadReport += ucCustomerWiseSalesData_Event_LoadReport;
        ucCustomerWiseSalesReport.Event_CloseScreen += ucCustomerWiseSalesReport_Event_CloseScreen;
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
        pnlCustomerWiseSalesData.Visible = true;
        pnlCustomerWiseSalesReport.Visible = false;
    }

    public void ucCustomerWiseSalesData_Event_LoadReport(int agentId, int month, int year)
    {
        pnlCustomerWiseSalesData.Visible = false;
        pnlCustomerWiseSalesReport.Visible = true;
        ucCustomerWiseSalesReport.LoadReport(agentId, month, year);
    }

    public void ucCustomerWiseSalesReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}