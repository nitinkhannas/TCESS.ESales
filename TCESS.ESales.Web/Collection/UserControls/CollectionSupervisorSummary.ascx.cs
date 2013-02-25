#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces.GhatoCollection;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects.GhatoCollection;

#endregion

public partial class Collection_UserControls_CollectionSupervisorSummary : BaseUserControl
{
    public void SetCollectionModeForSupervisor(int paymentModeId, int paymentHeaderFor)
    {
        ViewState[Globals.StateMgmtVariables.PAYMENTMODE] = paymentModeId;
        ViewState[Globals.StateMgmtVariables.PAYMENTHEADERFOR] = paymentHeaderFor;

        if ((int)HelperClass.PaymentModes.CHEQUE == paymentModeId)
        {
            divCollection.Visible = true;            
            divAfterCollection.Visible = true;
            lblTotalAmount.Text = Labels.TotalAmountCollected;
            lblCashInHand.Text = Labels.AMOUNTINHAND;
            
            GetCollectionSummary();
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ////Load payment details to transit to head cashier
            LoadBatchDetails();

            ////Fill bank name dropdown from database
            FillDropdownForBanks();
        }

        ////Load payment transit details to show header values
        LoadHeaderDetails();
    }

    protected void btnActivatePayment_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            BatchTransferDTO batchTransferDTO = ESalesUnityContainer.Container.Resolve<IPaymentService>()
                .GetBatchByBatchId(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.BATCHID]));

            batchTransferDTO.BT_ApprovedBy = GetCurrentUserId();
            batchTransferDTO.BT_ApprovedDate = DateTime.Now;
            batchTransferDTO.BT_Status = (int)Globals.BatchIdentity.FORSUPERVISORSCREEN;
            //batchTransferDTO.BT_BankName = Convert.ToInt32(ddlBankName.SelectedItem.Value);
            //batchTransferDTO.BT_BankBranch = txtBranchName.Text.Trim();

            ESalesUnityContainer.Container.Resolve<IPaymentService>().UpdateBatchDetails(batchTransferDTO);

            ////Shows message box to user
            ucMessageBoxForGrid.ShowMessage(Messages.PAYMENTAPPROVEDANDACTIVATED);

            ResetControls();

            ////Load payment details to transit to head cashier
            LoadBatchDetails();
        }
    }

    protected IEnumerable grdBatchPayments_MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<BatchTransferDTO>();
    }

    protected void grdBatchPayments_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdBatchPayments.PageIndex = e.NewPageIndex;

        //Load Batch details from database
        LoadBatchDetails();
    }

    protected void grdBatchPayments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == Globals.GridCommandEvents.VIEW)
        {
            ViewState[Globals.StateMgmtVariables.BATCHID] = e.CommandArgument;

            ////Bind all associated payment collections with a specific batch.
            PopulatePaymentCollections();
        }
    }

    protected IEnumerable grdPaymentDetails_MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<PaymentCollectionDTO>();
    }

    protected IEnumerable grdCollection_MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<PaymentCollectionDTO>();
    }

    private void ResetControls()
    {
        ddlBankName.SelectedValue = "0";
        txtBranchName.Text = string.Empty;
        divPaymentCollectionLabel.Visible = false;
        lblBatchId.Text = string.Empty;
    }

    private void LoadBatchDetails()
    {
        int paymentMode = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.PAYMENTMODE]);
        IList<BatchTransferDTO> lstBatchTransfer = ESalesUnityContainer.Container.
            Resolve<IPaymentService>().GetBatchDetails(null, paymentMode);

        if (lstBatchTransfer.Count > 0)
        {
            grdBatchPayments.DataSource = lstBatchTransfer;
            grdBatchPayments.DataBind();
        }
        else
        {
            base.ShowBlankRowInGrid<BatchTransferDTO>(grdBatchPayments);
        }

        base.ShowBlankRowInGrid<PaymentCollectionDTO>(grdPaymentDetails);
    }

    /// <summary>
    /// Bind all associated payment collections with a specific batch.
    /// </summary>
    private void PopulatePaymentCollections()
    {
        int batchId = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.BATCHID]);

        IList<PaymentCollectionDTO> lstCustomers = ESalesUnityContainer.Container.Resolve<IPaymentService>()
            .GetCollectionDetailsFromBatchId(batchId);

        // Displays Payment collections with selected Batch Id.
        if (lstCustomers.Count > 0)
        {
            divPaymentCollectionLabel.Visible = true;

            lblBatchId.Text = batchId.ToString();

            grdPaymentDetails.DataSource = lstCustomers;
            grdPaymentDetails.DataBind();
        }
        else
        {
            divPaymentCollectionLabel.Visible = false;
            lblBatchId.Text = string.Empty;
            base.ShowBlankRowInGrid<PaymentCollectionDTO>(grdPaymentDetails);
        }
    }

    /// <summary>
    /// Load payment transit details to show header values
    /// </summary>
    private void LoadHeaderDetails()
    {
        IList<object> lstHeader = ESalesUnityContainer.Container.Resolve<IPaymentService>()
            .GetCollectionHeaderForReports(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.PAYMENTMODE]),
            Convert.ToInt32(ViewState[Globals.StateMgmtVariables.PAYMENTHEADERFOR]));

        lblCashInHandAmount.Text = Convert.ToString(lstHeader[0]);
        lblInTransitAmount.Text = Convert.ToString(lstHeader[1]);
        lblTransferredAmount.Text = Convert.ToString(lstHeader[2]);
        lblTotalAmountCollected.Text = Convert.ToString(lstHeader[3]);
        lblTotalTransactions.Text = Convert.ToString(lstHeader[4]);
        lblPendingTransactionsAtCounter.Text = Convert.ToString(lstHeader[5]);
    }

    /// <summary>
    /// Fill bank name dropdown from database
    /// </summary>
    private void FillDropdownForBanks()
    {
        MasterList.FillDropdownForBanks(ddlBankName);
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1);
    }

    /// <summary>
    /// Row edit/update/cancel function for grid view
    /// </summary>
    /// <param name="rowIndex"></param>
    private void GridViewRowUpdateFunctions(int rowIndex)
    {
        grdBatchPayments.EditIndex = rowIndex;

        ////Load payment details to transit to head cashier
        LoadBatchDetails();
    }

    private void GetCollectionSummary()
    {
        IList<CollectionSummaryDTO> lstCollectionSummary = ESalesUnityContainer.Container
            .Resolve<IPaymentService>().GetCollectionSummaryDetails();

        // Displays Payment collections with selected Batch Id.
        if (lstCollectionSummary.Count > 0)
        {
            grdCollection.DataSource = lstCollectionSummary;
            grdCollection.DataBind();
        }
        else
        {
            base.ShowBlankRowInGrid<PaymentCollectionDTO>(grdCollection);
        }
    }
}