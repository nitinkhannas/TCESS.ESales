using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class CustomerPartnerDTO
    {
        #region Primitive Properties

        public virtual int Cust_Partner_ID
        {
            get;
            set;
        }

        public virtual int Cust_Partner_CustId
        {
            get;
            set;
        }

        public virtual string Cust_Partner_Name
        {
            get;
            set;
        }

        public virtual string Cust_Partner_FatherName
        {
            get;
            set;
        }

        public virtual bool Cust_Partner_IsBlacklisted
        {
            get;
            set;
        }

        public virtual string Cust_Partner_BlacklistedBy
        {
            get;
            set;
        }

        public virtual int Cust_Partner_CreatedBy
        {
            get;
            set;
        }

        public virtual Nullable<DateTime> Cust_Partner_CreatedDate
        {
            get;
            set;
        }

        public virtual Nullable<DateTime> Cust_Partner_LastUpdatedDate
        {
            get;
            set;
        }

        public virtual bool Cust_Partner_IsDeleted
        {
            get;
            set;
        }

        #endregion
    }
}
