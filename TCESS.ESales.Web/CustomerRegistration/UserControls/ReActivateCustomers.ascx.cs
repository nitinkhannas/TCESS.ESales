#region Using directives

using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using System.Collections;
using Resources;
using TCESS.ESales.CommonLayer.Exception;

#endregion

public partial class CustomerRegistration_UserControls_ReActivateCustomers : BaseUserControl
{
        protected void Page_Init(object sender, EventArgs e)
        {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Get list of customer for activation and code allotement
            GetCustomersForActivation();
        }
    }

    /// <summary>
    /// Get list of customer for activation and code allotement
    /// </summary>
    private void GetCustomersForActivation()
    {
        IList<CustomerDTO> lstCustomer = new List<CustomerDTO>();
        lstCustomer = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetReValidatedCustomersByCustomer();

        if (lstCustomer.Count > 0)
        {
            grdActivateCustomers.DataSource = lstCustomer;
            grdActivateCustomers.DataBind();
        }
        else
        {
            ShowBlankGrid();
        }
    }

    private void ShowBlankGrid()
    {
        base.ShowBlankRowInGrid<CustomerDTO>(grdActivateCustomers);
    }

    protected void grdActivateCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == Globals.GridCommandEvents.VIEW)
        {
            Session[Globals.StateMgmtVariables.VIEWCUSTOMERSOURCE] = 1;
            Session[Globals.StateMgmtVariables.CUSTOMERID] = e.CommandArgument;
            Response.Redirect("ViewCustomerDetails.aspx");
        }
    }

    protected void grdActivateCustomers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(e.NewEditIndex);
    }

    protected void grdActivateCustomers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1);
    }

    protected void grdActivateCustomers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            ExceptionHandler.AppExceptionManager.Process(() =>
            {
                int customerId = Convert.ToInt32(grdActivateCustomers.DataKeys[e.RowIndex]["Cust_ID"]);
                
                CustomerDTO customerDetails = MasterList.GetCustomerDetailsById(customerId);

                customerDetails.Cust_LastUpdatedDate = DateTime.Now;
                customerDetails.Cust_RegCustType = true;
                customerDetails.Cust_Status = true;

                //Gets the currently selected DCA id from dropdown
                int allotedQuantity = Convert.ToInt32(((DropDownList)grdActivateCustomers.Rows[e.RowIndex].FindControl("ddlAllotedQty")).SelectedValue);

                //Get customer material details by customer id
                IList<CustomerMaterialMapDTO> listCustMatDetails = ESalesUnityContainer.Container.Resolve<ICustomerMaterialService>()
                    .GetCustomerMaterialDetailsByCustomerId(customerId);

                (from custMaterial in listCustMatDetails select custMaterial).Update(
                            custMaterial => custMaterial.Cust_Mat_AllotedQuantityId = allotedQuantity);

                //To update customer details with customer code
                ESalesUnityContainer.Container.Resolve<ICustomerService>().SaveAndUpdateCustomerDetails(customerDetails, listCustMatDetails);

                //Create message for code allottment to be sent to customer
               // string englishMessage = "Apke Unit " + customerDetails.Cust_TradeName + " ka Code Number " + ((TextBox)grdActivateCustomers.Rows[e.RowIndex].FindControl("txtCustomerCode")).Text + " hai. SMS Booking Sewa shuru ki ja rahi hai. Hamara sales office aapse jald hi sampark karega. DCA GHATO";

                //Sends the message to customer
                //SmsUtility.SendSMS(customerDetails.Cust_MobileNo, englishMessage);
                ucMessageBoxForGrid.ShowMessage("Customer re-activated");
            }, Globals.ExceptionTypes.ExceptionShielding.ToString());
        }
        catch (Exception ex)
        {
        }
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1);
    }

    /// <summary>
    /// Row edit/update/cancel function for grid view
    /// </summary>
    /// <param name="rowIndex">rowIndex of gridview</param>
    private void GridViewRowUpdateFunctions(int rowIndex)
    {
        grdActivateCustomers.EditIndex = rowIndex;

        //Get list of customer for activation and code allotement
        GetCustomersForActivation();
    }

    protected void grdActivateCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grdActivateCustomers.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
        {
            //Gets list of alloted quantity
            MasterList.GetAllotedQuantityInDropDown((DropDownList)e.Row.FindControl("ddlAllotedQty"));
        }
    }
}