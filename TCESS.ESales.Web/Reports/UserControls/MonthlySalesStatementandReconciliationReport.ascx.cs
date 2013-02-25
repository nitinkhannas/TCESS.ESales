#region Using directive
using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Microsoft.Reporting.WebForms;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Reports;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
#endregion

public partial class Reports_UserControls_MonthlySalesStatementandReconciliationReport : BaseUserControl
{
    public event CloseScreenEventHandler Event_CloseScreen;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void LoadReport(DateTime fromDate,int month)
    {
        //Reset report viewer control
        reportViewer.Reset();

        //Initializes report viewer and set report as embedded resource
        Common.SetReportEmbeddedResource(reportViewer, "TCESS.ESales.CommonLayer.Reports.MonthlySalesStatementandReconciliationReport.rdlc");

        //Set datasource for cash collection report
        IList<SalesReportDTO> lstSalesRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
            .GetSalesReport(fromDate, month);
        ReportDataSource MonthlySalesStatementandReconciliationDataSource = new ReportDataSource("dsMonthlySalesStatementandReconciliationReport", lstSalesRpt);
        reportViewer.LocalReport.DataSources.Add(MonthlySalesStatementandReconciliationDataSource);
        
        //Set report parameters
        string smonth = TCESS.ESales.CommonLayer.CommonLibrary.MasterList.GetMonthName(month);
        //Set report parameters
        ReportParameter tmonth = new ReportParameter("Month", smonth);
        ReportParameter fromDt = new ReportParameter("FromDate", Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy"));
        reportViewer.LocalReport.SetParameters(new ReportParameter[] { tmonth, fromDt });
    }
    protected void btnReturn_Click(object sender, System.EventArgs e)
    {
        //Returns to cash collection data screen
        Event_CloseScreen(sender);
    }
}