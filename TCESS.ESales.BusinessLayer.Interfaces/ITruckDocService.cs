using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCESS.ESales.DataTransferObjects;

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface ITruckDocService
    {
        IList<TruckDocDetailsDTO> GetTruckDocDetailsByTruckId(int truckId);
        bool TruckDocumentNoExists(int truckDocId, int docId, string docNo);
        TruckDocDetailsDTO GetTruckDocDetailsByTruckIdAndDocId(int truckId, int documentId);
        TruckDocumentsDTO GetTruckDocDetailsByTruckDocId(int truckDocId);
    }
}