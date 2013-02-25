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
    public class CautionListService : ICautionListService
    {
        /// <summary>
        /// Get CautionList For Customers
        /// </summary>
        /// <param name="isBlacklisted">bool:isBlacklisted</param>
        /// <returns></returns>
        public IList<CustomerDTO> GetCautionListForCustomers(bool isBlacklisted)
        {
            List<CustomerDTO> lstCustomers = new List<CustomerDTO>();

            List<customer> lstCustomersEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<customer>>()
                .GetQuery().Where(item => item.Cust_IsBlacklisted == isBlacklisted && item.Cust_IsDeleted == true)
                .OrderBy(order => order.Cust_FirmName).ToList();

            AutoMapper.Mapper.Map(lstCustomersEntity, lstCustomers);

            //return value
            return lstCustomers;
        }

        /// <summary>
        /// Get CautionList For Trucks
        /// </summary>
        /// <param name="isBlacklisted">bool:isBlacklisted</param>
        /// <returns></returns>
        public IList<TruckDetailsDTO> GetCautionListForTrucks(bool isBlacklisted)
        {
            List<TruckDetailsDTO> lstTruckDetails = new List<TruckDetailsDTO>();

            List<truckdetail> lstTruckDetailsEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<truckdetail>>()
                .GetQuery().Where(item => item.Truck_IsBlacklisted == isBlacklisted && item.Truck_IsDeleted == true)
                .OrderBy(OrderedParallelQuery => OrderedParallelQuery.Truck_RegNo).ToList();

            AutoMapper.Mapper.Map(lstTruckDetailsEntity, lstTruckDetails);

            //return value
            return lstTruckDetails;
        }

        /// <summary>
        /// Get Caution List of Trucks By CustomerId
        /// </summary>
        /// <param name="custId"></param>
        /// <returns></returns>
        public IList<TruckDetailsDTO> GetCautionListForTrucksByCustId(int custId)
        {
            List<TruckDetailsDTO> lstTruckDetails = new List<TruckDetailsDTO>();

            List<truckdetail> lstTruckDetailsEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<truckdetail>>()
                .GetQuery().Where(item => item.Truck_CustomerId == custId && item.Truck_IsBlacklisted == false && item.Truck_IsDeleted == false)
                .OrderBy(T => T.Truck_RegNo).ToList();
            AutoMapper.Mapper.Map(lstTruckDetailsEntity, lstTruckDetails);

            return lstTruckDetails;
        }

        /// <summary>
        /// Update Caution List For Trucks
        /// </summary>
        /// <param name="truckDetails"></param>
        /// <returns></returns>
        public int UpdateCautionListForTrucks(TruckDetailsDTO truckDetails)
        {
            truckdetail truckdetailEntity = new truckdetail();
            AutoMapper.Mapper.Map(truckDetails, truckdetailEntity);

            ESalesUnityContainer.Container.Resolve<IGenericRepository<truckdetail>>().Update(truckdetailEntity);

            //return value
            return truckdetailEntity.Truck_Id;
        }

        /// <summary>
        /// Get Caution List For AuthReps
        /// </summary>
        /// <param name="isBlacklisted">bool:isBlacklisted</param>
        /// <returns></returns>
        public IList<AuthRepDTO> GetCautionListForAuthReps(bool isBlacklisted)
        {
            List<AuthRepDTO> lstAuthReps = new List<AuthRepDTO>();

            List<authrepdetail> lstAuthRepsEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<authrepdetail>>()
                .GetQuery().Where(item => item.AuthRep_IsDeleted == true
                && item.AuthRep_IsBlacklisted == isBlacklisted).OrderBy(order => order.AuthRep_Name).ToList();

            AutoMapper.Mapper.Map(lstAuthRepsEntity, lstAuthReps);

            //return value
            return lstAuthReps;

        }

        /// <summary>
        /// Update Caution List For AuthRep
        /// </summary>
        /// <param name="authRepDetails"></param>
        /// <returns></returns>
        public int UpdateCautionListForAuthRep(AuthRepDTO authRepDetails)
        {
            authrepdetail authrepdetailEntity = new authrepdetail();
            AutoMapper.Mapper.Map(authRepDetails, authrepdetailEntity);

            ESalesUnityContainer.Container.Resolve<IGenericRepository<authrepdetail>>().Update(authrepdetailEntity);

            //return value
            return authrepdetailEntity.AuthRep_Id;
        }

        /// <summary>
        /// Get Caution List For Standalone Trucks
        /// </summary>
        /// <param name="isBlacklisted">bool:</param>
        /// <returns></returns>
        public IList<StandaloneTrucksDTO> GetCautionListForStandaloneTrucks(bool isBlacklisted)
        {
            List<StandaloneTrucksDTO> lstStandaloneTruck = new List<StandaloneTrucksDTO>();

            List<standalonetruck> lstStandaloneTruckEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<standalonetruck>>().GetQuery().Where(item => item.StandaloneTruck_IsDeleted == true
                && item.StandaloneTruck_IsBlacklisted == isBlacklisted).OrderBy(order => order.StandaloneTruck_RegNo).ToList();

            AutoMapper.Mapper.Map(lstStandaloneTruckEntity, lstStandaloneTruck);

            //return value
            return lstStandaloneTruck;
        }

        /// <summary>
        /// Update Caution List For Standalone Trucks
        /// </summary>
        /// <param name="standaloneTruck"></param>
        /// <returns></returns>
        public int UpdateCautionListForStandaloneTrucks(StandaloneTrucksDTO standaloneTruck)
        {
            standalonetruck standaloneTruckEntity = new standalonetruck();
            AutoMapper.Mapper.Map(standaloneTruck, standaloneTruckEntity);

            ESalesUnityContainer.Container.Resolve<IGenericRepository<standalonetruck>>().Update(standaloneTruckEntity);

            //return value
            return standaloneTruckEntity.StandaloneTruck_Id;
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
                .GetSingle(item => item.Cust_Code == customerCode && (item.Cust_IsBlacklisted == false && item.Cust_IsDeleted == false));

            AutoMapper.Mapper.Map(customerEntity, customerDetails);
            return customerDetails;
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
                && item.Cust_Doc_IsDeleted == false && item.customer.Cust_IsBlacklisted == false), customerDocDetails);

            return customerDocDetails;
        }
    }
}