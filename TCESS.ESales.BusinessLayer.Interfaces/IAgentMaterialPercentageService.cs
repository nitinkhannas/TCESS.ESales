#region Using directives

using System.Collections.Generic;
using System.Data;
using TCESS.ESales.DataTransferObjects;

#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface IAgentMaterialPercentageService
    {
        int UpdateAgentPercentage(AgentMaterialPercentageDTO agentMaterialPercentageDetails);
        void UpdateAgentpercentage(List<AgentMaterialPercentageDTO> lstAgentMaterialPercentage);
        void SaveAgentMaterialPercentage(AgentMaterialPercentageDTO agentMaterialPercentageDetails);
        AgentMaterialPercentageDTO GetAgentMaterialPercentageByAMPId(int ampId);
         
        IList<AgentMaterialPercentageDTO> GetAgentMaterialPercentByMaterialTypeId(int materialTypeId);
        IList<AgentMaterialPercentageDTO> GetAgentMaterialPercentByAgentId(int agentId);
    }
}