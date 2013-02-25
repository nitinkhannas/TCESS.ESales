#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class CustomerRegistration_ViewCustomerDetails : BasePage
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
            if (Session[Globals.StateMgmtVariables.CUSTOMERID] != null)
            {
                ViewState[Globals.StateMgmtVariables.CUSTOMERID] = Session[Globals.StateMgmtVariables.CUSTOMERID];
                int customerId = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]);
                PopulateCustomerDetails(customerId);
                
                //Get truck details for customer linked trucks by customer id   
                GetTruckDetails(customerId);
                
                //Get auth rep details by customer id
                GetAuthRepDetails(customerId);
                Session[Globals.StateMgmtVariables.CUSTOMERID] = null;
            }

            //Hide FileUpload control and button if navigated from Activate Customer screen
            if (Convert.ToInt32(Session[Globals.StateMgmtVariables.VIEWCUSTOMERSOURCE]) == 1)
            {
                UploadFileArea.Visible = false;
                btnSaveAndUpload.Visible = false;
                btnAuthCertificate.Visible = true;
                ButtonArea.Align = "center";
            }
        }
	}

    /// <summary>
    /// Populate customer details by customer id
    /// </summary>
    /// <param name="customerId"></param>
    private void PopulateCustomerDetails(int customerId)
    {
        CustomerDTO customerDetails = MasterList.GetCustomerDetailsById(customerId);
        lblTradeName.Text = customerDetails.Cust_TradeName;
        lblFirmName.Text = customerDetails.Cust_FirmName;
        lblOwnerName.Text = customerDetails.Cust_OwnerName;
        lblFatherName.Text = customerDetails.Cust_FathersName;
        lblUnitAddress.Text = customerDetails.Cust_UnitAddress;
        lblRegisteredAddress.Text = customerDetails.Cust_RegisteredAddress;
        lblState.Text = customerDetails.Cust_State_Name;
        lblDistrict.Text = customerDetails.Cust_District_Name;
        lblLandmark.Text = customerDetails.Cust_Landmark;
        lblPinCode.Text = Convert.ToString(customerDetails.Cust_Pincode);
        lblMobileNo.Text = customerDetails.Cust_MobileNo;
        lblPhoneNumber.Text = customerDetails.Cust_PhoneNo;
        lblOwnershipStatus.Text = customerDetails.Cust_OwnershipName;
        lblBusinessType.Text = customerDetails.Cust_Business_Name;
        lblSalesType.Text = customerDetails.Cust_SalesType == 1 ? Labels.WithinJharkhand : Labels.OutsideJharkhand;
        lblPartnerMobileNumber.Text = customerDetails.Cust_PartnerPhoneNo;
        lblAMEOffice.Text = customerDetails.AMEBlockOffice;
        lblVisitDate.Text = Convert.ToDateTime(customerDetails.Cust_AMEVisitDate).ToString("dd MMM yyyy");

        //Get customer material details by customer id
        IList<CustomerMaterialMapDTO> lstCustMaterial = ESalesUnityContainer.Container.Resolve<ICustomerMaterialService>()
         .GetCustomerMaterialDetailsByCustomerId(customerId);
        grdCustomerMaterialMapping.DataSource = lstCustMaterial;
        grdCustomerMaterialMapping.DataBind();

        //Get customer document details by customer id
        IList<CustomerDocDetailsDTO> lstCustomerDocuments = new List<CustomerDocDetailsDTO>();
        lstCustomerDocuments = ESalesUnityContainer.Container.Resolve<ICustomerDocService>()
            .GetCustomerDocumentDetails(customerId);
        grdDocument.DataSource = lstCustomerDocuments;
        grdDocument.DataBind();
    }

    /// <summary>
    /// Get truck details for customer linked trucks by customer id
    /// </summary>
    /// <param name="customerId"></param>
    private void GetTruckDetails(int customerId)
	{
		IList<TruckDetailsDTO> lstTruckDetails = ESalesUnityContainer.Container.Resolve<ITruckService>()
            .GetTruckDetailsForCustomer(customerId);
        		
        if (lstTruckDetails.Count > 0)
		{
			rptList.DataSource = lstTruckDetails;
			rptList.DataBind();
		}
	}

	protected void rptTruck_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		{
			Label lblTruck = (Label)e.Item.FindControl("txtTruck_Id");
            if (lblTruck != null)
			{
                GridView grdTruckDoc = (GridView)e.Item.FindControl("grdTruckDocument");
                grdTruckDoc.DataSource = ESalesUnityContainer.Container.Resolve<ITruckDocService>()
                    .GetTruckDocDetailsByTruckId(Convert.ToInt32(lblTruck.Text));
                grdTruckDoc.DataBind();
			}
		}
	}

    /// <summary>
    /// Get auth rep details by customer id
    /// </summary>
    /// <param name="customerId"></param>
    private void GetAuthRepDetails(int customerId)
    {
        IList<AuthRepDTO> objAuthRepDetails = ESalesUnityContainer.Container.Resolve<IAuthRepService>()
            .GetAuthRepDetailsForCustomer(customerId);

        if (objAuthRepDetails.Count > 0)
        {
            rptAuthRep.DataSource = objAuthRepDetails;
            rptAuthRep.DataBind();
        }
    }

	protected void rptAuthRep_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		{
			Label lblAuthRep = (Label)e.Item.FindControl("txtAuthID");
            if (lblAuthRep != null)
			{
                GridView grdAuthRepDoc = (GridView)e.Item.FindControl("grdAuthRepDocument");
                grdAuthRepDoc.DataSource = ESalesUnityContainer.Container.Resolve<IAuthRepService>()
                    .GetAuthRepDocDetailsByAuthRepId(Convert.ToInt32(lblAuthRep.Text));
                grdAuthRepDoc.DataBind();
			}
		}
	}

	protected void grdDocument_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		if (e.CommandName == Globals.GridCommandEvents.VIEWDOC)
		{
			Session[Globals.StateMgmtVariables.DOCID] = e.CommandArgument;
			Session[Globals.StateMgmtVariables.DOCTYPE] = 1;
			String ImageUrl = "../ImageHandler/ImagePopUp.aspx ";
			ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}', 'newwindow','toolbar=yes,location=no,menubar=no,width=950,height=500,resizable=yes,scrollbars=yes,top=50,left=50');</script>", ImageUrl));
		}
	}

	protected void grdTruckDocument_RowCommand(object sender, GridViewCommandEventArgs e)
	{
        if (e.CommandName == Globals.GridCommandEvents.VIEWDOC)
		{
            Session[Globals.StateMgmtVariables.DOCID] = e.CommandArgument;
            Session[Globals.StateMgmtVariables.DOCTYPE] = 2;
			String ImageUrl = "../ImageHandler/ImagePopUp.aspx ";
			ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}', 'newwindow','toolbar=yes,location=no,menubar=no,width=950,height=500,resizable=yes,scrollbars=yes,top=50,left=50');</script>", ImageUrl));
		}
	}

	protected void grdAuthRepDocument_RowCommand(object sender, GridViewCommandEventArgs e)
	{
        if (e.CommandName == Globals.GridCommandEvents.VIEWDOC)
		{
            Session[Globals.StateMgmtVariables.DOCID] = e.CommandArgument;
            Session[Globals.StateMgmtVariables.DOCTYPE] = 3;
			String ImageUrl = "../ImageHandler/ImagePopUp.aspx ";
			ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}', 'newwindow','toolbar=yes,location=no,menubar=no,width=950,height=500,resizable=yes,scrollbars=yes,top=50,left=50');</script>", ImageUrl));
		}
	}

    protected void btnSaveAndUpload_Click(object sender, EventArgs e)
	{
        if (filAuthDoc.PostedFile.ContentLength > Convert.ToInt32(Globals.ConfigVariables.MAXREQUESTLENGTH))
        {
            customValidator.IsValid = false;
            ucMessageBox.ShowMessage(Messages.MaximumUploadLimit);
        }

        if (!filAuthDoc.HasFile)
        {
            customValidator.IsValid = false;
            ucMessageBox.ShowMessage(Messages.NoUploadDocument);
        }

        if(Page.IsValid)
        {
            ESalesUnityContainer.Container.Resolve<ICustAuthorizationService>()
                .SaveAndUpdateCustomerAuthorizationDetails(InitializeCustomerAuthorizationDetails());

            CustomerDTO customerDetails = MasterList.GetCustomerDetailsById(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]));
			customerDetails.Cust_Status = true;
            
            ESalesUnityContainer.Container.Resolve<ICustomerService>().SaveAndUpdateCustomerDetails(customerDetails, null);
            ucMessageBoxForGrid.ShowMessage(Resources.Messages.CustomerDetailsSavedSuccessfully);
		}
	}

	private CustAuthorizationDetailDTO InitializeCustomerAuthorizationDetails()
	{
		CustAuthorizationDetailDTO custAuthorizationDetail = new CustAuthorizationDetailDTO();
        custAuthorizationDetail.CustAuth_CustId = (Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]));
		custAuthorizationDetail.CustAuth_Date = DateTime.Now;
        custAuthorizationDetail.CustAuth_Status = true;
        custAuthorizationDetail.CustAuth_CreatedBy = GetCurrentUserId();
        custAuthorizationDetail.CustAuth_CreatedDate = DateTime.Now;

        //If fileupload control has file
        if (filAuthDoc.HasFile)
		{
            string uploadFilePath = Path.Combine(Server.MapPath("../CustomerAuthImages"), filAuthDoc.FileName);
            filAuthDoc.SaveAs(uploadFilePath);
            
            custAuthorizationDetail.CustAuth_File = ImageToBlob.ConvertImageToByteArray(uploadFilePath);
            custAuthorizationDetail.CustAuth_FileName = filAuthDoc.FileName;

            //Delete the file from application folder after converting into byte array
            File.Delete(uploadFilePath);
		}        
        return custAuthorizationDetail;
	}

    protected void btnClose_Click(object sender, EventArgs e)
    {
        string pageToRedirect = string.Empty;
        int viewCustomerSource = Convert.ToInt32(Session[Globals.StateMgmtVariables.VIEWCUSTOMERSOURCE]);
        
        //If Page source is Activate customer screen
        if (viewCustomerSource == 1)
        {
            pageToRedirect = "ActivateCustomers.aspx";
        }
        //If Page source is View customer screen
        else
        {
            pageToRedirect = "ViewCustomer.aspx";
        }
        Response.Redirect(pageToRedirect);
    }

    protected void btnAuthCertificate_Click(object sender, EventArgs e)
    {
        Session[Globals.StateMgmtVariables.DOCID] = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]);
        Session[Globals.StateMgmtVariables.DOCTYPE] = 4;
        String ImageUrl = "../ImageHandler/ImagePopUp.aspx ";
        ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}', 'newwindow','toolbar=yes,location=no,menubar=no,width=950,height=500,resizable=yes,scrollbars=yes,top=50,left=50');</script>", ImageUrl));
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        Response.Redirect("ViewCustomer.aspx");
    }
}