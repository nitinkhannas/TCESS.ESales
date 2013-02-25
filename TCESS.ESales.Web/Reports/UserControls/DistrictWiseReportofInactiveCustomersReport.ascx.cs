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

public partial class Reports_UserControls_DistrictWiseReportofInactiveCustomersReport : BaseUserControl 
{
    public event CloseScreenEventHandler Event_CloseScreen;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void LoadReport(int agentId, DateTime fromDate, DateTime toDate)
    {
        //Reset report viewer control
        reportViewer.Reset();

        //Initializes report viewer and set report as embedded resource
        Common.SetReportEmbeddedResource(reportViewer, "TCESS.ESales.CommonLayer.Reports.DistrictWiseReportofInactiveCustomersReport.rdlc");

        //Set datasource for loading advice
        IList<BookingDTO> lstLoadingAdviceRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
            .GetDistrictWiseReportofInactiveCustomersReport(agentId, fromDate, toDate);
        ReportDataSource loadingAdviceDataSource = new ReportDataSource("dsDistrictWiseReportofInactiveCustomersReport", lstLoadingAdviceRpt);
        reportViewer.LocalReport.DataSources.Add(loadingAdviceDataSource);
        
    }

    protected void btnReturn_Click(object sender, System.EventArgs e)
    {
        //Returns to loading advice data screen
        Event_CloseScreen(sender);
    }
}