using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCESS.ESales.PersistenceLayer.Entity;

namespace TCESS.ESales.PersistenceLayer.Interfaces
{
    public interface IBookingModeRepository
    {
        /// <summary>
        /// Retrieves list of booking type details by booking type
        /// </summary>
        /// <returns>returns list of booking type details</returns>
        IList<bookingmodedetail> GetBookingModeDetails();
    }
}