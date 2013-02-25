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

public partial class Reports_UserControls_DispatchCustomerwiseReport :  BaseUserControl
{
    public event CloseScreenEventHandler Event_CloseScreen;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void LoadReport(string filterText, int filterValue, DateTime fromDate, DateTime toDate)
    {
        //Reset report viewer control
        reportViewer.Reset();

        //Initializes report viewer and set report as embedded resource
        Common.SetReportEmbeddedResource(reportViewer, "TCESS.ESales.CommonLayer.Reports.DispatchCustomerwiseReport.rdlc");

        //Set datasource for cash collection report
        IList<DispatchReportDTO> lstDispatchReportRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
            .GetDispatchReportCustomerwise(filterText, filterValue, Convert.ToDateTime(fromDate),
            Convert.ToDateTime(toDate));
        ReportDataSource CashCollectionDataSource = new ReportDataSource("dsDispatchReport", lstDispatchReportRpt);
        reportViewer.LocalReport.DataSources.Add(CashCollectionDataSource);

        string rptTitle = string.Empty;
        string rptFilter = string.Empty;
        string rptFilterValue = string.Empty;
        if (filterValue == 1 && filterText != null)
        {
            rptTitle = "Customer";
            rptFilter = "Customer Name ";
            rptFilterValue = "Customer Code :";
        }
        else if (filterValue == 2 && filterText != null)
        {
            rptTitle = "Truck";
            rptFilter = "Truck No ";
            rptFilterValue = "hidden";
        }
        else
        {
            rptTitle = "Customer";
            rptFilter = "Customer Name ";
            rptFilterValue = "Customer Code :";
        }
       
        //Set report parameters
        ReportParameter fromDt = new ReportParameter("FromDate", Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy"));
        ReportParameter toDt = new ReportParameter("ToDate", Convert.ToDateTime(toDate).ToString("dd/MM/yyyy"));
        ReportParameter title = new ReportParameter("Title", rptTitle.ToString());
        ReportParameter filter = new ReportParameter("Filter", rptFilter.ToString());
        ReportParameter filterVal = new ReportParameter("FilterValue", rptFilterValue);

        reportViewer.LocalReport.SetParameters(new ReportParameter[] { fromDt, toDt, title, filter, filterVal });
    }

    protected void btnReturn_Click(object sender, System.EventArgs e)
    {
        //Returns to cash collection data screen
        Event_CloseScreen(sender);
    }
}