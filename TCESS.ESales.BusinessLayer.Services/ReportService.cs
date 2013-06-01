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
using TCESS.ESales.DataTransferObjects.GhatoCollection;
using TCESS.ESales.BusinessLayer.Interfaces.GhatoCollection;
#endregion

namespace TCESS.ESales.BusinessLayer.Services
{
    public class ReportService : IReportService
    {
        /// <summary>
        /// Get loading advice report for date range
        /// </summary>
        /// <param name="agentId">Agent id for which report is generated</param>
        /// <param name="fromDate">From date range</param>
        /// <param name="toDate">To date range</param>
        /// <returns>returns Loading advice report for the provided date range</returns>
        public IList<BookingDTO> GetLoadingAdivceReport(int agentId, DateTime fromDate, DateTime toDate)
        {
            List<BookingDTO> lstLoadingAdivceRpt = new List<BookingDTO>();
            List<booking> lstLoadingAdivceRptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                .GetQuery().Where(item => item.Booking_Agent_Id == agentId && item.Booking_Status == true &&
               (item.Booking_Date >= fromDate.Date && item.Booking_Date <= toDate.Date)
               && item.Booking_IsDeleted == false).OrderBy(order => order.Booking_CreatedDate).ToList();
            AutoMapper.Mapper.Map(lstLoadingAdivceRptEntity, lstLoadingAdivceRpt);
            return lstLoadingAdivceRpt;
        }

        /// <summary>
        /// Get Dispatch Report by truckNo. or agentId, fromDate and toDate
        /// </summary>
        /// <param name="agentId">Int32:agentId</param>
        /// <param name="fromDate">DateTime:fromDate</param>
        /// <param name="toDate">DateTime:toDate</param>
        /// <returns></returns>
        public IList<DispatchReportDTO> GetDispatchReportCustomerwise(string filterText, int filterValue, DateTime fromDate, DateTime toDate)
        {
            toDate = toDate.AddDays(1);
            fromDate = fromDate.Date.AddHours(3);
            toDate = toDate.Date.AddHours(3);
            toDate = toDate.AddSeconds(-1);

            List<DispatchReportDTO> lstDispatchReportDTO = new List<DispatchReportDTO>();
            List<dispatchreport> lstBookingEntity = new List<dispatchreport>();

            if (filterValue == 0 && filterText == string.Empty)
            {
                lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<dispatchreport>>().GetQuery().
                   Where(item => (item.Booking_Date <= toDate && item.Booking_Date >= fromDate))
                   .OrderBy(order => order.Booking_Date).ToList();
            }
            else if (filterValue == 0 && filterText != string.Empty)
            {
                DateTime fDate = new DateTime(fromDate.Year, fromDate.Month, 1);

                lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<dispatchreport>>().GetQuery().
                   Where(item => (item.Booking_Date <= toDate && item.Booking_Date >= fDate)
                       && item.CustCode == filterText).OrderBy(order => order.Booking_Date).ToList();
            }
            else if (filterValue == 1)
            {
                lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<dispatchreport>>().GetQuery().
                   Where(item => (item.Booking_Date <= toDate && item.Booking_Date >= fromDate)
                       && item.CustCode == filterText).OrderBy(order => order.Booking_Date).ToList();
            }
            else if (filterValue == 2)
            {
                lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<dispatchreport>>().GetQuery().
                   Where(item => (item.Booking_Date <= toDate && item.Booking_Date >= fromDate)
                       && item.TruckNo == filterText).OrderBy(order => order.Booking_Date).ToList();
            }

            if (lstBookingEntity.Count > 0)
            {
                AutoMapper.Mapper.Map(lstBookingEntity, lstDispatchReportDTO);
            }
            return lstDispatchReportDTO;
        }

        /// <summary>
        /// Get cash collection report for date range
        /// </summary>
        /// <param name="agentId">Agent id for which report is generated</param>
        /// <param name="fromDate">From date range</param>
        /// <param name="toDate">To date range</param>
        /// <returns>returns Loading advice report for the provided date range</returns>
        public IList<MoneyReceiptDTO> GetCashCollectionReport(int agentId, DateTime fromDate, DateTime toDate)
        {
            toDate = toDate.AddDays(1);
            List<MoneyReceiptDTO> lstCashCollectionRpt = new List<MoneyReceiptDTO>();

            List<moneyreceipt> lstCashCollectionRptEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<moneyreceipt>>().GetQuery().Where(item => item.booking.Booking_Agent_Id == agentId && item.MoneyReceipt_IsDeleted == false &&
                (item.MoneyReceipt_CreateDate >= fromDate.Date && item.MoneyReceipt_CreateDate <= toDate.Date))
                .OrderBy(order => order.MoneyReceipt_CreateDate).ToList();

            AutoMapper.Mapper.Map(lstCashCollectionRptEntity, lstCashCollectionRpt);
            return lstCashCollectionRpt;
        }

        /// <summary>
        /// Get pending booking report for date range
        /// </summary>
        /// <param name="fromDate">From date range</param>
        /// <param name="toDate">To date range</param>
        /// <returns>returns pending booking report for the provided date range</returns>
        public IList<BookingDTO> GetPendingBookingReport(int agentId, DateTime fromDate, DateTime toDate)
        {
            List<BookingDTO> lstBookingDTO = new List<BookingDTO>();

            List<booking> lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>().GetQuery().
                Where(item => item.Booking_Agent_Id == agentId && item.Booking_AccountSettled == false && item.Booking_Status == true
                    && (item.Booking_Date <= toDate.Date && item.Booking_Date >= fromDate.Date)
                    && item.Booking_IsDeleted == false).OrderBy(order => order.agentdetail.Agent_Name).ToList();

            AutoMapper.Mapper.Map(lstBookingEntity, lstBookingDTO);
            return lstBookingDTO;
        }

        /// <summary>
        /// Get Dispatch Report by agentId,fromDate and toDate
        /// </summary>
        /// <param name="agentId">Int32:agentId</param>
        /// <param name="fromDate">DateTime:fromDate</param>
        /// <param name="toDate">DateTime:toDate</param>
        /// <returns></returns>
        public IList<DispatchReportDTO> GetDispatchReport(int agentId, DateTime fromDate, DateTime toDate)
        {
            toDate = toDate.AddDays(1);
            fromDate = fromDate.Date.AddHours(3);
            toDate = toDate.Date.AddHours(3);
            toDate = toDate.AddSeconds(-1);
            List<DispatchReportDTO> lstDispatchReportDTO = new List<DispatchReportDTO>();
            if (agentId == 0)
            {
                List<dispatchreport> lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<dispatchreport>>().GetQuery().
                   Where(item => (item.Booking_Date >= fromDate && item.Booking_Date <= toDate))
                   .OrderBy(order => order.Booking_Date).ToList();
                AutoMapper.Mapper.Map(lstBookingEntity, lstDispatchReportDTO);
            }
            else
            {
                List<dispatchreport> lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<dispatchreport>>().GetQuery().
                                   Where(item => item.Booking_Agent_Id == agentId && (item.Booking_Date >= fromDate && item.Booking_Date <= toDate))
                                   .OrderBy(order => order.Booking_Date).ToList();
                AutoMapper.Mapper.Map(lstBookingEntity, lstDispatchReportDTO);
            }
            return lstDispatchReportDTO;
        }

