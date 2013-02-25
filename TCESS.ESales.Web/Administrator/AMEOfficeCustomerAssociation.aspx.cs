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

#endregion

public partial class Administrator_AMEOfficeCustomerAssociation : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckIsUserAuthenticated();

        if (!IsPostBack)
        {  
            //To populate AME grid
            PopulateAME();
        }
    }

    /// <summary>
    /// Popualte all active AME Blocks
    /// </summary>
    private void PopulateAME()
    {
        IList<AmeBlockDTO> listAMEBlockDTO = MasterList.GetAmeBlockList();
        grdAMEBlocks.DataSource = listAMEBlockDTO;
        grdAMEBlocks.DataBind();

        if (listAMEBlockDTO.Count > 0)
        {
            grdAMEBlocks.DataSource = listAMEBlockDTO;
            grdAMEBlocks.DataBind();

            //Gets Customer details by selected AME Block
            PopulateAMEAssociatedCustomer();
        }
        else
        {
            base.ShowBlankRowInGrid<AmeBlockDTO>(grdAMEBlocks);
            base.ShowBlankRowInGrid<CustomerDTO>(grdCustomerDetails);
        }
    }

    protected void grdAMEBlocks_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == Globals.GridCommandEvents.SHOWCUSTOMER)
        {
            ViewState[Globals.StateMgmtVariables.BLOCKID] = e.CommandArgument;
            
            //Sets the selected AME Block name as header on Customer grid
            lblAMEBlocks.Text = ((LinkButton)e.CommandSource).Text;

            //Gets Customer details by selected AME Block
            PopulateAMEAssociatedCustomer();
        }
    }

    protected void grdAMEBlocks_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAMEBlocks.PageIndex = e.NewPageIndex;
        PopulateAME();
    }

    protected IEnumerable grdAMEBlocks_MustAddARow(IEnumerable data)
    {        
        return base.AddBlankRowInGrid<AmeBlockDTO>();
    }

    /// <summary>
    /// Bind all associated customer with AME Blocks. Byy default customers associated with first AME Blocks are shown
    /// </summary>
    private void PopulateAMEAssociatedCustomer()
    {
        if (ViewState[Globals.StateMgmtVariables.BLOCKID] == null)
        {
            lblAMEBlocks.Text = ((LinkButton)grdAMEBlocks.Rows[0].Cells[1].FindControl("lnkBlockName")).Text;

            //AME Blocks Id is saved in Viewstate to populate the associated customer by default for first AME
            ViewState[Globals.StateMgmtVariables.BLOCKID] = grdAMEBlocks.DataKeys[0].Value;
        }

        IList<CustomerDTO> listCustomers = ESalesUnityContainer.Container.Resolve<IAmeBlockService>()
            .GetCustomerAssociatedForAmeBlock(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.BLOCKID]));

        // Displays Customer with selected AME Block.
        if (listCustomers.Count > 0)
        {
            grdCustomerDetails.DataSource = listCustomers;
            grdCustomerDetails.DataBind();
        }
        else
        {
            base.ShowBlankRowInGrid<CustomerDTO>(grdCustomerDetails);
        }
    }
    
    protected void grdCustomerDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    { 
        // To populate Block dropdowm list for update
        if (grdCustomerDetails.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
        {   
            DropDownList ddlAMEOffice = (DropDownList)e.Row.FindControl("ddlBlock");
            
            ddlAMEOffice.DataSource = MasterList.GetAmeBlockList();
            ddlAMEOffice.DataBind();
            ddlAMEOffice.Items.Insert(0, new ListItem(Messages.SelectAMEOffice, "0"));
                        
            //To fetch particular AME blockid
            if (ViewState[Globals.StateMgmtVariables.BLOCKID] != null)
            {
                ddlAMEOffice.SelectedValue = Convert.ToString(ViewState[Globals.StateMgmtVariables.BLOCKID]);
            }
        }
    }

    protected void grdCustomerDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdCustomerDetails.EditIndex = e.NewEditIndex;
        grdCustomerDetails.Columns[2].Visible = true;

        //Gets Customer details by selected AME Block
        PopulateAMEAssociatedCustomer();
    }

    protected IEnumerable grdCustomerDetails_MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<CustomerDTO>();
    }
        
    protected void grdCustomerDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (Page.IsValid)
        {
            //Get Customer details from database
            CustomerDTO customerDetails = MasterList.GetCustomerDetailsById(Convert.ToInt32(grdCustomerDetails.DataKeys[e.RowIndex].Value));

            customerDetails.Cust_AMEBlockId = Convert.ToInt32(((DropDownList)grdCustomerDetails.Rows[e.RowIndex].FindControl("ddlBlock")).SelectedValue);
            customerDetails.Cust_CreatedBy = base.GetCurrentUserId();

            //Updates Customer AME association
            int custId = ESalesUnityContainer.Container.Resolve<IAmeBlockService>().UpdateCustomerAmeBlock(customerDetails);

            grdCustomerDetails.EditIndex = -1;

            grdCustomerDetails.Columns[2].Visible = false;

            //Gets Customer details by selected AME Block
            PopulateAMEAssociatedCustomer();
            ucMessageBoxForGrid.ShowMessage(Resources.Messages.CustomerAMEOfficeUpdatedSuccessfully);
        }
    }

    protected void grdCustomerDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCustomerDetails.PageIndex = e.NewPageIndex;

        //Gets Customer details by selected AME Block
        PopulateAMEAssociatedCustomer();
    }   

    protected void grdCustomerDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdCustomerDetails.EditIndex = -1;
        grdCustomerDetails.Columns[2].Visible = false;

        //Gets Customer details by selected AME Block
        PopulateAMEAssociatedCustomer();
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        PopulateAMEAssociatedCustomer();
    }
}