#region Namespace

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Administrator_ManagePagesInRoles : BasePage
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
            //Get active roles from database
            GetRoles();

            //Get pages with selected role
            GetPagesWithRoles(string.Empty);
        }
    }

    /// <summary>
    /// Get active roles from database
    /// </summary>
    private void GetRoles()
    {
        string[] lstRoles = Roles.GetAllRoles();
        Array.Sort(lstRoles);

        ddlRoleName.DataSource = lstRoles;
        ddlRoleName.DataBind();
        ddlRoleName.Items.Insert(0, new ListItem(Messages.SelectRole, "0"));
    }

    protected void ddlRoleName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRoleName.SelectedItem != null)
        {
            if (ddlRoleName.SelectedIndex > 0)
            {
                //Get role id by role name
                GetRoleIdByRoleName(ddlRoleName.SelectedItem.Value);

                //Get pages with selected role
                GetPagesWithRoles(ddlRoleName.SelectedItem.Value);
            }
            else
            {
                grdPageInRoles.ShowFooter = false;
                base.ShowBlankRowInGrid<PagesInRoleDTO>(grdPageInRoles);
            }
        }
    }

    /// <summary>
    /// Get role id by role name
    /// </summary>
    /// <param name="roleName">String: role name</param>
    private void GetRoleIdByRoleName(string roleName)
    {
        ViewState[Globals.StateMgmtVariables.ROLEID] = ESalesUnityContainer.Container.Resolve<IMembershipService>()
            .GetRoleIdByRoleName(roleName);
    }

    /// <summary>
    /// Get pages with selected role
    /// </summary>
    /// <param name="roleName">String: role name</param>
    private void GetPagesWithRoles(string roleName)
    {
        if (!string.IsNullOrEmpty(roleName))
        {
            if (ViewState[Globals.StateMgmtVariables.ROLEID] == null)
            {
                //Get role id by role name
                GetRoleIdByRoleName(roleName);
            }

            //Get pages in role from database
            IList<PagesInRoleDTO> lstPagesInRole = ESalesUnityContainer.Container.Resolve<IMembershipService>()
                .GetPagesInRole(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.ROLEID]));

            if (lstPagesInRole.Count > 0)
            {  
                grdPageInRoles.ShowFooter = true;
                grdPageInRoles.DataSource = lstPagesInRole;
                grdPageInRoles.DataBind();              
            }
            else
            {
                grdPageInRoles.ShowFooter = true;
                base.ShowBlankRowInGrid<PagesInRoleDTO>(grdPageInRoles);
            }
        }
        else
        {
            grdPageInRoles.ShowFooter = false;
            base.ShowBlankRowInGrid<PagesInRoleDTO>(grdPageInRoles);
        }
    }

    /// <summary>
    /// Row edit/update/cancel function for grid view
    /// </summary>
    /// <param name="rowIndex">rowIndex of gridview</param>
    private void GridViewRowUpdateFunctions(int editIndex)
    {
        grdPageInRoles.EditIndex = editIndex;

        //Get pages with selected role
        GetPagesWithRoles(ddlRoleName.SelectedItem.Text);
    }

    protected void grdPageInRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPageInRoles.PageIndex = e.NewPageIndex;

        //Get pages with selected role
        GetPagesWithRoles(ddlRoleName.SelectedItem.Text);
    }

    protected IEnumerable grdPageInRoles_MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<PagesInRoleDTO>();
    }

    protected void grdPageInRoles_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(e.NewEditIndex);
    }

    protected void grdPageInRoles_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int pageId = Convert.ToInt32(grdPageInRoles.DataKeys[e.RowIndex]["Page_Role_PageId"]);
        CheckBox chkActive = (CheckBox)grdPageInRoles.Rows[e.RowIndex].FindControl("chkActive");

        ESalesUnityContainer.Container.Resolve<IMembershipService>().UpdatePageStatus(pageId,
            base.GetCurrentUserId(), Convert.ToInt32(ViewState[Globals.StateMgmtVariables.ROLEID]), chkActive.Checked);
        ucMessageBoxForGrid.ShowMessage(Messages.PageInRoleUpdatedSuccessfully);
    }

    protected void grdPageInRoles_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int pageRoleId = Convert.ToInt32(grdPageInRoles.DataKeys[e.RowIndex]["Page_Role_Id"]);
        ESalesUnityContainer.Container.Resolve<IMembershipService>().DeletePagesFromRole(pageRoleId, base.GetCurrentUserId());
        ucMessageBoxForGrid.ShowMessage(Messages.PageInRoleDeletedSuccessfully);
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1);
    }

    protected void grdPageInRoles_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1);
    }

    protected void grdPageInRoles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //Bind footer truck dropdown list
            DropDownList ddlPageName = (DropDownList)e.Row.FindControl("ddlPageName");
            IList<PageInfoDTO> listPageInfo = ESalesUnityContainer.Container.Resolve<IMembershipService>()
                .GetAllPages(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.ROLEID]));
            ddlPageName.DataSource = listPageInfo;
            ddlPageName.DataBind();

            ddlPageName.Items.Insert(0, new ListItem(Messages.SelectPageName, "0"));
        }
    }

    protected void grdPageInRoles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals(Globals.GridCommandEvents.ADDNEW))
        {
            if (ddlRoleName.SelectedItem.Value == "0")
            {
                customValidator.IsValid = false;
                ucMessageBoxForGrid.ShowMessage(Messages.SelectRoleName);
            }

            if (Page.IsValid)
            {
                GridViewRow r = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                int pageId = Convert.ToInt32(((DropDownList)(grdPageInRoles.FooterRow.FindControl("ddlPageName"))).SelectedValue);
                bool isActive = ((CheckBox)r.FindControl("chkNewActive")).Checked;
                int roleId = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.ROLEID]);

                //Initialize DTO and save values in database
                SavePagesInRoles(pageId, roleId, isActive);
                ucMessageBoxForGrid.ShowMessage(Messages.PagesInRoleSavedSuccessfully);
            }
        }
    }

    /// <summary>
    /// Initialize DTO and save values in database
    /// </summary>
    /// <param name="pageId">Int32: page id</param>
    /// <param name="roleId">Int32: role id</param>
    /// <param name="isActive">Boolean : isactive = true for page is active</param>
    private void SavePagesInRoles(int pageId, int roleId, bool isActive)
    {        
        PagesInRoleDTO pagesInRolesDetails = new PagesInRoleDTO();
        pagesInRolesDetails.Page_Role_PageId = pageId;
        pagesInRolesDetails.Page_Role_IsActive = isActive;
        pagesInRolesDetails.Page_Role_RoleId = roleId;
        pagesInRolesDetails.Page_Role_CreatedBy = base.GetCurrentUserId();
        pagesInRolesDetails.Page_Role_CreatedDate = DateTime.Now;

        ESalesUnityContainer.Container.Resolve<IMembershipService>().AddPagesToRoles(pagesInRolesDetails);
    }

    protected void PageName_ServerValidate(object sender, ServerValidateEventArgs args)
    {        
        if (ESalesUnityContainer.Container.Resolve<IMembershipService>()
            .CheckIfPageExistsInRole(Convert.ToInt32(args.Value), Convert.ToInt32(ViewState[Globals.StateMgmtVariables.ROLEID])))
        {
            args.IsValid = false;
        }
    }
}