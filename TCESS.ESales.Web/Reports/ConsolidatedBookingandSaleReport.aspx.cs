using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_ConsolidatedBookingandSaleReport : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucConsolidatedBookingandSaleData.Event_LoadReport += ucConsolidatedBookingandSaleData_Event_LoadReport;
        ucConsolidatedBookingandSaleReport.Event_CloseScreen += ucConsolidatedBookingandSaleReport_Event_CloseScreen;
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
        pnlConsolidatedBookingandSaleData.Visible = true;
        pnlConsolidatedBookingandSaleReport.Visible = false;
    }

    public void ucConsolidatedBookingandSaleData_Event_LoadReport(int agentId, DateTime fromDate, DateTime toDate)
    {
        pnlConsolidatedBookingandSaleData.Visible = false;
        pnlConsolidatedBookingandSaleReport.Visible = true;
        ucConsolidatedBookingandSaleReport.LoadReport(agentId, fromDate, toDate);
    }

    public void ucConsolidatedBookingandSaleReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}