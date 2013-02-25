using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class Form27C_HistoryDTO : BaseDTO
    {
        #region Primitive Properties

        public virtual int Form27c_History_Id
        {
            get;
            set;
        }

        public virtual System.DateTime ReceivedDate
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> AcceptedByTSLDate
        {
            get;
            set;
        }

        public virtual string ValidMonth
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> CreatedDate
        {
            get;
            set;
        }

        public virtual Nullable<int> CreadedBy
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> ModifiedDate
        {
            get;
            set;
        }

        public virtual Nullable<int> ModifiedBy
        {
            get;
            set;
        }

        public virtual Nullable<int> IsSubmitted
        {
            get;
            set;
        }

        public virtual Nullable<int> IsAffidavitSubmitted
        {
            get;
            set;
        }

        public int Cust_Id
        {
            get;
            set;
        }

        public string Cust_Code
        {
            get;
            set;
        }

        public string Cust_UnitName
        {
            get;
            set;
        }
       
        public virtual Nullable<System.DateTime> AffidavitSubmitDate
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> AffidavitExpiryDate
        {
            get;
            set;
        }

        public virtual string ValidYear
        {
            get;
            set;
        }

        public virtual Nullable<int> ValidMonthCount
        {
            get;
            set;
        }

        public virtual Nullable<int> CurrentMonth
        {
            get;
            set;
        }

        public virtual string RejectionReason
        {
            get;
            set;
        }

        #endregion

        #region Navigation Properties

        private CustomerDTO _customer;

        public CustomerDTO customer
        {
            get { return _customer; }
            set
            {
                if (!ReferenceEquals(_customer, value))
                {
                    var previousValue = _customer;
                    _customer = value;
                    Cust_Id = value.Cust_Id;
                    Cust_Code = value.Cust_Code;
                    Cust_UnitName = value.Cust_FirmName;
                }
            }
        }        

        #endregion
    }
}
