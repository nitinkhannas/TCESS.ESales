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

public partial class GhatoCollection_UserControls_ManagePaymentCollection : BaseUserControl
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Show blank row in grid
            ShowBlankRowInGrid<PaymentCollectionDTO>(grdManagePayments);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            ////Load cheque details based on the filter criteria
            LoadPaymentDetails(txtSearchValue.Text.Trim());
        }
    }

    protected IEnumerable grdManagePayments_MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<PaymentCollectionDTO>();
    }

    protected void grdManagePayments_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdManagePayments.PageIndex = e.NewPageIndex;

        //Get payment details from database filtered on search criteria
        LoadPaymentDetails(string.Empty);
    }

    protected void grdManagePayments_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(e.NewEditIndex, txtSearchValue.Text.Trim());
    }

    protected void grdManagePayments_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1, txtSearchValue.Text.Trim());
    }

    protected void grdManagePayments_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (Page.IsValid)
        {
            PaymentCollectionDTO paymentCollection = ESalesUnityContainer.Container.Resolve<IPaymentService>()
                .GetCollectionDetailsById(Convert.ToInt32(grdManagePayments.DataKeys[e.RowIndex].Value));
            
            GridViewRow currentRow = grdManagePayments.Rows[e.RowIndex];

            paymentCollection.PC_InstrumentNo = GetTextBoxValue(currentRow, "txtInstrumentNumber");
            string instrumentDate = GetTextBoxValue(currentRow, "txtInstrumentDate");
            paymentCollection.PC_InstrumentDate = string.IsNullOrEmpty(instrumentDate) ? (DateTime?)null : Convert.ToDateTime(instrumentDate);
            paymentCollection.PC_BankDrawn = GetDropDownValue(currentRow, "ddlBankDrawn");
            paymentCollection.PC_Amount = Convert.ToDecimal(GetTextBoxValue(currentRow, "txtAmount"));
            
            //To update payment collection details
            ESalesUnityContainer.Container.Resolve<IPaymentService>().SaveOrUpdateCollection(paymentCollection);
            ucMessageBoxForGrid.ShowMessage(Messages.PAYMENTUPDATEDSUCCESSFULLY);
        }
    }

    protected void grdManagePayments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // To populate Bank dropdowm list for update
        if (grdManagePayments.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToInt32(grdManagePayments.DataKeys[e.Row.RowIndex]["PC_BankDrawn"]) == 0)
            {
                e.Row.FindControl("txtInstrumentNumber").Visible = false;
                e.Row.FindControl("txtInstrumentDate").Visible = false;
                e.Row.FindControl("ddlBankDrawn").Visible = false;
                e.Row.FindControl("txtBankBranch").Visible = false;                
            }
            else
            {
                DropDownList ddlBankDrawn = (DropDownList)e.Row.FindControl("ddlBankDrawn");
                MasterList.FillDropdownForBanks(ddlBankDrawn);

                //To set BankId in dropdown box
                ddlBankDrawn.SelectedValue = grdManagePayments.DataKeys[e.Row.RowIndex]["PC_BankDrawn"].ToString();
            }
        }
    }

    private string GetTextBoxValue(GridViewRow currentRow, string controlName)
    {
        string textBoxValue = string.Empty;
        if (currentRow.FindControl(controlName).Visible == true)
        {
            textBoxValue = ((TextBox)currentRow.FindControl(controlName)).Text;
        }

        return textBoxValue;
    }

    private int GetDropDownValue(GridViewRow currentRow, string controlName)
    {
        int dropDownValue = 0;
        if (currentRow.FindControl(controlName).Visible == true)
        {
            dropDownValue = Convert.ToInt32(((DropDownList)currentRow.FindControl(controlName)).SelectedItem.Value);
        }

        return dropDownValue;
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1, string.Empty);
    }

    /// <summary>
    /// Row edit/update/cancel function for grid view
    /// </summary>
    /// <param name="rowIndex"></param>
    private void GridViewRowUpdateFunctions(int rowIndex, string searchCriteria)
    {
        grdManagePayments.EditIndex = rowIndex;

        //Get payment details from database filtered on search criteria
        LoadPaymentDetails(searchCriteria);
    }

    /// <summary>
    /// Get payment details from database filtered on search criteria
    /// </summary>
    private void LoadPaymentDetails(string searchCriteria)
    {
        IList<PaymentCollectionDTO> lstPaymentCollection = null;
        Nullable<int> userId = 0;

        if (Convert.ToInt32(ViewState[Globals.StateMgmtVariables.ACTIONMODEFORMANAGEPAYMENTS]) == (int)HelperClass.ActionModeForManagePayments.EDIT)
        {
            userId = null;
        }
        else
        {
            userId = base.GetCurrentUserId();
            
            //Check if user is superadmin
            if (userId == 1)
            {
                userId = null;
            }
        }

            lstPaymentCollection = ESalesUnityContainer.Container.Resolve<IPaymentService>()
            .GetCollectionDetails(txtSearchValue.Text.Trim(), GetSearchType(), userId);
        
        if (lstPaymentCollection.Count > 0)
        {
            grdManagePayments.DataSource = lstPaymentCollection;
            grdManagePayments.DataBind();
        }
        else
        {
            ShowBlankRowInGrid<PaymentCollectionDTO>(grdManagePayments);
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
}