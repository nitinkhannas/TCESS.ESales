#region Namespace

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
    public class MaterialTypeService : IMaterialTypeService
    {
        /// <summary>
        /// Get the list of all active Material Type
        /// </summary>
        /// <returns>List of Material Type</returns>
        public IList<MaterialTypeDTO> GetMaterialTypeList(bool isActiveOnly)
        {
            List<MaterialTypeDTO> lstMaterialTypes = new List<MaterialTypeDTO>();
            List<materialtype> lstMaterialTypesEntity = new List<materialtype>();

            if (isActiveOnly)
            {
                lstMaterialTypesEntity = ESalesUnityContainer.Container
                   .Resolve<IGenericRepository<materialtype>>().GetQuery().Where(item => item.MaterialType_IsDeleted == false && item.MaterialType_IsActive == isActiveOnly)
                   .OrderBy(order => order.MaterialType_Name).ToList();
            }
            else
            {
                lstMaterialTypesEntity = ESalesUnityContainer.Container
                    .Resolve<IGenericRepository<materialtype>>().GetQuery().Where(item => item.MaterialType_IsDeleted == false)
                    .OrderBy(order => order.MaterialType_Name).ToList();
            }

            AutoMapper.Mapper.Map(lstMaterialTypesEntity, lstMaterialTypes);
            return lstMaterialTypes;
        }

        /// <summary>
        /// Get Material Type By Id
        /// </summary>
        /// <param name="materialTypeId">Int32:materialTypeId</param>
        /// <returns></returns>
        public MaterialTypeDTO GetMaterialTypeById(int materialTypeId)
        {
            MaterialTypeDTO materialTypeDetails = new MaterialTypeDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<materialtype>>()
                .GetSingle(item => item.MaterialType_Id == materialTypeId), materialTypeDetails);
            return materialTypeDetails;
        }

        /// <summary>
        /// Save Material Type
        /// </summary>
        /// <param name="materialTypeDetails"></param>
        /// <returns></returns>
        public int SaveMaterialType(MaterialTypeDTO materialTypeDetails)
        {
            materialtype materialtypeEntity = new materialtype();
            AutoMapper.Mapper.Map(materialTypeDetails, materialtypeEntity);
            ESalesUnityContainer.Container.Resolve<IGenericRepository<materialtype>>().Save(materialtypeEntity);

            IList<AgentDTO> listAgentDTO = ESalesUnityContainer.Container.Resolve<IAgentService>().GetAgentList();
            AgentMaterialPercentageDTO objAgentMaterialPercentageDTO = new AgentMaterialPercentageDTO();

            foreach (AgentDTO agent in listAgentDTO)
            {

                objAgentMaterialPercentageDTO.AMP_Agent_Id = agent.Agent_Id;
                objAgentMaterialPercentageDTO.AMP_MaterialType_Id = materialtypeEntity.MaterialType_Id;
                objAgentMaterialPercentageDTO.AMP_IsActive = true;
                objAgentMaterialPercentageDTO.AMP_CreatedBy = materialtypeEntity.MaterialType_CreatedBy;
                ESalesUnityContainer.Container.Resolve<IAgentMaterialPercentageService>()
                    .SaveAgentMaterialPercentage(objAgentMaterialPercentageDTO);
            }
            return materialtypeEntity.MaterialType_Id;
        }

        /// <summary>
        /// Update Material Type
        /// </summary>
        /// <param name="materialTypeDetails"></param>
        /// <returns></returns>
        public int UpdateMaterialType(MaterialTypeDTO materialTypeDetails)
        {
            materialtype materialtypeEntity = new materialtype();
            materialtype_history materialtypehistoryEntity = new materialtype_history();
            int materialid;
            using (TransactionScope transactionScope = new TransactionScope())
            {
                AutoMapper.Mapper.Map(materialTypeDetails, materialtypeEntity);
                ESalesUnityContainer.Container.Resolve<IGenericRepository<materialtype>>().Update(materialtypeEntity);
                materialid = materialTypeDetails.MaterialType_Id;

                materialTypeDetails.MaterialType_Id = 0;
                AutoMapper.Mapper.Map(materialTypeDetails, materialtypehistoryEntity);
                ESalesUnityContainer.Container.Resolve<IGenericRepository<materialtype_history>>().Save(materialtypehistoryEntity);
                transactionScope.Complete();
            }
            return materialid;
        }

        /// <summary>
        /// Delete Material Type by materialTypeId
        /// </summary>
        /// <param name="materialTypeId">Int32:materialTypeId</param>
        public void DeleteMaterialType(int materialTypeId)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                MaterialTypeDTO materialTypeDetails = new MaterialTypeDTO();
                materialTypeDetails = ESalesUnityContainer.Container.Resolve<IMaterialTypeService>()
                    .GetMaterialTypeById(materialTypeId);
                materialTypeDetails.MaterialType_IsDeleted = true;
                UpdateMaterialType(materialTypeDetails);

                AgentMaterialPercentageService agentMatPercentageService = new AgentMaterialPercentageService();
                IList<AgentMaterialPercentageDTO> lstAgentMaterialPercentage = agentMatPercentageService
                    .GetAgentMaterialPercentByMaterialTypeId(materialTypeId);

                foreach (var percent in lstAgentMaterialPercentage)
                {
                    AgentMaterialPercentageDTO agentMaterialPercentage = agentMatPercentageService
                        .GetAgentMaterialPercentageByAMPId(percent.AMP_Id);
                    agentMaterialPercentage.AMP_IsDeleted = true;
                    ESalesUnityContainer.Container.Resolve<IAgentMaterialPercentageService>()
                        .UpdateAgentPercentage(agentMaterialPercentage);
                }
                transactionScope.Complete();
            }
        }

        /// <summary>
        /// Get the last two updated records
        /// </summary>
        /// <returns>List of Material History</returns>
        public IList<MaterialTypeDTO> GetMaterialTypeHistoryList()
        {
            List<MaterialTypeDTO> lstMaterialTypesHistory = new List<MaterialTypeDTO>();
            List<materialtype_history> lstMaterialTypesHistoryEntity = new List<materialtype_history>();

            lstMaterialTypesHistoryEntity = ESalesUnityContainer.Container
               .Resolve<IGenericRepository<materialtype_history>>().GetQuery()
               .OrderByDescending(order => order.MaterialType_LastUpdatedDate).Take(2).ToList();

            AutoMapper.Mapper.Map(lstMaterialTypesHistoryEntity, lstMaterialTypesHistory);
            return lstMaterialTypesHistory;
        }
    }
}