using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCESS.ESales.DataTransferObjects;

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface ICustomerDocService
    {
        CustomerDocDetailsDTO GetCustomerByDocumentId(int documentType, string documentNumber);
        CustomerDocDetailsDTO GetRegisteredCustomerByDocumentId(int documentType, string documentNumber);
        IList<CustomerDocDetailsDTO> GetCustomerDocumentDetails(int customerId);
		CustomerDetailsForCodeAllocDTO GetCustomerDocumentDetailsForCodeAlloc(int customerId);
        bool CustomerDocumentNoExists(int custDocId, int docId, string docNo);
		CustomerDocDetailsDTO GetCustomerDocumentDetailsByDocIdAndCustId(int customerId, int documentId);
        CustomerDocumentsDTO GetCustomerDocumentDetailsByCustDocId(int documentId);
    }
}