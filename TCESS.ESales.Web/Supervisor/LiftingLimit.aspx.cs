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
using TCESS.ESales.PersistenceLayer.Interfaces;
using TCESS.ESales.PersistenceLayer.Entity;

#endregion

public partial class Supervisor_LiftingLimit : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //base.CheckIsUserAuthenticated();
        if (!IsPostBack)
        {
            GetActivelimit();
            PopulateLiftingLimitHistory();
        }
    }

    public void GetActivelimit()
    {
        IList<LiftingLimitDTO> LstLimitDTO = ESalesUnityContainer.Container.Resolve<ILiftingLimit>().GetLimitList();
        if (LstLimitDTO.Count > 0)
        {
            grdCustCautionLstMaster.DataSource = LstLimitDTO;
            grdCustCautionLstMaster.DataBind();
        }
        else
        {
            base.ShowBlankRowInGrid<LiftingLimitDTO>(grdCustCautionLstMaster);
        }
    }

    protected void grdCustCautionLstMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MasterList Master = new MasterList();
            //Bind footer customer dropdown list
            DropDownList ddlTruckType = (DropDownList)e.Row.FindControl("ddlTruckType");
            DropDownList ddlBusinessType = (DropDownList)e.Row.FindControl("txtBusinessType");
            DropDownList ddlLiftingLimit = (DropDownList)e.Row.FindControl("ddlLiftingLimit");
            DropDownList ddlTimeInterval = (DropDownList)e.Row.FindControl("ddlTimeInterval");

            int selectedBusinessValue = 1;
            int selectedTruckValue = 1;
            int selectedLiftingIntervalValue = 1;
            int selectedTimeIntervalValue = 1;

            if (e.Row.DataItem != null)
            {
                selectedBusinessValue = ((TCESS.ESales.DataTransferObjects.LiftingLimitDTO)(e.Row.DataItem)).LiftingLimit_BusinessTypeID;
                selectedTruckValue = ((TCESS.ESales.DataTransferObjects.LiftingLimitDTO)(e.Row.DataItem)).LiftingLimit_TruckRegType_Id; ;
                selectedLiftingIntervalValue = ((TCESS.ESales.DataTransferObjects.LiftingLimitDTO)(e.Row.DataItem)).LiftingLimit_IntervalId;
                selectedTimeIntervalValue = ((TCESS.ESales.DataTransferObjects.LiftingLimitDTO)(e.Row.DataItem)).LiftingLimit_Timeinterval;
            }

            if (ddlBusinessType != null)
            {
                BindBusinessType(ddlBusinessType, selectedBusinessValue);
            }

            if (ddlTruckType != null)
            {
                BindTruckType(ddlTruckType, selectedTruckValue);
            }

            if (ddlLiftingLimit != null)
            {
                BindLiftingInterval(ddlLiftingLimit, selectedLiftingIntervalValue);
            }

            if (ddlTimeInterval != null)
            {
                BindTimeInterval(ddlTimeInterval, selectedTimeIntervalValue);
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //Bind footer customer dropdown list
            IList<LiftingLimitDTO> LstLimitDTO = ESalesUnityContainer.Container.Resolve<ILiftingLimit>().GetLimitList();
            IList<BusinessTypeDTO> lstBusinessType = MasterList.GetBusinessTypeList();

            List<BusinessTypeDTO> lst = new List<BusinessTypeDTO>();

            foreach (BusinessTypeDTO b in lstBusinessType)
            {
                lst.Add(b);
                foreach (LiftingLimitDTO l in LstLimitDTO)
                {
                    if (b.BusinessType_Name == l.LiftingLimit_Business_Name)
                    {
                        lst.Remove(b);
                    }
                }
            }

            DropDownList ddlCustomerName = (DropDownList)e.Row.FindControl("ddlBusinessType");
            ddlCustomerName.DataSource = lst;
            ddlCustomerName.DataBind();
            ddlCustomerName.Items.Insert(0, new ListItem(Messages.SelectBusinessType, "0"));

            DropDownList ddlTruckType = (DropDownList)e.Row.FindControl("ddlTruckType");
            ddlTruckType.DataSource = MasterList.GetTruckregTypeList();
            ddlTruckType.DataBind();
            ddlTruckType.Items.Insert(0, new ListItem("Select Truck Type", "0"));

            DropDownList ddlLiftingLimit = (DropDownList)e.Row.FindControl("ddlLiftingLimit");
            ddlLiftingLimit.DataSource = MasterList.GetLiftingIntervalList();
            ddlLiftingLimit.DataBind();
            ddlLiftingLimit.Items.Insert(0, new ListItem("Select Lifting Truck", "0"));

            DropDownList ddlTimeInterval = (DropDownList)e.Row.FindControl("ddlTimeInterval");
            ddlTimeInterval.DataSource = MasterList.GetLiftingIntervalList();
            ddlTimeInterval.DataBind();
            ddlTimeInterval.Items.Insert(0, new ListItem("Select Lifting Interval", "0"));
        }
    }

    private void BindBusinessType(DropDownList ddl, int selectedValue)
    {
        //Bind footer customer dropdown list
        DropDownList ddlCustomerName = ddl;
        ddlCustomerName.DataSource = MasterList.GetBusinessTypeList();
        ddlCustomerName.DataBind();
        ddlCustomerName.Items.Insert(0, new ListItem(Messages.SelectBusinessType, "0"));
     
        if (selectedValue > 0)
        {
            ddl.SelectedValue = selectedValue.ToString();
        }
    }

    public void BindTruckType(DropDownList ddl, int selectedValue)
    {
        DropDownList ddlTruckType = ddl;
        ddlTruckType.DataSource = MasterList.GetTruckregTypeList();
        ddlTruckType.DataBind();
        ddlTruckType.Items.Insert(0, new ListItem("SelectTruckType", "0"));
        ddlTruckType.Items.Insert(1, new ListItem("Local Truck", "3"));
     
        if (selectedValue > 0)
        {
            ddl.SelectedValue = selectedValue.ToString();
        }
    }

    private void BindTimeInterval(DropDownList ddl, int selectedValue)
    {
        //Bind footer customer dropdown list
        DropDownList ddlCustomerName = ddl;
        ddlCustomerName.DataSource = MasterList.GetLiftingIntervalList();
        ddlCustomerName.DataBind();
        ddlCustomerName.Items.Insert(0, new ListItem("Select Lifting Interval", "0"));
     
        if (selectedValue > 0)
        {
            ddl.SelectedValue = selectedValue.ToString();
        }
    }

    private void BindLiftingInterval(DropDownList ddl, int selectedValue)
    {
        //Bind footer customer dropdown list
        DropDownList ddlCustomerName = ddl;
        ddlCustomerName.DataSource = MasterList.GetLiftingIntervalList();
        ddlCustomerName.DataBind();
        ddlCustomerName.Items.Insert(0, new ListItem("Select Lifting Truck", "0"));
     
        if (selectedValue > 0)
        {
            ddl.SelectedValue = selectedValue.ToString();
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
                LiftingLimitDTO lifitinglimit = new LiftingLimitDTO();
                lifitinglimit.LifitingLimit_Date = DateTime.Now.Date;
                lifitinglimit.LiftingLimit_IsActive = true;
                lifitinglimit.LiftingLimit_CreatedDate = DateTime.Now;
                lifitinglimit.LiftingLimit_LastUpdated = DateTime.Now;
                lifitinglimit.LiftingLimit_CreatedBy = GetCurrentUserId();
                lifitinglimit.LiftingLimit_BusinessTypeID = Convert.ToInt32(((DropDownList)gvRow.FindControl("ddlBusinessType")).SelectedValue);
                lifitinglimit.LiftingLimit_TruckRegType_Id = Convert.ToInt32(((DropDownList)gvRow.FindControl("ddlTruckType")).SelectedValue);
                lifitinglimit.Annual_LiftingLimit_Limit = Convert.ToInt32(((TextBox)gvRow.FindControl("tbBookingLimit")).Text); ;
                lifitinglimit.LiftingLimit_Timeinterval = Convert.ToInt32(((DropDownList)gvRow.FindControl("ddlTimeInterval")).SelectedValue);
                lifitinglimit.LiftingLimit_Limit = Convert.ToInt32(((DropDownList)gvRow.FindControl("ddlLiftingLimit")).SelectedValue);
                lifitinglimit.LiftingLimit_IntervalId = Convert.ToInt32(((DropDownList)gvRow.FindControl("ddlTimeInterval")).SelectedValue);
                ESalesUnityContainer.Container.Resolve<ILiftingLimit>().SaveLiftingLimit(lifitinglimit);
                GetActivelimit();
                PopulateLiftingLimitHistory();
                ucMessageBoxForGrid.ShowMessage(Resources.Messages.Liftinglimitadd);
            }
        }
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        GridViewRowUpdateFunctions(-1);
        GetActivelimit();
        PopulateLiftingLimitHistory();
    }

    private void PopulateLiftingLimitHistory()
    {
        IList<LiftingLimitDTO> lstLiftingLimitHistory = ESalesUnityContainer.Container.Resolve<ILiftingLimit>()
            .GetLiftingLimitHistoryList();

        //If material type history exists
        if (lstLiftingLimitHistory.Count > 0)
        {
            gridHistory.DataSource = lstLiftingLimitHistory;
            gridHistory.DataBind();
        }
    }

    protected void grdCustCautionLstMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewRowUpdateFunctions(e.NewEditIndex);
    }

    private void GridViewRowUpdateFunctions(int rowIndex)
    {
        grdCustCautionLstMaster.EditIndex = rowIndex;

        //Get all active material type information from database
        GetActivelimit();
        PopulateLiftingLimitHistory();
    }

    protected void grdCustCautionLstMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        LiftingLimitDTO liftingLimitDetails = new LiftingLimitDTO();
        liftingLimitDetails = ESalesUnityContainer.Container.Resolve<ILiftingLimit>()
            .GetLiftingLimitById(Convert.ToInt32(grdCustCautionLstMaster.DataKeys[e.RowIndex].Value));

        liftingLimitDetails.Annual_LiftingLimit_Limit = Convert.ToInt32(((TextBox)grdCustCautionLstMaster.Rows[e.RowIndex].FindControl("txtBookingLimit")).Text);
        liftingLimitDetails.LiftingLimit_Business_Name = ((DropDownList)grdCustCautionLstMaster.Rows[e.RowIndex].FindControl("txtBusinessType")).SelectedItem.Text;
        liftingLimitDetails.LiftingLimit_BusinessTypeID = Convert.ToInt32(((DropDownList)grdCustCautionLstMaster.Rows[e.RowIndex].FindControl("txtBusinessType")).SelectedValue);
        liftingLimitDetails.LiftingLimit_LastUpdated = DateTime.Now;
        liftingLimitDetails.LiftingLimit_Limit = Convert.ToInt32(((DropDownList)grdCustCautionLstMaster.Rows[e.RowIndex].FindControl("ddlLiftingLimit")).SelectedValue);
        liftingLimitDetails.LiftingLimit_Timeinterval = Convert.ToInt32(((DropDownList)grdCustCautionLstMaster.Rows[e.RowIndex].FindControl("ddlTimeInterval")).SelectedValue);
        liftingLimitDetails.LiftingLimit_TruckRegType_Id = Convert.ToInt32(((DropDownList)grdCustCautionLstMaster.Rows[e.RowIndex].FindControl("ddlTruckType")).SelectedValue);
        liftingLimitDetails.LiftingLimit_TruckRegType_Name = ((DropDownList)grdCustCautionLstMaster.Rows[e.RowIndex].FindControl("ddlTruckType")).SelectedItem.Text;
        liftingLimitDetails.liftinginterval.liftinginterval_Id = Convert.ToInt32(((DropDownList)grdCustCautionLstMaster.Rows[e.RowIndex].FindControl("ddlLiftingLimit")).SelectedValue);
        //liftingLimitDetails.truckregtype.TruckRegType_IsDeleted = true;
        //liftingLimitDetails.truckregtype.TruckRegType_Id = Convert.ToInt32(((DropDownList)grdCustCautionLstMaster.Rows[e.RowIndex].FindControl("ddlTruckType")).SelectedValue);

        UpdateLiftingIntervalByBusinessTypeId(liftingLimitDetails.LiftingLimit_BusinessTypeID, liftingLimitDetails.Annual_LiftingLimit_Limit, liftingLimitDetails.LiftingLimit_Limit, liftingLimitDetails.LiftingLimit_Timeinterval, liftingLimitDetails.LiftingLimit_TruckRegType_Id);

        int liftingLimitId = ESalesUnityContainer.Container.Resolve<ILiftingLimit>().UpdateLiftingLimit(liftingLimitDetails);
        ucMessageBoxForGrid.ShowMessage("Lifting Details Saved Successfully");
    }

    protected void grdCustCautionLstMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridViewRowUpdateFunctions(-1);
    }

    protected void grdCustCautionLstMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int LiftingLimit_ID = Convert.ToInt32(grdCustCautionLstMaster.DataKeys[e.RowIndex].Value);

        //Delete material type from database
        ESalesUnityContainer.Container.Resolve<ILiftingLimit>().DeleteLiftingLimit(LiftingLimit_ID);
        ucMessageBoxForGrid.ShowMessage("Lifting Limit Deleted successfully");
    }

    /// <summary>
    /// Updates the lifting interval by business type id.
    /// </summary>
    /// <param name="businessTypeId">The business type id.</param>
    /// <param name="annualLimit">The annual limit.</param>
    /// <param name="liftingLimit">The lifting limit.</param>
    /// <param name="liftingInterval">The lifting interval.</param>
    /// <param name="truckRegId">The truck reg id.</param>
    private void UpdateLiftingIntervalByBusinessTypeId(int businessTypeId, int annualLimit, int liftingLimit, int liftingInterval, int truckRegId)
    {
        List<CustomerMaterialMapDTO> lstCustomerMatDetails = new List<CustomerMaterialMapDTO>();
        List<CustomerMaterialMapDTO> lstCustomerMat = new List<CustomerMaterialMapDTO>();
        List<AllotedQuantityDTO> lstQty = new List<AllotedQuantityDTO>();
        int quantityFlag = 0;
        int qtyId = 0;

        // Update truckregtypeid for business other than bricks
        if (businessTypeId != 1)
        {
            ESalesUnityContainer.Container.Resolve<ILiftingLimit>().UpdateLiftingLimitTruckRegId(truckRegId);
        }

        // Get details of customer by businesstypeid
        List<int> lstCustomer = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetCustomerIdByBusinessTypeId(businessTypeId);
        // Get all customermaterialmap details
        lstCustomerMatDetails = ESalesUnityContainer.Container.Resolve<ICustomerMaterialService>().GetCustomerMaterialDetailsList();

        // Get allotted quantity details
        lstQty = ESalesUnityContainer.Container.Resolve<ILiftingLimit>().GetAllottedQuantityDetails();

        // Check if quantity exists in allotted quantity details
        foreach (AllotedQuantityDTO item in lstQty)
        {
            if (item.Alloted_Quantity == annualLimit.ToString())
            {
                quantityFlag = 1;
                qtyId = item.Alloted_Id;
                break;
            }
        }
        // Will add new entry in allotted quantity if it doesn't exist
        if (quantityFlag != 1)
        {
            AllotedQuantityDTO allottedQty = new AllotedQuantityDTO();

            allottedQty.Alloted_Quantity = annualLimit.ToString();
            allottedQty.Alloted_CreatedBy = GetCurrentUserId();
            allottedQty.Alloted_CreatedDate = DateTime.Now;
            allottedQty.Alloted_IsDeleted = false;

            qtyId = ESalesUnityContainer.Container.Resolve<ILiftingLimit>().InsertAllottedQuantity(allottedQty);
        }

        // Get filtered list of customers based on businesstypeid
        lstCustomerMat = lstCustomerMatDetails.FindAll(C => lstCustomer.Contains(C.Cust_Mat_CustId));

        foreach (CustomerMaterialMapDTO item in lstCustomerMat)
        {
            customermaterialmap upCustomerMatEntity = new customermaterialmap();
            item.Cust_Mat_LiftingLimit = liftingLimit;
            item.Cust_Mat_AllotedQuantityId = qtyId;
            item.Cust_Mat_Timeinterval = liftingInterval;
            item.Cust_Mat_LastUpdatedDate = DateTime.Now;
            AutoMapper.Mapper.Map(item, upCustomerMatEntity);
            ESalesUnityContainer.Container.Resolve<IGenericRepository<customermaterialmap>>().Update(upCustomerMatEntity);
        }
    }
}