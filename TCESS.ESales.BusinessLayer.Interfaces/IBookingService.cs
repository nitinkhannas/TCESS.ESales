#region Using directives

using System;
using System.Collections.Generic;
using TCESS.ESales.DataTransferObjects;

#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface IBookingService
    {
		int SaveAndUpdateBookingDetail(BookingDTO bookingDetails);
        BookingDTO GetBookingDetailByBookingId(int bookingId, bool isMoneyReceiptIssued);
        IList<BookingDTO> GetCounterWiseAcceptedBookingsForAgent(int agentId, int counterId);
        IList<BookingDTO> GetRejectedBookingsForAgents();
        int GetTodaysBookingCountByMode(int bookingMode, DateTime currentDate);
        IList<BookingDTO> GetTodaysAdvanceBooking(DateTime date);
        IList<object> GetTotalIssuedQty(int customerId, int materialId, DateTime currentDate);
        string SaveAllBookingInfo(IList<DcaMaterialAllocationDTO> ListDCAMaterialAllocation, BookingDTO bookingDetails,
            CounterDetailsDTO counterDailyDetail,int smsRegId);
		int GetTruckCountForDate(int truckId, System.DateTime currentDate, int truckType);
		int GetTruckCountForDateBarcode(DateTime currentDate, int truckStatus);
		BookingDTO GetBookingDetailForReprint(int bookingId);
        BookingDTO GetBookingDetailBySmsId(int smsId);
        IList<BookingDTO> GetIntransisCustomerQty(int customerId,DateTime fromDate,DateTime toDate);
        void SaveAllRejectedBookingInfo(BookingDTO bookingDetails,int smsRegId);
    }
}