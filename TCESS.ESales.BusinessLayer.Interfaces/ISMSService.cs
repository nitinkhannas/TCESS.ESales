#region Using directives

using System.Collections.Generic;
using TCESS.ESales.DataTransferObjects;
using System;

#endregion


namespace TCESS.ESales.BusinessLayer.Interfaces
{
	public interface ISMSService
	{
        bool SaveAndUpdateSMSDetailsList(List<SMSRegistrationDTO> lstSMSRegistration);
		int SaveAndUpdateSMSDetails(SMSRegistrationDTO smsDetails);
		SMSRegistrationDTO GetTodaysSMSDetailsById(int smsRegId, DateTime Currentdate);
		List<SMSRegistrationDTO> GetTodaysSMSDetails();
		List<SMSRegistrationDTO> GetTotalSMSDetailsForDate(DateTime smsDate);
		int GetTodaysCountByTruckId(string truckNumber);
		int GetTodaysCountByCustId(int custId);
		SMSRegistrationDTO GetPreviousdDateSMSDetailsByTruckNo(string truckNumber);
        List<SMSRegistrationDTO> GetLastBookingDateByCustId(int custId);
        void GetTodaysSMSDetailsByTruckNo(string truckNumber);
        SMSRegistrationDTO GetDetailsBySmsIdBookingId(int bookingId, int smsId);
        SMSRegistrationDTO GetSmsDetailsByBookingId(int bookingId);
    }
}
