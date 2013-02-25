#region Using directives

using System.Collections.Generic;
using TCESS.ESales.DataTransferObjects;

#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface ITruckService
    {
        int SaveAndUpdateTruckDetailsForCustomer(TruckDetailsDTO truckDetails);
        void SaveAndUpdateTruckDocumentDetails(IList<TruckDocDetailsDTO> lstTruckDetails,
            IList<TruckDocumentsDTO> listTruckDocument);
        void DeleteTruck(TruckDetailsDTO truckDetails);
        TruckDetailsDTO GetTruckDetailsByTruckId(int truckId);
        TruckDetailsDTO GetTruckDetailsByTruckRegistrationId(string truckRegistrationId);
        IList<TruckDetailsDTO> GetTruckDetailsForCustomer(int custId);
		TruckVerificationDTO GetAllTruckDetails(string truckNumber);
        IList<TruckCarryCapacityDTO> GetTruckCarryCapacity();
        IList<TruckWheelerDTO> GetTruckWheels();
        IList<TruckVerificationDTO> GetAllInactiveTruckDetails();
        void ActivateInactiveTruck(int truckType, string truckNumber);
        IList<TruckVerificationDTO> GetAllSuspendedTrucks();
    }
}