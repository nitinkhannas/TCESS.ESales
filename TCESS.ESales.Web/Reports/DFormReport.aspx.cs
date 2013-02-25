#region Namespace
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.PersistenceLayer.Entity;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
#endregion

public partial class Reports_DFormReport : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucDformReportData.Event_LoadReport += ucDFormReportData_Event_LoadReport;
        ucDformReport.Event_CloseScreen += ucDFormReport_Event_CloseScreen;
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
        pnlDformReportData.Visible = true;
        pnlDformReportPrint.Visible = false;
    }

    public void ucDFormReportData_Event_LoadReport(DateTime fromDate, DateTime toDate)
    {
        pnlDformReportData.Visible = false;
        pnlDformReportPrint.Visible = true;
        ucDformReport.LoadReport(fromDate, toDate);
    }

    public void ucDFormReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}