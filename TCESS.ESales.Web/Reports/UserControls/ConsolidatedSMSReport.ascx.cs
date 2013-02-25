using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Microsoft.Reporting.WebForms;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Reports;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

public partial class Reports_UserControls_ConsolidatedSMSReport : BaseUserControl
{
    public event CloseScreenEventHandler Event_CloseScreen;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void LoadReport(int agentId, DateTime fromDate, DateTime toDate)
    {
        //Reset report viewer control
        reportViewer.Reset();

        ////Initializes report viewer and set report as embedded resource
        //Common.SetReportEmbeddedResource(reportViewer, "TCESS.ESales.CommonLayer.Reports.ConsolidatedSMSReport.rdlc");

        ////Set datasource for loading advice
        //IList<SMSRegistrationDTO> lstLoadingSMSBookingRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
        //    .GetLoadingSMSBookingReport(agentId, fromDate, toDate);
        //ReportDataSource loadingSMSBookingDataSource = new ReportDataSource("dsSMSBookingReport", lstLoadingSMSBookingRpt);
        //reportViewer.LocalReport.DataSources.Add(loadingSMSBookingDataSource);

        ////Set report parameters
        //ReportParameter fromDt = new ReportParameter("FromDate", Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy"));
        //ReportParameter toDt = new ReportParameter("ToDate", Convert.ToDateTime(toDate).ToString("dd/MM/yyyy"));
        //reportViewer.LocalReport.SetParameters(new ReportParameter[] { fromDt, toDt });
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        //Returns to loading advice data screen
        Event_CloseScreen(sender);
    }
}