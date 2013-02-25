using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class AffidavitDetailsDTO : BaseDTO
    {
        #region Primitive Properties

        public virtual int AffidavitDetailsId
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

        public virtual Nullable<System.DateTime> AffidavitCreatedDate
        {
            get;
            set;
        }

        public virtual Nullable<int> AffidavitCreatedBy
        {
            get;
            set;
        }

        public virtual Nullable<int> AffidavitIsSubmitted
        {
            get;
            set;
        }

        public int Affidavit_CustID
        {
            get;
            set;
        }

        public virtual string Affidavit_ApprovedBy
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
                    Affidavit_CustID = value.Cust_Id;
                }
            }
        }
        

        #endregion
    }
}
