#region Namespace

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

#endregion

public partial class Reports_UserControls_BookingsForTheDayDisplay : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtToDate.Attributes.Add("ReadOnly", "true");
            txtFromDate.Attributes.Add("ReadOnly", "true");
            txtToDateValidator.Enabled = false;
            txtFromDateValidator.Enabled = false;
            //Generate loading advice report data
            GenerateBookingsForTheDayDisplayReport((DateTime?)DateTime.Now, (DateTime?)DateTime.Now);
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

        //Generate Bookings For The Day Display report data
        GenerateBookingsForTheDayDisplayReport(fromDate, toDate);
    }
    /// <summary>
    /// Generate Bookings For The Day Display report data
    /// </summary>
    /// <param name="fromDate">From date selection criteria</param>
    /// <param name="toDate">To date selection criteria</param>
    private void GenerateBookingsForTheDayDisplayReport(DateTime? fromDate, DateTime? toDate)
    {
       
        if (fromDate != null)
        {
            IList<BookingDTO> lstLoadingAdviceRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
                .GetLoadingAdivceReport(base.GetAgentByUserId().UAM_Agent_Id, Convert.ToDateTime(fromDate),
                Convert.ToDateTime(toDate));

            if (lstLoadingAdviceRpt.Count > 0)
            {
                grdBookingsForTheDay.DataSource = lstLoadingAdviceRpt;
                grdBookingsForTheDay.DataBind();
            }
            else
            {
                base.ShowBlankRowInGrid<BookingDTO>(grdBookingsForTheDay);
            }
        }
        else
        {
            base.ShowBlankRowInGrid<BookingDTO>(grdBookingsForTheDay);
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
        base.ShowBlankRowInGrid<BookingDTO>(grdBookingsForTheDay);
    }
}