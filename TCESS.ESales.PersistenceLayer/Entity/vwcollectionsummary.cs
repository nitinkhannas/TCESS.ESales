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
    public partial class vwcollectionsummary : EntityBase
    {
        #region Primitive Properties
    
        public virtual int Id
        {
            get;
            set;
        }
    
        public virtual Nullable<decimal> TotalAmount
        {
            get;
            set;
        }
    
        public virtual Nullable<decimal> ChequeCollected
        {
            get;
            set;
        }
    
        public virtual Nullable<decimal> DDCollected
        {
            get;
            set;
        }
    
        public virtual Nullable<decimal> RTGSCollected
        {
            get;
            set;
        }

        #endregion
    }
}
