using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_DispatchCustomerwiseReport : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
	{
		ucDispatchReportData.Event_LoadReport += ucDispatchData_Event_LoadReport;
		ucDispatchReport.Event_CloseScreen += ucDispatchReport_Event_CloseScreen;
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
		pnlDispatchData.Visible = true;
		pnlDispatchReport.Visible = false;
	}

	public void ucDispatchData_Event_LoadReport(string filterText, DateTime fromDate, DateTime toDate)
	{
		pnlDispatchData.Visible = false;
		pnlDispatchReport.Visible = true;
        DropDownList ddlFilter = (DropDownList)ucDispatchReportData.FindControl("ddlFilter");
        ucDispatchReport.LoadReport(filterText, Convert.ToInt32(ddlFilter.SelectedItem.Value), fromDate, toDate);
	}

	public void ucDispatchReport_Event_CloseScreen(object sender)
	{
		ShowInitialValues();
	}
}
