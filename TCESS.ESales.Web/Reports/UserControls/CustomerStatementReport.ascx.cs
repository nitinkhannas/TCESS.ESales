using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Microsoft.Reporting.WebForms;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Reports;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.DataTransferObjects.GhatoCollection;
using TCESS.ESales.BusinessLayer.Interfaces.GhatoCollection;

public partial class Reports_UserControls_CustomerStatementReport : BaseUserControl
{
    public event CloseScreenEventHandler Event_CloseScreen;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void LoadReport(int customerId, DateTime fromDate, DateTime toDate)
    {
        //Reset report viewer control
        reportViewer.Reset();

        //Initializes report viewer and set report as embedded resource
        Common.SetReportEmbeddedResource(reportViewer, "TCESS.ESales.CommonLayer.Reports.CustomerCollectionAndSettlement.rdlc");

        //Set datasource for loading advice
        if (fromDate != null)
        {
            IList<CustomerCollectionSettlementDTO> lstCustomerCollectionSettlementDTO = null;
            CustomerDTO cust = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetCustomerDetailsById(customerId);
            decimal openingBalance = ESalesUnityContainer.Container.Resolve<IReportService>().GetOpeningBalance(customerId, fromDate, toDate);

            lstCustomerCollectionSettlementDTO = ESalesUnityContainer.Container.Resolve<IReportService>().GetConsolidatedCollectionReport(customerId, fromDate, toDate);
            IList<BookingDTO> lstBookingDTO = ESalesUnityContainer.Container.Resolve<IBookingService>().GetHoldPendingBooking(customerId, fromDate, toDate);
            IList<PaymentCollectionDTO> lstPaymentCollectionDTO = ESalesUnityContainer.Container.Resolve<IPaymentService>().GetHoldActiveCollectionForPeriodByCustomer(customerId, fromDate, toDate);


            ReportDataSource customerCollectionSettlementDataSource = new ReportDataSource("dsCustomerCollectionSettlement", lstCustomerCollectionSettlementDTO);
            ReportDataSource BookingDataSource = new ReportDataSource("dsBooking", lstBookingDTO);
            ReportDataSource PaymentCollectionDataSource = new ReportDataSource("dsPaymentCollection", lstPaymentCollectionDTO);
            reportViewer.LocalReport.DataSources.Add(customerCollectionSettlementDataSource);
            reportViewer.LocalReport.DataSources.Add(BookingDataSource);
            reportViewer.LocalReport.DataSources.Add(PaymentCollectionDataSource);


            //Set report parameters
            ReportParameter fromDt = new ReportParameter("FromDate", Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy"));
            ReportParameter toDt = new ReportParameter("ToDate", Convert.ToDateTime(toDate).ToString("dd/MM/yyyy"));
            ReportParameter code = new ReportParameter("Code", cust.Cust_Code);
            ReportParameter dist = new ReportParameter("DISTRICT", cust.Cust_District_Name);
            ReportParameter custName = new ReportParameter("CustName", cust.Cust_TradeName);
            ReportParameter OpeningBalance = new ReportParameter("OpeningBalance", Convert.ToString(openingBalance));
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { fromDt, toDt, code, dist, custName, OpeningBalance });
        }
    }
    protected void btnReturn_Click(object sender, System.EventArgs e)
    {
        //Returns to loading advice data screen
        Event_CloseScreen(sender);
    }
}