#region Using directives

using System;
using System.Configuration;
using System.Web.UI;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces.GhatoCollection;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects.GhatoCollection;

#endregion

public partial class Collection_UserControls_EditCheque : BaseUserControl
{
    public CloseScreenEventHandler Event_ShowChequeActivationScreen;

    public void ShowChequeDetails(int collectionId)
    {
        ViewState[Globals.StateMgmtVariables.COLLECTIONID] = collectionId;

        PaymentCollectionDTO collectionDetails = ESalesUnityContainer.Container.Resolve<IPaymentService>()
               .GetCollectionDetailsById(collectionId);
        txtChequeNumber.Text = collectionDetails.PC_InstrumentNo;
        txtChequeDate.Text = String.Format("{0:dd/MM/yyyy}", collectionDetails.PC_InstrumentDate);
        ViewState[Globals.StateMgmtVariables.INSTRUMENTTYPE] = collectionDetails.PC_InstrumentDate;
        txtCustomerCode.Text = collectionDetails.CustomerCode;
        txtCustomerName.Text = collectionDetails.CustomerName;
        txtBankName.Text = collectionDetails.BankName;
        txtBranchName.Text = collectionDetails.PC_BankBranch;
        txtAmount.Text = Convert.ToString(collectionDetails.PC_Amount);

        ResetControls();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Loads bank details from database
            MasterList.FillDropdownForBanks(ddlBank);

            //Loads rejection reasons from database
            MasterList.FillDropdownForRejectionReasons(ddlRejectionReason);
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Event_ShowChequeActivationScreen(sender);
    }

    protected void btnAccept_Click(object sender, EventArgs e)
    {
        if (!CheckPageValidity())
        {
            return;
        }

        if (Page.IsValid)
        {
            PaymentCollectionDTO paymentCollection = ESalesUnityContainer.Container.Resolve<IPaymentService>()
                .GetCollectionDetailsById(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.COLLECTIONID]));

            int chequeStatus = Convert.ToInt32(ddlChequeStatus.SelectedItem.Value);
            paymentCollection.PC_InstrumentStatus = chequeStatus;
            paymentCollection.PC_InstrumentRealizationDate = chequeStatus == 1 ? DateTime.Now : (DateTime?)null;            
            paymentCollection.PC_DateOfCredit = chequeStatus == 1 ? DateTime.Now : (DateTime?)null;
            
            ////Checks if there are some deductions in cheque during realization
            ////If so, use amount credited field
            paymentCollection.PC_Amount = chequeStatus == 1 ?
                string.IsNullOrEmpty(Convert.ToString(txtAmountCredited.Text)) ?
                Convert.ToDecimal(txtAmount.Text) :
                Convert.ToDecimal(txtAmountCredited.Text) : Convert.ToDecimal(txtAmount.Text);

            ////If there are some deductions in cheque during realization
            ////store the cheque amount in previous amount field in database
            paymentCollection.PC_PreviousAmount = chequeStatus == 1 ?
                !string.IsNullOrEmpty(Convert.ToString(txtAmountCredited.Text)) ? 
                Convert.ToDecimal(txtAmountCredited.Text) : 
                Convert.ToDecimal(txtAmount.Text) : (decimal?)null;
            paymentCollection.PC_LastUpdateDate = DateTime.Now;
            paymentCollection.PC_LastUpdatedBy = base.GetCurrentUserId();

            ESalesUnityContainer.Container.Resolve<IPaymentService>().SaveOrUpdateCollection(paymentCollection);

            if (chequeStatus == 1)
            {
                ucMessageBox.ShowMessage(Messages.CHEQUEACTIVATED);
            }
            else if (chequeStatus == 3)
            {
                ucMessageBox.ShowMessage(Messages.CHEQUEREJECTED);

                string englishMessage = string.Format(Messages.CHEQUEBOUNCED,
                    paymentCollection.CustomerCode, 
                    String.Format("{0:dd/MM/yyyy}", paymentCollection.PC_ReceiptDate),
                    paymentCollection.PC_Id, paymentCollection.PC_Amount,
                    paymentCollection.PC_InstrumentNo);
                
                SmsUtility.SendSMSForBookings(paymentCollection.PC_MobileNumber, englishMessage);
            }
        }

        //return to cheque activation screen
        Event_ShowChequeActivationScreen(sender);
    }

    private bool CheckPageValidity()
    {
        bool result = true;

        if (ddlChequeStatus.SelectedItem.Value == "3" && ddlRejectionReason.SelectedItem.Value == "0")
        {
            customValidator.IsValid = false;
            ucMessageBox.ShowMessage(ErrorMessages.RequiredRejectionReason);
            result = false;
        }
        else if (ddlChequeStatus.SelectedItem.Value == "1")
        {
            decimal chequeAmountDifference = Convert.ToDecimal(ConfigurationManager.AppSettings["ChequeAmountDifference"]);
            
            if (ddlBank.SelectedItem.Value == "0")
            {
                customValidator.IsValid = false;
                ucMessageBox.ShowMessage(ErrorMessages.RequiredBankName);
                result = false;
            }
            else if (string.IsNullOrEmpty(txtDateOfCredit.Text))
            {
                customValidator.IsValid = false;
                ucMessageBox.ShowMessage(ErrorMessages.REQUIREDDATEOFCREDIT);
                result = false;
            }
            else if (string.IsNullOrEmpty(txtAmountCredited.Text))
            {
                customValidator.IsValid = false;
                ucMessageBox.ShowMessage(ErrorMessages.REQUIREDAMOUNTCREDITED);
                result = false;
            }
            else if (Convert.ToDecimal(txtAmountCredited.Text) > Convert.ToDecimal(txtAmount.Text))
            {
                customValidator.IsValid = false;
                ucMessageBox.ShowMessage(ErrorMessages.AMOUNTCREDITEDMUSTBELESSCHEQUEAMOUNT);
                result = false;
            }
            else if (Convert.ToDateTime(txtDateOfCredit.Text) < Convert.ToDateTime(ViewState[Globals.StateMgmtVariables.INSTRUMENTTYPE]))
            {
                customValidator.IsValid = false;
                ucMessageBox.ShowMessage(ErrorMessages.IncorrectDateOfCredit);
                result = false;
            }
            else if ((Convert.ToDecimal(txtAmount.Text) - Convert.ToDecimal(txtAmountCredited.Text)) > chequeAmountDifference) 
            {
                customValidator.IsValid = false;
                ucMessageBox.ShowMessage(string.Format(ErrorMessages.InvalidAmountCreditedFigure, chequeAmountDifference.ToString()));
                result = false;
            }
        }            
        return result;
    }

    private void ResetControls()
    {
        ddlChequeStatus.SelectedValue = "0";
        ddlBank.SelectedValue = "0";
        ddlRejectionReason.SelectedValue = "0";
        txtDateOfCredit.Text = string.Empty;
        txtAmountCredited.Text = string.Empty;
    }

    protected void ddlChequeStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlChequeStatus.SelectedValue) ==
            (int)Globals.ChequeStatus.REJECTED)
        {
            ddlRejectionReason.Enabled = true;
        }
        else
        {
            ddlRejectionReason.Enabled = false;
        }
    }
}