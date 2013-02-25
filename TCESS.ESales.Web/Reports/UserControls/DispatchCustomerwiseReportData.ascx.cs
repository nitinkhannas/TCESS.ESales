#region Using directives

using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion


public partial class Reports_UserControls_DispatchCustomerwiseReportData : BaseUserControl
{
    public event ShowCustomerwiseReportEventHandler Event_LoadReport;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtToDate.Attributes.Add("ReadOnly", "true");
            txtFromDate.Attributes.Add("ReadOnly", "true");

            //Generate cash collection report data
            GenerateDispatchReport((DateTime?)null, (DateTime?)null, base.GetAgentByUserId().UAM_Agent_Id.ToString(), Convert.ToInt32(ddlFilter.SelectedItem.Value));
            txtFromDateValidator.Enabled = false;
            txtToDateValidator.Enabled = false;
        }

        DateTime fromDate = DateTime.Now;
        DateTime toDate = DateTime.Now;

        if (!String.IsNullOrEmpty(txtToDate.Text.Trim()))
        {
            fromDate = Convert.ToDateTime(txtFromDate.Text);
            toDate = Convert.ToDateTime(txtToDate.Text);
        }
        if (!String.IsNullOrEmpty(Request.QueryString["custcode"]))
        {
            GenerateDispatchReport(fromDate, toDate, Request.QueryString["custcode"].ToString(), Convert.ToInt32(ddlFilter.SelectedItem.Value));
        }
    }
    
    /// <summary>
    /// Generate dispatch report data
    /// </summary>
    /// <param name="fromDate">From date selection criteria</param>
    /// <param name="toDate">To date selection criteria</param>
    /// <param name="dcaName">dcaName selection criteria</param> 
    private void GenerateDispatchReport(DateTime? fromDate, DateTime? toDate, string filterText, int filterValue)
    {
        if (fromDate != null)
        {
            IList<DispatchReportDTO> lstDispatchReportRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
                .GetDispatchReportCustomerwise(filterText, filterValue, Convert.ToDateTime(fromDate),
                Convert.ToDateTime(toDate));

            //If Dispatch report contains some data
            if (lstDispatchReportRpt.Count > 0)
            {
                grdDispatch.DataSource = lstDispatchReportRpt;
                grdDispatch.DataBind();
            }
            else
            {
                base.ShowBlankRowInGrid<DispatchReportDTO>(grdDispatch);
            }
        }
        else
        {
            base.ShowBlankRowInGrid<DispatchReportDTO>(grdDispatch);
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now;

            if (!String.IsNullOrEmpty(txtToDate.Text.Trim()))
            {
                fromDate = Convert.ToDateTime(txtFromDate.Text);
                toDate = Convert.ToDateTime(txtToDate.Text);
            }
            GenerateDispatchReport(fromDate, toDate, txtFilter.Text.Trim(), Convert.ToInt32(ddlFilter.SelectedItem.Value));
        }
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
        base.ShowBlankRowInGrid<DispatchReportDTO>(grdDispatch);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now;

            if (!String.IsNullOrEmpty(txtToDate.Text.Trim()))
            {
                fromDate = Convert.ToDateTime(txtFromDate.Text);
                toDate = Convert.ToDateTime(txtToDate.Text);
            }
            if (String.IsNullOrEmpty(Request.QueryString["custcode"]))
            {
                Event_LoadReport(txtFilter.Text.Trim(), fromDate, toDate);
            }
            else
            {
                Event_LoadReport(Request.QueryString["custcode"].ToString(), fromDate, toDate);
            }
        }
    }
}