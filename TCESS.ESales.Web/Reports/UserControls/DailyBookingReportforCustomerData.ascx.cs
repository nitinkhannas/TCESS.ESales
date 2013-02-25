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


public partial class Reports_UserControls_DailyBookingReportforCustomerData : BaseUserControl
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

            //Generate Daily Booking Report for all DCAs Report
            GenerateDailyBookingReportforallDCAsReport((DateTime?)null, (DateTime?)null);
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

        //Generate Daily Booking Report for all DCAs Report
        GenerateDailyBookingReportforallDCAsReport(fromDate, toDate);
    }

    /// <summary>
    /// Generate Daily Booking Report for all DCAs Report
    /// </summary>
    /// <param name="fromDate">From date selection criteria</param>
    /// <param name="toDate">To date selection criteria</param>
    private void GenerateDailyBookingReportforallDCAsReport(DateTime? fromDate, DateTime? toDate)
    {
        if (fromDate != null)
        {
            IList<BookingDTO> dailyBookingReportforallDCAsReport = ESalesUnityContainer.Container.Resolve<IReportService>()
                .GetDailyBookingReportforCustomerReport(Convert.ToInt32(Membership.GetUser().ProviderUserKey), Convert.ToDateTime(fromDate),
                Convert.ToDateTime(toDate));

            if (dailyBookingReportforallDCAsReport.Count > 0)
            {
                grdDailyBookingReportforCustomer.DataSource = dailyBookingReportforallDCAsReport;
                grdDailyBookingReportforCustomer.DataBind();
            }
            else
            {
                base.ShowBlankRowInGrid<BookingDTO>(grdDailyBookingReportforCustomer);
            }
        }
        else
        {
            base.ShowBlankRowInGrid<BookingDTO>(grdDailyBookingReportforCustomer);
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
        base.ShowBlankRowInGrid<BookingDTO>(grdDailyBookingReportforCustomer);
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

        //Show loading Daily Booking Report for all DCAs Report
        Event_LoadReport(Convert.ToInt32(Membership.GetUser().ProviderUserKey), fromDate, toDate);
    }
}