        /// <summary>
        /// Get SMS Booking report for date range
        /// </summary>
        /// <param name="agentId">Agent id for which report is generated</param>
        /// <param name="fromDate">From date range</param>
        /// <param name="toDate">To date range</param>
        /// <returns>returns Loading sms booking report for the provided date range</returns>
        public IList<SMSRegistrationDTO> GetLoadingSMSBookingReport(int agentId, DateTime fromDate, DateTime toDate)
        {
            List<SMSRegistrationDTO> lstLoadingSMSBookingRpt = new List<SMSRegistrationDTO>();
            List<smsregistration> lstLoadingSMSBookingRptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<smsregistration>>()
                .GetQuery().Where(item => (item.SMSReg_Date >= fromDate.Date && item.SMSReg_Date <= toDate.Date)
               ).OrderBy(order => order.SMSReg_CreatedDate).ToList();

            AutoMapper.Mapper.Map(lstLoadingSMSBookingRptEntity, lstLoadingSMSBookingRpt);
            return lstLoadingSMSBookingRpt;
        }

        /// <summary>
        /// Get SMS Booking report for date range and filter value provided
        /// </summary>
        /// <param name="fromDate">From date range</param>
        /// <param name="toDate">To date range</param>
        /// <param name="smsStatusFilter">SMS status i.e. Accepted or Rejected</param>
        /// <returns>returns Loading sms booking report for the provided date range and filter value</returns>
        public IList<SMSRegistrationDTO> GetLoadingSMSBookingReport(DateTime fromDate, DateTime toDate, int smsStatusFilter)
        {
            List<SMSRegistrationDTO> lstLoadingSMSBookingRpt = new List<SMSRegistrationDTO>();

            if (smsStatusFilter == 1)
            {
                List<smsregistration> lstLoadingSMSBookingRptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<smsregistration>>()
                    .GetQuery().Where(item => (item.SMSReg_Date >= fromDate.Date && item.SMSReg_Date <= toDate.Date
                        && item.SMSReg_BookingStatus == true && item.SMSReg_IsDeleted == false)
                   ).OrderBy(order => order.SMSReg_CreatedDate).ToList();
                AutoMapper.Mapper.Map(lstLoadingSMSBookingRptEntity, lstLoadingSMSBookingRpt);
            }
            else if (smsStatusFilter == 2)
            {
                List<smsregistration> lstLoadingSMSBookingRptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<smsregistration>>()
                    .GetQuery().Where(item => (item.SMSReg_Date >= fromDate.Date && item.SMSReg_Date <= toDate.Date
                        && item.SMSReg_BookingStatus == false && item.SMSReg_IsDeleted == true)
                   ).OrderBy(order => order.SMSReg_CreatedDate).ToList();
                AutoMapper.Mapper.Map(lstLoadingSMSBookingRptEntity, lstLoadingSMSBookingRpt);
            }
            return lstLoadingSMSBookingRpt;
        }

        /// <summary>
        /// Get Daily Booking Status Report
        /// </summary>
        /// <param name="agentId">Agent id for which report is generated</param>
        /// <param name="fromDate">From date range</param>
        /// <param name="toDate">To date range</param>
        /// <returns>returns Loading sms booking report for the provided date range</returns>
        public IList<object> GetDailyBookingStatusReport(int agentId, DateTime fromDate, DateTime toDate)
        {
            int smsAccepted = 0;
            int bookings = 0;
            int loadingAdvIssue = 0;
            int truckIn = 0;
            decimal material = 0;
            int truckOuts = 0;
            int prevDayBookings = 0;
            int pendings = 0;

            toDate = toDate.AddDays(1);
            DateTime smsFromDate = fromDate.Date.AddDays(-1);
            DateTime smsToDate = toDate.Date.AddDays(-1);

            DateTime accountFromDate = fromDate.Date.AddHours(3);
            DateTime accountToDate = toDate.Date.AddHours(3);
            accountToDate = accountToDate.AddSeconds(-1);

            List<smsregistration> lstSmsEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<smsregistration>>()
                .GetQuery().Where(item => (item.SMSReg_Date >= smsFromDate && item.SMSReg_Date < smsToDate && item.SMSReg_BookingStatus == true && item.SMSReg_IsDeleted == false)).ToList();
            if (lstSmsEntity.Count > 0)
            {
                smsAccepted = lstSmsEntity.Count;
            }

            List<booking> lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                .GetQuery().Where(item => (item.Booking_CreatedDate <= toDate.Date && item.Booking_CreatedDate >= fromDate.Date))
                                   .OrderBy(order => order.Booking_Date).ToList();
            if (lstBookingEntity.Count > 0)
            {
                bookings = lstBookingEntity.Count;
            }

            List<moneyreceipt> lstMoneyReceiptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<moneyreceipt>>()
                   .GetQuery().Where(item => (item.MoneyReceipt_CreateDate <= toDate.Date && item.MoneyReceipt_CreateDate >= fromDate.Date))
                                   .OrderBy(order => order.MoneyReceipt_CreateDate).ToList();
            if (lstMoneyReceiptEntity.Count > 0)
            {
                loadingAdvIssue = lstMoneyReceiptEntity.Count;
            }

            truckIn = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                   .GetQuery().Where(item => item.Booking_TruckInTime <= toDate.Date && item.Booking_TruckInTime >= fromDate.Date && item.Booking_TruckIn == true).Count();

            material = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                         .GetQuery().Where(item => item.Booking_TruckInTime <= toDate.Date && item.Booking_TruckInTime >= fromDate.Date && item.Booking_TruckMatLifted == true).Count();

            List<settlementofaccount> lstSettlementofAccountEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<settlementofaccount>>()
                   .GetQuery().Where(item => (item.Account_CreatedDate >= accountFromDate && item.Account_CreatedDate <= accountToDate))
                                   .OrderBy(order => order.Account_CreatedDate).ToList();
            if (lstSettlementofAccountEntity.Count > 0)
            {
                truckOuts = lstSettlementofAccountEntity.Count;
            }

            //Added by ansharma 09-28-2012
            List<booking> lstPrevBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                .GetQuery().Where(item => (item.Booking_CreatedDate <= smsToDate.Date && item.Booking_CreatedDate >= smsFromDate.Date
                    && item.Booking_MoneyReceiptIssued == true && !(item.Booking_AccountSettled == true
                    && item.Booking_AccountSettledDate <= smsToDate.Date
                    && item.Booking_AccountSettledDate >= smsFromDate.Date)))
                    .OrderBy(order => order.Booking_Date).ToList();

            if (lstPrevBookingEntity.Count > 0)
            {
                // Opening Pendings
                prevDayBookings = lstPrevBookingEntity.Count;
            }

            //Clossing Pendings
            pendings = (loadingAdvIssue - truckOuts) + prevDayBookings;
            return new object[] { smsAccepted, bookings, loadingAdvIssue, truckIn, material, truckOuts, prevDayBookings, pendings };
        }

