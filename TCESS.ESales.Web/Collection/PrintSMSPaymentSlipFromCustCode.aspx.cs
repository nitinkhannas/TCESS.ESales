#region Using directives

using System;

#endregion

public partial class Collection_PrintSMSPaymentSlipFromCustCode : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            base.CheckIsUserAuthenticated();
            ucSMSPaymentPrint.SetSMSValidationMode((int)HelperClass.VerificationMode.CUSTOMERCODE);
        }

    }
}