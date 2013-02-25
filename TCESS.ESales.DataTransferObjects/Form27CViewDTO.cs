using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class Form27CViewDTO : BaseDTO
    {
        #region Primitive Properties

        public virtual int Booking_Id
        {
            get;
            set;
        }

        public virtual int Booking_Cust_Id
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> Booking_Date
        {
            get;
            set;
        }

        public virtual Nullable<int> Booking_Month
        {
            get;
            set;
        }

        public virtual Nullable<int> Booking_Year
        {
            get;
            set;
        }

        public virtual long Trips
        {
            get;
            set;
        }

        public virtual Nullable<decimal> Quantity
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> Account_Created_Date
        {
            get;
            set;
        }

        public virtual string Name_of_District
        {
            get;
            set;
        }

        public virtual string TSL_Value
        {
            get;
            set;
        }

        public virtual Nullable<int> Account_Form27CId
        {
            get;
            set;
        }

        public virtual string Cust_code
        {
            get;
            set;
        }

        public virtual string Customer_Name
        {
            get;
            set;
        }

        public virtual System.DateTime Received_Date
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> Accepted_Date
        {
            get;
            set;
        }

        public virtual string PAN
        {
            get;
            set;
        }

        public virtual string Valid_Month
        {
            get;
            set;
        }

        public virtual string Valid_Year
        {
            get;
            set;
        }

        #endregion
    }
}
