#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.DataTransferObjects.Masters;
using TCESS.ESales.BusinessLayer.Interfaces.Masters;

#endregion

public partial class Masters_Bank : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Get bank from database
            GetBank();
        }
    }

    /// <summary>
    /// Get business type from database
    /// </summary>
    private void GetBank()
    {
        IList<BankDTO> lstBanks = ESalesUnityContainer.Container.Resolve<IMasterService>().GetBankDetails();

        if (lstBanks.Count > 0)
        {
            grdBank.DataSource = lstBanks;
            grdBank.DataBind();
        }
        else
        {
            ShowBlankRowInGrid<BankDTO>(grdBank);
        }
    }

    protected IEnumerable grdBank_MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<BusinessTypeDTO>();
    }

    protected void grdBank_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdBank.PageIndex = e.NewPageIndex;

        //Get business type from database
        GetBank();
    }

    protected void grdBank_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
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
        grdBank.EditIndex = rowIndex;

        //Get business type from database
        GetBank();
    }

    protected void grdBank_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == Globals.GridCommandEvents.ADDNEW)
        {
            if (Page.IsValid)
            {
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                BankDTO bankDetails = new BankDTO();
                bankDetails.Bank_Name = ((TextBox)row.FindControl("txtNewBankName")).Text;
                bankDetails.Bank_AccountNo = ((TextBox)row.FindControl("txtNewBankAccountNo")).Text;
                bankDetails.Bank_LastUpdatedDate = DateTime.Now;
                bankDetails.Bank_CreatedBy = GetCurrentUserId();
                
                ESalesUnityContainer.Container.Resolve<IMasterService>().SaveAndUpdateBankDetails(bankDetails);
                ucMessageBoxForGrid.ShowMessage("Bank details saved successfully");
            }
        }
    }

    protected void grdBank_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ESalesUnityContainer.Container.Resolve<IMasterService>()
            .DeleteBank(Convert.ToInt32(grdBank.DataKeys[e.RowIndex].Value));
        ucMessageBoxForGrid.ShowMessage("Bank details deleted successfully");
    }

    protected void grdBank_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(e.NewEditIndex);
    }

    protected void grdBank_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (Page.IsValid)
        {
            BankDTO bankDetails = new BankDTO();
            bankDetails = ESalesUnityContainer.Container.Resolve<IMasterService>()
                .GetBanksDetailsById(Convert.ToInt32(grdBank.DataKeys[e.RowIndex].Value));

            bankDetails.Bank_Name = ((TextBox)grdBank.Rows[e.RowIndex].FindControl("txtBankName")).Text;
            bankDetails.Bank_AccountNo = ((TextBox)grdBank.Rows[e.RowIndex].FindControl("txtBankAccountNo")).Text;
            bankDetails.Bank_LastUpdatedDate = DateTime.Now;
            bankDetails.Bank_CreatedBy = GetCurrentUserId();           

            //To update business type
            ESalesUnityContainer.Container.Resolve<IMasterService>().SaveAndUpdateBankDetails(bankDetails);
            ucMessageBoxForGrid.ShowMessage("Bank details updated successfully");
        }  
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1);
    }
}