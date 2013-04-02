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
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Collection_UserControls_SMSReceipt : BaseUserControl
{
    public event CloseScreenEventHandler Event_CloseScreen;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.SetReportEmbeddedResource(reportViewer, "TCESS.ESales.CommonLayer.Reports.SMSPaymentReceipt.rdlc");
        }
    }

    public void GetSMSDetails(int smsPaymentId, int validDays)
    {
        //Gets collection details from collectionId
        SMSPaymentRegistrationDTO SMSPaymentDetails = ESalesUnityContainer.Container.
            Resolve<IPaymentService>().GetSMSPaymentDetails(smsPaymentId, validDays);

        //Sets report parameters and refreshes it
        SetReportParameters(SMSPaymentDetails, reportViewer);
        reportViewer.LocalReport.Refresh();
    }

    private void SetReportParameters(SMSPaymentRegistrationDTO smsPaymentDetails, ReportViewer reportViewer)
    {
        string amount = Convert.ToString(smsPaymentDetails.SMSPay_Amount);
        ReportParameter rpSMSPay_Id = new ReportParameter("SMSPay_Id", Convert.ToString(smsPaymentDetails.SMSPay_Id));
        ReportParameter rpSMSPay_Date = new ReportParameter("SMSPay_Date", String.Format("{0:dd/MM/yyyy}",
            Convert.ToString(smsPaymentDetails.SMSPay_Date)));
        ReportParameter rpCustomerCode = new ReportParameter("CustomerCode", smsPaymentDetails.SMSPay_CustCode);
        ReportParameter rpTradeName = new ReportParameter("TradeName", smsPaymentDetails.SMSPay_Cust_TradeName);
        ReportParameter rpCustomerName = new ReportParameter("CustomerName", Convert.ToString(smsPaymentDetails.SMSPay_CustomerName));
        ReportParameter rpDistrictName = new ReportParameter("DistrictName", Convert.ToString(smsPaymentDetails.SMSPay_Cust_District_Name));
        ReportParameter rpAmount = new ReportParameter("Amount", amount);
        CurrencyConvertor currencyConvertor = new CurrencyConvertor();
        string amountInWords = currencyConvertor.Convertor(string.Format("{0:0.00}", amount));
        ReportParameter rpAmountInWords = new ReportParameter("AmountInWords", amountInWords);
        
        reportViewer.LocalReport.SetParameters(new ReportParameter[] {rpSMSPay_Id, 
            rpSMSPay_Date, rpCustomerCode, rpTradeName, rpCustomerName, rpDistrictName,
            rpAmount, rpAmountInWords});
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Event_CloseScreen(sender);
    }
}