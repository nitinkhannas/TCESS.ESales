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

public partial class CustomerRegistration_UserControls_CustomerAlloc : BaseUserControl
{
	public event CloseScreenEventHandler Event_CloseScreen;
	protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			Common.SetReportEmbeddedResource(ReportViewer, "TCESS.ESales.CommonLayer.Reports.CodeAlloc.rdlc");
		}
    }
	public void ShowCustomerDetails(int customerId)
	{
		//Get customer document details by Customer Id
		ReportDataSource customerDocDataSource = new ReportDataSource("dsCodeAlloc", GetCustomerDocDetails(customerId));

		
		ReportViewer.LocalReport.DataSources.Add(customerDocDataSource);
		ReportViewer.LocalReport.Refresh();
	}
	private IList<CustomerDetailsForCodeAllocDTO> GetCustomerDocDetails(int customerId)
	{
		IList<CustomerDetailsForCodeAllocDTO> lstCustomerDetails = new List<CustomerDetailsForCodeAllocDTO>();
		CustomerDetailsForCodeAllocDTO customerDetails = new CustomerDetailsForCodeAllocDTO();
		customerDetails = ESalesUnityContainer.Container.Resolve<ICustomerDocService>()
			.GetCustomerDocumentDetailsForCodeAlloc(customerId);
		lstCustomerDetails.Add(customerDetails);
		return lstCustomerDetails;
	}
	protected void btnReturn_Click(object sender, EventArgs e)
	{
		Event_CloseScreen(sender);
	}
}