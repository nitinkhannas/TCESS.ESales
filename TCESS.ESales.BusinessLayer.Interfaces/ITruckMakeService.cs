using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCESS.ESales.DataTransferObjects;

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface ITruckMakeService
    {
        int SaveAndUpdateTruckMake(TruckMakeDTO truckMakeDetail);
        IList<TruckMakeDTO> GetTruckMakelist();
        TruckMakeDTO GetTruckMakeById(int truckMakeId);
        bool TruckMakeExists(int truckMakeId, string truckMakeName);
    }
}