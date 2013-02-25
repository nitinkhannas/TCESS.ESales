#region Namespace

using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Reports_RoadPermitReport : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucRoadPermitReportData.Event_LoadReport += ucRoadPermitReportData_Event_LoadReport;
        ucRoadPermitReport.Event_CloseScreen += ucRoadPermitReport_Event_CloseScreen;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowInitialValues();
        }
    }

    /// <summary>
    /// Show Page Values when it initially Loads or Refreshes
    /// </summary>
    private void ShowInitialValues()
    {
        //Sets visibility of frames that contains user controls
        pnlRoadPermitReportData.Visible = true;
        pnlRoadPermitReportPrint.Visible = false;
    }

    public void ucRoadPermitReportData_Event_LoadReport(DateTime fromDate, DateTime toDate)
    {
        pnlRoadPermitReportData.Visible = false;
        pnlRoadPermitReportPrint.Visible = true;
        ucRoadPermitReport.LoadReport(fromDate, toDate);
    }

    public void ucRoadPermitReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}