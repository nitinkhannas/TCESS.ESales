#region Using directives

using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.BusinessLayer.Services.Customer;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.PersistenceLayer.Entity;
using TCESS.ESales.PersistenceLayer.Interfaces;
using System;

#endregion

namespace TCESS.ESales.BusinessLayer.Services
{
    public class CustomerService : CustomerBaseService, ICustomerService
    {
        /// <summary>
        /// Save And Update Customer Details
        /// </summary>
        /// <param name="customerDetails"></param>
        public int SaveAndUpdateCustomerDetails(CustomerDTO customerDetails, IList<CustomerMaterialMapDTO> listCustomerMaterial)
        {
            customer customerEntity = new customer();
            using (TransactionScope transactionScope = new TransactionScope())
            {
                AutoMapper.Mapper.Map(customerDetails, customerEntity);

                if (customerDetails.Cust_Id == 0)
                {
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<customer>>().Save(customerEntity);
                }
                else
                {
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<customer>>().Update(customerEntity);
                }

                if (listCustomerMaterial != null)
                {
                    if (listCustomerMaterial.Count > 0)
                    {
                        (from customerMaterials in listCustomerMaterial select customerMaterials).Update(
                            customerMaterials => customerMaterials.Cust_Mat_CustId = customerEntity.Cust_Id);

                        CustomerMaterialService custMaterialService = new CustomerMaterialService();
                        custMaterialService.SaveAndUpdateCustomerMaterialDetails(listCustomerMaterial);
                    }
                }
                transactionScope.Complete();
            }
            return customerEntity.Cust_Id;
        }

