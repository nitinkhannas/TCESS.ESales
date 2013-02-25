#region Using directives

using System;

#endregion

public partial class GhatoCollection_ManagePaymentCollections : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckIsUserAuthenticated();
    }
}