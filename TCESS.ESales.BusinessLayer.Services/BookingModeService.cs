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
    public class BookingModeService : IBookingModeService
    {
        /// <summary>
        /// Retrieves all active booking types from database
        /// </summary>
        /// <returns>returns list of active booking types</returns>
        public IList<BookingModeDTO> GetBookingTypes()
        {
            List<BookingModeDTO> lstBookingType = new List<BookingModeDTO>();
            List<bookingmode> lstBookingTypeEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<bookingmode>>()
                .GetQuery().Where(item => item.BookingMode_IsDeleted == false && item.BookingMode_IsExpirable == true)
                .OrderBy(order => order.BookingMode_Name).ToList();

            //Provides mapping of bookingtype with BookingTypeDTO
            AutoMapper.Mapper.Map(lstBookingTypeEntity, lstBookingType);
            return lstBookingType;
        }

        /// <summary>
        /// Retrieves list of booking type details by booking type
        /// </summary>
        /// <returns>returns list of booking type details</returns>
        public IList<BookingModeDetailDTO> GetBookingModeDetails()
        {
            List<BookingModeDetailDTO> lstBookingModeDetails = new List<BookingModeDetailDTO>();
            
            //Provides mapping of list of bookingtype with list of BookingTypeDTO
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IBookingModeRepository>().GetBookingModeDetails(),
                lstBookingModeDetails);
            return lstBookingModeDetails;
        }

        /// <summary>
        /// Save booking mode details in database
        /// </summary>
        /// <param name="bookingModeDetails">values of BbookingModeDetails object</param>
        /// <returns>returns integer value indicating if records saved in database</returns>
        public int SaveBookingModeDetails(BookingModeDetailDTO bookingModeDetails)
        {
            bookingmodedetail bookingModeDetailEntity = new bookingmodedetail();
            AutoMapper.Mapper.Map(bookingModeDetails, bookingModeDetailEntity);
            if (bookingModeDetailEntity.BookingDetails_Id > 0)
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<bookingmodedetail>>().Update(bookingModeDetailEntity);
            }
            else
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<bookingmodedetail>>().Save(bookingModeDetailEntity);
            }
            return bookingModeDetailEntity.BookingDetails_Id;
        }

        /// <summary>
        /// Retrieves an active booking mode which is not expirable from database
        /// </summary>
        /// <returns>returns default booking mode</returns>
        public BookingModeDTO GetDefaultBookingMode()
        {
            BookingModeDTO bookingMode = new BookingModeDTO();
            bookingmode bookingModeEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<bookingmode>>()
                .GetSingle(item => item.BookingMode_IsDeleted == false && item.BookingMode_IsExpirable == false);

            //Provides mapping of bookingtype with BookingTypeDTO
            AutoMapper.Mapper.Map(bookingModeEntity, bookingMode);
            return bookingMode;
        }

        /// <summary>
        /// Verifies if booking mode id already exists for a day
        /// </summary>
        /// <param name="bookingModeId">booking mode id to be checked for duplicity</param>
        /// <returns>returns true if booking mode id exists in database, false otherwise</returns>
        public bool VerifyDuplicateBookingMode(int bookingModeId)
        {
            bool result = false;
            DateTime currentDate = DateTime.Now.Date;
            BookingModeDetailDTO bookingModeDetail = new BookingModeDetailDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<bookingmodedetail>>()
                    .GetSingle(item => item.BookingDetails_Date == currentDate && item.BookingDetails_IsDeleted==false
                        && item.BookingDetails_Mode_Id == bookingModeId), bookingModeDetail);

            //If booking mode id already exists
            if (bookingModeDetail.BookingDetails_Id > 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Verifies if booking mode id already exists for a Time
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public bool BookingModeExists(TimeSpan startTime, TimeSpan endTime)
        {
            DateTime currentDate = DateTime.Now.Date;
            List<bookingmodedetail> lstBookingModeDetail = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<bookingmodedetail>>().GetQuery()
                .Where(item => item.BookingDetails_IsDeleted == false && item.BookingDetails_Date == currentDate
                    && (item.BookingDetails_EndTime > startTime && item.BookingDetails_StartTime < endTime)).ToList();
            return lstBookingModeDetail.Count > 0 ? true : false;
        }
    }
}