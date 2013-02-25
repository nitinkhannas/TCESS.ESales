#region Using directives

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Exception;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Bookings_UserControls_CounterCreation : BaseUserControl
{
    public event CloseScreenEventHandler Event_CloseScreen;
    /// <summary>
    /// Page Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateUser(0);
            txtMacId.Text = GetMac();
        }
    }
    /// <summary>
    /// To show Blank screen
    /// </summary>
    public void ShowBlankScreen()
    {
        ViewState[Globals.StateMgmtVariables.COUNTERID] = null;
        ViewState[Globals.StateMgmtVariables.AGENTID] = null;
        ResetFields();
    }
    /// <summary>
    /// To populate User by counterID
    /// </summary>
    /// <param name="counterId"></param>
    public void PopulateUser(int counterId)
    {
        MembershipUserCollection usercolletion = Membership.GetAllUsers();
        IList<CounterDTO> lstCounter = new List<CounterDTO>();

        if (counterId != 0)
        {
             lstCounter = ESalesUnityContainer.Container.Resolve<ICounterService>().GetCounters(counterId);
        }
        else
        {
            lstCounter = ESalesUnityContainer.Container.Resolve<ICounterService>().GetCounters(0);
        }
       
        var result = from MembershipUser user in Membership.GetAllUsers() where !(from cnt in lstCounter 
        select cnt.Counter_User_Id).Contains(Convert.ToInt32(user.ProviderUserKey )) select user;

        ddlUser.DataSource = result.Where(item => item.UserName  != "SuperAdmin" && item.IsApproved==true);
        ddlUser.DataBind();
        ddlUser.Items.Insert(0, new ListItem(Messages.SelectUser, "0"));
    }
    /// <summary>
    /// Save button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                ExceptionHandler.AppExceptionManager.Process(() =>
                {
                    //Initialize counter details
                    CounterDTO counters = InitializeCounterDetails();

                    //Saves counter detail
                    ESalesUnityContainer.Container.Resolve<ICounterService>().SaveAndUpdateCounters(counters);

                    if (ViewState[Globals.StateMgmtVariables.COUNTERID] != null)
                    {
                        ViewState["CounterId"] = null;
                        ucMessageBox.ShowMessage(Messages.CounterUpdatedSuccessfully);
                        Event_CloseScreen(sender);
                    }
                    else
                    {
                        ucMessageBox.ShowMessage(Messages.CounterCreatedSuccessfully);
                        ResetFields();
                    }
                }, Globals.ExceptionTypes.AssistingAdministrators.ToString());
            }
            catch (Exception ex)
            {
            }
        }
    }

    /// <summary>
    /// Initialized counter details from page values
    /// </summary>
    /// <returns>returns counter details with duly filled in values</returns>
    private CounterDTO InitializeCounterDetails()
    {    
        CounterDTO counterDetails = new CounterDTO();
        if (ViewState[Globals.StateMgmtVariables.COUNTERID] != null)
        {
            counterDetails.Counter_Id = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.COUNTERID]);
        }

        counterDetails.Counter_Name = txtCounterName.Text.Trim();
        counterDetails.Counter_MAC_Id = txtMacId.Text.Trim();
        counterDetails.Counter_Agent_Id = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.AGENTID]);
        counterDetails.Counter_User_Id = Convert.ToInt32(Membership.GetUser(ddlUser.SelectedItem.Value).ProviderUserKey);
        counterDetails.Counter_CreatedBy = base.GetCurrentUserId();
        counterDetails.Counter_CreatedDate = DateTime.Now;
        counterDetails.Counter_LastUpdatedDate = DateTime.Now;
        return counterDetails;
    }
    /// <summary>
    /// Event for Reset button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetFields();
    }
    /// <summary>
    /// To populate Counter detail dropdown by counterId
    /// </summary>
    /// <param name="counterId">Int32:counterId</param>
    public void PopulateCounterDetails(int counterId)
    {
        try
        {
            ExceptionHandler.AppExceptionManager.Process(() =>
            {
                ViewState["CounterId"] = counterId;
                PopulateUser(counterId);
                CounterDTO counterDetails = ESalesUnityContainer.Container.Resolve<ICounterService>().
                        GetCounterDetailsById(counterId);

                ViewState[Globals.StateMgmtVariables.COUNTERID] = counterDetails.Counter_Id;
                txtCounterName.Text = counterDetails.Counter_Name;
                txtMacId.Text = counterDetails.Counter_MAC_Id;
                MembershipUser objMembershipUser = Membership.GetUser(counterDetails.Counter_User_Id);
                ddlUser.SelectedValue = objMembershipUser.UserName;
                SetAgentDetails(Convert.ToInt32(counterDetails.Counter_User_Id));
            }, Globals.ExceptionTypes.AssistingAdministrators.ToString());
        }
        catch (Exception ex)
        {
        }
    }
    /// <summary>
    /// To Reset Fields
    /// </summary>
    public void ResetFields()
    {
        txtCounterName.Text = string.Empty;
        txtMacId.Text = GetMac();
        txtAgent.Text = string.Empty;
        ddlUser.SelectedIndex = 0;
    }
    /// <summary>
    /// Event for cancel button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Event_CloseScreen(sender);
    }
    /// <summary>
    /// Event for selected Index change of dropdown list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUser.SelectedItem.Value.ToString() != "0")
        {
            SetAgentDetails(Convert.ToInt32(Membership.GetUser(ddlUser.SelectedItem.Text).ProviderUserKey));
        }
    }
    /// <summary>
    /// Save Agent Detail By UserId
    /// </summary>
    /// <param name="userId"></param>
    private void SetAgentDetails(int userId)
    {
        UserAgentMappingDTO agentMapDetails = MasterList.GetAgentByUserId(userId);

        txtAgent.Text = agentMapDetails.UAM_Agent_Name;
        ViewState[Globals.StateMgmtVariables.AGENTID] = agentMapDetails.UAM_Agent_Id;
    }

    /// <summary>
    /// Gets the MAC address of the system currently logged in to the server
    /// </summary>
    /// <returns>MAC address of the first operation nic found.</returns>
    private string GetIPAddress()
    {
        string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] ipRange = ipAddress.Split(',');
            int le = ipRange.Length - 1;
            string trueIP = ipRange[le];
        }
        else
        {
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        }
        return ipAddress;
    }
    /// <summary>
    /// Function to get Mac
    /// </summary>
    /// <returns></returns>
	private string GetMac()
	{
		string macId = string.Empty;
		//Get MAC Address of cient machine
		GetMacAddressFromIPAddress macAddress = new GetMacAddressFromIPAddress();
		//Get MAC Address of cient machine
		macId = macAddress.GetMACAddressFromARP(GetIPAddress());
		macId = string.IsNullOrEmpty(macId) ? MasterList.GetMacAddress().Replace(":", "") : macId.Replace(":", "");
		return macId.ToUpper();
	}

    /// <summary>
    /// TO check MACId for this user already exists or not
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected void CheckMACId_ServerValidate(object sender, ServerValidateEventArgs args)
    {
          CounterDTO objCounterDTO=new CounterDTO();
          if (ViewState["CounterId"] != null)
          {
              objCounterDTO = ESalesUnityContainer.Container.Resolve<ICounterService>().GetCounterDetailsByMacId(txtMacId.Text.Trim(),  Convert.ToInt32(Membership.GetUser(ddlUser.SelectedItem.Value).ProviderUserKey), Convert.ToInt32(ViewState["CounterId"]));
          }
          else
          {
              objCounterDTO = ESalesUnityContainer.Container.Resolve<ICounterService>().GetCounterDetailsByMacId(txtMacId.Text.Trim(), Convert.ToInt32(Membership.GetUser(ddlUser.SelectedItem.Value).ProviderUserKey), 0);
          }

        if (objCounterDTO.Counter_Id >0)
        {
            args.IsValid = false;
        }
    }
}