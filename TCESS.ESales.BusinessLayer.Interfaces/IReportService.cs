#region Using directives

using System;
using System.Collections.Generic;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.DataTransferObjects.GhatoCollection;
#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface IReportService
    {
        /// <summary>
        /// Get loading advice report for date range
        /// </summary>
        /// <param name="agentId">Agent id for which report is generated</param>
        /// <param name="fromDate">From date range</param>
        /// <param name="toDate">To date range</param>
        /// <returns>returns Loading advice report for the provided date range</returns>
        IList<BookingDTO> GetLoadingAdivceReport(int agentId, DateTime fromDate, DateTime toDate);
        /// <summary>
        /// Get pending booking report for date range
        /// </summary>
        /// <param name="fromDate">From date range</param>
        /// <param name="toDate">To date range</param>
        /// <returns>returns pending booking report for the provided date range</returns>
        IList<BookingDTO> GetPendingBookingReport(int agentId, DateTime fromDate, DateTime toDate);
        /// <summary>
        /// Get cash collection report for date range
        /// </summary>
        /// <param name="agentId">Agent id for which report is generated</param>
        /// <param name="fromDate">From date range</param>
        /// <param name="toDate">To date range</param>
        /// <returns>returns Loading advice report for the provided date range</returns>
        IList<MoneyReceiptDTO> GetCashCollectionReport(int agentId, DateTime fromDate, DateTime toDate);
        /// <summary>
        /// Get dispatch report for date range
        /// </summary>
        /// <param name="agentId">Agent id for which report is generated</param>
        /// <param name="fromDate">From date range</param>
        /// <param name="toDate">To date range</param>
        /// <returns>returns dispatch report for the provided date range</returns>
        IList<DispatchReportDTO> GetDispatchReport(int agentId, DateTime fromDate, DateTime toDate);
        /// <summary>
        /// Get SMS booking report for date range
        /// </summary>
        /// <param name="agentId">Agent id for which report is generated</param>
        /// <param name="fromDate">From date range</param>
        /// <param name="toDate">To date range</param>
        /// <returns>returns SMS booking report for the provided date range</returns>
        IList<SMSRegistrationDTO> GetLoadingSMSBookingReport(int agentId, DateTime fromDate, DateTime toDate);
        /// <summary>
        /// Get SMS Booking report for date range and filter value provided
        /// </summary>
        /// <param name="fromDate">From date range</param>
        /// <param name="toDate">To date range</param>
        /// <param name="smsStatusFilter">Sms Status</param>
        /// <returns>returns Loading sms booking report for the provided date range</returns>
        IList<SMSRegistrationDTO> GetLoadingSMSBookingReport(DateTime fromDate, DateTime toDate, int smsStatusFilter);
        /// <summary>
        /// Get SMS Booking Limit for date range
        /// </summary>
        /// <param name="fromDate">From date range</param>
        /// <param name="toDate">To date range</param>
        /// <returns>returns Loading sms limit report for the provided date range</returns>
        IList<SMSLimitDTO> GetSMSLimitReport(DateTime fromDate, DateTime toDate);
        /// <summary>
        /// Get SMS booking report for date range
        /// </summary>
        /// <param name="agentId">Agent id for which report is generated</param>
        /// <param name="fromDate">From date range</param>
        /// <param name="toDate">To date range</param>
        /// <returns>returns SMS booking report for the provided date range</returns>        
        IList<object> GetDailyBookingStatusReport(int agentId, DateTime fromDate, DateTime toDate);
        IList<object> GetDailyBookingStatusReportForBarChart(int agentId, DateTime fromDate, DateTime toDate);
        /// <summary>
        /// Get Daily Booking Report for all DCAs Report
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        IList<BookingDTO> GetDailyBookingReportforallDCAsReport(int agentId, DateTime fromDate, DateTime toDate);
        /// <summary>
        /// Get Daily Booking Report for Customer Report
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        IList<BookingDTO> GetDailyBookingReportforCustomerReport(int userId, DateTime fromDate, DateTime toDate);
        /// <summary>
        /// Get Settlement Of Accounts
        /// </summary>
        /// <param name="Account_Id"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        IList<SettlementOfAccountsDTO> GetSettlementOfAccounts(int Account_Id, DateTime fromDate, DateTime toDate);
        /// <summary>
        /// Get DFormutilization Statement For The Month Data
        /// </summary>
        /// <param name="Account_Id"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        IList<SettlementOfAccountsDTO> GetDFormutilizationStatementForTheMonthData(int Account_Id, int month, int year);
        /// <summary>
        /// Get Monthly Dispatch Report
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        IList<DispatchReportDTO> GetMonthlyDispatchReport(int agentId, int month, int year);
        /// <summary>
        /// Get District Wise Report of Inactive Customers Report
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        IList<BookingDTO> GetDistrictWiseReportofInactiveCustomersReport(int agentId, DateTime fromDate, DateTime toDate);
        /// <summary>
        /// Get District Wise Sales Report
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        IList<SalesReportDTO> GetDistrictWiseSalesReport(int agentId, int month);
        /// <summary>
        /// Get Customer Wise Sales Report
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        IList<CustomerwiseSalesReportDTO> GetCustomerWiseSalesReport(int agentId, int month, int year);
        /// <summary>
        /// Get Daily Sales Summary Report
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        IList<SaleSummaryDTO> GetDailySalesSummaryReport(DateTime fromDate, DateTime toDate);
        /// <summary>
        /// Get Consolidated Advice Report
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        IList<BookingDTO> GetConsolidatedAdviceReport(int agentId, DateTime fromDate, DateTime toDate);
        /// <summary>
        /// Get Pending Booking For All Report
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        IList<BookingDTO> GetPendingBookingForAllReport(DateTime fromDate, DateTime toDate);
        /// <summary>
        /// Get Daily Booking Report for DCA
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        IList<BookingDTO> GetDailyBookingReportforDCA(DateTime fromDate, DateTime toDate);
        /// <summary>
        /// Get Sales Report
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        IList<SalesReportDTO> GetSalesReport(DateTime fromDate, int month);
        /// <summary>
        /// Get Daily Loading Advice Report For All
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        IList<BookingDTO> GetDailyLoadingAdviceReportForAll(DateTime fromDate, DateTime toDate);
        IList<DispatchReportDTO> GetDispatchReportCustomerwise(string filterText, int filterValue, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Get DForm Report
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        IList<SettlementOfAccountsDTO> GetDFormReport(DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Get RoadPermit Report
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        IList<SettlementOfAccountsDTO> GetRoadPermitReport(DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Get Booking details counter wise
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        IList<object> GetBookingsCounterWise(DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Get Pending Booking details
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        IList<BookingDTO> GetPendingReport(int agentId, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Gets the form27 C report.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns></returns>
        IList<Form27CViewDTO> GetForm27CReport(DateTime fromDate, DateTime toDate);
        IList<Form27CViewDTO> Form27CViewList(DateTime fromDate, DateTime toDate);
        IList<SmsExecutiveListDTO> GetSMSSendingList();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<SMSRegistrationDTO> GetPendingSMSList();

        IList<ConsolidatedCustomerCollectionReportDTO> GetConsolidatedCustomerCollection(DateTime fromDate, DateTime toDate);

        IList<CustomerCollectionSettlementDTO> GetConsolidatedCollectionReport(int customerID, DateTime fromDate, DateTime toDate);
    }
}