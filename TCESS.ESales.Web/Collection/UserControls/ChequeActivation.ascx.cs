#region Using directives

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces.GhatoCollection;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects.GhatoCollection;

#endregion

public partial class Collection_UserControls_ChequeActivation : BaseUserControl
{
    public ShowDataByIdEventHandler Event_ShowEditScreen;

    public void ShowChequeDetails()
    {
        LoadChequeDetails(string.Empty);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ////Load active cheque details
            LoadChequeDetails(string.Empty);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            ////Load cheque details based on the filter criteria
            LoadChequeDetails(txtChequeNumber.Text.Trim());
        }
    }

    protected void grdChequeActivation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Event_ShowEditScreen(Convert.ToInt32(e.CommandArgument));
    }

    protected void grdChequeActivation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdChequeActivation.PageIndex = e.NewPageIndex;

        ////Load active cheque details
        LoadChequeDetails(string.Empty);
    }

    /// <summary>
    /// Load active cheque details
    /// </summary>
    private void LoadChequeDetails(string chequeNumber)
    {
        IList<PaymentCollectionDTO> lstPaymentCollection = ESalesUnityContainer.Container.Resolve<IPaymentService>()
            .GetChequeDetailsForActivation((int)HelperClass.PaymentModes.CHEQUE, chequeNumber);

        if (lstPaymentCollection.Count > 0)
        {
            grdChequeActivation.DataSource = lstPaymentCollection;
            grdChequeActivation.DataBind();
        }
        else
        {
            base.ShowBlankRowInGrid<PaymentCollectionDTO>(grdChequeActivation);
        }
    }
}