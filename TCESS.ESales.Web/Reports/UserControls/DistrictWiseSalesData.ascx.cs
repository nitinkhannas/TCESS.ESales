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

public partial class Reports_UserControls_DistrictWiseSalesData : BaseUserControl
{
    int month;
    public event ShowMonthEventHandler Event_LoadReport;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            PopulateDCA();
            DateTime tnow = DateTime.Now;
            ddlMonth.SelectedValue = tnow.Month.ToString();
            GenerateDistrictWiseSalesReport(month);
            GenerateDistrictWiseSalesReport(month);
        }
        //month = Int32.Parse(ddlMonth.SelectedValue);
        //GenerateDistrictWiseSalesReport(month);
    }

    /// <summary>
    /// Get active DCA list and populate drop down controls
    /// </summary>
    private void PopulateDCA()
    {
        if (base.GetAgentByUserId().UAM_Agent_Id != 0)
        {
            lblAgentName.Text = base.GetAgentByUserId().UAM_Agent_Name;
        }
        else
        {
            lblAgentName.Text = "ALL";
        }
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        month = Int32.Parse(ddlMonth.SelectedValue);
        //Generate
        GenerateDistrictWiseSalesReport(month);
    }
    private void GenerateDistrictWiseSalesReport(int month)
    {        
        IList<SalesReportDTO> lstDistrictWiseSalesRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
                .GetDistrictWiseSalesReport(base.GetAgentByUserId().UAM_Agent_Id, month);
         if (lstDistrictWiseSalesRpt.Count > 0)
            {
                PopulateDCA();
                grdDistrictWiseSales.DataSource = lstDistrictWiseSalesRpt;
                grdDistrictWiseSales.DataBind();
            }
            else
            {
                PopulateDCA();
                base.ShowBlankRowInGrid<SalesReportDTO>(grdDistrictWiseSales);
            }
        }        
    
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMonth.SelectedValue = (Convert.ToInt32(ddlMonth.SelectedValue)).ToString();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        month = Int32.Parse(ddlMonth.SelectedValue); 
        Event_LoadReport(Convert.ToInt32(base.GetAgentByUserId().UAM_Agent_Id), month);
    }
}