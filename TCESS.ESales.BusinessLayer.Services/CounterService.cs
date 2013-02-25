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
    public class CounterService : ICounterService
    {
        /// <summary>
        /// Get counter details
        /// </summary>
        /// <returns>returns list of counter details</returns>
        public IList<CounterDTO> GetCounterDetails()
        {
            List<CounterDTO> lstCounterDetails = new List<CounterDTO>();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<ICounterRepository>().GetCounterDetails(),
                lstCounterDetails);
            return lstCounterDetails;
        }

        /// <summary>
        /// Get counter details for Current Date
        /// </summary>
        /// <returns>returns list of counter details</returns>
        public IList<CounterDetailsDTO> GetCounterDetailsListForCurrentDate()
        {
            List<CounterDetailsDTO> lstCounterDetails = new List<CounterDetailsDTO>();
            DateTime Currentdate = DateTime.Now.Date;
            List<counterdetail> objlstCounterDetail = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<counterdetail>>().GetQuery()
                .Where(item => item.CounterDetail_Date == Currentdate).ToList();

            AutoMapper.Mapper.Map(objlstCounterDetail, lstCounterDetails);
            return lstCounterDetails;
        }

        /// <summary>
        /// Get counter List
        /// </summary>
        /// <returns>returns list of counter</returns>
        public IList<CounterDTO> GetCounterList()
        {
            List<CounterDTO> lstCounter = new List<CounterDTO>();

            List<counter> objlstCounter = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<counter>>().GetQuery()
                .Where(item => item.Counter_Id > 0).ToList();

            AutoMapper.Mapper.Map(objlstCounter, lstCounter);
            return lstCounter;
        }

        /// <summary>
        /// get Get Counters by counter Id
        /// </summary>
        /// <param name="counterId">int32:counterId</param>
        /// <returns></returns>
        public IList<CounterDTO> GetCounters(int counterId)
        {
            List<CounterDTO> lstCounterDTO = new List<CounterDTO>();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<ICounterRepository>().GetCounters(counterId),
               lstCounterDTO);
            return lstCounterDTO;
        }

        /// <summary>
        /// Save and Update Counters
        /// </summary>
        /// <param name="counterDetails"></param>
        public void SaveAndUpdateCounters(CounterDTO counterDetails)
        {
            counter counterEntity = new counter();
            AutoMapper.Mapper.Map(counterDetails, counterEntity);

            if (counterDetails.Counter_Id == 0)
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<counter>>().Save(counterEntity);
            }
            else
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<counter>>().Update(counterEntity);
            }
        }

        /// <summary>
        /// Get Counter Details  By MacId
        /// </summary>
        /// <param name="macAddress">string:macAddress</param>
        /// <param name="userId">int32:userId</param>
        /// <param name="counterId">int32:counterId</param>
        /// <returns></returns>
        public CounterDTO GetCounterDetailsByMacId(string macAddress, int userId, int counterId)
        {
            CounterDTO counterDetails = new CounterDTO();
            counter counterEntity = new counter();

            if (userId > 0)
            {
                counterEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<counter>>()
                   .GetSingle(item => item.Counter_IsDeleted == false && item.Counter_MAC_Id == macAddress
                       && item.Counter_User_Id == userId);
            }
            else
            {
                if (counterId > 0)
                {
                    counterEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<counter>>()
                      .GetSingle(item => item.Counter_IsDeleted == false && item.Counter_MAC_Id == macAddress
                          && item.Counter_Id != counterId);
                }
                else
                {
                    counterEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<counter>>()
                       .GetSingle(item => item.Counter_IsDeleted == false && item.Counter_MAC_Id == macAddress);
                }
            }

            AutoMapper.Mapper.Map(counterEntity, counterDetails);
            return counterDetails;
        }

        /// <summary>
        /// Get Counter Details By Id
        /// </summary>
        /// <param name="counterId">int32:counterId</param>
        /// <returns></returns>
        public CounterDTO GetCounterDetailsById(int counterId)
        {
            CounterDTO CounterDetails = new CounterDTO();
            counter objlstCounterDetail = ESalesUnityContainer.Container.Resolve<IGenericRepository<counter>>()
                .GetSingle(item => item.Counter_IsDeleted == false && item.Counter_Id == counterId);

            AutoMapper.Mapper.Map(objlstCounterDetail, CounterDetails);
            return CounterDetails;
        }

        /// <summary>
        /// Save Counter Daily Details
        /// </summary>
        /// <param name="counterDailyDetail"></param>
        /// <param name="ListDCAMaterialAllocation"></param>
        public void SaveCounterDailyDetails(CounterDetailsDTO counterDailyDetail, IList<DcaMaterialAllocationDTO> ListDCAMaterialAllocation)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                ESalesUnityContainer.Container.Resolve<IDcaMaterialAllocationService>().SaveAndUpdateDCAMaterialDetails(ListDCAMaterialAllocation);

                IList<CounterDetailsDTO> lstAgentCounters = GetCounterDailyDetails(counterDailyDetail.CounterDetail_Agent_Id);
                int counterId = 0;
                counterId = (from counters in lstAgentCounters where counters.CounterDetail_Counter_ID == counterDailyDetail.CounterDetail_Counter_ID select counters.CounterDetail_Id).FirstOrDefault();

                if (counterId == 0)
                {
                    counterdetail counterDailyDetailEntity = new counterdetail();
                    AutoMapper.Mapper.Map(counterDailyDetail, counterDailyDetailEntity);
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<counterdetail>>().Save(counterDailyDetailEntity);
                }
                transactionScope.Complete();
            }
        }

        /// <summary>
        /// Get Counter Daily Details
        /// </summary>
        /// <param name="agentId">Int32:agentId</param>
        /// <returns></returns>
        public IList<CounterDetailsDTO> GetCounterDailyDetails(int agentId)
        {
            List<CounterDetailsDTO> lstCounterDailyDetails = new List<CounterDetailsDTO>();
            DateTime Currentdate = DateTime.Now.Date;
            List<counterdetail> objlstCounterDetail = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<counterdetail>>().GetQuery()
                .Where(item => item.CounterDetail_Date == Currentdate && item.CounterDetail_Agent_Id == agentId).ToList();

            AutoMapper.Mapper.Map(objlstCounterDetail, lstCounterDailyDetails);
            return lstCounterDailyDetails;
        }

        /// <summary>
        /// Update Counter Daily Details
        /// </summary>
        /// <param name="counterDailyDetail"></param>
        public void UpdateCounterDailyDetails(CounterDetailsDTO counterDailyDetail)
        {
            counterdetail counterDailyDetailEntity = new counterdetail();
            AutoMapper.Mapper.Map(counterDailyDetail, counterDailyDetailEntity);
            ESalesUnityContainer.Container.Resolve<IGenericRepository<counterdetail>>().Update(counterDailyDetailEntity);
        }

        /// <summary>
        /// Get Counter Details By User Id
        /// </summary>
        /// <param name="userId">Int32:userId</param>
        /// <returns></returns>
        public int GetCounterDetailsByUserId(int userId)
        {
            IList<CounterDTO> listCounter = GetCounterDetails();
            int counterId = listCounter.Where(item => item.Counter_User_Id == userId)
                .Select(item => item.Counter_Id).FirstOrDefault();
            return counterId;
        }
    }
}