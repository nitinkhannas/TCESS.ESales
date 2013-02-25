using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class Form27PeriodTypeDTO : BaseDTO
    {
        #region Primitive Properties

        public virtual int form27cPeriodType_Id
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> CreatedDate
        {
            get;
            set;
        }

        public virtual Nullable<int> CreatedBy
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

        public virtual string PeriodType
        {
            get;
            set;
        }

        public int PeriodTypeId
        {
            get;
            set;
        }        

        #endregion

        #region Navigation Properties

        private PeriodTypeDTO _periodtype1;

        public PeriodTypeDTO periodtype1
        {
            get { return _periodtype1; }
            set
            {
                if (!ReferenceEquals(_periodtype1, value))
                {
                    var previousValue = _periodtype1;
                    _periodtype1 = value;
                    PeriodTypeId = value.PeriodType_Id;
                }
            }
        }

        #endregion
    }
}
