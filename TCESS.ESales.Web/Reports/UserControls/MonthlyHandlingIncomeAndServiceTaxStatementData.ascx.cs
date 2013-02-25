#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Reports_UserControls_MonthlyHandlingIncomeAndServiceTaxStatementData : BaseUserControl
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
            PopulateDCA();
            ddlYear.SelectedValue = tnow.Year.ToString();
            ddlMonth.SelectedValue = tnow.Month.ToString();
            base.ShowBlankRowInGrid<DispatchReportDTO>(grdMontlyHandlingIncomeAndServiceTaxStatement);
        }
        base.ShowBlankRowInGrid<DispatchReportDTO>(grdMontlyHandlingIncomeAndServiceTaxStatement);
    }

    /// <summary>
    /// Get active DCA list and populate drop down controls
    /// </summary>
    private void PopulateDCA()
    {
        if (base.GetAgentByUserId().UAM_Agent_Id != 0)
        {
            lblAgentName.Text = base.GetAgentByUserId().UAM_Agent_Name;
        }
        else
        {
            lblAgentName.Text = "ALL";
        }
    }

    /// <summary>
    /// Generate Daily Handling Income And Service Tax Statement
    /// </summary>
    /// <param name="fromDate">From date selection criteria</param>
    /// <param name="toDate">To date selection criteria</param>
    /// <param name="dcaName">dcaName selection criteria</param>     
    private void GenerateLoadingAdviceReport(string dcaName, int month, int year)
    {
        IList<DispatchReportDTO> lstDispatchReportRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
                .GetMonthlyDispatchReport(Convert.ToInt32(base.GetAgentByUserId().UAM_Agent_Id), month, year);
        if (lstDispatchReportRpt.Count > 0)
        {
            //PopulateDCA();
            grdMontlyHandlingIncomeAndServiceTaxStatement.DataSource = lstDispatchReportRpt;
            grdMontlyHandlingIncomeAndServiceTaxStatement.DataBind();
        }
        else
        {
            //PopulateDCA();
            base.ShowBlankRowInGrid<DispatchReportDTO>(grdMontlyHandlingIncomeAndServiceTaxStatement);
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        year = Int32.Parse(ddlYear.SelectedValue);
        month = Int32.Parse(ddlMonth.SelectedValue);

        //Generate
        GenerateLoadingAdviceReport(base.GetAgentByUserId().UAM_Agent_Id.ToString(), month, year);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        year = Int32.Parse(ddlYear.SelectedValue);
        month = Int32.Parse(ddlMonth.SelectedValue);

        //Show loading advice report
        Event_LoadReport(Convert.ToInt32(base.GetAgentByUserId().UAM_Agent_Id), month, year);
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