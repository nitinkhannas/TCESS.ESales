#region Namespace

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

public partial class Supervisor_ManageCautionListForAuth : BasePage 
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
            PopulateAuthRepCuationList();
        }
    }
     
    private void PopulateAuthRepCuationList()
    {
        IList<AuthRepDTO> listAuthRepDTO = ESalesUnityContainer.Container.Resolve<ICautionListService>()
            .GetCautionListForAuthReps(true);
        
        if (listAuthRepDTO.Count > 0)
        {
            grdAuthRepCuationLst.DataSource = listAuthRepDTO;
            grdAuthRepCuationLst.DataBind();
        }
        else
        {
            base.ShowBlankRowInGrid<AuthRepDTO>(grdAuthRepCuationLst);
        }
    }

    protected void grdAuthRepCuationLst_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //Bind footer truck dropdown list
            DropDownList ddlAuthRep = (DropDownList)e.Row.FindControl("ddlAuthRep");
            ddlAuthRep.DataSource = ESalesUnityContainer.Container.Resolve<ICautionListService>().GetCautionListForAuthReps(false);
            ddlAuthRep.DataBind();
            ddlAuthRep.Items.Insert(0, new ListItem(Messages.SelectAuthRepresentative, "0"));
        }
    }

    protected void grdAuthRepCuationLst_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals(Globals.GridCommandEvents.ADDNEW))
        {            
            GridViewRow gvRow = (GridViewRow)((Button)e.CommandSource).NamingContainer;
            AuthRepDTO authRepDetails = new AuthRepDTO();

            authRepDetails = ESalesUnityContainer.Container.Resolve<IAuthRepService>()
                .GetAuthRepById(Convert.ToInt32(((DropDownList)gvRow.FindControl("ddlAuthRep")).SelectedValue));
            authRepDetails.AuthRep_IsBlacklisted = true;
            authRepDetails.AuthRep_IsDeleted = true;
            authRepDetails.AuthRep_BlacklistedBy = ((DropDownList)gvRow.FindControl("ddlBlackListedBy")).SelectedValue;
            
            int authRepId = ESalesUnityContainer.Container.Resolve<ICautionListService>()
                .UpdateCautionListForAuthRep(authRepDetails);
            
            PopulateAuthRepCuationList();
            if (authRepId > 0)
            {
                ucMessageBoxForGrid.ShowMessage(Resources.Messages.AuthRepCautionListCreatedSuccessfully);
            }
        }
    }
  
    protected void grdAuthRepCuationLst_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int authRepId = Convert.ToInt32(grdAuthRepCuationLst.DataKeys[e.RowIndex].Value);
        
        AuthRepDTO authRepDetails = ESalesUnityContainer.Container.Resolve<IAuthRepService>().GetAuthRepById(authRepId);
        authRepDetails.AuthRep_IsBlacklisted = false;
        authRepDetails.AuthRep_BlacklistedBy = null;
        
        ESalesUnityContainer.Container.Resolve<ICautionListService>().UpdateCautionListForAuthRep(authRepDetails);
        ucMessageBoxForGrid.ShowMessage(Resources.Messages.AuthRepCautionListDeletedSuccessfully);
    }
 
    protected void grdAuthRepCuationLst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAuthRepCuationLst.PageIndex = e.NewPageIndex;
        PopulateAuthRepCuationList();
    }

    protected IEnumerable grdAuthRepCuationLst_MustAddARow(IEnumerable data)
    {
        //return the value
        return base.AddBlankRowInGrid<CustomerDTO>();
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        PopulateAuthRepCuationList();
    }
}