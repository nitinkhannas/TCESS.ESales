#region Using directives

using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.CommonLayer.CommonLibrary;

#endregion

public partial class Reports_UserControls_ConsolidatedBookingandSaleData : BaseUserControl
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
            PopulateDCA();
            //Generate loading advice report data
            GenerateLoadingConsolidatedBookingandSaleData((DateTime?)null, (DateTime?)null);
        }
    }

    /// <summary>
    /// Get active DCA list and populate drop down controls
    /// </summary>
    private void PopulateDCA()
    {
        if (base.GetAgentByUserId().UAM_Agent_Id != 0)
        {
            lblAgentName.Text = base.GetAgentByUserId().UAM_Agent_Name;
            lbl_Filter.Visible = false;
            ddlDCAGroup.Visible = false;
        }
        else
        {
            lbl_Filter.Visible = true;
            ddlDCAGroup.Visible = true;
            lblAgentName.Visible = false;
            lblDCAName.Visible = false;
            MasterList.GetAgentListInDropDownForReports(ddlDCAGroup);
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

        //Generate loading ConsolidatedBooking and Sale Data
        GenerateLoadingConsolidatedBookingandSaleData(fromDate, toDate);
    }

    /// <summary>
    /// Generate Loading Consolidated Booking and Sale Data
    /// </summary>
    /// <param name="fromDate">From date selection criteria</param>
    /// <param name="toDate">To date selection criteria</param>
    private void GenerateLoadingConsolidatedBookingandSaleData(DateTime? fromDate, DateTime? toDate)
    {
        if (fromDate != null)
        {
            int selectedDCA = Convert.ToInt32(base.GetAgentByUserId().UAM_Agent_Id);
            if (ddlDCAGroup.SelectedItem != null)
                selectedDCA = Convert.ToInt32(ddlDCAGroup.SelectedItem.Value);
            IList<DispatchReportDTO> lstLoadingAdviceRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
                .GetDispatchReport(selectedDCA, Convert.ToDateTime(fromDate),
                Convert.ToDateTime(toDate));

            if (lstLoadingAdviceRpt.Count > 0)
            {
                grdBookingSale.DataSource = lstLoadingAdviceRpt;
                grdBookingSale.DataBind();
            }
            else
            {
                base.ShowBlankRowInGrid<DispatchReportDTO>(grdBookingSale);
            }
        }
        else
        {
            base.ShowBlankRowInGrid<DispatchReportDTO>(grdBookingSale);
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
        base.ShowBlankRowInGrid<DispatchReportDTO>(grdBookingSale);
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
        int selectedDCA = Convert.ToInt32(base.GetAgentByUserId().UAM_Agent_Id);
        if (ddlDCAGroup.SelectedItem != null)
            selectedDCA = Convert.ToInt32(ddlDCAGroup.SelectedItem.Value);
        Event_LoadReport(selectedDCA, fromDate, toDate);
    }
}