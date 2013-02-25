#region Using directive

using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using System.Web.UI.WebControls;

#endregion

public partial class Reports_UserControls_PendingReportData : BaseUserControl
{
    public event ShowReportEventHandler Event_LoadReport;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtToDate.Attributes.Add("ReadOnly", "true");
            txtFromDate.Attributes.Add("ReadOnly", "true");
            txtFromDateValidator.Enabled = false;
            txtToDateValidator.Enabled = false;

            //Generate pending booking report data
            GeneratePendingReport((DateTime?)null, (DateTime?)null);
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        GetPendingReportData();
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
        base.ShowBlankRowInGrid<BookingDTO>(grdPendingBooking);
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

        //Show pending booking report
        Event_LoadReport(base.GetAgentByUserId().UAM_Agent_Id, fromDate, toDate);
    }

    protected void grdPendingBooking_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPendingBooking.PageIndex = e.NewPageIndex;

        //Gets booking pending details
        GetPendingReportData();
    }

    #region Private Methods

    /// <summary>
    /// Generate pending report data
    /// </summary>
    /// <param name="fromDate">From date selection criteria</param>
    /// <param name="toDate">To date selection criteria</param>
    private void GeneratePendingReport(DateTime? fromDate, DateTime? toDate)
    {
        if (fromDate != null)
        {
            IList<BookingDTO> lstBookingDTO = ESalesUnityContainer.Container.Resolve<IReportService>()
                .GetPendingReport(base.GetAgentByUserId().UAM_Agent_Id, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));

            //If pending report contains some data
            if (lstBookingDTO.Count > 0)
            {
                grdPendingBooking.DataSource = lstBookingDTO;
                grdPendingBooking.DataBind();
            }
            else
            {
                base.ShowBlankRowInGrid<BookingDTO>(grdPendingBooking);
            }
        }
        else
        {
            base.ShowBlankRowInGrid<BookingDTO>(grdPendingBooking);
        }
    }

    private void GetPendingReportData()
    {
        DateTime fromDate = DateTime.Now;
        DateTime toDate = DateTime.Now;

        if (!String.IsNullOrEmpty(txtToDate.Text.Trim()))
        {
            fromDate = Convert.ToDateTime(txtFromDate.Text);
            toDate = Convert.ToDateTime(txtToDate.Text);
        }

        //Generate pending booking report data
        GeneratePendingReport(fromDate, toDate);
    }

    #endregion
}