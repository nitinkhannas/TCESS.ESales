#region Namespace

using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Microsoft.Reporting.WebForms;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Reports;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Exception;

#endregion

public partial class CustomerRegistration_UserControls_PrintCustomers : BaseUserControl
{
    public event CloseScreenEventHandler Event_CloseScreen;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.SetReportEmbeddedResource(ReportViewer, "TCESS.ESales.CommonLayer.Reports.ActivateCustomerReoprt.rdlc");
        }
    }

    public void ShowCustomerDetails(int customerId)
    {
        SetReportParametersForCustomers(GetCustomerDetails(Convert.ToInt32(customerId)), ReportViewer);
            
        //Get material type details by Customer Id
        ReportDataSource materialTypeDataSource = new ReportDataSource("dsMaterialTypeDetails", 
            GetMaterialTypeDetails(customerId));

        //Get customer document details by Customer Id
        ReportDataSource customerDocDataSource = new ReportDataSource("dsCustomerDocDetails", GetCustomerDocDetails(customerId));

        ReportViewer.LocalReport.DataSources.Add(materialTypeDataSource);
        ReportViewer.LocalReport.DataSources.Add(customerDocDataSource);
        ReportViewer.LocalReport.Refresh();
    }

    private IList<CustomerMaterialMapDTO> GetMaterialTypeDetails(int customerId)
    {
        IList<CustomerMaterialMapDTO> lstCustMaterialType = new List<CustomerMaterialMapDTO>();
        try
        {
            ExceptionHandler.AppExceptionManager.Process(() =>
            {
                lstCustMaterialType = ESalesUnityContainer.Container.Resolve<ICustomerMaterialService>()
                 .GetCustomerMaterialDetailsByCustomerId(customerId);
            }, Globals.ExceptionTypes.ExceptionShielding.ToString());            
        }            
        catch (Exception ex)
        {
        }
        return lstCustMaterialType;
    }

    private IList<CustomerDocDetailsDTO> GetCustomerDocDetails(int customerId)
    {
        IList<CustomerDocDetailsDTO> lstCustomerDocuments = new List<CustomerDocDetailsDTO>();
        lstCustomerDocuments = ESalesUnityContainer.Container.Resolve<ICustomerDocService>()
            .GetCustomerDocumentDetails(customerId);
        return lstCustomerDocuments;
    }

    private CustomerDTO GetCustomerDetails(int customerId)
    {
        CustomerDTO customerDetails = MasterList.GetCustomerDetailsById(customerId);

        //return the value
        return customerDetails;
    }

    private void SetReportParametersForCustomers(CustomerDTO customerDTO, ReportViewer reportViewer)
    {
        ReportParameter Cust_TradeName = new ReportParameter("Cust_FirmName", customerDTO.Cust_FirmName);
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

        reportViewer.LocalReport.SetParameters(new ReportParameter[] {Cust_TradeName, Cust_OwnerName, Cust_RegisteredAddress,
             Cust_UnitAddress, Cust_Pincode, Cust_State, Cust_District, Cust_Landmark, Cust_PhoneNo, Cust_MobileNo, 
             Cust_OwnershipStatus, Cust_FatherName, Cust_AMEVisitDate, Cust_BusinessType, Cust_SalesType });
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Event_CloseScreen(sender);
    }
}