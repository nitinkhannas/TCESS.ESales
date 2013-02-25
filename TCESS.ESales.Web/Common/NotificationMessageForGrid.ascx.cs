#region Using directives

using System;

#endregion

public partial class Common_NotificationMessageForGrid : BaseUserControl
{
    public event OkMessageBoxEventHandler Event_OkButton;

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
        Event_OkButton(sender, e);
    }
}