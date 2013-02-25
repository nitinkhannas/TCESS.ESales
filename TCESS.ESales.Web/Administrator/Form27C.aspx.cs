#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

public partial class Administrator_Form27C : BasePage
{
    #region GlobalVariables
    int value = 0;
    #endregion

    #region Events

    protected void Page_Init(object sender, EventArgs e)
    {
        ucViewImage.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtCustomerCode.Focus();
            //MonthValidation();
            BindDdlMonth();
            FillBlankGrid();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        AcceptSave();
        PopulateForm27CHistory();
        ResetFields();
    }

    protected void btnAffidavit_Click(object sender, EventArgs e)
    {
        GetAndSetAffidavitDetails();
        //ResetFields();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetFields();
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        string mobileNo = string.Empty;
        string englishMessage = string.Empty;
        int result = 0;
        string customerID = ViewState[Globals.StateMgmtVariables.CUSTOMERID].ToString();
        mobileNo = ViewState[Globals.StateMgmtVariables.MOBILENO].ToString();
        Form27CDTO form27CDetails = ESalesUnityContainer.Container.Resolve<IForm27CService>().GetForm27CDetailsByCustId(Convert.ToInt32(customerID));

        if (ddlRejectionReason.SelectedIndex > 0)
        {
            switch (ddlRejectionReason.SelectedIndex)
            {
                case 1:

                    form27CDetails.RejectionReason = ddlRejectionReason.SelectedItem.Text;
                    result = ESalesUnityContainer.Container.Resolve<IForm27CService>().UpdateForm27C(form27CDetails);

                    string customerCode = ViewState[Globals.StateMgmtVariables.CUSTOMERCODE].ToString();
                    CustomerDTO customerDetail = ESalesUnityContainer.Container.Resolve<ICustomerService>()
                    .GetActiveCustomerDetailsByCode(customerCode);

                    customerDetail.Cust_IsDeleted = true;
                    customerDetail.Cust_LastUpdatedDate = DateTime.Now;

                    ESalesUnityContainer.Container.Resolve<ICustomerService>().UpdateCustomerDetails(customerDetail);
                    englishMessage = "Aapke Code " + customerCode + " mein " + DateTime.Now.ToString("y") + " ka Form 27C mein PAN hamare record se na milne ke kaaran saweeker nahin kiya gaya hai. Ghato sale office se sampark karen.";
                    SmsUtility.SendSMSForBookings(mobileNo, englishMessage);

                    ucMessageBox.ShowMessage("Form 27C rejected due to Invalid PAN Number");
                    break;

                case 2:

                    form27CDetails.RejectionReason = ddlRejectionReason.SelectedItem.Text;
                    result = ESalesUnityContainer.Container.Resolve<IForm27CService>().UpdateForm27C(form27CDetails);

                    englishMessage = "Aapke Code " + txtCustomerCode.Text.Trim() + " mein " + DateTime.Now.ToString("y") + " ka Form 27C mein SIGN hamare record se na milne ke kaaran saweeker nahin kiya gaya hai. Ghato sale office se sampark karen.";
                    SmsUtility.SendSMSForBookings(mobileNo, englishMessage);
                    ucMessageBox.ShowMessage("Form 27C rejected due to Invalid signature");
                    break;

                case 3:

                    //form27CDetails.RejectionReason = ddlRejectionReason.SelectedItem.Text;
                    //result = ESalesUnityContainer.Container.Resolve<IForm27CService>().UpdateForm27C(form27CDetails);

                    //englishMessage = "Aapke Code " + txtCustomerCode.Text.Trim() + " mein " + DateTime.Now.ToString("m") + " ka Form 27C mein SIGN hamare record se na milne ke kaaran saweeker nahin kiya gaya hai. Ghato sale office se sampark karen.";
                    //mobileNo = ViewState[Globals.StateMgmtVariables.MOBILENO].ToString();

                    break;
            }
        }
        else
        {
            ucMessageBox.ShowMessage("Please select suitable Rejection Reason first");
        }
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        chkSignValid.Checked = true;
    }

    protected void validate_Click(object sender, EventArgs e)
    {
        //Get customer details by customer code
        FillGridWithCustomerDetails(txtCustomerCode.Text.Trim(), txtPANNo.Text.Trim());
    }

