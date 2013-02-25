#region Using directives

using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces.GhatoCollection;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects.GhatoCollection;

#endregion

public partial class GhatoCollection_ChequePayments : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            base.CheckIsUserAuthenticated();

            ////Load payment transit details to show header values
            LoadPaymentTransitDetails();

            ////Load consolidated report 
            ////for all the transactions completed in a day from each active counter
            GetConsolidatedReport();
        }
    }

    /// <summary>
    /// Load consolidated report 
    /// for all the transactions completed in a day from each active counter
    /// </summary>
    private void GetConsolidatedReport()
    {
        IList<ConsolidatedReportDTO> lstConsolidatedReport = ESalesUnityContainer.Container.Resolve<IPaymentService>().GetReportForChequePayments();

        if (lstConsolidatedReport.Count > 0)
        {
            grdConsolidatedReport.DataSource = lstConsolidatedReport;
            grdConsolidatedReport.DataBind();
        }
        else
        {
            base.ShowBlankRowInGrid<ConsolidatedReportDTO>(grdConsolidatedReport);
        }
    }

    /// <summary>
    /// Load payment transit details to show header values
    /// </summary>
    private void LoadPaymentTransitDetails()
    {
        IList<object> lstPaymentTransit = ESalesUnityContainer.Container.Resolve<IPaymentService>()
            .GetCollectionHeaderForReports((int)HelperClass.PaymentModes.CHEQUE, (int)Globals.PaymentHeader.FORREPORTSCREEN);
        lblCashInHandAmount.Text = Convert.ToString(lstPaymentTransit[0]);
        lblInTransitAmount.Text = Convert.ToString(lstPaymentTransit[1]);
        lblTransferredAmount.Text = Convert.ToString(lstPaymentTransit[2]);
        lblTotalAmountCollected.Text = Convert.ToString(lstPaymentTransit[3]);
        lblTotalTransactions.Text = Convert.ToString(lstPaymentTransit[4]);
        lblPendingTransactionsAtCounter.Text = Convert.ToString(lstPaymentTransit[5]);
    }
}