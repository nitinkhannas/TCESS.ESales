#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.PersistenceLayer.Entity;
using TCESS.ESales.PersistenceLayer.Interfaces;

#endregion

namespace TCESS.ESales.BusinessLayer.Services
{
    public class AgentMaterialPercentageService : IAgentMaterialPercentageService
    {
        /// <summary>
        /// To save the agent material percentage
        /// </summary>
        /// <param name="agentMaterialPercentageDetails"></param>
        public void SaveAgentMaterialPercentage(AgentMaterialPercentageDTO agentMaterialPercentageDetails)
        {
            agentmaterialpercentage agentMaterialPercentageEntity = new agentmaterialpercentage();
            AutoMapper.Mapper.Map(agentMaterialPercentageDetails, agentMaterialPercentageEntity);

            ESalesUnityContainer.Container.Resolve<IGenericRepository<agentmaterialpercentage>>()
                .Save(agentMaterialPercentageEntity);
        }

        /// <summary>
        /// To update the agent material percentage
        /// </summary>
        /// <param name="agentMaterialPercentageDetails"></param>
        /// <returns></returns>
        public int UpdateAgentPercentage(AgentMaterialPercentageDTO agentMaterialPercentageDetails)
        {
            agentmaterialpercentage agentMaterialPercentageEntity = new agentmaterialpercentage();
            AutoMapper.Mapper.Map(agentMaterialPercentageDetails, agentMaterialPercentageEntity);
            ESalesUnityContainer.Container.Resolve<IGenericRepository<agentmaterialpercentage>>()
                .Update(agentMaterialPercentageEntity);
            return agentMaterialPercentageEntity.AMP_Id;
        }

        /// <summary>
        /// get material percentage by Id
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="materialTypeId"></param>
        /// <returns></returns>
        public AgentMaterialPercentageDTO GetAgentMaterialPercentageByIds(int agentId, int materialTypeId)
        {
            AgentMaterialPercentageDTO agentMaterialPercentageDetails = new AgentMaterialPercentageDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<agentmaterialpercentage>>()
                .GetSingle(item => item.AMP_Agent_Id == agentId && item.AMP_MaterialType_Id == materialTypeId
                    && item.AMP_IsDeleted == false), agentMaterialPercentageDetails);
            return agentMaterialPercentageDetails;
        }

        /// <summary>
        /// get material percentage by material type ID
        /// </summary>
        /// <param name="materialTypeId"></param>
        /// <returns></returns>
        public IList<AgentMaterialPercentageDTO> GetAgentMaterialPercentByMaterialTypeId(int materialTypeId)
        {
            List<AgentMaterialPercentageDTO> lstAgentMaterialPercentageDTO = new List<AgentMaterialPercentageDTO>();
            List<agentmaterialpercentage> lstAgentMaterialPercentageEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<agentmaterialpercentage>>().GetQuery()
                .Where(item => item.AMP_MaterialType_Id == materialTypeId && item.AMP_IsDeleted == false && item.AMP_IsActive == true)
                .OrderBy(order => order.agentdetail.Agent_Name).ToList();

            AutoMapper.Mapper.Map(lstAgentMaterialPercentageEntity, lstAgentMaterialPercentageDTO);
            return lstAgentMaterialPercentageDTO;
        }

        /// <summary>
        /// To get Material Percentage by AgentID
        /// </summary>
        /// <param name="agentId"></param>
        /// <returns></returns>
        public IList<AgentMaterialPercentageDTO> GetAgentMaterialPercentByAgentId(int agentId)
        {
            List<AgentMaterialPercentageDTO> lstAgentMaterialPercentageDTO = new List<AgentMaterialPercentageDTO>();
            List<agentmaterialpercentage> lstAgentMaterialPercentageEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<agentmaterialpercentage>>()
                .GetQuery().Where(item => item.AMP_Agent_Id == agentId && item.AMP_IsDeleted == false)
                .OrderBy(order => order.agentdetail.Agent_Name).ToList();

            AutoMapper.Mapper.Map(lstAgentMaterialPercentageEntity, lstAgentMaterialPercentageDTO);
            return lstAgentMaterialPercentageDTO;
        }

        /// <summary>
        /// To select agent by agent id
        /// </summary>
        /// <param name="ampId"></param>
        /// <returns></returns>
        public AgentMaterialPercentageDTO GetAgentMaterialPercentageByAMPId(int ampId)
        {            
            AgentMaterialPercentageDTO agentMaterialPercentageDetails = new AgentMaterialPercentageDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<agentmaterialpercentage>>()
                .GetSingle(item => item.AMP_Id == ampId && item.AMP_IsDeleted == false), agentMaterialPercentageDetails);
            return agentMaterialPercentageDetails;
        }
        /// <summary>
        /// To Update Agent Material percentage
        /// </summary>
        /// <param name="listAgentMaterialPercentage"></param>
        public void UpdateAgentpercentage(List<AgentMaterialPercentageDTO> lstAgentMaterialPercentage)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                foreach (AgentMaterialPercentageDTO item in lstAgentMaterialPercentage)
                {
                    AgentMaterialPercentageDTO agentMaterialPercentage = GetAgentMaterialPercentageByAMPId(item.AMP_Id);
                    agentMaterialPercentage.AMP_Percentage = item.AMP_Percentage;
                    agentMaterialPercentage.AMP_LastUpdatedDate = DateTime.Now;
                    UpdateAgentPercentage(agentMaterialPercentage);
                }
                transactionScope.Complete();
            }
        }        
    }
}