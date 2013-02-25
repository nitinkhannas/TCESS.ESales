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

public partial class Reports_Form27CReport : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucForm27CReportData.Event_LoadReport += ucForm27CReportData_Event_LoadReport;
        ucForm27CReport.Event_CloseScreen += ucForm27CReport_Event_CloseScreen;
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
        pnlForm27CReportData.Visible = true;
        pnlForm27CReportPrint.Visible = false;
    }

    public void ucForm27CReportData_Event_LoadReport(DateTime fromDate, DateTime toDate)
    {
        pnlForm27CReportData.Visible = false;
        pnlForm27CReportPrint.Visible = true;
        ucForm27CReport.LoadReport(fromDate, toDate);
    }

    public void ucForm27CReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }
}