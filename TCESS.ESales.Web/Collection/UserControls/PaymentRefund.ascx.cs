#region Using directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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

public partial class Collection_UserControls_PaymentRefund : BaseUserControl
{
    public event CloseScreenEventHandler Event_ShowCancellationScreen;

    protected void Page_Init(object sender, EventArgs e)
    {
        ucYesNoMessageBox.Event_YesButtonClicked += ucYesNoMessageBox_Event_YesButtonClicked;
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
        ucRefundReceipt.Event_CloseScreen += ucRefundReceipt_Event_CloseScreen;
    }

    public void SetPaymentModeForUser(int paymentModeId)
    {
        ViewState[Globals.StateMgmtVariables.PAYMENTMODE] = paymentModeId;
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

    private void GetDocumentTypeToValidate()
    {
        ddlValidationType.DataSource = ESalesUnityContainer.Container.
            Resolve<IDocumentTypeService>().GetDocumentTypeForGhatoCollection();
        ddlValidationType.DataBind();
        ddlValidationType.Items.Insert(0, new ListItem(Labels.SelectValidationType, "0"));
        ddlValidationType.Items.Insert(1, new ListItem(Labels.BankAccountNumber, "1"));
        ddlValidationType.Items.Insert(2, new ListItem(Labels.MobileNumber, "2"));
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            btnAccept.Enabled = false;
            txtAmount.ReadOnly = false;

            IList<CustomerDTO> lstCustomer = ESalesUnityContainer.Container.Resolve<ICustomerService>().ValidateCustomerDetails(txtCustomerCode.Text.Trim(),
                Convert.ToInt32(ddlValidationType.SelectedItem.Value), txtValidationValue.Text.Trim());

            if (lstCustomer.Count > 0)
            {
                ViewState[Globals.StateMgmtVariables.CUSTOMERID] = lstCustomer[0].Cust_Id;
                if (ValidatedPaymentID(txtPaymentID.Text.Trim()))
                {
                    if (CheckCurrentBalance())
                    {
                        grdCustomersDetails.DataSource = lstCustomer;
                        grdCustomersDetails.DataBind();
                        btnValidateAmount.Enabled = true;
                        ViewState[Globals.StateMgmtVariables.PAYMENTID] = txtPaymentID.Text.Trim();
                    }
                }
                else
                {
                    ucMessageBoxForGrid.ShowMessage("Enter a valid ID");
                    ResetControls(false);
                }
            }
            else
            {
                ucMessageBoxForGrid.ShowMessage(Messages.CustomerDetailsNotFound);
                ResetControls(false);
            }
        }
    }

    private void ResetControls(bool clearSearchParameters)
    {
        if (clearSearchParameters)
        {
            txtCustomerCode.Text = string.Empty;
            ddlValidationType.SelectedValue = "0";
            txtValidationValue.Text = string.Empty;
            txtPaymentID.Text = string.Empty;
            txtPaymentID.ReadOnly = false;
            //Show blank grid
            base.ShowBlankRowInGrid<CustomerDTO>(grdCustomersDetails);
        }

        txtAmount.Text = string.Empty;
        txtInstrumentNumber.Text = string.Empty;
        ddlBankDrawn.SelectedValue = "0";
        txtInstrumentDate.Text = string.Empty;
        txtPayerName.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        txtBranchName.Text = string.Empty;
        btnValidateAmount.Enabled = false;
        ViewState[Globals.StateMgmtVariables.REFUNDID] = null;
    }

