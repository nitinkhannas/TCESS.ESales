using System;

namespace TCESS.ESales.DataTransferObjects
{
    public class CustomerDocumentsViewDTO : BaseDTO
    {
        #region Primitive Properties

        public int CustDoc_Id
        {
            get;
            set;
        }

        public int CustDoc_Doc_Id
        {
            get;
            set;
        }

        public int CustDoc_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> CustDoc_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> CustDoc_LastUpdatedDate
        {
            get;
            set;
        }

        public bool CustDoc_IsDeleted
        {
            get;
            set;
        }

        #endregion
    }
}
