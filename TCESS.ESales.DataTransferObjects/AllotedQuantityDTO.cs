using System;

namespace TCESS.ESales.DataTransferObjects
{
    public class AllotedQuantityDTO :BaseDTO 
    {
        #region Primitive Properties

        public int Alloted_Id
        {
            get;
            set;
        }

        public string Alloted_Quantity
        {
            get;
            set;
        }

        public int Alloted_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> Alloted_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> Alloted_LastUpdatedDate
        {
            get;
            set;
        }

        public bool  Alloted_IsDeleted
        {
            get;
            set;
        }

        #endregion
    }
}