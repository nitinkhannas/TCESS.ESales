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


public partial class Reports_UserControls_DispatchReport : BaseUserControl
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
		Common.SetReportEmbeddedResource(reportViewer, "TCESS.ESales.CommonLayer.Reports.DispatchReport.rdlc");

		//Set datasource for cash collection report
		IList<DispatchReportDTO> lstDispatchReportRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
			.GetDispatchReport(Convert.ToInt32(agentId), Convert.ToDateTime(fromDate),
			Convert.ToDateTime(toDate));
		ReportDataSource CashCollectionDataSource = new ReportDataSource("dsDispatchReport", lstDispatchReportRpt);
		reportViewer.LocalReport.DataSources.Add(CashCollectionDataSource);
		string agentName = "";
        if (agentId > 0)
        {
            AgentDTO _agentName = ESalesUnityContainer.Container.Resolve<IAgentService>().GetAgentByAgentId(agentId);
            agentName = _agentName.Agent_Name;
        }
		else
		{
			agentName = "ALL";
		}
		//Set report parameters
		ReportParameter fromDt = new ReportParameter("FromDate", Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy"));
		ReportParameter toDt = new ReportParameter("ToDate", Convert.ToDateTime(toDate).ToString("dd/MM/yyyy"));
		ReportParameter agent = new ReportParameter("agent", Convert.ToString(agentName));
		reportViewer.LocalReport.SetParameters(new ReportParameter[] { fromDt, toDt, agent });
	}
	protected void btnReturn_Click(object sender, System.EventArgs e)
	{
		//Returns to cash collection data screen
		Event_CloseScreen(sender);
	}
	
}