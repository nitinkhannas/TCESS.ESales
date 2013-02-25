#region Namespace

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using System.Collections;
using System.Web.Security;
using TCESS.ESales.CommonLayer.CommonLibrary;

#endregion

public partial class Supervisor_LiftingRestrictions : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckIsUserAuthenticated();

        if (!IsPostBack)
        {
            BindMaterialType();

            //Populate Customer with alloted quantity
            PopulateAllotedQuantity(0);
        }
    }

    private void BindMaterialType()
    {
        ddlMaterialType.DataSource = ESalesUnityContainer.Container.Resolve<IMaterialTypeService>().GetMaterialTypeList(true);
        ddlMaterialType.DataBind();
        ddlMaterialType.Items.Insert(0, new ListItem(Messages.SelectMaterialType, "0"));
    }

    /// <summary>
    /// Gets alloted quantity details
    /// </summary>
    private void PopulateAllotedQuantity(int materialTypeId)
    {
        if (materialTypeId > 0)
        {
            IList<CustomerMaterialMapDTO> lstCustomersAllotedQty = ESalesUnityContainer.Container.Resolve<ICustomerMaterialService>()
                .GetCustomerMaterialDetailsByMaterialId(materialTypeId);

            //If customer is alloted some 
            if (lstCustomersAllotedQty.Count > 0)
            {
                grdCustAllotedQuantity.DataSource = lstCustomersAllotedQty;
                grdCustAllotedQuantity.DataBind();
            }
            else
            {
                ShowBlankRowInGrid<CustomerMaterialMapDTO>(grdCustAllotedQuantity);
            }
        }
        else
        {
            ShowBlankRowInGrid<CustomerMaterialMapDTO>(grdCustAllotedQuantity);
        }
    }

    protected void ddlMaterialType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMaterialType.SelectedItem != null)
        {            
            PopulateAllotedQuantity(Convert.ToInt32(ddlMaterialType.SelectedItem.Value));
        }
    }

    protected void grdCustAllotedQuantity_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //bind alloted quantity drop down on edit 
        if (grdCustAllotedQuantity.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
        {            
            DropDownList ddlQuantity = (DropDownList)e.Row.FindControl("ddlQuantity");
            MasterList.GetAllotedQuantityInDropDown(ddlQuantity);
            
            string allotedQuantityId = grdCustAllotedQuantity.DataKeys[e.Row.RowIndex]["Cust_Mat_AllotedQuantityId"].ToString();
            if (allotedQuantityId == "1")
            {
                ddlQuantity.SelectedIndex = 1;
            }
            else
            {
                ddlQuantity.SelectedValue = allotedQuantityId;
            }

            if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "CustomerRegType")))
            {
                ddlQuantity.Enabled = true;
            }

            //set customer type on edit
            RadioButtonList rdbCustomerRegistrationType = (RadioButtonList)e.Row.FindControl("rdbCustomerRegistrationType");
            rdbCustomerRegistrationType.SelectedValue = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "CustomerRegType")).ToString();
        }
    }

    private void GridViewRowUpdateFunctions(int editIndex)
    {
        grdCustAllotedQuantity.EditIndex = editIndex;

        //Populate Customer with alloted quantity
        PopulateAllotedQuantity(Convert.ToInt32(ddlMaterialType.SelectedItem.Value));
    }

    protected void grdCustAllotedQuantity_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewRowUpdateFunctions(e.NewEditIndex);
    }

    protected void grdCustAllotedQuantity_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridViewRowUpdateFunctions(-1);
    }

    protected void grdCustAllotedQuantity_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (Page.IsValid)
        {
            int customerId = Convert.ToInt32(grdCustAllotedQuantity.DataKeys[e.RowIndex]["Cust_Mat_CustId"]);
            CustomerDTO customerDetails = MasterList.GetCustomerDetailsById(customerId);

            RadioButtonList rdbCustomerType = (RadioButtonList)grdCustAllotedQuantity.Rows[e.RowIndex]
                .FindControl("rdbCustomerRegistrationType");
            customerDetails.Cust_RegCustType = rdbCustomerType.SelectedValue == "1" ? true : false;
            customerDetails.Cust_CreatedBy = GetCurrentUserId();

            //Get customer material details by customer material map id
            CustomerMaterialMapDTO customerMaterialDetails = ESalesUnityContainer.Container.Resolve<ICustomerMaterialService>()
                .GetCustomerMaterialById(Convert.ToInt32(grdCustAllotedQuantity.DataKeys[e.RowIndex]["Cust_Mat_Id"]));

            //Initialized new material details to customer
            DropDownList ddlQuantity = (DropDownList)grdCustAllotedQuantity.Rows[e.RowIndex].FindControl("ddlQuantity");
            TextBox txtLiftingLimit = (TextBox)grdCustAllotedQuantity.Rows[e.RowIndex].FindControl("txtLiftingLimit");
            customerMaterialDetails.Cust_Mat_AllotedQuantityId = Convert.ToInt32(ddlQuantity.SelectedItem.Value);
            customerMaterialDetails.Cust_Mat_LiftingLimit = Convert.ToInt32(txtLiftingLimit.Text);

            IList<CustomerMaterialMapDTO> listCustomerMaterial = new List<CustomerMaterialMapDTO>();
            listCustomerMaterial.Add(customerMaterialDetails);
            
            //Save Customer and Customer Material Type Details
            ESalesUnityContainer.Container.Resolve<ICustomerService>().SaveAndUpdateCustomerDetails(customerDetails, listCustomerMaterial);
            
            GridViewRowUpdateFunctions(-1);
            ucMessageBox.ShowMessage(Resources.Messages.AllotedQuantityUpdatedSuccessfully);
         }
    }

    protected void grdCustAllotedQuantity_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Change the page index
        grdCustAllotedQuantity.PageIndex = e.NewPageIndex;

        //Populate Customer with alloted quantity
        PopulateAllotedQuantity(Convert.ToInt32(ddlMaterialType.SelectedItem.Value));
    }    

    public string CheckNull(object sender)
    {
        if (Object.ReferenceEquals(sender, null) || Convert.ToBoolean(sender) == false)
        {
            return "0";
        }
        else
        {
            return "1";
        }
    }

    protected IEnumerable grdCustAllotedQuantity_MustAddARow(IEnumerable data)
    {
        return AddBlankRowInGrid<CustomerMaterialMapDTO>();
    }

    protected void CustomerRegType_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList rdbCustomerRegTypeList = (RadioButtonList)sender;
        GridViewRow row = (GridViewRow)rdbCustomerRegTypeList.NamingContainer;
        DropDownList ddlQuantity = ((DropDownList)grdCustAllotedQuantity.Rows[row.RowIndex].FindControl("ddlQuantity"));

        if (rdbCustomerRegTypeList.SelectedValue == "0")
        {
            ddlQuantity.SelectedIndex = 1;
            ddlQuantity.Enabled = false;
        }
        else
        {
            ddlQuantity.Enabled = true;
        }
    }

    protected void EditAllotedQuantity_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        CustomValidator customval = (CustomValidator)sender;
        GridViewRow r = (GridViewRow)customval.NamingContainer;
        Int32 NewQuantity = 0;
        
        try
        {
            NewQuantity = Convert.ToInt32(((DropDownList)r.FindControl("ddlQuantity")).SelectedItem.Text.Trim());
        }
        catch
        {
            NewQuantity = 0;
        }

        Int32 AllotedQunaity=0;
        try
        {
            AllotedQunaity =Convert.ToInt32(grdCustAllotedQuantity.DataKeys[r.RowIndex][3].ToString());
        }
        catch
        {
            AllotedQunaity = 0;
        }

        if (NewQuantity < AllotedQunaity)
        {
            args.IsValid = false;
        }
    }
}