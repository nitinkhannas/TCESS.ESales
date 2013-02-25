#region Namespace

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using System.Collections;
using System.Collections.Specialized;
using Resources;

#endregion

public partial class Supervisor_ManageCautionListForTrucks : BasePage
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
            PopulateMandatoryDocuments();
            PopulateTruckCautionList();
            DropDownList ddlTruckRegNo = (DropDownList)grdTruckCautionLst.FooterRow.FindControl("ddlTruckRegNo");
            ddlTruckRegNo.Items.Insert(0, new ListItem(Messages.SelectTruckRegNo, "0"));
        }
    }

    private void PopulateTruckCautionList()
    {
        IList<TruckDetailsDTO> listTruckDetail = ESalesUnityContainer.Container.Resolve<ICautionListService>()
            .GetCautionListForTrucks(true);

        if (listTruckDetail.Count > 0)
        {
            grdTruckCautionLst.DataSource = listTruckDetail;
            grdTruckCautionLst.DataBind();
        }
        else
        {
            base.ShowBlankRowInGrid<TruckDetailsDTO>(grdTruckCautionLst);
        }
    }

    protected void grdTruckCautionLst_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int truckId = Convert.ToInt32(grdTruckCautionLst.DataKeys[e.RowIndex].Value);

        TruckDetailsDTO truckDetails = new TruckDetailsDTO();
        truckDetails = ESalesUnityContainer.Container.Resolve<ITruckService>().GetTruckDetailsByTruckId(truckId);
        truckDetails.Truck_IsBlacklisted = false;
        truckDetails.Truck_BlacklistedBy = null;
        truckDetails.Truck_LastUpdatedDate = DateTime.Now;
        truckDetails.Truck_IsDeleted = false;

        ESalesUnityContainer.Container.Resolve<ICautionListService>().UpdateCautionListForTrucks(truckDetails);
        txtDocNumber.Text = "";
        ucMessageBoxForGrid.ShowMessage(Resources.Messages.TruckCautionListDeletedSuccessfully);
    }

    protected void grdTruckCautionLst_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //Bind footer truck dropdown list
            //DropDownList ddlTruckRegNo = (DropDownList)e.Row.FindControl("ddlTruckRegNo");
            //ddlTruckRegNo.DataSource = ESalesUnityContainer.Container.Resolve<ICautionListService>().GetCautionListForTrucks(false);
            //ddlTruckRegNo.DataBind();
            //ddlTruckRegNo.Items.Insert(0, new ListItem(Resources.Messages.SelectTruckRegNo, "0"));
        }
    }

    protected void grdTruckCautionLst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTruckCautionLst.PageIndex = e.NewPageIndex;
        PopulateTruckCautionList();
    }

    protected void grdTruckCautionLst_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == Globals.GridCommandEvents.ADDNEW)
        {
            GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;

            TruckDetailsDTO truckDetails = new TruckDetailsDTO();

            truckDetails = ESalesUnityContainer.Container.Resolve<ITruckService>()
                .GetTruckDetailsByTruckId(Convert.ToInt32(((DropDownList)row.FindControl("ddlTruckRegNo")).SelectedValue));
            truckDetails.Truck_IsBlacklisted = true;
            truckDetails.Truck_BlacklistedBy = ((DropDownList)row.FindControl("ddlBlackListedBy")).SelectedValue;
            truckDetails.Truck_LastUpdatedDate = DateTime.Now;
            truckDetails.Truck_IsDeleted = true;

            int truckId = ESalesUnityContainer.Container.Resolve<ICautionListService>()
                .UpdateCautionListForTrucks(truckDetails);
            //PopulateTruckCautionList();
            ucMessageBoxForGrid.ShowMessage(Resources.Messages.TruckCautionListAddedSuccessfully);
        }
    }

    protected IEnumerable grdTruckCautionLst_MustAddARow(IEnumerable data)
    {
        //return the value
        return base.AddBlankRowInGrid<CustomerDTO>();
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        //PopulateTruckCautionList();
        FillDropdownWithTruckRegNo(Convert.ToInt32(ddlMandatoryDoc.SelectedItem.Value), txtDocNumber.Text.Trim());
    }

    private void PopulateMandatoryDocuments()
    {
        ddlMandatoryDoc.DataSource = ESalesUnityContainer.Container.Resolve<IDocumentTypeService>().GetUniqueDocumentTypeList();
        ddlMandatoryDoc.DataBind();
        ddlMandatoryDoc.Items.Insert(0, new ListItem(Labels.CustomerCode, "0"));
    }

    private void FillDropdownWithTruckRegNo(int mandatoryDocId, string documentNo)
    {
        IList<CustomerDTO> lstCustomerDTO = new List<CustomerDTO>();
        IList<TruckDetailsDTO> lstTruckDTO = new List<TruckDetailsDTO>();

        if (Convert.ToInt32(mandatoryDocId) == 0)
        {
            CustomerDTO customerDetails = new CustomerDTO();
            customerDetails = ESalesUnityContainer.Container.Resolve<ICautionListService>().GetCustomerDetailsByCode(documentNo);
            if (customerDetails.Cust_Id > 0)
            {
                lstTruckDTO = ESalesUnityContainer.Container.Resolve<ICautionListService>().GetCautionListForTrucksByCustId(customerDetails.Cust_Id);
            }
        }
        else
        {
            CustomerDocDetailsDTO docDetails = new CustomerDocDetailsDTO();
            docDetails = ESalesUnityContainer.Container.Resolve<ICautionListService>().GetCustomerByDocumentId(mandatoryDocId, documentNo);

            if (docDetails.Cust_Doc_Customer != null)
            {
                lstTruckDTO = ESalesUnityContainer.Container.Resolve<ICautionListService>().GetCautionListForTrucksByCustId(docDetails.Cust_Doc_Id);
            }
        }

        if (lstTruckDTO.Count > 0)
        {
            PopulateTruckCautionList();
            DropDownList ddlTruckRegNo = (DropDownList)grdTruckCautionLst.FooterRow.FindControl("ddlTruckRegNo");
            ddlTruckRegNo.DataSource = lstTruckDTO;
            ddlTruckRegNo.DataBind();
            ddlTruckRegNo.Items.Insert(0, new ListItem(Messages.SelectTruckRegNo, "0"));
        }
        else
        {
            PopulateTruckCautionList();
            DropDownList ddlTruckRegNo = (DropDownList)grdTruckCautionLst.FooterRow.FindControl("ddlTruckRegNo");
            ddlTruckRegNo.Items.Insert(0, new ListItem(Messages.SelectTruckRegNo, "0"));
        }
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            FillDropdownWithTruckRegNo(Convert.ToInt32(ddlMandatoryDoc.SelectedItem.Value), txtDocNumber.Text.Trim());
        }
    }
}