﻿using System;
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

public partial class Reports_UserControls_DailyBookingReportforallData : BaseUserControl
{
   // public event ShowDataByDateEventHandler Event_LoadReport;
    public event ShowReportEventHandler Event_LoadReport;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtToDate.Attributes.Add("ReadOnly", "true");
            txtFromDate.Attributes.Add("ReadOnly", "true");
            txtToDateValidator.Enabled = false;
            txtFromDateValidator.Enabled = false;
            if (base.GetAgentByUserId().UAM_Agent_Id != 0)
            {
                lbl_Filter.Visible = false;
                ddlDCAGroup.Visible = false;
            }
            else
            {
                lbl_Filter.Visible = true;
                ddlDCAGroup.Visible = true;
                //Get active agent details from database
                PopulateDCA();
            }
           

            //Generate Daily Booking Report for all DCAs Report
            GenerateDailyBookingReportforallDCAsReport((DateTime?)null, (DateTime?)null);
        }
    }

    /// <summary>
    /// Get active agent details from database
    /// </summary>
    private void PopulateDCA()
    {
        MasterList.GetAgentListInDropDownForReports(ddlDCAGroup);
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
             IList<BookingDTO> dailyBookingReportforallDCAsReport = null;
             int selectedDCA = 0;
             if (ddlDCAGroup.SelectedItem != null)
                 selectedDCA = Convert.ToInt32(ddlDCAGroup.SelectedItem.Value);
             if (selectedDCA  == 0)
            
              dailyBookingReportforallDCAsReport  = ESalesUnityContainer.Container.Resolve<IReportService>()
                    .GetDailyBookingReportforDCA(Convert.ToDateTime(fromDate),
                    Convert.ToDateTime(toDate));
           
            else
          
               dailyBookingReportforallDCAsReport = ESalesUnityContainer.Container.Resolve<IReportService>()
                    .GetDailyBookingReportforallDCAsReport(selectedDCA, Convert.ToDateTime(fromDate),
                    Convert.ToDateTime(toDate));

       


            if (dailyBookingReportforallDCAsReport.Count > 0)
            {
                grdDailyBookingReportforallDCAs.DataSource = dailyBookingReportforallDCAsReport;
                grdDailyBookingReportforallDCAs.DataBind();
            }
            else
            {
                base.ShowBlankRowInGrid<BookingDTO>(grdDailyBookingReportforallDCAs);
            }
        }
        else
        {
            base.ShowBlankRowInGrid<BookingDTO>(grdDailyBookingReportforallDCAs);
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
        base.ShowBlankRowInGrid<BookingDTO>(grdDailyBookingReportforallDCAs);
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
        int selectedDCA = 0;
        if (ddlDCAGroup.SelectedItem != null)
            selectedDCA = Convert.ToInt32(ddlDCAGroup.SelectedItem.Value);
        //Show loading Daily Booking Report for all DCAs Report
        Event_LoadReport(selectedDCA, fromDate, toDate);
    }
}