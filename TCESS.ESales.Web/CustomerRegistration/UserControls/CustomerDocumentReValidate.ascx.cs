#region Namespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCESS.ESales.CommonLayer.CommonLibrary;
using System.Collections;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.DataTransferObjects;
using Microsoft.Practices.Unity;
using System.IO;
using Resources;
using System.Globalization;
using TCESS.ESales.CommonLayer.Exception;
using System.Configuration;
using System.Text.RegularExpressions;

#endregion


public partial class CustomerRegistration_UserControls_CustomerDocumentReValidate : BaseUserControl
{
    //public event ShowDataEventHandler Event_ShowAddTruckScreen;
    public event ShowDataEventHandler Event_ShowParnetScreen;
    public event ShowDataByIdEventHandler Event_ShowCustomerReport;
    public event CloseScreenEventHandler Event_CloseScreen;
    public event CloseScreenEventHandler Event_ShowCustomerRegistrationScreen;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDocumentGrid();
        }
    }

    private void LoadMaterialTypes(int customerId)
    {
        IList<CustomerMaterialMapDTO> lstCustMaterial = ESalesUnityContainer.Container.Resolve<ICustomerMaterialService>()
         .GetCustomerMaterialDetails(customerId);

        grdCustomerMaterialMapping.DataSource = lstCustMaterial;
        grdCustomerMaterialMapping.DataBind();
    }

    public void PopulateCustomerDetails(int customerId, bool isEdit)
    {
        if (!IsPostBack)
        {
            LoadDocumentGrid();
        }
        CustomerDTO customerDetails = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetCustomerDetails(customerId);

        ViewState[Globals.StateMgmtVariables.CUSTOMERID] = customerId;

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
        if (customerDetails.Cust_OwnershipStatus != 1)
        { btnAddPatInfo.Visible = true; }
        lblBusinessType.Text = customerDetails.Cust_Business_Name;
        lblSalesType.Text = customerDetails.Cust_SalesType == 1 ? Labels.WithinJharkhand : Labels.OutsideJharkhand;
        lblPartnerMobileNumber.Text = customerDetails.Cust_PartnerPhoneNo;
        lblAMEOffice.Text = customerDetails.AMEBlockOffice;
        lblVisitDate.Text = Convert.ToDateTime(customerDetails.Cust_AMEReVisitDate).ToString("dd MMM yyyy");
        lblNoOfChimney.Text = Convert.ToString(customerDetails.Cust_NoOfChimneys);
        lblBrickCapacity.Text = Convert.ToString(customerDetails.Cust_BrickCapacity);
        lblExciseRange.Text = customerDetails.Cust_Excise_Range;
        lblExciseDiv.Text = customerDetails.Cust_Excise_Div;
        lblExciseComm.Text = customerDetails.Cust_Excise_Comm;
        lblBankName.Text = customerDetails.Cust_BankName;
        lblAccountNo.Text = Convert.ToString(customerDetails.Cust_BankAccountNo);
        lblBankBranch.Text = customerDetails.Cust_BankBranch;
        lblChequeNo.Text = Convert.ToString(customerDetails.Cust_BankChequeNo);
        lblAccountType.Text = customerDetails.Cust_BankAccountType == 1 ? "Saving" : "Current";
        lblIFSCCode.Text = customerDetails.Cust_BankIFCICode;
        lblVATReturn.Text = Convert.ToDateTime(customerDetails.Cust_VATFiledON).ToString("dd MMM yyyy");
        lblUnitStatus.Text = customerDetails.Cust_UnitStatus == 1 ? "Working" : "Not Working";
        lblAMEVisitName.Text = customerDetails.Cust_AMEName;
        LoadMaterialTypes(customerId);

        string folderPath = MasterList.CheckIfFolderExists(customerDetails.Cust_FolderName, Globals.FolderDetails.FOLDERPATH);
        ViewState[Globals.StateMgmtVariables.CUSTFOLDERNAME] = customerDetails.Cust_FolderName;
        ViewState[Globals.StateMgmtVariables.FILEPATH] = folderPath;
        //If folder path to upload truck document does not contain a value
        lblFolderPath.Text = string.Empty;

        //Shows the folder path to upload truck documents
        lblFolderPath.Text = Messages.FilePath + folderPath;

        PopulateDocumentsList(customerId, folderPath);

        if (isEdit == true)
        {
            btnRightArea.Align = "center";
            btnCancel.Visible = true;
            btnSave.Text = Labels.Update;
            // btnAddPatInfo.Visible = false;
            btnNewRegistration.Visible = false;
        }
    }

    private void PopulateDocumentsList(int customerId, string folderPath)
    {
        IList<CustomerDocDetailsDTO> listCustDocDetail = ESalesUnityContainer.Container.Resolve<ICustomerDocService>()
             .GetCustomerDocumentDetails(customerId);

        if (listCustDocDetail.Count > 0)
        {
            foreach (GridViewRow row in grdDocument.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CustomerDocDetailsDTO custDocDetails =
                        (from custDocs in listCustDocDetail
                         where custDocs.Cust_Doc_DocId == Convert.ToInt32(grdDocument.DataKeys[row.RowIndex].Value)
                         && custDocs.Cust_Doc_IsDeleted == false
                         select custDocs).FirstOrDefault();

                    if (custDocDetails != null)
                    {
                        // ((CheckBox)row.FindControl("chkScanComplete")).Checked = true;
                        ((TextBox)row.FindControl("txtDocNo")).Text = custDocDetails.Cust_Doc_No;
                        ((HiddenField)row.FindControl("hdnCustDocId")).Value = custDocDetails.Cust_Doc_Id.ToString();
                        ((TextBox)row.FindControl("txtDocExDate")).Text = string.IsNullOrEmpty(Convert.ToString(custDocDetails.Cust_Doc_ExDate)) ? string.Empty :
                            Convert.ToDateTime(custDocDetails.Cust_Doc_ExDate).ToString("dd MMM yyyy");
                        //  ((Label)row.FindControl("lblFileName")).Text = Convert.ToString(custDocDetails.Cust_Doc_FileName);

                        //CustomerDocumentsDTO custDocument = ESalesUnityContainer.Container.Resolve<ICustomerDocService>()
                        //    .GetCustomerDocumentDetailsByCustDocId(custDocDetails.Cust_Doc_Id);

                        //if (custDocument.CustDoc_File != null)
                        //{
                        //    System.Drawing.Image returnImage = ImageToBlob.ConvertByteArrayToImage(custDocument.CustDoc_File);

                        //    string filePath = Path.Combine(folderPath, custDocDetails.Cust_Doc_FileName);

                        //    if (File.Exists(filePath))
                        //    {
                        //        File.Delete(filePath);
                        //    }

                        //    //Save the image
                        //    returnImage.Save(filePath);
                        // }
                    }
                }
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
    protected void PAN_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        CustomValidator docExDateValidator = (CustomValidator)sender;
        GridViewRow row = (GridViewRow)docExDateValidator.NamingContainer;
        Int32 rowIndex = row.RowIndex;

        string acronym = ((Label)grdDocument.Rows[rowIndex].FindControl("lblAcronymName")).Text;

        if (acronym == Globals.AcronymType.PAN )
        {
            CheckBox chkScan = (CheckBox)grdDocument.Rows[rowIndex].FindControl("chkScanComplete");

            if (chkScan.Checked)
            {
                TextBox txtDocNo = (TextBox)grdDocument.Rows[rowIndex].FindControl("txtDocNo");
                Regex regex = new Regex(@"^[\w]{3}(p|P|c|C|h|H|f|F|a|A|t|T|b|B|l|L|j|J|g|G)[\w][\d]{4}[\w]$");
                Match match = regex.Match(txtDocNo.Text);
                if (match.Success)
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
        }
    }
    /// <summary>
    /// Tin Number validator
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected void TIN_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        CustomValidator docExDateValidator = (CustomValidator)sender;
        GridViewRow row = (GridViewRow)docExDateValidator.NamingContainer;
        Int32 rowIndex = row.RowIndex;

        string acronym = ((Label)grdDocument.Rows[rowIndex].FindControl("lblAcronymName")).Text;

        if (acronym == Globals.AcronymType.TIN)
        {
            CheckBox chkScan = (CheckBox)grdDocument.Rows[rowIndex].FindControl("chkScanComplete");

            if (chkScan.Checked)
            {
                TextBox txtDocNo = (TextBox)grdDocument.Rows[rowIndex].FindControl("txtDocNo");
                Regex regex = new Regex(@"^[\d]{11}$");
                Match match = regex.Match(txtDocNo.Text);
                if (match.Success)
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
        }
    }

    //protected void DocExDate_ServerValidate(object sender, ServerValidateEventArgs args)
    //{
    //    CustomValidator docExDateValidator = (CustomValidator)sender;
    //    GridViewRow row = (GridViewRow)docExDateValidator.NamingContainer;
    //    Int32 rowIndex = row.RowIndex;

    //    string acronym = ((Label)grdDocument.Rows[rowIndex].FindControl("lblAcronymName")).Text;
    //    CheckBox chkScan = (CheckBox)grdDocument.Rows[rowIndex].FindControl("chkScanComplete");
    //    switch (acronym)
    //    {

    //        case Globals.AcronymType.REGN:
    //        case Globals.AcronymType.PUC:

    //            if (chkScan.Checked)
    //            {
    //                TextBox txtDocExDate = (TextBox)grdDocument.Rows[rowIndex].FindControl("txtDocExDate");

    //                if (string.IsNullOrEmpty(txtDocExDate.Text))
    //                {
    //                    args.IsValid = false;

    //                }
    //            }
    //            break;
    //        case Globals.AcronymType.PAN:
    //            if (chkScan.Checked)
    //            {
    //                TextBox txtDocNo = (TextBox)grdDocument.Rows[rowIndex].FindControl("txtDocNo");
    //                Regex regex = new Regex(@"^[\w]{4}(p|P|c|C|h|H|f|F|a|A|t|T|b|B|l|L|j|J|g|G)[\w][\d]{4}[\w]$");
    //                Match match = regex.Match(txtDocNo.Text);
    //                if (match.Success)
    //                    args.IsValid = true;
    //                else
    //                    args.IsValid = false;
    //            }
    //            break;
    //        case Globals.AcronymType.TIN:
    //            if (chkScan.Checked)
    //            {
    //                TextBox txtDocNo = (TextBox)grdDocument.Rows[rowIndex].FindControl("txtDocNo");
    //                Regex regex = new Regex(@"^[\d]{11}$");
    //                Match match = regex.Match(txtDocNo.Text);
    //                if (match.Success)
    //                    args.IsValid = true;
    //                else
    //                    args.IsValid = false;
    //            }
    //            break;
    //        default:
    //            break;
    //    }

    //}

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

            var documentName = (from F in new DirectoryInfo(Convert.ToString(ViewState[Globals.StateMgmtVariables.FILEPATH])).GetFiles()
                                where F.Name.ToUpper().Contains(docAcronym.ToUpper())
                                select F.Name).FirstOrDefault();

            if (documentName == null)
            {
                chkScanCompleted.Checked = false;
                ucMessageBox.ShowMessage(((Label)grdDocument.Rows[rowIndex].FindControl("lblname")).Text + " document is not scanned");
                validator.IsValid = false;
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
                    SaveDocumentListForCustomer();
                    ucMessageBox.ShowMessage(Messages.CustomerDocumentDetailsSavedSuccessfully);
                }, Globals.ExceptionTypes.AssistingAdministrators.ToString());
            }
            catch (Exception ex)
            {
            }

            //RememberScannedDocumentList();
            PopulateDocumentsList(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]), ViewState[Globals.StateMgmtVariables.FILEPATH].ToString());
        }
    }

    private void RememberScannedDocumentList()
    {
        ArrayList checkboxCheckedList = new ArrayList();

        foreach (GridViewRow row in grdDocument.Rows)
        {
            int rowIndex = (int)grdDocument.DataKeys[row.RowIndex].Value;
            bool result = ((CheckBox)row.FindControl("chkScanComplete")).Checked;

            if (result)
            {
                checkboxCheckedList.Add(rowIndex);
            }
        }
        ViewState[Globals.StateMgmtVariables.CHECKEDITEMS] = checkboxCheckedList;
    }

    private void LoadDocumentGrid()
    {
        IList<DocTypeDTO> lstDocType = ESalesUnityContainer.Container.Resolve<IDocumentTypeService>().GetDocumentTypeListForCustomers();
        // ConfigurationManager.AppSettings["LogFilePath"]
        lstDocType = (from f in lstDocType where ConfigurationManager.AppSettings["ValidateDocument"].Contains(f.Doc_Id.ToString()) select f).ToList<DocTypeDTO>();
        grdDocument.DataSource = lstDocType;
        grdDocument.DataBind();
    }

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

                    if (((Label)(row.Cells[6].Controls[1])).Text.Trim() != string.Empty)
                    {
                        string filePath = Path.Combine(Convert.ToString(ViewState[Globals.StateMgmtVariables.FILEPATH]),
                            ((Label)(row.Cells[6].Controls[1])).Text);
                        custDocument.CustDoc_File = ImageToBlob.ConvertImageToByteArray(filePath);
                    }
                    else
                    {
                        custDocument.CustDoc_File = null;
                    }

                    listCustDocument.Add(custDocument);

                    custDocDetails.Cust_Doc_FileName = ((Label)(row.Cells[6].Controls[1])).Text;
                    custDocDetails.Cust_Doc_CreatedBy = GetCurrentUserId();
                    custDocDetails.Cust_Doc_CreatedDate = DateTime.Now;
                    custDocDetails.Cust_Doc_LastUpdatedDate = DateTime.Now;

                    listDocDetail.Add(custDocDetails);
                }
            }
        }


        //Save Customer Document Details
        ESalesUnityContainer.Container.Resolve<ICustomerService>().SaveAndUpdateCustomerDocumentDetails(listDocDetail, listCustDocument);
        DeleteFiles();
    }

    protected void grdDocument_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtDocExDate = (TextBox)e.Row.FindControl("txtDocExDate");
            txtDocExDate.Attributes.Add("ReadOnly", "true");

            if (ViewState[Globals.StateMgmtVariables.CHECKEDITEMS] != null)
            {
                ArrayList checkboxCheckedList = (ArrayList)ViewState[Globals.StateMgmtVariables.CHECKEDITEMS];
                if (checkboxCheckedList.Contains(grdDocument.DataKeys[e.Row.RowIndex].Value))
                {
                    CheckBox chkDocument = (CheckBox)e.Row.FindControl("chkScanComplete");
                    chkDocument.Checked = true;
                }
            }
        }
    }

    protected void btnNewRegistration_Click(object sender, EventArgs e)
    {
        Event_ShowCustomerRegistrationScreen(sender);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Event_CloseScreen(sender);
    }

    protected void btnAddAuthRep_Click(object sender, EventArgs e)
    {
        string folderName = Convert.ToString(ViewState[Globals.StateMgmtVariables.CUSTFOLDERNAME]);
        Event_ShowParnetScreen(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]), true, folderName);
    }

    protected void btnAddTruck_Click(object sender, EventArgs e)
    {
        string folderName = Convert.ToString(ViewState[Globals.StateMgmtVariables.CUSTFOLDERNAME]);
        //Event_ShowAddTruckScreen(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]), true, folderName);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Event_ShowCustomerReport(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]));
    }

    private void DeleteFiles()
    {
        if (ViewState[Globals.StateMgmtVariables.CUSTFOLDERNAME] != null)
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
    }
    protected void DocNoExist_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        CustomValidator customval = (CustomValidator)sender;
        GridViewRow r = (GridViewRow)customval.NamingContainer;

        if (((CheckBox)r.FindControl("chkScanComplete")).Checked)
        {
            string docNo = ((TextBox)r.FindControl("txtDocNo")).Text.Trim();
            int docId = Convert.ToInt32(grdDocument.DataKeys[r.RowIndex].Value);

            string custDocId = ((HiddenField)r.FindControl("hdnCustDocId")).Value;
            int customerDocId = custDocId != string.Empty ? Convert.ToInt32(custDocId) : 0;

            if (ESalesUnityContainer.Container.Resolve<ICustomerDocService>().CustomerDocumentNoExists(customerDocId, docId, docNo))
            {
                args.IsValid = false;
            }
        }
    }
}