        public IList<object> GetDailyBookingStatusReportForBarChart(int agentId, DateTime fromDate, DateTime toDate)
        {
            int smsAccepted = 0;
            int bookings = 0;
            int loadingAdvIssue = 0;
            int truckOuts = 0;
            int prevDayBookings = 0;
            int pendings = 0;

            toDate = toDate.AddDays(1);
            DateTime smsFromDate = fromDate.Date.AddDays(-1);
            DateTime smsToDate = toDate.Date.AddDays(-1);

            DateTime accountFromDate = fromDate.Date.AddHours(3);
            DateTime accountToDate = toDate.Date.AddHours(3);
            accountToDate = accountToDate.AddSeconds(-1);

            List<smsregistration> lstSmsEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<smsregistration>>()
                .GetQuery().Where(item => (item.SMSReg_Date >= smsFromDate && item.SMSReg_Date < smsToDate && item.SMSReg_BookingStatus == true && item.SMSReg_IsDeleted == false)).ToList();
            if (lstSmsEntity.Count > 0)
            {
                smsAccepted = lstSmsEntity.Count;
            }

            List<booking> lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                .GetQuery().Where(item => (item.Booking_CreatedDate <= toDate.Date && item.Booking_CreatedDate >= fromDate.Date))
                                   .OrderBy(order => order.Booking_Date).ToList();
            if (lstBookingEntity.Count > 0)
            {
                bookings = lstBookingEntity.Count;
            }

            List<moneyreceipt> lstMoneyReceiptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<moneyreceipt>>()
                   .GetQuery().Where(item => (item.MoneyReceipt_CreateDate <= toDate.Date && item.MoneyReceipt_CreateDate >= fromDate.Date))
                                   .OrderBy(order => order.MoneyReceipt_CreateDate).ToList();
            if (lstMoneyReceiptEntity.Count > 0)
            {
                loadingAdvIssue = lstMoneyReceiptEntity.Count;
            }

            List<settlementofaccount> lstSettlementofAccountEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<settlementofaccount>>()
                   .GetQuery().Where(item => (item.Account_CreatedDate >= accountFromDate && item.Account_CreatedDate <= accountToDate))
                                   .OrderBy(order => order.Account_CreatedDate).ToList();
            if (lstSettlementofAccountEntity.Count > 0)
            {
                truckOuts = lstSettlementofAccountEntity.Count;
            }

            //Added by ansharma 09-28-2012
            List<booking> lstPrevBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                .GetQuery().Where(item => (item.Booking_CreatedDate <= smsToDate.Date && item.Booking_CreatedDate >= smsFromDate.Date
                    && item.Booking_MoneyReceiptIssued == true && !(item.Booking_AccountSettled == true
                    && item.Booking_AccountSettledDate <= smsToDate.Date
                    && item.Booking_AccountSettledDate >= smsFromDate.Date)))
                    .OrderBy(order => order.Booking_Date).ToList();

            if (lstPrevBookingEntity.Count > 0)
            {
                prevDayBookings = lstPrevBookingEntity.Count;
            }

            pendings = (prevDayBookings + loadingAdvIssue) - truckOuts;
            return new object[] { smsAccepted, bookings, loadingAdvIssue, truckOuts, prevDayBookings, pendings };
        }

        /// <summary>
        /// Get Daily Booking Report for all DCAs Report
        /// </summary>        
        /// <param name="fromDate">From date range</param>
        /// <param name="toDate">To date range</param>
        /// <returns>returns Loading advice report for the provided date range</returns>
        public IList<BookingDTO> GetDailyBookingReportforallDCAsReport(int agentId, DateTime fromDate, DateTime toDate)
        {
            List<BookingDTO> lstLoadingAdivceRpt = new List<BookingDTO>();
            List<booking> lstLoadingAdivceRptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                .GetQuery().Where(item => item.Booking_Agent_Id == agentId && item.Booking_Status == true &&
               (item.Booking_Date >= fromDate.Date && item.Booking_Date <= toDate.Date)
               && item.Booking_IsDeleted == false).OrderBy(order => order.Booking_CreatedDate).ToList();

            AutoMapper.Mapper.Map(lstLoadingAdivceRptEntity, lstLoadingAdivceRpt);
            return lstLoadingAdivceRpt;
        }

        /// <summary>
        /// Get Daily Booking Report for Customer
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IList<BookingDTO> GetDailyBookingReportforCustomerReport(int custId, DateTime fromDate, DateTime toDate)
        {
            List<BookingDTO> lstLoadingAdivceRpt = new List<BookingDTO>();
            List<booking> lstLoadingAdivceRptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                .GetQuery().Where(item => item.Booking_Cust_Id == custId && item.Booking_Status == true &&
               (item.Booking_Date >= fromDate.Date && item.Booking_Date <= toDate.Date)
               && item.Booking_IsDeleted == false).OrderBy(order => order.Booking_CreatedDate).ToList();

            AutoMapper.Mapper.Map(lstLoadingAdivceRptEntity, lstLoadingAdivceRpt);
            return lstLoadingAdivceRpt;
        }

        /// <summary>
        /// Get Settlement Of Accounts
        /// </summary>
        /// <param name="account_Id"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IList<SettlementOfAccountsDTO> GetSettlementOfAccounts(int account_Id, DateTime fromDate, DateTime toDate)
        {
            List<SettlementOfAccountsDTO> settlementOfAccounts = new List<SettlementOfAccountsDTO>();
            List<settlementofaccount> settlementOfAccountsRptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<settlementofaccount>>()
                .GetQuery().Where(item => item.Account_Booking_Id == account_Id &&
               (item.Account_CreatedDate >= fromDate.Date && item.Account_CreatedDate <= toDate.Date)
               && item.Account_IsDeleted == false).OrderBy(order => order.Account_CreatedDate).ToList();

            AutoMapper.Mapper.Map(settlementOfAccountsRptEntity, settlementOfAccounts);
            return settlementOfAccounts;
        }

        /// <summary>
        /// Get DFormutilization Statement For The Month Data
        /// </summary>
        /// <param name="account_Id"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public IList<SettlementOfAccountsDTO> GetDFormutilizationStatementForTheMonthData(int account_Id, int month, int year)
        {
            List<SettlementOfAccountsDTO> settlementOfAccounts = new List<SettlementOfAccountsDTO>();
            List<settlementofaccount> settlementOfAccountsRptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<settlementofaccount>>()
                .GetQuery().Where(item => item.Account_Booking_Id == account_Id &&
               (((DateTime)item.Account_CreatedDate).Month == month && ((DateTime)item.Account_CreatedDate).Year == year)
               && item.Account_IsDeleted == false).OrderBy(order => order.Account_CreatedDate).ToList();

            AutoMapper.Mapper.Map(settlementOfAccountsRptEntity, settlementOfAccounts);
            return settlementOfAccounts;

        }

