using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class TruckRegTypeDTO:BaseDTO
    {
        #region Primitive Properties

        public  int TruckRegType_Id
        {
            get;
            set;
        }

        public  string TruckRegType_Name
        {
            get;
            set;
        }

        public  int TruckRegType_CreatedBy
        {
            get;
            set;
        }

        public  Nullable<System.DateTime> TruckRegType_CreatedDate
        {
            get;
            set;
        }

        public  Nullable<System.DateTime> TruckRegType_LastUpdatedDate
        {
            get;
            set;
        }

        public  bool TruckRegType_IsDeleted
        {
            get;
            set;
        }

        #endregion
    }
}
