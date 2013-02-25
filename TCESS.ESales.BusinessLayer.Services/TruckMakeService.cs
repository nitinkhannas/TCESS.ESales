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
    public class TruckMakeService : ITruckMakeService
    {
        /// <summary>
        /// Save And Update Truck Make
        /// </summary>
        /// <param name="truckMakeDetail"></param>
        /// <returns></returns>
        public int SaveAndUpdateTruckMake(TruckMakeDTO truckMakeDetail)
        {
            truckmake truckmakeEntity = new truckmake();
            AutoMapper.Mapper.Map(truckMakeDetail, truckmakeEntity);

            if (truckMakeDetail.TruckMake_Id == 0)
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<truckmake>>().Save(truckmakeEntity);
            }
            else
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<truckmake>>().Update(truckmakeEntity);
            }

            //return the value
            return truckmakeEntity.TruckMake_Id;
        }

        /// <summary>
        /// Get Truck Make list
        /// </summary>
        /// <returns></returns>
        public IList<TruckMakeDTO> GetTruckMakelist()
        {
            List<TruckMakeDTO> lstTruckMakes = new List<TruckMakeDTO>();
            List<truckmake> lstTruckMakesEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<truckmake>>()
                .GetQuery().Where(item => item.TruckMake_IsDeleted == false)
                .OrderBy(order => order.TruckMake_Name).ToList();

            AutoMapper.Mapper.Map(lstTruckMakesEntity, lstTruckMakes);

            //return value
            return lstTruckMakes;
        }

        /// <summary>
        /// Get Truck Make By truckMakeId
        /// </summary>
        /// <param name="truckMakeId">Int32:truckMakeId</param>
        /// <returns></returns>
        public TruckMakeDTO GetTruckMakeById(int truckMakeId)
        {
            TruckMakeDTO objTruckMakeDTO = new TruckMakeDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<truckmake>>()
                .GetSingle(item => item.TruckMake_Id == truckMakeId), objTruckMakeDTO);

            //return value
            return objTruckMakeDTO;
        }

        /// <summary>
        /// Verify Truck Make Exists or not by truckMakeId and truckMakeName
        /// </summary>
        /// <param name="truckMakeId">Int32:truckMakeId</param>
        /// <param name="truckMakeName">string:truckMakeName</param>
        /// <returns></returns>
        public bool TruckMakeExists(int truckMakeId, string truckMakeName)
        {
            TruckMakeDTO objTruckMakeDTO = new TruckMakeDTO();
            if (truckMakeId == 0)
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<truckmake>>()
                    .GetSingle(item => item.TruckMake_Name == truckMakeName
                        && item.TruckMake_IsDeleted == false), objTruckMakeDTO);
            }
            else
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<truckmake>>()
                    .GetSingle(item => item.TruckMake_Name == truckMakeName
                        && item.TruckMake_IsDeleted == false && item.TruckMake_Id != truckMakeId), objTruckMakeDTO);
            }

            if (objTruckMakeDTO.TruckMake_Id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}