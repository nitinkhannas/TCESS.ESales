using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_CustomerStatement : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucCustomerStatementReport.Event_CloseScreen += ucCustomerStatementReport_Event_CloseScreen;
        ucCustomerStatementData.Event_LoadReport += ucCustomerStatementData_Event_LoadReport;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.CheckIsUserAuthenticated();
        if (!IsPostBack)
        {
            ShowInitialValues();
        }

    }
    public void ucCustomerStatementData_Event_LoadReport(int customerID, DateTime fromDate, DateTime toDate)
    {
        pnlCustomerStatementData.Visible = false;
        pnlCustomerStatementReport.Visible = true;
        ucCustomerStatementReport.LoadReport(customerID, fromDate, toDate);
    }
    public void ucCustomerStatementReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
    private void ShowInitialValues()
    {
        pnlCustomerStatementData.Visible = true;
        pnlCustomerStatementReport.Visible = false;
    }


}