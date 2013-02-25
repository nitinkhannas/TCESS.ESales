#region Namespace

using System.Collections.Generic;
using TCESS.ESales.DataTransferObjects;

#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface ILocationService
	{
		int SaveState(StateDTO stateDetail);
        int UpdateState(StateDTO stateDetail);
		IList<StateDTO> GetStateList();
        StateDTO GetStateByStateId(int stateId);
        bool StateExists(int stateId, string state);

        int SaveAndUpdateDistrict(DistrictDTO districtDetails);
        DistrictDTO GetDistrictByDistId(int districtId);
		IList<DistrictDTO> GetDistrictList();
		IList<DistrictDTO> GetDistrictListByStateId(int stateId);
        bool DistrictExists(int stateId, int districtId, string districtName);
	}
}