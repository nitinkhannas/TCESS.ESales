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
	public class SMSService : ISMSService
	{
		/// <summary>
		/// Save And Update SMS Details
		/// </summary>
		/// <param name="smsDetails"></param>
		/// <returns></returns>
		public int SaveAndUpdateSMSDetails(SMSRegistrationDTO smsDetails)
		{
			int smsReg_Id = 0;
			smsregistration smsRegistrationEntity = new smsregistration();
			AutoMapper.Mapper.Map(smsDetails, smsRegistrationEntity);

			if (smsDetails.SMSReg_Id == 0)
			{
				ESalesUnityContainer.Container.Resolve<IGenericRepository<smsregistration>>().Save(smsRegistrationEntity);
			}
			else
			{
				ESalesUnityContainer.Container.Resolve<IGenericRepository<smsregistration>>().Update(smsRegistrationEntity);
			}

			smsReg_Id = smsRegistrationEntity.SMSReg_Id;

			//return the details
			return smsReg_Id;
		}

        public bool SaveAndUpdateSMSDetailsList(List<SMSRegistrationDTO> lstSMSRegistration)
        {
            List<smsregistration> tSmsregistration = new List<smsregistration>();
            AutoMapper.Mapper.Map(lstSMSRegistration, tSmsregistration);            
            return ESalesUnityContainer.Container.Resolve<IGenericRepository<smsregistration>>().SaveAndUpdateSMSDetailsList<smsregistration>(tSmsregistration);
        }

		/// <summary>
		/// Get Todays SMS Details
		/// </summary>
		/// <returns></returns>
		public List<SMSRegistrationDTO> GetTodaysSMSDetails()
		{
			List<SMSRegistrationDTO> lstSMSRegistrationDetails = new List<SMSRegistrationDTO>();
			DateTime Currentdate = DateTime.Now.Date;
			List<smsregistration> lstSMSRegistrationDetailsEntity = ESalesUnityContainer.Container
				.Resolve<IGenericRepository<smsregistration>>().GetQuery()
				.Where(item => item.SMSReg_IsDeleted == false && item.SMSReg_Date == Currentdate && item.SMSReg_BookingStatus == false).ToList();

			AutoMapper.Mapper.Map(lstSMSRegistrationDetailsEntity, lstSMSRegistrationDetails);

			//return the value
			return lstSMSRegistrationDetails;
		}

		/// <summary>
		/// Get Todays Count By TruckId
		/// </summary>
		/// <param name="truckNumber">Int32:truckNumber</param>
		/// <returns></returns>
		public int GetTodaysCountByTruckId(string truckNumber)
		{
			List<SMSRegistrationDTO> lstSMSRegistrationDetails = new List<SMSRegistrationDTO>();
			DateTime Currentdate = DateTime.Now.Date;
			List<smsregistration> lstSMSRegistrationDetailsEntity = ESalesUnityContainer.Container
				.Resolve<IGenericRepository<smsregistration>>().GetQuery()
				.Where(item => item.SMSReg_IsDeleted == false && item.SMSReg_Date == Currentdate && item.SMSReg_TruckNo == truckNumber).ToList();
			//&& item.SMSReg_BookingStatus==true


			AutoMapper.Mapper.Map(lstSMSRegistrationDetailsEntity, lstSMSRegistrationDetails);
			int count = (from regCount in lstSMSRegistrationDetails where regCount.SMSReg_BookingStatus == true select regCount.SMSReg_Id).Count();
			//return the value
			return count;
		}

		/// <summary>
		/// Get Todays Count By CustId
		/// </summary>
		/// <param name="custId">Int32:custId</param>
		/// <returns></returns>
		public int GetTodaysCountByCustId(int custId)
		{
			List<SMSRegistrationDTO> lstSMSRegistrationDetails = new List<SMSRegistrationDTO>();
			DateTime Currentdate = DateTime.Now.Date;
			List<smsregistration> lstSMSRegistrationDetailsEntity = ESalesUnityContainer.Container
				.Resolve<IGenericRepository<smsregistration>>().GetQuery()
				.Where(item => item.SMSReg_IsDeleted == false && item.SMSReg_Date == Currentdate && item.SMSReg_CustId == custId && item.SMSReg_BookingStatus == true).ToList();

			AutoMapper.Mapper.Map(lstSMSRegistrationDetailsEntity, lstSMSRegistrationDetails);

			//return the value
			return lstSMSRegistrationDetails.Count;
		}

		/// <summary>
		/// Get Todays SMS Details By smsRegId and Currentdate
		/// </summary>
		/// <param name="smsRegId">int32:smsRegId</param>
		/// <param name="Currentdate">DateTime:Currentdate</param>
		/// <returns></returns>
		public SMSRegistrationDTO GetTodaysSMSDetailsById(int smsRegId, DateTime Currentdate)
		{
			SMSRegistrationDTO smsRegistrationDetails = new SMSRegistrationDTO();
			smsregistration smsRegistrationDetailsEntity = ESalesUnityContainer.Container
				.Resolve<IGenericRepository<smsregistration>>().GetSingle(item => item.SMSReg_IsDeleted == false 
                    && item.SMSReg_Date == Currentdate && item.SMSReg_Id == smsRegId);

			AutoMapper.Mapper.Map(smsRegistrationDetailsEntity, smsRegistrationDetails);

			//return the value
			return smsRegistrationDetails;
		}

		public List<SMSRegistrationDTO> GetTotalSMSDetailsForDate(DateTime smsDate)
		{
			List<SMSRegistrationDTO> lstSMSRegistrationDetails = new List<SMSRegistrationDTO>();
			DateTime Currentdate = smsDate.Date;
			List<smsregistration> lstSMSRegistrationDetailsEntity = ESalesUnityContainer.Container
				.Resolve<IGenericRepository<smsregistration>>().GetQuery()
				.Where(item => item.SMSReg_Date == Currentdate).ToList();

			AutoMapper.Mapper.Map(lstSMSRegistrationDetailsEntity, lstSMSRegistrationDetails);

			//return the value
			return lstSMSRegistrationDetails;
		}

		/// <summary>
		/// Get Yesterday SMS Details By truckNumber
		/// </summary>
		/// <param name="smsRegId">string:truckNumber</param>
		/// <returns></returns>
		public SMSRegistrationDTO GetPreviousdDateSMSDetailsByTruckNo(string truckNumber)
		{
			SMSRegistrationDTO SMSRegistrationDetails = new SMSRegistrationDTO();
			DateTime PreviousDate = DateTime.Now.Date.AddDays(-1);
			smsregistration SMSRegistrationDetailsEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<smsregistration>>()
                .GetSingle(item => item.SMSReg_IsDeleted == false && item.SMSReg_BookingStatus == true 
                    && item.SMSReg_Date == PreviousDate && item.SMSReg_TruckNo == truckNumber);

			AutoMapper.Mapper.Map(SMSRegistrationDetailsEntity, SMSRegistrationDetails);
			return SMSRegistrationDetails;
		}

        /// <summary>
        /// Get Last Booking Date By CustomerId
        /// </summary>
        /// <param name="custId">int:custId</param>
        /// <returns></returns>
        public List<SMSRegistrationDTO> GetLastBookingDateByCustId(int custId)
        {
            DateTime Currentdate = DateTime.Now.Date;
            List<SMSRegistrationDTO> lstSMSRegistrationDetails = new List<SMSRegistrationDTO>();

            List<smsregistration> lstSMSRegistrationDetailsEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<smsregistration>>().GetQuery()
                .OrderByDescending(L => L.SMSReg_Date)
                .Where(L => L.SMSReg_CustId == custId && L.SMSReg_Date != Currentdate && L.SMSReg_BookingStatus == true).ToList();

            AutoMapper.Mapper.Map(lstSMSRegistrationDetailsEntity, lstSMSRegistrationDetails);
            return lstSMSRegistrationDetails;
        }

        /// <summary>
        /// Removes duplicate SMS based on Truck No.
        /// </summary>
        /// <param name="truckNumber">string:TruckNo</param>
        /// <returns></returns>
        public void GetTodaysSMSDetailsByTruckNo(string truckNumber)
        {
            List<smsregistration> lstSMSRegistrationDetailsEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<smsregistration>>().GetQuery()
                .Where(L => L.SMSReg_TruckNo == truckNumber && L.SMSReg_BookingStatus == false && L.SMSReg_IsDeleted == false).ToList();

            if (lstSMSRegistrationDetailsEntity.Count > 0)
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    foreach (smsregistration counter in lstSMSRegistrationDetailsEntity)
                    {
                        counter.SMSReg_RejectionReason = "Duplicate Entry";
                        counter.SMSReg_LastUpdatedDate = DateTime.Now;
                        counter.SMSReg_IsDeleted = true;
                        ESalesUnityContainer.Container.Resolve<IGenericRepository<smsregistration>>().Update(counter);
                    }
                    transactionScope.Complete();
                }
            }
        }

        public SMSRegistrationDTO GetDetailsBySmsIdBookingId(int bookingId, int smsId)
        {
            SMSRegistrationDTO smsDetails = new SMSRegistrationDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<smsregistration>>()
                .GetSingle(item => item.booking.Booking_Id == bookingId && item.SMSReg_Id == smsId), smsDetails);

            return smsDetails;
        }

        public SMSRegistrationDTO GetSmsDetailsByBookingId(int bookingId)
        {
            SMSRegistrationDTO smsDetails = new SMSRegistrationDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<smsregistration>>()
                .GetSingle(item => item.booking.Booking_Id == bookingId), smsDetails);

            return smsDetails;
        }
	}
}