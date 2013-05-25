using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_ConsolidatedCustomerCollectionReport : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucConsolidatedCustomerCollectionReport.Event_CloseScreen += ucDailyBookingReportforCustomerReport_Event_CloseScreen;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
       // base.CheckIsUserAuthenticated();
        if (!IsPostBack)
        {

        }

    }

    public void ucDailyBookingReportforCustomerReport_Event_CloseScreen(object sender)
    {
        
    }
}