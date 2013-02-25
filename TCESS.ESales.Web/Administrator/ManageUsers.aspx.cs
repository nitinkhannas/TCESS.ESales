#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.BusinessLayer.Interfaces.Users;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.DataTransferObjects.Users;

#endregion

public partial class Administrator_ManageUsers : BasePage
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
            //Bind to Users Details
            BindUserDetails();
        }
    }

    /// <summary>
    /// Gets Users Details
    /// </summary>
    private void BindUserDetails()
    {
        IList<UserAgentMappingDTO> lstUsers = ESalesUnityContainer.Container.Resolve<IUserAgentService>()
            .GetUsersAndAgentDetails();
        
        if (lstUsers.Count > 0)
        {
            grdManageUsers.DataSource = lstUsers;
            grdManageUsers.DataBind();
        }
        else
        {
            base.ShowBlankRowInGrid<UserAgentMappingDTO>(grdManageUsers);
        }
    }    

    public string FindRoleNameByUserId(object sender)
    {
        string roleName = string.Empty;
        if (Convert.ToInt32(sender) > 0)
        {
            MembershipUser user = Membership.GetUser(Convert.ToInt32(sender));
            string[] lstRoleName = Roles.GetRolesForUser(user.UserName);
            roleName = lstRoleName.Length > 0 ? lstRoleName[0] : string.Empty;
        }
        return roleName;
    }

    public string FindUserNameByUserId(object sender)
    {
        string userName = string.Empty;
        if (Convert.ToInt32(sender) > 0)
        {
            MembershipUser user = Membership.GetUser(Convert.ToInt32(sender));
            userName = user.UserName;
        }
        return userName;
    }

    protected void grdManageUsers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ViewState[Globals.StateMgmtVariables.ROLENAME] = ((Label)(grdManageUsers.Rows[e.NewEditIndex].FindControl("lblRoleName"))).Text;

        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(e.NewEditIndex);
    }

    protected void grdManageUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (Page.IsValid)
        {
            MembershipUser user = Membership.GetUser(Convert.ToInt32(grdManageUsers.DataKeys[e.RowIndex]["UAM_User_Id"]));
            TextBox txtPassword = (TextBox)grdManageUsers.Rows[e.RowIndex].FindControl("txtNewPassword");
            
            if (!string.IsNullOrEmpty(txtPassword.Text))
            {                
                bool isPasswordChanged = user.ChangePassword(user.ResetPassword(), 
                    EncryptDecrypt.EncryptPassword(txtPassword.Text.Trim()));
            }

            int userAgentMapId = Convert.ToInt32(grdManageUsers.DataKeys[e.RowIndex]["UAM_Id"]);
            UserAgentMappingDTO userAgentMapDTO = ESalesUnityContainer.Container.Resolve<IUserAgentService>()
                    .GetUserAgentMappingByMappingId(userAgentMapId);

            userAgentMapDTO.UAM_Agent_Id = Convert.ToInt32(((DropDownList)grdManageUsers.Rows[e.RowIndex].FindControl("ddlAgentName"))
                .SelectedItem.Value);

            int userId = Convert.ToInt32(grdManageUsers.DataKeys[e.RowIndex]["UAM_User_Id"]);
            
            ESalesUnityContainer.Container.Resolve<IUserAgentService>().UpdateUserAgentDetails(userAgentMapDTO);

            if (!String.IsNullOrEmpty(Convert.ToString(ViewState[Globals.StateMgmtVariables.ROLENAME])))
            {
                Roles.RemoveUserFromRole(user.UserName, Convert.ToString(ViewState[Globals.StateMgmtVariables.ROLENAME]));
            }

            Roles.AddUserToRole(user.UserName, ((DropDownList)grdManageUsers.Rows[e.RowIndex].FindControl("ddlRoleName"))
                .SelectedItem.Text);
            ucMessageBoxForGrid.ShowMessage(Messages.UserUpdatedSuccessfully);
        }
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1);
    }

    protected void grdManageUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdManageUsers.PageIndex = e.NewPageIndex;

        //Bind to Users Details
        BindUserDetails();
    }

    protected void grdManageUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        MembershipUser user = Membership.GetUser(Convert.ToInt32(grdManageUsers.DataKeys[e.RowIndex]["UAM_User_Id"]));
        user.IsApproved = false;
        Membership.UpdateUser(user);

        ESalesUnityContainer.Container.Resolve<IMembershipService>()
            .DeleteUserInRoles(Convert.ToInt32(grdManageUsers.DataKeys[e.RowIndex]["UAM_User_Id"]));

        ESalesUnityContainer.Container.Resolve<IUserAgentService>()
            .DeleteUserAgentMapping(Convert.ToInt32(grdManageUsers.DataKeys[e.RowIndex]["UAM_Id"]));

        ucMessageBoxForGrid.ShowMessage(Messages.UserDeletedSuccessfully);
    }

    private void ucMessageBoxForGrid_Event_CloseScreen(object sender)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1);
    }

    protected IEnumerable grdManageUsers_MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<UserAgentMappingDTO>();
    }

    /// <summary>
    /// Row edit/update/cancel function for grid view
    /// </summary>
    /// <param name="rowIndex">rowIndex of gridview</param>
    private void GridViewRowUpdateFunctions(int editIndex)
    {
        grdManageUsers.EditIndex = editIndex;

        //Bind to Users Details
        BindUserDetails();
    }

    protected void grdManageUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1);
    }

    protected void grdManageUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grdManageUsers.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlRoleName = (DropDownList)e.Row.FindControl("ddlRoleName");
            string[] lstRoles = Roles.GetAllRoles();
            Array.Sort(lstRoles);

            ddlRoleName.DataSource = lstRoles;
            ddlRoleName.DataBind();
            ddlRoleName.Items.Insert(0, new ListItem(Messages.SelectRole, "0"));
            ddlRoleName.SelectedIndex = Array.IndexOf(lstRoles, ViewState[Globals.StateMgmtVariables.ROLENAME]) + 1;

            DropDownList ddlAgentName = (DropDownList)e.Row.FindControl("ddlAgentName");
            MasterList.GetAgentListInDropDown(ddlAgentName);
            ddlAgentName.SelectedValue = grdManageUsers.DataKeys[e.Row.RowIndex]["UAM_Agent_Id"].ToString();
        }
    }
}