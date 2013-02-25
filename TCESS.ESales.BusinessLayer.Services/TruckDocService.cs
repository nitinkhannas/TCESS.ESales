#region Using directives

using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.PersistenceLayer.Entity;
using TCESS.ESales.PersistenceLayer.Interfaces;

#endregion

namespace TCESS.ESales.BusinessLayer.Services
{
    public class TruckDocService : ITruckDocService
    {
        /// <summary>
        /// Get Truck Doc Details By TruckDocId
        /// </summary>
        /// <param name="truckDocId">Int32:TruckDocId</param>
        /// <returns></returns>
        public TruckDocumentsDTO GetTruckDocDetailsByTruckDocId(int truckDocId)
        {
            TruckDocumentsDTO truckDocument = new TruckDocumentsDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<truckdocument>>()
            .GetSingle(item => item.TruckDoc_Doc_Id == truckDocId && item.TruckDoc_IsDeleted == false), truckDocument);

            return truckDocument;
        }

        /// <summary>
        /// Get Truck Doc Details By TruckId
        /// </summary>
        /// <param name="truckId">Int32:truckId</param>
        /// <returns></returns>
        public IList<TruckDocDetailsDTO> GetTruckDocDetailsByTruckId(int truckId)
        {
            List<TruckDocDetailsDTO> lstTruckDocDetails = new List<TruckDocDetailsDTO>();
            List<truckdocdetail> lstTruckDocDetailsEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<truckdocdetail>>().GetQuery()
                .Where(item => item.Truck_Doc_TruckId == truckId).ToList();

            AutoMapper.Mapper.Map(lstTruckDocDetailsEntity, lstTruckDocDetails);

            //return the value
            return lstTruckDocDetails;
        }

        /// <summary>
        /// Get Truck Doc Details By TruckId And DocId
        /// </summary>
        /// <param name="truckId">Int32:truckId</param>
        /// <param name="documentId">Int32:documentId</param>
        /// <returns></returns>
        public TruckDocDetailsDTO GetTruckDocDetailsByTruckIdAndDocId(int truckId, int documentId)
        {
            TruckDocDetailsDTO objTruckDocDetails = new TruckDocDetailsDTO();
            truckdocdetail TruckDocDetailsEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<truckdocdetail>>()
                .GetSingle(item => item.Truck_Doc_TruckId == truckId && item.Truck_Doc_DocId == documentId && item.Truck_Doc_IsDeleted == false);

            AutoMapper.Mapper.Map(TruckDocDetailsEntity, objTruckDocDetails);

            //return the value
            return objTruckDocDetails;
        }

        /// <summary>
        /// Verify Truck Document No Exists by truckDocId,docId and docNo
        /// </summary>
        /// <param name="truckDocId">Int32:truckDocId</param>
        /// <param name="docId">Int32:docId</param>
        /// <param name="docNo">string:docNo</param>
        /// <returns></returns>
        public bool TruckDocumentNoExists(int truckDocId, int docId, string docNo)
        {
            TruckDocDetailsDTO objTruckDocDetailsDTO = new TruckDocDetailsDTO();
            if (truckDocId > 0)
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<truckdocdetail>>().GetSingle
                    (item => item.Truck_Doc_Id != truckDocId && item.Truck_Doc_DocId == docId && item.Truck_Doc_DocNo == docNo &&
                        item.Truck_Doc_IsDeleted == false), objTruckDocDetailsDTO);
            }
            else
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<truckdocdetail>>().GetSingle
                   (item => item.Truck_Doc_DocId == docId && item.Truck_Doc_DocNo == docNo &&
                       item.Truck_Doc_IsDeleted == false), objTruckDocDetailsDTO);
            }
            return objTruckDocDetailsDTO.Truck_Doc_Id > 0 ? true : false;
        }
    }
}