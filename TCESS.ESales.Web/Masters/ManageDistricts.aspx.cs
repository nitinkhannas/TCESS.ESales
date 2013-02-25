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
using Resources;

#endregion

public partial class Masters_ManageDistricts :BasePage 
{
	protected void Page_Init(object sender, EventArgs e)
	{
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
	}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
			//Get all active states from database
			GetState();
        }
    }

	/// <summary>
	/// Get all active states from database
	/// </summary>
	private void GetState()
    {
        IList<StateDTO> listStateDTO = ESalesUnityContainer.Container.Resolve<ILocationService>().GetStateList();
        if (listStateDTO.Count > 0)
        {
            grdState.DataSource = listStateDTO;
            grdState.DataBind();
            
            //Gets districts by default state
			GetDistrict();
        }
        else
        {
            base.ShowBlankRowInGrid<StateDTO>(grdState);
            base.ShowBlankRowInGrid<DistrictDTO>(grdDistrict);
        }
    }

	/// <summary>
	/// Get list of district by selected state
	/// </summary>
	private void GetDistrict()
    {
        if (ViewState[Globals.StateMgmtVariables.STATEID] == null)
        {
            lblDistrictName.Text = ((LinkButton)grdState.Rows[0].Cells[1].FindControl("lnkState")).Text;

            //AME Blocks Id is saved in Viewstate to populate the associated customer by default for first AME
            ViewState[Globals.StateMgmtVariables.STATEID] = grdState.DataKeys[0].Value;
        }

        IList<DistrictDTO> lstDistrict = ESalesUnityContainer.Container.Resolve<ILocationService>()
            .GetDistrictListByStateId(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.STATEID]));

        // Displays Customer with selected AME Block.
		if (lstDistrict.Count > 0)
        {
			grdDistrict.DataSource = lstDistrict;
            grdDistrict.DataBind();
        }
        else
        {
            base.ShowBlankRowInGrid<DistrictDTO>(grdDistrict);
        }
    }

    protected void grdState_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == Globals.GridCommandEvents.SHOWDISTRICT)
        {
            ViewState[Globals.StateMgmtVariables.STATEID] = e.CommandArgument;

            //Sets the selected AME Block name as header on Customer grid
            lblDistrictName.Text = ((LinkButton)e.CommandSource).Text;

			//Get list of district by selected state
			GetDistrict();
        }
    }

    protected void grdState_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdState.PageIndex = e.NewPageIndex;
		
		//Get all active states from database
		GetState();
    }

    protected IEnumerable grdState_MustAddARow(IEnumerable data)
    {
        //return the value
        return base.AddBlankRowInGrid<StateDTO>();
    }

    protected IEnumerable grdDistrict_MustAddARow(IEnumerable data)
    {
        //return the value
        return base.AddBlankRowInGrid<DistrictDTO>();
    }

    protected void grdDistrict_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == Globals.GridCommandEvents.ADDNEW)
        {
            if (Page.IsValid)
            {
                DistrictDTO districtDetails = new DistrictDTO();

                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                districtDetails.Dist_Name = ((TextBox)row.FindControl("txtNewDistrict")).Text;
                districtDetails.Dist_StateId = Convert.ToInt32(ViewState["StateId"]);
                districtDetails.Dist_CreatedDate = DateTime.Now;
                districtDetails.Dist_LastUpdatedDate = DateTime.Now;
                districtDetails.Dist_Createdby = GetCurrentUserId();
                
                int districtId = ESalesUnityContainer.Container.Resolve<ILocationService>().SaveAndUpdateDistrict(districtDetails);
				ucMessageBoxForGrid.ShowMessage(Messages.DistrictSavedSuccessfully);
            }
        }
    }

    protected void grdDistrict_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DistrictDTO objDistrictDTO = ESalesUnityContainer.Container.Resolve<ILocationService>()
            .GetDistrictByDistId(Convert.ToInt32(grdDistrict.DataKeys[e.RowIndex].Value));
        objDistrictDTO.Dist_IsDeleted = true;
        
        ESalesUnityContainer.Container.Resolve<ILocationService>().SaveAndUpdateDistrict(objDistrictDTO);
		ucMessageBoxForGrid.ShowMessage(Messages.DistrictDeletedSuccessfully);
    }

	protected void grdDistrict_RowUpdating(object sender, GridViewUpdateEventArgs e)
	{
		if (Page.IsValid)
		{
			DistrictDTO districtDetails = new DistrictDTO();
			districtDetails = ESalesUnityContainer.Container.Resolve<ILocationService>()
			   .GetDistrictByDistId(Convert.ToInt32(grdDistrict.DataKeys[e.RowIndex].Value));

			districtDetails.Dist_Name = ((TextBox)grdDistrict.Rows[e.RowIndex].FindControl("txtDistrict")).Text;
			districtDetails.Dist_LastUpdatedDate = DateTime.Now;
			districtDetails.Dist_Createdby = GetCurrentUserId();

			int stateId = ESalesUnityContainer.Container.Resolve<ILocationService>().SaveAndUpdateDistrict(districtDetails);
			ucMessageBoxForGrid.ShowMessage(Messages.DistrictUpdatedSuccessfully);
		}
	}

	/// <summary>
	/// Row edit/update/cancel function for grid view
	/// </summary>
	/// <param name="rowEditIndex"></param>
	private void GridViewRowUpdateFunctions(int rowEditIndex)
	{
		grdDistrict.EditIndex = rowEditIndex;

		//Get list of district by selected state
		GetDistrict();
	}

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
	{
		//Row edit/update/cancel function for grid view
		GridViewRowUpdateFunctions(-1);
	}

    protected void grdDistrict_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDistrict.PageIndex = e.NewPageIndex;
		
		//Get list of district by selected state
		GetDistrict();
    }

    protected void grdDistrict_RowEditing(object sender, GridViewEditEventArgs e)
    {
		//Row edit/update/cancel function for grid view
		GridViewRowUpdateFunctions(e.NewEditIndex);
    }    

    protected void grdDistrict_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
		//Row edit/update/cancel function for grid view
		GridViewRowUpdateFunctions(-1);
    }

    protected void AddDistrict_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        TextBox txtNewDistrict =(TextBox) grdDistrict.FooterRow.FindControl("txtNewDistrict");

        if (ESalesUnityContainer.Container.Resolve<ILocationService>()
            .DistrictExists(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.STATEID]), 0, txtNewDistrict.Text.Trim()))
        {
            args.IsValid = false;
        }
    }

    protected void EditDistrict_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        CustomValidator customval = (CustomValidator)sender;
        GridViewRow r = (GridViewRow)customval.NamingContainer;
        
        string distName = ((TextBox)r.FindControl("txtDistrict")).Text.Trim();
        int distId = Convert.ToInt32(grdDistrict.DataKeys[r.RowIndex].Value);
        
        if (ESalesUnityContainer.Container.Resolve<ILocationService>()
            .DistrictExists(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.STATEID]), distId, distName))
        {
            args.IsValid = false;
        }
    }
}