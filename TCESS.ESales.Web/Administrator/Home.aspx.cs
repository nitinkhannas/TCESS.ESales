#region Using directives

using System;

#endregion

public partial class Home : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckIsUserAuthenticated();
    }
}