        /// <summary>
        /// Save And Update Customer Document Details
        /// </summary>
        /// <param name="listCustomerDocumentsDetails"></param>
        public void SaveAndUpdateCustomerDocumentDetails(IList<CustomerDocDetailsDTO> listCustDocDetails,
            IList<CustomerDocumentsDTO> listCustDocument)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                for (int i = 0; i < listCustDocDetails.Count; i++)
                {
                    customerdocdetail custDocdetailsEntity = new customerdocdetail();

                    CustomerDocService custDocuments = new CustomerDocService();
                    CustomerDocDetailsDTO customerDocDetail = custDocuments.GetCustomerDocumentDetailsByDocIdAndCustId(
                        listCustDocDetails[i].Cust_Doc_CustId, listCustDocDetails[i].Cust_Doc_DocId);

                    if (customerDocDetail.Cust_Doc_Id > 0)
                    {
                        AutoMapper.Mapper.Map(customerDocDetail, custDocdetailsEntity);

                        custDocdetailsEntity.Cust_Doc_FileName = listCustDocDetails[i].Cust_Doc_FileName;
                        custDocdetailsEntity.Cust_Doc_No = listCustDocDetails[i].Cust_Doc_No;
                        custDocdetailsEntity.Cust_Doc_ExDate = listCustDocDetails[i].Cust_Doc_ExDate;

                        ESalesUnityContainer.Container.Resolve<IGenericRepository<customerdocdetail>>()
                            .Update(custDocdetailsEntity);
                    }
                    else
                    {
                        AutoMapper.Mapper.Map(listCustDocDetails[i], custDocdetailsEntity);
                        ESalesUnityContainer.Container.Resolve<IGenericRepository<customerdocdetail>>()
                            .Save(custDocdetailsEntity);
                    }

                    CustomerDocumentsDTO customerDocument = custDocuments.GetCustomerDocumentDetailsByCustDocId(custDocdetailsEntity.Cust_Doc_Id);
                    customerdocument custDocumentEntity = new customerdocument();

                    if (customerDocument.CustDoc_Id > 0)
                    {
                        AutoMapper.Mapper.Map(customerDocument, custDocumentEntity);

                        if (listCustDocument[i].CustDoc_File == null)
                        {
                            custDocumentEntity.CustDoc_IsDeleted = true;
                        }
                        else
                        {
                            custDocumentEntity.CustDoc_File = listCustDocument[i].CustDoc_File;
                        }

                        ESalesUnityContainer.Container.Resolve<IGenericRepository<customerdocument>>().Update(custDocumentEntity);
                    }
                    else
                    {
                        AutoMapper.Mapper.Map(listCustDocument[i], custDocumentEntity);
                        custDocumentEntity.CustDoc_Doc_Id = custDocdetailsEntity.Cust_Doc_Id;

                        ESalesUnityContainer.Container.Resolve<IGenericRepository<customerdocument>>().Save(custDocumentEntity);
                    }
                }
                transactionScope.Complete();
            }
        }

        /// <summary>
        /// Get Customer Details By Id
        /// </summary>
        /// <param name="customerId">Int32:customerId</param>
        /// <returns></returns>
        public CustomerDTO GetCustomerDetailsById(int customerId)
        {
            CustomerDTO customerDetails = new CustomerDTO();
            customer customerEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<customer>>()
                .GetSingle(item => item.Cust_Id == customerId);

            AutoMapper.Mapper.Map(customerEntity, customerDetails);

            //return the value
            return customerDetails;
        }

        /// <summary>
        /// Get Customer Details by customer Id
        /// </summary>
        /// <param name="customerId">Int32:customerId</param>
        /// <returns></returns>
        public CustomerDTO GetCustomerDetails(int customerId)
        {
            CustomerDTO customerDetails = new CustomerDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<ICustomerRepository>().GetCustomerDetails(customerId),
                customerDetails);

            //return the value
            return customerDetails;
        }

        /// <summary>
        /// Get Customers By Customer Status
        /// </summary>
        /// <returns></returns>
        public IList<CustomerDTO> GetCustomersByCustomerStatus(bool customerStatus)
        {
            List<CustomerDTO> lstCustomers = new List<CustomerDTO>();
            List<customer> lstCustomerEntity = new List<customer>();

            if (customerStatus == false)
            {
                lstCustomerEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<customer>>().GetQuery()
                    .Where(item => item.Cust_IsDeleted == false && item.Cust_Status == false)
                    .OrderBy(order => order.Cust_FirmName).ToList();
            }
            else
            {
                lstCustomerEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<customer>>().GetQuery()
                    .Where(item => item.Cust_IsDeleted == false && item.Cust_Status == true && item.Cust_Code == null)
                    .OrderBy(order => order.Cust_FirmName).ToList();
            }

            AutoMapper.Mapper.Map(lstCustomerEntity, lstCustomers);
            return lstCustomers;
        }

        public IList<CustomerDTO> GetReValidatedCustomersByCustomer()
        {
            List<CustomerDTO> lstCustomers = new List<CustomerDTO>();
            List<customer> lstCustomerEntity = new List<customer>();

            lstCustomerEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<customer>>().GetQuery()
                            .Where(item => item.Cust_IsDeleted == false && item.Cust_Status == false && item.Cust_IsVarified == true)
                            .OrderBy(order => order.Cust_FirmName).ToList();
            AutoMapper.Mapper.Map(lstCustomerEntity, lstCustomers);
            return lstCustomers;
        }

        /// <summary>
        /// Get list of activate customers for customer DCA association
        /// </summary>
        /// <returns>returns list of customers where status is activated</returns>
        public IList<CustomerDTO> GetCustomerForDCAAssociation()
        {
            List<CustomerDTO> lstCustomers = new List<CustomerDTO>();
            List<customer> lstCustomerEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<customer>>().GetQuery()
                    .Where(item => item.Cust_IsDeleted == false && item.Cust_Status == true)
                    .OrderBy(order => order.Cust_FirmName).ToList();

            AutoMapper.Mapper.Map(lstCustomerEntity, lstCustomers);
            return lstCustomers;
        }

        /// <summary>
        /// Get list of customers for sending SMS
        /// </summary>
        /// <returns>returns list of customers where Cust_RegCustType status is activated</returns>
        public IList<CustomerDTO> GetCustomerForSMSSending()
        {
            List<CustomerDTO> lstCustomers = new List<CustomerDTO>();
            List<customer> lstCustomerEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<customer>>().GetQuery()
                    .Where(item => item.Cust_IsDeleted == false && item.Cust_Status == true && item.Cust_RegCustType == true && item.Cust_Code != null)
                    .OrderBy(order => order.Cust_FirmName).ToList();

            AutoMapper.Mapper.Map(lstCustomerEntity, lstCustomers);
            return lstCustomers;
        }

        /// <summary>
        /// Get list of new customers for sending SMS
        /// </summary>
        /// <returns>returns list of customers added recently where Cust_RegCustType status is activated</returns>
        public IList<CustomerDTO> GetNewCustomerForSMSSending()
        {
            List<CustomerDTO> lstCustomers = new List<CustomerDTO>();
            List<customer> lstCustomerEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<customer>>().GetQuery()
                    .Where(item => item.Cust_IsDeleted == false && item.Cust_Status == true && item.Cust_RegCustType == true && item.Cust_Code != null
                            && item.Cust_IsBlacklisted == false && item.Cust_SmsSent == false).OrderBy(order => order.Cust_FirmName).ToList();

            AutoMapper.Mapper.Map(lstCustomerEntity, lstCustomers);
            return lstCustomers;
        }

        /// <summary>
        /// Delete Customer by customerId
        /// </summary>
        /// <param name="customerId">Int32:customerId</param>
        public void DeleteCustomer(int customerId)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                IList<TruckDetailsDTO> lstTruckDetails = ESalesUnityContainer.Container.Resolve<ITruckService>()
                    .GetTruckDetailsForCustomer(customerId);

                (from truckDetail in lstTruckDetails select truckDetail).Update(
                    truckDetail => truckDetail.Truck_IsDeleted = true);

                foreach (TruckDetailsDTO truckDetails in lstTruckDetails)
                {
                    ESalesUnityContainer.Container.Resolve<ITruckService>().DeleteTruck(truckDetails);
                }

                IList<AuthRepDTO> lstAuthRepDetails = ESalesUnityContainer.Container.Resolve<IAuthRepService>()
                    .GetAuthRepDetailsForCustomer(customerId);

                (from authRepDetails in lstAuthRepDetails select authRepDetails).Update(
                    authRepDetails => authRepDetails.AuthRep_IsDeleted = true);

                foreach (var authRepDetails in lstAuthRepDetails)
                {
                    ESalesUnityContainer.Container.Resolve<IAuthRepService>().DeleteAuthRep(authRepDetails);
                }

                IList<CustomerMaterialMapDTO> lstCustMaterialDetails = ESalesUnityContainer.Container
                    .Resolve<ICustomerMaterialService>().GetCustomerMaterialDetailsByCustomerId(customerId);

                (from customerMaterials in lstCustMaterialDetails select customerMaterials).Update(
                    customerMaterials => customerMaterials.Cust_Mat_IsDeleted = true);

                foreach (var customerMaterials in lstCustMaterialDetails)
                {
                    CustomerMaterialService custMaterialService = new CustomerMaterialService();
                    custMaterialService.DeleteCustomerMaterials(customerMaterials);
                }

                CustomerDocService custDocuments = new CustomerDocService();
                IList<CustomerDocDetailsDTO> lstCustDocDetails = custDocuments.GetCustomerDocumentDetails(customerId);

                (from customerDocs in lstCustDocDetails select customerDocs).Update(
                    customerDocs => customerDocs.Cust_Doc_IsDeleted = true);

                foreach (CustomerDocDetailsDTO customerDocs in lstCustDocDetails)
                {
                    DeleteCustomerDocumentDetails(customerDocs);
                }

                CustomerDTO customerDetails = GetCustomerDetailsById(customerId);
                customerDetails.Cust_IsDeleted = true;

                customer customerEntity = new customer();
                AutoMapper.Mapper.Map(customerDetails, customerEntity);

                ESalesUnityContainer.Container.Resolve<IGenericRepository<customer>>().Update(customerEntity);
                transactionScope.Complete();
            }
        }

        /// <summary>
        /// Delete Customer Document Details
        /// </summary>
        /// <param name="customerDocs"></param>
        private static void DeleteCustomerDocumentDetails(CustomerDocDetailsDTO customerDocs)
        {
            customerdocdetail custDocEntity = new customerdocdetail();
            AutoMapper.Mapper.Map(customerDocs, custDocEntity);
            ESalesUnityContainer.Container.Resolve<IGenericRepository<customerdocdetail>>().Update(custDocEntity);
        }

        /// <summary>
        /// Get ustomer Details By MobileNumber
        /// </summary>
        /// <param name="mobileNumber">string:mobileNumber</param>
        /// <returns>returns list of customers where Cust_MobileNo matches with mobileNumber</returns>
        public IList<CustomerDTO> GetCustomerDetailsByMobileNumber(string mobileNumber)
        {
            List<CustomerDTO> lstCustomer = new List<CustomerDTO>();
            List<customer> lstCustomerEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<customer>>().GetQuery()
                .Where(item => item.Cust_MobileNo == mobileNumber && item.Cust_IsDeleted == false && item.Cust_IsBlacklisted == false)
                .OrderBy(order => order.Cust_Code).ToList();

            AutoMapper.Mapper.Map(lstCustomerEntity, lstCustomer);
            return lstCustomer;
        }

        /// <summary>
        /// Get ustomer Details By MobileNumber and customer code
        /// </summary>
        /// <param name="mobileNumber">string:mobileNumber</param>
        /// <returns>returns list of customers where Cust_MobileNo matches with mobileNumber</returns>
        public CustomerDTO GetCustomerDetailsForCashSMS(string mobileNumber, string customerCode)
        {
            CustomerDTO customerDTO = new CustomerDTO();

            customer customerEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<customer>>().
                GetSingle(item => item.Cust_MobileNo == mobileNumber
                    && item.Cust_Code == customerCode
                    && item.Cust_IsDeleted == false
                    && item.Cust_IsBlacklisted == false);

            if (customerEntity != null)
            {
                AutoMapper.Mapper.Map(customerEntity, customerDTO);
            }
            else
            {
                customerDTO = null;
            }
            
            return customerDTO;
        }


        /// <summary>
        /// Get ustomer Details By MobileNumber and customer code
        /// </summary>
        /// <param name="mobileNumber">string:mobileNumber</param>
        /// <returns>returns list of customers where Cust_MobileNo matches with mobileNumber</returns>
        public CustomerDTO GetBlacklistedCustomerDetailsForCashSMS(string mobileNumber, string customerCode)
        {
            CustomerDTO customerDTO = new CustomerDTO();

            customer customerEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<customer>>().
                GetSingle(item => item.Cust_MobileNo == mobileNumber
                    && item.Cust_Code == customerCode);

            if (customerEntity != null)
            {
                AutoMapper.Mapper.Map(customerEntity, customerDTO);
            }
            else
            {
                customerDTO = null;
            }

            return customerDTO;
        }

        /// <summary>
        /// Get Customer Details By Code
        /// </summary>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        public CustomerDTO GetCustomerDetailsByCode(string customerCode)
        {
            CustomerDTO customerDetails = new CustomerDTO();
            customer customerEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<customer>>()
                .GetSingle(item => item.Cust_Code == customerCode);

            AutoMapper.Mapper.Map(customerEntity, customerDetails);
            return customerDetails;
        }

        public CustomerDTO GetActiveCustomerDetailsByCode(string customerCode)
        {
            CustomerDTO customerDetails = new CustomerDTO();
            customer customerEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<customer>>()
                .GetSingle(item => item.Cust_Code == customerCode && item.Cust_IsDeleted == false);

            AutoMapper.Mapper.Map(customerEntity, customerDetails);
            return customerDetails;
        }

        public void UpdateCustomerDetails(CustomerDTO customerDetails)
        {
            customer customerEntity = new customer();
            AutoMapper.Mapper.Map(customerDetails, customerEntity);
            ESalesUnityContainer.Container.Resolve<IGenericRepository<customer>>().Update(customerEntity);
        }

        public void EditCustomerDocumentDetails(IList<CustomerDocDetailsDTO> listCustDocDetails, IList<CustomerDocumentsDTO> listCustDocument)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                for (int i = 0; i < listCustDocDetails.Count; i++)
                {
                    customerdocdetail custDocdetailsEntity = new customerdocdetail();

                    CustomerDocService custDocuments = new CustomerDocService();
                    CustomerDocDetailsDTO customerDocDetail = custDocuments.GetCustomerDocumentDetailsByDocIdAndCustId(
                        listCustDocDetails[i].Cust_Doc_CustId, listCustDocDetails[i].Cust_Doc_DocId);

                    if (customerDocDetail.Cust_Doc_Id > 0)
                    {
                        AutoMapper.Mapper.Map(customerDocDetail, custDocdetailsEntity);

                        custDocdetailsEntity.Cust_Doc_FileName = listCustDocDetails[i].Cust_Doc_FileName;
                        custDocdetailsEntity.Cust_Doc_No = listCustDocDetails[i].Cust_Doc_No;
                        custDocdetailsEntity.Cust_Doc_ExDate = listCustDocDetails[i].Cust_Doc_ExDate;

                        ESalesUnityContainer.Container.Resolve<IGenericRepository<customerdocdetail>>()
                            .Update(custDocdetailsEntity);
                    }
                    else
                    {
                        AutoMapper.Mapper.Map(listCustDocDetails[i], custDocdetailsEntity);
                        ESalesUnityContainer.Container.Resolve<IGenericRepository<customerdocdetail>>()
                            .Save(custDocdetailsEntity);
                    }

                    CustomerDocumentsDTO customerDocument = new CustomerDocumentsDTO();
                    CustomerDocumentsViewDTO customerDocumentView = new CustomerDocumentsViewDTO();
                    AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<customerdocumentsview>>()
                    .GetSingle(item => item.CustDoc_Doc_Id == custDocdetailsEntity.Cust_Doc_Id && item.CustDoc_IsDeleted == false), customerDocumentView);

                    AutoMapper.Mapper.Map(customerDocumentView, customerDocument);

                    customerdocument custDocumentEntity = new customerdocument();

                    if (customerDocument.CustDoc_Id > 0)
                    {
                        AutoMapper.Mapper.Map(customerDocument, custDocumentEntity);

                        if (listCustDocument[i].CustDoc_File == null)
                        {
                            custDocumentEntity.CustDoc_IsDeleted = true;
                        }
                        else
                        {
                            custDocumentEntity.CustDoc_File = listCustDocument[i].CustDoc_File;
                        }

                        ESalesUnityContainer.Container.Resolve<IGenericRepository<customerdocument>>().Update(custDocumentEntity);
                    }
                    else
                    {
                        AutoMapper.Mapper.Map(listCustDocument[i], custDocumentEntity);
                        custDocumentEntity.CustDoc_Doc_Id = custDocdetailsEntity.Cust_Doc_Id;

                        ESalesUnityContainer.Container.Resolve<IGenericRepository<customerdocument>>().Save(custDocumentEntity);
                    }
                }
                transactionScope.Complete();
            }
        }

        public List<int> GetCustomerIdByBusinessTypeId(int businessTypeId)
        {
            List<CustomerDTO> lstCustomerDetails = new List<CustomerDTO>();

            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<customer>>()
                .GetQuery().ToList(), lstCustomerDetails);

            List<int> lstCustomerId = lstCustomerDetails.Where(item => item.Cust_BusinessType == businessTypeId).Select(item => item.Cust_Id).ToList();
            return lstCustomerId;
        }

        /// <summary>
        /// Validates customer details and retrieve customer owner name and trade name
        /// </summary>
        /// <param name="customerCode">Customer code</param>
        /// <param name="validationTypeId">Validatation type id</param>
        /// <param name="validationValue">Validation value</param>
        /// <returns>returns customer owner name and trade name</returns>
        public IList<CustomerDTO> ValidateCustomerDetails(string customerCode, int validationTypeId, string validationValue)
        {
            IList<CustomerDTO> lstCustomer = new List<CustomerDTO>();

            if (!string.IsNullOrEmpty(customerCode) && validationTypeId == 1)
            {
                AutoMapper.Mapper.Map(base.CustomerRepository.GetQuery().Where(item => 
                    item.Cust_Code == customerCode && item.Cust_BankAccountNo == validationValue).
                    ToList(), lstCustomer);
            }
            else if (!string.IsNullOrEmpty(customerCode) && validationTypeId == 2)
            {
                AutoMapper.Mapper.Map(base.CustomerRepository.GetQuery().Where(item => 
                    item.Cust_Code == customerCode && item.Cust_MobileNo == validationValue).
                    ToList(), lstCustomer);
            }
            else if (validationValue == "0")
            {
                AutoMapper.Mapper.Map(base.CustomerRepository.GetQuery().Where(item => 
                    item.Cust_Id == validationTypeId).ToList(), lstCustomer);
            }
            else
            {
                AutoMapper.Mapper.Map((from customerItem in base.CustomerRepository.GetQuery().Where(item => item.Cust_Code == customerCode)
                                       join customerDoc in base.CustomerDocRepository.GetQuery() on customerItem.Cust_Id equals customerDoc.Cust_Doc_CustId
                                       where customerDoc.Cust_Doc_DocId == validationTypeId && customerDoc.Cust_Doc_No == validationValue
                                       select customerItem).ToList(), lstCustomer);
            }
            return lstCustomer;
        }

		public int SaveAndUpdateCustomerPatner(CustomerPartnerDTO customerPartner)
        {
            customerpartner customerpartnerEntity = new customerpartner();
            AutoMapper.Mapper.Map(customerPartner, customerpartnerEntity);

            if (customerpartnerEntity.Cust_Partner_ID > 0)
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<customerpartner>>().Update(customerpartnerEntity);
            }
            else
            {
                //Save agent details
                ESalesUnityContainer.Container.Resolve<IGenericRepository<customerpartner>>().Save(customerpartnerEntity);
            }
            return customerpartnerEntity.Cust_Partner_ID;
        }

        public List<CustomerPartnerDTO> GetPatnerList(int customerId)
        {
            List<CustomerPartnerDTO> lstCustomerPartner = new List<CustomerPartnerDTO>();
            List<customerpartner> lstCustomerPartnerEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<customerpartner>>().GetQuery()
                .Where(item => item.Cust_Partner_CustId == customerId)
                .OrderBy(order => order.Cust_Partner_ID).ToList();

            AutoMapper.Mapper.Map(lstCustomerPartnerEntity, lstCustomerPartner);
            return lstCustomerPartner;
        }


        public IList<CustomerDTO> GetActiveCustomerList()
        {
            List<CustomerDTO> lstCustomers = new List<CustomerDTO>();
            List<customer> lstCustomerEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<customer>>().GetQuery()
                    .Where(item => item.Cust_IsDeleted == false && item.Cust_Status == true && item.Cust_RegCustType == true && item.Cust_Code != null
                            && item.Cust_IsBlacklisted == false).OrderBy(order => order.Cust_FirmName).ToList();

            AutoMapper.Mapper.Map(lstCustomerEntity, lstCustomers);
            return lstCustomers;
        }
    }
}