#region Using directives

using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.CommonLayer.CommonLibrary;

#endregion

public partial class Reports_UserControls_MonthlySalesStatementandReconciliationData : BaseUserControl
{
    int month;
    public event ShowDateMonthEventHandler Event_LoadReport;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime tnow = DateTime.Now;
            ddlMonth.SelectedValue = tnow.Month.ToString();
            //Generate cash collection report data
           // GenerateDispatchReport((DateTime?)DateTime.Now.Date, System.DateTime.Now.Month);
            txtFromDateValidator.Enabled = true;
            base.ShowBlankRowInGrid<SalesReportDTO>(grdSalesStatement);
        }
        base.ShowBlankRowInGrid<SalesReportDTO>(grdSalesStatement);
    }
    private void GenerateDispatchReport(DateTime? fromDate, int month)
    {
        if (fromDate != null)
        {
            IList<SalesReportDTO> lstDispatchReportRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
                .GetSalesReport(Convert.ToDateTime(fromDate),month);

            //If Dispatch report contains some data
            if (lstDispatchReportRpt.Count > 0)
            {
                grdSalesStatement.DataSource = lstDispatchReportRpt;
                grdSalesStatement.DataBind();
            }
            else
            {
                base.ShowBlankRowInGrid<SalesReportDTO>(grdSalesStatement);
            }
        }
        else
        {
            base.ShowBlankRowInGrid<SalesReportDTO>(grdSalesStatement);
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        DateTime fromDate = DateTime.Now;
        month = Int32.Parse(ddlMonth.SelectedValue);

        if (!String.IsNullOrEmpty(txtFromDate.Text.Trim()))
        {
            fromDate = Convert.ToDateTime(txtFromDate.Text);            
        }
        GenerateDispatchReport(fromDate, month);
    }
    
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        DateTime fromDate = DateTime.Now;
        month = Int32.Parse(ddlMonth.SelectedValue);

        if (!String.IsNullOrEmpty(txtFromDate.Text.Trim()))
        {
            fromDate = Convert.ToDateTime(txtFromDate.Text);            
        }
        Event_LoadReport(fromDate, month);
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMonth.SelectedValue = (Convert.ToInt32(ddlMonth.SelectedValue)).ToString();
    }
}