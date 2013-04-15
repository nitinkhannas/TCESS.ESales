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
    public partial class counter : EntityBase
    {
        #region Primitive Properties
    
        public virtual int Counter_Id
        {
            get;
            set;
        }
    
        public virtual string Counter_Name
        {
            get;
            set;
        }
    
        public virtual Nullable<int> Counter_Agent_Id
        {
            get { return _counter_Agent_Id; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_counter_Agent_Id != value)
                    {
                        if (agentdetail != null && agentdetail.Agent_Id != value)
                        {
                            agentdetail = null;
                        }
                        _counter_Agent_Id = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<int> _counter_Agent_Id;
    
        public virtual Nullable<int> Counter_User_Id
        {
            get { return _counter_User_Id; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_counter_User_Id != value)
                    {
                        if (my_aspnet_users != null && my_aspnet_users.id != value)
                        {
                            my_aspnet_users = null;
                        }
                        _counter_User_Id = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<int> _counter_User_Id;
    
        public virtual string Counter_MAC_Id
        {
            get;
            set;
        }
    
        public virtual bool Counter_IsActive
        {
            get;
            set;
        }
    
        public virtual bool Counter_IsPayment
        {
            get;
            set;
        }
    
        public virtual int Counter_CreatedBy
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> Counter_CreatedDate
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> Counter_LastUpdatedDate
        {
            get;
            set;
        }
    
        public virtual bool Counter_IsDeleted
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual agentdetail agentdetail
        {
            get { return _agentdetail; }
            set
            {
                if (!ReferenceEquals(_agentdetail, value))
                {
                    var previousValue = _agentdetail;
                    _agentdetail = value;
                    Fixupagentdetail(previousValue);
                }
            }
        }
        private agentdetail _agentdetail;
    
        public virtual ICollection<booking> bookings
        {
            get
            {
                if (_bookings == null)
                {
                    var newCollection = new FixupCollection<booking>();
                    newCollection.CollectionChanged += Fixupbookings;
                    _bookings = newCollection;
                }
                return _bookings;
            }
            set
            {
                if (!ReferenceEquals(_bookings, value))
                {
                    var previousValue = _bookings as FixupCollection<booking>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupbookings;
                    }
                    _bookings = value;
                    var newValue = value as FixupCollection<booking>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupbookings;
                    }
                }
            }
        }
        private ICollection<booking> _bookings;
    
        public virtual my_aspnet_users my_aspnet_users
        {
            get { return _my_aspnet_users; }
            set
            {
                if (!ReferenceEquals(_my_aspnet_users, value))
                {
                    var previousValue = _my_aspnet_users;
                    _my_aspnet_users = value;
                    Fixupmy_aspnet_users(previousValue);
                }
            }
        }
        private my_aspnet_users _my_aspnet_users;
    
        public virtual ICollection<counterdetail> counterdetails
        {
            get
            {
                if (_counterdetails == null)
                {
                    var newCollection = new FixupCollection<counterdetail>();
                    newCollection.CollectionChanged += Fixupcounterdetails;
                    _counterdetails = newCollection;
                }
                return _counterdetails;
            }
            set
            {
                if (!ReferenceEquals(_counterdetails, value))
                {
                    var previousValue = _counterdetails as FixupCollection<counterdetail>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupcounterdetails;
                    }
                    _counterdetails = value;
                    var newValue = value as FixupCollection<counterdetail>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupcounterdetails;
                    }
                }
            }
        }
        private ICollection<counterdetail> _counterdetails;

        #endregion
        #region Association Fixup
    
        private bool _settingFK = false;
    
        private void Fixupagentdetail(agentdetail previousValue)
        {
            if (previousValue != null && previousValue.counters.Contains(this))
            {
                previousValue.counters.Remove(this);
            }
    
            if (agentdetail != null)
            {
                if (!agentdetail.counters.Contains(this))
                {
                    agentdetail.counters.Add(this);
                }
                if (Counter_Agent_Id != agentdetail.Agent_Id)
                {
                    Counter_Agent_Id = agentdetail.Agent_Id;
                }
            }
            else if (!_settingFK)
            {
                Counter_Agent_Id = null;
            }
        }
    
        private void Fixupmy_aspnet_users(my_aspnet_users previousValue)
        {
            if (previousValue != null && previousValue.counters.Contains(this))
            {
                previousValue.counters.Remove(this);
            }
    
            if (my_aspnet_users != null)
            {
                if (!my_aspnet_users.counters.Contains(this))
                {
                    my_aspnet_users.counters.Add(this);
                }
                if (Counter_User_Id != my_aspnet_users.id)
                {
                    Counter_User_Id = my_aspnet_users.id;
                }
            }
            else if (!_settingFK)
            {
                Counter_User_Id = null;
            }
        }
    
        private void Fixupbookings(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (booking item in e.NewItems)
                {
                    item.counter = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (booking item in e.OldItems)
                {
                    if (ReferenceEquals(item.counter, this))
                    {
                        item.counter = null;
                    }
                }
            }
        }
    
        private void Fixupcounterdetails(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (counterdetail item in e.NewItems)
                {
                    item.counter = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (counterdetail item in e.OldItems)
                {
                    if (ReferenceEquals(item.counter, this))
                    {
                        item.counter = null;
                    }
                }
            }
        }

        #endregion
    }
}
