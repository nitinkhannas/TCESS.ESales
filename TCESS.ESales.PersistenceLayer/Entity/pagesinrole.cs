//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace TCESS.ESales.PersistenceLayer.Entity
{
    public partial class pagesinrole : EntityBase
    {
        #region Primitive Properties
    
        public virtual int Page_Role_Id
        {
            get;
            set;
        }
    
        public virtual int Page_Role_PageId
        {
            get { return _page_Role_PageId; }
            set
            {
                if (_page_Role_PageId != value)
                {
                    if (pageinfo != null && pageinfo.Page_Id != value)
                    {
                        pageinfo = null;
                    }
                    _page_Role_PageId = value;
                }
            }
        }
        private int _page_Role_PageId;
    
        public virtual int Page_Role_RoleId
        {
            get;
            set;
        }
    
        public virtual bool Page_Role_IsActive
        {
            get;
            set;
        }
    
        public virtual int Page_Role_CreatedBy
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> Page_Role_CreatedDate
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> Page_Role_LastUpdatedDate
        {
            get;
            set;
        }
    
        public virtual bool Page_Role_IsDeleted
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual pageinfo pageinfo
        {
            get { return _pageinfo; }
            set
            {
                if (!ReferenceEquals(_pageinfo, value))
                {
                    var previousValue = _pageinfo;
                    _pageinfo = value;
                    Fixuppageinfo(previousValue);
                }
            }
        }
        private pageinfo _pageinfo;

        #endregion
        #region Association Fixup
    
        private void Fixuppageinfo(pageinfo previousValue)
        {
            if (previousValue != null && previousValue.pagesinroles.Contains(this))
            {
                previousValue.pagesinroles.Remove(this);
            }
    
            if (pageinfo != null)
            {
                if (!pageinfo.pagesinroles.Contains(this))
                {
                    pageinfo.pagesinroles.Add(this);
                }
                if (Page_Role_PageId != pageinfo.Page_Id)
                {
                    Page_Role_PageId = pageinfo.Page_Id;
                }
            }
        }

        #endregion
    }
}
