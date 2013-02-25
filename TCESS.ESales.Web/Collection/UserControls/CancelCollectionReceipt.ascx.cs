#region Using directives

using System;
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

public partial class Collection_UserControls_CancelCollectionReceipt : BaseUserControl
{
    public event ShowCollectionEventHandler Event_ShowCollectionScreen;

    public void ShowCollectionDetailsForCancellation()
    {
        LoadCollectionDetails(txtSearchValue.Text.Trim());
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Show blank row in grid
            ShowBlankRowInGrid<PaymentCollectionDTO>(grdCancelCollection);
        }
    }

    protected void grdCancelCollection_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCancelCollection.PageIndex = e.NewPageIndex;

        //Get payment details from database filtered on search criteria
        LoadCollectionDetails(string.Empty);
    }

    protected void grdCancelCollection_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(e.NewEditIndex, txtSearchValue.Text.Trim());
    }

    protected void grdCancelCollection_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1, txtSearchValue.Text.Trim());
    }

    protected void grdCancelCollection_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SetVisibilityOfActionModes(e.Row);
        }
    }

    protected void grdCancelCollection_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == Globals.GridCommandEvents.REISSUE)
        {
            GridViewRow row = ((GridViewRow)((Control)e.CommandSource).NamingContainer);

            Event_ShowCollectionScreen(Convert.ToInt32(e.CommandArgument),
               Convert.ToInt32(grdCancelCollection.DataKeys[row.RowIndex][1]),
               Convert.ToInt32(grdCancelCollection.DataKeys[row.RowIndex][2]));
        }
    }

    protected void grdCancelCollection_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (Page.IsValid)
        {
            PaymentCollectionDTO paymentCollection = ESalesUnityContainer.Container.Resolve<IPaymentService>()
              .GetCollectionDetailsById(Convert.ToInt32(grdCancelCollection.DataKeys[e.RowIndex]["PC_Id"]));

            GridViewRow currentRow = grdCancelCollection.Rows[e.RowIndex];

            paymentCollection.PC_Status = (int)Globals.CollectionStatus.CANCELLED;
            paymentCollection.PC_Remark = GetTextBoxValue(currentRow, "txtReason");
            paymentCollection.PC_LastUpdateDate = DateTime.Now;
            paymentCollection.PC_LastUpdatedBy = base.GetCurrentUserId();
            paymentCollection.PC_IsDeleted = true;
           
            //To update payment collection details
            ESalesUnityContainer.Container.Resolve<IPaymentService>().SaveOrUpdateCollection(paymentCollection);
            ucMessageBoxForGrid.ShowMessage(Messages.RECEIPTCANCELLEDSUCCESSFULLY);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadCollectionDetails(txtSearchValue.Text.Trim());
    }

    /// <summary>
    /// Get textbox value for the current row
    /// </summary>
    /// <param name="currentRow">current row which need to searched</param>
    /// <param name="controlName">control name to retrieve value</param>
    /// <returns>retrieves value of the textbox</returns>
    private string GetTextBoxValue(GridViewRow currentRow, string controlName)
    {
        string textBoxValue = string.Empty;
        if (currentRow.FindControl(controlName).Visible == true)
        {
            textBoxValue = ((TextBox)currentRow.FindControl(controlName)).Text;
        }
        return textBoxValue;
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1, string.Empty);
    }

    private void GridViewRowUpdateFunctions(int rowIndex, string searchCriteria)
    {
        grdCancelCollection.EditIndex = rowIndex;

        //Get payment details from database filtered on search criteria
        LoadCollectionDetails(searchCriteria);
    }

    private void LoadCollectionDetails(string searchCriteria)
    {
        Nullable<int> userId = base.GetCurrentUserId();
        
        //Check if user is superadmin
        if (userId == (int)HelperClass.UserType.SUPERADMIN)
        {
            userId = null;
        }

        IList<PaymentCollectionDTO> lstPaymentCollection = ESalesUnityContainer.Container.
            Resolve<IPaymentService>().GetCollectionDetailsForCancelAndReIssue(txtSearchValue.Text.Trim(), 
            GetSearchType(), userId);

        if (lstPaymentCollection.Count > 0)
        {
            grdCancelCollection.DataSource = lstPaymentCollection;
            grdCancelCollection.DataBind();
        }
        else
        {
            ShowBlankRowInGrid<PaymentCollectionDTO>(grdCancelCollection);
        }
    }

    private bool GetSearchType()
    {
        bool searchType = false;
        if (ddlSearchType.SelectedItem.Value == "2")
        {
            searchType = true;
        }

        return searchType;
    }

    private void SetVisibilityOfActionModes(GridViewRow currentRow)
    {
        PaymentCollectionDTO collectionItem = (PaymentCollectionDTO)currentRow.DataItem;

        if (collectionItem.PC_Status == (int)Globals.CollectionStatus.CANCELLED)
        {
            SetVisibilityForReIssueButton(currentRow, true);
            SetVisibilityForCancelButton(currentRow, false);
        }
        else
        {
            SetVisibilityForCancelButton(currentRow, true);
            SetVisibilityForReIssueButton(currentRow, false);
        }
    }

    private void SetVisibilityForReIssueButton(GridViewRow currentRow, bool visibility)
    {   
        if (currentRow.FindControl("lnkReIssue") != null)
        {
            currentRow.FindControl("lnkReIssue").Visible = visibility;
        }
    }

    private void SetVisibilityForCancelButton(GridViewRow currentRow, bool visibility)
    {
        if (currentRow.FindControl("lnkEdit") != null)
        {
            currentRow.FindControl("lnkEdit").Visible = visibility;
        }
    }
}