        /// <summary>
        /// Get Monthly Dispatch Report
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public IList<DispatchReportDTO> GetMonthlyDispatchReport(int agentId, int month, int year)
        {
            List<DispatchReportDTO> lstDispatchReportDTO = new List<DispatchReportDTO>();

            if (agentId == 0)
            {
                List<dispatchreport> lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<dispatchreport>>().GetQuery().
                   Where(item => (((DateTime)item.Booking_Date).Month == month && ((DateTime)item.Booking_Date).Year == year))
                   .OrderBy(order => order.Booking_Date).ToList();
                AutoMapper.Mapper.Map(lstBookingEntity, lstDispatchReportDTO);
            }
            else
            {
                List<dispatchreport> lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<dispatchreport>>().GetQuery().
                                   Where(item => item.Booking_Agent_Id == agentId && (((DateTime)item.Booking_Date).Month == month && ((DateTime)item.Booking_Date).Year == year))
                                   .OrderBy(order => order.Booking_Date).ToList();
                AutoMapper.Mapper.Map(lstBookingEntity, lstDispatchReportDTO);
            }
            return lstDispatchReportDTO;
        }

        /// <summary>
        /// Get District Wise Report of Inactive Customers Report
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IList<BookingDTO> GetDistrictWiseReportofInactiveCustomersReport(int agentId, DateTime fromDate, DateTime toDate)
        {
            List<BookingDTO> lstLoadingAdivceRpt = new List<BookingDTO>();
            List<booking> lstLoadingAdivceRptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                .GetQuery().Where(item => item.Booking_Agent_Id == agentId && item.Booking_Status == true &&
               (((DateTime)item.Booking_Date).Month <= (fromDate.Month) - 3 && ((DateTime)item.Booking_Date).Month <= (toDate.Month) - 3)
               && item.Booking_IsDeleted == false).OrderBy(order => order.Booking_CreatedDate).ToList();

            AutoMapper.Mapper.Map(lstLoadingAdivceRptEntity, lstLoadingAdivceRpt);
            return lstLoadingAdivceRpt;
        }

        /// <summary>
        /// Get District Wise Sales Report
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public IList<SalesReportDTO> GetDistrictWiseSalesReport(int agentId, int month)
        {
            int priviousmonth = (month - 1);
            List<BookingDTO> lstLoadingAdivceRpt = new List<BookingDTO>();

            if (agentId == 0)
            {
                List<booking> lstLoadingAdivceRptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                    .GetQuery().Where((item => item.Booking_Agent_Id == agentId && item.Booking_Status == true && item.Booking_Date <= System.DateTime.Now && item.Booking_IsDeleted == false))
                  .OrderBy(order => order.Booking_CreatedDate).ToList();
                AutoMapper.Mapper.Map(lstLoadingAdivceRptEntity, lstLoadingAdivceRpt);
            }
            else
            {
                List<booking> lstLoadingAdivceRptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                    .GetQuery().Where((item => item.Booking_Status == true && item.Booking_Date <= System.DateTime.Now && item.Booking_IsDeleted == false))
                  .OrderBy(order => order.Booking_CreatedDate).ToList();
                AutoMapper.Mapper.Map(lstLoadingAdivceRptEntity, lstLoadingAdivceRpt);
            }

            List<String> lst = lstLoadingAdivceRpt.GroupBy(f => f.Booking_Cust_District_Name).Select(f => f.Key).ToList<String>();
            List<SalesReportDTO> lstSalesData = new List<SalesReportDTO>();

            foreach (var distName in lst)
            {
                SalesReportDTO distSalesData = new SalesReportDTO();
                distSalesData.SalesReport_District = distName.ToString();
                distSalesData.SalesReport_Pre_Qty = lstLoadingAdivceRpt.Where(F => F.Booking_Cust_District_Name == distName.ToString() && (Convert.ToDateTime((F.Booking_Date))).Month == priviousmonth).ToList().Sum(F => F.Booking_Qty);
                distSalesData.SalesReport_Pre_Trip = lstLoadingAdivceRpt.Where(F => F.Booking_Cust_District_Name == distName.ToString() && (Convert.ToDateTime((F.Booking_Date))).Month == priviousmonth).ToList().Count();

                distSalesData.SalesReport_CrrMt_Qty = lstLoadingAdivceRpt.Where(F => F.Booking_Cust_District_Name == distName.ToString() && (Convert.ToDateTime((F.Booking_Date))).Month == month).ToList().Sum(F => F.Booking_Qty);
                distSalesData.SalesReport_CrrMt_Trip = lstLoadingAdivceRpt.Where(F => F.Booking_Cust_District_Name == distName.ToString() && (Convert.ToDateTime((F.Booking_Date))).Month == month).ToList().Count();

                distSalesData.SalesReport_Crr_Qty = lstLoadingAdivceRpt.Where(F => F.Booking_Cust_District_Name == distName.ToString()).ToList().Sum(F => F.Booking_Qty);
                distSalesData.SalesReport_Crr_Trip = lstLoadingAdivceRpt.Where(F => F.Booking_Cust_District_Name == distName.ToString()).ToList().Count();

                lstSalesData.Add(distSalesData);
            }
            return lstSalesData;
        }

        /// <summary>
        /// Get Customer Wise Sales Report
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public IList<CustomerwiseSalesReportDTO> GetCustomerWiseSalesReport(int agentId, int month, int year)
        {
            DateTime currdate = DateTime.Now;
            List<CustomerwiseSalesReportDTO> lstSalesData = new List<CustomerwiseSalesReportDTO>();

            List<loadingadivcerpt> lstLoadingAdivceRptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<loadingadivcerpt>>()
                .GetQuery().Where(item => item.Booking_Date <= currdate).OrderBy(order => order.Booking_Date).ToList();
            AutoMapper.Mapper.Map(lstLoadingAdivceRptEntity, lstSalesData);

            IList<object> lst = new List<object>();

            foreach (CustomerwiseSalesReportDTO item in lstSalesData)
            {
                lst = GetBookingsByMonth(month, year, item.Cust_Id);

                item.SalesReport_CrrMt_Qty = Convert.ToInt32(lst[0]);
                item.SalesReport_CrrMt_Trip = Convert.ToInt32(lst[1]);
                item.SalesReport_Crr_Qty = Convert.ToInt32(lst[2]);
                item.SalesReport_Crr_Trip = Convert.ToInt32(lst[3]);
            }

            return lstSalesData.OrderBy(x => x.CustomerCode).ToList();
        }

