using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_CustomerStatement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.CheckIsUserAuthenticated();
        if (!IsPostBack)
        {
            ShowInitialValues();
        }

    }
    private void ShowInitialValues()
    {
        //Sets visibility of frames that contains user        + controls
        pnlCustomerStatementData.Visible = true;
        
    }
}