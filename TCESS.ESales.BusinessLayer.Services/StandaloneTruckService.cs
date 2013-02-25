#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.PersistenceLayer.Entity;
using TCESS.ESales.PersistenceLayer.Interfaces;

#endregion

namespace TCESS.ESales.BusinessLayer.Services
{
    public class StandaloneTruckService : IStandaloneTruckService
    {
        /// <summary>
        /// Get Standalone Truck Details
        /// </summary>
        /// <returns></returns>
        public IList<StandaloneTrucksDTO> GetStandaloneTruckDetails()
        {
            List<StandaloneTrucksDTO> lstStandaloneTruck = new List<StandaloneTrucksDTO>();
            List<standalonetruck> lstStandaloneTruckEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<standalonetruck>>().GetQuery()
                .Where(item => item.StandaloneTruck_IsDeleted == false).ToList();

            AutoMapper.Mapper.Map(lstStandaloneTruckEntity, lstStandaloneTruck);

            //return the value
            return lstStandaloneTruck;
        }

        /// <summary>
        /// Get Standalone Truck By TruckId
        /// </summary>
        /// <param name="truckId">Int32:truckId</param>
        /// <returns></returns>
        public StandaloneTrucksDTO GetStandaloneTruckByTruckId(int truckId)
        {
            StandaloneTrucksDTO truckDetails = new StandaloneTrucksDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<standalonetruck>>()
            .GetSingle(item => item.StandaloneTruck_Id == truckId), truckDetails);

            //return the value
            return truckDetails;
        }

        /// <summary>
        /// Delete Standalone TruckDoc Details
        /// </summary>
        /// <param name="standaloneTruckDocs"></param>
        private static void DeleteStandaloneTruckDocDetails(StandaloneTruckDocDetails standaloneTruckDocs)
        {
            standalonetruckdocdetail standaloneTruckDocEntity = new standalonetruckdocdetail();
            AutoMapper.Mapper.Map(standaloneTruckDocs, standaloneTruckDocEntity);

            ESalesUnityContainer.Container.Resolve<IGenericRepository<standalonetruckdocdetail>>()
                .Update(standaloneTruckDocEntity);
        }

        /// <summary>
        /// Delete Standalone Truck by truckId
        /// </summary>
        /// <param name="truckId">Int32:truckId</param>
        public void DeleteStandaloneTruck(Int32 truckId)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                IList<StandaloneTruckDocDetails> lstStandaloneTruckDoc = GetStandaloneTruckDocDetailsByTruckId(truckId);
                (from truckDocDetail in lstStandaloneTruckDoc select truckDocDetail).Update(
                    truckDocDetail => truckDocDetail.StandaloneTruck_Doc_IsDeleted = true);

                foreach (var standaloneTruckDocs in lstStandaloneTruckDoc)
                {
                    DeleteStandaloneTruckDocDetails(standaloneTruckDocs);
                }

                StandaloneTrucksDTO standaloneTruckDetails = GetStandaloneTruckByTruckId(truckId);
                standaloneTruckDetails.StandaloneTruck_IsDeleted = true;

                standalonetruck standaloneTruckEntity = new standalonetruck();
                AutoMapper.Mapper.Map(standaloneTruckDetails, standaloneTruckEntity);

                ESalesUnityContainer.Container.Resolve<IGenericRepository<standalonetruck>>().Update(standaloneTruckEntity);

