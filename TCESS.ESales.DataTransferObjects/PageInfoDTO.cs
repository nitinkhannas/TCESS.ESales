using System;

namespace TCESS.ESales.DataTransferObjects
{
    public class PageInfoDTO
    {
        public int Page_Id
        {
            get;
            set;
        }

        public string Page_Name
        {
            get;
            set;
        }

        public string Page_NavigateURL
        {
            get;
            set;
        }

        public int Page_Level
        {
            get;
            set;
        }

        public int Page_ParentPageLevelId
        {
            get;
            set;
        }

        public bool Page_IsActive
        {
            get;
            set;
        }

        public int Page_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> Page_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> Page_LastUpdatedDate
        {
            get;
            set;
        }

        public bool Page_IsDeleted
        {
            get;
            set;
        }
    }
}