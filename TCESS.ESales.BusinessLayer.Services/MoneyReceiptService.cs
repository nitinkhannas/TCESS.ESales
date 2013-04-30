#region Using directives

using System;
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
    public class MoneyReceiptService : IMoneyReceiptService
    {
        /// <summary>
        /// Save And Update MoneyReceipt
        /// </summary>
        /// <param name="moneyreceiptDetail"></param>
        /// <returns></returns>
        public int SaveAndUpdateMoneyReceipt(MoneyReceiptDTO moneyreceiptDetail)
        {
            moneyreceipt moneyreceiptEntity = new moneyreceipt();
            AutoMapper.Mapper.Map(moneyreceiptDetail, moneyreceiptEntity);
            
            if (moneyreceiptDetail.MoneyReceipt_Id > 0)
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<moneyreceipt>>().Update(moneyreceiptEntity);
            }
            else
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<moneyreceipt>>().Save(moneyreceiptEntity);
            }
            return moneyreceiptEntity.MoneyReceipt_Id;
        }

        /// <summary>
        /// Get All MoneyReceipts by agentId
        /// </summary>
        /// <param name="agentId">Int32:agentId</param>
        /// <returns></returns>
        public IList<MoneyReceiptDTO> GetAllMoneyReceipts(int agentId)
        {
            List<MoneyReceiptDTO> lstMoneyReceipt = new List<MoneyReceiptDTO>();

            List<moneyreceipt> lstMoneyReceiptEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<moneyreceipt>>().GetQuery()
                .Where(item => item.MoneyReceipt_IsDeleted == false && item.booking.Booking_Agent_Id == agentId && item.booking.Booking_AccountSettled==false).ToList();

            AutoMapper.Mapper.Map(lstMoneyReceiptEntity, lstMoneyReceipt);
            return lstMoneyReceipt;
        }

        /// <summary>
        /// Get Money Receipt By moneyReceiptId and bookingId
        /// </summary>
        /// <param name="moneyReceiptId">Int32:moneyReceiptId</param>
        /// <param name="bookingId">int32:bookingId</param>
        /// <returns></returns>
        public MoneyReceiptDTO GetMoneyReceiptById(int moneyReceiptId, int bookingId)
        {
            MoneyReceiptDTO moneyReceiptDetails = new MoneyReceiptDTO();

            if (moneyReceiptId > 0)
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<moneyreceipt>>()
                    .GetSingle(item => item.MoneyReceipt_Id == moneyReceiptId), moneyReceiptDetails);
            }
            else
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<moneyreceipt>>()
                    .GetSingle(item => item.MoneyReceipt_Booking_Id == bookingId), moneyReceiptDetails);
            }
            return moneyReceiptDetails;
        }
      
        /// <summary>
        /// Get Money Receipt count and total money  By Userid on Current day
        /// </summary>
        /// <param name="userID">Int32:userID</param>
        /// <returns></returns>
        public IList<object> GetMoneyRecepitCount(int userID)
        {
            int count = 0;
            string totalMoney = string.Empty; 
            
            if (userID > 0)
            {
                DateTime currentDatetime = DateTime.Now.Date;
                List<moneyreceipt> lstmoneyreceipt = ESalesUnityContainer.Container
                 .Resolve<IGenericRepository<moneyreceipt>>().GetQuery()
                 .Where(item => item.MoneyReceipt_IsDeleted == false && (item.MoneyReceipt_CreatedBy == userID && item.MoneyReceipt_CreateDate >= currentDatetime)).ToList();
                
                if (lstmoneyreceipt.Count > 0)
                {
                    count = lstmoneyreceipt.Count;
                    totalMoney = lstmoneyreceipt.Select(item => item.MoneyReceipt_AmountPaid).Sum().ToString();
                    return new object[] { count, totalMoney };
                }
            }
            return new object[] { count, totalMoney };
        }       
    }
}