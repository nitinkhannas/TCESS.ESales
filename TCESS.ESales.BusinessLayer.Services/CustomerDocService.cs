#region Using directives

using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.PersistenceLayer.Entity;
using TCESS.ESales.PersistenceLayer.Interfaces;

#endregion

namespace TCESS.ESales.BusinessLayer.Services
{
    public class CustomerDocService : ICustomerDocService
    {
        /// <summary>
        /// Get Customer Document Details By CustDocId
        /// </summary>
        /// <param name="custDocId">Int32:custDocId</param>
        /// <returns></returns>
        public CustomerDocumentsDTO GetCustomerDocumentDetailsByCustDocId(int custDocId)
        {
            CustomerDocumentsDTO customerDocuments = new CustomerDocumentsDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<customerdocument>>()
            .GetSingle(item => item.CustDoc_Doc_Id == custDocId && item.CustDoc_IsDeleted == false), customerDocuments);
            return customerDocuments;
        }

        /// <summary>
        /// Get Customer Document Details By DocId And CustId
        /// </summary>
        /// <param name="customerId">Int32:customerId</param>
        /// <param name="documentId">Int32:documentId</param>
        /// <returns></returns>
        public CustomerDocDetailsDTO GetCustomerDocumentDetailsByDocIdAndCustId(int customerId, int documentId)
        {
            CustomerDocDetailsDTO customerDocDetails = new CustomerDocDetailsDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<customerdocdetail>>()
            .GetSingle(item => item.Cust_Doc_CustId == customerId && item.Cust_Doc_DocId == documentId
                && item.Cust_Doc_IsDeleted == false), customerDocDetails);
            return customerDocDetails;
        }

        /// <summary>
        /// Get Registered Customer By documentType and documentNumber
        /// </summary>
        /// <param name="documentType">Int32:documentType</param>
        /// <param name="documentNumber">string:documentNumber</param>
        /// <returns></returns>
        public CustomerDocDetailsDTO GetRegisteredCustomerByDocumentId(int documentType, string documentNumber)
        {
            CustomerDocDetailsDTO customerDocDetails = new CustomerDocDetailsDTO();

            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<customerdocdetail>>()
            .GetSingle(item => item.Cust_Doc_DocId == documentType
                && item.Cust_Doc_No == documentNumber
                && item.Cust_Doc_IsDeleted == false
                && item.customer.Cust_IsBlacklisted == false
                && item.customer.Cust_RegCustType == true
                && item.customer.Cust_Code != null
                ), customerDocDetails);

            return customerDocDetails;
        }

        /// <summary>
        /// Get Customer By Document Id
        /// </summary>
        /// <param name="documentType">Int32:documentType</param>
        /// <param name="documentNumber">string:documentNumber</param>
        /// <returns></returns>
        public CustomerDocDetailsDTO GetCustomerByDocumentId(int documentType, string documentNumber)
        {
            CustomerDocDetailsDTO customerDocDetails = new CustomerDocDetailsDTO();

            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<customerdocdetail>>()
            .GetSingle(item => item.Cust_Doc_DocId == documentType && item.Cust_Doc_No.Equals(documentNumber)
                && item.Cust_Doc_IsDeleted == false), customerDocDetails);

            return customerDocDetails;
        }

        /// <summary>
        /// Get Customer Document Details by customerId
        /// </summary>
        /// <param name="customerId">Int32:customerId</param>
        /// <returns></returns>
        public IList<CustomerDocDetailsDTO> GetCustomerDocumentDetails(int customerId)
        {
            List<CustomerDocDetailsDTO> lstCustomerDocDetails = new List<CustomerDocDetailsDTO>();

            List<customerdocdetail> objListCustomerDocDetail = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<customerdocdetail>>().GetQuery()
                .Where(item => item.Cust_Doc_CustId == customerId).ToList();

            AutoMapper.Mapper.Map(objListCustomerDocDetail, lstCustomerDocDetails);

            //return the value
            return lstCustomerDocDetails;
        }

		/// <summary>
		/// Get Customer Details For Code Allocation by customerId
		/// </summary>
		/// <param name="customerId">Int32:customerId</param>
		/// <returns></returns>
		public CustomerDetailsForCodeAllocDTO GetCustomerDocumentDetailsForCodeAlloc(int customerId)
		{
			CustomerDetailsForCodeAllocDTO customerDocDetails = new CustomerDetailsForCodeAllocDTO();

			AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<customerdetailsforcodealloc>>()
			.GetSingle(item => item.Cust_Id == customerId), customerDocDetails);

			return customerDocDetails;
		}

        /// <summary>
        /// Verify Customer Document No Exists or not by custDocId,docId and docNo
        /// </summary>
        /// <param name="custDocId">Int32:custDocId</param>
        /// <param name="docId">Int32:docId</param>
        /// <param name="docNo">string:docNo</param>
        /// <returns></returns>
        public bool CustomerDocumentNoExists(int custDocId, int docId, string docNo)
        {
            CustomerDocDetailsDTO objCustomerDocDetailsDTO = new CustomerDocDetailsDTO();
            if (custDocId > 0)
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<customerdocdetail>>().GetSingle
                    (item => item.Cust_Doc_Id != custDocId && item.Cust_Doc_DocId == docId && item.Cust_Doc_No == docNo &&
                        item.Cust_Doc_IsDeleted == false), objCustomerDocDetailsDTO);
            }
            else
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<customerdocdetail>>().GetSingle
                    (item => item.Cust_Doc_DocId == docId && item.Cust_Doc_No == docNo && item.Cust_Doc_IsDeleted == false), objCustomerDocDetailsDTO);
            }
            return objCustomerDocDetailsDTO.Cust_Doc_Id > 0 ? true : false;
        }
    }
}