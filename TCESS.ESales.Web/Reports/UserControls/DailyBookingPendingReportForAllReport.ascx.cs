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

public partial class Reports_UserControls_DailyBookingPendingReportForAllReport : BaseUserControl
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
        Common.SetReportEmbeddedResource(reportViewer, "TCESS.ESales.CommonLayer.Reports.DailyBookingPendingReportForAll.rdlc");

        //Set datasource for pending bookings
        IList<BookingDTO> lstPendingBookingRpt = null;
        if (agentId == 0)
          lstPendingBookingRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
            .GetPendingBookingForAllReport(fromDate, toDate);
        else
            lstPendingBookingRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
           .GetPendingBookingReport(agentId,fromDate, toDate);

        ReportDataSource loadingAdviceDataSource = new ReportDataSource("dsBookingPendingAll", lstPendingBookingRpt);
        reportViewer.LocalReport.DataSources.Add(loadingAdviceDataSource);
        string agentName = "";
        if (agentId > 0)
        {
            AgentDTO _agentName = ESalesUnityContainer.Container.Resolve<IAgentService>().GetAgentByAgentId(agentId);
            agentName = _agentName.Agent_Name;
        }
        else
        {
            agentName = "All";
        }

        //Set report parameters
        ReportParameter fromDt = new ReportParameter("FromDate", Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy"));
        ReportParameter toDt = new ReportParameter("ToDate", Convert.ToDateTime(toDate).ToString("dd/MM/yyyy"));
        ReportParameter agent = new ReportParameter("agent", Convert.ToString(agentName));
        reportViewer.LocalReport.SetParameters(new ReportParameter[] { agent, fromDt, toDt });
    }

    protected void btnReturn_Click(object sender, System.EventArgs e)
    {
        //Returns to pending booking data screen
        Event_CloseScreen(sender);
    }
}