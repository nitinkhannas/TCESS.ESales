using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Security;

public partial class Reports_UserControls_ConsolidatedAdviceData : BaseUserControl
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
            GenerateLoadingAdviceReport((DateTime?)null, (DateTime?)null);
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
        }
        else
        {
            lblAgentName.Text = "ALL";
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

        //Generate loading advice report data
        GenerateLoadingAdviceReport(fromDate, toDate);
    }

    /// <summary>
    /// Generate Loading Advice report data
    /// </summary>
    /// <param name="fromDate">From date selection criteria</param>
    /// <param name="toDate">To date selection criteria</param>
    private void GenerateLoadingAdviceReport(DateTime? fromDate, DateTime? toDate)
    {
        if (fromDate != null)
        {
            IList<BookingDTO> lstLoadingAdviceRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
                .GetConsolidatedAdviceReport(base.GetAgentByUserId().UAM_Agent_Id, Convert.ToDateTime(fromDate),
                Convert.ToDateTime(toDate));

            if (lstLoadingAdviceRpt.Count > 0)
            {
                grdConsolidatedAdvice.DataSource = lstLoadingAdviceRpt;
                grdConsolidatedAdvice.DataBind();
            }
            else
            {
                base.ShowBlankRowInGrid<BookingDTO>(grdConsolidatedAdvice);
            }
        }
        else
        {
            base.ShowBlankRowInGrid<BookingDTO>(grdConsolidatedAdvice);
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
        base.ShowBlankRowInGrid<BookingDTO>(grdConsolidatedAdvice);
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
}