                transactionScope.Complete();
            }
        }

        /// <summary>
        /// Get Standalone Truck Doc Details By TruckId
        /// </summary>
        /// <param name="truckId">Int32:truckId</param>
        /// <returns></returns>
        public IList<StandaloneTruckDocDetails> GetStandaloneTruckDocDetailsByTruckId(int truckId)
        {
            List<StandaloneTruckDocDetails> lstStandaloneTruckDoc = new List<StandaloneTruckDocDetails>();
            List<standalonetruckdocdetail> lstStandaloneTruckDocEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<standalonetruckdocdetail>>().GetQuery()
                .Where(item => item.StandaloneTruck_Doc_TruckId == truckId).ToList();

            AutoMapper.Mapper.Map(lstStandaloneTruckDocEntity, lstStandaloneTruckDoc);

            //return the value
            return lstStandaloneTruckDoc;
        }

        /// <summary>
        /// Save And Update Standalone Trucks
        /// </summary>
        /// <param name="truckDetails"></param>
        /// <returns></returns>
        public int SaveAndUpdateStandaloneTrucks(StandaloneTrucksDTO truckDetails)
        {
            standalonetruck standaloneTruckEntity = new standalonetruck();
            AutoMapper.Mapper.Map(truckDetails, standaloneTruckEntity);

            if (truckDetails.StandaloneTruck_Id == 0)
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<standalonetruck>>().Save(standaloneTruckEntity);
            }
            else
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<standalonetruck>>().Update(standaloneTruckEntity);
            }

            //return the value
            return standaloneTruckEntity.StandaloneTruck_Id;
        }

        /// <summary>
        /// Get Standalone Truck Doc Details By TruckId And DocId
        /// </summary>
        /// <param name="truckId">Int32:truckId</param>
        /// <param name="documentId">Int32:documentId</param>
        /// <returns></returns>
        public StandaloneTruckDocDetails GetStandaloneTruckDocDetailsByTruckIdAndDocId(int truckId, int documentId)
        {
            StandaloneTruckDocDetails objTruckDocDetails = new StandaloneTruckDocDetails();
            standalonetruckdocdetail TruckDocDetailsEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<standalonetruckdocdetail>>()
                .GetSingle(item => item.StandaloneTruck_Doc_TruckId == truckId && item.StandaloneTruck_Doc_DocId == documentId && item.StandaloneTruck_Doc_IsDeleted == false);

            AutoMapper.Mapper.Map(TruckDocDetailsEntity, objTruckDocDetails);

            //return the value
            return objTruckDocDetails;
        }

        /// <summary>
        /// Save And Update Standalone Truck Document Details
        /// </summary>
        /// <param name="lstTruckDetails"></param>
        public void SaveAndUpdateStandaloneTruckDocumentDetails(IList<StandaloneTruckDocDetails> lstTruckDetails)
        {
            foreach (StandaloneTruckDocDetails item in lstTruckDetails)
            {
                standalonetruckdocdetail truckDocDetailsEntity = new standalonetruckdocdetail();
                StandaloneTruckDocDetails truckDocDetail = GetStandaloneTruckDocDetailsByTruckIdAndDocId(item.StandaloneTruck_Doc_TruckId,
                    item.StandaloneTruck_Doc_DocId);

                if (truckDocDetail.StandaloneTruck_Doc_Id > 0)
                {
                    AutoMapper.Mapper.Map(truckDocDetail, truckDocDetailsEntity);

                    truckDocDetailsEntity.StandaloneTruck_Doc_DocNo = item.StandaloneTruck_Doc_DocNo;
                    truckDocDetailsEntity.StandaloneTruck_Doc_ExDate = item.StandaloneTruck_Doc_ExDate;
                    if (item.StandaloneTruck_Doc_File == null)
                    {
                        truckDocDetailsEntity.StandaloneTruck_Doc_IsDeleted = true;
                    }
                    else
                    {
                        truckDocDetailsEntity.StandaloneTruck_Doc_File = item.StandaloneTruck_Doc_File;
                        truckDocDetailsEntity.StandaloneTruck_Doc_IsDeleted = false;
                    }
                    truckDocDetailsEntity.StandaloneTruck_Doc_FileName = item.StandaloneTruck_Doc_FileName;
                    truckDocDetailsEntity.StandaloneTruck_Doc_CreatedBy = item.StandaloneTruck_Doc_CreatedBy;
                    truckDocDetailsEntity.StandaloneTruck_Doc_CreatedDate = item.StandaloneTruck_Doc_CreatedDate;
                    truckDocDetailsEntity.StandaloneTruck_Doc_LastUpdatedDate = item.StandaloneTruck_Doc_LastUpdatedDate;

                    ESalesUnityContainer.Container.Resolve<IGenericRepository<standalonetruckdocdetail>>()
                        .Update(truckDocDetailsEntity);
                }
                else
                {
                    AutoMapper.Mapper.Map(item, truckDocDetailsEntity);
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<standalonetruckdocdetail>>()
                        .Save(truckDocDetailsEntity);
                }
            }
        }

        /// <summary>
        /// Get Standalone Truck By RegNo
        /// </summary>
        /// <param name="regNo">String:regNo</param>
        /// <returns></returns>
        public StandaloneTrucksDTO GetStandaloneTruckByRegNo(string regNo)
        {
            StandaloneTrucksDTO truckDetails = new StandaloneTrucksDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<standalonetruck>>()
            .GetSingle(item => item.StandaloneTruck_RegNo == regNo), truckDetails);
            return truckDetails;
        }

        public bool StandaloneTruckExists(int truckId, string regNo)
        {
            StandaloneTrucksDTO truckDetails = new StandaloneTrucksDTO();
            bool result = false;

            if (truckId == 0)
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<standalonetruck>>()
                    .GetSingle(item => item.StandaloneTruck_RegNo == regNo ), truckDetails);
            }
            else
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<standalonetruck>>()
                    .GetSingle(item => item.StandaloneTruck_RegNo == regNo && item.StandaloneTruck_IsDeleted == false
                        && item.StandaloneTruck_Id != truckId), truckDetails);
            }

            if (truckDetails.StandaloneTruck_Id > 0)
            {
                result = true;
            }
            return result;
        }

        public int StandaloneTruckCount()
        {
            int count = 0;
            DateTime todaydate = DateTime.Now.Date;
            count = ESalesUnityContainer.Container
            .Resolve<IGenericRepository<standalonetruck>>().GetQuery()
            .Where(item => item.StandaloneTruck_IsDeleted == false && item.StandaloneTruck_CreatedDate >= todaydate).ToList().Count();
            return count;
        }

        public int TotalStandaloneTruckCount()
        {
            int count = 0;
            count = ESalesUnityContainer.Container
            .Resolve<IGenericRepository<standalonetruck>>().GetQuery().ToList().Count();
            return count;
        }
    }
}