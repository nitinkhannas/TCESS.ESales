#region Using directives

using System;

#endregion

namespace TCESS.ESales.DataTransferObjects
{
    public class BookingModeDTO : BaseDTO
    {
        public int BookingMode_Id
        {
            get;
            set;
        }

        public string BookingMode_Name
        {
            get;
            set;
        }

        public int BookingMode_Group
        {
            get;
            set;
        }

        public int BookinMode_AddDays
        {
            get;
            set;
        }

        public bool BookingMode_IsExpirable
        {
            get;
            set;
        }

        public int BookingType_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> BookingType_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> BookingType_LastUpdatedDate
        {
            get;
            set;
        }

        public bool BookingType_IsDeleted
        {
            get;
            set;
        }
    }
}