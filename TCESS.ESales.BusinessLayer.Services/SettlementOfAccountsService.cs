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
    public class SettlementOfAccountsService : ISettlementOfAccountsService
    {
        public decimal GetQtySum(int customerID)
        {
            List<SettlementOfAccountsDTO> lstSettlementOfAcct = new List<SettlementOfAccountsDTO>();
            List<booking> lstBooking = ESalesUnityContainer.Container
                    .Resolve<IGenericRepository<booking>>().GetQuery().Where(k => k.Booking_Cust_Id == customerID).ToList();

            List<decimal> qty = new List<decimal>();
            if (lstBooking.Count > 0)
            {
                lstBooking.ForEach(item =>
                    {

                        qty.AddRange(ESalesUnityContainer.Container
                      .Resolve<IGenericRepository<settlementofaccount>>().GetQuery().Where(k => k.Account_Booking_Id == item.Booking_Id && k.Account_IsDeleted == false).Select(k => k.Account_Quantity).ToList());

                    });
            }
            return qty.Sum();
        }

        /// <summary>
        /// To Save Settlement Of Accounts
        /// </summary>
        /// <param name="settlementOfAcct"></param>
        /// <returns></returns>
        public int SaveSettlementOfAccounts(SettlementOfAccountsDTO settlementOfAcct)
        {
            settlementofaccount settlementOfAcctEntity = new settlementofaccount();

            using (TransactionScope transactionScope = new TransactionScope())
            {
                AutoMapper.Mapper.Map(settlementOfAcct, settlementOfAcctEntity);

                ESalesUnityContainer.Container.Resolve<IGenericRepository<settlementofaccount>>().Save(settlementOfAcctEntity);

                BookingService bookingService = new BookingService();
                BookingDTO bookingDetail = bookingService.GetBookingDetailByBookingId(settlementOfAcct.Account_Booking_Id, true);

                //Set money receipt issued status flag to true and save booking details in database
                bookingDetail.Booking_AccountSettled = true;
                bookingDetail.Booking_AccountSettledDate = DateTime.Now.Date;   // Added by ansharma on 26-Sep-2012

                bookingService.SaveAndUpdateBookingDetail(bookingDetail);

                transactionScope.Complete();
            }

            //return value
            return settlementOfAcctEntity.Account_Id;
        }

        /// <summary>
        /// Get Settlement Of Accounts By accountId
        /// </summary>
        /// <param name="accountId">Int32:accountId</param>
        /// <returns></returns>
        public SettlementOfAccountsDTO GetSettlementOfAccountsById(int accountId)
        {
            SettlementOfAccountsDTO settlementOfAcct = new SettlementOfAccountsDTO();

            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<settlementofaccount>>()
                .GetSingle(item => item.Account_Id == accountId && item.Account_IsDeleted == false), settlementOfAcct);

            //return value
            return settlementOfAcct;
        }

        public IList<SettlementOfAccountsDTO> GetSettlementDetailsForDay(DateTime fromDate)
        {
            IList<SettlementOfAccountsDTO> lstSettlementOfAcct = new List<SettlementOfAccountsDTO>();

            List<settlementofaccount> lstSettlementOfAcctDetail = ESalesUnityContainer.Container
                    .Resolve<IGenericRepository<settlementofaccount>>().GetQuery()
                    .Where(item => item.booking.Booking_IsDeleted == false && item.Account_IsDeleted == false && item.Account_CreatedDate >= fromDate.Date).ToList();
            AutoMapper.Mapper.Map(lstSettlementOfAcctDetail, lstSettlementOfAcct);

            //return the value
            return lstSettlementOfAcct;
        }

        public SettlementOfAccountsDTO GetSettlementOfAccountsByBookingId(int bookingID)
        {
            SettlementOfAccountsDTO settlementOfAcct = new SettlementOfAccountsDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<settlementofaccount>>()
                .GetSingle(item => item.Account_Booking_Id == bookingID && item.Account_IsDeleted == false), settlementOfAcct);

            //return value
            return settlementOfAcct;
        }

        /// <summary>
        /// Get Money Receipt count and total money  By Userid on Current day
        /// </summary>
        /// <param name="userID">Int32:userID</param>
        /// <returns></returns>
        public IList<object> GetSettlementOfAccountsCount(int userID)
        {
            int count = 0;
            string totalMoney = "";

            if (userID > 0)
            {
                DateTime fromDatetime = DateTime.Now.Date.AddHours(3);
                DateTime toDatetime = DateTime.Now.Date.AddDays(1).AddHours(3).AddSeconds(-1);

                List<settlementofaccount> lstsettlementaccount = ESalesUnityContainer.Container
                  .Resolve<IGenericRepository<settlementofaccount>>().GetQuery()
                 .Where(item => (item.booking.Booking_IsDeleted == false && item.Account_IsDeleted == false) && (item.Account_CreatedDate >= fromDatetime && item.Account_CreatedDate <= toDatetime && item.Account_CreatedBy == userID)).ToList();

                if (lstsettlementaccount.Count > 0)
                {
                    count = lstsettlementaccount.Count;
                    totalMoney = lstsettlementaccount.Select(item => item.Account_Balance).Sum().ToString();
                    return new object[] { count, totalMoney };
                }
            }
            return new object[] { count, totalMoney };
        }

        public SettlementOfAccountsDTO GetLastSettlementOfAccountsByTruckNo(string truckNo)
        {
            SettlementOfAccountsDTO settlementOfAcct = new SettlementOfAccountsDTO();
            List<settlementofaccount> lstSettlementOfAcctDetail = ESalesUnityContainer.Container.Resolve<IGenericRepository<settlementofaccount>>()
                .GetQuery().Where(item => item.booking.truckdetail.Truck_RegNo == truckNo || item.booking.standalonetruck.StandaloneTruck_RegNo == truckNo).OrderByDescending(F => F.Account_CreatedDate).ToList();

            if (lstSettlementOfAcctDetail.Count > 0)
            {
                AutoMapper.Mapper.Map(lstSettlementOfAcctDetail[0], settlementOfAcct);
            }
            return settlementOfAcct;
        }


        public decimal GetMaterialAmountLiftedByCustomer(int customerID, DateTime fromDate, DateTime toDate)
        {
            List<settlementofaccount> lstSettlementOfAcctDetail = ESalesUnityContainer.Container.Resolve<IGenericRepository<settlementofaccount>>()
                .GetQuery().Where(item => item.booking.Booking_Cust_Id == customerID && (item.Account_CreatedDate <= toDate && item.Account_CreatedDate >= fromDate)).ToList();
            return lstSettlementOfAcctDetail.Sum(amt => amt.Account_TotalAmount);
        }
    }
}