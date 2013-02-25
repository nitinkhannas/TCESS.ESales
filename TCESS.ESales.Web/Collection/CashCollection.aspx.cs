#region Using directives

using System;

#endregion

public partial class GhatoCashCollection_CashCollection : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            base.CheckIsUserAuthenticated();
            ucPaymentCollection.SetPaymentModeForUser((int)HelperClass.PaymentModes.CASH);
        }
    }
}