        /// <summary>
        /// Get Daily Sales Summary Report
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IList<SaleSummaryDTO> GetDailySalesSummaryReport(DateTime fromDate, DateTime toDate)
        {
            toDate = toDate.AddDays(1);
            IList<AgentDTO> lstAgent = ESalesUnityContainer.Container.Resolve<IAgentService>().GetAgentList();
            IList<SaleSummaryDTO> lstSalesSummry = new List<SaleSummaryDTO>();

            foreach (AgentDTO agent in lstAgent)
            {
                SaleSummaryDTO salesSummry = new SaleSummaryDTO();
                List<booking> lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
               .GetQuery().Where(item => (item.Booking_CreatedDate <= toDate.Date && item.Booking_CreatedDate >= fromDate.Date && item.Booking_Agent_Id == agent.Agent_Id && item.Booking_Status == true))
                               .OrderBy(order => order.Booking_Date).ToList();
                if (lstBookingEntity != null)
                {
                    salesSummry.SaleSummary_TotalQtyBooked = lstBookingEntity.Sum(F => F.Booking_Qty);
                    salesSummry.SaleSummary_TotalNoOfTrcuksBooked = lstBookingEntity.Count();
                }
                List<SettlementOfAccountsDTO> settlementOfAccounts = new List<SettlementOfAccountsDTO>();
                List<settlementofaccount> lstsettlementofaccountEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<settlementofaccount>>()
               .GetQuery().Where(item => (item.Account_CreatedDate <= toDate.Date && item.Account_CreatedDate >= fromDate.Date))
                               .OrderBy(order => order.Account_CreatedDate).ToList();
                AutoMapper.Mapper.Map(lstsettlementofaccountEntity, settlementOfAccounts);
                if (settlementOfAccounts != null)
                {
                    salesSummry.SaleSummary_TotalQtySoled = Convert.ToDecimal(settlementOfAccounts.Where(F => F.Account_Agent_Id == agent.Agent_Id).Sum(F => F.Account_Quantity));
                    salesSummry.SaleSummary_TotalNoOfTrcuksSale = settlementOfAccounts.Where(F => F.Account_Agent_Id == agent.Agent_Id).Count();
                }
                salesSummry.SaleSummary_Agent_ShortName = agent.Agent_ShortName;
                lstSalesSummry.Add(salesSummry);
            }
            return lstSalesSummry;
        }

        /// <summary>
        /// Get Consolidated Advice Report
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IList<BookingDTO> GetConsolidatedAdviceReport(int agentId, DateTime fromDate, DateTime toDate)
        {
            List<BookingDTO> lstLoadingAdivceRpt = new List<BookingDTO>();
            List<booking> lstLoadingAdivceRptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                .GetQuery().Where(item => item.Booking_Agent_Id == agentId && item.Booking_Status == true &&
               (item.Booking_Date >= fromDate.Date && item.Booking_Date <= toDate.Date)
               && item.Booking_IsDeleted == false).OrderBy(order => order.Booking_CreatedDate).ToList();

            AutoMapper.Mapper.Map(lstLoadingAdivceRptEntity, lstLoadingAdivceRpt);

            foreach (BookingDTO booking in lstLoadingAdivceRpt)
            {

                settlementofaccount settlementOfAccountsRptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<settlementofaccount>>()
                    .GetSingle(item => item.Account_Booking_Id == booking.Booking_Id
                    && item.Account_IsDeleted == false);
                if (settlementOfAccountsRptEntity != null)
                {
                    booking.Booking_Account_InvoiceNumber = settlementOfAccountsRptEntity.Account_InvoiceNumber;
                }
            }
            return lstLoadingAdivceRpt;
        }

        /// <summary>
        /// Get Pending Booking For All Report
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IList<BookingDTO> GetPendingBookingForAllReport(DateTime fromDate, DateTime toDate)
        {
            List<BookingDTO> lstBookingDTO = new List<BookingDTO>();
            List<booking> lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>().GetQuery().
                Where(item => item.Booking_AccountSettled == false && item.Booking_Status == true
                    && (item.Booking_Date <= toDate.Date && item.Booking_Date >= fromDate.Date)
                    && item.Booking_IsDeleted == false).OrderBy(order => order.agentdetail.Agent_Name).ToList();

            AutoMapper.Mapper.Map(lstBookingEntity, lstBookingDTO);
            return lstBookingDTO;
        }

        /// <summary>
        /// Get Daily Booking Report for DCA
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IList<BookingDTO> GetDailyBookingReportforDCA(DateTime fromDate, DateTime toDate)
        {
            List<BookingDTO> lstLoadingAdivceRpt = new List<BookingDTO>();
            List<booking> lstLoadingAdivceRptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                .GetQuery().Where(item => item.Booking_Status == true &&
               (item.Booking_Date >= fromDate.Date && item.Booking_Date <= toDate.Date)
               && item.Booking_IsDeleted == false).OrderBy(order => order.Booking_CreatedDate).ToList();

            AutoMapper.Mapper.Map(lstLoadingAdivceRptEntity, lstLoadingAdivceRpt);
            return lstLoadingAdivceRpt;
        }

