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

#endregion

public partial class GhatoCollection_UserControls_PaymentCollection : BaseUserControl
{
    public event CloseScreenEventHandler Event_ShowCancellationScreen;

    public void SetPaymentModeForUser(int paymentModeId)
    {
        ViewState[Globals.StateMgmtVariables.PAYMENTMODE] = paymentModeId;

        ////Show initial screen details
        ShowInitialScreen();

        SetSettingsForSMSAcceptance(paymentModeId);

        int counterId = ESalesUnityContainer.Container.Resolve<ICounterService>().
            GetCounterDetailsByUserId(base.GetCurrentUserId());

        if (counterId == 0)
        {
            ucMessageBoxForGrid.ShowMessage(Messages.CounerNotAlloted);
            ResetControls(false);
            btnValidate.Enabled = false;
        }
    }

    public void SetPaymentModeForUser(int collectionId, int paymentModeId, int custId)
    {
        ViewState[Globals.StateMgmtVariables.PAYMENTMODE] = paymentModeId;
        ViewState[Globals.StateMgmtVariables.COLLECTIONID] = collectionId;

        if (collectionId > 0)
        {
            IList<CustomerDTO> lstCustomer = GetCustomerDetails(string.Empty, custId, "0");
            BindCustomerDetails(lstCustomer);

            GetPaymentModeForUsers();
            ResetControls(false);

            txtCustomerCode.Text = lstCustomer[0].Cust_Code;
            txtRemarks.Text = "Issued against Receipt No " + collectionId;
            txtRemarks.ReadOnly = true;
            txtCustomerCode.ReadOnly = true;
            btnValidate.Enabled = false;
            btnCancel.Visible = true;

            ////Show initial screen details
            ShowInitialScreen();
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        ucYesNoMessageBox.Event_YesButtonClicked += ucYesNoMessageBox_Event_YesButtonClicked;
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
        ucPaymentReceipt.Event_CloseScreen += ucPaymentReceipt_Event_CloseScreen;
    }

    void ucYesNoMessageBox_Event_YesButtonClicked(object sender, EventArgs args)
    {
        btnAccept.Enabled = true;
        txtAmount.ReadOnly = true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDocumentTypeToValidate();
            GetPaymentModeForUsers();

            //Show blank grid
            base.ShowBlankRowInGrid<CustomerDTO>(grdCustomersDetails);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Event_ShowCancellationScreen(sender);
    }

    protected void btnAccept_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int previousCollectionId = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.COLLECTIONID]);

            ////Save new collection details
            int newCollectionId = SaveNewCollection(previousCollectionId);

            ////Checks if its request for reissue of cancelled receipt
            ////If yes, update new receipt no in cancelled receipt
            if (previousCollectionId > 0)
            {
                ////Update collection status and new receipt no in database 
                ////for cancelled collection
                UpdateStatusForCancelledCollection(previousCollectionId, newCollectionId);
            }
            else
            {
                ViewState[Globals.StateMgmtVariables.NEWCOLLECTIONID] = newCollectionId;
            }

            //Shows message box to user
            ucMessageBoxForGrid.ShowMessage(Messages.PaymentSubmittedSuccessfully);
        }
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (!tdVal1.Visible)
            {
                ddlValidationType.SelectedItem.Value = "2";
                CustomerDTO cust = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetCustomerDetailsByCode(txtCustomerCode.Text.Trim());
                ViewState[Globals.StateMgmtVariables.CUSTOMERNAME] = cust.Cust_OwnerName;
                txtValidationValue.Text = cust.Cust_MobileNo;
            }

            //Get customer details based on the search information entered
            IList<CustomerDTO> lstCustomer = GetCustomerDetails(txtCustomerCode.Text.Trim(),
                Convert.ToInt32(ddlValidationType.SelectedItem.Value), txtValidationValue.Text.Trim());

            //Bind customer details with the customer list
            BindCustomerDetails(lstCustomer);

            //Reset the controls
            ResetControls(false);
        }
    }

    protected void btnSMSValidate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            SMSPaymentRegistrationDTO smsPaymentDetails = ESalesUnityContainer.Container
                .Resolve<IPaymentService>().GetSMSPaymentDetails(Convert.ToInt32(txtSMSId.Text.Trim()));

            //Get customer details based on the search information entered
            IList<CustomerDTO> lstCustomer = GetCustomerDetails(string.Empty,
                smsPaymentDetails.SMSPay_CustId, "0");

            //Bind customer details with the customer list
            BindCustomerDetails(lstCustomer);

            if (smsPaymentDetails.SMSPay_Amount > 0)
            {
                txtAmount.Text = smsPaymentDetails.SMSPay_Amount.ToString();
                txtAmount.ReadOnly = true;
                btnAccept.Enabled = true;
            }
        }
    }

    protected void btnValidateAmount_Click(object sender, EventArgs e)
    {
        CurrencyConvertor currencyConvertor = new CurrencyConvertor();
        string amountInWords = currencyConvertor.Convertor(string.Format("{0:0.00}", Convert.ToDecimal(txtAmount.Text.Trim())));
        string message = string.Concat(amountInWords, "<br/><br/>Do you want to continue");
        ucYesNoMessageBox.ShowMessage(message);
    }

    /// <summary>
    /// Reset controls on screen
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetControls(true);
    }

    /// <summary>
    /// Update status for cancelled collection
    /// </summary>
    /// <param name="previousCollectionId">Collection Id for cancelled collection</param>
    private void UpdateStatusForCancelledCollection(int previousCollectionId, int newCollectionId)
    {
        PaymentCollectionDTO previousCollection = ESalesUnityContainer.Container.
            Resolve<IPaymentService>().GetCollectionDetailsById(previousCollectionId);
        previousCollection.PC_Status = (int)Globals.CollectionStatus.REISSUED;
        previousCollection.PC_NewReceiptId = newCollectionId;

        ////Sets the new collection id in viewstate
        ViewState[Globals.StateMgmtVariables.COLLECTIONID] = newCollectionId;

        ////Update payment collection details in database
        ESalesUnityContainer.Container.Resolve<IPaymentService>().SaveOrUpdateCollection(previousCollection);
    }

    /// <summary>
    /// Save new collection details
    /// </summary>
    /// <param name="previousCollectionId">Collection Id for cancelled collection</param>
    private int SaveNewCollection(int previousCollectionId)
    {
        int paymentMode = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.PAYMENTMODE]);

        PaymentCollectionDTO paymentCollection = new PaymentCollectionDTO();

        paymentCollection.PC_ReceiptNo = string.Format("{0}/{1}/{2}/{3}",
            DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(),
            DateTime.Now.Day.ToString(), GetLeadingZeroesForReceiptNumber());

        paymentCollection.PC_CustId = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]);
        paymentCollection.PC_ReceiptDate = DateTime.Now;
        paymentCollection.PC_Amount = Convert.ToDecimal(txtAmount.Text);

        ////If Payment mode is Cheque, DD and RTGS
        if (paymentMode != (int)HelperClass.PaymentModes.CASH)
        {
            paymentCollection.PC_InstrumentNo = txtInstrumentNumber.Text.Trim();
            paymentCollection.PC_BankDrawn = Convert.ToInt32(ddlBankDrawn.SelectedItem.Value);
            paymentCollection.PC_BankBranch = txtBranchName.Text.Trim();
            paymentCollection.PC_InstrumentDate = Convert.ToDateTime(txtInstrumentDate.Text);
        }

        ////If Payment mode is RTGS
        if (paymentMode == (int)HelperClass.PaymentModes.RTGSTRANSFER)
        {
            //Get details for IFSC code
            paymentCollection.PC_BankIFSCCode = txtIFSCCode.Text.Trim();

            txtPayerName.Text = Convert.ToString(ViewState[Globals.StateMgmtVariables.CUSTOMERNAME]);
            txtMobileNo.Text = txtValidationValue.Text;
        }

        ////If its reissue of a cancelled collection
        if (previousCollectionId > 0)
        {
            ////Get previous collection Id and set it in old receipt no column in database
            paymentCollection.PC_OldReceiptId = previousCollectionId;
        }

        paymentCollection.PC_PaymentMode = paymentMode;
        paymentCollection.PC_PayerName = txtPayerName.Text.Trim();
        paymentCollection.PC_MobileNumber = txtMobileNo.Text.Trim();
        paymentCollection.PC_Remark = txtRemarks.Text.Trim();
        paymentCollection.PC_ReprintCount = 0;
        paymentCollection.PC_CreatedBy = base.GetCurrentUserId();
        paymentCollection.PC_CreatedDate = DateTime.Now;

        //Saves payment collection details in database
        return ESalesUnityContainer.Container.Resolve<IPaymentService>().
            SaveOrUpdateCollection(paymentCollection);
    }

    private string GetLeadingZeroesForReceiptNumber()
    {
        int id = 1;
        string leadingZeroes = string.Empty;

        if (id < 10)
        {
            leadingZeroes = "0000";
        }
        else if (id < 100)
        {
            leadingZeroes = "000";
        }
        else if (id < 1000)
        {
            leadingZeroes = "00";
        }
        else if (id < 10000)
        {
            leadingZeroes = "0";
        }
        return leadingZeroes;
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

                grdCustomersDetails.DataSource = lstCustomer;
                grdCustomersDetails.DataBind();
                btnValidateAmount.Enabled = true;
            }
        }
        else
        {
            ucMessageBoxForGrid.ShowMessage(Messages.CustomerDetailsNotFound);
            ResetControls(false);
        }
    }

    private void ResetControls(bool clearSearchParameters)
    {
        if (clearSearchParameters)
        {
            if (Convert.ToInt32(ViewState[Globals.StateMgmtVariables.COLLECTIONID]) == 0)
            {
                txtCustomerCode.Text = string.Empty;
                btnValidateAmount.Enabled = false;
                txtRemarks.Text = string.Empty;
            }

            ddlValidationType.SelectedValue = "0";
            txtValidationValue.Text = string.Empty;
        }

        txtAmount.ReadOnly = false;
        txtAmount.Text = string.Empty;
        txtInstrumentNumber.Text = string.Empty;
        ddlBankDrawn.SelectedValue = "0";
        txtInstrumentDate.Text = string.Empty;
        txtPayerName.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        txtBranchName.Text = string.Empty;
        btnAccept.Enabled = false;
        ViewState[Globals.StateMgmtVariables.NEWCOLLECTIONID] = null;
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

    private void GetPaymentModeForUsers()
    {
        int paymentMode = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.PAYMENTMODE]);
        trPayeeType.Visible = true;

        CheckValidation(true);

        ////If payment mode is not Cash, show Instrument and bank details
        if (paymentMode == (int)HelperClass.PaymentModes.CHEQUE ||
            paymentMode == (int)HelperClass.PaymentModes.DEMANDDRAFT ||
            paymentMode == (int)HelperClass.PaymentModes.RTGSTRANSFER)
        {
            trInstrumentType.Visible = true;
            trBankDrawn.Visible = true;

            //If payment mode is RTGS, show IFSC code
            if (Convert.ToInt32(ViewState[Globals.StateMgmtVariables.PAYMENTMODE]) == (int)HelperClass.PaymentModes.RTGSTRANSFER)
            {
                trIFSCCode.Visible = true;
                trPayeeType.Visible = false;
                lblInstrumentNumber.Text = "RGTS Number";
                lblInstrumentDate.Text = "RTGS Date";
                CheckValidation(false);
            }

            //Fill bank details in drop down
            FillDropdownForBanks();
        }
    }

    private void CheckValidation(bool show)
    {
        if (show)
        {
            txtValidationValue.Visible = true;
            tdVal1.Visible = true;
            tdVal2.Visible = true;
            tdVal3.Visible = true;
            tdVal4.Visible = true;
            tdVal5.Visible = true;
        }
        else
        {
            tdVal1.Visible = false;
            tdVal2.Visible = false;
            tdVal3.Visible = false;
            tdVal4.Visible = false;
            tdVal5.Visible = false;
            txtValidationValue.Visible = false;
            txtValidationValue.Text = "0";
        }
    }

    private void FillDropdownForBanks()
    {
        MasterList.FillDropdownForBanks(ddlBankDrawn);
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        int collectionId = ViewState[Globals.StateMgmtVariables.COLLECTIONID] == null ?
            Convert.ToInt32(ViewState[Globals.StateMgmtVariables.NEWCOLLECTIONID]) :
            Convert.ToInt32(ViewState[Globals.StateMgmtVariables.COLLECTIONID]);

        if (collectionId > 0)
        {
            pnlPaymentCollection.Visible = false;
            pnlPaymentReceipt.Visible = true;

            SendSMSToCustomer(collectionId);

            ucPaymentReceipt.GetPaymentDetails(collectionId, false);
        }
        else
        {
            //Show blank grid
            base.ShowBlankRowInGrid<CustomerDTO>(grdCustomersDetails);
        }
    }

    private void ucPaymentReceipt_Event_CloseScreen(object sender)
    {
        pnlPaymentCollection.Visible = true;
        pnlPaymentReceipt.Visible = false;

        if (Convert.ToInt32(ViewState[Globals.StateMgmtVariables.COLLECTIONID]) > 0)
        {
            Event_ShowCancellationScreen(sender);
        }
        else
        {
            //Show blank grid
            base.ShowBlankRowInGrid<CustomerDTO>(grdCustomersDetails);

            //Reset controls
            ResetControls(true);
        }
    }

    /// <summary>
    /// Show initial screen details
    /// </summary>
    private void ShowInitialScreen()
    {
        pnlPaymentReceipt.Visible = false;
        pnlPaymentCollection.Visible = true;
    }

    private void SendSMSToCustomer(int collectionId)
    {
        PaymentCollectionDTO collectionDetails = ESalesUnityContainer.Container.
            Resolve<IPaymentService>().GetCollectionDetailsById(collectionId);

        int paymentModeId = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.PAYMENTMODE]);
        string englishMessage = string.Empty;

        switch (paymentModeId)
        {
            case (int)HelperClass.PaymentModes.CASH:
                englishMessage = string.Format(Messages.CASHDEPOSITEDSMS,
                    collectionDetails.CustomerCode,
                    String.Format("{0:dd/MM/yyyy}", collectionDetails.PC_ReceiptDate),
                    collectionDetails.PC_Id, collectionDetails.PC_Amount,
                    collectionDetails.PC_PayerName);
                break;

            case (int)HelperClass.PaymentModes.CHEQUE:
                englishMessage = string.Format(Messages.CHEQUEDEPOSITEDSMS,
                    collectionDetails.CustomerCode,
                    String.Format("{0:dd/MM/yyyy}", collectionDetails.PC_ReceiptDate),
                    collectionDetails.PC_Id, collectionDetails.PC_Amount,
                    collectionDetails.PC_InstrumentNo, collectionDetails.PC_PayerName);
                break;

            case (int)HelperClass.PaymentModes.DEMANDDRAFT:
                englishMessage = string.Format(Messages.DDDEPOSITEDSMS,
                    collectionDetails.CustomerCode,
                    String.Format("{0:dd/MM/yyyy}", collectionDetails.PC_ReceiptDate),
                    collectionDetails.PC_Id, collectionDetails.PC_Amount,
                    collectionDetails.PC_InstrumentNo, collectionDetails.PC_PayerName);
                break;

            case (int)HelperClass.PaymentModes.RTGSTRANSFER:
                englishMessage = string.Format(Messages.RTGSDEPOSITEDSMS,
                    collectionDetails.CustomerCode,
                    String.Format("{0:dd/MM/yyyy}", collectionDetails.PC_ReceiptDate),
                    collectionDetails.PC_Id, collectionDetails.PC_Amount,
                    collectionDetails.PC_InstrumentNo, collectionDetails.BankName);
                break;

            default:
                break;
        }
        SmsUtility.SendSMSForBookings(collectionDetails.PC_MobileNumber, englishMessage);
    }

    private void SetSettingsForSMSAcceptance(int paymentModeId)
    {
        tblSMSControls.Visible = false;
        tblValidateControls.Visible = true;

        if (paymentModeId == (int)Globals.PaymentModes.CASH)
        {
            tblSMSControls.Visible = true;
            tblValidateControls.Visible = false;
            btnValidateAmount.Visible = false;
        }
    }
}