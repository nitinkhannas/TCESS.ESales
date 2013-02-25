#region Using directive

using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using System.Web.UI.WebControls;
using TCESS.ESales.CommonLayer.CommonLibrary;

#endregion

public partial class Reports_UserControls_DailyBookingPendingReportForAllData : BaseUserControl
{
   // public event ShowDataByDateEventHandler Event_LoadReport;
    public event ShowReportEventHandler Event_LoadReport;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtToDate.Attributes.Add("ReadOnly", "true");
            txtFromDate.Attributes.Add("ReadOnly", "true");
            txtFromDateValidator.Enabled = false;
            txtToDateValidator.Enabled = false;
            if (base.GetAgentByUserId().UAM_Agent_Id != 0)
            {
                lbl_Filter.Visible = false;
                ddlDCAGroup.Visible = false;
            }
            else
            {
                lbl_Filter.Visible = true;
                ddlDCAGroup.Visible = true;
                //Get active agent details from database
                PopulateDCA();
            }

            //Generate pending booking report data
            GenerateBookingPendingReport((DateTime?)null, (DateTime?)null);
        }
    }

    /// <summary>
    /// Get active agent details from database
    /// </summary>
    private void PopulateDCA()
    {
        MasterList.GetAgentListInDropDownForReports(ddlDCAGroup);
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        GetBookingPendingData();
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
        base.ShowBlankRowInGrid<BookingDTO>(grdBooking);
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
        int selectedDCA = 0;
        if (ddlDCAGroup.SelectedItem != null)
            selectedDCA = Convert.ToInt32(ddlDCAGroup.SelectedItem.Value);

        //Show pending booking report
        Event_LoadReport(selectedDCA,fromDate, toDate);
    }

    protected void grdBooking_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdBooking.PageIndex = e.NewPageIndex;

        //Gets booking pending details
        GetBookingPendingData();
    }

    #region Private Methods

    /// <summary>
    /// Generate pending booking report data
    /// </summary>
    /// <param name="fromDate">From date selection criteria</param>
    /// <param name="toDate">To date selection criteria</param>
    private void GenerateBookingPendingReport(DateTime? fromDate, DateTime? toDate)
    {
        if (fromDate != null)
        {
            IList<BookingDTO> lstBookingDTO = null;
            int selectedDCA = 0;
            if (ddlDCAGroup.SelectedItem != null)
                selectedDCA = Convert.ToInt32(ddlDCAGroup.SelectedItem.Value);
               if (selectedDCA  == 0)
                lstBookingDTO = ESalesUnityContainer.Container.Resolve<IReportService>()
                .GetPendingBookingForAllReport(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
            else
                   lstBookingDTO = ESalesUnityContainer.Container.Resolve<IReportService>()
               .GetPendingBookingReport( selectedDCA ,Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));


            //If pending booking report contains some data
            if (lstBookingDTO.Count > 0)
            {
                grdBooking.DataSource = lstBookingDTO;
                grdBooking.DataBind();
            }
            else
            {
                base.ShowBlankRowInGrid<BookingDTO>(grdBooking);
            }
        }
        else
        {
            base.ShowBlankRowInGrid<BookingDTO>(grdBooking);
        }
    }

    private void GetBookingPendingData()
    {
        DateTime fromDate = DateTime.Now;
        DateTime toDate = DateTime.Now;

        if (!String.IsNullOrEmpty(txtToDate.Text.Trim()))
        {
            fromDate = Convert.ToDateTime(txtFromDate.Text);
            toDate = Convert.ToDateTime(txtToDate.Text);
        }

        //Generate pending booking report data
        GenerateBookingPendingReport(fromDate, toDate);
    }

    #endregion
}