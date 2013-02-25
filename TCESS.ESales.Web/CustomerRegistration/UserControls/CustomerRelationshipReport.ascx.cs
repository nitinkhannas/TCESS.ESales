#region Using directives

using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Microsoft.Reporting.WebForms;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Reports;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.CommonLayer.CommonLibrary;

#endregion

public partial class CustomerRegistration_UserControls_CustomerRelationshipReport : BaseUserControl
{
    public event ShowDataEventHandler Event_ShowCustomerDocumentScreen;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.SetReportEmbeddedResource(ReportViewer, "TCESS.ESales.CommonLayer.Reports.CustomerRelationshipReoprt.rdlc");
        }
    }

    public void ShowCustomerDetails(int customerId)
    {
        ViewState[Globals.StateMgmtVariables.CUSTOMERID] = customerId;

        ReportDataSource customerDocDataSource = new ReportDataSource("dsCustomerDocDetails",
            GetCustomerDocDetails(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID])));

        ReportDataSource truckDataSource = new ReportDataSource("dsTruckDetails",
            GetTruckDetails(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID])));

        ReportDataSource authRepDataSource = new ReportDataSource("dsAuthRepDetails",
            GetAuthRepDetails(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID])));

        ReportDataSource materialTypeDataSource = new ReportDataSource("dsMaterialTypeDetails",
            GetMaterialTypeDetails(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID])));

        SetReportParametersForCustomers(GetCustomerDetails(customerId), ReportViewer);

        ReportViewer.LocalReport.DataSources.Add(materialTypeDataSource);
        ReportViewer.LocalReport.DataSources.Add(customerDocDataSource);
        ReportViewer.LocalReport.DataSources.Add(truckDataSource);
        ReportViewer.LocalReport.DataSources.Add(authRepDataSource);

        ReportViewer.LocalReport.Refresh();
    }

    private IList<CustomerMaterialMapDTO> GetMaterialTypeDetails(int customerId)
    {
        IList<CustomerMaterialMapDTO> lstCustMaterialType = ESalesUnityContainer.Container.Resolve<ICustomerMaterialService>()
         .GetCustomerMaterialDetailsByCustomerId(customerId);
        return lstCustMaterialType;
    }

    private CustomerDTO GetCustomerDetails(int customerId)
    {
        CustomerDTO customerDetails = MasterList.GetCustomerDetailsById(customerId);
        return customerDetails;
    }

    private IList<CustomerDocDetailsDTO> GetCustomerDocDetails(int customerId)
    {
        IList<CustomerDocDetailsDTO> lstCustomerDocuments = new List<CustomerDocDetailsDTO>();
        lstCustomerDocuments = ESalesUnityContainer.Container.Resolve<ICustomerDocService>()
            .GetCustomerDocumentDetails(customerId);
        return lstCustomerDocuments;
    }

    private IList<TruckDetailsDTO> GetTruckDetails(int customerId)
    {
        IList<TruckDetailsDTO> lstTruckDetails = new List<TruckDetailsDTO>();
        lstTruckDetails = ESalesUnityContainer.Container.Resolve<ITruckService>().GetTruckDetailsForCustomer(customerId);
        return lstTruckDetails;
    }

    private IList<AuthRepDTO> GetAuthRepDetails(int customerId)
    {
        IList<AuthRepDTO> lstAuthRepDetails = new List<AuthRepDTO>();
        lstAuthRepDetails = ESalesUnityContainer.Container.Resolve<IAuthRepService>().GetAuthRepDetailsForCustomer(customerId);
        return lstAuthRepDetails;
    }

    private void SetReportParametersForCustomers(CustomerDTO customerDTO, ReportViewer reportViewer)
    {
        ReportParameter Cust_TradeName = new ReportParameter("Cust_TradeName", customerDTO.Cust_TradeName);
        ReportParameter Cust_OwnerName = new ReportParameter("Cust_OwnerName", customerDTO.Cust_OwnerName);
        ReportParameter Cust_RegisteredAddress = new ReportParameter("Cust_RegisteredAddress", customerDTO.Cust_RegisteredAddress);
        ReportParameter Cust_UnitAddress = new ReportParameter("Cust_UnitAddress", customerDTO.Cust_UnitAddress);
        ReportParameter Cust_Pincode = new ReportParameter("Cust_Pincode", Convert.ToString(customerDTO.Cust_Pincode));
        ReportParameter Cust_State = new ReportParameter("Cust_State", customerDTO.Cust_State_Name);
        ReportParameter Cust_District = new ReportParameter("Cust_District", customerDTO.Cust_District_Name);
        ReportParameter Cust_Landmark = new ReportParameter("Cust_Landmark", customerDTO.Cust_Landmark);
        ReportParameter Cust_PhoneNo = new ReportParameter("Cust_PhoneNo", customerDTO.Cust_PhoneNo);
        ReportParameter Cust_MobileNo = new ReportParameter("Cust_MobileNo", customerDTO.Cust_MobileNo);
        ReportParameter Cust_OwnershipStatus = new ReportParameter("Cust_OwnershipStatus", Convert.ToString(customerDTO.Cust_OwnershipName));
        ReportParameter Cust_FatherName = new ReportParameter("Cust_FatherName", customerDTO.Cust_FathersName);
        ReportParameter Cust_AMEVisitDate = new ReportParameter("Cust_AMEVisitDate", Convert.ToDateTime(customerDTO.Cust_AMEVisitDate).ToString("dd MMM yyyy"));
        ReportParameter Cust_BusinessType = new ReportParameter("Cust_BusinessType", customerDTO.Cust_Business_Name);
        ReportParameter Cust_SalesType = new ReportParameter("Cust_SalesType", customerDTO.Cust_SalesType == 1 ? "Within Jharkhand" : "Outside Jharkhand");
        ReportParameter Cust_Post = new ReportParameter("Cust_Post", customerDTO.Cust_Post);
        ReportParameter Cust_NoOfChimneys = new ReportParameter("Cust_NoOfChimneys", Convert.ToString(customerDTO.Cust_NoOfChimneys));
        ReportParameter Cust_BrickCapacity = new ReportParameter("Cust_BrickCapacity", Convert.ToString(customerDTO.Cust_BrickCapacity));
        ReportParameter Cust_Excise_Range = new ReportParameter("Cust_Excise_Range", customerDTO.Cust_Excise_Range);
        ReportParameter Cust_Excise_Div = new ReportParameter("Cust_Excise_Div", customerDTO.Cust_Excise_Div);
        ReportParameter Cust_Excise_Comm = new ReportParameter("Cust_Excise_Comm", customerDTO.Cust_Excise_Comm);



        ReportParameter Cust_BankName = new ReportParameter("Cust_BankName", customerDTO.Cust_BankName);
        ReportParameter Cust_BankAccountType = new ReportParameter("Cust_BankAccountType", customerDTO.Cust_SalesType == 1 ? "Saving" : "Current");
        ReportParameter Cust_BankBranch = new ReportParameter("Cust_BankBranch", customerDTO.Cust_BankBranch);
        ReportParameter Cust_BankAccountNo = new ReportParameter("Cust_BankAccountNo", Convert.ToString(customerDTO.Cust_BankAccountNo));
        ReportParameter Cust_BankIFCICode = new ReportParameter("Cust_BankIFCICode", (customerDTO.Cust_BankIFCICode));
        ReportParameter Cust_BankChequeNo = new ReportParameter("Cust_BankChequeNo", Convert.ToString(customerDTO.Cust_BankChequeNo));
        ReportParameter Cust_AMEName = new ReportParameter("Cust_AMEName", customerDTO.Cust_AMEName);
        ReportParameter Cust_VATFiledON = new ReportParameter("Cust_VATFiledON", Convert.ToDateTime(customerDTO.Cust_VATFiledON).ToString("dd MMM yyyy"));
        ReportParameter Cust_UnitStatus = new ReportParameter("Cust_UnitStatus", customerDTO.Cust_UnitStatus == 1 ? "Working" : "Not Working");

        reportViewer.LocalReport.SetParameters(new ReportParameter[] {Cust_TradeName, Cust_OwnerName, Cust_RegisteredAddress,
             Cust_UnitAddress, Cust_Pincode, Cust_State, Cust_District, Cust_Landmark, Cust_PhoneNo, Cust_MobileNo, 
             Cust_OwnershipStatus, Cust_FatherName, Cust_AMEVisitDate, Cust_BusinessType, Cust_SalesType,
			 Cust_Post,Cust_NoOfChimneys,Cust_BrickCapacity,Cust_Excise_Range,Cust_Excise_Div,Cust_Excise_Comm,Cust_BankName,
             Cust_BankBranch,Cust_BankAccountNo,Cust_BankAccountType,Cust_BankIFCICode,Cust_BankChequeNo,Cust_AMEName,
             Cust_VATFiledON,Cust_UnitStatus  });
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Event_ShowCustomerDocumentScreen(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]), false, string.Empty);
    }
}