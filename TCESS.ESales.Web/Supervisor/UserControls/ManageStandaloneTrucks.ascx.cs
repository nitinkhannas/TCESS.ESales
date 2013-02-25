#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Bookings_UserControls_ManageStandaloneTrucks : BaseUserControl
{
    public event ShowDataByIdEventHandler Event_ShowStandaloneTruck;

    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //FillGridWithTruckDetails();
            FillBlankGrid();
        }
    }
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            FillGridWithTruckDetails(txtTruckNumber.Text.Trim());
        }
    }

    private void FillGridWithTruckDetails(string TruckNo)
    {
          StandaloneTrucksDTO StandaloneTruck = new StandaloneTrucksDTO();
          List<StandaloneTrucksDTO> lstStandaloneTruck = new List<StandaloneTrucksDTO>();
       StandaloneTruck = ESalesUnityContainer.Container.Resolve<IStandaloneTruckService>()
           .GetStandaloneTruckByRegNo(TruckNo);
       if (StandaloneTruck != null && StandaloneTruck.StandaloneTruck_Id>0)
       {
           if (StandaloneTruck.StandaloneTruck_IsDeleted == true && StandaloneTruck.StandaloneTruck_IsBlacklisted==true)
           {
               ucMessageBoxForGrid.ShowMessage("Truck is already deleted");
           }
           else
           {
               lstStandaloneTruck.Add(StandaloneTruck);
               if (lstStandaloneTruck.Count > 0)
               {
                   grdManageStandaloneTrucks.DataSource = lstStandaloneTruck;
                   grdManageStandaloneTrucks.DataBind();
               }
               else
               {
                   FillBlankGrid();
               }
           }
       }
       else
       {
           FillBlankGrid();
       }

      
    }

    /// <summary>
    /// Bind Standalone truck details from database
    /// </summary>
    public void FillGridWithTruckDetails()
    {
        IList<StandaloneTrucksDTO> lstStandaloneTruck = (ESalesUnityContainer.Container.Resolve<IStandaloneTruckService>()
            .GetStandaloneTruckDetails());

        if (lstStandaloneTruck.Count > 0)
        {
            grdManageStandaloneTrucks.DataSource = lstStandaloneTruck;
            grdManageStandaloneTrucks.DataBind();
        }
        else
        {
            FillBlankGrid();
        }
    }

   public void FillBlankGrid()
    {
       
        ShowBlankRowInGrid<StandaloneTrucksDTO>(grdManageStandaloneTrucks);
        txtTruckNumber.Text = "";
    }

    protected IEnumerable grdManageStandaloneTrucks_MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<StandaloneTrucksDTO>();
    }
    
    protected void grdManageStandaloneTrucks_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == Globals.GridCommandEvents.ADDNEW)
        {
            Event_ShowStandaloneTruck(0);
        }
        else if(e.CommandName == Globals.GridCommandEvents.EDITTRUCK)
        {
            Event_ShowStandaloneTruck(Convert.ToInt32(e.CommandArgument));
        }
    }

    protected void grdManageStandaloneTrucks_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int truckId = Convert.ToInt32(grdManageStandaloneTrucks.DataKeys[e.RowIndex].Value);
        
        //Delete standlaone truck details from database
        ESalesUnityContainer.Container.Resolve<IStandaloneTruckService>().DeleteStandaloneTruck(truckId);

        //Bind Standalone truck details from database
        ucMessageBoxForGrid.ShowMessage(Resources.Messages.StandaloneTruckDeletedSuccessfully);
    }

    protected void grdManageStandaloneTrucks_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdManageStandaloneTrucks.PageIndex = e.NewPageIndex;

        //Bind Standalone truck details from database
        FillGridWithTruckDetails();
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        //FillGridWithTruckDetails();
        FillBlankGrid();
    }
}