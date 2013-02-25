#region Using directives

using System;
using Microsoft.Practices.Unity;
using Microsoft.Reporting.WebForms;
using TCESS.ESales.BusinessLayer.Interfaces.GhatoCollection;
using TCESS.ESales.BusinessLayer.Interfaces.Masters;
using TCESS.ESales.CommonLayer.Reports;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects.GhatoCollection;

#endregion

public partial class Collection_UserControls_RefundReceipt : BaseUserControl
{
    public event CloseScreenEventHandler Event_CloseScreen;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.SetReportEmbeddedResource(reportViewer, "TCESS.ESales.CommonLayer.Reports.PaymentRefundAcknowledgement.rdlc");
        }
    }

    public void GetRefundDetails(int refundId)
    {
        SetReportParameters(ESalesUnityContainer.Container.Resolve<IPaymentService>().GetPaymentRefundDetails(refundId), reportViewer);
        reportViewer.LocalReport.Refresh();
    }

    private void SetReportParameters(PaymentRefundDTO paymentRefundDetails, ReportViewer reportViewer)
    {
        ReportParameter rpReceiptNo = new ReportParameter("ReceiptNo", Convert.ToString(paymentRefundDetails.PR_ID));
        ReportParameter rpCustomerName = new ReportParameter("CustomerName", Convert.ToString(paymentRefundDetails.CustomerName));
        ReportParameter rpTradeName = new ReportParameter("TradeName", Convert.ToString(paymentRefundDetails.CustomerTradeName));
        ReportParameter rpAmount = new ReportParameter("Amount", Convert.ToString(paymentRefundDetails.PR_Amount));
        ReportParameter rpInstrumentType = new ReportParameter("InstrumentType", Convert.ToString(paymentRefundDetails.PaymentModeName));
        ReportParameter rpInstrumentNumber = new ReportParameter("InstrumentNumber", Convert.ToString(paymentRefundDetails.PR_InstrumentNo));
        ReportParameter rpInstrumentDate = new ReportParameter("InstrumentDate", Convert.ToDateTime(paymentRefundDetails.PR_InstrumentDate).ToString("dd/MMM/yyyy"));
        ReportParameter rpBankDrawn = new ReportParameter("BankDrawn", Convert.ToString(ESalesUnityContainer.Container.Resolve<IMasterService>().GetBanksDetailsById(Convert.ToInt32(paymentRefundDetails.PR_BankDrawn)).Bank_Name));
        ReportParameter rpPayer = new ReportParameter("Payer", Convert.ToString(paymentRefundDetails.PR_ReceiverName));
        ReportParameter rpPayerMobileNumber = new ReportParameter("PayerMobileNumber", Convert.ToString(paymentRefundDetails.PR_MobileNumber));

        reportViewer.LocalReport.SetParameters(new ReportParameter[] {
            rpReceiptNo, rpCustomerName, rpTradeName, rpAmount, rpInstrumentType, rpInstrumentNumber,rpInstrumentDate,
            rpBankDrawn,rpPayer,rpPayerMobileNumber});
    }

    /// <summary>
    /// Event for return button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Event_CloseScreen(sender);
    }
}