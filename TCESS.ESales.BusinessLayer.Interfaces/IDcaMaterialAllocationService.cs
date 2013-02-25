#region Using directives

using System;
using System.Collections.Generic;
using TCESS.ESales.DataTransferObjects;

#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface IDcaMaterialAllocationService
    {
        IList<DcaMaterialAllocationDTO> GetMaterialAllocationDetails(int materialType, DateTime transDate);
        IList<DcaMaterialAllocationDTO> GetMaterialAgentAllocationDetails(int agentId, DateTime transDate);
		IList<DcaMaterialAllocationDTO> GetAllMaterialAllocationDetails(int materialType, DateTime transDate);
        void SaveAndUpdateDCAMaterialDetails(IList<DcaMaterialAllocationDTO> ListDCAMaterialAllocation);
        List<int> GetAllMaterialAllocationActiveAgentID(int materialType, DateTime transDate);
        IList<int> GetAllMaterialAllocationAgentIDList(DateTime transDate);
    }
}