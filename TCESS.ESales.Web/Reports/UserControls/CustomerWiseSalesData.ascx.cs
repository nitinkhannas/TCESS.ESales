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

public partial class Reports_UserControls_CustomerWiseSalesData : BaseUserControl
{
   // public event ShowMonthEventHandler Event_LoadReport;
    public event ShowMonthYearEventHandler Event_LoadReport;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime tnow = DateTime.Now;
            ddlMonth.SelectedValue = tnow.Month.ToString();
            ddlyear.SelectedValue = tnow.Year.ToString();
            base.ShowBlankRowInGrid<CustomerwiseSalesReportDTO>(grdDistrictWiseSales);
        }  
    }
    
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        //Generate
        GenerateCustomerWiseSalesReport(Int32.Parse(ddlMonth.SelectedValue), Int32.Parse(ddlyear.SelectedValue));
    }

    private void GenerateCustomerWiseSalesReport(int month, int year)
    {
        IList<CustomerwiseSalesReportDTO> lstDistrictWiseSalesRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
                .GetCustomerWiseSalesReport(base.GetAgentByUserId().UAM_Agent_Id, month,year);
        if (lstDistrictWiseSalesRpt.Count > 0)
        {
            grdDistrictWiseSales.DataSource = lstDistrictWiseSalesRpt;
            grdDistrictWiseSales.DataBind();
        }
        else
        {
           base.ShowBlankRowInGrid<CustomerwiseSalesReportDTO>(grdDistrictWiseSales);
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Event_LoadReport(Convert.ToInt32(base.GetAgentByUserId().UAM_Agent_Id),Int32.Parse(ddlMonth.SelectedValue),Int32.Parse(ddlyear.SelectedValue));
    }
}