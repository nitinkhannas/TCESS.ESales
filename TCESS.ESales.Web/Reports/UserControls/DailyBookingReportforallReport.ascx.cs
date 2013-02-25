using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Microsoft.Reporting.WebForms;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Reports;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
public partial class Reports_UserControls_DailyBookingReportforallReport : BaseUserControl
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
        Common.SetReportEmbeddedResource(reportViewer, "TCESS.ESales.CommonLayer.Reports.DailyBookingReportforall.rdlc");

        //Set datasource for loading advice
        IList<BookingDTO> lstLoadingAdviceRpt = null;
        if(agentId ==0)
       lstLoadingAdviceRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
            .GetDailyBookingReportforDCA(fromDate, toDate);
        else
            lstLoadingAdviceRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
            .GetDailyBookingReportforallDCAsReport(agentId,fromDate, toDate);

        ReportDataSource loadingAdviceDataSource = new ReportDataSource("dsDailyBookingReportforall", lstLoadingAdviceRpt);
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
        reportViewer.LocalReport.SetParameters(new ReportParameter[] {agent, fromDt, toDt });
    }
    protected void btnReturn_Click(object sender, System.EventArgs e)
    {
        //Returns to loading advice data screen
        Event_CloseScreen(sender);
    }
}