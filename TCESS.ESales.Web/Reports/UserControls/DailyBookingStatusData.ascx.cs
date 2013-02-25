#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Reports_UserControls_DailyBookingStatusData : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            DateStampLabel.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
            //Generate loading advice report data
            GenerateDailyBookingStatusDataReport(DateTime.Now, DateTime.Now);
            GenerateBookingBreakup(DateTime.Now, DateTime.Now);
        }
        CreateDynamicTable(DateTime.Now, DateTime.Now);
    }

    private void GenerateInitialValues(DateTime fromDate, DateTime toDate)
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
                truckOuts = lstBookingStatusData[5],
                prevDayBookings = lstBookingStatusData[6],
                pendings = lstBookingStatusData[7]
            };
            var bookingStatusDataList = (new[] { bookingStatusData }).ToList();

        }
    }

    private void CreateDynamicTable(DateTime fromDate, DateTime toDate)
    {
        Table tbl = new Table();
        tbl.BorderWidth = 1;
        tbl.CellPadding = 1;

        tbl.Attributes.Add("class", "tableizer-table");

        PlaceHolder1.Controls.Clear();
        PlaceHolder1.Controls.Add(tbl);

        Dictionary<string, int> dcawiseGroupedList = new Dictionary<string, int>();

        IList<AgentDTO> lstAgent = ESalesUnityContainer.Container.Resolve<IAgentService>().GetAgentList().ToList();

        IList<BookingDTO> lstBookings = ESalesUnityContainer.Container.Resolve<IReportService>()
               .GetDailyBookingReportforDCA(DateTime.Now, DateTime.Now).ToList();

        if (lstBookings.Count > 0)
        {
            dcawiseGroupedList = lstBookings.GroupBy(F => F.Booking_Agent_AgentShortName).ToDictionary(x => x.Key, x => x.Count());
        }

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
                truckOuts = lstBookingStatusData[5],
                prevDayBookings = lstBookingStatusData[6],
                pendings = lstBookingStatusData[7]
            };
            var bookingStatusDataList = (new[] { bookingStatusData }).ToList();

            int toLimit = 8;
            if (lstAgent.Count > 5)
            {
                toLimit += lstAgent.Count - 5;
            }

            for (int k = 0; k <= toLimit; k++)
            {
                TableRow tr = new TableRow();
                tr.ID = k.ToString();
                for (int j = 0; j <= 3; j++)
                {
                    TableCell tc = new TableCell();

                    if (k == 0 && j == 0)
                    {
                        tc.Text = "BOOKINGS";
                        tr.Cells.Add(tc);
                        tc.Attributes.Add("style", "width:220px; background-color:#397dbc ; color:#FFFFFF");
                    }
                    else if (k == toLimit && j == 2)
                    {
                        tc.Text = "Closing Pendings";
                        tr.Cells.Add(tc);
                    }
                    else if (k == 0 && j == 1 || k == 0 && j == 3)
                    {
                        tc.Text = string.Empty;
                        tr.Cells.Add(tc);
                        tc.Attributes.Add("style", "width:70px; background-color:#397dbc");
                    }
                    else if (k == toLimit && j == 3)
                    {
                        tc.Text = bookingStatusDataList[0].pendings.ToString();
                        tr.Cells.Add(tc);
                    }
                    else if (k == 0 && j == 2)
                    {
                        tc.Text = "TRUCKS";
                        tr.Cells.Add(tc);
                        tc.Attributes.Add("style", "width:220px; background-color:#397dbc ;color:#FFFFFF");
                    }
                    else if (k == 1 && j == 0)
                    {
                        tc.Text = "SMS Accepted";
                        tr.Cells.Add(tc);
                    }
                    else if (k == 1 && j == 1)
                    {
                        tc.Text = bookingStatusDataList[0].smsAccepted.ToString();
                        tr.Cells.Add(tc);
                    }
                    else if (k == 1 && j == 2)
                    {
                        tc.Text = "Opening Pendings";
                        tr.Cells.Add(tc);
                    }
                    else if (k == 1 && j == 3)
                    {
                        tc.Text = bookingStatusDataList[0].prevDayBookings.ToString();
                        tr.Cells.Add(tc);
                    }
                    else if (k == 2 && j == 0)
                    {
                        tc.Text = "Bookings";
                        tr.Cells.Add(tc);
                    }
                    else if (k == 2 && j == 1)
                    {
                        tc.Text = bookingStatusDataList[0].bookings.ToString();
                        tr.Cells.Add(tc);
                    }
                    else if (k == 3 && j == 0)
                    {
                        tc.Text = "Allocated to:";
                        tr.Cells.Add(tc);
                    }
                    else if (k == 3 && j == 1)
                    {
                        tc.Text = string.Empty;
                        tr.Cells.Add(tc);
                    }
                    else if (k == 3 && j == 2)
                    {
                        tc.Text = "Loading Advice Issue";
                        tr.Cells.Add(tc);
                    }
                    else if (k == 3 && j == 3)
                    {
                        tc.Text = bookingStatusDataList[0].loadingAdvIssue.ToString();
                        tr.Cells.Add(tc);
                    }
                    else if (k == 4 && j == 2)
                    {
                        tc.Text = "Gate In";
                        tr.Cells.Add(tc);
                    }
                    else if (k == 4 && j == 3)
                    {
                        tc.Text = bookingStatusDataList[0].truckIn.ToString();
                        tr.Cells.Add(tc);
                    }
                    else if (k == 5 && j == 2)
                    {
                        tc.Text = "Loaded";
                        tr.Cells.Add(tc);
                    }
                    else if (k == 5 && j == 3)
                    {
                        tc.Text = bookingStatusDataList[0].material.ToString();
                        tr.Cells.Add(tc);
                    }
                    else if (k == 6 && j == 2)
                    {
                        tc.Text = "Truck Out";
                        tr.Cells.Add(tc);
                    }
                    else if (k == 6 && j == 3)
                    {
                        tc.Text = bookingStatusDataList[0].truckOuts.ToString();
                        tr.Cells.Add(tc);
                    }
                    else
                    {
                        tc.Text = string.Empty;
                        tr.Cells.Add(tc);
                    }

                    tbl.Rows.Add(tr);
                }
            }
        }
        int cnt = 4;
        foreach (AgentDTO i in lstAgent)
        {
            foreach (TableRow tr in tbl.Rows)
            {
                if (Convert.ToInt32(tr.ID) >= cnt)
                {
                    if (dcawiseGroupedList.Count == 0)
                    {
                        tr.Cells[0].Text = i.Agent_ShortName;
                        tr.Cells[1].Text = "0";
                        cnt++;
                        break;
                    }
                    else
                    {
                        foreach (KeyValuePair<string, int> k in dcawiseGroupedList)
                        {
                            if (k.Key == i.Agent_ShortName)
                            {
                                tr.Cells[0].Text = k.Key;
                                tr.Cells[1].Text = k.Value.ToString();
                                cnt++;
                                break;
                            }
                        }
                    }
                    break;
                }
            }
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
                    truckOuts = lstBookingStatusData[5],
                    prevDayBookings = lstBookingStatusData[6],
                    pendings = lstBookingStatusData[7]
                };
                var bookingStatusDataList = (new[] { bookingStatusData }).ToList();
                lblSmsaccepteddata.Text = bookingStatusDataList[0].smsAccepted.ToString();
                lblTotalbookingdata.Text = bookingStatusDataList[0].bookings.ToString();
                lblLoadingadvdata.Text = bookingStatusDataList[0].loadingAdvIssue.ToString();
                lblGateindata.Text = bookingStatusDataList[0].truckIn.ToString();
                lblloadeddata.Text = bookingStatusDataList[0].material.ToString();
                lblTruckoutdata.Text = bookingStatusDataList[0].truckOuts.ToString();
                lblOpeningsData.Text = bookingStatusDataList[0].prevDayBookings.ToString();
                lblPendingsData.Text = bookingStatusDataList[0].pendings.ToString();
            }
            else
            {
                Showzerovalue();
                // base.ShowBlankRowInGrid<BookingDTO>(grdDailyBookingStatus);
            }
        }
        else
        {
            Showzerovalue();
            //base.ShowBlankRowInGrid<BookingDTO>(grdDailyBookingStatus);
        }
    }

    public void Showzerovalue()
    {
        lblSmsaccepteddata.Text = "0";
        lblTotalbookingdata.Text = "0";
        lblLoadingadvdata.Text = "0";
        lblGateindata.Text = "0";
        lblloadeddata.Text = "0";
        lblTruckoutdata.Text = "0";
        lblOpeningsData.Text = "0";
        lblPendingsData.Text = "0";
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
        CreateDynamicTable(DateTime.Now, DateTime.Now);
    }

    private void GenerateBookingBreakup(DateTime fromDate, DateTime toDate)
    {
        Dictionary<string, int> dcawiseGroupedList = new Dictionary<string, int>();

        IList<BookingDTO> lstBookings = ESalesUnityContainer.Container.Resolve<IReportService>()
               .GetDailyBookingReportforDCA(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate)).ToList();

        if (lstBookings.Count > 0)
        {
            dcawiseGroupedList = lstBookings.GroupBy(F => F.Booking_Agent_AgentShortName).ToDictionary(x => x.Key, x => x.Count());

            foreach (var pair in dcawiseGroupedList)
            {
                switch (pair.Key)
                {
                    case "GAPL":
                        lblGapldata.Text = pair.Value.ToString();
                        break;
                    case "GSA":
                        lblGsadata.Text = pair.Value.ToString();
                        break;
                    case "MVS":
                        lblMvsdata.Text = pair.Value.ToString();
                        break;
                    case "NKCPL":
                        lblNkcpldata.Text = pair.Value.ToString();
                        break;
                    case "TIPL":
                        lblTipldata.Text = pair.Value.ToString();
                        break;
                }
            }
        }
        else
            BookingBreakupShowzerovalue();
    }

    public void BookingBreakupShowzerovalue()
    {
        lblGapldata.Text = "0";
        lblGsadata.Text = "0";
        lblMvsdata.Text = "0";
        lblNkcpldata.Text = "0";
        lblTipldata.Text = "0";
    }

    #endregion
}