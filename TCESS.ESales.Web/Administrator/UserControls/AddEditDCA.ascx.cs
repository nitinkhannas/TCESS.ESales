#region Using directives

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Administrator_UserControls_AddEditDCA : BaseUserControl
{
    public event CloseScreenEventHandler Event_CloseScreen;

    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Populate states and districts
            PopulateStateAndDistricts();

            //Make controls readonly from after page load is complete
            txtStartDate.Attributes.Add("ReadOnly", "true");
            txtClosureDate.Attributes.Add("ReadOnly", "true");
        }
    }

    public void ShowBlankScreen()
    {
        ViewState[Globals.StateMgmtVariables.AGENTID] = null;
        ResetControls();
        
        btnSave.Text = Labels.Save;
        btnAllocationPercentage.Enabled = false;
    }

    public void PopulateDCADetails(int agentId)
    {
        AgentDTO agentDetails = ESalesUnityContainer.Container.Resolve<IAgentService>().GetAgentByAgentId(agentId);

        //Fill agent detail on edit
        if (agentDetails.Agent_Id > 0)
        {
            ViewState[Globals.StateMgmtVariables.AGENTID] = agentDetails.Agent_Id;
            txtShortName.Text = agentDetails.Agent_ShortName;
            txtDCAName.Text = agentDetails.Agent_Name;
            txtStartDate.Text = agentDetails.Agent_StartDate.ToString("dd MMM yyyy");
            txtPanNumber.Text = agentDetails.Agent_PanNumber;
            txtSalesTaxNumber.Text = agentDetails.Agent_SalesTaxNumber;
            txtTSLCode.Text = agentDetails.Agent_TSLCode;

            if (!agentDetails.Agent_ClosureDate.Equals(null))
            {
                txtClosureDate.Text = Convert.ToDateTime(agentDetails.Agent_ClosureDate).ToString("dd MMM yyyy");
            }

            MasterList.GetDistrictListByStateId(ddlRegDistrict, agentDetails.Agent_RegState);
            MasterList.GetDistrictListByStateId(ddlLocalDistrict, Convert.ToInt32(agentDetails.Agent_LocalState));
            MasterList.GetDistrictListByStateId(ddlComDistrict, Convert.ToInt32(agentDetails.Agent_ComState));

            txtRegContactPerson.Text = agentDetails.Agent_RegContactPerson;
            txtRegAddress.Text = agentDetails.Agent_RegAddress;
            ddlRegState.SelectedValue = agentDetails.Agent_RegState.ToString();            
            ddlRegDistrict.SelectedValue = agentDetails.Agent_RegDistrict.ToString();
            txtRegPinCode.Text = agentDetails.Agent_RegPinCode.ToString();
            txtRegMobileNo.Text = agentDetails.Agent_RegMobileNo;
            txtRegPhoneNo.Text = agentDetails.Agent_RegPhoneNo;
            txtRegEmail.Text = agentDetails.Agent_RegEmail;

            txtLocalContactPerson.Text = agentDetails.Agent_LocalContactPerson;
            txtLocalAddress.Text = agentDetails.Agent_LocalAddress;
            ddlLocalState.SelectedValue = agentDetails.Agent_LocalState.ToString();
            ddlLocalDistrict.SelectedValue = agentDetails.Agent_LocalDistrict.ToString();
            txtLocalPinCode.Text = agentDetails.Agent_LocalPinCode.ToString();
            txtLocalMobileNo.Text = agentDetails.Agent_LocalMobileNo;
            txtLocalPhoneNo.Text = agentDetails.Agent_LocalPhoneNo;
            txtLocalEmail.Text = agentDetails.Agent_LocalEmail;

            txtComContactPerson.Text = agentDetails.Agent_ComContactPerson;
            txtComAddress.Text = agentDetails.Agent_ComAddress;
            ddlComState.SelectedValue = agentDetails.Agent_ComState.ToString();
            ddlComDistrict.SelectedValue = agentDetails.Agent_ComDistrict.ToString();
            txtComPinCode.Text = agentDetails.Agent_ComPinCode.ToString();
            txtComMobileNo.Text = agentDetails.Agent_ComMobileNo;
            txtComPhoneNo.Text = agentDetails.Agent_ComPhoneNo;
            txtComEmail.Text = agentDetails.Agent_ComEmail;

            btnSave.Text = Labels.Update;
            btnAllocationPercentage.Enabled = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void PopulateStateAndDistricts()
    {
        MasterList.GetStateList(ddlRegState);
        MasterList.GetStateList(ddlLocalState);
        MasterList.GetStateList(ddlComState);

        MasterList.GetDistrictListByStateId(ddlRegDistrict, 0);
        MasterList.GetDistrictListByStateId(ddlLocalDistrict, 0);
        MasterList.GetDistrictListByStateId(ddlComDistrict, 0);
    }

    protected void Date_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        if (txtClosureDate.Text.Trim() != string.Empty)
        {
            if (Convert.ToDateTime(txtStartDate.Text) >= Convert.ToDateTime(txtClosureDate.Text))
            {
                args.IsValid = false;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            //Get values from page controls and initialize Agent details
            AgentDTO agentDetails = IntializeAgentDetails();
            string message = string.Empty;

            int agentId = ESalesUnityContainer.Container.Resolve<IAgentService>().SaveAndUpdateAgent(agentDetails);

            if (agentId > 0)
            {
                btnAllocationPercentage.Enabled = true;
            }

            if (ViewState[Globals.StateMgmtVariables.AGENTID] != null)
            {
                ucMessageBoxForGrid.ShowMessage(Messages.DCAUpdatedSuccessfully);
            }
            else
            {
                ucMessageBox.ShowMessage(Messages.DCASavedSuccessfully);
                ResetControls();
            }
        }
    }

    void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        Event_CloseScreen(sender);
    }

    /// <summary>
    /// Initialize Agent details object with control values
    /// </summary>
    /// <returns></returns>
    private AgentDTO IntializeAgentDetails()
    {
        AgentDTO agentDetails = new AgentDTO();

        //To check need to edit or add agent
        if (ViewState[Globals.StateMgmtVariables.AGENTID] != null)
        {
            agentDetails.Agent_Id = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.AGENTID]);
        }

        agentDetails.Agent_ShortName = txtShortName.Text.Trim();
        agentDetails.Agent_Name = txtDCAName.Text.Trim();
        agentDetails.Agent_StartDate = Convert.ToDateTime(txtStartDate.Text.Trim());
        agentDetails.Agent_PanNumber = txtPanNumber.Text.Trim();
        agentDetails.Agent_SalesTaxNumber = txtSalesTaxNumber.Text.Trim();
        agentDetails.Agent_TSLCode = txtTSLCode.Text.Trim();

        if (!txtClosureDate.Text.Trim().Equals(string.Empty))
        {
            agentDetails.Agent_ClosureDate = Convert.ToDateTime(txtClosureDate.Text.Trim());
        }

        agentDetails.Agent_RegContactPerson = txtRegContactPerson.Text.Trim();
        agentDetails.Agent_RegAddress = txtRegAddress.Text.Trim();
        agentDetails.Agent_RegState = Convert.ToInt32(ddlRegState.SelectedValue);
        agentDetails.Agent_RegDistrict = Convert.ToInt32(ddlRegDistrict.SelectedValue);

        if (txtRegPinCode.Text.Trim() != string.Empty)
        {
            agentDetails.Agent_RegPinCode = Convert.ToInt32(txtRegPinCode.Text.Trim());
        }

        agentDetails.Agent_RegMobileNo = txtRegMobileNo.Text.Trim();
        agentDetails.Agent_RegPhoneNo = txtRegMobileNo.Text.Trim();
        agentDetails.Agent_RegEmail = txtRegEmail.Text.Trim();

        agentDetails.Agent_LocalContactPerson = txtLocalContactPerson.Text.Trim();
        agentDetails.Agent_LocalAddress = txtLocalAddress.Text.Trim();
        agentDetails.Agent_LocalState = Convert.ToInt32(ddlLocalState.SelectedValue);
        agentDetails.Agent_LocalDistrict = Convert.ToInt32(ddlLocalDistrict.SelectedValue);

        if (txtLocalPinCode.Text.Trim() != string.Empty)
        {
            agentDetails.Agent_LocalPinCode = Convert.ToInt32(txtLocalPinCode.Text.Trim());
        }

        agentDetails.Agent_LocalMobileNo = txtLocalMobileNo.Text.Trim();
        agentDetails.Agent_LocalPhoneNo = txtLocalPhoneNo.Text.Trim();
        agentDetails.Agent_LocalEmail = txtLocalEmail.Text.Trim();

        agentDetails.Agent_ComContactPerson = txtComContactPerson.Text.Trim();
        agentDetails.Agent_ComAddress = txtComAddress.Text.Trim();
        agentDetails.Agent_ComState = Convert.ToInt32(ddlComState.SelectedValue);
        agentDetails.Agent_ComDistrict = Convert.ToInt32(ddlComDistrict.SelectedValue);

        if (txtComPinCode.Text.Trim() != string.Empty)
        {
            agentDetails.Agent_ComPinCode = Convert.ToInt32(txtComPinCode.Text.Trim());
        }

        agentDetails.Agent_ComMobileNo = txtComMobileNo.Text.Trim();
        agentDetails.Agent_ComPhoneNo = txtComPhoneNo.Text.Trim();
        agentDetails.Agent_ComEmail = txtComEmail.Text.Trim();

        //return the value
        return agentDetails;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetControls();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Event_CloseScreen(sender);
    }

    protected void ddlRegState_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Populate districts with state details
        MasterList.GetDistrictListByStateId(ddlRegDistrict, Convert.ToInt32(ddlRegState.SelectedValue));
    }

    protected void ddlLocalState_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Populate local districts with state details
        MasterList.GetDistrictListByStateId(ddlLocalDistrict, Convert.ToInt32(ddlLocalState.SelectedValue));
    }

    protected void AgentShortName_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        int agentId = 0;

        if (ViewState[Globals.StateMgmtVariables.AGENTID] != null)
        {
            agentId = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.AGENTID]);
        }

        if (ESalesUnityContainer.Container.Resolve<IAgentService>().IsAgentDetailsExists(agentId, txtShortName.Text.Trim()))
        {
            args.IsValid = false;
        }
    }

    protected void ddlComState_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Populate local districts with state details
        MasterList.GetDistrictListByStateId(ddlComDistrict, Convert.ToInt32(ddlComState.SelectedValue));
    }

    /// <summary>
    /// Reset all controls
    /// </summary>
    private void ResetControls()
    {
        txtShortName.Text = string.Empty;
        txtDCAName.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtClosureDate.Text = string.Empty;
        txtTSLCode.Text = string.Empty;
        txtSalesTaxNumber.Text = string.Empty;
        txtPanNumber.Text = string.Empty;

        //Controls for registered address
        txtRegContactPerson.Text = string.Empty;
        txtRegAddress.Text = string.Empty;
        ddlRegState.SelectedIndex = 0;
        ddlRegDistrict.SelectedIndex = 0;
        txtRegPinCode.Text = string.Empty;
        txtRegMobileNo.Text = string.Empty;
        txtRegPhoneNo.Text = string.Empty;
        txtRegEmail.Text = string.Empty;

        //Controls for local address
        txtLocalContactPerson.Text = string.Empty;
        txtLocalAddress.Text = string.Empty;
        ddlLocalState.SelectedIndex = 0;
        ddlLocalDistrict.SelectedIndex = 0;
        txtLocalPinCode.Text = string.Empty;
        txtLocalMobileNo.Text = string.Empty;
        txtLocalPhoneNo.Text = string.Empty;
        txtLocalEmail.Text = string.Empty;

        //Controls for communication address
        txtComContactPerson.Text = string.Empty;
        txtComAddress.Text = string.Empty;
        ddlComState.SelectedIndex = 0;
        ddlComDistrict.SelectedIndex = 0;
        txtComPinCode.Text = string.Empty;
        txtComMobileNo.Text = string.Empty;
        txtComPhoneNo.Text = string.Empty;
        txtComEmail.Text = string.Empty;
    }
}