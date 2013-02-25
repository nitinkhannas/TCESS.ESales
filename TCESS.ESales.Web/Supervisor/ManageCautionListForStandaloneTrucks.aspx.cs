#region Namespace

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Supervisor_ManageCautionListForStandaloneTrucks : BasePage
{
    DropDownList ddlFooterTruckRegNo;
    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckIsUserAuthenticated();

        if (!IsPostBack)
        {
            PopulateStandaloneTruckCautionList();
            ddlFooterTruckRegNo = (DropDownList)grdStandaloneTruckCautionList.FooterRow.FindControl("ddlTruckRegNo");
            ddlFooterTruckRegNo.Items.Insert(0, new ListItem(Resources.Messages.SelectTruckRegNo, "0"));
        }
    }

    private void PopulateStandaloneTruckCautionList()
    {
        IList<StandaloneTrucksDTO> lstStandaloneTruck = ESalesUnityContainer.Container.Resolve<ICautionListService>()
            .GetCautionListForStandaloneTrucks(true);

        if (lstStandaloneTruck.Count > 0)
        {
            grdStandaloneTruckCautionList.DataSource = lstStandaloneTruck;
            grdStandaloneTruckCautionList.DataBind();
        }
        else
        {
            base.ShowBlankRowInGrid<StandaloneTrucksDTO>(grdStandaloneTruckCautionList);
        }
    }

    protected void grdStandaloneTruckCautionList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int truckId = Convert.ToInt32(grdStandaloneTruckCautionList.DataKeys[e.RowIndex].Value);

        StandaloneTrucksDTO truckDetails = ESalesUnityContainer.Container.Resolve<IStandaloneTruckService>()
            .GetStandaloneTruckByTruckId(truckId);
        truckDetails.StandaloneTruck_IsBlacklisted = false;
        truckDetails.StandaloneTruck_Blacklistedby = null;
        truckDetails.StandaloneTruck_IsDeleted = false;
        truckDetails.StandaloneTruck_LastUpdatedDate = DateTime.Now;
        ESalesUnityContainer.Container.Resolve<ICautionListService>().UpdateCautionListForStandaloneTrucks(truckDetails);
        ucMessageBoxForGrid.ShowMessage(Resources.Messages.StandaloneTruckCautionListDeletedSuccessfully);
    }

    protected void grdStandaloneTruckCautionList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //Bind truck dropdown list in footer row
            DropDownList ddlTruckRegNo = (DropDownList)e.Row.FindControl("ddlTruckRegNo");
            if (ddlFooterTruckRegNo != null)
            {
                foreach (ListItem item in ddlFooterTruckRegNo.Items)
                {
                    ddlTruckRegNo.Items.Add(item);
                }
            }
        }
    }

    protected void grdStandaloneTruckCautionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdStandaloneTruckCautionList.PageIndex = e.NewPageIndex;

        PopulateStandaloneTruckCautionList();
    }

    protected void grdStandaloneTruckCautionList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == Globals.GridCommandEvents.ADDNEW)
        {
            GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;

            StandaloneTrucksDTO truckDetails = new StandaloneTrucksDTO();

            truckDetails = ESalesUnityContainer.Container.Resolve<IStandaloneTruckService>()
                .GetStandaloneTruckByTruckId(Convert.ToInt32(((DropDownList)row.FindControl("ddlTruckRegNo")).SelectedValue));
            truckDetails.StandaloneTruck_IsBlacklisted = true;
            truckDetails.StandaloneTruck_IsDeleted = true;
            truckDetails.StandaloneTruck_Blacklistedby = ((DropDownList)row.FindControl("ddlBlackListedBy")).SelectedValue;
            truckDetails.StandaloneTruck_CustCode = txtCustomerCode.Text.Trim();
            truckDetails.StandaloneTruck_LastUpdatedDate = DateTime.Now;

            int truckId = ESalesUnityContainer.Container.Resolve<ICautionListService>()
                .UpdateCautionListForStandaloneTrucks(truckDetails);

            //deleting the selected TruckRegNo from the footer combo.
            ((DropDownList)row.FindControl("ddlTruckRegNo")).Items.Remove(((DropDownList)row.FindControl("ddlTruckRegNo")).SelectedItem);
            getComboItemsInAddl();
            PopulateStandaloneTruckCautionList();
            txtCustomerCode.Enabled = true;
            ucMessageBoxForGrid.ShowMessage(Resources.Messages.StandaloneTruckCautionListAddedSuccessfully);
        }
    }

    protected IEnumerable grdStandaloneTruckCautionList_MustAddARow(IEnumerable data)
    {
        //return the value
        return base.AddBlankRowInGrid<StandaloneTrucksDTO>();
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        getComboItemsInAddl();
        PopulateStandaloneTruckCautionList();
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            getComboItemsInAddl();
            FillGridCombo(txtTruckNumber.Text.Trim());
            PopulateStandaloneTruckCautionList();
            txtCustomerCode.Enabled = false;
        }
    }

    private void getComboItemsInAddl()
    {
        DropDownList ddlTruckRegNo = (DropDownList)grdStandaloneTruckCautionList.FooterRow.FindControl("ddlTruckRegNo");
        ddlFooterTruckRegNo = new DropDownList();
        foreach (ListItem item in ddlTruckRegNo.Items)
        {
            ddlFooterTruckRegNo.Items.Add(item);
        }
    }

    private void FillGridCombo(string TruckNo)
    {
        StandaloneTrucksDTO truckDetails = new StandaloneTrucksDTO();
        truckDetails = ESalesUnityContainer.Container.Resolve<IStandaloneTruckService>().GetStandaloneTruckByRegNo(TruckNo);

        CustomerDTO customerDetails = new CustomerDTO();
        customerDetails = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetCustomerDetailsByCode(txtCustomerCode.Text.Trim());

        if (!string.IsNullOrEmpty(customerDetails.Cust_Code))//customer exists
        {
            if (truckDetails.StandaloneTruck_Id > 0)// truckNo exists
            {
                if (!truckDetails.StandaloneTruck_IsBlacklisted)// Not Already CautionListed
                {
                    ListItem li = ddlFooterTruckRegNo.Items.FindByValue(truckDetails.StandaloneTruck_Id.ToString());
                    if (li == null)
                    {
                        ddlFooterTruckRegNo.Items.Add(new ListItem(truckDetails.StandaloneTruck_RegNo, truckDetails.StandaloneTruck_Id.ToString().Trim()));
                    }
                    else
                    {
                        ucMessageBoxForGrid.ShowMessage("This truck no is already verified.");
                    }
                }
                else
                {
                    ucMessageBoxForGrid.ShowMessage("This truck no is already in caution list.");
                }
            }
            else
            {
                ucMessageBoxForGrid.ShowMessage(Resources.Messages.TruckDetailsDoesNotExist);
            }
        }
        else
        {
            ucMessageBoxForGrid.ShowMessage("This customer code does not exist.");
        }
    }

    protected string GetCustomerName(string custname)
    {
        CustomerDTO custDetails = new CustomerDTO();
        custDetails = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetCustomerDetailsByCode(custname);

        if (custDetails.Cust_Id > 0)
        {
            return custDetails.Cust_FirmName;
        }
        else
        {
            return null;
        }
    }
}