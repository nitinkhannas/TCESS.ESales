using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Common_NotificationMessage : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnOk.OnClientClick = String.Format("fnClickOK('{0}','{1}')", btnOk.UniqueID, "");
    }

    public void ShowMessage(string message)
    {
        lblMessage.Text = message;
        mdlMessageBox.Show();
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        mdlMessageBox.Hide();
    }
}