        /// <summary>
        /// Get Sales Report
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public IList<SalesReportDTO> GetSalesReport(DateTime fromDate, int month)
        {
            int priviousmonth = month - 1;
            DateTime previousday = fromDate.AddDays(-1);
            List<BookingDTO> lstLoadingAdivceRpt = new List<BookingDTO>();

            List<booking> lstLoadingAdivceRptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                .GetQuery().Where((item => item.Booking_Status == true && item.Booking_Date <= System.DateTime.Now && item.Booking_IsDeleted == false))
              .OrderBy(order => order.Booking_CreatedDate).ToList();
            AutoMapper.Mapper.Map(lstLoadingAdivceRptEntity, lstLoadingAdivceRpt);

            List<String> lst = lstLoadingAdivceRpt.GroupBy(f => f.Booking_Agent_AgentName).Select(f => f.Key).ToList<String>();
            List<SalesReportDTO> lstSalesData = new List<SalesReportDTO>();

            foreach (var Name in lst)
            {
                SalesReportDTO distSalesData = new SalesReportDTO();
                distSalesData.SalesReport_DCA = Name.ToString();
                distSalesData.SalesReport_Pre_Qty = lstLoadingAdivceRpt.Where(F => F.Booking_Agent_AgentName == Name.ToString() && (Convert.ToDateTime((F.Booking_Date))).Month == priviousmonth).ToList().Sum(F => F.Booking_Qty);
                distSalesData.SalesReport_Pre_Trip = lstLoadingAdivceRpt.Where(F => F.Booking_Agent_AgentName == Name.ToString() && (Convert.ToDateTime((F.Booking_Date))).Month == priviousmonth).ToList().Count();

                distSalesData.SalesReport_CrrMt_Qty = lstLoadingAdivceRpt.Where(F => F.Booking_Agent_AgentName == Name.ToString() && (Convert.ToDateTime((F.Booking_Date))).Month == month).ToList().Sum(F => F.Booking_Qty);
                distSalesData.SalesReport_CrrMt_Trip = lstLoadingAdivceRpt.Where(F => F.Booking_Agent_AgentName == Name.ToString() && (Convert.ToDateTime((F.Booking_Date))).Month == month).ToList().Count();

                distSalesData.SalesReport_Crr_Qty = lstLoadingAdivceRpt.Where(F => F.Booking_Agent_AgentName == Name.ToString()).ToList().Sum(F => F.Booking_Qty);
                distSalesData.SalesReport_Crr_Trip = lstLoadingAdivceRpt.Where(F => F.Booking_Agent_AgentName == Name.ToString()).ToList().Count();

                distSalesData.SalesReport_CurrDay_Qty = lstLoadingAdivceRpt.Where(F => F.Booking_Agent_AgentName == Name.ToString() && (Convert.ToDateTime((F.Booking_Date))).Date == fromDate).ToList().Sum(F => F.Booking_Qty);
                distSalesData.SalesReport_CurrDay_Trip = lstLoadingAdivceRpt.Where(F => F.Booking_Agent_AgentName == Name.ToString() && (Convert.ToDateTime((F.Booking_Date))).Date == fromDate).ToList().Count();

                distSalesData.SalesReport_PreDay_Qty = lstLoadingAdivceRpt.Where(F => F.Booking_Agent_AgentName == Name.ToString() && (Convert.ToDateTime((F.Booking_Date))).Date <= previousday).ToList().Sum(F => F.Booking_Qty);
                distSalesData.SalesReport_PreDay_Trip = lstLoadingAdivceRpt.Where(F => F.Booking_Agent_AgentName == Name.ToString() && (Convert.ToDateTime((F.Booking_Date))).Date <= previousday).ToList().Count();
                lstSalesData.Add(distSalesData);
            }
            return lstSalesData;
        }

        /// <summary>
        /// Get Daily Loading Advice Report For All
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IList<BookingDTO> GetDailyLoadingAdviceReportForAll(DateTime fromDate, DateTime toDate)
        {
            List<BookingDTO> lstLoadingAdivceRpt = new List<BookingDTO>();
            List<booking> lstLoadingAdivceRptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                .GetQuery().Where(item => item.Booking_Status == true &&
               (item.Booking_Date >= fromDate.Date && item.Booking_Date <= toDate.Date)
               && item.Booking_IsDeleted == false).OrderBy(order => order.Booking_CreatedDate).ToList();

            AutoMapper.Mapper.Map(lstLoadingAdivceRptEntity, lstLoadingAdivceRpt);

            foreach (BookingDTO booking in lstLoadingAdivceRpt)
            {

                settlementofaccount settlementOfAccountsRptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<settlementofaccount>>()
                    .GetSingle(item => item.Account_Booking_Id == booking.Booking_Id
                    && item.Account_IsDeleted == false);
                if (settlementOfAccountsRptEntity != null)
                {
                    booking.Booking_Account_InvoiceNumber = settlementOfAccountsRptEntity.Account_InvoiceNumber;
                }
            }
            return lstLoadingAdivceRpt;
        }

        public IList<SMSLimitDTO> GetSMSLimitReport(DateTime fromDate, DateTime toDate)
        {
            List<SMSLimitDTO> lstLoadingSmsLimitRpt = new List<SMSLimitDTO>();
            List<smsbookinglimit> lstLoadingSmsLimitEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<smsbookinglimit>>()
                .GetQuery().Where(item => (item.SMSLimit_Date >= fromDate.Date && item.SMSLimit_Date <= toDate.Date))
                .OrderBy(order => order.SMSLimit_Date).ToList();

            AutoMapper.Mapper.Map(lstLoadingSmsLimitEntity, lstLoadingSmsLimitRpt);
            return lstLoadingSmsLimitRpt;
        }

        public IList<SettlementOfAccountsDTO> GetDFormReport(DateTime fromDate, DateTime toDate)
        {
            toDate = toDate.AddDays(1);
            fromDate = fromDate.Date.AddHours(3);
            toDate = toDate.Date.AddHours(3);
            toDate = toDate.AddSeconds(-1);

            List<SettlementOfAccountsDTO> settlementOfAccounts = new List<SettlementOfAccountsDTO>();
            List<settlementofaccount> settlementOfAccountsRptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<settlementofaccount>>()
                .GetQuery().Where(data => data.Account_CreatedDate >= fromDate && data.Account_CreatedDate <= toDate
                && data.Account_IsDeleted == false).OrderBy(order => order.Account_FormDNumber).ToList();

            AutoMapper.Mapper.Map(settlementOfAccountsRptEntity, settlementOfAccounts);
            return settlementOfAccounts;
        }

        public IList<SettlementOfAccountsDTO> GetRoadPermitReport(DateTime fromDate, DateTime toDate)
        {
            toDate = toDate.AddDays(1);
            fromDate = fromDate.Date.AddHours(3);
            toDate = toDate.Date.AddHours(3);
            toDate = toDate.AddSeconds(-1);

            List<SettlementOfAccountsDTO> settlementOfAccounts = new List<SettlementOfAccountsDTO>();
            List<settlementofaccount> settlementOfAccountsRptEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<settlementofaccount>>()
                .GetQuery().Where(data => data.Account_CreatedDate >= fromDate && data.Account_CreatedDate <= toDate
                && data.Account_IsDeleted == false).OrderBy(order => order.Account_RoadPermitNo).ToList();

            AutoMapper.Mapper.Map(settlementOfAccountsRptEntity, settlementOfAccounts);
            return settlementOfAccounts;
        }

        private IList<object> GetBookingsByMonth(int month, int year, int customerId)
        {
            DateTime currDate = DateTime.Now;
            List<bookingandsettlement> lstMonthBookings = ESalesUnityContainer.Container.Resolve<IGenericRepository<bookingandsettlement>>()
                .GetQuery().Where(item => item.Booking_Month == month && item.Booking_Year == year && item.Booking_Cust_Id == customerId)
                .ToList();

            decimal crrMonthQty = Convert.ToDecimal(lstMonthBookings.Sum(f => f.Quantity));
            int crrMonthTrips = Convert.ToInt32(lstMonthBookings.Sum(f => f.Trips));

            List<bookingandsettlement> lstDateBookings = ESalesUnityContainer.Container.Resolve<IGenericRepository<bookingandsettlement>>()
                .GetQuery().Where(item => item.Booking_Date <= currDate && item.Booking_Cust_Id == customerId)
                .ToList();

            decimal crrDateQty = Convert.ToDecimal(lstDateBookings.Sum(f => f.Quantity));
            int crrDateTrips = Convert.ToInt32(lstDateBookings.Sum(f => f.Trips));

            return new object[] { crrMonthQty, crrMonthTrips, crrDateQty, crrDateTrips };
        }

