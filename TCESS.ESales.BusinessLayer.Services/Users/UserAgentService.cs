#region Using directives

using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.PersistenceLayer.Entity;
using TCESS.ESales.PersistenceLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.DataTransferObjects.Users;
using TCESS.ESales.BusinessLayer.Interfaces.Users;


#endregion

namespace TCESS.ESales.BusinessLayer.Services.Users
{
    public class UserAgentService : UserBaseService, IUserAgentService
    {
        /// <summary>
        /// Save User Agent Mapping
        /// </summary>
        /// <param name="userAgentMapDetails"></param>
        public void SaveUserAgentMapping(UserAgentMappingDTO userAgentMapDetails)
        {
            useragentmapping userAgentMappingEntity = new useragentmapping();
            AutoMapper.Mapper.Map(userAgentMapDetails, userAgentMappingEntity);
            ESalesUnityContainer.Container.Resolve<IGenericRepository<useragentmapping>>().Save(userAgentMappingEntity);
        }

        /// <summary>
        /// Get Users And Agent Details
        /// </summary>
        /// <returns></returns>
        public IList<UserAgentMappingDTO> GetUsersAndAgentDetails()
        {
            //To retrive all users and associated agents from database
            List<UserAgentMappingDTO> lstUserAgentMappingDTO = (from uamItem in ESalesUnityContainer.Container
                                                                    .Resolve<IGenericRepository<useragentmapping>>().GetQuery().Where(item => item.UAM_IsDeleted == false) 
                                                                join upmItem in base.UserPaymentModeRepository.GetQuery().Where(item => item.UPM_IsDeleted == false) 
                                                                on uamItem.UAM_User_Id equals upmItem.UPM_UserId into item 
                                                                from subItem in item.DefaultIfEmpty()
                                                                select new UserAgentMappingDTO
                                                                {
                                                                    UAM_Id = uamItem.UAM_Id,
                                                                    UAM_Agent_Id = uamItem.UAM_Agent_Id,
                                                                    UAM_Agent_Name = uamItem.agentdetail.Agent_Name,
                                                                    UAM_User_Id = uamItem.UAM_User_Id,
                                                                    UPM_PaymentModeId = subItem == null ? 0 : subItem.UPM_PaymentMode,
                                                                    UPM_PaymentMode = subItem == null ? string.Empty : subItem.paymentmode.Paymentmode_Name
                                                                }).ToList();
            return lstUserAgentMappingDTO;
        }

        /// <summary>
        /// Check If Agent Not Referenced by agentId
        /// </summary>
        /// <param name="agentId">Int32:agentId</param>
        /// <returns></returns>
        public bool CheckIfAgentNotReferenced(int agentId)
        {
            bool isAgentExist = false;
            UserAgentMappingDTO userAgentDetails = new UserAgentMappingDTO();

            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<useragentmapping>>()
                .GetSingle(item => item.UAM_Agent_Id == agentId && item.UAM_IsDeleted == false), userAgentDetails);

            if (userAgentDetails.UAM_Agent_Id  != 0)
            {
                isAgentExist = true;
            }

            //return agent details
            return isAgentExist;
        }

        /// <summary>
        /// Delete User Agent Mapping by userAgentMapId
        /// </summary>
        /// <param name="userAgentMapId">Int32:userAgentMapId</param>
        public void DeleteUserAgentMapping(int userAgentMapId)
        {
            UserAgentMappingDTO userAgentMapDTO = GetUserAgentMappingByMappingId(userAgentMapId);
            userAgentMapDTO.UAM_IsDeleted = true;

            useragentmapping userAgentEntity = new useragentmapping();
            AutoMapper.Mapper.Map(userAgentMapDTO, userAgentEntity);

            ESalesUnityContainer.Container.Resolve<IGenericRepository<useragentmapping>>().Update(userAgentEntity);
        }

        /// <summary>
        /// Get User Agent Mapping By MappingId
        /// </summary>
        /// <param name="userAgentMapId">Int32:userAgentMapId</param>
        /// <returns></returns>
        public UserAgentMappingDTO GetUserAgentMappingByMappingId(int userAgentMapId)
        {
            //To select agent by agent id
            UserAgentMappingDTO userAgentDetails = new UserAgentMappingDTO();

            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<useragentmapping>>()
                .GetSingle(item => item.UAM_Id == userAgentMapId && item.UAM_IsDeleted == false), userAgentDetails);

            //return agent details
            return userAgentDetails;
        }

        /// <summary>
        /// Get Agent details by currently logged in user id
        /// </summary>
        /// <param name="userId">Int: currently logged in user id</param>
        /// <returns>returns complete details of agent</returns>
        public UserAgentMappingDTO GetAgentByUserId(int userId)
        {
            //To select agent by agent id
            UserAgentMappingDTO userAgentDetails = new UserAgentMappingDTO();

            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<useragentmapping>>()
                .GetSingle(item => item.UAM_User_Id == userId && item.UAM_IsDeleted == false), userAgentDetails);

            return userAgentDetails;
        }

        /// <summary>
        /// Update User Agent Details
        /// </summary>
        /// <param name="userAgentMapDetails"></param>
        public void UpdateUserAgentDetails(UserAgentMappingDTO userAgentMapDetails)
        {
            useragentmapping userAgentMapEntity = new useragentmapping();
            AutoMapper.Mapper.Map(userAgentMapDetails, userAgentMapEntity);
            ESalesUnityContainer.Container.Resolve<IGenericRepository<useragentmapping>>().Update(userAgentMapEntity);
        }
    }
}