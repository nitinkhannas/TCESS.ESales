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
    public class CustomerMastersService : ICustomerMastersService
    {
        /// <summary>
        /// Get Truck Reg Type List By Type Id
        /// </summary>
        /// <param name="businessTypeId">Int32:truckregTypeId</param>
        /// <returns></returns>
        public TruckRegTypeDTO GettruckregTypeListByTypeId(int truckregTypeId)
        {
            TruckRegTypeDTO truckregTypeDTO = new TruckRegTypeDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<truckregtype>>()
                .GetSingle(item => item.TruckRegType_Id == truckregTypeId), truckregTypeDTO);
            return truckregTypeDTO;
        }

        /// <summary>
        /// Save And Update Bussiness Type
        /// </summary>
        /// <param name="businessTypeDetails"></param>
        /// <returns></returns>
        public int SaveAndUpdateTruckregType(TruckRegTypeDTO truckregTypeDetails)
        {
            truckregtype truckregTypeEntity = new truckregtype();
            AutoMapper.Mapper.Map(truckregTypeDetails, truckregTypeEntity);

            if (truckregTypeEntity.TruckRegType_Id == 0)
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<truckregtype>>().Save(truckregTypeEntity);
            }
            else
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<truckregtype>>().Update(truckregTypeEntity);
            }
            return truckregTypeEntity.TruckRegType_Id;
        }        

        /// <summary>
        /// Get the list of all active TruckReg  Type
        /// </summary>
        /// <returns>List of  Truck Registration Type</returns>
        public IList<TruckRegTypeDTO> GetTruckRegTypeList()
        {
            List<TruckRegTypeDTO> lsttruckregTypes = new List<TruckRegTypeDTO>();
            List<truckregtype> lsttruckregTypesEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<truckregtype>>().GetQuery().Where(item => item.TruckRegType_IsDeleted == false)
                .OrderBy(order => order.TruckRegType_Name).ToList();

            AutoMapper.Mapper.Map(lsttruckregTypesEntity, lsttruckregTypes);
            return lsttruckregTypes;
        }

        /// <summary>
        /// Delete Truck Registration  Type
        /// </summary>
        /// <param name="businessTypeId">Int32:businessTypeId</param>
        public void DeleteTruckregType(int truckregTypeId)
        {
            TruckRegTypeDTO businessType = new TruckRegTypeDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<truckregtype>>()
                .GetSingle(item => item.TruckRegType_Id == truckregTypeId), businessType);

            businessType.TruckRegType_IsDeleted = true;
            SaveAndUpdateTruckregType(businessType);
        }

        /// <summary>
        /// Get Alloted Quantity List
        /// </summary>
        /// <returns></returns>
        public IList<AllotedQuantityDTO> GetAllotedQuantityList()
        {
            List<AllotedQuantityDTO> lstAllotedQuantity = new List<AllotedQuantityDTO>();
            List<allotedquantity> lstAllotedQuantityEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<allotedquantity>>().GetQuery()
                .Where(item => item.Alloted_IsDeleted == false && item.Alloted_Id > 1)
                .OrderBy(order => order.Alloted_Id).ToList();

            AutoMapper.Mapper.Map(lstAllotedQuantityEntity, lstAllotedQuantity);
            return lstAllotedQuantity;
        }

        /// <summary>
        /// Verify Truck registration  Type Exists or not bytruckregTypeId and truckreg Type
        /// </summary>
        /// <param name="businessTypeId">Int32:truckregTypeId</param>
        /// <param name="businessType">String:truckregType</param>
        /// <returns></returns>
        public bool IstruckregTypeExists(int truckregTypeId, string truckregType)
        {
            TruckRegTypeDTO truckregTypeDetails = new TruckRegTypeDTO();
            bool result = false;

            if (truckregTypeId == 0)
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<truckregtype>>()
                    .GetSingle(item => item.TruckRegType_Name == truckregType && item.TruckRegType_IsDeleted == false),
                    truckregTypeDetails);
            }
            else
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<truckregtype>>()
                    .GetSingle(item => item.TruckRegType_Name == truckregType && item.TruckRegType_IsDeleted == false &&
                        item.TruckRegType_Id != truckregTypeId), truckregTypeDetails);
            }

            if (truckregTypeDetails.TruckRegType_Id > 0)
            {
                result = true;
            }
            return result;
        }        

        public IList<LiftingIntervalDTO> GetLiftingIntervalList()
        {
            List<LiftingIntervalDTO> lstLiftingInterval = new List<LiftingIntervalDTO>();
            List<liftinginterval> lstLiftingIntervalEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<liftinginterval>>().GetQuery().OrderBy(order => order.liftinginterval_Id).ToList();

            AutoMapper.Mapper.Map(lstLiftingIntervalEntity, lstLiftingInterval);
            return lstLiftingInterval;
        }
    }
}