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
    public class LocationService : ILocationService
    {
        /// <summary>
        /// to Save State
        /// </summary>
        /// <param name="stateDetail"></param>
        /// <returns></returns>
        public int SaveState(StateDTO stateDetail)
        {
            state stateEntity = new state();
            AutoMapper.Mapper.Map(stateDetail, stateEntity);
            ESalesUnityContainer.Container.Resolve<IGenericRepository<state>>().Save(stateEntity);
            
            //return value
            return stateEntity.State_Id;
        }

        /// <summary>
        /// Get State By StateId
        /// </summary>
        /// <param name="stateId">Int32:stateId</param>
        /// <returns></returns>
        public StateDTO GetStateByStateId(int stateId)
        {
            StateDTO stateDetail = new StateDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<state>>()
                .GetSingle(item => item.State_Id == stateId), stateDetail);
            
            //return value
            return stateDetail;
        }

        /// <summary>
        /// To Update State
        /// </summary>
        /// <param name="stateDetail"></param>
        /// <returns></returns>
        public int UpdateState(StateDTO stateDetail)
        {
            state stateEntity = new state();
            AutoMapper.Mapper.Map(stateDetail, stateEntity);
            ESalesUnityContainer.Container.Resolve<IGenericRepository<state>>().Update(stateEntity);
            
            //return value
            return stateEntity.State_Id;          
        }

        /// <summary>
        /// Get list of all active States.
        /// </summary>
        /// <returns>Returns List of States</returns>
        public IList<StateDTO> GetStateList()
        {
            List<StateDTO> lstStates = new List<StateDTO>();
            List<state> lstStatesEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<state>>().GetQuery()
                .Where(item => item.State_IsDeleted == false).OrderBy(order => order.State_Name).ToList();

            AutoMapper.Mapper.Map(lstStatesEntity, lstStates);

            //return the value
            return lstStates;
        }
       
        /// <summary>
        /// Get list of all active Districts.
        /// </summary>
        /// <returns>Returns List of Districts</returns>
        public IList<DistrictDTO> GetDistrictList()
        {
            List<DistrictDTO> lstDistricts = new List<DistrictDTO>();
            List<district> lstDistrictsEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<district>>().GetQuery()
                .Where(item => item.Dist_IsDeleted == false).OrderBy(order => order.Dist_Name).ToList();

            AutoMapper.Mapper.Map(lstDistrictsEntity, lstDistricts);

            //return the value
            return lstDistricts;
        }

        /// <summary>
        /// Get District List By StateId
        /// </summary>
        /// <param name="stateId">Int32:stateId</param>
        /// <returns></returns>
        public IList<DistrictDTO> GetDistrictListByStateId(int stateId)
        {
            List<DistrictDTO> lstDistricts = new List<DistrictDTO>();
            List<district> lstDistrictsEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<district>>().GetQuery()
                .Where(item => item.Dist_IsDeleted == false && item.Dist_StateId == stateId)
               .OrderBy(order => order.Dist_Name).ToList();

            AutoMapper.Mapper.Map(lstDistrictsEntity, lstDistricts);

            //return the value
            return lstDistricts;
        }

        /// <summary>
        /// Save And Update District
        /// </summary>
        /// <param name="districtDetails"></param>
        /// <returns></returns>
        public int SaveAndUpdateDistrict(DistrictDTO districtDetails)
        {            
            district districtEntity = new district();
            AutoMapper.Mapper.Map(districtDetails, districtEntity);
            
            if (districtEntity.Dist_Id == 0)
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<district>>().Save(districtEntity);
            }
            else
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<district>>().Update(districtEntity);
            }

            //return value
            return districtEntity.Dist_Id;
        }

        /// <summary>
        /// Get District By DistId
        /// </summary>
        /// <param name="districtId">Int32:districtId</param>
        /// <returns></returns>
        public DistrictDTO GetDistrictByDistId(int districtId)
        {
           DistrictDTO districtDetails = new DistrictDTO();

           AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<district>>()
               .GetSingle(item => item.Dist_IsDeleted == false && item.Dist_Id == districtId),
              districtDetails);
            
            //return the value
           return districtDetails;
        }

        /// <summary>
        /// Verify District Exists or not by stateId,districtId and districtName
        /// </summary>
        /// <param name="stateId">Int32:stateId</param>
        /// <param name="districtId">Int32:districtId</param>
        /// <param name="districtName">string:districtName</param>
        /// <returns></returns>
        public bool DistrictExists(int stateId, int districtId, string districtName)
        {
            DistrictDTO districtDetails = new DistrictDTO();
            bool result = false;

            if (districtId == 0)
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<district>>()
                    .GetSingle(item => item.Dist_StateId == stateId &&
                    item.Dist_Name == districtName && item.Dist_IsDeleted == false), districtDetails);
            }
            else
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<district>>()
                    .GetSingle(item => item.Dist_StateId == stateId && item.Dist_Name == districtName 
                        && item.Dist_IsDeleted == false && item.Dist_Id != districtId), districtDetails);
            }

            if (districtDetails.Dist_Id > 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Verify State exists or not by stateId,state
        /// </summary>
        /// <param name="stateId">Int32:stateId</param>
        /// <param name="state">string:state</param>
        /// <returns></returns>
        public bool StateExists(int stateId, string state)
        {
            StateDTO stateDetails = new StateDTO();
            bool result = false;

            if (stateId == 0)
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<state>>()
                    .GetSingle(item => item.State_Name == state && item.State_IsDeleted == false), stateDetails);
            }
            else
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<state>>()
                    .GetSingle(item=>item.State_Name == state && item.State_IsDeleted == false
                        && item.State_Id != stateId), stateDetails);
            }

            if (stateDetails.State_Id > 0)
            {
                result = true;
            }
            return result;
        }
    }
}