        public IList<object> GetBookingsCounterWise(DateTime fromDate, DateTime toDate)
        {
            toDate = toDate.AddDays(1);
            DateTime bookingFromDate = fromDate.Date.AddDays(-1);
            DateTime bookingToDate = toDate.Date.AddDays(-1);

            DateTime accountFromDate = fromDate.Date.AddHours(3);
            DateTime accountToDate = toDate.Date.AddHours(3);
            accountToDate = accountToDate.AddSeconds(-1);

            List<object> lstBookingDetails = new List<object>();

            IList<booking> lstBookings = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                .GetQuery().Where(item => item.Booking_Status == true &&
               (item.Booking_Date >= fromDate.Date && item.Booking_Date <= toDate.Date)
               && item.Booking_IsDeleted == false).OrderBy(order => order.counter.Counter_Name).ToList();

            IList<counter> lstCounters = ESalesUnityContainer.Container.Resolve<IGenericRepository<counter>>()
                .GetQuery().Where(f => f.Counter_IsDeleted == false).OrderBy(f => f.Counter_Name).ToList();

            if (lstBookings.Count > 0)
            {
                lstBookingDetails = lstBookings.GroupBy(A => A.counter.Counter_Name)
                    .Select(lst => new { countername = lst.Key, dcaname = lst.Select(f => f.agentdetail.Agent_Name).ToList()[0], total = lst.Select(t => t.Booking_Id).Count(), loadingAdvIssue = lst.Where(item => item.Booking_MoneyReceiptIssued == true).Count(), completed = lst.Where(item => item.Booking_MoneyReceiptIssued == true && item.Booking_AccountSettled == true).Count(), pending = (lst.Where(item => item.Booking_MoneyReceiptIssued == true || item.Booking_MoneyReceiptIssued == false).Count() - (lst.Where(item => item.Booking_MoneyReceiptIssued == true && item.Booking_AccountSettled == true).Count())) }).ToList<object>();
            }
            else
            {
                lstBookingDetails = lstCounters
                    .Select(lst => new { countername = lst.Counter_Name, dcaname = lst.agentdetail.Agent_Name, total = 0, loadingAdvIssue = 0, completed = 0, pending = 0 }).ToList<object>();
            }
            return lstBookingDetails;
        }

        public IList<BookingDTO> GetPendingReport(int agentId, DateTime fromDate, DateTime toDate)
        {
            List<BookingDTO> lstBookingDTO = new List<BookingDTO>();

            List<booking> lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>().GetQuery().
                Where(item => item.Booking_Agent_Id == agentId && (item.Booking_Date >= fromDate.Date && item.Booking_Date <= toDate.Date)
                    && item.Booking_Status == true && item.Booking_MoneyReceiptIssued == true && item.Booking_AccountSettled == false)
                    .OrderBy(order => order.agentdetail.Agent_Name).ToList();

            AutoMapper.Mapper.Map(lstBookingEntity, lstBookingDTO);
            return lstBookingDTO;
        }

        public IList<Form27CViewDTO> GetForm27CReport(DateTime fromDate, DateTime toDate)
        {
            toDate = toDate.AddDays(1);
            IList<Form27CViewDTO> lstform27cview = ESalesUnityContainer.Container.Resolve<IReportService>().Form27CViewList(fromDate, toDate).ToList();
            return lstform27cview;
        }

        public IList<Form27CViewDTO> Form27CViewList(DateTime fromDate, DateTime toDate)
        {
            IList<Form27CViewDTO> form27cviewList = new List<Form27CViewDTO>();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<form27cview>>()
            .GetQuery().Where(item => item.Account_Created_Date >= fromDate && item.Account_Created_Date <= toDate).ToList(), form27cviewList);
            return form27cviewList;
        }

        private IList<object> GetBookingsBySelectedDate(DateTime fromDate, DateTime toDate)
        {
            DateTime currDate = DateTime.Now;
            List<bookingandsettlement> lstMonthBookings = ESalesUnityContainer.Container.Resolve<IGenericRepository<bookingandsettlement>>()
                .GetQuery().Where(item => item.Account_Created_Date >= fromDate && item.Account_Created_Date <= toDate).ToList();

            decimal crrMonthQty = Convert.ToDecimal(lstMonthBookings.Sum(f => f.Quantity));
            int crrMonthTrips = Convert.ToInt32(lstMonthBookings.Sum(f => f.Trips));
            int tslValue = Convert.ToInt32(lstMonthBookings.Select(f => f.TSL_Value).FirstOrDefault());

            return new object[] { crrMonthQty, crrMonthTrips, tslValue };
        }

