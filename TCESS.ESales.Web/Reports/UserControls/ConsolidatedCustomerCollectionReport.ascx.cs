#region Using directives

using System;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;
using TCESS.ESales.CommonLayer.Reports;
using TCESS.ESales.DataTransferObjects.GhatoCollection;

#endregion

public partial class Reports_UserControls_ConsolidatedCustomerCollectionReport : BaseUserControl
{
    public event CloseScreenEventHandler Event_CloseScreen;
    
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
        IList<ConsolidatedCustomerCollectionReportDTO> lstConsolidatedCustomer = null;
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