#region Namespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCESS.ESales.CommonLayer.CommonLibrary;
using System.Web.Security;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using Microsoft.Practices.Unity;
using System.Collections;
using Resources;
using System.IO;
using System.Globalization;

#endregion


public partial class CustomerRegistration_EditCoustomerDocuments : BasePage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			PopulateMandatoryDocuments();
		}
	}
	private void PopulateMandatoryDocuments()
	{

		ddlAllDocument.DataSource = ESalesUnityContainer.Container.Resolve<IDocumentTypeService>().GetDocumentTypeListForCustomers();
		ddlAllDocument.DataBind();
		ddlAllDocument.Items.Insert(0, new ListItem(Labels.DocumentName, "0"));
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
	private void FillGridWithCustomerDetails(int mandatoryDocId, string documentNo)
	{
		IList<CustomerDTO> lstCustomerDTO = new List<CustomerDTO>();
        CustomerDTO customerDetails = null;
		if (Convert.ToInt32(mandatoryDocId) == 0)
		{
			customerDetails = new CustomerDTO();
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

			CustomerDocDetailsDTO custDocDetails = ESalesUnityContainer.Container.Resolve<ICustomerDocService>()
			 .GetCustomerDocumentDetailsByDocIdAndCustId(Convert.ToInt32(lstCustomerDTO[0].Cust_Id), Convert.ToInt32(ddlAllDocument.SelectedItem.Value));

			if (custDocDetails.Cust_Doc_Id > 0)
			{
				LoadDocumentGrid();
				ViewState[Globals.StateMgmtVariables.CUSTOMERID] = custDocDetails.Cust_Doc_CustId;
				foreach (GridViewRow row in grdDocument.Rows)
				{
					if (row.RowType == DataControlRowType.DataRow)
					{

						if (custDocDetails != null)
						{

							((TextBox)row.FindControl("txtDocNo")).Text = custDocDetails.Cust_Doc_No;
							((HiddenField)row.FindControl("hdnCustDocId")).Value = custDocDetails.Cust_Doc_Id.ToString();
							((TextBox)row.FindControl("txtDocExDate")).Text = string.IsNullOrEmpty(Convert.ToString(custDocDetails.Cust_Doc_ExDate)) ? string.Empty :
								Convert.ToDateTime(custDocDetails.Cust_Doc_ExDate).ToString("dd MMM yyyy");
						}
					}
				}
				grdManageCustomers.DataSource = lstCustomerDTO;
				grdManageCustomers.DataBind();
				//ViewState[Globals.StateMgmtVariables.TRADENAME] = lstCustomerDTO[0].Cust_TradeName;
			}
			else
			{
				grdManageCustomers.DataSource = lstCustomerDTO;
				grdManageCustomers.DataBind();
				CustomerDocDetailsDTO custDocDetailsNew = ESalesUnityContainer.Container.Resolve<ICustomerDocService>()
							 .GetCustomerDocumentDetailsByDocIdAndCustId(Convert.ToInt32(lstCustomerDTO[0].Cust_Id), Convert.ToInt32(ddlMandatoryDoc.SelectedItem.Value));
				LoadDocumentGrid();
                ViewState[Globals.StateMgmtVariables.CUSTOMERID] = customerDetails.Cust_Id;
			}
		}
		else
		{
			FillBlankGrid();
		}
	}
	private void FillBlankGrid()
	{
		 base.ShowBlankRowInGrid<CustomerDocDetailsDTO>(grdDocument);
		 base.ShowBlankRowInGrid<CustomerDTO>(grdManageCustomers);
	}
	/// <summary>
	/// Loads Customer Document Details from database
	/// </summary>
	private void LoadDocumentGrid()
	{
		grdDocument.DataSource = ESalesUnityContainer.Container.Resolve<IDocumentTypeService>().GetDocumentTypeListForCustomers().Where(F => F.Doc_Id == Convert.ToInt32(ddlAllDocument.SelectedItem.Value));
		grdDocument.DataBind();
	}
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	private void SaveDocumentListForCustomer()
	{
		IList<CustomerDocDetailsDTO> listDocDetail = new List<CustomerDocDetailsDTO>();
		IList<CustomerDocumentsDTO> listCustDocument = new List<CustomerDocumentsDTO>();

		foreach (GridViewRow row in grdDocument.Rows)
		{
			if (row.RowType == DataControlRowType.DataRow)
			{
				if (((TextBox)(row.Cells[3].Controls[1])).Text != string.Empty)
				{
					CustomerDocDetailsDTO custDocDetails = new CustomerDocDetailsDTO();
					custDocDetails.Cust_Doc_CustId = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]);
					custDocDetails.Cust_Doc_DocId = Convert.ToInt32(grdDocument.DataKeys[row.RowIndex].Value);
					custDocDetails.Cust_Doc_No = ((TextBox)(row.Cells[3].Controls[1])).Text;

					DateTimeFormatInfo dateTimeFormatterProvider = DateTimeFormatInfo.CurrentInfo.Clone() as DateTimeFormatInfo;
					dateTimeFormatterProvider.ShortDatePattern = "dd/MM/yyyy";
					if (((TextBox)(row.Cells[4].Controls[1])).Text != string.Empty)
					{
						custDocDetails.Cust_Doc_ExDate = DateTime.Parse(((TextBox)(row.Cells[4].Controls[1])).Text, dateTimeFormatterProvider);
					}

					CustomerDocumentsDTO custDocument = new CustomerDocumentsDTO();
					custDocument.CustDoc_CreatedBy = base.GetCurrentUserId();
					custDocument.CustDoc_CreatedDate = DateTime.Now;

					//If fileupload control has file
					if (filAuthDoc.HasFile)
					{
						string uploadFilePath = Path.Combine(Server.MapPath("../CustomerAuthImages"), filAuthDoc.FileName);
						filAuthDoc.SaveAs(uploadFilePath);

						custDocument.CustDoc_File = ImageToBlob.ConvertImageToByteArray(uploadFilePath);
						custDocDetails.Cust_Doc_FileName = filAuthDoc.FileName;
						//Delete the file from application folder after converting into byte array
						File.Delete(uploadFilePath);
					}
					else
					{
						custDocument.CustDoc_File = null;
					}

					listCustDocument.Add(custDocument);

					custDocDetails.Cust_Doc_CreatedBy = GetCurrentUserId();
					custDocDetails.Cust_Doc_CreatedDate = DateTime.Now;
					custDocDetails.Cust_Doc_LastUpdatedDate = DateTime.Now;

					listDocDetail.Add(custDocDetails);
				}
			}
		}

		//Save Customer Document Details
		ESalesUnityContainer.Container.Resolve<ICustomerService>().EditCustomerDocumentDetails(listDocDetail, listCustDocument);
	}
	protected void DocNo_ServerValidate(object sender, ServerValidateEventArgs args)
	{
		CustomValidator docNoValidator = (CustomValidator)sender;
		GridViewRow row = (GridViewRow)docNoValidator.NamingContainer;
		Int32 rowIndex = row.RowIndex;

		CheckBox chkScan = (CheckBox)grdDocument.Rows[rowIndex].FindControl("chkScanComplete");

		// Check to see if it has already been validated.
		if (chkScan.Checked)
		{
			TextBox txtDocNo = (TextBox)grdDocument.Rows[rowIndex].FindControl("txtDocNo");

			if (string.IsNullOrEmpty(txtDocNo.Text))
			{
				args.IsValid = false;
			}
		}
	}
	protected void DocExDate_ServerValidate(object sender, ServerValidateEventArgs args)
	{
		CustomValidator docExDateValidator = (CustomValidator)sender;
		GridViewRow row = (GridViewRow)docExDateValidator.NamingContainer;
		Int32 rowIndex = row.RowIndex;

		string acronym = ((Label)grdDocument.Rows[rowIndex].FindControl("lblAcronymName")).Text;

		if (acronym == Globals.AcronymType.REGN || acronym == Globals.AcronymType.PUC)
		{
			CheckBox chkScan = (CheckBox)grdDocument.Rows[rowIndex].FindControl("chkScanComplete");

			if (chkScan.Checked)
			{
				TextBox txtDocExDate = (TextBox)grdDocument.Rows[rowIndex].FindControl("txtDocExDate");

				if (string.IsNullOrEmpty(txtDocExDate.Text))
				{
					args.IsValid = false;
				}
			}
		}
	}
	protected void DocNoExist_ServerValidate(object sender, ServerValidateEventArgs args)
	{
		CustomValidator customval = (CustomValidator)sender;
		GridViewRow r = (GridViewRow)customval.NamingContainer;
		string docNo = ((TextBox)r.FindControl("txtDocNo")).Text.Trim();
		int docId = Convert.ToInt32(grdDocument.DataKeys[r.RowIndex].Value);

		string custDocId = ((HiddenField)r.FindControl("hdnCustDocId")).Value;
		int customerDocId = custDocId != string.Empty ? Convert.ToInt32(custDocId) : 0;

		if (ESalesUnityContainer.Container.Resolve<ICustomerDocService>().CustomerDocumentNoExists(customerDocId, docId, docNo))
		{
			args.IsValid = false;
		}

	}
	protected void btnSaveAndUpload_Click(object sender, EventArgs e)
	{
		if (filAuthDoc.HasFile)
		{
			SaveDocumentListForCustomer();
		}
		Response.Redirect(Request.Url.AbsoluteUri);
	}
	protected void btnClose_Click(object sender, EventArgs e)
	{
		ResetFiled();
	}
	private void ResetFiled()
	{
		PopulateMandatoryDocuments();
		txtDocNumber.Text = string.Empty;
		FillBlankGrid();
	}
}