#region Using directives

using System;
using TCESS.ESales.CommonLayer.CommonLibrary;

#endregion

public partial class Collection_CollectionSupervisorSummary : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            base.CheckIsUserAuthenticated();
            ucCollectionSupervisorSummary.SetCollectionModeForSupervisor((int)HelperClass.PaymentModes.CHEQUE,
                (int)Globals.PaymentHeader.FORSUPERVISORSCREEN);
        }
    }
}