        public IList<SmsExecutiveListDTO> GetSMSSendingList()
        {
            List<SmsExecutiveListDTO> smsExecutiveListList = new List<SmsExecutiveListDTO>();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<smsexecutivelist>>()
            .GetQuery().ToList(), smsExecutiveListList);
            return smsExecutiveListList;
        }
        public IList<SMSRegistrationDTO> GetPendingSMSList()
        {
            DateTime PreviousDate = DateTime.Now.Date.AddDays(-1);
            List<SMSRegistrationDTO> smsRegistration = new List<SMSRegistrationDTO>();
            List<smsregistration> lstSmsEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<smsregistration>>()
                .GetQuery().Where(item => item.SMSReg_IsDeleted == false && item.SMSReg_BookingStatus == true && item.SMSReg_Date == PreviousDate && item.SMSReg_Booking_Id == null).ToList();
            AutoMapper.Mapper.Map(lstSmsEntity, smsRegistration);
            return smsRegistration;
        }

        public IList<ConsolidatedCustomerCollectionReportDTO> GetConsolidatedCustomerCollection(DateTime fromDate, DateTime toDate)
        {
            DateTime reportStartDate = DateTime.Now;
            List<ConsolidatedCustomerCollectionReportDTO> lstConsolidatedCustomerCollectionReportDTO = new List<ConsolidatedCustomerCollectionReportDTO>();
            IList<CustomerDTO> lstCustomer = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetActiveCustomerList().OrderBy(item => item.Cust_Code).ToList();
            IList<PaymentCollectionDTO> lstPaymentCollection = ESalesUnityContainer.Container.Resolve<IPaymentService>().GetActiveCollectionForPeriod(DateTime.Now, toDate);
            IList<BookingDTO> lstBooking = ESalesUnityContainer.Container.Resolve<IBookingService>().TotalLoadingAdviceIssued(toDate);
            IList<SettlementOfAccountsDTO> lstSettlementOfAccounts = ESalesUnityContainer.Container.Resolve<ISettlementOfAccountsService>().GetSettlementDetailsForPeriod(DateTime.Now, toDate);
            IList<PaymentCollectionDTO> lstHoldPaymentCollection = ESalesUnityContainer.Container.Resolve<IPaymentService>().GetHoldActiveCollectionForPeriod(DateTime.Now, toDate);
            
            foreach (CustomerDTO item in lstCustomer)
            {
                ConsolidatedCustomerCollectionReportDTO consolidatedCustomerRep = new ConsolidatedCustomerCollectionReportDTO();
                consolidatedCustomerRep.CustomerId = item.Cust_Id;
                consolidatedCustomerRep.CustomerName = item.Cust_TradeName;
                consolidatedCustomerRep.CustomerCode = item.Cust_Code;
                consolidatedCustomerRep.CustomerDistrict = item.Cust_District_Name;
                consolidatedCustomerRep.OpeningBalance = GetOpeningBalance(item.Cust_Id, fromDate, toDate);
                consolidatedCustomerRep.CollectionActive = lstPaymentCollection.Where(f => f.PC_CustId == item.Cust_Id).Sum(f => f.PC_Amount);
                consolidatedCustomerRep.TotalBalAvailable = consolidatedCustomerRep.OpeningBalance + consolidatedCustomerRep.CollectionActive;
                consolidatedCustomerRep.TotalLoadingAdviceIssued = lstBooking.Where(f => f.Booking_Cust_Id == item.Cust_Id).Sum(f => f.Booking_AdvanceAmount);
                consolidatedCustomerRep.TotalSettlement = lstSettlementOfAccounts.Where(f => f.Account_Booking_Cust_Id == item.Cust_Id).Sum(f => f.Account_Balance);
                consolidatedCustomerRep.ClosingBalance = consolidatedCustomerRep.TotalBalAvailable - consolidatedCustomerRep.TotalLoadingAdviceIssued - consolidatedCustomerRep.TotalSettlement;
                consolidatedCustomerRep.HoldForActivation = lstHoldPaymentCollection.Where(f => f.PC_CustId == item.Cust_Id).Sum(f => f.PC_Amount);
                lstConsolidatedCustomerCollectionReportDTO.Add(consolidatedCustomerRep);
            }
            return lstConsolidatedCustomerCollectionReportDTO;
        }

        private decimal GetOpeningBalance(int pCustId, DateTime fromDate, DateTime toDate)
        {
            decimal BalanceAmt;
            DateTime previousDay = toDate.AddDays(-1);
            decimal totalAmountCollected = ESalesUnityContainer.Container.Resolve<IPaymentService>().GetPaymentMadeByCustomer(pCustId, fromDate, previousDay);
            decimal totalRefundAmount = ESalesUnityContainer.Container.Resolve<IPaymentService>().GetCustomerPaymentRefundList(pCustId).Sum(f => f.PR_Amount);
            //get Total exp amount
            decimal totalMaterialLiftedAmount = ESalesUnityContainer.Container.Resolve<ISettlementOfAccountsService>().GetMaterialAmountLiftedByCustomer(pCustId, fromDate, previousDay);
            BalanceAmt = totalAmountCollected - (totalMaterialLiftedAmount + totalRefundAmount);
            return BalanceAmt;
        }


        public IList<CustomerCollectionSettlementDTO> GetConsolidatedCollectionReport(int customerID, DateTime fromDate, DateTime toDate)
        {
            IList<CustomerCollectionSettlementDTO> lstCustomerCollectionSettlement = new List<CustomerCollectionSettlementDTO>();

            //Collection
            IList<PaymentCollectionDTO> lstPaymentCollection = ESalesUnityContainer.Container.Resolve<IPaymentService>().GetPaymentByCustomer(customerID, fromDate, toDate);
            foreach (PaymentCollectionDTO item in lstPaymentCollection)
            {
                CustomerCollectionSettlementDTO customerCollectionSettlement = new CustomerCollectionSettlementDTO();
                customerCollectionSettlement.DateReceived = item.PC_ReceiptDate.ToString("dd.MM.yyyy");
                customerCollectionSettlement.DateActivated = item.PC_CreatedDate.ToString();//   .ToString();
                customerCollectionSettlement.TransactionType = item.PaymentModeName.ToString();
                customerCollectionSettlement.InstTruckNo = item.PC_InstrumentNo.ToString();
                customerCollectionSettlement.ReceiptNo = item.PC_InstrumentNo.ToString();
                customerCollectionSettlement.Refund = 0;
                customerCollectionSettlement.Settlement = 0;
                customerCollectionSettlement.Amount = item.PC_Amount;
                lstCustomerCollectionSettlement.Add(customerCollectionSettlement);
            }

            //Settlement 
            IList<SettlementOfAccountsDTO> lstSettlementLsit = ESalesUnityContainer.Container.Resolve<ISettlementOfAccountsService>().GetListOfMaterialLiftedByCustomer(customerID, fromDate, toDate);
            foreach (SettlementOfAccountsDTO item in lstSettlementLsit)
            {
                CustomerCollectionSettlementDTO customerCollectionSettlement = new CustomerCollectionSettlementDTO();
                customerCollectionSettlement.DateReceived = item.Account_Booking_Date.ToString("dd.MM.yyyy");
                customerCollectionSettlement.DateActivated = item.Account_CreatedDate.ToString();//   .ToString();
                customerCollectionSettlement.TransactionType = "Bill";
                customerCollectionSettlement.InstTruckNo = item.Account_Booking_StandaloneTruck_RegNo.ToString() != null ? item.Account_Booking_StandaloneTruck_RegNo.ToString() : item.Account_Booking_Truck_RegNo.ToString();
                customerCollectionSettlement.ReceiptNo = item.Account_Booking_Id.ToString();
                customerCollectionSettlement.Refund = 0;
                customerCollectionSettlement.Settlement = item.Account_TotalAmount;
                customerCollectionSettlement.Amount = 0;
                lstCustomerCollectionSettlement.Add(customerCollectionSettlement);
            }

            //Refund
            IList<PaymentRefundDTO> lstRefund = ESalesUnityContainer.Container.Resolve<IPaymentService>().GetCustomerRefundList(customerID, fromDate, toDate);
            foreach (PaymentRefundDTO item in lstRefund)
            {
                CustomerCollectionSettlementDTO customerCollectionSettlement = new CustomerCollectionSettlementDTO();
                customerCollectionSettlement.DateReceived = item.PR_CreatedDate.ToString();
                customerCollectionSettlement.DateActivated = item.PR_CreatedDate.ToString();//   .ToString();
                customerCollectionSettlement.TransactionType = "Refund" + item.PaymentModeName.ToString();
                customerCollectionSettlement.InstTruckNo = item.PR_InstrumentNo.ToString();
                customerCollectionSettlement.ReceiptNo = item.PR_ID.ToString();
                customerCollectionSettlement.Refund = item.PR_Amount;
                customerCollectionSettlement.Settlement =0;
                customerCollectionSettlement.Amount = 0;
                lstCustomerCollectionSettlement.Add(customerCollectionSettlement);
            }
            return lstCustomerCollectionSettlement;
        }
    }
}