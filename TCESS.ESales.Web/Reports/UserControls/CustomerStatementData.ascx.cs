#region Using directive

using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using System.Web.UI.WebControls;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.DataTransferObjects.GhatoCollection;

#endregion

public partial class Reports_UserControls_CustomerStatementData : BaseUserControl
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
        }
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
        base.ShowBlankRowInGrid<CustomerCollectionSettlementDTO>(grdBooking);
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
        ////Show pending booking report
        //Event_LoadReport(selectedDCA, fromDate, toDate);
    }


    #region Private Methods

    /// <summary>
    /// Generate pending booking report data
    /// </summary>
    /// <param name="fromDate">From date selection criteria</param>
    /// <param name="toDate">To date selection criteria</param>
    private void GenerateCustomerStatement(int CustomerId, DateTime fromDate, DateTime toDate)
    {
        if (fromDate != null)
        {
            IList<CustomerCollectionSettlementDTO> lstCustomerDTO = null;

            lstCustomerDTO = ESalesUnityContainer.Container.Resolve<IReportService>().GetConsolidatedCollectionReport(CustomerId, fromDate, toDate);
            IList<BookingDTO> lstBookingDTO = ESalesUnityContainer.Container.Resolve<IBookingService>().GetHoldPendingBooking(CustomerId, fromDate, toDate);

            //If pending booking report contains some data
            if (lstCustomerDTO.Count > 0)
            {
                grdBooking.DataSource = lstCustomerDTO;
                grdBooking.DataBind();
            }
            else
            {
                base.ShowBlankRowInGrid<CustomerCollectionSettlementDTO>(grdBooking);
            }

            if (lstBookingDTO.Count > 0)
            {
                grdHold.DataSource = lstBookingDTO;
                grdHold.DataBind();
            }
            else
            {
                base.ShowBlankRowInGrid<BookingDTO>(grdHold);
            }
        }
        else
        {
            base.ShowBlankRowInGrid<CustomerCollectionSettlementDTO>(grdBooking);
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

        CustomerDTO cust = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetCustomerDetailsByCode(txtCustomerCode.Text.Trim());
        if (cust.Cust_Id > 0)
        {
            //Generate Customer Statement
            GenerateCustomerStatement(cust.Cust_Id, fromDate, toDate);
        }
    }

    #endregion
}