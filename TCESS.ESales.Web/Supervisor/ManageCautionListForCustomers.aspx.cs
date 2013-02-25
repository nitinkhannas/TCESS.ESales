#region Namespace

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Supervisor_ManageCautionListForCustomers : BasePage
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
            //Populate customer caution list
            PopulateMandatoryDocuments();
            PopulateCustCautionList();
        }
    }

    private void PopulateMandatoryDocuments()
    {
        ddlMandatoryDoc.DataSource = ESalesUnityContainer.Container.Resolve<IDocumentTypeService>().GetUniqueDocumentTypeList();
        ddlMandatoryDoc.DataBind();
        ddlMandatoryDoc.Items.Insert(0, new ListItem(Labels.CustomerCode, "0"));
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            FillDropdownWithCustomerDetails(Convert.ToInt32(ddlMandatoryDoc.SelectedItem.Value), txtDocNumber.Text.Trim());
        }
    }

    private void FillDropdownWithCustomerDetails(int mandatoryDocId, string documentNo)
    {
        IList<CustomerDTO> lstCustomerDTO = new List<CustomerDTO>();

        if (Convert.ToInt32(mandatoryDocId) == 0)
        {
            CustomerDTO customerDetails = new CustomerDTO();
            customerDetails = ESalesUnityContainer.Container.Resolve<ICautionListService>().GetCustomerDetailsByCode(documentNo);
            if (customerDetails.Cust_Id > 0)
            {
                lstCustomerDTO.Add(customerDetails);
            }
        }
        else
        {
            CustomerDocDetailsDTO docDetails = new CustomerDocDetailsDTO();
            docDetails = ESalesUnityContainer.Container.Resolve<ICautionListService>().GetCustomerByDocumentId(mandatoryDocId, documentNo);

            if (docDetails.Cust_Doc_Customer != null)
            {
                lstCustomerDTO.Add(docDetails.Cust_Doc_Customer);
            }
        }

        if (lstCustomerDTO.Count > 0)
        {
            PopulateCustCautionList();
            DropDownList ddlCustomerName = (DropDownList)grdCustCautionLstMaster.FooterRow.FindControl("ddlCustomerName");
            ddlCustomerName.DataSource = lstCustomerDTO;
            ddlCustomerName.DataBind();
            ddlCustomerName.Items.Insert(0, new ListItem(Messages.SelectCustomer, "0"));
        }
        else
        {
            PopulateCustCautionList();
            // ShowBlankGrid();
        }
    }

    private void PopulateCustCautionList()
    {
        IList<CustomerDTO> LstCustomerDTO = ESalesUnityContainer.Container.Resolve<ICautionListService>().GetCautionListForCustomers(true);
        if (LstCustomerDTO.Count > 0)
        {
            grdCustCautionLstMaster.DataSource = LstCustomerDTO;
            grdCustCautionLstMaster.DataBind();
        }
        else
        {
            base.ShowBlankRowInGrid<CustomerDTO>(grdCustCautionLstMaster);
        }
    }

    protected void grdCustCautionLstMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            ////Bind footer customer dropdown list
            //DropDownList ddlCustomerName = (DropDownList)e.Row.FindControl("ddlCustomerName");
            //ddlCustomerName.DataSource = ESalesUnityContainer.Container.Resolve<ICautionListService>()
            //    .GetCautionListForCustomers(false);
            //ddlCustomerName.DataBind();
            //ddlCustomerName.Items.Insert(0, new ListItem(Messages.SelectCustomer, "0"));
        }
    }

    protected void grdCustCautionLstMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals(Globals.GridCommandEvents.ADDNEW))
        {
            if (Page.IsValid)
            {
                //To add customer in caution list 
                GridViewRow gvRow = (GridViewRow)((Button)e.CommandSource).NamingContainer;

                CustomerDTO customerDetails = MasterList.GetCustomerDetailsById(Convert.ToInt32(((DropDownList)gvRow.FindControl("ddlCustomerName")).SelectedValue));
                customerDetails.Cust_IsBlacklisted = true;
                customerDetails.Cust_IsDeleted = true;
                customerDetails.Cust_BlacklistedBy = ((DropDownList)gvRow.FindControl("ddlBlackListedBy")).SelectedValue;
                customerDetails.Cust_LastUpdatedDate = DateTime.Now;

                ESalesUnityContainer.Container.Resolve<ICustomerService>().SaveAndUpdateCustomerDetails(customerDetails, null);
                PopulateCustCautionList();
                txtDocNumber.Text = "";
                ucMessageBoxForGrid.ShowMessage(Resources.Messages.CustomerCautionListCreatedSuccessfully);
            }
        }
    }

    protected void grdCustCautionLstMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int customerId = Convert.ToInt32(grdCustCautionLstMaster.DataKeys[e.RowIndex].Value);

        CustomerDTO customerDetails = MasterList.GetCustomerDetailsById(customerId);
        customerDetails.Cust_IsBlacklisted = false;
        customerDetails.Cust_BlacklistedBy = null;
        customerDetails.Cust_LastUpdatedDate = DateTime.Now;
        customerDetails.Cust_IsDeleted = false;

        ESalesUnityContainer.Container.Resolve<ICustomerService>().SaveAndUpdateCustomerDetails(customerDetails, null);
        ucMessageBoxForGrid.ShowMessage(Resources.Messages.CustomerCautionListDeletedSuccessfully);
    }

    protected void grdCustCautionLstMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCustCautionLstMaster.PageIndex = e.NewPageIndex;
        PopulateCustCautionList();
    }

    protected IEnumerable grdCustCautionLstMaster_MustAddARow(IEnumerable data)
    {
        //return the value
        return base.AddBlankRowInGrid<CustomerDTO>();
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        PopulateCustCautionList();
    }
}