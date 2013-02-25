#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Masters_ManageStates : BasePage 
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
        }
        else
        {
            ShowBlankRowInGrid<StateDTO>(grdState);
        }
    }

    protected void grdState_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals(Globals.GridCommandEvents.ADDNEW))
        {
            if (Page.IsValid)
            {
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                StateDTO stateDetails = new StateDTO();

                stateDetails.State_Name = ((TextBox)row.FindControl("txtNewState")).Text;
                stateDetails.State_CreatedDate = DateTime.Now;
                stateDetails.State_CreatedBy = GetCurrentUserId();
                
				int stateId = ESalesUnityContainer.Container.Resolve<ILocationService>().SaveState(stateDetails);
				ucMessageBoxForGrid.ShowMessage(Messages.StateSavedSuccessfully);
            }
        }
    }

    protected void grdState_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (Page.IsValid)
        {
            //To update state
            StateDTO stateDetails = new StateDTO();
            stateDetails = ESalesUnityContainer.Container.Resolve<ILocationService>()
                .GetStateByStateId(Convert.ToInt32(grdState.DataKeys[e.RowIndex].Value));
            stateDetails.State_Name = ((TextBox)grdState.Rows[e.RowIndex].FindControl("txtState")).Text;
            stateDetails.State_LastUpdatedDate = DateTime.Now;
            stateDetails.State_CreatedBy = GetCurrentUserId();
            
            int stateId = ESalesUnityContainer.Container.Resolve<ILocationService>().UpdateState(stateDetails);
			ucMessageBoxForGrid.ShowMessage(Messages.StateUpdatedSuccessfully);
        }
    }

	protected void grdState_RowDeleting(object sender, GridViewDeleteEventArgs e)
	{
		int stateId = Convert.ToInt32(grdState.DataKeys[e.RowIndex].Value);
		StateDTO stateDetails = ESalesUnityContainer.Container.Resolve<ILocationService>().GetStateByStateId(stateId);
		stateDetails.State_IsDeleted = true;
		
		ESalesUnityContainer.Container.Resolve<ILocationService>().UpdateState(stateDetails);
		ucMessageBoxForGrid.ShowMessage(Messages.StateDeletedSuccessfully);
	}

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
	{
		//Row edit/update/cancel function for grid view
		GridViewRowUpdateFunctions(-1);
	}

	/// <summary>
	/// Row edit/update/cancel function for grid view
	/// </summary>
	/// <param name="rowEditIndex"></param>
	private void GridViewRowUpdateFunctions(int rowEditIndex)
	{
		grdState.EditIndex = rowEditIndex;

		//Get all active states from database
		GetState();
	}

    protected void grdState_RowEditing(object sender, GridViewEditEventArgs e)
    {
		//Row edit/update/cancel function for grid view
		GridViewRowUpdateFunctions(e.NewEditIndex);
    }

    protected void grdState_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
		//Row edit/update/cancel function for grid view
		GridViewRowUpdateFunctions(-1);
    }

    protected IEnumerable grdState_MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<StateDTO>();
    }

    protected void grdState_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdState.PageIndex = e.NewPageIndex;
		
		//Get all active states from database
		GetState();
    }

    protected void EditState_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        CustomValidator customval = (CustomValidator)sender;
        GridViewRow r = (GridViewRow)customval.NamingContainer;
        
        string state = ((TextBox)r.FindControl("txtState")).Text.Trim();
        int stateId = Convert.ToInt32(grdState.DataKeys[r.RowIndex].Value);
        
        if (ESalesUnityContainer.Container.Resolve<ILocationService>().StateExists(stateId, state))
        {
            args.IsValid = false;
        }
    }

    protected void AddState_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        TextBox txtNewState = (TextBox)grdState.FooterRow.FindControl("txtNewState");
        
        if (ESalesUnityContainer.Container.Resolve<ILocationService>().StateExists(0, txtNewState.Text.Trim()))
        {
            args.IsValid = false;
        }
    }
}