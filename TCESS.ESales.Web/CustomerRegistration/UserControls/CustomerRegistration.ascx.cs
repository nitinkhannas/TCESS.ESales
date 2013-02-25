#region Using directives

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Exception;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.Logging;

#endregion

public partial class CustomerRegistration_UserControls_CustomerRegistration : BaseUserControl
{
    public ShowDataEventHandler Event_ShowCustomerDocumentScreen;
    public CloseScreenEventHandler Event_CloseScreen;

    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateMasters();
            PopulateStateList();
            ResetFields();
        }

        btnCreateFolder.Text = ViewState[Globals.StateMgmtVariables.CUSTOMEREGTYPE] != null ? Labels.Save : Labels.CreateFolder;
        txtAMEVisitDate.Attributes.Add("ReadOnly", "true");
    }

    private void PopulateMasters()
    {
        ddlBusinessType.DataSource = MasterList.GetBusinessTypeList();
        ddlBusinessType.DataBind();
        ddlBusinessType.Items.Insert(0, new ListItem(Messages.SelectBusinessType, "0"));

        ddlOwnershipStatus.DataSource = MasterList.GetOwnershipStatusList();
        ddlOwnershipStatus.DataBind();
        ddlOwnershipStatus.Items.Insert(0, new ListItem(Messages.SelectOwnershipStatus, "0"));

        ddlAME.DataSource = MasterList.GetAmeBlockList();
        ddlAME.DataBind();
        ddlAME.Items.Insert(0, new ListItem(Messages.SelectAMEOffice, "0"));
    }

    public void ShowBlankScreen()
    {
        ViewState[Globals.StateMgmtVariables.CUSTOMERID] = null;
        ResetFields();
    }

    public void PopulateCustomerData(int customerId)
    {
        //Get customer details using customer id
        GetCustomerDetails(customerId);

        //Get list of materials which customer is registered to transact with using customer id
        PopulateCustMaterialTypeList(customerId);

        btnCreateFolder.Text = Labels.Update;
        btnReset.Visible = false;
        btnCancel.Visible = true;
    }

    /// <summary>
    /// Get customer details using customer id
    /// </summary>
    /// <param name="customerId">Int32: customer id</param>
    private void GetCustomerDetails(int customerId)
    {
        CustomerDTO customerDetails = MasterList.GetCustomerDetailsById(customerId);
        ViewState[Globals.StateMgmtVariables.CUSTOMERID] = customerId;

        txtTradeName.Text = customerDetails.Cust_TradeName;
        txtFirmName.Text = customerDetails.Cust_FirmName;
        txtOwnerName.Text = customerDetails.Cust_OwnerName;
        txtFatherName.Text = customerDetails.Cust_FathersName;
        txtUnitAddress.Text = customerDetails.Cust_UnitAddress;
        txtRegAddress.Text = customerDetails.Cust_RegisteredAddress;
        txtPost.Text = customerDetails.Cust_Post;
        ddlState.SelectedValue = customerDetails.Cust_State.ToString();
        PopulateDistricts(customerDetails.Cust_State);
        try
        {
            ddlDist.SelectedValue = customerDetails.Cust_District.ToString();
        }
        catch
        {
            ddlDist.SelectedValue = "0";
        }

        txtLandMark.Text = customerDetails.Cust_Landmark;
        txtPincode.Text = Convert.ToString(customerDetails.Cust_Pincode);
        txtMobile.Text = customerDetails.Cust_MobileNo;
        txtPhoneNo.Text = customerDetails.Cust_PhoneNo;
        ddlOwnershipStatus.SelectedValue = customerDetails.Cust_OwnershipStatus.ToString();
        ddlBusinessType.SelectedValue = customerDetails.Cust_BusinessType.ToString();
        ddlSaleType.SelectedValue = customerDetails.Cust_SalesType.ToString();
        txtPartnerMobile.Text = customerDetails.Cust_PartnerPhoneNo;
        ddlAME.SelectedValue = customerDetails.Cust_AMEBlockId.ToString();
        txtAMEVisitDate.Text = Convert.ToDateTime(customerDetails.Cust_AMEVisitDate).ToString("dd-MMM-yyyy");
        txtNoOfChimney.Text = Convert.ToString(customerDetails.Cust_NoOfChimneys);
        txtBrickCapacity.Text = Convert.ToString(customerDetails.Cust_BrickCapacity);
        txtExciseDiv.Text = customerDetails.Cust_Excise_Div;
        txtExciseRange.Text = customerDetails.Cust_Excise_Range;
        txtExciseComm.Text = customerDetails.Cust_Excise_Comm;
        txtBankName.Text = customerDetails.Cust_BankName;
        txtAccountNo.Text = Convert.ToString(customerDetails.Cust_BankAccountNo);
        txtBankBranch.Text = customerDetails.Cust_BankBranch;
        txtChequeNo.Text = Convert.ToString(customerDetails.Cust_BankChequeNo);
        ddlAccountType.SelectedValue = customerDetails.Cust_BankAccountType.ToString();
        txtICFAICode.Text = customerDetails.Cust_BankIFCICode;
        txtVatFiledOn.Text = Convert.ToDateTime(customerDetails.Cust_VATFiledON).ToString("dd MMM yyyy");
        ddlUnitStatus.SelectedValue = customerDetails.Cust_UnitStatus.ToString();
        txtAMEName.Text = customerDetails.Cust_AMEName;
        ViewState[Globals.StateMgmtVariables.CUSTFOLDERNAME] = customerDetails.Cust_FolderName;
    }

    /// <summary>
    /// Get list of materials which customer is registered to transact with
    /// </summary>
    /// <param name="customerId">Int32: customer id</param>
    private void PopulateCustMaterialTypeList(int customerId)
    {
        IList<CustomerMaterialMapDTO> lstCustomerMaterialMap = ESalesUnityContainer.Container.Resolve<ICustomerMaterialService>()
             .GetCustomerMaterialDetailsByCustomerId(customerId);

        foreach (GridViewRow row in grdCustomerMaterialMapping.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CustomerMaterialMapDTO customerMaterialDetails =
                    (from custAnnualReq in lstCustomerMaterialMap
                     where custAnnualReq.Cust_Mat_MaterialId == Convert.ToInt32(grdCustomerMaterialMapping.DataKeys[row.RowIndex].Value)
                     select custAnnualReq).FirstOrDefault();

                if (customerMaterialDetails != null)
                {
                    ((CheckBox)row.FindControl("chkMaterialType")).Checked = true;
                    ((CheckBox)row.FindControl("chkMaterialType")).Enabled = false;
                    ((DropDownList)row.FindControl("ddlAnnualRequirements")).SelectedValue =
                        Convert.ToString(customerMaterialDetails.Cust_Mat_AnnualRequirement);
                    ((DropDownList)row.FindControl("ddlAnnualRequirements")).Enabled = false;
                }
            }
        }
    }

    private void BindMaterialTypeList()
    {
        grdCustomerMaterialMapping.DataSource = ESalesUnityContainer.Container.Resolve<IMaterialTypeService>()
            .GetMaterialTypeList(true);
        grdCustomerMaterialMapping.DataBind();
    }

    protected void chkMaterialType_Checked(object sender, EventArgs e)
    {
        CheckBox chkMaterialType = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chkMaterialType.NamingContainer;
        Int32 rowIndex = row.RowIndex;

        DropDownList ddlMaterialType = ((DropDownList)grdCustomerMaterialMapping.Rows[rowIndex].FindControl("ddlAnnualRequirements"));
        ddlMaterialType.Enabled = false;
        ddlMaterialType.SelectedValue = "0";

        if (chkMaterialType.Checked == true)
        {
            ddlMaterialType.Enabled = true;
        }
    }

    protected void AnnualRequirements_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        CustomValidator docNoValidator = (CustomValidator)sender;
        GridViewRow row = (GridViewRow)docNoValidator.NamingContainer;
        Int32 rowIndex = row.RowIndex;

        CheckBox chkScan = (CheckBox)grdCustomerMaterialMapping.Rows[rowIndex].FindControl("chkMaterialType");

        // Check to see if it has already been validated.
        if (chkScan.Checked)
        {
            DropDownList ddlAnnualRequirement = (DropDownList)grdCustomerMaterialMapping.Rows[rowIndex].FindControl("ddlAnnualRequirements");

            if (ddlAnnualRequirement.SelectedItem.Value == "0")
            {
                args.IsValid = false;
            }
        }
    }

    private void CheckIfPageIsValid()
    {
        bool isChecked = MasterList.CheckIfNoDocumentSelected(grdCustomerMaterialMapping, "chkMaterialType");

        if (!isChecked)
        {
            gridValidator.IsValid = false;
            ucMessageBox.ShowMessage(Messages.AnnualRequirementMissing);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        CheckIfPageIsValid();

        if (Page.IsValid)
        {
            try
            {
                ExceptionHandler.AppExceptionManager.Process(() =>
                {
                    if (!Directory.Exists(Globals.FolderDetails.FOLDERPATH))
                    {
                        Directory.CreateDirectory(Globals.FolderDetails.FOLDERPATH);
                    }

                    //Initialized customer details
                    CustomerDTO customerDetails = InitializeCustomerDetails();
                    SetBankDetails(customerDetails);
                    if (customerDetails.Cust_Id == 0)
                    {
                        string folderName = customerDetails.Cust_FirmName.Replace(" ", "_") + "_" + MasterList.GetUniqueTransactionId();
                        Directory.CreateDirectory(Globals.FolderDetails.FOLDERPATH + folderName);

                        customerDetails.Cust_FolderName = folderName;
                    }

                    //Initialized customer material type details
                    IList<CustomerMaterialMapDTO> listCustomerMaterial = InitializeMaterialTypeDetails();

                    //Save customer and customer material type details where material type is the annual requirement of customer
                    int customerId = ESalesUnityContainer.Container.Resolve<ICustomerService>()
                        .SaveAndUpdateCustomerDetails(customerDetails, listCustomerMaterial);

                    //Send SMS for customer registration confirmation
                    SmsUtility.SendSMS(txtMobile.Text.Trim(), Messages.CustomerRegistrationConfirmation);

                    ViewState[Globals.StateMgmtVariables.CUSTOMERID] = customerId;

                    if (ViewState[Globals.StateMgmtVariables.CUSTOMEREGTYPE] == null)
                    {
                        if (ViewState[Globals.StateMgmtVariables.EDITCUSTOMER] != null)
                        {
                            ucMessageBoxForGrid.ShowMessage(Messages.CustomerDetailsUpdatedSuccessfully);
                        }
                        else
                        {
                            ucMessageBoxForGrid.ShowMessage(Messages.CustomerDetailsSavedSuccessfully);
                        }
                    }
                    else
                    {
                        ucMessageBoxForGrid.ShowMessage(Messages.ProspectiveCustomerSavedSuccessfully);
                        ResetFields();
                    }
                }, Globals.ExceptionTypes.ExceptionShielding.ToString());
            }
            catch (Exception ex)
            {
                CustomLogger.WriteLog(ex.Message);
            }
        }
    }

    private void SetBankDetails(CustomerDTO customerDetails)
    {

        customerDetails.Cust_BankAccountNo = txtAccountNo.Text.Trim();
        customerDetails.Cust_BankIFCICode = txtICFAICode.Text.Trim();
        customerDetails.Cust_BankName = txtBankName.Text.Trim();
        customerDetails.Cust_BankBranch = txtBankBranch.Text.Trim();
        customerDetails.Cust_BankChequeNo = Convert.ToInt32(txtChequeNo.Text.Trim());
        customerDetails.Cust_AMEName = txtAMEName.Text.Trim();
        DateTimeFormatInfo dateTimeFormatterProvider = DateTimeFormatInfo.CurrentInfo.Clone() as DateTimeFormatInfo;
        dateTimeFormatterProvider.ShortDatePattern = "dd/MM/yyyy";
        customerDetails.Cust_VATFiledON = DateTime.Parse(txtVatFiledOn.Text, dateTimeFormatterProvider);
        customerDetails.Cust_UnitStatus = Convert.ToInt32(ddlUnitStatus.SelectedItem.Value);
        customerDetails.Cust_BankAccountType = Convert.ToInt32(ddlAccountType.SelectedItem.Value);
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        if (ViewState[Globals.StateMgmtVariables.EDITCUSTOMER] != null)
        {
            Event_CloseScreen(sender);
        }
        else
        {
            if (Event_ShowCustomerDocumentScreen != null)
                Event_ShowCustomerDocumentScreen(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]), false,
                    string.Empty);
        }
    }

    private CustomerDTO InitializeCustomerDetails()
    {
        CustomerDTO customerDetails = new CustomerDTO();

        if (ViewState[Globals.StateMgmtVariables.CUSTOMERID] != null)
        {
            int customerId = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]);
            
            customerDetails = MasterList.GetCustomerDetailsById(customerId);
            ViewState[Globals.StateMgmtVariables.EDITCUSTOMER] = true;
        }

        customerDetails.Cust_TradeName = txtTradeName.Text.Trim();
        customerDetails.Cust_FirmName = txtFirmName.Text.Trim();
        customerDetails.Cust_OwnerName = txtOwnerName.Text.Trim();
        customerDetails.Cust_FathersName = txtFatherName.Text.Trim();
        customerDetails.Cust_UnitAddress = txtUnitAddress.Text.Trim(); ;
        customerDetails.Cust_Post = txtPost.Text.Trim();
        customerDetails.Cust_RegisteredAddress = txtRegAddress.Text.Trim();
        customerDetails.Cust_State = Convert.ToInt32(ddlState.SelectedItem.Value);
        customerDetails.Cust_District = Convert.ToInt32(ddlDist.SelectedItem.Value);
        customerDetails.Cust_Landmark = txtLandMark.Text.Trim();
        customerDetails.Cust_Pincode = Convert.ToInt32(txtPincode.Text);
        customerDetails.Cust_MobileNo = txtMobile.Text.Trim();
        customerDetails.Cust_PhoneNo = txtPhoneNo.Text.Trim();
        customerDetails.Cust_OwnershipStatus = Convert.ToInt32(ddlOwnershipStatus.SelectedItem.Value);
        customerDetails.Cust_BusinessType = Convert.ToInt32(ddlBusinessType.SelectedItem.Value);
        customerDetails.Cust_SalesType = Convert.ToInt32(ddlSaleType.SelectedItem.Value);
        customerDetails.Cust_PartnerPhoneNo = txtPartnerMobile.Text.Trim();
        customerDetails.Cust_AMEBlockId = Convert.ToInt32(ddlAME.SelectedItem.Value);
        customerDetails.Cust_NoOfChimneys = Convert.ToInt32(txtNoOfChimney.Text);
        customerDetails.Cust_BrickCapacity = Convert.ToInt32(txtBrickCapacity.Text);
        customerDetails.Cust_Excise_Range = txtExciseRange.Text.Trim();
        customerDetails.Cust_Excise_Div = txtExciseDiv.Text.Trim();
        customerDetails.Cust_Excise_Comm = txtExciseComm.Text.Trim();
        customerDetails.Cust_CreatedBy = GetCurrentUserId();
        customerDetails.Cust_FolderName = Convert.ToString(ViewState[Globals.StateMgmtVariables.CUSTFOLDERNAME]);
        customerDetails.Cust_RegCustType = ViewState[Globals.StateMgmtVariables.CUSTOMEREGTYPE] != null ? Convert.ToBoolean(customerDetails.Cust_RegCustType) : true;

        if (customerDetails.Cust_Id > 0)
        {
            customerDetails.Cust_LastUpdatedDate = DateTime.Now;
        }
        else
        {
            customerDetails.Cust_CreatedDate = DateTime.Now;
        }

        DateTimeFormatInfo dateTimeFormatterProvider = DateTimeFormatInfo.CurrentInfo.Clone() as DateTimeFormatInfo;
        dateTimeFormatterProvider.ShortDatePattern = "dd/MM/yyyy";
        customerDetails.Cust_AMEVisitDate = DateTime.Parse(txtAMEVisitDate.Text, dateTimeFormatterProvider);
        return customerDetails;
    }

    private IList<CustomerMaterialMapDTO> InitializeMaterialTypeDetails()
    {
        IList<CustomerMaterialMapDTO> listCustomerMaterial = new List<CustomerMaterialMapDTO>();

        foreach (GridViewRow row in grdCustomerMaterialMapping.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkMaterial = ((CheckBox)(row.Cells[0].Controls[1]));
                if (chkMaterial.Enabled == true && chkMaterial.Checked == true)
                {
                    CustomerMaterialMapDTO customerMaterialMap = new CustomerMaterialMapDTO();
                    customerMaterialMap.Cust_Mat_MaterialId = Convert.ToInt32(grdCustomerMaterialMapping.DataKeys[row.RowIndex].Value);
                    customerMaterialMap.Cust_Mat_AnnualRequirement = Convert.ToInt32(((DropDownList)(row.Cells[2].Controls[1])).SelectedItem.Text);
                    customerMaterialMap.Cust_Mat_AllotedQuantityId = 1;
                    customerMaterialMap.Cust_Mat_LiftingLimit = 10;
                    customerMaterialMap.Cust_Mat_CreatedBy = GetCurrentUserId();
                    customerMaterialMap.Cust_Mat_CreatedDate = DateTime.Now;

                    listCustomerMaterial.Add(customerMaterialMap);
                }
            }
        }

        //return the value
        return listCustomerMaterial;
    }

    private void PopulateStateList()
    {
        MasterList.GetStateList(ddlState);
    }

    private void PopulateDistricts(int stateId)
    {
        MasterList.GetDistrictListByStateId(ddlDist, stateId);
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetFields();
    }

    private void ResetFields()
    {
        txtTradeName.Text = string.Empty;
        txtFirmName.Text = string.Empty;
        ddlOwnershipStatus.SelectedIndex = 0;
        txtRegAddress.Text = string.Empty;
        txtUnitAddress.Text = string.Empty;
        txtOwnerName.Text = string.Empty;
        txtFatherName.Text = string.Empty;
        txtPincode.Text = string.Empty;
        txtLandMark.Text = string.Empty;
        txtMobile.Text = string.Empty;
        txtPhoneNo.Text = string.Empty;
        ddlSaleType.SelectedIndex = 0;
        ddlBusinessType.SelectedIndex = 0;
        ddlAME.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        txtPartnerMobile.Text = string.Empty;
        txtBrickCapacity.Text = string.Empty;
        txtNoOfChimney.Text = string.Empty;
        txtExciseDiv.Text = string.Empty;
        txtExciseRange.Text = string.Empty;
        txtExciseComm.Text = string.Empty;
        txtPost.Text = string.Empty;
        ddlDist.Items.Clear();
        ddlDist.Items.Insert(0, new ListItem(Messages.SelectDistrict, "0"));
        ddlAccountType.SelectedIndex = 0;
        ddlUnitStatus.SelectedIndex = 0;
        txtAMEName.Text = string.Empty;
        txtAccountNo.Text = string.Empty;
        txtBankName.Text = string.Empty;
        txtBankBranch.Text = string.Empty;
        txtICFAICode.Text = string.Empty;
        txtAccountNo.Text = string.Empty;
        txtChequeNo.Text = string.Empty;
        txtVatFiledOn.Text = string.Empty;
        BindMaterialTypeList();
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedItem != null)
        {
            if (ddlState.SelectedItem.Value != "0")
            {
                MasterList.GetDistrictListByStateId(ddlDist, Convert.ToInt32(ddlState.SelectedItem.Value));

                if (ddlState.SelectedItem.Value == "3")
                {
                    ddlSaleType.SelectedValue = "1";
                }
                else
                {
                    ddlSaleType.SelectedValue = "2";
                }

                ddlDist.Focus();
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Event_CloseScreen(sender);
    }

    public void Event_SetCustomerRegistrationType(bool customerRegType)
    {
        ViewState[Globals.StateMgmtVariables.CUSTOMEREGTYPE] = customerRegType;
    }
}