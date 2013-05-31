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
    public class BookingService : IBookingService
    {
        /// <summary>
        /// Save and Update Booking Details
        /// </summary>
        /// <param name="bookingDetails"></param>
        /// <returns></returns>
        public int SaveAndUpdateBookingDetail(BookingDTO bookingDetails)
        {
            booking bookingEntity = new booking();
            AutoMapper.Mapper.Map(bookingDetails, bookingEntity);
            if (bookingDetails.Booking_Id == 0)
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>().Save(bookingEntity);
            }
            else
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>().Update(bookingEntity);
            }
            return bookingEntity.Booking_Id;
        }

        /// <summary>
        /// Get Booking Detail By BookingId
        /// </summary>
        /// <param name="bookingId">Int32:bookingId</param>
        /// <param name="isMoneyReceiptIssued">bool:isMoneyReceiptIssued</param>
        /// <returns></returns>
        public BookingDTO GetBookingDetailByBookingId(int bookingId, bool isMoneyReceiptIssued)
        {
            BookingDTO bookingDetails = new BookingDTO();

            if (isMoneyReceiptIssued == true)
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                    .GetSingle(item => item.Booking_Id == bookingId && item.Booking_MoneyReceiptIssued == true
                        && item.Booking_AccountSettled == false && item.Booking_IsDeleted == false), bookingDetails);
            }
            else
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                    .GetSingle(item => item.Booking_Id == bookingId && item.Booking_IsDeleted == false
                    && item.Booking_MoneyReceiptIssued == false), bookingDetails);
            }
            return bookingDetails;
        }

        /// <summary>
        /// Get total issued quantity for a customer and material on given date 
        /// </summary>
        /// <param name="customerId">Int32: customerId</param>
        /// <param name="materialId">Int32: materialId</param>
        /// <param name="currentDate">DateTime: currentDate</param>
        /// <returns>returns object containing 1. sum of total quantity and 2. total lifted trucks for booking mode</returns>
        public IList<object> GetTotalIssuedQty(int customerId, int materialId, DateTime currentDate)
        {
            int sumQty = 0;
            int totalLiftedTrucks = 0;

            List<settlementofaccount> lstSettlementEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<settlementofaccount>>()
                .GetQuery().Where(item => item.booking.Booking_Cust_Id == customerId && item.booking.Booking_MaterialType_Id == materialId
                && item.booking.Booking_Status == true).ToList();

            //If customer has already settled bookings
            if (lstSettlementEntity.Count > 0)
            {
                //Sum of total quantity issued for a day
                sumQty = Convert.ToInt32(lstSettlementEntity.Select(Q => Q.Account_Quantity).Sum());

                //Total bookings issued for current selected booking mode
                totalLiftedTrucks = lstSettlementEntity.Where(item => item.Account_CreatedDate == currentDate).Count();
            }
            return new object[] { sumQty, totalLiftedTrucks };
        }

        /// <summary>
        /// Get Counter Wise Accepted Bookings For Agent
        /// </summary>
        /// <param name="agentId">int32:agentId</param>
        /// <param name="counterId">int32:counterId</param>
        /// <returns></returns>
        public IList<BookingDTO> GetCounterWiseAcceptedBookingsForAgent(int agentId, int counterId)
        {
            DateTime smsFromDate = DateTime.Now.Date;
            List<BookingDTO> lstBookingDetails = new List<BookingDTO>();

            List<booking> lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                .GetQuery().Where(item => item.Booking_IsDeleted == false && item.Booking_Agent_Id == agentId &&
                    item.Booking_MoneyReceiptIssued == false && item.Booking_CounterId == counterId
                    && item.Booking_Date >= smsFromDate).ToList();

            AutoMapper.Mapper.Map(lstBookingEntity, lstBookingDetails);
            return lstBookingDetails;
        }

        /// <summary>
        /// Get Rejected Bookings For Agents
        /// </summary>
        /// <returns></returns>
        public IList<BookingDTO> GetRejectedBookingsForAgents()
        {
            List<BookingDTO> lstBookingDetails = new List<BookingDTO>();

            List<booking> lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                .GetQuery().Where(item => item.Booking_IsDeleted == false &&
                    item.Booking_Status == false).ToList();

            AutoMapper.Mapper.Map(lstBookingEntity, lstBookingDetails);
            return lstBookingDetails;
        }

        /// <summary>
        /// To Save all booking Info
        /// </summary>
        /// <param name="lstDCAMaterialAllocation"></param>
        /// <param name="bookingDetails"></param>
        /// <param name="counterDailyDetail"></param>
        /// <param name="smsRegId"></param>
        /// <returns></returns>
        public string SaveAllBookingInfo(IList<DcaMaterialAllocationDTO> lstDCAMaterialAllocation, BookingDTO bookingDetails,
            CounterDetailsDTO counterDailyDetail, int smsRegId)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                DcaMaterialAllocationService dcaMatAllocationService = new DcaMaterialAllocationService();
                //update agent details
                dcaMatAllocationService.SaveAndUpdateDCAMaterialDetails(lstDCAMaterialAllocation);

                bookingDetails.Booking_CounterId = counterDailyDetail.CounterDetail_Counter_ID;
                bookingDetails.Booking_Status = true;

                //Save Booking Details
                int savedBookingID = SaveAndUpdateBookingDetail(bookingDetails);

                //update counter Details
                counterDailyDetail.CounterDetail_Count += 1;
                CounterService counters = new CounterService();
                counters.UpdateCounterDailyDetails(counterDailyDetail);
                //update SMS Details
                if (smsRegId > 0)
                {
                    SMSRegistrationDTO smsRegdetails = ESalesUnityContainer.Container.Resolve<ISMSService>().GetTodaysSMSDetailsById(smsRegId, DateTime.Now.Date.AddDays(-1));
                    smsRegdetails.SMSReg_Booking_Id = savedBookingID;
                    ESalesUnityContainer.Container.Resolve<ISMSService>().SaveAndUpdateSMSDetails(smsRegdetails);
                }
                transactionScope.Complete();
                return counterDailyDetail.CounterDetail_Counter_ID.ToString();
            }
        }

        /// <summary>
        /// Get total bookings done for the current booking mode
        /// </summary>
        /// <param name="bookingMode">int32:Booking mode</param>
        /// <param name="currentDate">dateTime:current date</param>
        /// <returns></returns>
        public int GetTodaysBookingCountByMode(int bookingMode, DateTime currentDate)
        {
            List<booking> lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                .GetQuery().Where(item => item.Booking_IsDeleted == false && item.Booking_Status == true
                    && item.Booking_Mode == bookingMode && item.Booking_Date == currentDate).ToList();
            return lstBookingEntity.Count();
        }

        /// <summary>
        /// Get Todays Advance Booking
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IList<BookingDTO> GetTodaysAdvanceBooking(DateTime date)
        {
            List<BookingDTO> lstBookingDetails = new List<BookingDTO>();
            List<booking> lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>().GetQuery().
                Where(item => item.Booking_Date == date.Date && item.Booking_Status == true
               && item.Booking_IsAdvanced == true && item.Booking_IsDeleted == false)
                .OrderBy(order => order.Booking_CreatedDate).ToList();
            AutoMapper.Mapper.Map(lstBookingEntity, lstBookingDetails);
            return lstBookingDetails;
        }

        /// <summary>
        /// Get Truck Count For Date
        /// </summary>
        /// <param name="truckId">Int32:</param>
        /// <param name="currentDate">DateTime:currentDate</param>
        /// <param name="truckType">Int32:truckType</param>
        /// <returns></returns>
        public int GetTruckCountForDate(int truckId, DateTime currentDate, int truckType)
        {
            BookingDTO bookingDetails = new BookingDTO();
            int count = 0;
            if (truckType == 1)
            {
                List<booking> lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>().GetQuery().
               Where(item => item.Booking_Date == currentDate && item.Booking_Status == true
              && item.Booking_Truck_Id == truckId && item.Booking_IsDeleted == false)
               .OrderBy(order => order.Booking_CreatedDate).ToList();
                count = lstBookingEntity.Count;
            }
            else
            {
                List<booking> lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>().GetQuery().
               Where(item => item.Booking_Date == currentDate && item.Booking_Status == true
              && item.Booking_StandAlone_Truck_Id == truckId && item.Booking_IsDeleted == false)
               .OrderBy(order => order.Booking_CreatedDate).ToList();
                count = lstBookingEntity.Count;
            }
            return count;
        }

        /// <summary>
        /// Get Truck Count For date for the barcode application
        /// </summary>
        /// <param name="currentDate">DateTime:currentDate</param>
        /// <param name="TruckInformation">Int32:truckstatus</param>
        /// <returns></returns>
        public int GetTruckCountForDateBarcode(DateTime currentDate, int truckStatus)
        {
            BookingDTO bookingDetails = new BookingDTO();
            int count = 0;
            if (truckStatus == 1)
            {
                List<booking> lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>().GetQuery().
               Where(item => item.Booking_TruckInTime >= currentDate && item.Booking_Status == true
               && item.Booking_IsDeleted == false)
               .OrderBy(order => order.Booking_CreatedDate).ToList();
                count = lstBookingEntity.Count;
            }
            else if (truckStatus == 2)
            {
                List<booking> lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>().GetQuery().
               Where(item => item.Booking_TruckMatLiftedTime >= currentDate && item.Booking_Status == true
              && item.Booking_IsDeleted == false)
               .OrderBy(order => order.Booking_CreatedDate).ToList();
                count = lstBookingEntity.Count;
            }
            else
            {
                count = 0;
            }
            return count;
        }

        /// <summary>
        /// Get Booking Detail By BookingId
        /// </summary>
        /// <param name="bookingId">Int32:bookingId</param>
        /// <returns></returns>
        public BookingDTO GetBookingDetailForReprint(int bookingId)
        {
            BookingDTO bookingDetails = new BookingDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                .GetSingle(item => item.Booking_Id == bookingId && item.Booking_IsDeleted == false), bookingDetails);
            return bookingDetails;
        }

        /// <summary>
        /// Get Booking Detail By SmsId
        /// </summary>
        /// <param name="smsId">Int32:smsId</param>
        /// <returns></returns>
        public BookingDTO GetBookingDetailBySmsId(int smsId)
        {
            SMSRegistrationDTO smsDetails = new SMSRegistrationDTO();
            BookingDTO bookingDetails = new BookingDTO();

            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<smsregistration>>()
                .GetSingle(item => item.SMSReg_Id == smsId && item.SMSReg_BookingStatus == true && item.SMSReg_IsDeleted == false), smsDetails);

            if (smsDetails.SMSReg_Id > 0)
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                    .GetSingle(item => item.Booking_Id == smsDetails.SMSReg_Booking_Id && item.Booking_MoneyReceiptIssued == true
                        && item.Booking_AccountSettled == false && item.Booking_IsDeleted == false), bookingDetails);
            }
            return bookingDetails;
        }

        public IList<BookingDTO> GetIntransisCustomerQty(int customerId, DateTime fromDate, DateTime toDate)
        {
            List<BookingDTO> lstBookingDetails = new List<BookingDTO>();
            List<booking> lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>().GetQuery().
                Where(item => item.Booking_Cust_Id == customerId
                    && item.Booking_AccountSettled == false
                    && item.Booking_Status == true
                    && item.Booking_IsDeleted == false
                    && (item.Booking_Date <= toDate && item.Booking_Date >= fromDate))
                .OrderBy(order => order.Booking_CreatedDate).ToList();
            AutoMapper.Mapper.Map(lstBookingEntity, lstBookingDetails);
            return lstBookingDetails;
        }


        public void SaveAllRejectedBookingInfo(BookingDTO bookingDetails, int smsRegId)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                //Save Booking Details
                int savedBookingID = SaveAndUpdateBookingDetail(bookingDetails);
                if (smsRegId > 0)
                {
                    SMSRegistrationDTO smsRegdetails = ESalesUnityContainer.Container.Resolve<ISMSService>().GetTodaysSMSDetailsById(smsRegId, DateTime.Now.Date.AddDays(-1));
                    smsRegdetails.SMSReg_Booking_Id = savedBookingID;
                    ESalesUnityContainer.Container.Resolve<ISMSService>().SaveAndUpdateSMSDetails(smsRegdetails);
                }
                transactionScope.Complete();
            }
        }


        public IList<BookingDTO> GetUnpaidBooking()
        {
            List<BookingDTO> lstBookingDetails = new List<BookingDTO>();
            DateTime toDate = DateTime.Now.AddDays(-1);
            List<booking> lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                .GetQuery().Where(item => item.Booking_IsDeleted == false &&
                    item.Booking_Status == true && item.Booking_MoneyReceiptIssued==false && item.Booking_Date <= toDate).ToList();

            AutoMapper.Mapper.Map(lstBookingEntity, lstBookingDetails);
            return lstBookingDetails; 
        }

        public IList<BookingDTO> GetHoldPendingBooking(DateTime fromDate, DateTime toDate)
        {
            List<BookingDTO> lstBookingDetails = new List<BookingDTO>();
            List<booking> lstBookingEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<booking>>()
                .GetQuery().Where(item => item.Booking_IsDeleted == false &&
                    item.Booking_Status == true && item.Booking_AccountSettled == false && (item.Booking_Date >= fromDate && item.Booking_Date <= toDate)).ToList();

            AutoMapper.Mapper.Map(lstBookingEntity, lstBookingDetails);
            return lstBookingDetails;
        }
    }
}