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

public partial class Reports_UserControls_DistrictWiseReportofInactiveCustomersData : BaseUserControl
{
    public event ShowReportEventHandler Event_LoadReport;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //District Wise Report of Inactive Customers
            GenerateDistrictWiseReportofInactiveCustomersReport((DateTime?)System.DateTime.Now, (DateTime?)System.DateTime.Now);
        }
    }
    /// <summary>
    /// Generate Loading Advice report data
    /// </summary>
    /// <param name="fromDate">From date selection criteria</param>
    /// <param name="toDate">To date selection criteria</param>
    private void GenerateDistrictWiseReportofInactiveCustomersReport(DateTime? fromDate, DateTime? toDate)
    {
        if (fromDate != null)
        {
            IList<BookingDTO> lstLoadingAdviceRpt = ESalesUnityContainer.Container.Resolve<IReportService>()
                .GetDistrictWiseReportofInactiveCustomersReport(base.GetAgentByUserId().UAM_Agent_Id, Convert.ToDateTime(fromDate),
                Convert.ToDateTime(toDate));

            if (lstLoadingAdviceRpt.Count > 0)
            {
                grdDistrictWiseReportofInactiveCustomers.DataSource = lstLoadingAdviceRpt;
                grdDistrictWiseReportofInactiveCustomers.DataBind();
            }
            else
            {
                base.ShowBlankRowInGrid<BookingDTO>(grdDistrictWiseReportofInactiveCustomers);
            }
        }
        else
        {
            base.ShowBlankRowInGrid<BookingDTO>(grdDistrictWiseReportofInactiveCustomers);
        }
    }    

    public void btnPrint_Click(object sender, EventArgs e)
    {
        DateTime fromDate = DateTime.Now;
        DateTime toDate = DateTime.Now;       
        //Show loading advice report
        Event_LoadReport(base.GetAgentByUserId().UAM_Agent_Id, fromDate, toDate);
    }
}