using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class pervioussettlementDTO : BaseDTO
    {
        #region Primitive Properties

        public virtual int Booking_Id
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> Booking_CreatedDate
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> Account_CreatedDate
        {
            get;
            set;
        }

        #endregion
    }
}
