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
    public partial class roadpermitreport : EntityBase
    {
        #region Primitive Properties
    
        public virtual Nullable<System.DateTime> SettlementDate
        {
            get;
            set;
        }
    
        public virtual string RoadPermitNo
        {
            get;
            set;
        }
    
        public virtual string CustomerName
        {
            get;
            set;
        }
    
        public virtual string CustomerAddress
        {
            get;
            set;
        }
    
        public virtual string District
        {
            get;
            set;
        }
    
        public virtual string TSLInvoiceNo
        {
            get;
            set;
        }
    
        public virtual decimal QuantityLifted
        {
            get;
            set;
        }
    
        public virtual string TruckNo
        {
            get;
            set;
        }

        #endregion
    }
}
