#region Namespace

using System;
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
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using System.Drawing;
using System.Drawing.Imaging;

#endregion

public partial class Bookings_UserControls_AddEditStandaloneTrucks : BaseUserControl
{
    public event CloseScreenEventHandler Event_CloseScreen;

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
            StandaloneTruckCount();
            PopulateTruckregistrationType();

            //Get document type list for truck registration and bind it to gridview control
            GetMandatoryDocumentList();
        }
    }
    private void PopulateTruckregistrationType()
    {
        IList<TruckRegTypeDTO> lsttruckregistrationtype = MasterList.GetTruckregTypeList();
        ddltruckregistration.DataSource = lsttruckregistrationtype;
        ddltruckregistration.DataBind();
        ddltruckregistration.Items.Insert(0, new ListItem(Messages.SelectRegistration, "0"));
    }

    public void StandaloneTruckCount()
    {
        int count = ESalesUnityContainer.Container.Resolve<IStandaloneTruckService>().StandaloneTruckCount();
        int totalcount = ESalesUnityContainer.Container.Resolve<IStandaloneTruckService>().TotalStandaloneTruckCount();
        lblTruckcountdata.Text = count.ToString();
        lblTotalTruckcountdata.Text = totalcount.ToString();
    }

    public void ShowBlankScreen()
    {
        string folderPath = MasterList.CheckIfFolderExists(Globals.FolderDetails.STANDALONETRUCKFOLDER,
                Globals.FolderDetails.FOLDERPATH);
        ViewState[Globals.StateMgmtVariables.TRUCKID] = null;

        if (!lblFolderPath.Text.Contains(folderPath))
        {
            lblFolderPath.Text += folderPath;
        }
        btnCancel.Visible = false;
    }

    public void PopulateTruckDetails(int truckId)
    {
        StandaloneTrucksDTO truckDetails = ESalesUnityContainer.Container.Resolve<IStandaloneTruckService>()
            .GetStandaloneTruckByTruckId(truckId);

        ViewState[Globals.StateMgmtVariables.TRUCKID] = truckDetails.StandaloneTruck_Id;
        txtTruckRegNo.Text = truckDetails.StandaloneTruck_RegNo;
        txtOwnerName.Text = truckDetails.StandaloneTruck_OwnerName;
        txtDriverName.Text = truckDetails.StandaloneTruck_DriverName;
        ddlTruckMake.SelectedValue = truckDetails.StandaloneTruck_Make.ToString();
        txtCarryCapacity.Text = truckDetails.StandaloneTruckCarryCapacity_Type;
        txtWheeler.Text = truckDetails.StandaloneTruckWheeler_Type;
        txtRegAddress.Text = truckDetails.StandaloneTruck_Address;
        ddlStates.SelectedValue = truckDetails.StandaloneTruck_State.ToString();
        txtMobileNo.Text = truckDetails.StandaloneTruck_MobileNo;
        txtPhoneNo.Text = truckDetails.StandaloneTruck_PhoneNo;
        txtDriverShortAdd.Text = truckDetails.StandaloneTruck_DriverShortAdd;
        txtOwnerShortAdd.Text = truckDetails.StandaloneTruck_OwnerShortAdd;
        if (!string.IsNullOrEmpty(truckDetails.StandaloneTruck_RegType.ToString()))
            ddltruckregistration.SelectedValue = truckDetails.StandaloneTruck_RegType.ToString();
        string folderPath = MasterList.CheckIfFolderExists(Globals.FolderDetails.STANDALONETRUCKFOLDER,
                Globals.FolderDetails.FOLDERPATH);

        if (!lblFolderPath.Text.Contains(folderPath))
        {
            lblFolderPath.Text += folderPath;
        }

        //Show document list submitted by customer for standalone trucks
        PopulateDocumentsList(truckId, folderPath, truckDetails.StandaloneTruck_RegNo);

        btnSave.Text = Labels.Update;
    }

    /// <summary>
    /// Show document list submitted by customer for standalone trucks
    /// </summary>
    /// <param name="truckId">Unique id for Truck</param>
    /// <param name="folderPath">Folder path where scanned documents are kept</param>
    /// <param name="truckRegno">Truck registration number</param>
    private void PopulateDocumentsList(int truckId, string folderPath, string truckRegno)
    {
        IList<StandaloneTruckDocDetails> lstTruckDocDetails = ESalesUnityContainer.Container.Resolve<IStandaloneTruckService>()
             .GetStandaloneTruckDocDetailsByTruckId(truckId);

        foreach (GridViewRow row in grdDocument.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                StandaloneTruckDocDetails truckDocDetails = (from truckDocs in lstTruckDocDetails
                                                             where truckDocs.StandaloneTruck_Doc_DocId == Convert.ToInt32(grdDocument.DataKeys[row.RowIndex].Value)
                                                             && truckDocs.StandaloneTruck_Doc_IsDeleted == false
                                                             select truckDocs).FirstOrDefault();

                if (truckDocDetails != null)
                {
                    ((CheckBox)row.FindControl("chkScanComplete")).Checked = true;
                    ((TextBox)row.FindControl("txtDocNo")).Text = truckDocDetails.StandaloneTruck_Doc_DocNo;
                    ((HiddenField)row.FindControl("hdnImge")).Value = ByteToString(truckDocDetails.StandaloneTruck_Doc_File);
                    ((TextBox)row.FindControl("txtDocExDate")).Text = string.IsNullOrEmpty(Convert.ToString(truckDocDetails.StandaloneTruck_Doc_ExDate)) ? string.Empty :
                        Convert.ToDateTime(truckDocDetails.StandaloneTruck_Doc_ExDate).ToString("dd MMM yyyy");
                    ((Label)row.FindControl("lblFileName")).Text = Convert.ToString(truckDocDetails.StandaloneTruck_Doc_FileName);

                    if (truckDocDetails.StandaloneTruck_Doc_File != null)
                    {
                        System.Drawing.Image returnImage = ImageToBlob.ConvertByteArrayToImage(truckDocDetails.StandaloneTruck_Doc_File);
                        string filePath = string.Empty;
                        if (Directory.Exists(folderPath + "\\" + truckRegno))
                        {
                            filePath = Path.Combine(folderPath + "\\" + truckRegno, truckDocDetails.StandaloneTruck_Doc_FileName);
                        }
                        else
                        {
                            filePath = Path.Combine(folderPath, truckDocDetails.StandaloneTruck_Doc_FileName);
                        }

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

    private string ByteToString(byte[] rawData)
    {
        return "[" + string.Join(",", rawData.Select(b => b.ToString())) + "]";
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

    protected void grdDocument_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtDocExDate = (TextBox)e.Row.FindControl("txtDocExDate");
            txtDocExDate.Attributes.Add("ReadOnly", "true");

            HiddenField hdnImge = (HiddenField)e.Row.FindControl("hdnImge");
            HiddenField hdnFileName = (HiddenField)e.Row.FindControl("hdnFileName");
            CheckBox chkScanComplete = (CheckBox)e.Row.FindControl("chkScanComplete");
            Label lblFileName = (Label)e.Row.FindControl("lblFileName");
            Label lblAcronym = (Label)e.Row.FindControl("lblAcronym");
            chkScanComplete.Attributes.Add("onclick", "return ReadFiles('" + chkScanComplete.ClientID + "','" + hdnImge.ClientID + "','" + lblFileName.ClientID + "','" + hdnFileName.ClientID + "','" + lblAcronym.ClientID + "')");
        }
    }

    protected void chkScanComplete_Checked(object sender, EventArgs e)
    {
        CheckBox chkScanCompleted = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chkScanCompleted.NamingContainer;
        Int32 rowIndex = row.RowIndex;
        Label fileName = ((Label)grdDocument.Rows[rowIndex].FindControl("lblFileName"));
        HiddenField hdnFileName = (HiddenField)grdDocument.Rows[rowIndex].FindControl("hdnFileName");
        TextBox txtDocNo = (TextBox)grdDocument.Rows[rowIndex].FindControl("txtDocNo");
        TextBox txtDocExDate = (TextBox)grdDocument.Rows[rowIndex].FindControl("txtDocExDate");
        fileName.Text = hdnFileName.Value;

        if (chkScanCompleted.Checked == true)
        {
            if (hdnFileName.Value == string.Empty)
            {
                string docAcronym = ((Label)grdDocument.Rows[rowIndex].FindControl("lblAcronym")).Text;

                CustomValidator validator = (CustomValidator)grdDocument.Rows[rowIndex].FindControl("ScanCompleteValidator");

                string filePath = Path.Combine(Globals.FolderDetails.FOLDERPATH, Globals.FolderDetails.STANDALONETRUCKFOLDER);

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
            int truckId = 0;

            if (ViewState[Globals.StateMgmtVariables.TRUCKID] != null)
            {
                truckId = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.TRUCKID]);
            }

            StandaloneTrucksDTO truckDetails = new StandaloneTrucksDTO();

            //Get Standalone Truck Details and initialize DTO
            truckDetails = InitializeTruckDetails(truckId);

            //Save Truck Details
            truckId = ESalesUnityContainer.Container.Resolve<IStandaloneTruckService>().SaveAndUpdateStandaloneTrucks(truckDetails);

            //Get Standalone Truck Document Details and initialize DTO
            IList<StandaloneTruckDocDetails> lstDocDetails = InitializeDocumentListForStandaloneTrucks(truckId);

            //Save Standalone Truck Document Details
            ESalesUnityContainer.Container.Resolve<IStandaloneTruckService>().SaveAndUpdateStandaloneTruckDocumentDetails(lstDocDetails);
            StandaloneTruckCount();
            if (ViewState[Globals.StateMgmtVariables.TRUCKID] != null)
            {
                ucMessageBoxForGrid.ShowMessage(Resources.Messages.StandaloneTruckUpdatedSuccessfully);
                ResetFields();
            }
            else
            {
                ucMessageBox.ShowMessage(Resources.Messages.StandaloneTruckSavedSuccessfully);
                ResetFields();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private StandaloneTrucksDTO InitializeTruckDetails(int truckId)
    {
        StandaloneTrucksDTO truckDetails = new StandaloneTrucksDTO();
        truckDetails.StandaloneTruck_Id = truckId;
        truckDetails.StandaloneTruck_RegNo = txtTruckRegNo.Text.Trim();
        truckDetails.StandaloneTruck_OwnerName = txtOwnerName.Text.Trim();
        truckDetails.StandaloneTruck_OwnerShortAdd = txtOwnerShortAdd.Text.Trim();
        truckDetails.StandaloneTruck_DriverName = txtDriverName.Text.Trim();
        truckDetails.StandaloneTruck_DriverShortAdd = txtDriverShortAdd.Text.Trim();
        truckDetails.StandaloneTruck_Make = Convert.ToInt32(ddlTruckMake.SelectedItem.Value);
        truckDetails.StandaloneTruck_Address = txtRegAddress.Text.Trim();
        truckDetails.StandaloneTruck_State = Convert.ToInt32(ddlStates.SelectedItem.Value);
        truckDetails.StandaloneTruck_MobileNo = txtMobileNo.Text.Trim();
        truckDetails.StandaloneTruck_PhoneNo = txtPhoneNo.Text.Trim();
        truckDetails.StandaloneTruck_CreatedBy = base.GetCurrentUserId();
        truckDetails.StandaloneTruck_RegType = Convert.ToInt32(ddltruckregistration.SelectedItem.Value);
        truckDetails.StandaloneTruck_CustCode = string.Empty;

        if (truckId > 0)
        {
            truckDetails.StandaloneTruck_LastUpdatedDate = DateTime.Now;
        }
        else
        {
            truckDetails.StandaloneTruck_CreatedDate = DateTime.Now;
        }
        return truckDetails;
    }

    private IList<StandaloneTruckDocDetails> InitializeDocumentListForStandaloneTrucks(int truckId)
    {
        IList<StandaloneTruckDocDetails> lstTruckDetails = new List<StandaloneTruckDocDetails>();
        foreach (GridViewRow row in grdDocument.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                if (((TextBox)(row.Cells[3].Controls[1])).Text != string.Empty)
                {
                    StandaloneTruckDocDetails truckDocDetail = new StandaloneTruckDocDetails();
                    truckDocDetail.StandaloneTruck_Doc_TruckId = truckId;
                    truckDocDetail.StandaloneTruck_Doc_DocId = Convert.ToInt32(grdDocument.DataKeys[row.RowIndex].Value);
                    truckDocDetail.StandaloneTruck_Doc_DocNo = ((TextBox)(row.Cells[3].Controls[1])).Text;
                    HiddenField hdnFileName = (HiddenField)row.FindControl("hdnFileName");
                    DateTimeFormatInfo dateTimeFormatterProvider = DateTimeFormatInfo.CurrentInfo.Clone() as DateTimeFormatInfo;
                    dateTimeFormatterProvider.ShortDatePattern = "dd/MM/yyyy";
                    truckDocDetail.StandaloneTruck_Doc_ExDate = DateTime.Parse(((TextBox)(row.Cells[4].Controls[1])).Text, dateTimeFormatterProvider);

                    if (((Label)(row.Cells[6].Controls[1])).Text.Trim() != string.Empty)
                    {
                        if (hdnFileName.Value == string.Empty)
                        {
                            string filePath = Path.Combine(Globals.FolderDetails.FOLDERPATH, Globals.FolderDetails.STANDALONETRUCKFOLDER);
                            filePath = Path.Combine(filePath, ((Label)(row.Cells[6].Controls[1])).Text);
                            truckDocDetail.StandaloneTruck_Doc_File = ImageToBlob.ConvertImageToByteArray(filePath);
                        }
                        else
                        {
                            string strBytes = ((HiddenField)(row.FindControl("hdnImge"))).Value;
                            string str = strBytes.Substring(1, strBytes.Length - 2);
                            string[] arr = str.Split(',');

                            byte[] rawData = Array.ConvertAll(arr, Convert.ToByte);
                            truckDocDetail.StandaloneTruck_Doc_File = rawData;
                        }
                    }
                    else
                    {
                        ((TextBox)(row.Cells[3].Controls[1])).Text = string.Empty;
                        ((TextBox)(row.Cells[4].Controls[1])).Text = string.Empty;
                        truckDocDetail.StandaloneTruck_Doc_File = null;
                    }

                    truckDocDetail.StandaloneTruck_Doc_FileName = ((Label)(row.Cells[6].Controls[1])).Text;
                    truckDocDetail.StandaloneTruck_Doc_CreatedBy = GetCurrentUserId();
                    truckDocDetail.StandaloneTruck_Doc_CreatedDate = DateTime.Now;
                    truckDocDetail.StandaloneTruck_Doc_LastUpdatedDate = DateTime.Now;
                    lstTruckDetails.Add(truckDocDetail);
                }
            }
        }
        return lstTruckDetails;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetFields();
        Event_CloseScreen(sender);
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
        string acronym = ((Label)grdDocument.Rows[rowIndex].FindControl("lblAcronym")).Text;
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

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetFields();
        StandaloneTruckCount();
    }

    public void ResetFields()
    {
        txtTruckRegNo.Text = string.Empty;
        txtDriverName.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        txtOwnerName.Text = string.Empty;
        txtPhoneNo.Text = string.Empty;
        txtRegAddress.Text = string.Empty;
        txtCarryCapacity.Text = string.Empty;
        txtDriverShortAdd.Text = string.Empty;
        txtOwnerShortAdd.Text = string.Empty;
        ddlStates.SelectedIndex = 0;
        ddlTruckMake.SelectedIndex = 0;
        txtWheeler.Text = string.Empty;
        ddltruckregistration.SelectedIndex = 0;

        // Get document type list for truck registration and bind it to gridview control
        GetMandatoryDocumentList();
    }

    /// <summary>
    /// Get document type list for truck registration and bind it to gridview control
    /// </summary>
    private void GetMandatoryDocumentList()
    {
        grdDocument.DataSource = ESalesUnityContainer.Container.Resolve<IDocumentTypeService>().GetDocumentTypeListForTrucks();
        grdDocument.DataBind();
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

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        Event_CloseScreen(sender);
    }

    /// <summary>
    /// Event to check Truck Reg No already exist or not
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected void txtTruckRegNo_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        bool truckExists = ESalesUnityContainer.Container.Resolve<IStandaloneTruckService>()
            .StandaloneTruckExists(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.TRUCKID]), txtTruckRegNo.Text.Trim());

        if (truckExists)
        {
            args.IsValid = false;
        }
    }
}