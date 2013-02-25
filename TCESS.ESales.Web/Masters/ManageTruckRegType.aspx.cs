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

public partial class Masters_ManageTruckRegType : BasePage
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
            GetTruckregType();
        }
    }

    /// <summary>
    /// Get business type from database
    /// </summary>
    private void GetTruckregType()
    {
        IList<TruckRegTypeDTO> lsttruckregTypes = MasterList.GetTruckregTypeList();

        if (lsttruckregTypes.Count > 0)
        {
            grdTruckRegType.DataSource = lsttruckregTypes;
            grdTruckRegType.DataBind();
        }
        else
        {
            ShowBlankRowInGrid<BusinessTypeDTO>(grdTruckRegType);
        }
    }

    protected void grdTruckRegType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == Globals.GridCommandEvents.ADDNEW)
        {
            if (Page.IsValid)
            {
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                TruckRegTypeDTO truckregTypeDetails = new TruckRegTypeDTO();
                truckregTypeDetails.TruckRegType_Name = ((TextBox)row.FindControl("txtNewtruckregType")).Text;
                truckregTypeDetails.TruckRegType_CreatedDate = DateTime.Now;
                truckregTypeDetails.TruckRegType_LastUpdatedDate = DateTime.Now;
                truckregTypeDetails.TruckRegType_CreatedBy= GetCurrentUserId();

                int businessTypeId = ESalesUnityContainer.Container.Resolve<ICustomerMastersService>()
                    .SaveAndUpdateTruckregType(truckregTypeDetails);
                ucMessageBoxForGrid.ShowMessage(Messages.TruckRegistrationSaved);
            }
        }
    }

    protected void grdTruckRegType_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(e.NewEditIndex);
    }

    protected void grdTruckRegType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ESalesUnityContainer.Container.Resolve<ICustomerMastersService>()
            .DeleteTruckregType(Convert.ToInt32(grdTruckRegType.DataKeys[e.RowIndex].Value));
        ucMessageBoxForGrid.ShowMessage(Messages.TruckTypeDeleted);
    }

    protected void grdTruckRegType_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (Page.IsValid)
        {
            TruckRegTypeDTO truckregType = new TruckRegTypeDTO();
            truckregType = ESalesUnityContainer.Container.Resolve<ICustomerMastersService>()
                .GettruckregTypeListByTypeId(Convert.ToInt32(grdTruckRegType.DataKeys[e.RowIndex].Value));

            truckregType.TruckRegType_Name = ((TextBox)grdTruckRegType.Rows[e.RowIndex].FindControl("txttruckregType")).Text;
            truckregType.TruckRegType_LastUpdatedDate = DateTime.Now;
            truckregType.TruckRegType_CreatedBy = GetCurrentUserId();

            //To update business type
            ESalesUnityContainer.Container.Resolve<ICustomerMastersService>().SaveAndUpdateTruckregType(truckregType);
            ucMessageBoxForGrid.ShowMessage(Messages.TruckTypeUpdated);
        }
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1);
    }

    protected void grdTruckRegType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTruckRegType.PageIndex = e.NewPageIndex;

        //Get business type from database
        GetTruckregType();
    }

    protected IEnumerable grdTruckRegType__MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<TruckRegTypeDTO>();
    }

    protected void grdTruckRegType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
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
        grdTruckRegType.EditIndex = rowIndex;

        //Get business type from database
        GetTruckregType();
    }

    protected void EditBusinessType_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        CustomValidator customval = (CustomValidator)sender;
        GridViewRow row = (GridViewRow)customval.NamingContainer;
        string businessTypeName = ((TextBox)row.FindControl("txttruckregType")).Text.Trim();
        int businessTypeId = Convert.ToInt32(grdTruckRegType.DataKeys[row.RowIndex].Value);

        if (ESalesUnityContainer.Container.Resolve<ICustomerMastersService>()
            .IstruckregTypeExists(businessTypeId, businessTypeName))
        {
            
            args.IsValid = false;
        }
    }

    protected void AddBusinessType_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        TextBox txtNewBusinessType = (TextBox)grdTruckRegType.FooterRow.FindControl("txtNewtruckregType");

        if (ESalesUnityContainer.Container.Resolve<ICustomerMastersService>()
            .IstruckregTypeExists(0, txtNewBusinessType.Text.Trim()))
        {
            args.IsValid = false;
        }
    }
}