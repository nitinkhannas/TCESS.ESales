#region Using directives

using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Reports_UserControls_DformReportData : BaseUserControl
{
    public event ShowDataByDateEventHandler Event_LoadReport;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtToDate.Attributes.Add("ReadOnly", "true");
            txtFromDate.Attributes.Add("ReadOnly", "true");

            //Generate Dform report data
            GenerateDFormReport((DateTime?)null, (DateTime?)null);
            txtFromDateValidator.Enabled = false;
            txtToDateValidator.Enabled = false;
        }
    }

    /// <summary>
    /// Generate DForm report data
    /// </summary>
    /// <param name="fromDate">From date selection criteria</param>
    /// <param name="toDate">To date selection criteria</param>
    private void GenerateDFormReport(DateTime? fromDate, DateTime? toDate)
    {
        if (fromDate != null)
        {
            IList<SettlementOfAccountsDTO> lstDistrictWiseSalesRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
                    .GetDFormReport(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
            if (lstDistrictWiseSalesRpt.Count > 0)
            {
                grdDForm.DataSource = lstDistrictWiseSalesRpt;
                grdDForm.DataBind();
            }
            else
            {
                base.ShowBlankRowInGrid<SettlementOfAccountsDTO>(grdDForm);
            }
        }
        else
        {
            base.ShowBlankRowInGrid<SettlementOfAccountsDTO>(grdDForm);
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        GetDFormReportData();
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
        base.ShowBlankRowInGrid<SettlementOfAccountsDTO>(grdDForm);
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

    private void GetDFormReportData()
    {
        DateTime fromDate = DateTime.Now;
        DateTime toDate = DateTime.Now;

        if (!String.IsNullOrEmpty(txtToDate.Text.Trim()))
        {
            fromDate = Convert.ToDateTime(txtFromDate.Text);
            toDate = Convert.ToDateTime(txtToDate.Text);
        }

        //Generate DForm report data
        GenerateDFormReport(fromDate, toDate);
    }
}