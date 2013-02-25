using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.BusinessLayer.Services;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using System.Linq;
using Resources;
using System.Collections;
using System.IO;

public partial class CustomerRegistration_UserControls_ManageCustomers : BaseUserControl
{
	public event ShowDataByIdEventHandler Event_ShowCustomerRegistrationScreen;
	public event ShowDataEventHandler Event_ShowCustomerDocumentRegistrationScreen;

	protected void Page_Init(object sender, EventArgs e)
	{
		ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			PopulateMandatoryDocuments();
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
			FillGridWithCustomerDetails(Convert.ToInt32(ddlMandatoryDoc.SelectedItem.Value), txtDocNumber.Text.Trim());
			ViewState[Globals.StateMgmtVariables.MANDATORYDOCTYPE] = ddlMandatoryDoc.SelectedItem.Value;
			ViewState[Globals.StateMgmtVariables.MANDATORYDOCNO] = txtDocNumber.Text.Trim();
		}
	}

	/// <summary>
	/// 
	/// </summary>
	private void FillBlankGrid()
	{
		ShowBlankRowInGrid<CustomerDTO>(grdManageCustomers);
	}

	/// <summary>
	/// 
	/// </summary>
	private void FillGridWithCustomerDetails(int mandatoryDocId, string documentNo)
	{
		IList<CustomerDTO> lstCustomerDTO = new List<CustomerDTO>();

		if (Convert.ToInt32(mandatoryDocId) == 0)
		{
			CustomerDTO customerDetails = new CustomerDTO();
			customerDetails = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetCustomerDetailsByCode(documentNo);
			if (customerDetails.Cust_Id > 0)
			{
				lstCustomerDTO.Add(customerDetails);
			}
			
		}
		else
		{
			CustomerDocDetailsDTO docDetails = new CustomerDocDetailsDTO();
			docDetails = ESalesUnityContainer.Container.Resolve<ICustomerDocService>().GetCustomerByDocumentId(mandatoryDocId, documentNo);

			if (docDetails.Cust_Doc_Customer != null)
			{
				lstCustomerDTO.Add(docDetails.Cust_Doc_Customer);
			}
		}

		if (lstCustomerDTO.Count > 0)
		{

			grdManageCustomers.DataSource = lstCustomerDTO;
			grdManageCustomers.DataBind();
			ViewState[Globals.StateMgmtVariables.TRADENAME] = lstCustomerDTO[0].Cust_TradeName;
		}
		else
		{
			FillBlankGrid();
		}
	}

	protected IEnumerable grdManageCustomers_MustAddARow(IEnumerable data)
	{
		//return the value
		return base.AddBlankRowInGrid<CustomerDTO>();
	}

	protected void grdManageCustomers_RowDeleting(object sender, GridViewDeleteEventArgs e)
	{
		ESalesUnityContainer.Container.Resolve<ICustomerService>().
			DeleteCustomer(Convert.ToInt32(grdManageCustomers.DataKeys[e.RowIndex].Value));
		ucMessageBoxForGrid.ShowMessage(Resources.Messages.CustomerDetailsDeletedSuccessfully);
	}

	protected void grdManageCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		if (e.CommandName == Globals.GridCommandEvents.EDITCUSTOMER)
		{
			Event_ShowCustomerRegistrationScreen(Convert.ToInt32(e.CommandArgument));
		}
		else if (e.CommandName == Globals.GridCommandEvents.EDITDOCUMENT)
		{
			Event_ShowCustomerDocumentRegistrationScreen(Convert.ToInt32(e.CommandArgument), true, string.Empty);
		}
	}

	public void ShowDefaultManageCustomerScreen()
	{
		FillGridWithCustomerDetails(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.MANDATORYDOCTYPE]),
			Convert.ToString(ViewState[Globals.StateMgmtVariables.MANDATORYDOCNO]));
	}

	private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
	{
		FillGridWithCustomerDetails(Convert.ToInt32(ddlMandatoryDoc.SelectedItem.Value), txtDocNumber.Text.Trim());
	}
}