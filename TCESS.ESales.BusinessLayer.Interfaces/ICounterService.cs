#region Using directives

using System.Collections.Generic;
using TCESS.ESales.DataTransferObjects;

#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface ICounterService
    {
        IList<CounterDTO> GetCounterDetails();
        IList<CounterDTO> GetCounters(int counterId);
        CounterDTO GetCounterDetailsByMacId(string macAddress, int userId, int counterId);
        CounterDTO GetCounterDetailsById(int counterId);
        int GetCounterDetailsByUserId(int userId);
        IList<CounterDetailsDTO> GetCounterDetailsListForCurrentDate();
        IList<CounterDTO> GetCounterList();

        void SaveAndUpdateCounters(CounterDTO counterDetails);
        void SaveCounterDailyDetails(CounterDetailsDTO counterDailyDetail, IList<DcaMaterialAllocationDTO> lstDCAMaterialAllocation);
        IList<CounterDetailsDTO> GetCounterDailyDetails(int agentId);

    }
}