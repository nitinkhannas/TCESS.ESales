using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{

    public class LiftingLimitDTO : BaseDTO
    {
        #region Primitive Properties

        public int LiftingLimit_ID
        {
            get;
            set;
        }

        public int LiftingLimit_Limit
        {
            get;
            set;
        }

        public Nullable<System.DateTime> LifitingLimit_Date
        {
            get;
            set;
        }

        public Nullable<bool> LiftingLimit_IsActive
        {
            get;
            set;
        }

        public Nullable<System.DateTime> LiftingLimit_CreatedDate
        {
            get;
            set;
        }

        public Nullable<System.DateTime> LiftingLimit_LastUpdated
        {
            get;
            set;
        }

        public int LiftingLimit_CreatedBy
        {
            get;
            set;
        }

        public int LiftingLimit_Timeinterval
        {
            get;
            set;
        }

        public int LiftingLimit_BusinessTypeID
        {
            get;
            set;
        }

        public string LiftingLimit_Business_Name
        {
            get;
            set;
        }

        public virtual int Annual_LiftingLimit_Limit
        {
            get;
            set;
        }

        public virtual int LiftingLimit_TruckRegType_Id
        {
            get;
            set;
        }

        public string LiftingLimit_TruckRegType_Name
        {
            get;
            set;
        }

        public int LiftingLimit_IntervalId
        {
            get;
            set;
        }


        #endregion

        #region Navigation Properties
        private BusinessTypeDTO _businesstype;
        private TruckRegTypeDTO _truckregtype;
        private LiftingIntervalDTO _liftinginterval;

        public BusinessTypeDTO businesstype
        {
            get { return _businesstype; }
            set
            {
                if (!ReferenceEquals(_businesstype, value))
                {
                    var previousValue = _businesstype;
                    _businesstype = null;
                    LiftingLimit_Business_Name = value.BusinessType_Name;
                }
            }
        }

        public TruckRegTypeDTO truckregtype
        {
            get { return _truckregtype; }
            set
            {
                if (!ReferenceEquals(_truckregtype, value))
                {
                    var previousValue = _truckregtype;
                    _truckregtype = value;
                    if (_truckregtype != null)
                        LiftingLimit_TruckRegType_Name = value.TruckRegType_Name;
                }
            }
        }


        public virtual LiftingIntervalDTO liftinginterval
        {
            get { return _liftinginterval; }
            set
            {
                if (!ReferenceEquals(_liftinginterval, value))
                {
                    var previousValue = _liftinginterval;
                    _liftinginterval = value;
                    LiftingLimit_IntervalId = value.liftinginterval_Id;
                }
            }
        }



        #endregion

    }
}
