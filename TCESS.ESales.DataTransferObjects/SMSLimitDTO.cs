using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class SMSLimitDTO : BaseDTO
    {
        #region Primitive Properties

        public int SMSLimit_Id
        {
            get;
            set;
        }

        public int SMSLimit_Limit
        {
            get;
            set;
        }

        public Nullable<System.DateTime> SMSLimit_Date
        {
            get;
            set;
        }

        public Nullable<bool> SMSLimit_IsActive
        {
            get;
            set;
        }

        public Nullable<System.DateTime> SMSLimit_CreatedDate
        {
            get;
            set;
        }

        public Nullable<System.DateTime> SMSLimit_LastUpdatedDate
        {
            get;
            set;
        }

        public int SMSLimit_CreatedBy
        {
            get;
            set;
        }

        public string SMSLimit_AuthorizedBy
        {
            get;
            set;
        }

        #endregion
    }
}