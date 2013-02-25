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

public partial class Reports_UserControls_CustomerWiseSalesReport : BaseUserControl
{
    public event CloseScreenEventHandler Event_CloseScreen;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void LoadReport(int agentId, int month, int year)
    {
        //Reset report viewer control
        reportViewer.Reset();

        //Initializes report viewer and set report as embedded resource
        Common.SetReportEmbeddedResource(reportViewer, "TCESS.ESales.CommonLayer.Reports.CustomerWiseSalesReport.rdlc");

        //Set datasource for cash collection report
        IList<CustomerwiseSalesReportDTO> lstDistrictWiseSalesRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
            .GetCustomerWiseSalesReport(Convert.ToInt32(agentId), month, year);
        ReportDataSource CashCollectionDataSource = new ReportDataSource("dsCustomerWiseSalesReport", lstDistrictWiseSalesRpt);
        reportViewer.LocalReport.DataSources.Add(CashCollectionDataSource);        
        //Set report parameters
        string smonth = TCESS.ESales.CommonLayer.CommonLibrary.MasterList.GetMonthName(month);
        
        //Set report parameters
        ReportParameter tmonth = new ReportParameter("Month", smonth);
        ReportParameter tyear = new ReportParameter("Year", year.ToString());   
        reportViewer.LocalReport.SetParameters(new ReportParameter[] { tmonth,tyear });
    }
    protected void btnReturn_Click(object sender, System.EventArgs e)
    {
        //Returns to cash collection data screen
        Event_CloseScreen(sender);
    }
}