    protected void btnSign_Click(object sender, EventArgs e)
    {
        if (ViewState[Globals.StateMgmtVariables.DOCID] != null && ViewState[Globals.StateMgmtVariables.DOCID].ToString() != "0")
        {
            ucViewImage.ShowMessage(Convert.ToString(ViewState[Globals.StateMgmtVariables.DOCID]));
            btnSave.Enabled = true;
        }
        else
        {
            ucMessageBox.ShowMessage("Signature not found");
        }
    }

    protected void rdAffidavit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdAffidavit.SelectedItem.Value == "1")
        {
            if (txtCustomerCode.Text == "")
            {
                ucMessageBox.ShowMessage("Please validate customer first");
                rdAffidavit.SelectedIndex = 1;
                txtApprovedBy.Enabled = false;
            }
            else
            {
                txtApprovedBy.Enabled = true;
                ucMessageBox.ShowMessage("Do you want to save Affidavit");
            }
        }
        else
        {
            txtApprovedBy.Enabled = false;
        }
    }

    protected void ddlRejectionReason_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void txtTSLAcceptedDate_TextChanged(object sender, EventArgs e)
    {
        // MonthValidation();
        //ss ddlMonth.Enabled = true;
    }

    #endregion

    #region Methods

    private void BindDdlMonth()
    {
        List<MonthsDTO> listMonth = ESalesUnityContainer.Container.Resolve<IForm27CService>().GetMonthList().ToList();

        ddlMonth.DataTextField = "MonthName";
        ddlMonth.DataValueField = "Months_Id";
        ddlMonth.DataSource = listMonth;
        ddlMonth.DataBind();
        ddlMonth.Items.Insert(0, new ListItem("Select"));
    }
     
    private void AcceptSave()
    {        
        if (Page.IsValid)
        {
            if (chkSignValid.Checked)
            {
                string customerID = ViewState[Globals.StateMgmtVariables.CUSTOMERID].ToString();

                AffidavitDetailsDTO affidavitDetails = ESalesUnityContainer.Container.Resolve<IAffidavitDetails>()
                .GetAffidavitDetailsByCustId(Convert.ToInt32(customerID));

               Form27PeriodTypeDTO form27PeriodTypeDetails = ESalesUnityContainer.Container.Resolve<IForm27CService>()
               .GetForm27PeriodType();

                if (affidavitDetails.AffidavitExpiryDate >= DateTime.Now)
                {                    
                    Form27CDTO form27CDetails = new Form27CDTO();
                    form27CDetails.ReceivedDate = Convert.ToDateTime(txtReceivedDate.Text);
                    form27CDetails.CreatedBy = GetCurrentUserId();
                    form27CDetails.CreatedDate = DateTime.Now;
                    form27CDetails.Cust_Id = Convert.ToInt32(customerID);
                    form27CDetails.ValidMonth = ddlMonth.SelectedItem.Text;
                    form27CDetails.ValidYear = DateTime.Now.Year.ToString();
                    form27CDetails.CurrentMonth = Convert.ToInt32(ddlMonth.SelectedValue);
                    form27CDetails.PeriodType = form27PeriodTypeDetails.PeriodTypeId;


                    int result = ESalesUnityContainer.Container.Resolve<IForm27CService>().SaveForm27C(form27CDetails);

                    if (result > 0)
                    {
                        //string englishMessage = "Hamen aapke Code " + txtCustomerCode.Text.Trim() + " mein " + DateTime.Now.ToString("y") + " ka Form 27C prapt hua hai. Aap apne unit mein prayog ke liye Tailings ki bookings karen";
                        //string mobileNo = ViewState[Globals.StateMgmtVariables.MOBILENO].ToString();
                        //SmsUtility.SendSMSForBookings(mobileNo, englishMessage + " .DCA Ghato");
                        ucMessageBox.ShowMessage("Form 27C Saved Successfully.");
                    }
                    else
                    {
                        ucMessageBox.ShowMessage("Form 27C Not Saved.");
                    }
                    
                }
                else
                {
                    ucMessageBox.ShowMessage("Please submit Affidavit for the current year.");
                }
            }
            else
            {
                ucMessageBox.ShowMessage("Please verify signature.");
            }
        }
    }

    private void FillBlankGrid()
    {
        ShowBlankRowInGrid<CustomerDTO>(grdManageCustomers);
    }

    private void FillGridWithCustomerDetails(string customerCode, string txtPAN)
    {
        CustomerDTO customer = new CustomerDTO();
        bool flagValid = false;
        bool flagPAN = false;

        IList<CustomerDTO> lstCustomerDTO = new List<CustomerDTO>();

        CustomerDTO customerDetail = ESalesUnityContainer.Container.Resolve<ICustomerService>()
            .GetActiveCustomerDetailsByCode(customerCode);

        lstCustomerDTO.Add(customerDetail);

        IList<CustomerDocDetailsDTO> customerDocDetails = ESalesUnityContainer.Container.Resolve<ICustomerDocService>()
            .GetCustomerDocumentDetails(customerDetail.Cust_Id);

        ViewState[Globals.StateMgmtVariables.CUSTOMERCODE] = customerDetail.Cust_Code;
        ViewState[Globals.StateMgmtVariables.CUSTOMERID] = customerDetail.Cust_Id;
        ViewState[Globals.StateMgmtVariables.MOBILENO] = customerDetail.Cust_MobileNo;
        ViewState[Globals.StateMgmtVariables.DOCID] = (from Doc in customerDocDetails
                                                       where Doc.Cust_Doc_DocId == 13
                                                       select Doc.Cust_Doc_Id).FirstOrDefault();

        ResetFields();
        List<CustomerDTO> lst = new List<CustomerDTO>();
        foreach (CustomerDocDetailsDTO l in customerDocDetails)
        {
            if (l.Cust_Doc_No.ToLower() == txtPAN.ToLower())
            {
                if (customerDetail.Cust_Id > 0)
                {
                    txtCustomerCode.Text = customerCode;
                    txtPANNo.Text = txtPAN;
                    txtReceivedDate.Enabled = true;
                    txtTSLAcceptedDate.Enabled = true;
                    ddlMonth.Enabled = true;                    
                    flagValid = true;
                }
            }
            else
            {
                txtCustomerCode.Text = customerCode;
                txtPANNo.Text = txtPAN;
                rdAffidavit.Enabled = false;
                ucMessageBox.ShowMessage("PAN Number is not Valid");
                ddlRejectionReason.Enabled = true;
                btnReject.Enabled = true;
                flagPAN = true;
            }
        }

        if (flagValid)
        {
            grdManageCustomers.DataSource = lstCustomerDTO;
            grdManageCustomers.DataBind();
            rdAffidavit.Enabled = true;
            txtApprovedBy.Enabled = false;
            ucMessageBox.ShowMessage("Customer Valid");
            PopulateForm27CHistory();
            GetAffidavitDetails();
        }
        else
        {
            if (flagPAN)
            {
                txtCustomerCode.Text = customerCode;
                txtPANNo.Text = txtPAN;
                customerDetail.Cust_IsDeleted = true;
                customerDetail.Cust_LastUpdatedDate = DateTime.Now;
                ddlRejectionReason.Enabled = true;
                rdAffidavit.Enabled = false;
                btnReject.Enabled = true;
                ucMessageBox.ShowMessage("PAN Number is not Valid");
            }
            else
            {
                rdAffidavit.Enabled = false;
                ddlRejectionReason.Enabled = true;
                btnReject.Enabled = true;
                ucMessageBox.ShowMessage("Customer Not Valid");
            }
        }
    }

    private void ResetFields()
    {
        FillBlankGrid();
        txtCustomerCode.Text = string.Empty;
        txtPANNo.Text = string.Empty;
        txtReceivedDate.Text = string.Empty;
        txtTSLAcceptedDate.Text = string.Empty;
        txtTSLAcceptedDate.Enabled = false;
        ddlRejectionReason.SelectedIndex = 0;
        rdAffidavit.SelectedIndex = 1;
        txtApprovedBy.Enabled = false;
        txtApprovedBy.Text = string.Empty;
        ddlRejectionReason.Enabled = false;
        btnSign.Enabled = false;
        ddlMonth.Enabled = false;
        ddlMonth.SelectedIndex = 0;        
        txtReceivedDate.Enabled = false;
        btnSave.Enabled = false;
        btnAffidavit.Enabled = false;
        btnReject.Enabled = false;
        rdAffidavit.Enabled = false;
        gridHistory.Visible = false;
        lblHistory.Visible = false;
        chkSignValid.Checked = false;
        txtCustomerCode.Focus();
    }

    //  value is number to be added to get the new month
    // ddlStartMonth is the value from where month to be added in dropdown
    private void Bind_CaseStatus(int value, int ddlStartMonth)
    {
        int totalNumberOfMonth = 0;
        int currentMonthId = ddlStartMonth;
        if (txtTSLAcceptedDate.Text.Trim() != string.Empty)
        {
            List<MonthsDTO> listMonth = ESalesUnityContainer.Container.Resolve<IForm27CService>().GetMonthList().ToList();

            string selectedDateMonth = txtTSLAcceptedDate.Text.Substring(txtTSLAcceptedDate.Text.IndexOf("-") + 1, 3);
            string selectedDateYear = txtTSLAcceptedDate.Text.Substring(txtTSLAcceptedDate.Text.LastIndexOf("-") + 1);

            List<MonthsDTO> lst = new List<MonthsDTO>();

            int toLimit = 0;
            totalNumberOfMonth = currentMonthId + value;
            if (totalNumberOfMonth < 12)
            {
                toLimit = totalNumberOfMonth - 1;
            }
            else
            {
                toLimit = 12;
            }

            for (int i = currentMonthId; i <= toLimit; i++)
            {
                foreach (MonthsDTO data in listMonth)
                {
                    if (data.Months_Id == i)
                    {
                        MonthsDTO dt = new MonthsDTO();

                        dt.MonthName = data.MonthName + " " + selectedDateYear;
                        dt.Months_Id = i;
                        lst.Add(dt);
                    }
                }
            }

            if (totalNumberOfMonth > 12)
            {
                int nextYearMonths = totalNumberOfMonth - 12;

                for (int i = 1; i < nextYearMonths; i++)
                {
                    foreach (MonthsDTO data in listMonth)
                    {
                        if (data.Months_Id == i)
                        {
                            MonthsDTO dt = new MonthsDTO();

                            dt.MonthName = data.MonthName + " " + (Convert.ToInt32(selectedDateYear) + 1);
                            dt.Months_Id = i;
                            lst.Add(dt);
                        }
                    }
                }
            }
            else
            {

            }

            ddlMonth.DataTextField = "MonthName";
            ddlMonth.DataValueField = "Months_Id";
            ddlMonth.DataSource = lst;
            ddlMonth.DataBind();
        }
        else
        {
            List<MonthsDTO> listMonth = ESalesUnityContainer.Container.Resolve<IForm27CService>().GetMonthList().ToList();
            ddlMonth.DataTextField = "MonthName";
            ddlMonth.DataValueField = "Months_Id";
            ddlMonth.DataSource = listMonth;
            ddlMonth.DataBind();
        }
    }

    private void GetAffidavitDetails()
    {
        AffidavitDetailsDTO affidavitDetail = ESalesUnityContainer.Container.Resolve<IAffidavitDetails>()
            .GetAffidavitDetailsByCustId(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]));

        if (affidavitDetail.AffidavitExpiryDate > DateTime.Now)
        {
            rdAffidavit.SelectedItem.Value = "1";
            btnAffidavit.Enabled = false;
            txtApprovedBy.Enabled = true;
            btnSign.Enabled = true;
            txtReceivedDate.Enabled = true;
            txtTSLAcceptedDate.Enabled = true;
     
        }
        else
        {
            rdAffidavit.SelectedItem.Value = "2";
            btnAffidavit.Enabled = true;
            txtApprovedBy.Enabled = false;
            btnSign.Enabled = false;
            txtReceivedDate.Enabled = false;
            txtTSLAcceptedDate.Enabled = false;
        }
    }

    private void GetAndSetAffidavitDetails()
    {
        AffidavitDetailsDTO affidavitDetail = ESalesUnityContainer.Container.Resolve<IAffidavitDetails>()
            .GetAffidavitDetailsByCustId(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]));

        if (affidavitDetail.AffidavitExpiryDate > DateTime.Now)
        {
            ucMessageBox.ShowMessage("Affidavit Already submitted");
        }
        else
        {
            if (affidavitDetail.Affidavit_CustID > 0)
            {
                affidavitDetail.AffidavitSubmitDate = DateTime.Now;
                affidavitDetail.AffidavitExpiryDate = DateTime.Now.AddYears(1);
                int result = ESalesUnityContainer.Container.Resolve<IAffidavitDetails>().UpdateAffidavitDetails(affidavitDetail);

                if (result > 0)
                {
                    ucMessageBox.ShowMessage("Affidavit Saved Successfully");
                }
            }
            else
            {
                AffidavitDetailsDTO affidavitDetails = new AffidavitDetailsDTO();
                affidavitDetails.Affidavit_CustID = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]);
                affidavitDetails.AffidavitCreatedBy = GetCurrentUserId();
                affidavitDetails.AffidavitCreatedDate = DateTime.Now;
                affidavitDetails.AffidavitSubmitDate = DateTime.Now;
                affidavitDetails.AffidavitExpiryDate = DateTime.Now.AddYears(1);
                affidavitDetails.AffidavitIsSubmitted = 1;

                int result = ESalesUnityContainer.Container.Resolve<IAffidavitDetails>().SaveAffidavitDetails(affidavitDetails);

                if (result > 0)
                {
                    ucMessageBox.ShowMessage("Affidavit Saved Successfully");
                    FillGridWithCustomerDetails(txtCustomerCode.Text.Trim(), txtPANNo.Text.Trim());
                }
            }
        }
    }

    private void PopulateForm27CHistory()
    {
        IList<Form27CDTO> lstForm27CHistory = ESalesUnityContainer.Container.Resolve<IForm27CService>()
            .GetForm27CDetailsByCustIdList(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]));
               
        if (lstForm27CHistory.Count > 0)
        {
            lblHistory.Visible = true;
            gridHistory.Visible = true;
            IList<Form27CDTO> lastFiveItem = lstForm27CHistory.OrderByDescending(k => k.Form27C_Id).Take(5).ToList();
            gridHistory.DataSource = lastFiveItem;
            gridHistory.DataBind();
        }
    }

    private void MonthValidation()
    {
        Form27PeriodTypeDTO form27PeriodTypeDetails = ESalesUnityContainer.Container.Resolve<IForm27CService>()
            .GetForm27PeriodType();

        int ddlStartindex = DateTime.Now.Month;
        int ddlStartMonth = DateTime.Now.Month;

        if (form27PeriodTypeDetails.PeriodTypeId > 0)
        {
            switch (form27PeriodTypeDetails.PeriodTypeId)
            {
                case 1:
                    value = 2;
                    ddlStartindex = DateTime.Now.Month;
                    break;
                case 2:
                    value = 3;
                    if (ddlStartMonth >= 1 && ddlStartMonth <= 3)
                    {
                        ddlStartindex = 1;
                    }
                    if (ddlStartMonth >= 4 && ddlStartMonth <= 6)
                    {
                        ddlStartindex = 4;
                    }
                    if (ddlStartMonth >= 7 && ddlStartMonth <= 9)
                    {
                        ddlStartindex = 7;
                    }
                    if (ddlStartMonth >= 10 && ddlStartMonth <= 12)
                    {
                        ddlStartindex = 10;
                    }
                    break;
                case 3:
                    value = 6;
                    if (ddlStartMonth >= 3 && ddlStartMonth <= 9)
                    {
                        ddlStartindex = 4;
                    }
                    if (ddlStartMonth >= 1 && ddlStartMonth <= 3)
                    {
                        ddlStartindex = 10;
                    }
                    if (ddlStartMonth >= 10 && ddlStartMonth <= 12)
                    {
                        ddlStartindex = 10;
                    }
                    break;
                case 4:
                    value = 12;
                    ddlStartindex = 4;
                    break;
            }

            // Bind_CaseStatus(value, ddlStartindex);
        }

    }

    #endregion

}