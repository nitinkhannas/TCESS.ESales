using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

public partial class Reports_UserControls_DCACollectionSupervisorData : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateStampLabel.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
            GenerateDcaCollectionData(DateTime.Now, DateTime.Now);
        }
    }

    private void GenerateDcaCollectionData(DateTime fromDate, DateTime toDate)
    {
        List<object> lstCounterwiseBookingDetails = new List<object>();

        lstCounterwiseBookingDetails = ESalesUnityContainer.Container.Resolve<IReportService>()
            .GetBookingsCounterWise(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate)).ToList();

        if (lstCounterwiseBookingDetails.Count > 0)
        {
            grdDCACollection.DataSource = lstCounterwiseBookingDetails;
            grdDCACollection.DataBind();
        }
    }

    protected void UpdateTimer_Tick(object sender, EventArgs e)
    {
        DateStampLabel.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
        GenerateDcaCollectionData(DateTime.Now, DateTime.Now);
    }
}