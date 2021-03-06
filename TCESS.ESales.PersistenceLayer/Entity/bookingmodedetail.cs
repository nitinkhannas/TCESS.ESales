//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace TCESS.ESales.PersistenceLayer.Entity
{
    public partial class bookingmodedetail : EntityBase
    {
        #region Primitive Properties
    
        public virtual int BookingDetails_Id
        {
            get;
            set;
        }
    
        public virtual int BookingDetails_Mode_Id
        {
            get { return _bookingDetails_Mode_Id; }
            set
            {
                if (_bookingDetails_Mode_Id != value)
                {
                    if (bookingmode != null && bookingmode.BookingMode_Id != value)
                    {
                        bookingmode = null;
                    }
                    _bookingDetails_Mode_Id = value;
                }
            }
        }
        private int _bookingDetails_Mode_Id;
    
        public virtual System.DateTime BookingDetails_Date
        {
            get;
            set;
        }
    
        public virtual System.TimeSpan BookingDetails_StartTime
        {
            get;
            set;
        }
    
        public virtual System.TimeSpan BookingDetails_EndTime
        {
            get;
            set;
        }
    
        public virtual int BookingDetails_TimeInterval
        {
            get;
            set;
        }
    
        public virtual int BookingDetails_Trucks
        {
            get;
            set;
        }
    
        public virtual int BookingDetails_CreatedBy
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> BookingDetails_CreatedDate
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> BookingDetails_LastUpdatedDate
        {
            get;
            set;
        }
    
        public virtual bool BookingDetails_IsDeleted
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual bookingmode bookingmode
        {
            get { return _bookingmode; }
            set
            {
                if (!ReferenceEquals(_bookingmode, value))
                {
                    var previousValue = _bookingmode;
                    _bookingmode = value;
                    Fixupbookingmode(previousValue);
                }
            }
        }
        private bookingmode _bookingmode;

        #endregion
        #region Association Fixup
    
        private void Fixupbookingmode(bookingmode previousValue)
        {
            if (previousValue != null && previousValue.bookingmodedetails.Contains(this))
            {
                previousValue.bookingmodedetails.Remove(this);
            }
    
            if (bookingmode != null)
            {
                if (!bookingmode.bookingmodedetails.Contains(this))
                {
                    bookingmode.bookingmodedetails.Add(this);
                }
                if (BookingDetails_Mode_Id != bookingmode.BookingMode_Id)
                {
                    BookingDetails_Mode_Id = bookingmode.BookingMode_Id;
                }
            }
        }

        #endregion
    }
}
