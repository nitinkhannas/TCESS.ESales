using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class TruckMakeDTO :BaseDTO 
    {
        #region Primitive Properties

        public int TruckMake_Id
        {
            get;
            set;
        }

        public string TruckMake_Name
        {
            get;
            set;
        }

        public int TruckMake_TruckWheeler_Id
        {
            get;
            set;
        }

        public string TruckMake_TruckWheeler_Value
        {
            get;
            set;
        }

        public int TruckMake_TruckCC_Id
        {
            get;
            set;
        }

        public string TruckMake_TruckCC_Value
        {
            get;
            set;
        } 

        public int TruckMake_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> TruckMake_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> TruckMake_LastUpdatedDate
        {
            get;
            set;
        }

        public bool TruckMake_IsDeleted
        {
            get;
            set;
        }

        #endregion
       
        #region Navigation Properties

        private TruckWheelerDTO _truckwheeler;
        private TruckCarryCapacityDTO _truckcarrycapacity;

        public TruckWheelerDTO truckwheeler
        {
            get { return _truckwheeler; }
            set
            {
                if (!ReferenceEquals(_truckwheeler, value))
                {
                    var previousValue = _truckwheeler;
                    _truckwheeler = null;
                    TruckMake_TruckWheeler_Value = value.TruckWheeler_Value;
                }
            }
        }

        public TruckCarryCapacityDTO truckcarrycapacity
        {
            get { return _truckcarrycapacity; }
            set
            {
                if (!ReferenceEquals(_truckcarrycapacity, value))
                {
                    var previousValue = _truckcarrycapacity;
                    _truckcarrycapacity = null;
                    TruckMake_TruckCC_Value = value.TruckCC_Value;
                }
            }
        }
       
        #endregion
    }
}