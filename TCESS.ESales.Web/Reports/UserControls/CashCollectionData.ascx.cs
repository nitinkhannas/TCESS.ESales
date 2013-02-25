#region Using directives

using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.CommonLayer.CommonLibrary;

#endregion

public partial class Reports_UserControls_CashCollectionData : BaseUserControl
{
    public event ShowReportEventHandler Event_LoadReport;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtToDate.Attributes.Add("ReadOnly", "true");
            txtFromDate.Attributes.Add("ReadOnly", "true");

            //Generate cash collection report data
            GenerateCashCollectionReport((DateTime?)null, (DateTime?)null);
            txtFromDateValidator.Enabled = false;
            txtToDateValidator.Enabled = false;
        }
    }

    /// <summary>
    /// Generate Cash collection report data
    /// </summary>
    /// <param name="fromDate">From date selection criteria</param>
    /// <param name="toDate">To date selection criteria</param>
    private void GenerateCashCollectionReport(DateTime? fromDate, DateTime? toDate)
    {
        if (fromDate != null)
        {            
            IList<MoneyReceiptDTO> lstCashCollectionRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
                .GetCashCollectionReport(base.GetAgentByUserId().UAM_Agent_Id, Convert.ToDateTime(fromDate),
                Convert.ToDateTime(toDate));

            //If cash collection report contains some data
            if (lstCashCollectionRpt.Count > 0)
            {
                grdCashcollection.DataSource = lstCashCollectionRpt;
                grdCashcollection.DataBind();
                
            }
            else
            {
                base.ShowBlankRowInGrid<MoneyReceiptDTO>(grdCashcollection);
                
            }
        }
        else
        {
            base.ShowBlankRowInGrid<MoneyReceiptDTO>(grdCashcollection);
            
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
        
        GenerateCashCollectionReport(fromDate, toDate);
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
        base.ShowBlankRowInGrid<MoneyReceiptDTO>(grdCashcollection);
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

        Event_LoadReport(base.GetAgentByUserId().UAM_Agent_Id, fromDate, toDate);
    }
}