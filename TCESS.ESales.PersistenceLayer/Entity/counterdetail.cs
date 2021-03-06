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
    public partial class counterdetail : EntityBase
    {
        #region Primitive Properties
    
        public virtual int CounterDetail_Id
        {
            get;
            set;
        }
    
        public virtual int CounterDetail_Counter_ID
        {
            get { return _counterDetail_Counter_ID; }
            set
            {
                if (_counterDetail_Counter_ID != value)
                {
                    if (counter != null && counter.Counter_Id != value)
                    {
                        counter = null;
                    }
                    _counterDetail_Counter_ID = value;
                }
            }
        }
        private int _counterDetail_Counter_ID;
    
        public virtual int CounterDetail_Agent_Id
        {
            get { return _counterDetail_Agent_Id; }
            set
            {
                if (_counterDetail_Agent_Id != value)
                {
                    if (agentdetail != null && agentdetail.Agent_Id != value)
                    {
                        agentdetail = null;
                    }
                    _counterDetail_Agent_Id = value;
                }
            }
        }
        private int _counterDetail_Agent_Id;
    
        public virtual System.DateTime CounterDetail_Date
        {
            get;
            set;
        }
    
        public virtual int CounterDetail_Count
        {
            get;
            set;
        }
    
        public virtual Nullable<int> CounterDetail_CreatedBy
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> CounterDetail_CreatedDate
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> CounterDetail_LastupdatedDate
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
    
        public virtual counter counter
        {
            get { return _counter; }
            set
            {
                if (!ReferenceEquals(_counter, value))
                {
                    var previousValue = _counter;
                    _counter = value;
                    Fixupcounter(previousValue);
                }
            }
        }
        private counter _counter;

        #endregion
        #region Association Fixup
    
        private void Fixupagentdetail(agentdetail previousValue)
        {
            if (previousValue != null && previousValue.counterdetails.Contains(this))
            {
                previousValue.counterdetails.Remove(this);
            }
    
            if (agentdetail != null)
            {
                if (!agentdetail.counterdetails.Contains(this))
                {
                    agentdetail.counterdetails.Add(this);
                }
                if (CounterDetail_Agent_Id != agentdetail.Agent_Id)
                {
                    CounterDetail_Agent_Id = agentdetail.Agent_Id;
                }
            }
        }
    
        private void Fixupcounter(counter previousValue)
        {
            if (previousValue != null && previousValue.counterdetails.Contains(this))
            {
                previousValue.counterdetails.Remove(this);
            }
    
            if (counter != null)
            {
                if (!counter.counterdetails.Contains(this))
                {
                    counter.counterdetails.Add(this);
                }
                if (CounterDetail_Counter_ID != counter.Counter_Id)
                {
                    CounterDetail_Counter_ID = counter.Counter_Id;
                }
            }
        }

        #endregion
    }
}
