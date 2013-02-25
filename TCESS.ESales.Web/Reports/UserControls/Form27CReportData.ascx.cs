#region Using directives

using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Reports_UserControls_Form27CReportData : BaseUserControl
{
    public event ShowDataByDateEventHandler Event_LoadReport;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtToDate.Attributes.Add("ReadOnly", "true");
            txtFromDate.Attributes.Add("ReadOnly", "true");

            //Generate Form27C report data
            GenerateForm27CReport((DateTime?)null, (DateTime?)null);
            txtFromDateValidator.Enabled = false;
            txtToDateValidator.Enabled = false;
        }
    }

    /// <summary>
    /// Generate Form27C report data
    /// </summary>
    /// <param name="fromDate">From date selection criteria</param>
    /// <param name="toDate">To date selection criteria</param>
    private void GenerateForm27CReport(DateTime? fromDate, DateTime? toDate)
    {
        if (fromDate != null)
        {
            IList<Form27CViewDTO> lstForm27CRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
                .GetForm27CReport(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));

            if (lstForm27CRpt.Count > 0)
            {
                grdForm27C.DataSource = lstForm27CRpt;
                grdForm27C.DataBind();
            }
            else
            {
                base.ShowBlankRowInGrid<Form27CViewDTO>(grdForm27C);
            }
        }
        else
        {
            base.ShowBlankRowInGrid<Form27CViewDTO>(grdForm27C);
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        GetForm27CReportData();
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
        base.ShowBlankRowInGrid<Form27CViewDTO>(grdForm27C);
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

    private void GetForm27CReportData()
    {
        DateTime fromDate = DateTime.Now;
        DateTime toDate = DateTime.Now;

        if (!String.IsNullOrEmpty(txtToDate.Text.Trim()))
        {
            fromDate = Convert.ToDateTime(txtFromDate.Text);
            toDate = Convert.ToDateTime(txtToDate.Text);
        }

        //Generate DForm report data
        GenerateForm27CReport(fromDate, toDate);
    }
}