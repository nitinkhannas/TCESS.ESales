using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class TruckCarryCapacityDTO
    {
        #region Primitive Properties

        public int TruckCC_Id
        {
            get;
            set;
        }

        public string TruckCC_Value
        {
            get;
            set;
        }

        public int TruckCC_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> TruckCC_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> TruckCC_LastUpdatedDate
        {
            get;
            set;
        }

        public bool TruckCC_IsDeleted
        {
            get;
            set;
        }

        #endregion
    }
}