    private Boolean CheckCurrentBalance()
    {
        //get total deposit amount 
        decimal totalAmountCollected = ESalesUnityContainer.Container.Resolve<IPaymentService>().GetPaymentMadeByCustomer(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]), Convert.ToDateTime(ConfigurationManager.AppSettings["PaymentStartDate"]), Convert.ToDateTime(ConfigurationManager.AppSettings["PaymentEndDate"]));
        //get Total refund amount
        decimal totalRefundAmount = ESalesUnityContainer.Container.Resolve<IPaymentService>().GetCustomerPaymentRefundList(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID])).Sum(f => f.PR_Amount);
        //get Total exp amount
        decimal totalMaterialLiftedAmount = ESalesUnityContainer.Container.Resolve<ISettlementOfAccountsService>().GetMaterialAmountLiftedByCustomer(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]), Convert.ToDateTime(ConfigurationManager.AppSettings["PaymentStartDate"]), Convert.ToDateTime(ConfigurationManager.AppSettings["PaymentEndDate"]));
        //Get InTransit amount
        decimal InTransitAmount = GetAmount(ESalesUnityContainer.Container.Resolve<IBookingService>().GetIntransisCustomerQty(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]), Convert.ToDateTime(ConfigurationManager.AppSettings["PaymentStartDate"]), Convert.ToDateTime(ConfigurationManager.AppSettings["PaymentEndDate"])).Sum(item => item.Booking_Qty));

        if ((totalAmountCollected - totalRefundAmount) > (totalMaterialLiftedAmount + InTransitAmount))
        {
            decimal balanceAmount = totalAmountCollected - (totalMaterialLiftedAmount + InTransitAmount + totalRefundAmount);
            ViewState[Globals.StateMgmtVariables.SELECTEDAMOUNT] = string.Format("{0:N2}", balanceAmount);
            return true;
        }
        else
        {
            ResetControls(false);
            //Reset controls on page to default state
            ucMessageBoxForGrid.ShowMessage("No funds for refund");
            return false;
        }
    }

    public string BalanceAvaliable()
    {
        return Convert.ToString(ViewState[Globals.StateMgmtVariables.SELECTEDAMOUNT]);
    }

    private decimal GetAmount(decimal qty)
    {
        MaterialTypeDTO materialTypeDetails = new MaterialTypeDTO();
        materialTypeDetails = ESalesUnityContainer.Container.Resolve<IMaterialTypeService>()
            .GetMaterialTypeById(Convert.ToInt32(ESalesUnityContainer.Container.Resolve<IMaterialTypeService>().GetMaterialTypeList(true)[0].MaterialType_Id));

        decimal handlingRate = Convert.ToDecimal(qty) * Convert.ToDecimal(materialTypeDetails.MaterialType_HandlingRate);
        decimal tiscoRate = Convert.ToDecimal(qty) * Convert.ToDecimal(materialTypeDetails.MaterialType_TiscoRate);
        decimal grossAmount = handlingRate + tiscoRate;
        decimal serviceTax = handlingRate * (Convert.ToDecimal(materialTypeDetails.MaterialType_ServiceTax) / 100);
        decimal educationCess = serviceTax * (Convert.ToDecimal(materialTypeDetails.MaterialType_EducationCess) / 100);
        decimal higherEducationCess = serviceTax * (Convert.ToDecimal(materialTypeDetails.MaterialType_HigherEducationCess) / 100);
        decimal netAmount = grossAmount + serviceTax + educationCess + higherEducationCess;
        return netAmount;
    }

    private Boolean ValidatedPaymentID(string paymentID)
    {
        PaymentCollectionDTO paymentCollection = ESalesUnityContainer.Container.Resolve<IPaymentService>()
            .GetCollectionDetailsByReceiptNo(paymentID);
        if (paymentCollection.PC_Id > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void btnAccept_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int paymentMode = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.PAYMENTMODE]);

            PaymentRefundDTO paymentRefund = new PaymentRefundDTO();
            paymentRefund.PR_CustID = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]);
            paymentRefund.PR_CreatedDate = DateTime.Now;
            paymentRefund.PR_Amount = Convert.ToDecimal(txtAmount.Text);
            paymentRefund.PR_VadidatedID = Convert.ToString(ViewState[Globals.StateMgmtVariables.PAYMENTID]);
            paymentRefund.PR_CreatedBy = base.GetCurrentUserId();
            paymentRefund.PR_Remarks = txtRemarks.Text.Trim();
            paymentRefund.PR_PaymentMode = paymentMode;
            paymentRefund.PR_ReceiverName = txtPayerName.Text;
            paymentRefund.PR_MobileNumber = txtMobileNo.Text;

            ////If Payment mode is Cheque, DD and RTGS
            if (paymentMode != (int)HelperClass.PaymentModes.CASH)
            {
                paymentRefund.PR_InstrumentNo = txtInstrumentNumber.Text.Trim();
                paymentRefund.PR_BankDrawn = Convert.ToInt32(ddlBankDrawn.SelectedItem.Value);
                paymentRefund.PR_BankBranch = txtBranchName.Text.Trim();
                paymentRefund.PR_InstrumentDate = Convert.ToDateTime(txtInstrumentDate.Text);
            }

            ////If Payment mode is RTGS
            if (paymentMode == (int)HelperClass.PaymentModes.RTGSTRANSFER)
            {
                //Get details for IFSC code
                paymentRefund.PR_BankIFSCCode = txtIFSCCode.Text.Trim();
            }

            //Saves payment collection details in database
          ViewState[Globals.StateMgmtVariables.REFUNDID]= ESalesUnityContainer.Container.Resolve<IPaymentService>().SavePaymentRefund(paymentRefund);

            //Reset controls
            ResetControls(true);

            //Shows message box to user
            ucMessageBoxForGrid.ShowMessage("Payment Refund made successfully.");
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetControls(true);
    }

    private void GetPaymentModeForUsers()
    {
        IList<UserPaymentModeMappingDTO> lstUserPaymentModeMapping = ESalesUnityContainer.
            Container.Resolve<IUserPaymentModeService>().GetPaymentModesByUserId(base.GetCurrentUserId());

        ////If payment mode is not Cash, show Instrument and bank details
        if (Convert.ToInt32(ViewState[Globals.StateMgmtVariables.PAYMENTMODE]) != (int)HelperClass.PaymentModes.CASH)
        {
            trInstrumentType.Visible = true;
            trBankDrawn.Visible = true;

            //If payment mode is RTGS, show IFSC code
            if (Convert.ToInt32(ViewState[Globals.StateMgmtVariables.PAYMENTMODE]) == (int)HelperClass.PaymentModes.RTGSTRANSFER)
            {
                trIFSCCode.Visible = true;
                lblInstrumentNumber.Text = "RGTS Number";
                lblInstrumentDate.Text = "RTGS Date";
            }
            FillDropdownForBanks();
        }
    }

    protected void btnValidateAmount_Click(object sender, EventArgs e)
    {
        if (Convert.ToDecimal(txtAmount.Text.Trim()) <= Convert.ToDecimal(ViewState[Globals.StateMgmtVariables.SELECTEDAMOUNT]))
        {
            CurrencyConvertor currencyConvertor = new CurrencyConvertor();
            string amountInWords = currencyConvertor.Convertor(string.Format("{0:0.00}", Convert.ToDecimal(txtAmount.Text.Trim())));
            string message = string.Concat(amountInWords, "<br/><br/>Do you want to continue");
            ucYesNoMessageBox.ShowMessage(message);
        }
        else
        {
            ucMessageBoxForGrid.ShowMessage("Refund amount cannot be greater than balance amount");
        }
    }

    private void FillDropdownForBanks()
    {
        MasterList.FillDropdownForBanks(ddlBankDrawn);
    }

    private void ucYesNoMessageBox_Event_YesButtonClicked(object sender, EventArgs args)
    {
        btnAccept.Enabled = true;
        txtAmount.ReadOnly = true;
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        int refundId = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.REFUNDID]) ;

        if (refundId > 0)
        {
            pnlPaymentRefund.Visible = false;
            pnlRefundReceipt.Visible = true;

            //SendSMSToCustomer(collectionId);

            ucRefundReceipt.GetRefundDetails(refundId);
        }
        else
        {
            //Show blank grid
            base.ShowBlankRowInGrid<CustomerDTO>(grdCustomersDetails);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Event_ShowCancellationScreen(sender);
    }
    
    private void ucRefundReceipt_Event_CloseScreen(object sender)
    {
        pnlPaymentRefund.Visible = true;
        pnlRefundReceipt.Visible = false;

        if (Convert.ToInt32(ViewState[Globals.StateMgmtVariables.REFUNDID]) > 0)
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
}