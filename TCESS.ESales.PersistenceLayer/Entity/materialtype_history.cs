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
    public partial class materialtype_history : EntityBase
    {
        #region Primitive Properties
    
        public virtual int MaterialType_Id
        {
            get;
            set;
        }
    
        public virtual string MaterialType_Code
        {
            get;
            set;
        }
    
        public virtual string MaterialType_Name
        {
            get;
            set;
        }
    
        public virtual decimal MaterialType_TiscoRate
        {
            get;
            set;
        }
    
        public virtual decimal MaterialType_CSTRate
        {
            get;
            set;
        }
    
        public virtual decimal MaterialType_CFormRate
        {
            get;
            set;
        }
    
        public virtual decimal MaterialType_HandlingRate
        {
            get;
            set;
        }
    
        public virtual decimal MaterialType_ServiceTax
        {
            get;
            set;
        }
    
        public virtual decimal MaterialType_EducationCess
        {
            get;
            set;
        }
    
        public virtual decimal MaterialType_HigherEducationCess
        {
            get;
            set;
        }
    
        public virtual bool MaterialType_IsActive
        {
            get;
            set;
        }
    
        public virtual int MaterialType_CreatedBy
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> MaterialType_CreatedDate
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> MaterialType_LastUpdatedDate
        {
            get;
            set;
        }
    
        public virtual bool MaterialType_IsDeleted
        {
            get;
            set;
        }

        #endregion
    }
}
