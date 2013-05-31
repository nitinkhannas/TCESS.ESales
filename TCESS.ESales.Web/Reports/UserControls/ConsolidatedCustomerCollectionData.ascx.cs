using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects.GhatoCollection;

public partial class Reports_UserControls_ConsolidatedCustomerCollectionData : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetConsolidatedCustomerCollectionData();
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }

    private void GetConsolidatedCustomerCollectionData()
    {
        IList<ConsolidatedCustomerCollectionReportDTO> lstConsolidatedCustomerData = ESalesUnityContainer.
            Container.Resolve<IReportService>().GetConsolidatedCustomerCollection(Convert.ToDateTime(ConfigurationManager.AppSettings["PaymentStartDate"]), 
            DateTime.Now);

        if (lstConsolidatedCustomerData.Count > 0)
        {
            grdConsolidatedCustomerCollection.DataSource = lstConsolidatedCustomerData;
            grdConsolidatedCustomerCollection.DataBind();
        }
        else
        {
            base.ShowBlankRowInGrid<ConsolidatedCustomerCollectionReportDTO>(grdConsolidatedCustomerCollection);
        }
    }
}