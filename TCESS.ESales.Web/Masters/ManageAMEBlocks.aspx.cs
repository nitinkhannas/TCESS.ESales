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

public partial class Masters_ManageAMEBlocks : BasePage 
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Get AME block details
            GetAMEBlockDetails();
        }
    }

    /// <summary>
    /// Get AME block details
    /// </summary>
    private void GetAMEBlockDetails()
    {
        IList<AmeBlockDTO> listState = MasterList.GetAmeBlockList();

        if (listState.Count > 0)
        {
            grdBlock.DataSource = listState;
            grdBlock.DataBind();
        }
        else
        {
            ShowBlankRowInGrid<AmeBlockDTO>(grdBlock);
        }
    }    

    protected void grdBlock_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdBlock.PageIndex = e.NewPageIndex;

        //Get AME block details
        GetAMEBlockDetails();
    }

    protected void grdBlock_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals(Globals.GridCommandEvents.ADDNEW))
        {
            if (Page.IsValid)
            {
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                AmeBlockDTO ameBlockDetails = new AmeBlockDTO();
                ameBlockDetails.Blocks_Name = ((TextBox)row.FindControl("txtNewBlock")).Text;
                ameBlockDetails.Blocks_CreatedDate = DateTime.Now;
                ameBlockDetails.Blocks_CreatedBy = GetCurrentUserId();

                //Saves Ame block details in database
                int blockId = ESalesUnityContainer.Container.Resolve<IAmeBlockService>().SaveAndUpdateAmeBlock(ameBlockDetails);
                ucMessageBoxForGrid.ShowMessage(Messages.AMEBlockSavedSuccessfully);
            }
        }
    }

    protected void grdBlock_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (Page.IsValid)
        {            
            AmeBlockDTO ameBlockDetails = new AmeBlockDTO();
            
            //Gets Ame block details by block id for update
            ameBlockDetails = ESalesUnityContainer.Container.Resolve<IAmeBlockService>()
                .GetAmeBlockListByBlockId(Convert.ToInt32(grdBlock.DataKeys[e.RowIndex].Value));
            
            //Initializes DTO properties with control values
            ameBlockDetails.Blocks_Name = ((TextBox)grdBlock.Rows[e.RowIndex].FindControl("txtBlock")).Text;
            ameBlockDetails.Blocks_LastUpdatedDate = DateTime.Now;
            ameBlockDetails.Blocks_CreatedBy = GetCurrentUserId();

            //Update Ame block details in database
            int ameBlockId = ESalesUnityContainer.Container.Resolve<IAmeBlockService>().SaveAndUpdateAmeBlock(ameBlockDetails);
            ucMessageBoxForGrid.ShowMessage(Messages.AMEBlockUpdatedSuccessfully);
        }
    }

    protected void grdBlock_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ameBlockId = Convert.ToInt32(grdBlock.DataKeys[e.RowIndex].Value);
        AmeBlockDTO ameBlockDetails = ESalesUnityContainer.Container.Resolve<IAmeBlockService>().GetAmeBlockListByBlockId(ameBlockId);

        ameBlockDetails.Blocks_IsDeleted = true;

        ESalesUnityContainer.Container.Resolve<IAmeBlockService>().SaveAndUpdateAmeBlock(ameBlockDetails);
        ucMessageBoxForGrid.ShowMessage(Messages.AMEBlockDeletedSuccessfully);
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1);
    }

    protected void grdBlock_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(e.NewEditIndex);
    }

    protected void grdBlock_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1);
    }

    private void GridViewRowUpdateFunctions(int commandIndex)
    {
        grdBlock.EditIndex = commandIndex;
        
        //Get AME block details
        GetAMEBlockDetails();
    }

    protected IEnumerable grdBlocke__MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<AmeBlockDTO>();
    }

    protected void EditAMEBlock_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        CustomValidator customval = (CustomValidator)sender;
        GridViewRow r =(GridViewRow)customval.NamingContainer;
        string blockName = ((TextBox)r.FindControl("txtBlock")).Text.Trim();
        
        int ameBlockId =Convert.ToInt32(grdBlock.DataKeys[r.RowIndex].Value);

        if (ESalesUnityContainer.Container.Resolve<IAmeBlockService>().AmeBlockExists(ameBlockId, blockName))
        {
            args.IsValid = false;
        }
    }

    protected void AddAMEBlock_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        TextBox txtNewBlock = (TextBox)grdBlock.FooterRow.FindControl("txtNewBlock");
        if (ESalesUnityContainer.Container.Resolve<IAmeBlockService>().AmeBlockExists(0, txtNewBlock.Text.Trim()))
        {
            args.IsValid = false;
        }
    }    
}