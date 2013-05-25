using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Microsoft.Reporting.WebForms;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Reports;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.DataTransferObjects.GhatoCollection;
using System.Configuration;

public partial class Reports_UserControls_ConsolidatedCustomerCollectionReport : BaseUserControl
{
    public event CloseScreenEventHandler Event_CloseScreen;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        LoadReport();
    }
    public void LoadReport()
    {
        //Reset report viewer control
        reportViewer.Reset();
        //Initializes report viewer and set report as embedded resource
        Common.SetReportEmbeddedResource(reportViewer, "TCESS.ESales.CommonLayer.Reports.ConsolidatedCustomerCollectionReport.rdlc");
        
        //Set datasource for loading advice
        IList<ConsolidatedCustomerCollectionReportDTO> lstConsolidatedCustomer = ESalesUnityContainer.Container.Resolve<IReportService>().GetConsolidatedCustomerCollection(Convert.ToDateTime(ConfigurationManager.AppSettings["PaymentStartDate"]), DateTime.Now);
        ReportDataSource loadingAdviceDataSource = new ReportDataSource("ConsolidatedCollection", lstConsolidatedCustomer);
        reportViewer.LocalReport.DataSources.Add(loadingAdviceDataSource);

        //Set report parameters
        ReportParameter toDt = new ReportParameter("ToDate", DateTime.Now.ToString("dd/MM/yyyy"));
        reportViewer.LocalReport.SetParameters(new ReportParameter[] { toDt });

    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        //Returns to loading advice data screen
        Event_CloseScreen(sender);
    }
    
}