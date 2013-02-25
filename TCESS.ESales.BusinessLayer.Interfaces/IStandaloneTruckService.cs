#region Using directives

using System;
using System.Collections.Generic;
using TCESS.ESales.DataTransferObjects;

#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface IStandaloneTruckService
    {
        IList<StandaloneTrucksDTO> GetStandaloneTruckDetails();
        StandaloneTrucksDTO GetStandaloneTruckByTruckId(int truckId);
        void DeleteStandaloneTruck(Int32 truckId);
        int SaveAndUpdateStandaloneTrucks(StandaloneTrucksDTO truckDetails);
        void SaveAndUpdateStandaloneTruckDocumentDetails(IList<StandaloneTruckDocDetails> lstTruckDetails);
        StandaloneTrucksDTO GetStandaloneTruckByRegNo(string regNo);        
        IList<StandaloneTruckDocDetails> GetStandaloneTruckDocDetailsByTruckId(int truckId);
        bool StandaloneTruckExists(int truckId, string regNo);
        int StandaloneTruckCount();
        int TotalStandaloneTruckCount();
        StandaloneTruckDocDetails GetStandaloneTruckDocDetailsByTruckIdAndDocId(int truckId, int documentId);
    }
}