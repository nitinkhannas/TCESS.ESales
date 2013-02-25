#region Using directives

using System;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls;
using Resources;
using TCESS.ESales.CommonLayer.CommonLibrary;

#endregion

public partial class Administrator_ManageRoles : BasePage
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
        }
    }

    /// <summary>
    /// Get active roles from database
    /// </summary>
    private void GetRoles()
    {
        string[] lstRoles = Roles.GetAllRoles();

        if (lstRoles.Length > 0)
        {
            //Sort roles by role name
            Array.Sort(lstRoles);

            grdRoles.DataSource = lstRoles;
            grdRoles.DataBind();
        }
        else
        {
            base.ShowBlankRowInGrid(grdRoles);
        }
    }

    protected void RoleName_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        if (Roles.RoleExists(args.Value.Trim()))
        {
            args.IsValid = false;
        }
    }

    protected void grdRoles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals(Globals.GridCommandEvents.ADDNEW))
        {
            if (Page.IsValid)
            {
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                string roleName = ((TextBox)row.FindControl("txtRoleName")).Text;

                //Create new role in database
                Roles.CreateRole(roleName);
                ucMessageBoxForGrid.ShowMessage(Messages.RoleCreatedSuccessfully);
            }
        }
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        //Get active roles from database
        GetRoles();
    }

    protected void grdRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdRoles.PageIndex = e.NewPageIndex;

        //Get active roles from database
        GetRoles();
    }

    protected void grdRoles_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string roleName = ((Label)(grdRoles.Rows[e.RowIndex].FindControl("lblRoleName"))).Text;
        
        //If user exists in role, role cannot be deleted        
        if (Roles.GetUsersInRole(roleName).Length > 0)
        {
            customValidator.IsValid = false;
            ucMessageBoxForGrid.ShowMessage(Messages.UserExistsWithRoleName);
        }

        if (Page.IsValid)
        {
            //Delete role name from database
            Roles.DeleteRole(roleName);
            ucMessageBoxForGrid.ShowMessage(Messages.RoleDeletedSuccessfully);
        }
    }

    protected IEnumerable grdRoles_MustAddARow(IEnumerable data)
    {
        return Roles.GetAllRoles();
    }
}