#region Using directives

using System;
using Microsoft.Practices.Unity;
using Microsoft.Reporting.WebForms;
using TCESS.ESales.BusinessLayer.Interfaces.GhatoCollection;
using TCESS.ESales.CommonLayer.Reports;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects.GhatoCollection;
using TCESS.ESales.BusinessLayer.Interfaces.Masters;
using TCESS.ESales.CommonLayer.CommonLibrary;

#endregion

public partial class GhatoCollection_UserControls_PaymentReceipt : BaseUserControl
{
    public event CloseScreenEventHandler Event_CloseScreen;

    public void GetPaymentDetails(int collectionId, bool isRePrint)
    {
        //Gets collection details from collectionId
        PaymentCollectionDTO paymentCollectionDetails = ESalesUnityContainer.Container.
            Resolve<IPaymentService>().GetCollectionDetailsById(collectionId);

        //Sets report parameters and refreshes it
        SetReportParameters(paymentCollectionDetails, reportViewer, isRePrint);
        reportViewer.LocalReport.Refresh();

        if (isRePrint)
        {
            UpdateCollectionDetailsForRePrint(paymentCollectionDetails);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.SetReportEmbeddedResource(reportViewer, "TCESS.ESales.CommonLayer.Reports.PaymentAcknowledgement.rdlc");
        }
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

    private void SetReportParameters(PaymentCollectionDTO paymentCollectionDetails, ReportViewer reportViewer, bool isRePrint)
    {
        string amount = Convert.ToString(paymentCollectionDetails.PC_Amount);
        ReportParameter rpReceiptNo = new ReportParameter("ReceiptNo", Convert.ToString(paymentCollectionDetails.PC_Id));
        ReportParameter rpReceiptDate = new ReportParameter("ReceiptDate", String.Format("{0:dd/MM/yyyy}", 
            Convert.ToString(paymentCollectionDetails.PC_ReceiptDate)));
        ReportParameter rpCustomerCode = new ReportParameter("CustomerCode", paymentCollectionDetails.CustomerCode);
        ReportParameter rpTradeName = new ReportParameter("TradeName", paymentCollectionDetails.CustomerTradeName);
        ReportParameter rpCustomerName = new ReportParameter("CustomerName", Convert.ToString(paymentCollectionDetails.CustomerName));
        ReportParameter rpDistrictName = new ReportParameter("DistrictName", Convert.ToString(paymentCollectionDetails.CustomerDistrict));
        ReportParameter rpAmount = new ReportParameter("Amount", amount);
        CurrencyConvertor currencyConvertor = new CurrencyConvertor();
        string amountInWords = currencyConvertor.Convertor(string.Format("{0:0.00}", amount));
        ReportParameter rpAmountInWords = new ReportParameter("AmountInWords", amountInWords);
        ReportParameter rpInstrumentType = new ReportParameter("InstrumentType", Convert.ToString(paymentCollectionDetails.PaymentModeName));
        ReportParameter rpInstrumentNumber = new ReportParameter("InstrumentNumber", Convert.ToString(paymentCollectionDetails.PC_InstrumentNo));
        ReportParameter rpInstrumentDate = new ReportParameter("InstrumentDate", Convert.ToDateTime(paymentCollectionDetails.PC_InstrumentDate).ToString("dd/MMM/yyyy"));
        ReportParameter rpBankDrawn = new ReportParameter("BankDrawn", Convert.ToString(ESalesUnityContainer.Container.Resolve<IMasterService>().GetBanksDetailsById(Convert.ToInt32(paymentCollectionDetails.PC_BankDrawn)).Bank_Name));
        ReportParameter rpBankBranch = new ReportParameter("BranchName", Convert.ToString(paymentCollectionDetails.PC_BankBranch));
        ReportParameter rpPayer = new ReportParameter("Payer", Convert.ToString(paymentCollectionDetails.PC_PayerName));
        ReportParameter rpPayerMobileNumber = new ReportParameter("PayerMobileNumber", Convert.ToString(paymentCollectionDetails.PC_MobileNumber));
        ReportParameter rpPrintNumber = new ReportParameter("PrintNumber", Convert.ToString(GetPrintName(isRePrint == true ? (paymentCollectionDetails.PC_ReprintCount + 1) : paymentCollectionDetails.PC_ReprintCount)));

        reportViewer.LocalReport.SetParameters(new ReportParameter[] {rpReceiptNo, 
            rpReceiptDate, rpCustomerCode, rpTradeName, rpCustomerName, rpDistrictName,
            rpAmount, rpAmountInWords, rpInstrumentType, rpInstrumentNumber, 
            rpInstrumentDate, rpBankDrawn, rpBankBranch, rpPayer, 
            rpPayerMobileNumber,rpPrintNumber});
    }

    private string GetPrintName(int pPrintCount)
    {
        switch (pPrintCount)
        {
            case 0: return "Original";
            case 1: return "First Copy";
            case 2: return "Second Copy";
            case 3: return "Third Copy";
            default: return "Original";
        }
    }

    private void UpdateCollectionDetailsForRePrint(PaymentCollectionDTO paymentCollectionDetails)
    {
        paymentCollectionDetails.PC_ReprintCount += 1;
        paymentCollectionDetails.PC_ReceiptNo = paymentCollectionDetails.PC_Id.ToString();
        paymentCollectionDetails.PC_LastPrintDate = DateTime.Now;
        ESalesUnityContainer.Container.Resolve<IPaymentService>().SaveOrUpdateCollection(paymentCollectionDetails);
    }
}