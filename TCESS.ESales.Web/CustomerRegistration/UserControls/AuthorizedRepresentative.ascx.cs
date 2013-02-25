#region Using directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
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

public partial class CustomerRegistration_UserControls_AuthorizedRepresentative : BaseUserControl
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
            BindMandatoryDocumentList();
        }
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

    public void PopulateAuthRepData(int authRepId)
    {
        AuthRepDTO authRepDetails = ESalesUnityContainer.Container.Resolve<IAuthRepService>().GetAuthRepById(authRepId);
        ViewState[Globals.StateMgmtVariables.CUSTOMERID] = authRepDetails.AuthRep_Customer.Cust_Id;
        ViewState[Globals.StateMgmtVariables.AUTHREPID] = authRepId;
        txtAuthName.Text = authRepDetails.AuthRep_Name;
        txtAuthFatherName.Text = authRepDetails.AuthRep_FatherName;
        txtAddress.Text = authRepDetails.AuthRep_Address;
        txtMobileNumber.Text = authRepDetails.AuthRep_Mobile;

        string folderPath = SetFolderPathForDocumentUpload(authRepDetails.AuthRep_Customer.Cust_FolderName);

        PopulateDocumentsList(authRepId, folderPath);

        btnSave.Text = Labels.Update;
        btnCancel.Visible = true;
        btnReturn.Visible = false;
        btnReset.Visible = false;
        btnArea.Align = "center";
    }

    public void ShowBlankScreen(int customerId, bool isFirstAuthRep, string folderName)
    {
        ViewState[Globals.StateMgmtVariables.CUSTOMERID] = customerId;
        ViewState[Globals.StateMgmtVariables.AUTHREPID] = null;

        SetFolderPathForDocumentUpload(folderName);

        ResetFields();
        
        if (!isFirstAuthRep)
        {
            btnSave.Text = Labels.SaveAndUpload;
            btnReturn.Visible = false;
            btnReset.Visible = true;
            btnCancel.Visible = true;
            btnArea.Align = "center";
        }
    }

    private void PopulateDocumentsList(int authRepId, string folderPath)
    {
        IList<AuthRepDocDetailDTO> lstAuthRepDocDetails = ESalesUnityContainer.Container.Resolve<IAuthRepService>()
             .GetAuthRepDocDetailsByAuthRepId(authRepId);

        foreach (GridViewRow row in grdDocument.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                AuthRepDocDetailDTO authRepDocDetails =
                    (from authRepDocs in lstAuthRepDocDetails
                     where authRepDocs.AuthRep_Doc_DocId == Convert.ToInt32(grdDocument.DataKeys[row.RowIndex].Value) && authRepDocs.AuthRep_Doc_IsDeleted == false
                     select authRepDocs).FirstOrDefault();

                if (authRepDocDetails != null)
                {
                    ((CheckBox)row.FindControl("chkScanComplete")).Checked = true;
                    ((TextBox)row.FindControl("txtDocNo")).Text = authRepDocDetails.AuthRep_Doc_DocNo;
                    ((HiddenField)row.FindControl("hdnAuthRepDocId")).Value = authRepDocDetails.AuthRep_Doc_Id.ToString();
                    ((TextBox)row.FindControl("txtDocExDate")).Text = string.IsNullOrEmpty(Convert.ToString(authRepDocDetails.AuthRep_Doc_ExDate)) ? string.Empty :
                        Convert.ToDateTime(authRepDocDetails.AuthRep_Doc_ExDate).ToString("dd MMM yyyy");
                    ((Label)row.FindControl("lblFileName")).Text = authRepDocDetails.AuthRep_Doc_FileName;

                    AuthRepDocumentsDTO authRepDocument = ESalesUnityContainer.Container.Resolve<IAuthRepService>()
                        .GetAuthRepDocDetailsByDocId(authRepDocDetails.AuthRep_Doc_Id);

                    if (authRepDocument.AuthRepDoc_File != null)
                    {
                        System.Drawing.Image img = ImageToBlob.ConvertByteArrayToImage(authRepDocument.AuthRepDoc_File);

                        string filePath = Path.Combine(folderPath, authRepDocDetails.AuthRep_Doc_FileName);

                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }

                        //Save the image
                        img.Save(filePath);
                    }
                }
            }
        }
    }

    private void BindMandatoryDocumentList()
    {
        grdDocument.DataSource = ESalesUnityContainer.Container.Resolve<IDocumentTypeService>().GetDocumentTypeListForAuthRep();
        grdDocument.DataBind();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetFields();
    }

    private void ResetFields()
    {
		DeleteFiles();
		txtAuthName.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtAuthFatherName.Text = string.Empty;
        txtMobileNumber.Text = string.Empty;

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
                                where F.Name.ToUpper().Contains(docAcronym)
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

    private void CheckIfPageIsValid()
    {
        bool isChecked = MasterList.CheckIfNoDocumentSelected(grdDocument, "chkScanComplete");

        if (!isChecked)
        {
            gridValidator.IsValid = false;
            ucMessageBox.ShowMessage(Messages.NoDocumentScanned);
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
                        AuthRepDTO authRepDetails = InitializeAuthRepDetails();

                        //Save Auth Rep Details
                        int authRepId = ESalesUnityContainer.Container.Resolve<IAuthRepService>()
                            .SaveAndUpdateAuthRepDetailsForCustomer(authRepDetails);

                        //Save Auth Rep Document Details
                        SaveAuthRepDocumentDetails(authRepId);

                        if (ViewState[Globals.StateMgmtVariables.AUTHREPID] != null)
                        {
                            ucMessageBoxForGrid.ShowMessage(Resources.Messages.AuthRepUpdatedSuccessfully);
                        }
                        else
                        {
                            ucMessageBox.ShowMessage(Resources.Messages.AuthRepSavedSuccessfully);
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
        if (ViewState[Globals.StateMgmtVariables.AUTHREPID] != null)
        {
            Event_CloseScreen(sender);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private AuthRepDTO InitializeAuthRepDetails()
    {
        AuthRepDTO authRepDetails = new AuthRepDTO();
        
        if (ViewState[Globals.StateMgmtVariables.AUTHREPID] != null)
        {
            authRepDetails.AuthRep_Id = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.AUTHREPID]);
        }

        authRepDetails.AuthRep_CustomerId = Convert.ToInt32(ViewState["CustId"]);
        authRepDetails.AuthRep_Name = txtAuthName.Text.Trim();
        authRepDetails.AuthRep_FatherName = txtAuthFatherName.Text.Trim();
        authRepDetails.AuthRep_Address = txtAddress.Text.Trim();
        authRepDetails.AuthRep_Mobile = txtMobileNumber.Text.Trim();
        authRepDetails.AuthRep_CreatedBy = GetCurrentUserId();

        if (authRepDetails.AuthRep_Id > 0)
        {
            authRepDetails.AuthRep_LastUpdatedDate = DateTime.Now;
        }
        else
        {
            authRepDetails.AuthRep_CreatedDate = DateTime.Now;
        }

        //return the value
        return authRepDetails;
    }

    private void SaveAuthRepDocumentDetails(int authRepId)
    {
        IList<AuthRepDocDetailDTO> listAuthRep = new List<AuthRepDocDetailDTO>();
        IList<AuthRepDocumentsDTO> listAuthRepDocument = new List<AuthRepDocumentsDTO>();

        foreach (GridViewRow row in grdDocument.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                if (((TextBox)(row.Cells[3].Controls[1])).Text != string.Empty)
                {
                    AuthRepDocDetailDTO authRepDocDetails = new AuthRepDocDetailDTO();
                    authRepDocDetails.AuthRep_Doc_AuthId = authRepId;
                    authRepDocDetails.AuthRep_Doc_DocId = Convert.ToInt32(grdDocument.DataKeys[row.RowIndex].Value);
                    authRepDocDetails.AuthRep_Doc_DocNo = ((TextBox)(row.Cells[3].Controls[1])).Text;

                    DateTimeFormatInfo dateTimeFormatterProvider = DateTimeFormatInfo.CurrentInfo.Clone() as DateTimeFormatInfo;
                    dateTimeFormatterProvider.ShortDatePattern = "dd/MM/yyyy";
                    if (((TextBox)(row.Cells[4].Controls[1])).Text != string.Empty)
                    {
                        authRepDocDetails.AuthRep_Doc_ExDate = DateTime.Parse(((TextBox)(row.Cells[4].Controls[1])).Text, dateTimeFormatterProvider);
                    }

                    AuthRepDocumentsDTO authRepDocument = new AuthRepDocumentsDTO();
                    authRepDocument.AuthRepDoc_CreatedBy = base.GetCurrentUserId();
                    authRepDocument.AuthRepDoc_CreatedDate = DateTime.Now;

                    if (((Label)(row.Cells[6].Controls[1])).Text.Trim() != string.Empty)
                    {
                        string filePath = Path.Combine(Convert.ToString(ViewState[Globals.StateMgmtVariables.FILEPATH]), ((Label)(row.Cells[6].Controls[1])).Text);
                        authRepDocument.AuthRepDoc_File = ImageToBlob.ConvertImageToByteArray(filePath);
                    }
                    else
                    {
                        authRepDocument.AuthRepDoc_File = null;
                    }

                    listAuthRepDocument.Add(authRepDocument);

                    authRepDocDetails.AuthRep_Doc_FileName = ((Label)(row.Cells[6].Controls[1])).Text;
                    authRepDocDetails.AuthRep_Doc_CreatedBy = GetCurrentUserId();
                    authRepDocDetails.AuthRep_Doc_CreatedDate = DateTime.Now;
                    authRepDocDetails.AuthRep_Doc_LastUpdatedDate = DateTime.Now;

                    listAuthRep.Add(authRepDocDetails);
                }
            }
        }

        //Save Auth Rep Document Details
        ESalesUnityContainer.Container.Resolve<IAuthRepService>().SaveAndUpdateAuthRepDocDetails(listAuthRep, listAuthRepDocument);
    }

    protected void lnkOpenFolder_Click(object sender, EventArgs e)
    {
        Process.Start(Convert.ToString(Session[Globals.StateMgmtVariables.FILEPATH]));
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

    protected void DocNoExist_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        CustomValidator customval = (CustomValidator)sender;
        GridViewRow r = (GridViewRow)customval.NamingContainer;

        if (((CheckBox)r.FindControl("chkScanComplete")).Checked)
        {
            string docNo = ((TextBox)r.FindControl("txtDocNo")).Text.Trim();
            int docId = Convert.ToInt32(grdDocument.DataKeys[r.RowIndex].Value);

            string custDocId = ((HiddenField)r.FindControl("hdnAuthRepDocId")).Value;
            int customerDocId = custDocId != string.Empty ? Convert.ToInt32(custDocId) : 0;

            if (ESalesUnityContainer.Container.Resolve<IAuthRepService>()
                .AuthRepDocumentNoExists(customerDocId, docId, docNo))
            {
                args.IsValid = false;
            }
        }
    }
    /// <summary>
    ///Event to check the dublicacy of AuthRep
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected void txtAuthName_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        AuthRepDTO objAuthRepDTO = ESalesUnityContainer.Container.Resolve<IAuthRepService>().GetAuthRepByName(txtAuthName.Text.Trim());
        if (Convert.ToInt32(objAuthRepDTO.AuthRep_Name) > 0)
        {
            args.IsValid = false;
        }
    }
}
