#region Namespace

using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Reports_UserControls_SMSBookingData : BaseUserControl
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

            //Generate loading advice report data
            GenerateLoadingSMSBookingReport((DateTime?)null, (DateTime?)null, 0);
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        DateTime fromDate = DateTime.Now;
        DateTime toDate = DateTime.Now;
        int smsStatusFilter = 0;

        if (!string.IsNullOrEmpty(txtFromDate.Text))
        {
            fromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
            toDate = Convert.ToDateTime(txtToDate.Text.Trim());
        }

        if (IsFilterOn())
        {
            smsStatusFilter = Convert.ToInt32(ddlSmsStatus.SelectedItem.Value);
            GenerateLoadingSMSBookingReport(fromDate, toDate, smsStatusFilter);
        }
        else
        {
            //Generate loading SMS Booking data
            GenerateLoadingSMSBookingReport(fromDate, toDate, smsStatusFilter);
        }
    }
    
    /// <summary>
    /// Generate Loading SMS Booking
    /// </summary>
    /// <param name="fromDate">From date selection criteria</param>
    /// <param name="toDate">To date selection criteria</param>
    private void GenerateLoadingSMSBookingReport(DateTime? fromDate, DateTime? toDate, int smsStatusFilter)
    {
        if (fromDate != null)
        {
            IList<SMSRegistrationDTO> lstLoadingAdviceRpt = null;
            if (!IsFilterOn())
            {
                lstLoadingAdviceRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
                    .GetLoadingSMSBookingReport(base.GetAgentByUserId().UAM_Agent_Id, Convert.ToDateTime(fromDate),
                    Convert.ToDateTime(toDate));
            }
            else
            {
                lstLoadingAdviceRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
                    .GetLoadingSMSBookingReport(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), smsStatusFilter);
            }

            if (lstLoadingAdviceRpt.Count > 0)
            {
                grdSMSBooking.DataSource = lstLoadingAdviceRpt;
                grdSMSBooking.DataBind();
            }
            else
            {
                base.ShowBlankRowInGrid<SMSRegistrationDTO>(grdSMSBooking);
            }
        }
        else
        {
            base.ShowBlankRowInGrid<SMSRegistrationDTO>(grdSMSBooking);
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
        base.ShowBlankRowInGrid<SMSRegistrationDTO>(grdSMSBooking);
    }

    public void btnPrint_Click(object sender, EventArgs e)
    {
        DateTime fromDate = DateTime.Now;
        DateTime toDate = DateTime.Now;

        if (!string.IsNullOrEmpty(txtFromDate.Text))
        {
            fromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
            toDate = Convert.ToDateTime(txtToDate.Text.Trim());
        }

        //Show loading advice report
        Event_LoadReport(base.GetAgentByUserId().UAM_Agent_Id, fromDate, toDate);
    }

    private bool IsFilterOn()
    {
        if (Convert.ToInt32(ddlSmsStatus.SelectedItem.Value) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}