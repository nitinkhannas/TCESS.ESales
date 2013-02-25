#region Using directives

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
    public class AgentService : IAgentService
    {
        /// <summary>
        /// Retrives all active agents of the system
        /// </summary>
        /// <returns>returns list of active agents</returns>
        public IList<AgentDTO> GetAgentList()
        {            
            List<AgentDTO> listAgent = new List<AgentDTO>();

            List<agentdetail> listAgentEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<agentdetail>>()
                .GetQuery().Where(item => item.Agent_IsDeleted == false).OrderBy(order => order.Agent_Name).ToList();

            AutoMapper.Mapper.Map(listAgentEntity, listAgent);
            return listAgent;
        }

        /// <summary>
        /// Save and update agent details in database
        /// </summary>
        /// <param name="agentDetails">object containing agent details</param>
        public int SaveAndUpdateAgent(AgentDTO agentDetails)
        {            
            agentdetail agentEntity = new agentdetail();
            AutoMapper.Mapper.Map(agentDetails, agentEntity);

            if (agentEntity.Agent_Id > 0)
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<agentdetail>>().Update(agentEntity);
            }
            else
            {
                //Save agent details
                ESalesUnityContainer.Container.Resolve<IGenericRepository<agentdetail>>().Save(agentEntity);

                //Gets list of materials from database
                IList<MaterialTypeDTO> listMaterial = ESalesUnityContainer.Container.Resolve<IMaterialTypeService>()
                    .GetMaterialTypeList(false);

                //Loops through list of materials and create mapping between agent and material
                foreach (var material in listMaterial)
                {                    
                    AgentMaterialPercentageDTO agentMaterialPercentageDetails = new AgentMaterialPercentageDTO();
                    agentMaterialPercentageDetails.AMP_Agent_Id = agentEntity.Agent_Id;
                    agentMaterialPercentageDetails.AMP_MaterialType_Id = material.MaterialType_Id;
                    agentMaterialPercentageDetails.AMP_IsActive = true;
                    agentMaterialPercentageDetails.AMP_CreatedBy = agentEntity.Agent_CreatedBy;
                    AgentMaterialPercentageService agentMatPercentageService = new AgentMaterialPercentageService();

                    //Save agent material mapping
                    agentMatPercentageService.SaveAgentMaterialPercentage(agentMaterialPercentageDetails);
                }
            }
            return agentEntity.Agent_Id;
        }

        /// <summary>
        /// Retrieves agent details by Agent Id
        /// </summary>
        /// <param name="agentId">Int32: agentId</param>
        /// <returns></returns>
        public AgentDTO GetAgentByAgentId(int agentId)
        {            
            AgentDTO agentDetails = new AgentDTO();
            
            //Select agent by agent id and maps it to DTO object
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<agentdetail>>()
                .GetSingle(item => item.Agent_Id == agentId && item.Agent_IsDeleted == false), agentDetails);
            return agentDetails;
        }

        /// <summary>
        /// Delete agent details by agent Id
        /// </summary>
        /// <param name="agentID">Agent Id</param>
        public void DeleteAgent(int agentId)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                AgentMaterialPercentageService agentMatPercentageService = new AgentMaterialPercentageService();
                IList<AgentMaterialPercentageDTO> lstAgentMaterialPercentage = agentMatPercentageService.GetAgentMaterialPercentByAgentId(agentId);

                foreach (AgentMaterialPercentageDTO item in lstAgentMaterialPercentage)
                {
                    AgentMaterialPercentageDTO agentMaterialPercentage = agentMatPercentageService
                        .GetAgentMaterialPercentageByAMPId(item.AMP_Id);

                    agentMaterialPercentage.AMP_IsDeleted = true;
                    agentMatPercentageService.UpdateAgentPercentage(agentMaterialPercentage);
                }

                AgentDTO agentDetails = GetAgentByAgentId(agentId);
                agentDetails.Agent_IsDeleted = true;

                agentdetail agentEntity = new agentdetail();
                AutoMapper.Mapper.Map(agentDetails, agentEntity);

                ESalesUnityContainer.Container.Resolve<IGenericRepository<agentdetail>>().Update(agentEntity);
                //Commit transaction
                transactionScope.Complete();
            }
        }
        
        /// <summary>
        /// Checks if Agent details exists in database
        /// </summary>
        /// <param name="agentId">Int32: AgentId</param>
        /// <param name="shortName">String: Agent short name</param>
        /// <returns>returns true if agent exists, false otherwise</returns>
        public bool IsAgentDetailsExists(int agentId, string shortName)
        {           
            AgentDTO agentDetails = new AgentDTO();
            bool result = false;

            //If new agent details
            if (agentId == 0)
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<agentdetail>>()
                    .GetSingle(item => item.Agent_ShortName == shortName && item.Agent_IsDeleted == false), agentDetails);
            }
            //If agent details already exist in database
            else if (agentId > 0)
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<agentdetail>>()
                    .GetSingle(item => item.Agent_ShortName == shortName && item.Agent_IsDeleted == false
                        && item.Agent_Id != agentId), agentDetails);                
            }

            if (agentDetails.Agent_Id > 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Retrieves agent details by Agent Id
        /// </summary>
        /// <param name="agentId">Int32: agentId</param>
        /// <returns></returns>
        public string GetAgentShortNameByAgentId(int agentId)
        {    
            //Select agent by agent id and maps it to DTO object
         return   ESalesUnityContainer.Container.Resolve<IGenericRepository<agentdetail>>()
                 .GetSingle(item => item.Agent_Id == agentId && item.Agent_IsDeleted == false).Agent_ShortName;
        }
    }
}