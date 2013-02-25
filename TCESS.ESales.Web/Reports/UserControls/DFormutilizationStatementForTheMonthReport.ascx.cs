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

public partial class Reports_UserControls_DFormutilizationStatementForTheMonthReport : BaseUserControl
{
    public event CloseScreenEventHandler Event_CloseScreen;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void LoadReport(int month,int year)
    {
        //Reset report viewer control
        reportViewer.Reset();

        //Initializes report viewer and set report as embedded resource
        Common.SetReportEmbeddedResource(reportViewer, "TCESS.ESales.CommonLayer.Reports.DFormutilizationStatementForTheMonth.rdlc");

        //Set datasource for loading advice
        IList<SettlementOfAccountsDTO> lstSettlementOfAccountsRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
                .GetDFormutilizationStatementForTheMonthData(base.GetAgentByUserId().UAM_Agent_Id, month, year);
        ReportDataSource loadingAdviceDataSource = new ReportDataSource("dsDFormutilizationStatementForTheMonth", lstSettlementOfAccountsRpt);
        reportViewer.LocalReport.DataSources.Add(loadingAdviceDataSource);
        string smonth = TCESS.ESales.CommonLayer.CommonLibrary.MasterList.GetMonthName(month);
        //Set report parameters
        ReportParameter tmonth = new ReportParameter("Month",smonth);
        ReportParameter tyear = new ReportParameter("Year",Convert.ToString(year));
        reportViewer.LocalReport.SetParameters(new ReportParameter[] { tmonth,tyear });
    }
    protected void btnReturn_Click(object sender, System.EventArgs e)
    {
        //Returns to loading advice data screen
        Event_CloseScreen(sender);
    }

    
}