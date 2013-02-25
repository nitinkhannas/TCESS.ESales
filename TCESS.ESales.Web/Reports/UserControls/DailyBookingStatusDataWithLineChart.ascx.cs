#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Reports_UserControls_DailyBookingStatusDataWithLineChart : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Generate loading advice report data
            GenerateDailyBookingStatusDataReport(DateTime.Now, DateTime.Now);
            GenerateBookingBreakup(DateTime.Now, DateTime.Now);
        }
    }

    #region Private Methods

    /// <summary>
    /// Generate Loading Advice report data
    /// </summary>
    /// <param name="fromDate">From date selection criteria</param>
    /// <param name="toDate">To date selection criteria</param>
    private void GenerateDailyBookingStatusDataReport(DateTime fromDate, DateTime toDate)
    {
        if (fromDate != null)
        {
            IList<object> lstBookingStatusData = ESalesUnityContainer.Container.Resolve<IReportService>()
                .GetDailyBookingStatusReport(base.GetAgentByUserId().UAM_Agent_Id, Convert.ToDateTime(fromDate),
                Convert.ToDateTime(toDate)).ToList();

            if (lstBookingStatusData.Count > 0)
            {
                var bookingStatusData = new
                {
                    smsAccepted = lstBookingStatusData[0],
                    bookings = lstBookingStatusData[1],
                    loadingAdvIssue = lstBookingStatusData[2],
                    truckIn = lstBookingStatusData[3],
                    material = lstBookingStatusData[4],
                    truckOuts = lstBookingStatusData[5]
                };
                var bookingStatusDataList = (new[] { bookingStatusData }).ToList();
                
                grdDailyBookingStatus.DataSource = bookingStatusDataList;
                grdDailyBookingStatus.DataBind();

                // Generate bar chart from data
                for (int counter = 0; counter < lstBookingStatusData.Count; counter++)
                {
                    Chart1.Series[0].Points[counter].SetValueY(lstBookingStatusData[counter]);
                }
                Chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            }
            else
            {
                // base.ShowBlankRowInGrid<BookingDTO>(grdDailyBookingStatus);
            }
        }
        else
        {
            //base.ShowBlankRowInGrid<BookingDTO>(grdDailyBookingStatus);
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
        GenerateDailyBookingStatusDataReport(fromDate, toDate);
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

    }

    protected void UpdateTimer_Tick(object sender, EventArgs e)
    {
        DateStampLabel.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
        GenerateDailyBookingStatusDataReport(DateTime.Now, DateTime.Now);
        GenerateBookingBreakup(DateTime.Now, DateTime.Now);
    }

    private void GenerateBookingBreakup(DateTime fromDate, DateTime toDate)
    {
        List<object> dcawiseGroupedlists = new List<object>();
        IList<BookingDTO> lstBookings = ESalesUnityContainer.Container.Resolve<IReportService>()
               .GetDailyBookingReportforDCA(Convert.ToDateTime(fromDate),
               Convert.ToDateTime(toDate)).ToList();
        if (lstBookings.Count > 0)
        {
            dcawiseGroupedlists = lstBookings.GroupBy(F => F.Booking_Agent_AgentShortName).Select(lst => new { agent = lst.Key, Counts = lst.Count() }).ToList<object>();

            grdBookingBreakup.DataSource = dcawiseGroupedlists;
            grdBookingBreakup.DataBind();
            lblPageName.Visible = true;
        }
    }

    #endregion
}