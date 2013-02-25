using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCESS.ESales.DataTransferObjects;

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface ILiftingLimit
    {
        void SaveLiftingLimit(LiftingLimitDTO liftingLimitDetails);
        IList<LiftingLimitDTO> GetLiftingLimitHistoryList();
        IList<LiftingLimitDTO> GetLimitList();
        LiftingLimitDTO GetLiftingLimitById(int LiftingLimit_ID);
        int UpdateLiftingLimit(LiftingLimitDTO LiftingLimitDetails);
        void DeleteLiftingLimit(int LiftingLimit_ID);
        List<AllotedQuantityDTO> GetAllottedQuantityDetails();
        int InsertAllottedQuantity(AllotedQuantityDTO allottedQty);
        void UpdateLiftingLimitTruckRegId(int truckRegTypeId);
    }
}
