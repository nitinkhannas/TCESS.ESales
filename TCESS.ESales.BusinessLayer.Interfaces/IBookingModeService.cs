#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCESS.ESales.DataTransferObjects;

#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface IBookingModeService
    {
        /// <summary>
        /// Retrieves all active booking types from database
        /// </summary>
        /// <returns>returns list of active booking types</returns>
        IList<BookingModeDTO> GetBookingTypes();

        /// <summary>
        /// Retrieves list of booking type details by booking type
        /// </summary>
        /// <returns>returns list of booking type details</returns>
        IList<BookingModeDetailDTO> GetBookingModeDetails();

        /// <summary>
        /// Save booking mode details in database
        /// </summary>
        /// <param name="bookingModeDetails">values of BbookingModeDetails object</param>
        /// <returns>returns integer value indicating if records saved in database</returns>
        int SaveBookingModeDetails(BookingModeDetailDTO bookingModeDetails);

        /// <summary>
        /// Retrieves an active booking mode which is not expirable from database
        /// </summary>
        /// <returns>returns default booking mode</returns>
        BookingModeDTO GetDefaultBookingMode();

        bool VerifyDuplicateBookingMode(int bookingModeId);
       
        /// <summary>
        /// Retrieves an active booking mode which is not expirable from database
        /// </summary>
        /// <returns>returns true if booking within start time and end time range exists</returns>
        bool BookingModeExists(TimeSpan startTime,TimeSpan endTime);
    }
}