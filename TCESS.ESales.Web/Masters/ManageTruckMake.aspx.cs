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

public partial class ManageTruckMake : BasePage 
{
	protected void Page_Init(object sender, EventArgs e)
	{
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
	}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Get all active truck makes from database
			GetTruckMake();
        }
    }

	/// <summary>
	/// Get all active truck makes from database
	/// </summary>
    private void GetTruckMake()
    {
        IList<TruckMakeDTO> LstTruckMakeDTO = ESalesUnityContainer.Container.Resolve<ITruckMakeService>().GetTruckMakelist();
        
        if (LstTruckMakeDTO.Count > 0)
        {
            grdTruckMake.DataSource = LstTruckMakeDTO;
            grdTruckMake.DataBind();
        }
        else
        {
            ShowBlankRowInGrid<TruckMakeDTO>(grdTruckMake);
        }
    }

    protected void grdTruckMake_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals(Globals.GridCommandEvents.ADDNEW))
        {
            if (Page.IsValid)
            {
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                TruckMakeDTO truckMakeDetails = new TruckMakeDTO();

                truckMakeDetails.TruckMake_Name = ((TextBox)row.FindControl("txtNewTruckMakeName")).Text;
                truckMakeDetails.TruckMake_TruckWheeler_Id = Convert.ToInt32(((DropDownList)row.FindControl("ddlNewTruckWheeler")).SelectedValue);
                truckMakeDetails.TruckMake_TruckCC_Id = Convert.ToInt32(((DropDownList)row.FindControl("ddlNewTruckCarryCapacity")).SelectedValue);
                truckMakeDetails.TruckMake_CreatedDate = DateTime.Now;
                truckMakeDetails.TruckMake_CreatedBy = GetCurrentUserId();
                truckMakeDetails.TruckMake_LastUpdatedDate = DateTime.Now;

                int truckId = ESalesUnityContainer.Container.Resolve<ITruckMakeService>().SaveAndUpdateTruckMake(truckMakeDetails);
				ucMessageBoxForGrid.ShowMessage(Messages.TruckMakeSavedSuccessfully);
            }
        }
    }

    protected void grdTruckMake_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int truckMakeId = Convert.ToInt32(grdTruckMake.DataKeys[e.RowIndex].Value);
        
        TruckMakeDTO truckMakeDetails = MasterList.GetTruckMakeById(truckMakeId);
        truckMakeDetails.TruckMake_IsDeleted = true;

        ESalesUnityContainer.Container.Resolve<ITruckMakeService>().SaveAndUpdateTruckMake(truckMakeDetails);
		ucMessageBoxForGrid.ShowMessage(Messages.TruckMakeDeletedSuccessfully);
    }

    protected void grdTruckMake_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (Page.IsValid)
        {
            TruckMakeDTO truckMakeDetails = MasterList.GetTruckMakeById(Convert.ToInt32(grdTruckMake.DataKeys[e.RowIndex].Value));
            truckMakeDetails.TruckMake_Name = ((TextBox)grdTruckMake.Rows[e.RowIndex].FindControl("txtTruckMakeName")).Text;
            truckMakeDetails.TruckMake_TruckWheeler_Id = Convert.ToInt32(((DropDownList)grdTruckMake.Rows[e.RowIndex].FindControl("ddlTruckWheeler")).SelectedValue);
            truckMakeDetails.TruckMake_TruckCC_Id = Convert.ToInt32(((DropDownList)grdTruckMake.Rows[e.RowIndex].FindControl("ddlTruckCarryCapacity")).SelectedValue);
            truckMakeDetails.TruckMake_LastUpdatedDate = DateTime.Now;
            truckMakeDetails.TruckMake_CreatedBy = GetCurrentUserId();
            
            //To update truck make
            int truckmakeId = ESalesUnityContainer.Container.Resolve<ITruckMakeService>().SaveAndUpdateTruckMake(truckMakeDetails);
			ucMessageBoxForGrid.ShowMessage(Messages.TruckMakeUpdatedSuccessfully);
        }
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
		grdTruckMake.EditIndex = rowEditIndex;

		//Get all active truck makes from database
		GetTruckMake();
	}

	protected void grdTruckMake_RowEditing(object sender, GridViewEditEventArgs e)
	{
		//Row edit/update/cancel function for grid view
		GridViewRowUpdateFunctions(e.NewEditIndex);
	}

	protected void grdTruckMake_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
	{
		//Row edit/update/cancel function for grid view
		GridViewRowUpdateFunctions(-1);
	}

	protected void grdTruckMake_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		grdTruckMake.PageIndex = e.NewPageIndex;
		
		//Get all active truck makes from database
		GetTruckMake();
	}

    protected IEnumerable grdTruckMake_MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<TruckMakeDTO>();
    }

    protected void EditTruckMake_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        CustomValidator customval = (CustomValidator)sender;
        GridViewRow r = (GridViewRow)customval.NamingContainer;
        
        string truckMake = ((TextBox)r.FindControl("txtTruckMakeName")).Text.Trim();
        int truckMakeId = Convert.ToInt32(grdTruckMake.DataKeys[r.RowIndex].Value);

        if (ESalesUnityContainer.Container.Resolve<ITruckMakeService>().TruckMakeExists(truckMakeId, truckMake))
        {
            args.IsValid = false;
        }
    }

    protected void AddTruckMake_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        TextBox txtNewTruckMakeName = (TextBox)grdTruckMake.FooterRow.FindControl("txtNewTruckMakeName");

        if (ESalesUnityContainer.Container.Resolve<ITruckMakeService>().TruckMakeExists(0, txtNewTruckMakeName.Text.Trim()))
        {
            args.IsValid = false;
        }
    }

    protected void grdTruckMake_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grdTruckMake.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlTruckWheeler = (DropDownList)e.Row.FindControl("ddlTruckWheeler");
            PopulateTruckWheeler(ddlTruckWheeler);
            ddlTruckWheeler.SelectedValue = grdTruckMake.DataKeys[e.Row.RowIndex].Values[1].ToString();

            DropDownList ddlTruckCarryCapacity = (DropDownList)e.Row.FindControl("ddlTruckCarryCapacity");
            PopulateCarryCapacity(ddlTruckCarryCapacity);
            ddlTruckCarryCapacity.SelectedValue = grdTruckMake.DataKeys[e.Row.RowIndex].Values[2].ToString();            
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            DropDownList ddlNewTruckWheeler = (DropDownList)e.Row.FindControl("ddlNewTruckWheeler");
            PopulateTruckWheeler(ddlNewTruckWheeler);

            DropDownList ddlNewTruckCarryCapacity = (DropDownList)e.Row.FindControl("ddlNewTruckCarryCapacity");
            PopulateCarryCapacity(ddlNewTruckCarryCapacity);
        }
    }

    private void PopulateTruckWheeler(DropDownList ddlWheels)
    {
        MasterList.GetTruckWheels(ddlWheels);
    }

    private void PopulateCarryCapacity(DropDownList ddlCarryCapacity)
    {
        MasterList.GetTruckCarryCapacity(ddlCarryCapacity);
    }
}