#region Namespace

using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Reports_UserControls_RoadPermitsUtilisationStatementForTheMonthData : BaseUserControl
{
    int year, month;
    public event ShowReportMonthYearEventHandler Event_LoadReport;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime tnow = DateTime.Now;
            ArrayList ayear = new ArrayList();
            int i;
           
            for (i = 2011; i <= 2025; i++)
            {
                ayear.Add(i);
            }

            ddlYear.DataSource = ayear;
            ddlYear.DataBind();
            ddlYear.SelectedValue = tnow.Year.ToString();
            ddlMonth.SelectedValue = tnow.Month.ToString();
            GenerateLoadingAdviceReport(month, year);
        }
        //year = Int32.Parse(ddlYear.SelectedValue);
        //month = Int32.Parse(ddlMonth.SelectedValue);
        //GenerateLoadingAdviceReport(month, year);
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        year = Int32.Parse(ddlYear.SelectedValue);
        month = Int32.Parse(ddlMonth.SelectedValue);

        //Generate
        GenerateLoadingAdviceReport(month, year);
    }

    private void GenerateLoadingAdviceReport(int month, int year)
    {
        IList<SettlementOfAccountsDTO> lstSettlementOfAccountsRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
                .GetDFormutilizationStatementForTheMonthData(base.GetAgentByUserId().UAM_Agent_Id, month, year);

        if (lstSettlementOfAccountsRpt.Count > 0)
        {
            grdRoadPermitsUtilisationStatementForTheMonth.DataSource = lstSettlementOfAccountsRpt;
            grdRoadPermitsUtilisationStatementForTheMonth.DataBind();
        }
        else
        {
            base.ShowBlankRowInGrid<SettlementOfAccountsDTO>(grdRoadPermitsUtilisationStatementForTheMonth);
        }
    }

    public void btnPrint_Click(object sender, EventArgs e)
    {
        year = Int32.Parse(ddlYear.SelectedValue);
        month = Int32.Parse(ddlMonth.SelectedValue);
        //Show loading advice report
        Event_LoadReport(base.GetAgentByUserId().UAM_Agent_Id, month, year);
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMonth.SelectedValue = (Convert.ToInt32(ddlMonth.SelectedValue)).ToString();
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        year = Int32.Parse(ddlYear.SelectedValue);
    }
}