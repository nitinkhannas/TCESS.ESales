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

public partial class Reports_UserControls_DistrictWiseSalesReport : BaseUserControl
{
    public event CloseScreenEventHandler Event_CloseScreen;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void LoadReport(int agentId, int month)
    {
        //Reset report viewer control
        reportViewer.Reset();

        //Initializes report viewer and set report as embedded resource
        Common.SetReportEmbeddedResource(reportViewer, "TCESS.ESales.CommonLayer.Reports.DistrictWiseSalesReport.rdlc");

        //Set datasource for cash collection report
        IList<SalesReportDTO> lstDistrictWiseSalesRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
            .GetDistrictWiseSalesReport(Convert.ToInt32(agentId), month);
        ReportDataSource CashCollectionDataSource = new ReportDataSource("dsDistrictWiseSalesReport", lstDistrictWiseSalesRpt);
        reportViewer.LocalReport.DataSources.Add(CashCollectionDataSource);
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
        string smonth = TCESS.ESales.CommonLayer.CommonLibrary.MasterList.GetMonthName(month);
        //Set report parameters
        ReportParameter tmonth = new ReportParameter("Month", smonth);
        ReportParameter agent = new ReportParameter("agent", Convert.ToString(agentName));
        reportViewer.LocalReport.SetParameters(new ReportParameter[] { tmonth, agent });
    }
    protected void btnReturn_Click(object sender, System.EventArgs e)
    {
        //Returns to cash collection data screen
        Event_CloseScreen(sender);
    }
}