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

public partial class Collection_DayCollectionCounterSummary : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            base.CheckIsUserAuthenticated();

            ////Load payment details to transit to head cashier
            LoadPaymentDetails();

            ////Load batch details for all transferred batches
            LoadBatchDetails();

            ////Load payment transit details to show header values
            LoadPaymentTransitDetails();
        }
    }

    /// <summary>
    /// To collect the money receipt
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (CheckSelectedGridRows())
        {
            customValidator.IsValid = false;
            ucMessageBoxForGrid.ShowMessage(ErrorMessages.NOROWSSELECTED);
            return;
        }

        if (Page.IsValid)
        {
            BatchTransferDTO batchTransferDTO = new BatchTransferDTO();
            batchTransferDTO.PaymentTransits = new List<PaymentTransitDTO>();

            //Iterate through the Products.Rows property
            foreach (GridViewRow row in grdPaymentTransit.Rows)
            {
                PaymentTransitDTO paymentTransitDTO = new PaymentTransitDTO();

                //Access the CheckBox
                CheckBox chkBox = (CheckBox)row.FindControl("chkItem");
                if (chkBox.Checked)
                {
                    paymentTransitDTO.PaymentTransit_CollectionId = Convert.ToInt32(grdPaymentTransit.DataKeys[row.RowIndex]["PC_Id"]);
                    paymentTransitDTO.PaymentTransit_CreatedBy = base.GetCurrentUserId();

                    batchTransferDTO.PaymentTransits.Add(paymentTransitDTO);
                }                
            }

            batchTransferDTO.BT_CreatedBy = GetCurrentUserId();
            batchTransferDTO.BT_Status = (int)HelperClass.BatchStatus.PENDING;

            //Saves payment collection details in database
            ESalesUnityContainer.Container.Resolve<IPaymentService>().SendPaymentToHeadCashier(batchTransferDTO);

            ResetControls();

            //Shows message box to user
            ucMessageBoxForGrid.ShowMessage(Messages.PAYMENTSENTTOHEADCASHIER);

            ////Load payment transit details to show header values
            LoadPaymentTransitDetails();

            ////Load batch details for all transferred batches
            LoadBatchDetails();
        }
    }

    protected void chkHeader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)grdPaymentTransit.HeaderRow.FindControl("chkHeader");
        decimal selectedAmount = Convert.ToDecimal(ViewState["AmountToTransit"]);
        int selectedRows = Convert.ToInt32(ViewState["Count"]);
        int rowCount = 0;
        decimal amount = 0;

        foreach (GridViewRow row in grdPaymentTransit.Rows)
        {
            if (chkAll.Checked == true)
            {
                selectedAmount = 0;
                selectedRows = 0;

                ((CheckBox)row.FindControl("chkItem")).Checked = true;
                amount += Convert.ToDecimal(((Label)row.FindControl("lblAmount")).Text);
                rowCount += 1;
            }
            else
            {
                ((CheckBox)row.FindControl("chkItem")).Checked = false;
                selectedAmount -= Convert.ToDecimal(((Label)row.FindControl("lblAmount")).Text);
                selectedRows -= 1;
            }
        }

        if (selectedAmount > 0)
        {
            amount = selectedAmount;
            rowCount = selectedRows;
        }

        ViewState["Count"] = rowCount;
        SetAmountToTransitValue(amount);
    }

    protected void chkItem_CheckedChanged(object sender, EventArgs e)
    {
        ////Gets amount to transit from viewstate
        decimal amount = Convert.ToDecimal(ViewState["AmountToTransit"]);
        int count = Convert.ToInt32(ViewState["Count"]);

        CheckBox chkItem = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chkItem.NamingContainer;

        CheckBox chkHeader = (CheckBox)grdPaymentTransit.HeaderRow.FindControl("chkHeader");

        ////If checkbox in gridview row is checked  
        if (chkItem.Checked)
        {
            amount += Convert.ToDecimal(((Label)row.FindControl("lblAmount")).Text);
            ViewState["Count"] = count + 1;
            if (Convert.ToInt32(ViewState["Count"]) == grdPaymentTransit.Rows.Count)
            {
                chkHeader.Checked = true;
            }
        }
        else
        {
            amount -= Convert.ToDecimal(((Label)row.FindControl("lblAmount")).Text);
            chkHeader.Checked = false;
            ViewState["Count"] = count - 1;
        }
        SetAmountToTransitValue(amount);
    }

    protected IEnumerable grdPaymentTransit_MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<PaymentCollectionDTO>();
    }

    protected void grdPaymentTransit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPaymentTransit.PageIndex = e.NewPageIndex;

        ////Load payment details to transit to head cashier
        LoadPaymentDetails();
    }

    protected void grdPaymentTransit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox headerChk = (CheckBox)grdPaymentTransit.HeaderRow.FindControl("chkTransitPaymentHeader");
            CheckBox childChk = (CheckBox)e.Row.FindControl("chkTransitPayment");
        }
    }

    protected IEnumerable grdBatchPayments_MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<BatchTransferDTO>();
    }

    protected void grdBatchPayments_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdBatchPayments.PageIndex = e.NewPageIndex;

        ////Load batch details for all transferred batches
        LoadBatchDetails();
    }

    private void ResetControls()
    {
        txtAmountToTransit.Text = string.Empty;
        txtComments.Text = string.Empty;
        ViewState["AmountToTransit"] = null; 
    }

    private bool CheckSelectedGridRows()
    {
        bool counter = false;
        decimal selectedAmount = 0;

        //Iterate through the Payment Transit grid rows
        foreach (GridViewRow row in grdPaymentTransit.Rows)
        {
            //Access the CheckBox
            CheckBox chkBox = (CheckBox)row.FindControl("chkItem");
            if (chkBox.Checked)
            {
                selectedAmount += Convert.ToDecimal(((Label)grdPaymentTransit.Rows[row.RowIndex].FindControl("lblAmount")).Text);                
            }
        }

        if (selectedAmount == 0)
        {
            counter = true;
        }
        return counter;
    }

    /// <summary>
    /// Load payment details to transit to head cashier
    /// </summary>
    private void LoadPaymentDetails()
    {
        IList<PaymentCollectionDTO> lstPaymentCollectionDTO = ESalesUnityContainer.Container.Resolve<IPaymentService>()
            .GetCollectionDetails(string.Empty, null, base.GetCurrentUserId());

        if (lstPaymentCollectionDTO.Count > 0)
        {
            grdPaymentTransit.DataSource = lstPaymentCollectionDTO;
            grdPaymentTransit.DataBind();
        }
        else
        {
            base.ShowBlankRowInGrid<PaymentCollectionDTO>(grdPaymentTransit);
        }
    }
    
    /// <summary>
    /// Load payment transit details to show header values
    /// </summary>
    private void LoadPaymentTransitDetails()
    {
        IList<object> lstPaymentTransit = ESalesUnityContainer.Container.Resolve<IPaymentService>().
            GetCollectionHeaderForTransit(false, base.GetCurrentUserId());
        lblCashInHandAmount.Text = Convert.ToString(lstPaymentTransit[0]);
        lblInTransitAmount.Text = Convert.ToString(lstPaymentTransit[1]);
        lblTransferredAmount.Text = Convert.ToString(lstPaymentTransit[2]);
        lblTotalAmountCollected.Text = Convert.ToString(lstPaymentTransit[3]);
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
        grdPaymentTransit.EditIndex = rowIndex;
                
        ////Load payment details to transit to head cashier
        LoadPaymentDetails();
    }

    private void LoadBatchDetails()
    {
        Nullable<int> userId = base.GetCurrentUserId();
        
        //Checks If user is type of superadmin
        userId = userId == 1 ? null : userId;

        IList<BatchTransferDTO> lstBatchTransfer = ESalesUnityContainer.Container.
            Resolve<IPaymentService>().GetBatchDetailsForCollectionScreen(userId, null);

        if (lstBatchTransfer.Count > 0)
        {
            grdBatchPayments.DataSource = lstBatchTransfer;
            grdBatchPayments.DataBind();
        }
        else
        {
            base.ShowBlankRowInGrid<BatchTransferDTO>(grdBatchPayments);
        }
    }

    private void SetAmountToTransitValue(decimal amount)
    {
        ViewState["AmountToTransit"] = amount;        

        ////If amount is greater than zero
        if (amount > 0)
        {
            txtAmountToTransit.Text = amount.ToString();
        }
        else
        {
            txtAmountToTransit.Text = string.Empty;
        }
    }
}