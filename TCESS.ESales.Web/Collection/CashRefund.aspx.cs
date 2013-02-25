using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Collection_CashRefund : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            base.CheckIsUserAuthenticated();
            ucPaymentRefund.SetPaymentModeForUser((int)HelperClass.PaymentModes.CASH);
        }
    }
}