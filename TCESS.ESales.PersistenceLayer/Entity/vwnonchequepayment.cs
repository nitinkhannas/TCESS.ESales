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
    public partial class vwnonchequepayment : EntityBase
    {
        #region Primitive Properties
    
        public virtual Nullable<long> CounterId
        {
            get;
            set;
        }
    
        public virtual string CounterName
        {
            get;
            set;
        }
    
        public virtual decimal TotalAmount
        {
            get;
            set;
        }
    
        public virtual Nullable<decimal> TransferedAmount
        {
            get;
            set;
        }
    
        public virtual Nullable<decimal> InTransitAmount
        {
            get;
            set;
        }
    
        public virtual Nullable<decimal> CashInHand
        {
            get;
            set;
        }

        #endregion
    }
}
