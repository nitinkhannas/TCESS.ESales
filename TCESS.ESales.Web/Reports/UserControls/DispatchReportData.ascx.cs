#region Using directives

using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.CommonLayer.CommonLibrary;

#endregion

public partial class Reports_UserControls_DispatchReportData : BaseUserControl
{
	public event ShowReportEventHandler Event_LoadReport;

    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			txtToDate.Attributes.Add("ReadOnly", "true");
			txtFromDate.Attributes.Add("ReadOnly", "true");

			//Populate active DCA list
			PopulateDCA();
			//Generate cash collection report data
			GenerateDispatchReport((DateTime?)null, (DateTime?)null, base.GetAgentByUserId().UAM_Agent_Id.ToString());
			txtFromDateValidator.Enabled = false;
			txtToDateValidator.Enabled = false;
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

	/// <summary>
	/// Generate dispatch report data
	/// </summary>
	/// <param name="fromDate">From date selection criteria</param>
	/// <param name="toDate">To date selection criteria</param>
	/// <param name="dcaName">dcaName selection criteria</param> 
	private void GenerateDispatchReport(DateTime? fromDate, DateTime? toDate, string dcaName)
	{
        if (fromDate != null)
        {
            int selectedDCA = Convert.ToInt32(base.GetAgentByUserId().UAM_Agent_Id);
            if (ddlDCAGroup.SelectedItem != null)
                selectedDCA = Convert.ToInt32(ddlDCAGroup.SelectedItem.Value);
            IList<DispatchReportDTO> lstDispatchReportRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
                .GetDispatchReport(selectedDCA, (Convert.ToDateTime(fromDate)).Date,
                (Convert.ToDateTime(toDate)).Date);

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
		DateTime fromDate = DateTime.Now;
		DateTime toDate = DateTime.Now;

		if (!String.IsNullOrEmpty(txtToDate.Text.Trim()))
		{
			fromDate = Convert.ToDateTime(txtFromDate.Text);
			toDate = Convert.ToDateTime(txtToDate.Text);
		}

		GenerateDispatchReport(fromDate, toDate, base.GetAgentByUserId().UAM_Agent_Id.ToString());
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
        DateTime fromDate = DateTime.Now;
        DateTime toDate = DateTime.Now;

        if (!String.IsNullOrEmpty(txtToDate.Text.Trim()))
        {
            fromDate = Convert.ToDateTime(txtFromDate.Text);
            toDate = Convert.ToDateTime(txtToDate.Text);
        }
        int selectedDCA = Convert.ToInt32(base.GetAgentByUserId().UAM_Agent_Id);
        if (ddlDCAGroup.SelectedItem != null)
            selectedDCA = Convert.ToInt32(ddlDCAGroup.SelectedItem.Value);
        Event_LoadReport(selectedDCA, fromDate, toDate);
	}
}