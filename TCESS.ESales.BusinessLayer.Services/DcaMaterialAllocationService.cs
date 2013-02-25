#region Using directives

using System;
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
    public class DcaMaterialAllocationService : BaseService, IDcaMaterialAllocationService
    {
        /// <summary>
        /// Get Material Allocation Details by materialType and transDate
        /// </summary>
        /// <param name="materialType">Int32:materialType</param>
        /// <param name="transDate">Datetime:transDate</param>
        /// <returns></returns>
        public IList<DcaMaterialAllocationDTO> GetMaterialAllocationDetails(int materialType, DateTime transDate)
        {
            List<DcaMaterialAllocationDTO> lstMaterialAllocationDetails = new List<DcaMaterialAllocationDTO>();

            List<dcamaterialallocation> objListMaterialAllocationDetail = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<dcamaterialallocation>>().GetQuery()
                .Where(item => item.DCAMA_MaterialType_Id == materialType && item.DCAMA_Date == transDate
                    && item.DCAMA_TodayPercentage > 0 && item.DCAMA_IsAgentActive == true).ToList();

            AutoMapper.Mapper.Map(objListMaterialAllocationDetail, lstMaterialAllocationDetails);

            //return the value
            return lstMaterialAllocationDetails;
        }

        /// <summary>
        /// Save And Update DCA Material Details
        /// </summary>
        /// <param name="ListDCAMaterialAllocation"></param>
        public void SaveAndUpdateDCAMaterialDetails(IList<DcaMaterialAllocationDTO> ListDCAMaterialAllocation)
        {
            foreach (DcaMaterialAllocationDTO item in ListDCAMaterialAllocation)
            {
                dcamaterialallocation materialAllocationEntity = new dcamaterialallocation();
                AutoMapper.Mapper.Map(item, materialAllocationEntity);

                if (item.DCAMA_ID == 0)
                {
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<dcamaterialallocation>>().Save(materialAllocationEntity);
                }
                else
                {
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<dcamaterialallocation>>().Update(materialAllocationEntity);
                }
            }
        }

        /// <summary>
        /// Get Material Agent Allocation Details by agentId and transDate
        /// </summary>
        /// <param name="agent">Int32:agent</param>
        /// <param name="transDate">DateTime:transDate</param>
        /// <returns></returns>
        public IList<DcaMaterialAllocationDTO> GetMaterialAgentAllocationDetails(int agentId, DateTime transDate)
        {
            List<DcaMaterialAllocationDTO> lstMaterialAllocationDetails = new List<DcaMaterialAllocationDTO>();

            List<dcamaterialallocation> objListMaterialAllocationDetail = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<dcamaterialallocation>>().GetQuery()
                .Where(item => item.DCAMA_Agent_Id == agentId && item.DCAMA_Date == transDate).ToList();

            AutoMapper.Mapper.Map(objListMaterialAllocationDetail, lstMaterialAllocationDetails);
            return lstMaterialAllocationDetails;
        }

        public IList<DcaMaterialAllocationDTO> GetAllMaterialAllocationDetails(int materialType, DateTime transDate)
        {
            List<DcaMaterialAllocationDTO> lstMaterialAllocationDetails = new List<DcaMaterialAllocationDTO>();

            if (materialType != 0)
            {
                List<dcamaterialallocation> objListMaterialAllocationDetail = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<dcamaterialallocation>>().GetQuery()
                .Where(item => item.DCAMA_MaterialType_Id == materialType && item.DCAMA_Date == transDate).ToList();
                AutoMapper.Mapper.Map(objListMaterialAllocationDetail, lstMaterialAllocationDetails);
            }
            else
            {
                List<dcamaterialallocation> objListMaterialAllocationDetail = ESalesUnityContainer.Container
               .Resolve<IGenericRepository<dcamaterialallocation>>().GetQuery()
               .Where(item => item.DCAMA_Date == transDate).ToList();
                AutoMapper.Mapper.Map(objListMaterialAllocationDetail, lstMaterialAllocationDetails);
            }
            //return the value
            return lstMaterialAllocationDetails;
        }

        public List<int> GetAllMaterialAllocationActiveAgentID(int materialType, DateTime transDate)
        {
            List<DcaMaterialAllocationDTO> lstMaterialAllocationDetails = new List<DcaMaterialAllocationDTO>();

            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<dcamaterialallocation>>()
                .GetQuery().ToList(), lstMaterialAllocationDetails);

            List<int> lstAgentId = lstMaterialAllocationDetails.Where(item => item.DCAMA_Date == transDate && item.DCAMA_IsAgentActive == true).Select(item => item.DCAMA_Agent_Id).ToList();

            return lstAgentId;
        }

        public IList<int> GetAllMaterialAllocationAgentIDList(DateTime transDate)
        {
            List<DcaMaterialAllocationDTO> lstMaterialAllocationDetails = new List<DcaMaterialAllocationDTO>();

            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<dcamaterialallocation>>()
                .GetQuery().ToList(), lstMaterialAllocationDetails);

            List<int> lstAgentId = lstMaterialAllocationDetails
               .Where(item => item.DCAMA_Date == transDate && item.DCAMA_TodayPercentage > 0)
               .GroupBy(k => k.DCAMA_Agent_Id).Select(g => g.First().DCAMA_Agent_Id).ToList<int>();

            return lstAgentId;
        }
    }
}