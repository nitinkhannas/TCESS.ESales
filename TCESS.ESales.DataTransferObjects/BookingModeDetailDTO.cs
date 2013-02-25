using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class BookingModeDetailDTO : BaseDTO
    {
        public int BookingDetails_Id
        {
            get;
            set;
        }

        public int BookingDetails_Mode_Id
        {
            get;
            set;
        }

        public string BookingDetails_Mode_Name
        {
            get;
            set;
        }

        public DateTime BookingDetails_Date
        {
            get;
            set;
        }

        public TimeSpan BookingDetails_StartTime
        {
            get;
            set;
        }

        public TimeSpan BookingDetails_EndTime
        {
            get;
            set;
        }

        public int BookingDetails_TimeInterval
        {
            get;
            set;
        }

        public int BookingDetails_Trucks
        {
            get;
            set;
        }

        public int BookingDetails_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> BookingDetails_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> BookingDetails_LastUpdatedDate
        {
            get;
            set;
        }

        public bool BookingDetails_IsDeleted
        {
            get;
            set;
        }
		public bool BookingDetails_Mode_IsExpirable
		{
			get;
			set;
		}
		public int BookingDetails_Mode_Group
		{
			get;
			set;
		}

		public int BookingDetails_Mode_AddDays
		{
			get;
			set;
		}

        #region Navigation Properties

        private BookingModeDTO _bookingmode;

        public BookingModeDTO bookingmode
        {
            get { return _bookingmode; }
            set
            {
                if (!ReferenceEquals(_bookingmode, value))
                {
                    var previousValue = _bookingmode;
                    _bookingmode = null;
                    BookingDetails_Mode_Name = value.BookingMode_Name;
					BookingDetails_Mode_IsExpirable = value.BookingMode_IsExpirable;
					BookingDetails_Mode_Group = value.BookingMode_Group;
					BookingDetails_Mode_AddDays = value.BookinMode_AddDays;
                }
            }
        }        

        #endregion
    }
}