#region Namespaces

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Supervisor_UserControls_ManageInactiveTrucks : BaseUserControl
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillGridWithTruckDetails();
        }
    }

    private void FillBlankGrid()
    {
        ShowBlankRowInGrid<TruckVerificationDTO>(grdManageInactiveTrucks);
    }

    private void FillGridWithTruckDetails()
    {
        IList<TruckVerificationDTO> lstInactiveTrucks = new List<TruckVerificationDTO>();
        lstInactiveTrucks = ESalesUnityContainer.Container.Resolve<ITruckService>().GetAllInactiveTruckDetails();

        if (lstInactiveTrucks.Count > 0)
        {
            grdManageInactiveTrucks.DataSource = lstInactiveTrucks;
            grdManageInactiveTrucks.DataBind();
        }
        else
        {
            FillBlankGrid();
        }
    }

    protected void grdManageInactiveTrucks_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdManageInactiveTrucks.PageIndex = e.NewPageIndex;
        FillGridWithTruckDetails();
    }

    protected void grdManageInactiveTrucks_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int truckType = 0;
        string truckNo = null;

        if (e.CommandName == "Activate")
        {
            string[] arg = new string[2];
            char[] splitter = { ';' };
            arg = e.CommandArgument.ToString().Split(splitter);
            truckType = Convert.ToInt32(arg[0]);
            truckNo = arg[1];
        }
        if (truckType == 1)
        {
            ESalesUnityContainer.Container.Resolve<ITruckService>().ActivateInactiveTruck(truckType, truckNo);
            ucMessageBoxForGrid.ShowMessage(Resources.Messages.TruckActivated);
        }
        else if (truckType == 2)
        {
            ESalesUnityContainer.Container.Resolve<ITruckService>().ActivateInactiveTruck(truckType, truckNo);
            ucMessageBoxForGrid.ShowMessage(Resources.Messages.TruckActivated);
        }
    }

    protected void grdManageInactiveTrucks_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
        string truckNo = txtTruckNumber.Text;
        if (Page.IsValid)
        {
            TruckVerificationDTO truckDetails = new TruckVerificationDTO();
            truckDetails = ESalesUnityContainer.Container.Resolve<ITruckService>().GetAllTruckDetails(truckNo);

            if (truckDetails.type > 0)
            {
                IList<TruckVerificationDTO> lstTruckDetails = new List<TruckVerificationDTO>();
                lstTruckDetails.Add(truckDetails);
                grdManageInactiveTrucks.DataSource = lstTruckDetails;
                grdManageInactiveTrucks.DataBind();
            }
            else
            {
                FillBlankGrid();
            }
        }
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        GetSuspendedTrucks();
    }

    private void GetSuspendedTrucks()
    {
        IList<TruckVerificationDTO> lstSuspendedTrucks = new List<TruckVerificationDTO>();
        lstSuspendedTrucks = ESalesUnityContainer.Container.Resolve<ITruckService>().GetAllSuspendedTrucks();

        if (lstSuspendedTrucks.Count > 0)
        {
            grdManageInactiveTrucks.DataSource = lstSuspendedTrucks;
            grdManageInactiveTrucks.DataBind();
        }
        else
        {
            FillBlankGrid();
        }
    }
}