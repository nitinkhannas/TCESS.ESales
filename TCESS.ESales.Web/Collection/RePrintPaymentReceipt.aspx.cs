#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces.GhatoCollection;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects.GhatoCollection;

#endregion

public partial class GhatoCollection_RePrintPaymentReceipt : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
        ucPaymentReceipt.Event_CloseScreen += ucPaymentReceipt_Event_CloseScreen;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Checks if user is authenticated to view the page
            base.CheckIsUserAuthenticated();

            //Shows default panel of print receipt
            PnlPaymentReceipt.Visible = false;
            pnlGrid.Visible = true;

            //Show blank row in grid
            ShowBlankRowInGrid<PaymentCollectionDTO>(grdRePrint);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            ////Load cheque details based on the filter criteria
            LoadCollectionDetails(txtSearchValue.Text.Trim());
        }
    }

    protected IEnumerable grdRePrint_MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<PaymentCollectionDTO>();
    }

    protected void grdRePrint_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdRePrint.PageIndex = e.NewPageIndex;

        //Get payment details from database filtered on search criteria
        LoadCollectionDetails(string.Empty);
    }

    protected void grdRePrint_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == Globals.GridCommandEvents.PRINT)
        {
            PnlPaymentReceipt.Visible = true;
            pnlGrid.Visible = false;
            ucPaymentReceipt.GetPaymentDetails(Convert.ToInt32(e.CommandArgument), true);

            PaymentCollectionDTO paymentCollection = ESalesUnityContainer.Container.
            Resolve<IPaymentService>().GetCollectionDetailsById(Convert.ToInt32(e.CommandArgument));
        }
    }

    protected void grdRePrint_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Gets the value of reprint count from grid 
            string rePrintColumn = ((Label)e.Row.FindControl("lblRePrintCount")).Text;
            int rePrintValue = rePrintColumn == string.Empty ? 0 : Convert.ToInt32(rePrintColumn);
            
            //Checks if reprint count is greater than reprint configuration value 
            if (Convert.ToInt32(rePrintValue) >= 
                Convert.ToInt32(ConfigurationManager.AppSettings["MaxPrintCount"]))
            {
                //Sets link button as enabled or disabled 
                ((LinkButton)e.Row.FindControl("lnkPrint")).Enabled = false;
            }
        }
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
        grdRePrint.EditIndex = rowIndex;

        //Get payment details from database filtered on search criteria
        LoadCollectionDetails(searchCriteria);
    }

    /// <summary>
    /// Get payment details from database filtered on search criteria
    /// </summary>
    private void LoadCollectionDetails(string searchCriteria)
    {
        Nullable<int> userId = base.GetCurrentUserId();
        int maxPrintCount = Convert.ToInt32(ConfigurationManager.AppSettings["MaxPrintCount"]);

        //Check if user is superadmin
        if (userId == (int)HelperClass.UserType.SUPERADMIN)
        {
            userId = null;
        }

        IList<PaymentCollectionDTO> lstPaymentCollection = ESalesUnityContainer.Container.
            Resolve<IPaymentService>().GetCollectionDetailsForPrint(txtSearchValue.Text.Trim(), 
            GetSearchType(), userId);

        if (lstPaymentCollection.Count > 0)
        {
            grdRePrint.DataSource = lstPaymentCollection;
            grdRePrint.DataBind();
        }
        else
        {
            ShowBlankRowInGrid<PaymentCollectionDTO>(grdRePrint);
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

    private void ucPaymentReceipt_Event_CloseScreen(object sender)
    {
        PnlPaymentReceipt.Visible = false;
        pnlGrid.Visible = true;

        ////Load cheque details based on the filter criteria
        LoadCollectionDetails(txtSearchValue.Text.Trim());
    }
}