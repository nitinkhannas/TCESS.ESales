#region Using directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.Logging;

#endregion

public partial class Index : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
        txtUserName.Focus();
	}

	protected void btnLogin_Click(object sender, EventArgs e)
	{        
		bool islogin = Membership.ValidateUser(txtUserName.Text.Trim(), EncryptDecrypt.EncryptPassword(txtPassword.Text.Trim()));

		//If Login Successful
		if (islogin)
		{
			Globals._blankImageBytes = ImageToBlob.ConvertImageToByteArray(Path.Combine(Server.MapPath("Images"),
				"BlankFile.jpg"));

            string macAddress = GetMACAddress();
			//Gets UserId from current logged in Username
			int userID = Convert.ToInt32(Membership.GetUser(txtUserName.Text).ProviderUserKey);
			UpdateCounterDetails(macAddress, userID);
			
			FormsAuthentication.SetAuthCookie(txtUserName.Text, true);
			Response.Redirect("Administrator/Home.aspx");
		}
		else
		{
			CustomValidator.IsValid = false;
			CustomValidator.ErrorMessage = Messages.IncorrectCredentials;
			txtPassword.Focus();
		}
	}

    private void UpdateCounterDetails(string macAddress,int userId)
    {
        //Gets counter details from MAC address
        CounterDTO counter = ESalesUnityContainer.Container.Resolve<ICounterService>()
			.GetCounterDetailsByMacId(macAddress, userId, 0);

        if (counter.Counter_Id > 0)
        {
            //Gets list of agent and material details from database
            IList<DcaMaterialAllocationDTO> lstAgentMaterialDetails = ESalesUnityContainer.Container
                .Resolve<IDcaMaterialAllocationService>()
                .GetMaterialAgentAllocationDetails(Convert.ToInt32(counter.Counter_Agent_Id), DateTime.Now.Date);

            //Update list of agent and mark agent material mapping as active
            (from agentMaterial in lstAgentMaterialDetails select agentMaterial)
                .Update(agentDetail => agentDetail.DCAMA_IsAgentActive = true);

            ESalesUnityContainer.Container.Resolve<ICounterService>()
                .SaveCounterDailyDetails(InitializeCounterDetails(counter), lstAgentMaterialDetails);
        }
    }

    private string GetMACAddress()
    {
        //Get MAC Address of cient machine
        GetMacAddressFromIPAddress macAddress = new GetMacAddressFromIPAddress();

        //Get MAC Address of cient machine
        string macId = macAddress.GetMACAddressFromARP(GetIPAddress());
        macId = string.IsNullOrEmpty(macId) ? MasterList.GetMacAddress().ToUpper().Replace(":", "") : macId.Replace(":", "").ToUpper();
        return macId;
    }

    /// <summary>
    /// Initialize counter details to be updated on daily basis
    /// </summary>
    /// <param name="counterDetail">Counter which is to be updated for current day</param>
    /// <returns>returns counter details</returns>
    private CounterDetailsDTO InitializeCounterDetails(CounterDTO counter)
    {
        CounterDetailsDTO counterDetails = new CounterDetailsDTO();
        counterDetails.CounterDetail_Counter_ID = counter.Counter_Id;
        counterDetails.CounterDetail_Agent_Id = Convert.ToInt32(counter.Counter_Agent_Id);
        counterDetails.CounterDetail_Date = DateTime.Now.Date;
        counterDetails.CounterDetail_Count = 0;
        counterDetails.CounterDetail_CreatedDate = DateTime.Now;
        return counterDetails;
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
}