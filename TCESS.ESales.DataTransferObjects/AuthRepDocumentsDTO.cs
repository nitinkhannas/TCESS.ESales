using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class AuthRepDocumentsDTO : BaseDTO
    {
        #region Primitive Properties

        public int AuthRepDoc_Id
        {
            get;
            set;
        }

        public int AuthRepDoc_DocId
        {
            get;
            set;
        }

        public byte[] AuthRepDoc_File
        {
            get;
            set;
        }

        public int AuthRepDoc_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> AuthRepDoc_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> AuthRepDoc_LastUpdatedDate
        {
            get;
            set;
        }

        public bool AuthRepDoc_IsDeleted
        {
            get;
            set;
        }

        #endregion
    }
}