#region Using directives

using System.Collections.Generic;
using TCESS.ESales.DataTransferObjects;

#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface ICautionListService
    {
        IList<CustomerDTO> GetCautionListForCustomers(bool isBlacklisted);

        IList<TruckDetailsDTO> GetCautionListForTrucks(bool isBlacklisted);
        IList<TruckDetailsDTO> GetCautionListForTrucksByCustId(int custId);
        int UpdateCautionListForTrucks(TruckDetailsDTO truckDetails);
        
        IList<AuthRepDTO> GetCautionListForAuthReps(bool isBlacklisted);
        int UpdateCautionListForAuthRep(AuthRepDTO authRepDetails);

        IList<StandaloneTrucksDTO> GetCautionListForStandaloneTrucks(bool isBlacklisted);
        int UpdateCautionListForStandaloneTrucks(StandaloneTrucksDTO standaloneTrucks);
        CustomerDTO GetCustomerDetailsByCode(string customerCode);
        CustomerDocDetailsDTO GetCustomerByDocumentId(int documentType, string documentNumber);
        
    }
}