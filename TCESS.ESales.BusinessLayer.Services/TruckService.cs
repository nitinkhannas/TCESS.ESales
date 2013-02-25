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
    public class TruckService : ITruckService
    {
        /// <summary>
        /// Save And Update Truck Details For Customer
        /// </summary>
        /// <param name="truckDetails"></param>
        public int SaveAndUpdateTruckDetailsForCustomer(TruckDetailsDTO truckDetails)
        {
            truckdetail truckDetailsEntity = new truckdetail();
            AutoMapper.Mapper.Map(truckDetails, truckDetailsEntity);

            if (truckDetails.Truck_Id == 0)
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<truckdetail>>().Save(truckDetailsEntity);
            }
            else
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<truckdetail>>().Update(truckDetailsEntity);
            }

            //return the value
            return truckDetailsEntity.Truck_Id;
        }

        /// <summary>
        /// Save And Update Truck Document Details
        /// </summary>
        /// <param name="listTruckDetails"></param>
        public void SaveAndUpdateTruckDocumentDetails(IList<TruckDocDetailsDTO> listTruckDetail,
            IList<TruckDocumentsDTO> listTruckDocument)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                for (int i = 0; i < listTruckDetail.Count; i++)
                {
                    truckdocdetail truckDocDetailsEntity = new truckdocdetail();

                    TruckDocService truckDocuments = new TruckDocService();
                    TruckDocDetailsDTO truckDocDetail = truckDocuments.GetTruckDocDetailsByTruckIdAndDocId(
                        listTruckDetail[i].Truck_Doc_TruckId, listTruckDetail[i].Truck_Doc_DocId);

                    if (truckDocDetail.Truck_Doc_Id > 0)
                    {
                        AutoMapper.Mapper.Map(truckDocDetail, truckDocDetailsEntity);

                        truckDocDetailsEntity.Truck_Doc_DocNo = listTruckDetail[i].Truck_Doc_DocNo;
                        truckDocDetailsEntity.Truck_Doc_ExDate = listTruckDetail[i].Truck_Doc_ExDate;
                        truckDocDetailsEntity.Truck_Doc_FileName = listTruckDetail[i].Truck_Doc_FileName;
                        truckDocDetailsEntity.Truck_Doc_CreatedBy = listTruckDetail[i].Truck_Doc_CreatedBy;
                        truckDocDetailsEntity.Truck_Doc_CreatedDate = listTruckDetail[i].Truck_Doc_CreatedDate;
                        truckDocDetailsEntity.Truck_Doc_LastUpdatedDate = listTruckDetail[i].Truck_Doc_LastUpdatedDate;
                        ESalesUnityContainer.Container.Resolve<IGenericRepository<truckdocdetail>>().Update(truckDocDetailsEntity);
                    }
                    else
                    {
                        AutoMapper.Mapper.Map(listTruckDetail[i], truckDocDetailsEntity);
                        ESalesUnityContainer.Container.Resolve<IGenericRepository<truckdocdetail>>().Save(truckDocDetailsEntity);
                    }

                    TruckDocumentsDTO truckDocument = truckDocuments.GetTruckDocDetailsByTruckDocId(truckDocDetailsEntity.Truck_Doc_Id);
                    truckdocument truckDocumentEntity = new truckdocument();

                    if (truckDocument.TruckDoc_Id > 0)
                    {
                        AutoMapper.Mapper.Map(truckDocument, truckDocumentEntity);
                        if (listTruckDocument[i].TruckDoc_File == null)
                        {
                            truckDocumentEntity.TruckDoc_IsDeleted = true;
                        }
                        else
                        {
                            truckDocumentEntity.TruckDoc_File = listTruckDocument[i].TruckDoc_File;
                            truckDocumentEntity.TruckDoc_IsDeleted = false;
                        }
                        ESalesUnityContainer.Container.Resolve<IGenericRepository<truckdocument>>().Update(truckDocumentEntity);
                    }
                    else
                    {
                        AutoMapper.Mapper.Map(listTruckDocument[i], truckDocumentEntity);

                        truckDocumentEntity.TruckDoc_Doc_Id = truckDocDetailsEntity.Truck_Doc_Id;

                        ESalesUnityContainer.Container.Resolve<IGenericRepository<truckdocument>>().Save(truckDocumentEntity);
                    }
                }
                transactionScope.Complete();
            }
        }

        /// <summary>
        /// Get Truck Details For Customer by custId
        /// </summary>
        /// <param name="custId">Int32:custId</param>
        /// <returns></returns>
        public IList<TruckDetailsDTO> GetTruckDetailsForCustomer(int custId)
        {
            List<TruckDetailsDTO> lstTruckDetails = new List<TruckDetailsDTO>();
            List<truckdetail> lstTruckDetailsEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<truckdetail>>()
                .GetQuery().Where(item => item.Truck_CustomerId == custId && item.Truck_IsDeleted == false)
                .OrderBy(order => order.Truck_RegNo).ToList();

            AutoMapper.Mapper.Map(lstTruckDetailsEntity, lstTruckDetails);

            //return the value
            return lstTruckDetails;
        }

        /// <summary>
        /// Get Truck Details By TruckId
        /// </summary>
        /// <param name="truckId">Int32:truckId</param>
        /// <returns></returns>
        public TruckDetailsDTO GetTruckDetailsByTruckId(int truckId)
        {
            TruckDetailsDTO truckDetails = new TruckDetailsDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<truckdetail>>()
            .GetSingle(item => item.Truck_Id == truckId), truckDetails);

            //return the value
            return truckDetails;
        }

        /// <summary>
        /// Delete Truck Doc Details
        /// </summary>
        /// <param name="truckDocs"></param>
        private static void DeleteTruckDocDetails(TruckDocDetailsDTO truckDocs)
        {
            truckdocdetail truckDocEntity = new truckdocdetail();
            AutoMapper.Mapper.Map(truckDocs, truckDocEntity);

            ESalesUnityContainer.Container.Resolve<IGenericRepository<truckdocdetail>>().Update(truckDocEntity);
        }

        /// <summary>
        /// Delete Truck
        /// </summary>
        /// <param name="truckDetails"></param>
        public void DeleteTruck(TruckDetailsDTO truckDetails)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                TruckDocService truckDocuments = new TruckDocService();
                IList<TruckDocDetailsDTO> lstTruckDocDetails = truckDocuments.GetTruckDocDetailsByTruckId(truckDetails.Truck_Id);

                (from truckDocDetail in lstTruckDocDetails select truckDocDetail).Update(
                    truckDocDetail => truckDocDetail.Truck_Doc_IsDeleted = true);

                foreach (var truckDocs in lstTruckDocDetails)
                {
                    DeleteTruckDocDetails(truckDocs);
                }

                truckdetail truckEntity = new truckdetail();
                AutoMapper.Mapper.Map(truckDetails, truckEntity);

                ESalesUnityContainer.Container.Resolve<IGenericRepository<truckdetail>>().Update(truckEntity);

                transactionScope.Complete();
            }
        }

        /// <summary>
        /// Gets list of Truck Carry Capacity
        /// </summary>
        /// <returns>returns list of Truck Carry Capacity</returns>
        public IList<TruckCarryCapacityDTO> GetTruckCarryCapacity()
        {
            List<TruckCarryCapacityDTO> lstTruckCarryCapacity = new List<TruckCarryCapacityDTO>();
            List<truckcarrycapacity> lstTruckCarryCapacityEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<truckcarrycapacity>>().GetQuery()
                .Where(item => item.TruckCC_IsDeleted == false).ToList();

            AutoMapper.Mapper.Map(lstTruckCarryCapacityEntity, lstTruckCarryCapacity);

            //return value
            return lstTruckCarryCapacity;
        }

        /// <summary>
        /// Gets list of Truck Carry Capacity
        /// </summary>
        /// <returns>returns list of Truck Carry Capacity</returns>
        public IList<TruckWheelerDTO> GetTruckWheels()
        {
            List<TruckWheelerDTO> lstTruckWheeler = new List<TruckWheelerDTO>();
            List<truckwheeler> lstTruckWheelerEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<truckwheeler>>().GetQuery()
                .Where(item => item.TruckWheeler_IsDeleted == false).ToList();

            AutoMapper.Mapper.Map(lstTruckWheelerEntity, lstTruckWheeler);

            //return value
            return lstTruckWheeler;
        }

        /// <summary>
        /// Get All Truck Details by truckNumber
        /// </summary>
        /// <param name="truckNumber">String:truckNumber</param>
        /// <returns></returns>
        public TruckVerificationDTO GetAllTruckDetails(string truckNumber)
        {
            TruckVerificationDTO truck = new TruckVerificationDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<truckverification>>()
            .GetSingle(item => item.Truck_RegNo == truckNumber && item.Truck_IsDeleted == 0), truck);

            return truck;
        }

        /// <summary>
        /// Get Truck Details By Truck Registration Id
        /// </summary>
        /// <param name="truckRegistrationId">string:truckRegistrationId</param>
        /// <returns></returns>
        public TruckDetailsDTO GetTruckDetailsByTruckRegistrationId(string truckRegistrationId)
        {
            TruckDetailsDTO truckDetails = new TruckDetailsDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<truckdetail>>()
            .GetSingle(item => item.Truck_RegNo == truckRegistrationId), truckDetails);

            //return the value
            return truckDetails;
        }

        /// <summary>
        /// Get all Inactive Truck Details
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public IList<TruckVerificationDTO> GetAllInactiveTruckDetails()
        {
            DateTime currDate = DateTime.Now.Date;
            int lastYear = (currDate.Year) - 1;
            DateTime lastDate = new DateTime(lastYear, currDate.Month, currDate.Day);

            // Settlements occured in last one year
            List<settlementofaccount> lstSettlementOfAcct = ESalesUnityContainer.Container.Resolve<IGenericRepository<settlementofaccount>>()
                .GetQuery().Where(item => item.Account_CreatedDate >= lastDate && item.Account_CreatedDate <= currDate)
                .OrderByDescending(F => F.Account_CreatedDate).ToList();
            
            List<SettlementOfAccountsDTO> lstAccount = new List<SettlementOfAccountsDTO>();
            if (lstSettlementOfAcct.Count > 0)
            {
                AutoMapper.Mapper.Map(lstSettlementOfAcct, lstAccount);
            }

            // List of trucks having settlement in last one year
            List<string> lstTrucks = lstAccount.Where(item => item.Account_Booking_Truck_RegNo != null).Select(item => item.Account_Booking_Truck_RegNo).ToList<string>();
            // List of all customer trucks
            List<TruckDetailsDTO> lstTruckDetails = new List<TruckDetailsDTO>();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<truckdetail>>()
            .GetQuery().Where(item => item.Truck_IsBlacklisted == false && item.Truck_IsDeleted == false).ToList(), lstTruckDetails);

            // List of trucks that are not involved in settlement from last one year
            List<TruckDetailsDTO> listDistinctTrucks = lstTruckDetails.FindAll(T => !lstTrucks.Contains(T.Truck_RegNo));

            // Mark trucks as inactive
            foreach (TruckDetailsDTO item in listDistinctTrucks)
            {
                truckdetail upTruckEntity = new truckdetail();
                item.Truck_IsSuspended = true;
                item.Truck_IsDeleted = true;
                AutoMapper.Mapper.Map(item, upTruckEntity);
                ESalesUnityContainer.Container.Resolve<IGenericRepository<truckdetail>>().Update(upTruckEntity);
            }

            // List of standalone trucks having settlement in last one year
            List<string> lstStandaloneTrucks = lstAccount.Where(item => item.Account_Booking_StandaloneTruck_RegNo != null).Select(item => item.Account_Booking_StandaloneTruck_RegNo).ToList<string>();
            // List of all standalone trucks
            List<StandaloneTrucksDTO> lstStandaloneDetails = new List<StandaloneTrucksDTO>();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<standalonetruck>>()
            .GetQuery().Where(item => item.StandaloneTruck_IsBlacklisted == false && item.StandaloneTruck_IsDeleted == false).ToList(), lstStandaloneDetails);

            // List of standalone trucks that are not involved in settlement from last one year
            List<StandaloneTrucksDTO> listDistinctStandaloneTrucks = lstStandaloneDetails.FindAll(T => !lstStandaloneTrucks.Contains(T.StandaloneTruck_RegNo));

            // Mark standalone trucks as inactive
            foreach (StandaloneTrucksDTO item in listDistinctStandaloneTrucks)
            {
                standalonetruck upStandaloneTruckEntity = new standalonetruck();
                item.StandaloneTruck_IsSuspended = true;
                item.StandaloneTruck_IsDeleted = true;
                AutoMapper.Mapper.Map(item, upStandaloneTruckEntity);
                ESalesUnityContainer.Container.Resolve<IGenericRepository<standalonetruck>>().Update(upStandaloneTruckEntity);
            }

            List<TruckVerificationDTO> trucks = new List<TruckVerificationDTO>();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<truckverification>>()
            .GetQuery().Where(item => item.Truck_IsSuspended == 1 && item.Truck_IsDeleted == 1), trucks);
            return trucks;
        }

        /// <summary>
        /// Get inactive truck details by truck type and truck no and activate it
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public void ActivateInactiveTruck(int truckType, string truckNumber)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                if (truckType == 1)
                {
                    TruckDetailsDTO _truckObj = new TruckDetailsDTO();
                    truckdetail upTruckEntity = new truckdetail();
                    AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<truckdetail>>()
                        .GetSingle(item => item.Truck_RegNo == truckNumber), _truckObj);
                    _truckObj.Truck_IsDeleted = false;
                    _truckObj.Truck_IsSuspended = false;
                    AutoMapper.Mapper.Map(_truckObj, upTruckEntity);
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<truckdetail>>().Update(upTruckEntity);
                }
                else if (truckType == 2)
                {
                    StandaloneTrucksDTO _standaloneTruckObj = new StandaloneTrucksDTO();
                    standalonetruck upstandaloneTruckEntity = new standalonetruck();
                    AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<standalonetruck>>()
                        .GetSingle(item => item.StandaloneTruck_RegNo == truckNumber), _standaloneTruckObj);
                    _standaloneTruckObj.StandaloneTruck_IsDeleted = false;
                    _standaloneTruckObj.StandaloneTruck_IsSuspended = false;
                    AutoMapper.Mapper.Map(_standaloneTruckObj, upstandaloneTruckEntity);
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<standalonetruck>>().Update(upstandaloneTruckEntity);
                }
                transaction.Complete();
            }
        }

        public IList<TruckVerificationDTO> GetAllSuspendedTrucks()
        {
            List<TruckVerificationDTO> trucks = new List<TruckVerificationDTO>();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<truckverification>>()
            .GetQuery().Where(item => item.Truck_IsSuspended == 1 && item.Truck_IsDeleted == 1), trucks);
            return trucks;
        }
    }
}