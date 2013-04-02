#region Using directives

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.BusinessLayer.Interfaces.GhatoCollection;
using TCESS.ESales.BusinessLayer.Interfaces.Users;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.DataTransferObjects.GhatoCollection;
using TCESS.ESales.DataTransferObjects.Users;
using System.Configuration;

#endregion


public partial class Collection_UserControls_PrintSMSPaymentReceipt : BaseUserControl
{
    public event CloseScreenEventHandler Event_ShowCancellationScreen;
    protected void Page_Init(object sender, EventArgs e)
    {
        ucYesNoMessageBox.Event_YesButtonClicked += ucYesNoMessageBox_Event_YesButtonClicked;
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
        ucSMSReceipt.Event_CloseScreen += ucPaymentReceipt_Event_CloseScreen;
    }
    void ucYesNoMessageBox_Event_YesButtonClicked(object sender, EventArgs args)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDocumentTypeToValidate();
            GetPaymentModeForUsers();

            //Show blank grid
            base.ShowBlankRowInGrid<SMSPaymentRegistrationDTO>(grdCustomersDetails);
        }
    }
    private void GetPaymentModeForUsers()
    {
        int verificationMode = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.VERIFICATIONMODE]);

    }
    public void SetSMSValidationMode(int modeId)
    {
        ViewState[Globals.StateMgmtVariables.VERIFICATIONMODE] = modeId;

        ////Show initial screen details
        ShowInitialScreen();
    }
    private void GetDocumentTypeToValidate()
    {
        ddlValidationType.DataSource = ESalesUnityContainer.Container.
            Resolve<IDocumentTypeService>().GetDocumentTypeForGhatoCollection();
        ddlValidationType.DataBind();
        ddlValidationType.Items.Insert(0, new ListItem(Labels.SELECTMANDATORYDOCUMENT, "0"));
        ddlValidationType.Items.Insert(1, new ListItem(Labels.BankAccountNumber, "1"));
        ddlValidationType.Items.Insert(2, new ListItem(Labels.MobileNumber, "2"));
    }

    private void ShowInitialScreen()
    {
        pnlPaymentCollection.Visible = true;
        pnlSMSReceipt.Visible = false;
        trCustCode.Visible = true;
        trSMS.Visible = false;
    }
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

            //Get customer details based on the search information entered
            IList<CustomerDTO> lstCustomer = GetCustomerDetails(txtCustomerCode.Text.Trim(),
                Convert.ToInt32(ddlValidationType.SelectedItem.Value), txtValidationValue.Text.Trim());

            //Bind customer details with the customer list
            BindCustomerDetails(lstCustomer);

            //Reset the controls
            ResetControls(false);
        }
    }
    protected void btnValidateID_Click(object sender, EventArgs e)
    {
        IList<SMSPaymentRegistrationDTO> paymentList = null;
        int verificationMode = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.VERIFICATIONMODE]);
        paymentList = ESalesUnityContainer.Container.Resolve<IPaymentService>().
            GetCustomerSMSPaymentList(null, Convert.ToInt32(txtValidationID.Text.Trim()),
            Convert.ToInt32(ConfigurationManager.AppSettings["AdvanceSMSValidDays"]));
        if (paymentList.Count > 0)
        {
            grdCustomersDetails.DataSource = paymentList;
            grdCustomersDetails.DataBind();
        }
        else
        {
            ucMessageBoxForGrid.ShowMessage("SMS Details Not  Found");
            ResetControls(false);
        }
    }
    private IList<CustomerDTO> GetCustomerDetails(string customerCode, int validationType, string validationValue)
    {
        IList<CustomerDTO> lstCustomer = ESalesUnityContainer.Container.Resolve<ICustomerService>().
            ValidateCustomerDetails(customerCode, validationType, validationValue);
        return lstCustomer;
    }
    private void BindCustomerDetails(IList<CustomerDTO> lstCustomer)
    {
        if (lstCustomer.Count > 0)
        {
            if (lstCustomer[0].Cust_IsBlacklisted == true)
            {
                ucMessageBoxForGrid.ShowMessage(Messages.CustomerIsBlackListed);
                ResetControls(false);
            }
            else if (lstCustomer[0].Cust_IsDeleted == true)
            {
                ucMessageBoxForGrid.ShowMessage(Messages.CustomerDoesNotExist);
                ResetControls(false);
            }
            else
            {
                ViewState[Globals.StateMgmtVariables.CUSTOMERID] = lstCustomer[0].Cust_Id;
                int verificationMode = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.VERIFICATIONMODE]);
                IList<SMSPaymentRegistrationDTO> paymentList = null;
                if (verificationMode == (int)HelperClass.VerificationMode.CUSTOMERCODE)
                {
                    paymentList = ESalesUnityContainer.Container.Resolve<IPaymentService>().GetCustomerSMSPaymentList(Convert.ToInt32(lstCustomer[0].Cust_Id), null, 3);
                }
                grdCustomersDetails.DataSource = paymentList;
                grdCustomersDetails.DataBind();

            }
        }
        else
        {
            ucMessageBoxForGrid.ShowMessage(Messages.CustomerDetailsNotFound);
            ResetControls(false);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Event_ShowCancellationScreen(sender);
    }

    protected void btnPrint_Click(object sender, CommandEventArgs e)
    {
        string smsPaymentId = (string)e.CommandArgument;

        pnlPaymentCollection.Visible = false;
        pnlSMSReceipt.Visible = true;

        ucSMSReceipt.GetSMSDetails(Convert.ToInt32(smsPaymentId),
            Convert.ToInt32(ConfigurationManager.AppSettings["AdvanceSMSValidDays"]));
    }

    private void ResetControls(bool p)
    {
        txtCustomerCode.Text = "";
        txtValidationID.Text = "";
        txtValidationValue.Text = "";
        ddlValidationType.SelectedValue = "0";
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        //int collectionId = ViewState[Globals.StateMgmtVariables.COLLECTIONID] == null ?
        //    Convert.ToInt32(ViewState[Globals.StateMgmtVariables.NEWCOLLECTIONID]) :
        //    Convert.ToInt32(ViewState[Globals.StateMgmtVariables.COLLECTIONID]);

        //if (collectionId > 0)
        //{
        //    pnlPaymentCollection.Visible = false;
        //    pnlPaymentReceipt.Visible = true;

        //    SendSMSToCustomer(collectionId);


        //}
        //else
        {
            //Show blank grid
            base.ShowBlankRowInGrid<SMSPaymentRegistrationDTO>(grdCustomersDetails);
        }
    }

    private void ucPaymentReceipt_Event_CloseScreen(object sender)
    {
        pnlPaymentCollection.Visible = true;
        pnlSMSReceipt.Visible = false;

        if (Convert.ToInt32(ViewState[Globals.StateMgmtVariables.COLLECTIONID]) > 0)
        {
            Event_ShowCancellationScreen(sender);
        }
        else
        {
            //Show blank grid
            base.ShowBlankRowInGrid<SMSPaymentRegistrationDTO>(grdCustomersDetails);

            //Reset controls
            ResetControls(true);
        }
    }


    protected void ddLSerachType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddLSerachType.SelectedItem.Value == "1")
        {
            trCustCode.Visible = true;
            trSMS.Visible = false;

        }
        else
        {
            trSMS.Visible = true;
            trCustCode.Visible = false;
        }
        //Show blank grid
        base.ShowBlankRowInGrid<SMSPaymentRegistrationDTO>(grdCustomersDetails);
        ResetControls(true);
    }
}