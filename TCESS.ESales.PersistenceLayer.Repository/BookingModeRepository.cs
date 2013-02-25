using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCESS.ESales.PersistenceLayer.Interfaces;
using TCESS.ESales.PersistenceLayer.Entity;
using TCESS.ESales.PersistenceLayer.EF;

namespace TCESS.ESales.PersistenceLayer.Repository
{
    public class BookingModeRepository : GenericRepository<counter>, IBookingModeRepository
    {
        /// <summary>
        /// Retrieves list of booking type details by booking type
        /// </summary>
        /// <returns>returns list of booking type details</returns>
        public IList<bookingmodedetail> GetBookingModeDetails()
        {
            eSalesEntities entityType = (eSalesEntities)base.ObjectContext;
            DateTime currentDate = DateTime.Now.Date;
            List<bookingmodedetail> lstBookingModeDet = (from item in entityType.bookingmodedetails.Include("bookingmode")
                                                         where item.BookingDetails_IsDeleted == false 
                                                         && item.BookingDetails_Date == currentDate select item)
                                                         .OrderBy(order => order.BookingDetails_Date).ToList();
            return lstBookingModeDet;
        }
    }
}