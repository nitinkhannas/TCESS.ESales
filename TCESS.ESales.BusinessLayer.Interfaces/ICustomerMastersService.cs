#region Using directives

using System.Collections.Generic;
using TCESS.ESales.DataTransferObjects;

#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    /// <summary>
    /// Interface to create and get all the masters releated to customer.
    /// </summary>
    public interface ICustomerMastersService
    {
        IList<AllotedQuantityDTO> GetAllotedQuantityList();

        int SaveAndUpdateTruckregType(TruckRegTypeDTO truckregTypeDetails);
        IList<TruckRegTypeDTO> GetTruckRegTypeList();
        void DeleteTruckregType(int truckregTypeId);
        TruckRegTypeDTO GettruckregTypeListByTypeId(int truckregTypeId);
        bool IstruckregTypeExists(int truckregTypeId, string truckregType);
        
        IList<LiftingIntervalDTO> GetLiftingIntervalList();
    }
}