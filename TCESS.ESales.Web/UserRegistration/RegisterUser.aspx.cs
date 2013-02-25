#region Using directives

using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces.Users;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class UserRegistration_RegisterUser : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        CheckIsUserAuthenticated();

        if (!IsPostBack)
		{
            //Get active user roles from database
			PopulateRole();
            
            //Get active agent details from database
            PopulateDCA();
         }
	}

	protected void btnAdd_Click(object sender, EventArgs e)
	{
        if (Page.IsValid)
        {
            if (Membership.FindUsersByName(txtUserName.Text.Trim()).Count == 0)
            {
                //Create new user in database
                MembershipUser userName = Membership.CreateUser(txtUserName.Text.Trim(), 
                    EncryptDecrypt.EncryptPassword(txtPassword.Text.Trim()));
                
                //Gets user id of newly created user
                Int32 newUserId = (Int32)userName.ProviderUserKey;

                //Add new user to selected role
                Roles.AddUserToRole(txtUserName.Text.Trim(), ddlUserType.SelectedItem.Text);
                
                //Saves user agent mapping so that user is always connected to selected user
                ESalesUnityContainer.Container.Resolve<IUserAgentService>()
                    .SaveUserAgentMapping(InitializeUserAgentDetails(newUserId));
                
                //Show notification message
                ucMessageBox.ShowMessage(Messages.UserCreatedSuccessfully);

                //Reset page controls
                RestControls();
            }
        }
	}

    /// <summary>
    /// Initialize user agent mapping details
    /// </summary>
    /// <param name="userid">User id which should be associated with agent</param>
    /// <returns>Returns User Agent mapping details</returns>
	private UserAgentMappingDTO InitializeUserAgentDetails(int userid)
	{
		UserAgentMappingDTO userAgentMappingDetails = new UserAgentMappingDTO();
        userAgentMappingDetails.UAM_User_Id = userid;
        userAgentMappingDetails.UAM_Agent_Id = Convert.ToInt32(ddlDCAGroup.SelectedItem.Value);
        //userAgentMappingDetails.UAM_FirstName = txtFirstName.Text.Trim();
        //userAgentMappingDetails.UAM_LastName = txtLastName.Text.Trim();
        userAgentMappingDetails.UAM_CreatedDate = DateTime.Now;
        userAgentMappingDetails.UAM_CreatedBy = GetCurrentUserId();
        
        //return the value
        return userAgentMappingDetails;
	}

    protected void UserName_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        if (Membership.FindUsersByName(txtUserName.Text.Trim()).Count != 0)
        {
            args.IsValid = false;
        }
    }

	protected void btnReset_Click(object sender, EventArgs e)
	{
		RestControls();
	}

    /// <summary>
    /// Reset page controls
    /// </summary>
    private void RestControls()
	{
		txtFirstName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        txtUserName.Text = string.Empty;
        txtPassword.Text = string.Empty;
		ddlDCAGroup.SelectedIndex = 0;
		ddlUserType.SelectedIndex = 0;
	}

    /// <summary>
    /// Get active agent details from database
    /// </summary>
	private void PopulateDCA()
	{
        MasterList.GetAgentListInDropDown(ddlDCAGroup);
	}

    /// <summary>
    /// Get active user roles from database
    /// </summary>
	private void PopulateRole()
	{
		string[] arrRoles = Roles.GetAllRoles();
        Array.Sort(arrRoles);
        ddlUserType.DataSource = arrRoles;
		ddlUserType.DataBind();
		ddlUserType.Items.Insert(0, new ListItem(Messages.SelectRole, "0"));
	}
}