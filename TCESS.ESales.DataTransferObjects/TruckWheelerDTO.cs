using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class TruckWheelerDTO
    {
        #region Primitive Properties

        public int TruckWheeler_Id
        {
            get;
            set;
        }

        public string TruckWheeler_Value
        {
            get;
            set;
        }

        public int TruckWheeler_CreatedBy
        {
            get;
            set;
        }

        public  Nullable<DateTime> TruckWheeler_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> TruckWheeler_LastUpdatedDate
        {
            get;
            set;
        }

        public bool TruckWheeler_IsDeleted
        {
            get;
            set;
        }

        #endregion
    }
}