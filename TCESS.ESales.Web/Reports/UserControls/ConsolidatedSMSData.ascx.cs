using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

public partial class Reports_UserControls_ConsolidatedSMSData : BaseUserControl
{
    public event ShowReportEventHandler Event_LoadReport;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtToDate.Attributes.Add("ReadOnly", "true");
            txtFromDate.Attributes.Add("ReadOnly", "true");
            txtToDateValidator.Enabled = false;
            txtFromDateValidator.Enabled = false;

            GenerateLoadingSMSBookingReport((DateTime?)null,(DateTime?)null);
        }
    }

    protected void chkDateRange_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDateRange.Checked)
        {
            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;
            txtFromDateValidator.Enabled = true;
            txtToDateValidator.Enabled = true;
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
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        DateTime fromDate = DateTime.Now;
        DateTime toDate = DateTime.Now;

        if (!string.IsNullOrEmpty(txtFromDate.Text))
        {
            fromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
            toDate = Convert.ToDateTime(txtToDate.Text.Trim());
        }
        GenerateLoadingSMSBookingReport(fromDate, toDate);
    }

    private void GenerateLoadingSMSBookingReport(DateTime? fromDate, DateTime? toDate)
    {
        if (fromDate != null)
        {
            IList<SMSLimitDTO> lstLoadingAdviceRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
                .GetSMSLimitReport(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
            
            if (lstLoadingAdviceRpt.Count > 0)
            {
                grdSMSBooking.DataSource = lstLoadingAdviceRpt;
                grdSMSBooking.DataBind();
            }
            else
            {
                base.ShowBlankRowInGrid<SMSLimitDTO>(grdSMSBooking);
            }
        }
        else
        {
            base.ShowBlankRowInGrid<SMSLimitDTO>(grdSMSBooking);
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        DateTime fromDate = DateTime.Now;
        DateTime toDate = DateTime.Now;

        if (!string.IsNullOrEmpty(txtFromDate.Text))
        {
            fromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
            toDate = Convert.ToDateTime(txtToDate.Text.Trim());
        }

        Event_LoadReport(base.GetAgentByUserId().UAM_Agent_Id, fromDate, toDate);
    }
}