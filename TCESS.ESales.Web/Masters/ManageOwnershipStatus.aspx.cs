#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces.Masters;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Masters_ManageOwnershipStatus : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Get all active ownership status from database
            GetOwnershipStatus();
        }
    }

    /// <summary>
    /// Get all active ownership status from database
    /// </summary>
    private void GetOwnershipStatus()
    {
        IList<OwnershipStatusDTO> lstOwnershipStatus = MasterList.GetOwnershipStatusList();
        if (lstOwnershipStatus.Count > 0)
        {
            grdOwnershipStatus.DataSource = lstOwnershipStatus;
            grdOwnershipStatus.DataBind();

        }
        else
        {
            ShowBlankRowInGrid<StateDTO>(grdOwnershipStatus);
        }
    }

    protected void grdOwnershipStatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == Globals.GridCommandEvents.ADDNEW)
        {
            if (Page.IsValid)
            {
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                OwnershipStatusDTO ownershipDetail = new OwnershipStatusDTO();
                ownershipDetail.OwnershipStatus_Name = ((TextBox)row.FindControl("txtNewOwnershipStatus")).Text;
                ownershipDetail.OwnershipStatus_CreatedDate = DateTime.Now;
                ownershipDetail.OwnershipStatus_LastUpdatedDate = DateTime.Now;
                ownershipDetail.OwnershipStatus_CreatedBy = GetCurrentUserId();
                
                int OwnershipStatusId = ESalesUnityContainer.Container.Resolve<IMasterService>()
                    .SaveAndUpdateOwnershipStatus(ownershipDetail);

                GetOwnershipStatus();
                ucMessageBoxForGrid.ShowMessage(Resources.Messages.OwnershipStatusSavedSuccessfully);
            }
        }
    }

    protected void grdOwnershipStatus_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ESalesUnityContainer.Container.Resolve<IMasterService>()
            .DeleteOwnershipStatus(Convert.ToInt32(grdOwnershipStatus.DataKeys[e.RowIndex].Value));
        ucMessageBoxForGrid.ShowMessage(Resources.Messages.OwnershipStatusDeletedSuccessfully);
    }

    protected void grdOwnershipStatus_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (Page.IsValid)
        {
            //To update ownershipStatus
            OwnershipStatusDTO ownershipDetail = new OwnershipStatusDTO();
            ownershipDetail = ESalesUnityContainer.Container.Resolve<IMasterService>()
                .GetOwnershipStatusListById(Convert.ToInt32(grdOwnershipStatus.DataKeys[e.RowIndex].Value));
            
            ownershipDetail.OwnershipStatus_Name = ((TextBox)grdOwnershipStatus.Rows[e.RowIndex].FindControl("txtOwnershipStatus")).Text;
            ownershipDetail.OwnershipStatus_LastUpdatedDate = DateTime.Now;
            ownershipDetail.OwnershipStatus_CreatedBy = GetCurrentUserId();
            
            int ownershipStatusId = ESalesUnityContainer.Container.Resolve<IMasterService>()
                .SaveAndUpdateOwnershipStatus(ownershipDetail);
            ucMessageBoxForGrid.ShowMessage(Resources.Messages.OwnershipStatusUpdatedSuccessfully);
        }
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1);
    }

    protected void grdOwnershipStatus_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1);
    }

    protected void grdOwnershipStatus_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(e.NewEditIndex);
    }

    protected void grdOwnershipStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {        
        grdOwnershipStatus.PageIndex = e.NewPageIndex;

        //Get all active ownership status from database
        GetOwnershipStatus();
    }

    protected IEnumerable grdOwnershipStatus_MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<OwnershipStatusDTO>();
    }

	/// <summary>
	/// Row edit/update/cancel function for grid view
	/// </summary>
	/// <param name="rowEditIndex"></param>
    private void GridViewRowUpdateFunctions(int rowEditIndex)
    {
        grdOwnershipStatus.EditIndex = rowEditIndex;

        //Get all active ownership status from database
        GetOwnershipStatus();
    }

    protected void EditOwnershipStatus_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        CustomValidator customval = (CustomValidator)sender;
        GridViewRow r = (GridViewRow)customval.NamingContainer;
        string ownershipStatus = ((TextBox)r.FindControl("txtOwnershipStatus")).Text.Trim();
        int ownershipStatusId = Convert.ToInt32(grdOwnershipStatus.DataKeys[r.RowIndex].Value);

        if (ESalesUnityContainer.Container.Resolve<IMasterService>()
            .IsOwnershipStatusExists(ownershipStatusId, ownershipStatus))
        {
            args.IsValid = false;
        }
    }

    protected void AddOwnershipStatus_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        TextBox txtNewOwnershipStatus = (TextBox)grdOwnershipStatus.FooterRow.FindControl("txtNewOwnershipStatus");
        
        if (ESalesUnityContainer.Container.Resolve<IMasterService>()
            .IsOwnershipStatusExists(0, txtNewOwnershipStatus.Text.Trim()))
        {
            args.IsValid = false;
        }
    }
}
