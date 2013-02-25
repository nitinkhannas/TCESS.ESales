#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.PersistenceLayer.Entity;
using TCESS.ESales.PersistenceLayer.Interfaces;

#endregion

namespace TCESS.ESales.BusinessLayer.Services
{
    public class LiftingLimitService : ILiftingLimit
    {
        public void SaveLiftingLimit(LiftingLimitDTO liftingLimitDetails)
        {
            liftinglimit liftinglimitEntity = new liftinglimit();
            AutoMapper.Mapper.Map(liftingLimitDetails, liftinglimitEntity);
            if (liftingLimitDetails.LiftingLimit_ID == 0)
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<liftinglimit>>().Save(liftinglimitEntity);
                    IList<LiftingLimitDTO> lifitinglimitActivelist = GetLimitList();
                    if (lifitinglimitActivelist.Count > 0)
                    {
                        foreach (LiftingLimitDTO liftinglimitdata in lifitinglimitActivelist)
                        {
                            if (liftinglimitdata.LiftingLimit_ID != liftinglimitEntity.LiftingLimit_ID && liftinglimitdata.LiftingLimit_BusinessTypeID == liftinglimitEntity.LiftingLimit_BusinessTypeID)
                            {
                                liftinglimit limitEntity = new liftinglimit();
                                liftinglimitdata.LiftingLimit_IsActive = false;
                                AutoMapper.Mapper.Map(liftinglimitdata, limitEntity);
                                ESalesUnityContainer.Container.Resolve<IGenericRepository<liftinglimit>>().TransactionalUpdate<liftinglimit>(limitEntity);
                            }
                        }
                        ESalesUnityContainer.Container.Resolve<IGenericRepository<liftinglimit>>().SaveChanges();
                    }

                    List<customermaterialmap> custmoerlist = ESalesUnityContainer.Container.Resolve<IGenericRepository<customermaterialmap>>()
                    .GetQuery().Where(data => data.customer.Cust_BusinessType == liftingLimitDetails.LiftingLimit_BusinessTypeID).ToList();

                    if (custmoerlist.Count > 0)
                    {
                        foreach (customermaterialmap customermaterialdata in custmoerlist)
                        {
                            customermaterialdata.Cust_Mat_LiftingLimit = liftingLimitDetails.LiftingLimit_Limit;
                            customermaterialdata.Cust_Mat_Timeinterval = liftingLimitDetails.LiftingLimit_Timeinterval;
                            ESalesUnityContainer.Container.Resolve<IGenericRepository<customermaterialmap>>().TransactionalUpdate<customermaterialmap>(customermaterialdata);
                        }
                        ESalesUnityContainer.Container.Resolve<IGenericRepository<customermaterialmap>>().SaveChanges();
                    }
                    ts.Complete();
                }
            }
        }

        public IList<LiftingLimitDTO> GetLimitList()
        {
            List<LiftingLimitDTO> lstlimits = new List<LiftingLimitDTO>();
            List<liftinglimit> limitEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<liftinglimit>>()
                .GetQuery().Where(data => data.LiftingLimit_IsActive == true).ToList();

            AutoMapper.Mapper.Map(limitEntity, lstlimits);
            return lstlimits;
        }

        public IList<LiftingLimitDTO> GetLiftingLimitHistoryList()
        {
            List<LiftingLimitDTO> lstLiftingLimitHistory = new List<LiftingLimitDTO>();
            List<liftinglimit_history> lstLiftingLimitEntity = new List<liftinglimit_history>();

            lstLiftingLimitEntity = ESalesUnityContainer.Container
               .Resolve<IGenericRepository<liftinglimit_history>>().GetQuery()
               .OrderByDescending(order => order.LiftingLimit_LastUpdated).Take(2).ToList();

            AutoMapper.Mapper.Map(lstLiftingLimitEntity, lstLiftingLimitHistory);

            //return value
            return lstLiftingLimitHistory;
        }

        public LiftingLimitDTO GetLiftingLimitById(int liftingLimit_ID)
        {
            LiftingLimitDTO liftingLimitDetails = new LiftingLimitDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<liftinglimit>>()
                .GetSingle(item => item.LiftingLimit_ID == liftingLimit_ID), liftingLimitDetails);
            liftingLimitDetails.truckregtype = null;
            return liftingLimitDetails;
        }

        public int UpdateLiftingLimit(LiftingLimitDTO liftingLimitDetails)
        {
            liftinglimit LiftingLimitEntity = new liftinglimit();
            liftinglimit_history LiftingLimithistoryEntity = new liftinglimit_history();
            int LiftingLimitid;
            
            using (TransactionScope transactionScope = new TransactionScope())
            {
                AutoMapper.Mapper.Map(liftingLimitDetails, LiftingLimitEntity);
                ESalesUnityContainer.Container.Resolve<IGenericRepository<liftinglimit>>().Update(LiftingLimitEntity);
                LiftingLimitid = liftingLimitDetails.LiftingLimit_ID;

                liftingLimitDetails.LiftingLimit_ID = 0;
                AutoMapper.Mapper.Map(liftingLimitDetails, LiftingLimithistoryEntity);
                ESalesUnityContainer.Container.Resolve<IGenericRepository<liftinglimit_history>>().Save(LiftingLimithistoryEntity);
                transactionScope.Complete();
            }
            return LiftingLimitid;
        }

        public void DeleteLiftingLimit(int LiftingLimit_ID)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                LiftingLimitDTO liftingLimitDetails = new LiftingLimitDTO();
                liftingLimitDetails = ESalesUnityContainer.Container.Resolve<ILiftingLimit>()
                    .GetLiftingLimitById(LiftingLimit_ID);
                liftingLimitDetails.LiftingLimit_IsActive = false;
                UpdateLiftingLimit(liftingLimitDetails);

                transactionScope.Complete();
            }
        }

        public List<AllotedQuantityDTO> GetAllottedQuantityDetails()
        {
            List<AllotedQuantityDTO> allottedQuantity = new List<AllotedQuantityDTO>();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<allotedquantity>>().GetQuery().ToList(), allottedQuantity);
            return allottedQuantity;
        }

        public int InsertAllottedQuantity(AllotedQuantityDTO allottedQty)
        {
            allotedquantity allottedQuantityEntity = new allotedquantity();
            AutoMapper.Mapper.Map(allottedQty, allottedQuantityEntity);
            ESalesUnityContainer.Container.Resolve<IGenericRepository<allotedquantity>>().Save(allottedQuantityEntity);
            return allottedQuantityEntity.Alloted_Id;
        }

        public void UpdateLiftingLimitTruckRegId(int truckRegTypeId)
        {
            List<liftinglimit> lstLimitEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<liftinglimit>>()
                .GetQuery().Where(item => item.LiftingLimit_IsActive == true && item.LiftingLimit_BusinessTypeID != 1).ToList();

            if (lstLimitEntity.Count > 0)
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    foreach (liftinglimit item in lstLimitEntity)
                    {
                        item.LiftingLimit_TruckRegType_Id = truckRegTypeId;
                        item.LiftingLimit_LastUpdated = DateTime.Now;
                        ESalesUnityContainer.Container.Resolve<IGenericRepository<liftinglimit>>().Update(item);
                    }
                    transactionScope.Complete();
                }
            }
        }
    }
}