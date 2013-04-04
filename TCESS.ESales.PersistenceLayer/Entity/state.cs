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
    public partial class state : EntityBase
    {
        #region Primitive Properties
    
        public virtual int State_Id
        {
            get;
            set;
        }
    
        public virtual string State_Name
        {
            get;
            set;
        }
    
        public virtual int State_CreatedBy
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> State_CreatedDate
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> State_LastUpdatedDate
        {
            get;
            set;
        }
    
        public virtual bool State_IsDeleted
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<agentdetail> agentdetails
        {
            get
            {
                if (_agentdetails == null)
                {
                    var newCollection = new FixupCollection<agentdetail>();
                    newCollection.CollectionChanged += Fixupagentdetails;
                    _agentdetails = newCollection;
                }
                return _agentdetails;
            }
            set
            {
                if (!ReferenceEquals(_agentdetails, value))
                {
                    var previousValue = _agentdetails as FixupCollection<agentdetail>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupagentdetails;
                    }
                    _agentdetails = value;
                    var newValue = value as FixupCollection<agentdetail>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupagentdetails;
                    }
                }
            }
        }
        private ICollection<agentdetail> _agentdetails;
    
        public virtual ICollection<customer> customers
        {
            get
            {
                if (_customers == null)
                {
                    var newCollection = new FixupCollection<customer>();
                    newCollection.CollectionChanged += Fixupcustomers;
                    _customers = newCollection;
                }
                return _customers;
            }
            set
            {
                if (!ReferenceEquals(_customers, value))
                {
                    var previousValue = _customers as FixupCollection<customer>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupcustomers;
                    }
                    _customers = value;
                    var newValue = value as FixupCollection<customer>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupcustomers;
                    }
                }
            }
        }
        private ICollection<customer> _customers;
    
        public virtual ICollection<district> districts
        {
            get
            {
                if (_districts == null)
                {
                    var newCollection = new FixupCollection<district>();
                    newCollection.CollectionChanged += Fixupdistricts;
                    _districts = newCollection;
                }
                return _districts;
            }
            set
            {
                if (!ReferenceEquals(_districts, value))
                {
                    var previousValue = _districts as FixupCollection<district>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupdistricts;
                    }
                    _districts = value;
                    var newValue = value as FixupCollection<district>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupdistricts;
                    }
                }
            }
        }
        private ICollection<district> _districts;
    
        public virtual ICollection<standalonetruck> standalonetrucks
        {
            get
            {
                if (_standalonetrucks == null)
                {
                    var newCollection = new FixupCollection<standalonetruck>();
                    newCollection.CollectionChanged += Fixupstandalonetrucks;
                    _standalonetrucks = newCollection;
                }
                return _standalonetrucks;
            }
            set
            {
                if (!ReferenceEquals(_standalonetrucks, value))
                {
                    var previousValue = _standalonetrucks as FixupCollection<standalonetruck>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupstandalonetrucks;
                    }
                    _standalonetrucks = value;
                    var newValue = value as FixupCollection<standalonetruck>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupstandalonetrucks;
                    }
                }
            }
        }
        private ICollection<standalonetruck> _standalonetrucks;
    
        public virtual ICollection<truckdetail> truckdetails
        {
            get
            {
                if (_truckdetails == null)
                {
                    var newCollection = new FixupCollection<truckdetail>();
                    newCollection.CollectionChanged += Fixuptruckdetails;
                    _truckdetails = newCollection;
                }
                return _truckdetails;
            }
            set
            {
                if (!ReferenceEquals(_truckdetails, value))
                {
                    var previousValue = _truckdetails as FixupCollection<truckdetail>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixuptruckdetails;
                    }
                    _truckdetails = value;
                    var newValue = value as FixupCollection<truckdetail>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixuptruckdetails;
                    }
                }
            }
        }
        private ICollection<truckdetail> _truckdetails;

        #endregion
        #region Association Fixup
    
        private void Fixupagentdetails(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (agentdetail item in e.NewItems)
                {
                    item.state = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (agentdetail item in e.OldItems)
                {
                    if (ReferenceEquals(item.state, this))
                    {
                        item.state = null;
                    }
                }
            }
        }
    
        private void Fixupcustomers(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (customer item in e.NewItems)
                {
                    item.state = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (customer item in e.OldItems)
                {
                    if (ReferenceEquals(item.state, this))
                    {
                        item.state = null;
                    }
                }
            }
        }
    
        private void Fixupdistricts(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (district item in e.NewItems)
                {
                    item.state = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (district item in e.OldItems)
                {
                    if (ReferenceEquals(item.state, this))
                    {
                        item.state = null;
                    }
                }
            }
        }
    
        private void Fixupstandalonetrucks(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (standalonetruck item in e.NewItems)
                {
                    item.state = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (standalonetruck item in e.OldItems)
                {
                    if (ReferenceEquals(item.state, this))
                    {
                        item.state = null;
                    }
                }
            }
        }
    
        private void Fixuptruckdetails(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (truckdetail item in e.NewItems)
                {
                    item.state = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (truckdetail item in e.OldItems)
                {
                    if (ReferenceEquals(item.state, this))
                    {
                        item.state = null;
                    }
                }
            }
        }

        #endregion
    }
}
