using System;

namespace TCESS.ESales.DataTransferObjects
{
    public class PagesInRoleDTO : BaseDTO
    {
        #region Primitive Properties

        public int Page_Role_Id
        {
            get;
            set;
        }

        public int Page_Role_PageId
        {
            get;
            set;
        }

        public string Page_Role_PageName
        {
            get;
            set;
        }

        public int Page_Role_RoleId
        {
            get;
            set;
        }

        public bool Page_Role_IsActive
        {
            get;
            set;
        }

        public int Page_Role_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> Page_Role_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> Page_Role_LastUpdatedDate
        {
            get;
            set;
        }

        public bool Page_Role_IsDeleted
        {
            get;
            set;
        }

        #endregion        
    }
}