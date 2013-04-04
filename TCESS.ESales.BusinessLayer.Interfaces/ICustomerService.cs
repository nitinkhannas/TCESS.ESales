#region Using directives

using System.Collections.Generic;
using TCESS.ESales.DataTransferObjects;

#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
	public interface ICustomerService
	{
        int SaveAndUpdateCustomerDetails(CustomerDTO customerDetails, IList<CustomerMaterialMapDTO> listDocDetails);
        void SaveAndUpdateCustomerDocumentDetails(IList<CustomerDocDetailsDTO> listCustDocDetails,
            IList<CustomerDocumentsDTO> listCustDocument);
		void EditCustomerDocumentDetails(IList<CustomerDocDetailsDTO> listCustDocDetails,
			IList<CustomerDocumentsDTO> listCustDocument);
        void DeleteCustomer(int customerId);

        CustomerDTO GetCustomerDetailsById(int customerId);
		CustomerDTO GetCustomerDetailsByCode(string customerCode);
        CustomerDTO GetCustomerDetails(int customerId);
        
        IList<CustomerDTO> GetCustomerDetailsByMobileNumber(string mobileNumber);
        CustomerDTO GetCustomerDetailsForCashSMS(string mobileNumber, string customerCode);

        IList<CustomerDTO> GetCustomersByCustomerStatus(bool customerStatus);
        IList<CustomerDTO> GetReValidatedCustomersByCustomer();
		IList<CustomerDTO> GetCustomerForDCAAssociation();
		IList<CustomerDTO> GetCustomerForSMSSending();
		void UpdateCustomerDetails(CustomerDTO customerDetails);
        IList<CustomerDTO> GetNewCustomerForSMSSending();
        CustomerDTO GetActiveCustomerDetailsByCode(string customerCode);
        List<int> GetCustomerIdByBusinessTypeId(int businessTypeId);

        /// <summary>
        /// Validates customer details and retrieve customer owner name and trade name
        /// </summary>
        /// <param name="customerCode">Customer code</param>
        /// <param name="validationTypeId">Validatation type id</param>
        /// <param name="validationValue">Validation value</param>
        /// <returns>returns customer owner name and trade name</returns>
        IList<CustomerDTO> ValidateCustomerDetails(string customerCode, int validationTypeId, string validationValue);
		int SaveAndUpdateCustomerPatner(CustomerPartnerDTO customerPartner);
        List<CustomerPartnerDTO> GetPatnerList(int customerId);
	}
}