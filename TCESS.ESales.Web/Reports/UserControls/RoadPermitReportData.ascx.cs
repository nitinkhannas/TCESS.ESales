#region Using directives

using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Reports_UserControls_RoadPermitReportData : BaseUserControl
{
    public event ShowDataByDateEventHandler Event_LoadReport;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtToDate.Attributes.Add("ReadOnly", "true");
            txtFromDate.Attributes.Add("ReadOnly", "true");

            //Generate Road Permit report data
            GenerateRoadPermitReport((DateTime?)null, (DateTime?)null);
            txtFromDateValidator.Enabled = false;
            txtToDateValidator.Enabled = false;
        }
    }

    /// <summary>
    /// Generate Road Permit report data
    /// </summary>
    /// <param name="fromDate">From date selection criteria</param>
    /// <param name="toDate">To date selection criteria</param>
    private void GenerateRoadPermitReport(DateTime? fromDate, DateTime? toDate)
    {
        if (fromDate != null)
        {
            IList<SettlementOfAccountsDTO> lstRoadPermitRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
                    .GetRoadPermitReport(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
            if (lstRoadPermitRpt.Count > 0)
            {
                grdRoadPermit.DataSource = lstRoadPermitRpt;
                grdRoadPermit.DataBind();
            }
            else
            {
                base.ShowBlankRowInGrid<SettlementOfAccountsDTO>(grdRoadPermit);
            }
        }
        else
        {
            base.ShowBlankRowInGrid<SettlementOfAccountsDTO>(grdRoadPermit);
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        GetRoadPermitReportData();
    }

    protected void chkDateRange_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDateRange.Checked)
        {
            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;
            txtToDateValidator.Enabled = true;
            txtFromDateValidator.Enabled = true;
        }
        else
        {
            txtFromDate.Enabled = false;
            txtToDate.Enabled = false;
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            txtToDateValidator.Enabled = false;
            txtFromDateValidator.Enabled = false;
        }
        base.ShowBlankRowInGrid<SettlementOfAccountsDTO>(grdRoadPermit);
    }

    public void btnPrint_Click(object sender, EventArgs e)
    {
        DateTime fromDate = DateTime.Now;
        DateTime toDate = DateTime.Now;

        if (!String.IsNullOrEmpty(txtToDate.Text.Trim()))
        {
            fromDate = Convert.ToDateTime(txtFromDate.Text);
            toDate = Convert.ToDateTime(txtToDate.Text);
        }

        Event_LoadReport(fromDate, toDate);
    }

    private void GetRoadPermitReportData()
    {
        DateTime fromDate = DateTime.Now;
        DateTime toDate = DateTime.Now;

        if (!String.IsNullOrEmpty(txtToDate.Text.Trim()))
        {
            fromDate = Convert.ToDateTime(txtFromDate.Text);
            toDate = Convert.ToDateTime(txtToDate.Text);
        }

        //Generate Road Permit report data
        GenerateRoadPermitReport(fromDate, toDate);
    }
}