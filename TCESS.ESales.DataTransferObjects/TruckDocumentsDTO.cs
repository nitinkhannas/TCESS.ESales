using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class TruckDocumentsDTO : BaseDTO
    {
        #region Primitive Properties

        public int TruckDoc_Id
        {
            get;
            set;
        }

        public int TruckDoc_DocId
        {
            get;
            set;
        }

        public byte[] TruckDoc_File
        {
            get;
            set;
        }

        public int TruckDoc_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> TruckDoc_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> TruckDoc_LastUpdatedDate
        {
            get;
            set;
        }

        public bool TruckDoc_IsDeleted
        {
            get;
            set;
        }

        #endregion
    }
}