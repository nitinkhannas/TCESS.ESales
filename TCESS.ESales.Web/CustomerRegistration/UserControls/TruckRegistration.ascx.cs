#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Exception;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class CustomerRegistration_UserControls_TruckRegistration : BaseUserControl
{
	public ShowDataEventHandler Event_ShowCustomerDocumentRegistrationScreen;
	public CloseScreenEventHandler Event_CloseScreen;

	protected void Page_Init(object sender, EventArgs e)
	{
		ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			PopulateStates();
			PopulateTruckMakelist();
			BindMandatoryDocumentList();
            PopulateTruckregistrationType();
		}
	}

    private void PopulateTruckregistrationType()
    {
        IList<TruckRegTypeDTO> lsttruckregistrationtype = MasterList.GetTruckregTypeList();
        ddltruckregistration.DataSource = lsttruckregistrationtype;
        ddltruckregistration.DataBind();
        ddltruckregistration.Items.Insert(0, new ListItem(Messages.SelectRegistration, "0"));
    }

	/// <summary>
	/// Get List of States
	/// </summary>
	private void PopulateStates()
	{
		MasterList.GetStateList(ddlStates);
	}

	/// <summary>
	/// Gets list of active Truck make
	/// </summary>
	private void PopulateTruckMakelist()
	{
        MasterList.GetTruckMakelist(ddlTruckMake);
	}

	private string SetFolderPathForDocumentUpload(string folderName)
	{
		string folderPath = MasterList.CheckIfFolderExists(folderName, Globals.FolderDetails.FOLDERPATH);
		ViewState[Globals.StateMgmtVariables.FILEPATH] = folderPath;

		//If folder path to upload truck document does not contain a value
		lblFolderPath.Text = string.Empty;

		//Shows the folder path to upload truck documents
		lblFolderPath.Text = Messages.FilePath + folderPath;

		return folderPath;
	}

	public void ShowBlankScreen(int customerId, bool isFirstTruck, string folderName)
	{
		ViewState[Globals.StateMgmtVariables.CUSTOMERID] = customerId;
		ViewState[Globals.StateMgmtVariables.TRUCKID] = null;
		SetFolderPathForDocumentUpload(folderName);

		ResetFields();

		if (!isFirstTruck)
		{
			btnSave.Text = Labels.SaveAndUpload;
			btnReturn.Visible = false;
			btnReset.Visible = true;
			btnCancel.Visible = true;
			btnArea.Align = "center";
		}
	}

	public void PopulateTruckData(int truckId)
	{
		TruckDetailsDTO truckDetails = ESalesUnityContainer.Container.Resolve<ITruckService>().GetTruckDetailsByTruckId(truckId);

		ViewState[Globals.StateMgmtVariables.CUSTOMERID] = truckDetails.Truck_CustomerId;
		ViewState[Globals.StateMgmtVariables.TRUCKID] = truckId;
		txtTruckRegNo.Text = truckDetails.Truck_RegNo;
		txtOwnerName.Text = truckDetails.Truck_OwnerName;
		txtDriverName.Text = truckDetails.Truck_DriverName;
		ddlTruckMake.SelectedValue = truckDetails.Truck_Make.ToString();
		txtWheeler.Text = truckDetails.TruckWheeler_Type;
		txtCarryCapacity.Text = truckDetails.TruckCarryCapacity_Type;
		txtRegAddress.Text = truckDetails.Truck_Address;
		ddlStates.SelectedValue = truckDetails.Truck_State.ToString();
		txtMobileNo.Text = truckDetails.Truck_MobileNo;
		txtPhoneNo.Text = truckDetails.Truck_PhoneNo;
        txtOwnerShortAdd.Text = truckDetails.Truck_OwnerShortAdd.Trim();
        txtDriverShortAdd.Text = truckDetails.Truck_DriverShortAdd.Trim();
        if (!string.IsNullOrEmpty(truckDetails.Truck_RegType.ToString()))
            ddltruckregistration.SelectedValue = truckDetails.Truck_RegType.ToString();

		string folderPath = SetFolderPathForDocumentUpload(truckDetails.Truck_Customer.Cust_FolderName);

		PopulateDocumentsList(truckId, folderPath);

		btnSave.Text = Labels.Update;
		btnCancel.Visible = true;
		btnReturn.Visible = false;
		btnReset.Visible = false;
		btnArea.Align = "center";
	}

	private void PopulateDocumentsList(int truckId, string folderPath)
	{
		IList<TruckDocDetailsDTO> lstTruckDocDetails = ESalesUnityContainer.Container.Resolve<ITruckDocService>()
			 .GetTruckDocDetailsByTruckId(truckId);

		foreach (GridViewRow row in grdDocument.Rows)
		{
			if (row.RowType == DataControlRowType.DataRow)
			{
				TruckDocDetailsDTO truckDocDetails =
					(from truckDocs in lstTruckDocDetails
					 where truckDocs.Truck_Doc_DocId == Convert.ToInt32(grdDocument.DataKeys[row.RowIndex].Value) && truckDocs.Truck_Doc_IsDeleted == false
					 select truckDocs).FirstOrDefault();

				if (truckDocDetails != null)
				{
					((CheckBox)row.FindControl("chkScanComplete")).Checked = true;
					((TextBox)row.FindControl("txtDocNo")).Text = truckDocDetails.Truck_Doc_DocNo;
					((HiddenField)row.FindControl("hdnTruckDocId")).Value = truckDocDetails.Truck_Doc_Id.ToString();
					((TextBox)row.FindControl("txtDocExDate")).Text = string.IsNullOrEmpty(Convert.ToString(truckDocDetails.Truck_Doc_ExDate)) ? string.Empty :
						Convert.ToDateTime(truckDocDetails.Truck_Doc_ExDate).ToString("dd MMM yyyy");
					((Label)row.FindControl("lblFileName")).Text = Convert.ToString(truckDocDetails.Truck_Doc_FileName);

					TruckDocumentsDTO truckDocument = ESalesUnityContainer.Container.Resolve<ITruckDocService>()
						.GetTruckDocDetailsByTruckDocId(truckDocDetails.Truck_Doc_Id);

					if (truckDocument.TruckDoc_File != null)
					{
						System.Drawing.Image returnImage = ImageToBlob.ConvertByteArrayToImage(truckDocument.TruckDoc_File);

						string filePath = Path.Combine(folderPath, truckDocDetails.Truck_Doc_FileName);

						if (File.Exists(filePath))
						{
							File.Delete(filePath);
						}

						//Save the image
						returnImage.Save(filePath);
					}
				}
			}
		}
	}

	/// <summary>
	/// Bind Mandatory document List with all active document types
	/// </summary>
	private void BindMandatoryDocumentList()
	{
		grdDocument.DataSource = ESalesUnityContainer.Container.Resolve<IDocumentTypeService>().GetDocumentTypeListForTrucks();
		grdDocument.DataBind();
	}

	protected void btnReset_Click(object sender, EventArgs e)
	{
		ResetFields();
	}

	private void ResetFields()
	{
		DeleteFiles();
		txtDriverName.Text = string.Empty;
		txtMobileNo.Text = string.Empty;
		txtOwnerName.Text = string.Empty;
		txtPhoneNo.Text = string.Empty;
		txtRegAddress.Text = string.Empty;
		txtTruckRegNo.Text = string.Empty;
		txtCarryCapacity.Text = string.Empty;
		txtDriverShortAdd.Text = string.Empty;
		txtOwnerShortAdd.Text = string.Empty;
		ddlStates.SelectedIndex = 0;
		ddlTruckMake.SelectedIndex = 0;
		txtWheeler.Text = string.Empty;
        ddltruckregistration.SelectedIndex = 0;

		BindMandatoryDocumentList();
	}

	private void DeleteFiles()
	{
		string filePath = Convert.ToString(ViewState[Globals.StateMgmtVariables.FILEPATH]);
		string[] files = Directory.GetFiles(filePath);
		foreach (string file in files)
		{
			if (File.Exists(file))
			{
				File.Delete(file);
			}
		}
	}

	protected void DocExDate_ServerValidate(object sender, ServerValidateEventArgs args)
	{
		CustomValidator docExDateValidator = (CustomValidator)sender;
		GridViewRow row = (GridViewRow)docExDateValidator.NamingContainer;
		Int32 rowIndex = row.RowIndex;
		string acronym = ((Label)grdDocument.Rows[rowIndex].FindControl("lblAcronymName")).Text;
		CheckBox chkScan = (CheckBox)grdDocument.Rows[rowIndex].FindControl("chkScanComplete");

		//Check if checkbox for document type is checked
		if (chkScan.Checked)
		{
			TextBox txtDocExDate = (TextBox)grdDocument.Rows[rowIndex].FindControl("txtDocExDate");

			//If expiry date is not selected
			if (string.IsNullOrEmpty(txtDocExDate.Text))
			{
				args.IsValid = false;
			}
		}
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

	protected void chkScanComplete_Checked(object sender, EventArgs e)
	{
		CheckBox chkScanCompleted = (CheckBox)sender;
		GridViewRow row = (GridViewRow)chkScanCompleted.NamingContainer;
		Int32 rowIndex = row.RowIndex;
		Label fileName = ((Label)grdDocument.Rows[rowIndex].FindControl("lblFileName"));

		if (chkScanCompleted.Checked == true)
		{
			string docAcronym = ((Label)grdDocument.Rows[rowIndex].FindControl("lblAcronymName")).Text;

			CustomValidator validator = (CustomValidator)grdDocument.Rows[rowIndex].FindControl("ScanCompleteValidator");

			string filePath = Convert.ToString(ViewState[Globals.StateMgmtVariables.FILEPATH]);

			var documentName = (from F in new DirectoryInfo(filePath).GetFiles().OrderByDescending(F => F.CreationTime)
								where F.Name.ToUpper().Contains(docAcronym.ToUpper())
								select F.Name).FirstOrDefault();

			if (documentName == null)
			{
				chkScanCompleted.Checked = false;
				validator.IsValid = false;
				ucMessageBox.ShowMessage(((Label)grdDocument.Rows[rowIndex].FindControl("lblname")).Text + " document is not scanned");
			}
			else
			{
				fileName.Text = documentName;
			}
		}
		else
		{
			fileName.Text = string.Empty;
		}
	}

	private bool CheckDocumentList()
	{
		bool isChecked = MasterList.CheckIfNoDocumentSelected(grdDocument, "chkScanComplete");

		if (!isChecked)
		{
			gridValidator.IsValid = false;
			ucMessageBox.ShowMessage(Messages.NoDocumentScanned);
		}
		return isChecked;
	}

	private void CheckIfPageIsValid()
	{
		if (CheckDocumentList())
		{
			bool isChecked = MasterList.CheckMandatoryDocumentList(grdDocument, "chkDocID", "chkScanComplete");

			if (!isChecked)
			{
				gridValidator.IsValid = false;
				ucMessageBox.ShowMessage(Messages.BlankMandatoryDocList);
			}
		}
	}

	protected void btnSave_Click(object sender, EventArgs e)
	{
		CheckIfPageIsValid();

		if (Page.IsValid)
		{
			try
			{
				ExceptionHandler.AppExceptionManager.Process(() =>
				{
					TruckDetailsDTO truckDetails = InitializeTruckDetails();

					//Save Truck Details
					int truckId = ESalesUnityContainer.Container.Resolve<ITruckService>().SaveAndUpdateTruckDetailsForCustomer(truckDetails);

					SaveDocumentListForTrucks(truckId);

					if (ViewState[Globals.StateMgmtVariables.TRUCKID] != null)
					{
						ucMessageBoxForGrid.ShowMessage(Messages.TruckUpdatedSuccessfully);
					}
					else
					{
						ucMessageBox.ShowMessage(Messages.TruckSavedSuccessfully);
						//Reset all the controls
						ResetFields();
					}
				}, Globals.ExceptionTypes.ExceptionShielding.ToString());
			}
			catch (Exception ex)
			{
			}
		}
	}

	private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
	{
		if (ViewState[Globals.StateMgmtVariables.TRUCKID] != null)
		{
			Event_CloseScreen(sender);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	private TruckDetailsDTO InitializeTruckDetails()
	{
		TruckDetailsDTO truckDetails = new TruckDetailsDTO();
		if (ViewState[Globals.StateMgmtVariables.TRUCKID] != null)
		{
			truckDetails.Truck_Id = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.TRUCKID]);
		}

		truckDetails.Truck_CustomerId = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]);
		truckDetails.Truck_RegNo = txtTruckRegNo.Text.Trim();
		truckDetails.Truck_OwnerName = txtOwnerName.Text.Trim();
		truckDetails.Truck_DriverName = txtDriverName.Text.Trim();
		truckDetails.Truck_Make = Convert.ToInt32(ddlTruckMake.SelectedItem.Value);
		truckDetails.Truck_Address = txtRegAddress.Text.Trim();
		truckDetails.Truck_OwnerShortAdd = txtOwnerShortAdd.Text.Trim();
		truckDetails.Truck_DriverShortAdd = txtDriverShortAdd.Text.Trim();
		truckDetails.Truck_State = Convert.ToInt32(ddlStates.SelectedItem.Value);
		truckDetails.Truck_MobileNo = txtMobileNo.Text.Trim();
		truckDetails.Truck_PhoneNo = txtPhoneNo.Text.Trim();
		truckDetails.Truck_CreatedBy = GetCurrentUserId();
        truckDetails.Truck_DriverShortAdd = txtDriverShortAdd.Text.Trim();
        truckDetails.Truck_OwnerShortAdd = txtOwnerShortAdd.Text.Trim();
        truckDetails.Truck_RegType = Convert.ToInt32(ddltruckregistration.SelectedItem.Value);

		if (truckDetails.Truck_Id > 0)
		{
			truckDetails.Truck_LastUpdatedDate = DateTime.Now;
		}
		else
		{
			truckDetails.Truck_CreatedDate = DateTime.Now;
		}

		//return the value
		return truckDetails;
	}

	private void SaveDocumentListForTrucks(int truckId)
	{
		IList<TruckDocDetailsDTO> listTruckDetail = new List<TruckDocDetailsDTO>();
		IList<TruckDocumentsDTO> listTruckDocument = new List<TruckDocumentsDTO>();

		foreach (GridViewRow row in grdDocument.Rows)
		{
			if (row.RowType == DataControlRowType.DataRow)
			{
				if (((TextBox)(row.Cells[3].Controls[1])).Text != string.Empty)
				{
					TruckDocDetailsDTO truckDocDetail = new TruckDocDetailsDTO();
					truckDocDetail.Truck_Doc_TruckId = truckId;
					truckDocDetail.Truck_Doc_DocId = Convert.ToInt32(grdDocument.DataKeys[row.RowIndex].Value);
					truckDocDetail.Truck_Doc_DocNo = ((TextBox)(row.Cells[3].Controls[1])).Text;

					DateTimeFormatInfo dateTimeFormatterProvider = DateTimeFormatInfo.CurrentInfo.Clone() as DateTimeFormatInfo;
					dateTimeFormatterProvider.ShortDatePattern = "dd/MM/yyyy";
					truckDocDetail.Truck_Doc_ExDate = DateTime.Parse(((TextBox)(row.Cells[4].Controls[1])).Text, dateTimeFormatterProvider);

					TruckDocumentsDTO truckDocument = new TruckDocumentsDTO();
					truckDocument.TruckDoc_CreatedBy = base.GetCurrentUserId();
					truckDocument.TruckDoc_CreatedDate = DateTime.Now;

					if (((Label)(row.Cells[6].Controls[1])).Text.Trim() != string.Empty)
					{
						string filePath = Path.Combine(Convert.ToString(ViewState[Globals.StateMgmtVariables.FILEPATH]), ((Label)(row.Cells[6].Controls[1])).Text);
						truckDocument.TruckDoc_File = ImageToBlob.ConvertImageToByteArray(filePath);
					}
					else
					{
						truckDocument.TruckDoc_File = null;
					}

					listTruckDocument.Add(truckDocument);

					truckDocDetail.Truck_Doc_FileName = ((Label)(row.Cells[6].Controls[1])).Text;
					truckDocDetail.Truck_Doc_CreatedBy = GetCurrentUserId();
					truckDocDetail.Truck_Doc_CreatedDate = DateTime.Now;
					truckDocDetail.Truck_Doc_LastUpdatedDate = DateTime.Now;

					listTruckDetail.Add(truckDocDetail);
				}
			}
		}
		//Save Truck Document Details
		ESalesUnityContainer.Container.Resolve<ITruckService>().SaveAndUpdateTruckDocumentDetails(listTruckDetail, listTruckDocument);
	}

	protected void grdDocument_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			TextBox txtDocExDate = (TextBox)e.Row.FindControl("txtDocExDate");
			txtDocExDate.Attributes.Add("ReadOnly", "true");
		}
	}

	protected void btnCancel_Click(object sender, EventArgs e)
	{
		Event_CloseScreen(sender);
	}

	protected void btnReturn_Click(object sender, EventArgs e)
	{
		Event_ShowCustomerDocumentRegistrationScreen(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]), false,
			string.Empty);
	}

	protected void ddlTruckMake_SelectedIndexChanged(object sender, EventArgs e)
	{
		int truckMakeId = Convert.ToInt32(ddlTruckMake.SelectedItem.Value);
		if (truckMakeId > 0)
		{
            TruckMakeDTO truckMakeDetails = MasterList.GetTruckMakeById(truckMakeId);
			txtWheeler.Text = truckMakeDetails.TruckMake_TruckWheeler_Value;
			txtCarryCapacity.Text = truckMakeDetails.TruckMake_TruckCC_Value;
		}
	}

	protected void DocNoExist_ServerValidate(object sender, ServerValidateEventArgs args)
	{
		CustomValidator customval = (CustomValidator)sender;
		GridViewRow r = (GridViewRow)customval.NamingContainer;

		if (((CheckBox)r.FindControl("chkScanComplete")).Checked)
		{
			string docNo = ((TextBox)r.FindControl("txtDocNo")).Text.Trim();
			int docId = Convert.ToInt32(grdDocument.DataKeys[r.RowIndex].Value);

			string truckDocId = ((HiddenField)r.FindControl("hdnTruckDocId")).Value;
			int truckDocumentId = truckDocId != string.Empty ? Convert.ToInt32(truckDocId) : 0;

			if (ESalesUnityContainer.Container.Resolve<ITruckDocService>().TruckDocumentNoExists(truckDocumentId, docId, docNo))
			{
				args.IsValid = false;
			}
		}
	}
    /// <summary>
    /// Event to check truck with TruckRegNo alredy exist or not
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected void txtTruckRegNo_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        TruckDetailsDTO truckDetails = ESalesUnityContainer.Container.Resolve<ITruckService>()
            .GetTruckDetailsByTruckRegistrationId(txtTruckRegNo.Text.Trim());

        if (Convert.ToInt32(truckDetails.Truck_Id) == 0)
        {
            if (truckDetails.Truck_RegNo != null)
            {
                args.IsValid = false;
            }
        }
    }
}