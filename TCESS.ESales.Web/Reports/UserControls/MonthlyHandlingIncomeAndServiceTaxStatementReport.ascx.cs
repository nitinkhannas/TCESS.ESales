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

public partial class Reports_UserControls_MonthlyHandlingIncomeAndServiceTaxStatementReport : BaseUserControl
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
        Common.SetReportEmbeddedResource(reportViewer, "TCESS.ESales.CommonLayer.Reports.MonthlyHandlingIncomeAndServiceTaxStatementReport.rdlc");

        //Set datasource for cash collection report
        IList<DispatchReportDTO> lstDispatchReportRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
            .GetMonthlyDispatchReport(Convert.ToInt32(agentId), Convert.ToInt32(month),
            Convert.ToInt32(year));
        ReportDataSource CashCollectionDataSource = new ReportDataSource("dsMonthlyHandlingIncomeAndServiceTaxStatementReport", lstDispatchReportRpt);
        reportViewer.LocalReport.DataSources.Add(CashCollectionDataSource);
        string smonth = TCESS.ESales.CommonLayer.CommonLibrary.MasterList.GetMonthName(month);
        string agentName = "";
        if (base.GetAgentByUserId().UAM_Agent_Id != 0)
        {
            agentName = base.GetAgentByUserId().UAM_Agent_Name;
        }
        else
        {
            agentName = "ALL";
        }
        //Set report parameters
        ReportParameter rmonth = new ReportParameter("Month", smonth);
        ReportParameter ryear = new ReportParameter("Year", Convert.ToString(year));
        ReportParameter agent = new ReportParameter("agent", Convert.ToString(agentName));
        reportViewer.LocalReport.SetParameters(new ReportParameter[] { rmonth, ryear, agent });
    }
    protected void btnReturn_Click(object sender, System.EventArgs e)
    {
        //Returns to cash collection data screen
        Event_CloseScreen(sender);
    }
}