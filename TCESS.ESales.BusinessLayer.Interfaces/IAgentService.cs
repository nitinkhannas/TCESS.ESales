#region Using directives

using System.Collections.Generic;
using System.Data;
using TCESS.ESales.DataTransferObjects;

#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface IAgentService
    {
        IList<AgentDTO> GetAgentList();
        int SaveAndUpdateAgent(AgentDTO agentDetails);
        AgentDTO GetAgentByAgentId(int agentId);
        void DeleteAgent(int agentId);
        bool IsAgentDetailsExists(int agentId, string shortName);
        string GetAgentShortNameByAgentId(int agentId);
    }
}