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

public partial class CustomerRegistration_UserControls_ManageAuthorizedRepresentative : BaseUserControl
{
    public ShowDataEventHandler Event_AddAuthRepDetails;
    public ShowDataByIdEventHandler Event_EditAuthRepDetails;

    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateMandatoryDocumentsList();
        }
    }
    private void FillBlankGrid()
    {
        ShowBlankRowInGrid<AuthRepDTO>(grdManageAuthRep);
    }

    private void PopulateMandatoryDocumentsList()
    {
        ddlMandatoryDoc.DataSource = ESalesUnityContainer.Container.Resolve<IDocumentTypeService>().GetUniqueDocumentTypeList();
        ddlMandatoryDoc.DataBind();
        ddlMandatoryDoc.Items.Insert(0, new ListItem(Labels.CustomerCode, "0"));
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            FillGridWithAuthRepDetails(Convert.ToInt32(ddlMandatoryDoc.SelectedItem.Value), txtDocNumber.Text.Trim());
            ViewState[Globals.StateMgmtVariables.MANDATORYDOCTYPE] = ddlMandatoryDoc.SelectedItem.Value;
            ViewState[Globals.StateMgmtVariables.MANDATORYDOCNO] = txtDocNumber.Text.Trim();
        }
    }

    private void FillGridWithAuthRepDetails(int mandatoryDocId, string documentNo)
    {
		CustomerDTO customer = new CustomerDTO();
		if (Convert.ToInt32(mandatoryDocId) == 0)
		{
			CustomerDTO customerDetails = new CustomerDTO();
			customerDetails = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetCustomerDetailsByCode(documentNo);
			if (customerDetails.Cust_Id > 0)
			{
				customer = customerDetails;
			}
		}
		else
		{
			CustomerDocDetailsDTO doctype = new CustomerDocDetailsDTO();
			doctype = ESalesUnityContainer.Container.Resolve<ICustomerDocService>().GetCustomerByDocumentId(mandatoryDocId, documentNo);
			
			if (doctype.Cust_Doc_Customer != null)
			{
				customer = doctype.Cust_Doc_Customer;
			}
		}

		if (customer.Cust_Id>0)
        {
            IList<AuthRepDTO> lstAuthRepDetailsDTO = (ESalesUnityContainer.Container.Resolve<IAuthRepService>()
					.GetAuthRepDetailsForCustomer(customer.Cust_Id));

			ViewState[Globals.StateMgmtVariables.CUSTOMERID] = customer.Cust_Id;
			ViewState[Globals.StateMgmtVariables.CUSTFOLDERNAME] = customer.Cust_FolderName;

            if (lstAuthRepDetailsDTO.Count > 0)
            {
                grdManageAuthRep.DataSource = lstAuthRepDetailsDTO;
                grdManageAuthRep.DataBind();
            }
            else
            {
                FillBlankGrid();
            }
        }
        else
        {
            FillBlankGrid();
        }
    }

    protected IEnumerable grdManageAuthRep_MustAddARow(IEnumerable data)
    {
        //return the value
        return base.AddBlankRowInGrid<AuthRepDTO>();
    }

    protected void grdManageAuthRep_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals(Globals.GridCommandEvents.ADDNEW))
        {
            if (ViewState[Globals.StateMgmtVariables.CUSTOMERID] != null)
            {
                Event_AddAuthRepDetails(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]), false,
                    Convert.ToString(ViewState[Globals.StateMgmtVariables.CUSTFOLDERNAME]));
            }
            else
            {
                ucMessageBoxForGrid.ShowMessage(ErrorMessages.GetCustomerDetails);
            }
        }
        else if (e.CommandName == Globals.GridCommandEvents.EDITAUTHREP)
        {
            Event_EditAuthRepDetails(Convert.ToInt32(e.CommandArgument));
        }
    }

    protected void grdManageAuthRep_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        AuthRepDTO authRepDetails = ESalesUnityContainer.Container.Resolve<IAuthRepService>()
            .GetAuthRepById(Convert.ToInt32(grdManageAuthRep.DataKeys[e.RowIndex].Value));
        authRepDetails.AuthRep_IsDeleted = true;

        ESalesUnityContainer.Container.Resolve<IAuthRepService>().DeleteAuthRep(authRepDetails);
        ucMessageBoxForGrid.ShowMessage(Messages.AuthRepDeletedSuccessfully);
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        ShowDefaultManageAuthRepScreen();
    }

    public void ShowDefaultManageAuthRepScreen()
    {
        FillGridWithAuthRepDetails(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.MANDATORYDOCTYPE]),
            Convert.ToString(ViewState[Globals.StateMgmtVariables.MANDATORYDOCNO]));
    }
}