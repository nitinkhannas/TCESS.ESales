#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.BusinessLayer.Interfaces.Masters;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Masters_ManageBusinessType : BasePage 
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Get business type from database
            GetBusinessType();
        }
    }

    /// <summary>
    /// Get business type from database
    /// </summary>
    private void GetBusinessType()
    {
        IList<BusinessTypeDTO> lstBusinessTypes = MasterList.GetBusinessTypeList();

        if (lstBusinessTypes.Count > 0)
        {
            grdBusinessType.DataSource = lstBusinessTypes;
            grdBusinessType.DataBind();
        }
        else
        {
            ShowBlankRowInGrid<BusinessTypeDTO>(grdBusinessType);
        }
    }

    protected void grdBusinessType_RowCommand(object sender, GridViewCommandEventArgs e)
    {        
        if (e.CommandName == Globals.GridCommandEvents.ADDNEW)
        {
            if (Page.IsValid)
            {
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                BusinessTypeDTO businessTypeDetails = new BusinessTypeDTO();
                businessTypeDetails.BusinessType_Name = ((TextBox)row.FindControl("txtNewBusinessType")).Text;
                businessTypeDetails.BusinessType_CreatedDate = DateTime.Now;
                businessTypeDetails.BusinessType_LastUpdatedDate = DateTime.Now;
                businessTypeDetails.BusinessType_CreatedBy = GetCurrentUserId();

                int businessTypeId = ESalesUnityContainer.Container.Resolve<IMasterService>()
                    .SaveAndUpdateBusinessType(businessTypeDetails);
                ucMessageBoxForGrid.ShowMessage(Messages.BusinessTypeSavedSuccessfully);
            }
        }
    }

    protected void grdBusinessType_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(e.NewEditIndex);
    }

    protected void grdBusinessType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ESalesUnityContainer.Container.Resolve<IMasterService>().DeleteBusinessType(Convert.ToInt32(grdBusinessType.DataKeys[e.RowIndex].Value));
        ucMessageBoxForGrid.ShowMessage(Messages.BusinessTypeDeletedSuccessfully);
    }

    protected void grdBusinessType_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (Page.IsValid)
        {            
            BusinessTypeDTO businessType = new BusinessTypeDTO();
            businessType = ESalesUnityContainer.Container.Resolve<IMasterService>()
                .GetBusinessTypeListByTypeId(Convert.ToInt32(grdBusinessType.DataKeys[e.RowIndex].Value));

            businessType.BusinessType_Name = ((TextBox)grdBusinessType.Rows[e.RowIndex].FindControl("txtBusinessType")).Text;
            businessType.BusinessType_LastUpdatedDate = DateTime.Now;
            businessType.BusinessType_CreatedBy = GetCurrentUserId();

            //To update business type
            ESalesUnityContainer.Container.Resolve<IMasterService>().SaveAndUpdateBusinessType(businessType);
            ucMessageBoxForGrid.ShowMessage(Messages.BusinessTypeUpdatedSuccessfully);
        }        
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1);
    }

    protected void grdBusinessType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdBusinessType.PageIndex = e.NewPageIndex;

        //Get business type from database
        GetBusinessType();
    }

    protected IEnumerable grdBusinessType__MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<BusinessTypeDTO>();
    }

    protected void grdBusinessType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1);
    }

    /// <summary>
    /// Row edit/update/cancel function for grid view
    /// </summary>
    /// <param name="rowIndex"></param>
    private void GridViewRowUpdateFunctions(int rowIndex)
    {
        grdBusinessType.EditIndex = rowIndex;
        
        //Get business type from database
        GetBusinessType();
    }

    protected void EditBusinessType_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        CustomValidator customval = (CustomValidator)sender;
        GridViewRow row =(GridViewRow)customval.NamingContainer;
        string businessTypeName = ((TextBox)row.FindControl("txtBusinessType")).Text.Trim();
        int businessTypeId = Convert.ToInt32(grdBusinessType.DataKeys[row.RowIndex].Value);
        
        if (ESalesUnityContainer.Container.Resolve<IMasterService>().IsBusinessTypeExists(businessTypeId, businessTypeName))
        {
            args.IsValid = false;
        }
    }

    protected void AddBusinessType_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        TextBox txtNewBusinessType = (TextBox)grdBusinessType.FooterRow.FindControl("txtNewBusinessType");
        
        if (ESalesUnityContainer.Container.Resolve<IMasterService>().IsBusinessTypeExists(0, txtNewBusinessType.Text.Trim()))
        {
            args.IsValid = false;
        }
    }
}