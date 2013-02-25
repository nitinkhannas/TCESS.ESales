#region Using directives

using System;

#endregion

public partial class Common_YesNoNotificationMessage : BaseUserControl
{
    public event YesNoMessageBoxEventHandler Event_YesButtonClicked;

    protected void Page_Load(object sender, EventArgs e)
    {
        btnNo.OnClientClick = String.Format("fnClickNo('{0}','{1}')", btnNo.UniqueID, "");
        btnYes.OnClientClick = String.Format("fnClickYes('{0}','{1}')", btnYes.UniqueID, "");
    }

    public void ShowMessage(string message)
    {
        lblMessage.Text = message;
        mdlMessageBox.Show();
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        mdlMessageBox.Hide();
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        mdlMessageBox.Hide();
        Event_YesButtonClicked(sender, e);
    }
}