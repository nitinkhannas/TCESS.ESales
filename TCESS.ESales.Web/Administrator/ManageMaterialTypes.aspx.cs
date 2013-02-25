#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using Resources;
using TCESS.ESales.CommonLayer.CommonLibrary;

#endregion

public partial class Administrator_ManageMaterialTypes : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckIsUserAuthenticated();

        if (!IsPostBack)
        { 
            //Populate all material types
            PopulateMaterialType();

            //Populate material type history for last two transactions
            PopulateMaterialTypeHistory();
        }
    }

    /// <summary>
    /// To populte material types
    /// </summary>
    private void PopulateMaterialType()
    {
        IList<MaterialTypeDTO> lstMaterialType = ESalesUnityContainer.Container.Resolve<IMaterialTypeService>()
            .GetMaterialTypeList(false);

        if (lstMaterialType.Count > 0)
        {
            grdMaterialType.DataSource = lstMaterialType;
            grdMaterialType.DataBind();
        }
        else
        {
            base.ShowBlankRowInGrid<MaterialTypeDTO>(grdMaterialType);
        }		
    }

    /// <summary>
    /// Populate material type history for last two transactions
    /// </summary>
	private void PopulateMaterialTypeHistory()
	{
		IList<MaterialTypeDTO> lstMaterialTypeHistory = ESalesUnityContainer.Container.Resolve<IMaterialTypeService>()
            .GetMaterialTypeHistoryList();
        
        //If material type history exists
        if (lstMaterialTypeHistory.Count > 0)
		{
            gridHistory.DataSource = lstMaterialTypeHistory;
			gridHistory.DataBind();
		}
	}

    protected void grdMaterialType_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewRowUpdateFunctions(e.NewEditIndex);
    }
    
    protected void grdMaterialType_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //To update material type
        MaterialTypeDTO materialTypeDetails = new MaterialTypeDTO();
        materialTypeDetails = ESalesUnityContainer.Container.Resolve<IMaterialTypeService>()
            .GetMaterialTypeById(Convert.ToInt32(grdMaterialType.DataKeys[e.RowIndex].Value));
        
        materialTypeDetails.MaterialType_Name = ((TextBox)grdMaterialType.Rows[e.RowIndex].FindControl("txtProductName")).Text;
        materialTypeDetails.MaterialType_Code = ((TextBox)grdMaterialType.Rows[e.RowIndex].FindControl("txtProductCode")).Text;
        materialTypeDetails.MaterialType_CSTRate = Convert.ToDecimal(((TextBox)grdMaterialType.Rows[e.RowIndex].FindControl("txtCSTRate")).Text);
        materialTypeDetails.MaterialType_EducationCess = Convert.ToDecimal(((TextBox)grdMaterialType.Rows[e.RowIndex].FindControl("txtEducationCess")).Text);
        materialTypeDetails.MaterialType_HigherEducationCess = Convert.ToDecimal(((TextBox)grdMaterialType.Rows[e.RowIndex].FindControl("txtHigherEducationCess")).Text);
        materialTypeDetails.MaterialType_HandlingRate = Convert.ToDecimal(((TextBox)grdMaterialType.Rows[e.RowIndex].FindControl("txtHandlingRate")).Text);
        materialTypeDetails.MaterialType_CFormRate = Convert.ToDecimal(((TextBox)grdMaterialType.Rows[e.RowIndex].FindControl("txtCFormRate")).Text);
        materialTypeDetails.MaterialType_TiscoRate = Convert.ToDecimal(((TextBox)grdMaterialType.Rows[e.RowIndex].FindControl("txtTiscoRate")).Text);
        materialTypeDetails.MaterialType_ServiceTax = Convert.ToDecimal(((TextBox)grdMaterialType.Rows[e.RowIndex].FindControl("txtServiceTax")).Text);
        materialTypeDetails.MaterialType_IsActive = ((CheckBox)grdMaterialType.Rows[e.RowIndex].FindControl("chkActive")).Checked;
        materialTypeDetails.MaterialType_LastUpdatedDate = DateTime.Now;
        
        int materialTypeId= ESalesUnityContainer.Container.Resolve<IMaterialTypeService>().UpdateMaterialType(materialTypeDetails);
        ucMessageBoxForGrid.ShowMessage(Messages.MaterialTypeUpdatedSuccessfully);
    }

    protected void grdMaterialType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == Globals.GridCommandEvents.ADDNEW)
        {
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            
            MaterialTypeDTO materialTypeDetails = new MaterialTypeDTO();
            materialTypeDetails.MaterialType_Name = ((TextBox)row.FindControl("txtNewProductName")).Text;
            materialTypeDetails.MaterialType_Code = ((TextBox)row.FindControl("txtNewProductCode")).Text;
            materialTypeDetails.MaterialType_CSTRate = Convert.ToDecimal(((TextBox)row.FindControl("txtNewCSTRate")).Text);
            materialTypeDetails.MaterialType_EducationCess = Convert.ToDecimal(((TextBox)row.FindControl("txtNewEducationCess")).Text);
            materialTypeDetails.MaterialType_HigherEducationCess = Convert.ToDecimal(((TextBox)row.FindControl("txtNewHigherEducationCess")).Text);
            materialTypeDetails.MaterialType_HandlingRate = Convert.ToDecimal(((TextBox)row.FindControl("txtNewHandlingRate")).Text);
            materialTypeDetails.MaterialType_CFormRate = Convert.ToDecimal(((TextBox)row.FindControl("txtNewCFormRate")).Text);
            materialTypeDetails.MaterialType_TiscoRate = Convert.ToDecimal(((TextBox)row.FindControl("txtNewTiscoRate")).Text);
            materialTypeDetails.MaterialType_ServiceTax = Convert.ToDecimal(((TextBox)row.FindControl("txtNewServiceTax")).Text);
            materialTypeDetails.MaterialType_IsActive = ((CheckBox)row.FindControl("chkNewActive")).Checked;
            materialTypeDetails.MaterialType_CreatedBy = base.GetCurrentUserId();
            materialTypeDetails.MaterialType_CreatedDate = DateTime.Now;

            int materialTypeId = ESalesUnityContainer.Container.Resolve<IMaterialTypeService>().SaveMaterialType(materialTypeDetails);
            ucMessageBoxForGrid.ShowMessage(Messages.MaterialTypeSavedSuccessfully);
        }
    }

    protected void grdMaterialType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int materialId = Convert.ToInt32(grdMaterialType.DataKeys[e.RowIndex].Value);

        //Delete material type from database
        ESalesUnityContainer.Container.Resolve<IMaterialTypeService>().DeleteMaterialType(materialId);
        ucMessageBoxForGrid.ShowMessage(Messages.MaterialTypeDeletedSuccessfully);
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1);
    }
    
    protected void grdMaterialType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridViewRowUpdateFunctions(-1);
    }

    /// <summary>
    /// Row edit/update/cancel function for grid view
    /// </summary>
    /// <param name="rowIndex"></param>
    private void GridViewRowUpdateFunctions(int rowIndex)
    {
        grdMaterialType.EditIndex = rowIndex;

        //Get all active material type information from database
        PopulateMaterialType();
    }    
    
    protected IEnumerable grdMaterialType_MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<MaterialTypeDTO>();
    }

    protected void grdMaterialType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdMaterialType.PageIndex = e.NewPageIndex;
        
        //Get all active material type information from database
        PopulateMaterialType();
    }
}