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
    public class CustomerMaterialService : ICustomerMaterialService
    {
        /// <summary>
        /// Get Customer Material Details By Material Id
        /// </summary>
        /// <param name="materialId">Int32:materialId</param>
        /// <returns></returns>
        public IList<CustomerMaterialMapDTO> GetCustomerMaterialDetailsByMaterialId(int materialId)
        {
            List<CustomerMaterialMapDTO> lstCustomerMaterialMapDTO = new List<CustomerMaterialMapDTO>();

            List<customermaterialmap> lstCustomerMaterialEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<customermaterialmap>>().GetQuery()
                .Where(item => item.Cust_Mat_MaterialId == materialId && item.Cust_Mat_IsDeleted == false).ToList();

            AutoMapper.Mapper.Map(lstCustomerMaterialEntity, lstCustomerMaterialMapDTO);
            return lstCustomerMaterialMapDTO;
        }

        /// <summary>
        /// Get Customer Material Details By Customer Id
        /// </summary>
        /// <param name="custID">Int32:custID</param>
        /// <returns></returns>
        public IList<CustomerMaterialMapDTO> GetCustomerMaterialDetailsByCustomerId(int customerId)
        {
            List<CustomerMaterialMapDTO> lstCustomerMaterialMapDTO = new List<CustomerMaterialMapDTO>();

            List<customermaterialmap> lstCustomerMaterialEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<customermaterialmap>>().GetQuery()
                .Where(item => item.Cust_Mat_CustId == customerId && item.Cust_Mat_IsDeleted == false).ToList();

            AutoMapper.Mapper.Map(lstCustomerMaterialEntity, lstCustomerMaterialMapDTO);
            return lstCustomerMaterialMapDTO;
        }

        /// <summary>
        /// Get Customer Material Details by customerId
        /// </summary>
        /// <param name="custID">Int32:custID</param>
        /// <returns></returns>
        public IList<CustomerMaterialMapDTO> GetCustomerMaterialDetails(int customerId)
        {
            List<CustomerMaterialMapDTO> lstCustomerMaterialMapDTO = new List<CustomerMaterialMapDTO>();

            IList<customermaterialmap> lstCustomerMaterialEntity = ESalesUnityContainer.Container
                .Resolve<ICustomerMaterialMapRepository>().GetCustomerMaterialDetails(customerId);

            AutoMapper.Mapper.Map(lstCustomerMaterialEntity, lstCustomerMaterialMapDTO);
            return lstCustomerMaterialMapDTO;
        }

        public bool SaveAndUpdateCustomerMaterial(IList<CustomerMaterialMapDTO> listCustomerMaterial)
        {     
            foreach (CustomerMaterialMapDTO item in listCustomerMaterial)
            {
                customermaterialmap custMaterialMapEntity = new customermaterialmap();

                CustomerMaterialMapDTO custMaterialMap = GetCustomerMaterialByCustomerAndMaterialId(item.Cust_Mat_CustId,
                    item.Cust_Mat_MaterialId);

                if (custMaterialMap.Cust_Mat_Id > 0)
                {
                    AutoMapper.Mapper.Map(custMaterialMap, custMaterialMapEntity);
                    custMaterialMapEntity.Cust_Mat_AllotedQuantityId = item.Cust_Mat_AllotedQuantityId;
                    custMaterialMapEntity.Cust_Mat_LiftingLimit = item.Cust_Mat_LiftingLimit;
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<customermaterialmap>>().Update(custMaterialMapEntity);
                }
                else
                {
                    AutoMapper.Mapper.Map(item, custMaterialMapEntity);
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<customermaterialmap>>().Save(custMaterialMapEntity);
                }
            }
            return true;
        }

        /// <summary>
        /// Save And Update Customer Material Details
        /// </summary>
        /// <param name="lstCustomerMaterial"></param>
        public void SaveAndUpdateCustomerMaterialDetails(IList<CustomerMaterialMapDTO> listCustomerMaterial)
        {
            foreach (CustomerMaterialMapDTO item in listCustomerMaterial)
            {
                customermaterialmap custMaterialMapEntity = new customermaterialmap();

                CustomerMaterialMapDTO custMaterialMap = GetCustomerMaterialByCustomerAndMaterialId(item.Cust_Mat_CustId,
                    item.Cust_Mat_MaterialId);

                if (custMaterialMap.Cust_Mat_Id > 0)
                {
                    AutoMapper.Mapper.Map(custMaterialMap, custMaterialMapEntity);
                    custMaterialMapEntity.Cust_Mat_AllotedQuantityId = item.Cust_Mat_AllotedQuantityId;
                    custMaterialMapEntity.Cust_Mat_LiftingLimit = item.Cust_Mat_LiftingLimit;
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<customermaterialmap>>().Update(custMaterialMapEntity);
                }
                else
                {
                    AutoMapper.Mapper.Map(item, custMaterialMapEntity);
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<customermaterialmap>>().Save(custMaterialMapEntity);
                }
            }
        }

        /// <summary>
        /// Delete Customer Materials
        /// </summary>
        /// <param name="customerMaterials"></param>
        public void DeleteCustomerMaterials(CustomerMaterialMapDTO customerMaterials)
        {
            customermaterialmap customerMaterialMapEntity = new customermaterialmap();
            AutoMapper.Mapper.Map(customerMaterials, customerMaterialMapEntity);
            ESalesUnityContainer.Container.Resolve<IGenericRepository<customermaterialmap>>().Update(customerMaterialMapEntity);
        }

        /// <summary>
        /// Get Customer Material Details By Customer Code
        /// </summary>
        /// <param name="customerCode">string:customerCode</param>
        /// <returns></returns>
        public IList<CustomerMaterialMapDTO> GetCustomerMaterialDetailsByCustomerCode(string customerCode)
        {
            List<CustomerMaterialMapDTO> lstCustomerMaterialMapDTO = new List<CustomerMaterialMapDTO>();

            List<customermaterialmap> lstCustomerMaterialEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<customermaterialmap>>().GetQuery()
                .Where(item => item.customer.Cust_Code == customerCode && item.Cust_Mat_IsDeleted == false).ToList();

            AutoMapper.Mapper.Map(lstCustomerMaterialEntity, lstCustomerMaterialMapDTO);
            return lstCustomerMaterialMapDTO;
        }

        /// <summary>
        /// Get Customer Material ById
        /// </summary>
        /// <param name="customerMaterialId">Int32:customerMaterialId</param>
        /// <returns></returns>
        public CustomerMaterialMapDTO GetCustomerMaterialById(int customerMaterialId)
        {
            CustomerMaterialMapDTO customerMaterialDetails = new CustomerMaterialMapDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<customermaterialmap>>()
                .GetSingle(item => item.Cust_Mat_Id == customerMaterialId), customerMaterialDetails);
            return customerMaterialDetails;
        }

        /// <summary>
        /// Get Customer Material By Customer And Material Id
        /// </summary>
        /// <param name="customerId">Int32:customerId</param>
        /// <param name="materialTypeId">Int32:materialTypeId</param>
        /// <returns></returns>
        public CustomerMaterialMapDTO GetCustomerMaterialByCustomerAndMaterialId(int customerId, int materialTypeId)
        {
            CustomerMaterialMapDTO customerMaterialDetails = new CustomerMaterialMapDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<customermaterialmap>>()
                .GetSingle(item => item.Cust_Mat_MaterialId == materialTypeId
                    && item.Cust_Mat_CustId == customerId), customerMaterialDetails);
            return customerMaterialDetails;
        }

        public List<CustomerMaterialMapDTO> GetCustomerMaterialDetailsList()
        {
            List<CustomerMaterialMapDTO> lstCustomerMaterialMapDTO = new List<CustomerMaterialMapDTO>();

            List<customermaterialmap> lstCustomerMaterialEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<customermaterialmap>>()
                .GetQuery().ToList();

            AutoMapper.Mapper.Map(lstCustomerMaterialEntity, lstCustomerMaterialMapDTO);
            return lstCustomerMaterialMapDTO;
        }
    }
}