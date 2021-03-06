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
    public partial class paymentmode : EntityBase
    {
        #region Primitive Properties
    
        public virtual int Paymentmode_Id
        {
            get;
            set;
        }
    
        public virtual string Paymentmode_Name
        {
            get;
            set;
        }
    
        public virtual bool Paymentmode_IsGhatoCollection
        {
            get;
            set;
        }
    
        public virtual int Paymentmode_CreatedBy
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> Paymentmode_CreatedDate
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> Paymentmode_LastupdatedDate
        {
            get;
            set;
        }
    
        public virtual bool Paymentmode_IsDeleted
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<moneyreceipt> moneyreceipts
        {
            get
            {
                if (_moneyreceipts == null)
                {
                    var newCollection = new FixupCollection<moneyreceipt>();
                    newCollection.CollectionChanged += Fixupmoneyreceipts;
                    _moneyreceipts = newCollection;
                }
                return _moneyreceipts;
            }
            set
            {
                if (!ReferenceEquals(_moneyreceipts, value))
                {
                    var previousValue = _moneyreceipts as FixupCollection<moneyreceipt>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupmoneyreceipts;
                    }
                    _moneyreceipts = value;
                    var newValue = value as FixupCollection<moneyreceipt>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupmoneyreceipts;
                    }
                }
            }
        }
        private ICollection<moneyreceipt> _moneyreceipts;
    
        public virtual ICollection<paymentrefund> paymentrefunds
        {
            get
            {
                if (_paymentrefunds == null)
                {
                    var newCollection = new FixupCollection<paymentrefund>();
                    newCollection.CollectionChanged += Fixuppaymentrefunds;
                    _paymentrefunds = newCollection;
                }
                return _paymentrefunds;
            }
            set
            {
                if (!ReferenceEquals(_paymentrefunds, value))
                {
                    var previousValue = _paymentrefunds as FixupCollection<paymentrefund>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixuppaymentrefunds;
                    }
                    _paymentrefunds = value;
                    var newValue = value as FixupCollection<paymentrefund>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixuppaymentrefunds;
                    }
                }
            }
        }
        private ICollection<paymentrefund> _paymentrefunds;
    
        public virtual ICollection<userpaymentmodemapping> userpaymentmodemappings
        {
            get
            {
                if (_userpaymentmodemappings == null)
                {
                    var newCollection = new FixupCollection<userpaymentmodemapping>();
                    newCollection.CollectionChanged += Fixupuserpaymentmodemappings;
                    _userpaymentmodemappings = newCollection;
                }
                return _userpaymentmodemappings;
            }
            set
            {
                if (!ReferenceEquals(_userpaymentmodemappings, value))
                {
                    var previousValue = _userpaymentmodemappings as FixupCollection<userpaymentmodemapping>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupuserpaymentmodemappings;
                    }
                    _userpaymentmodemappings = value;
                    var newValue = value as FixupCollection<userpaymentmodemapping>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupuserpaymentmodemappings;
                    }
                }
            }
        }
        private ICollection<userpaymentmodemapping> _userpaymentmodemappings;
    
        public virtual ICollection<paymentcollection> paymentcollections
        {
            get
            {
                if (_paymentcollections == null)
                {
                    var newCollection = new FixupCollection<paymentcollection>();
                    newCollection.CollectionChanged += Fixuppaymentcollections;
                    _paymentcollections = newCollection;
                }
                return _paymentcollections;
            }
            set
            {
                if (!ReferenceEquals(_paymentcollections, value))
                {
                    var previousValue = _paymentcollections as FixupCollection<paymentcollection>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixuppaymentcollections;
                    }
                    _paymentcollections = value;
                    var newValue = value as FixupCollection<paymentcollection>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixuppaymentcollections;
                    }
                }
            }
        }
        private ICollection<paymentcollection> _paymentcollections;

        #endregion
        #region Association Fixup
    
        private void Fixupmoneyreceipts(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (moneyreceipt item in e.NewItems)
                {
                    item.paymentmode = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (moneyreceipt item in e.OldItems)
                {
                    if (ReferenceEquals(item.paymentmode, this))
                    {
                        item.paymentmode = null;
                    }
                }
            }
        }
    
        private void Fixuppaymentrefunds(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (paymentrefund item in e.NewItems)
                {
                    item.paymentmode = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (paymentrefund item in e.OldItems)
                {
                    if (ReferenceEquals(item.paymentmode, this))
                    {
                        item.paymentmode = null;
                    }
                }
            }
        }
    
        private void Fixupuserpaymentmodemappings(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (userpaymentmodemapping item in e.NewItems)
                {
                    item.paymentmode = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (userpaymentmodemapping item in e.OldItems)
                {
                    if (ReferenceEquals(item.paymentmode, this))
                    {
                        item.paymentmode = null;
                    }
                }
            }
        }
    
        private void Fixuppaymentcollections(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (paymentcollection item in e.NewItems)
                {
                    item.paymentmode = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (paymentcollection item in e.OldItems)
                {
                    if (ReferenceEquals(item.paymentmode, this))
                    {
                        item.paymentmode = null;
                    }
                }
            }
        }

        #endregion
    }
}
