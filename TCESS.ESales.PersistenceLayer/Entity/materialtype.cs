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
    public partial class materialtype : EntityBase
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
        #region Navigation Properties
    
        public virtual ICollection<agentmaterialpercentage> agentmaterialpercentages
        {
            get
            {
                if (_agentmaterialpercentages == null)
                {
                    var newCollection = new FixupCollection<agentmaterialpercentage>();
                    newCollection.CollectionChanged += Fixupagentmaterialpercentages;
                    _agentmaterialpercentages = newCollection;
                }
                return _agentmaterialpercentages;
            }
            set
            {
                if (!ReferenceEquals(_agentmaterialpercentages, value))
                {
                    var previousValue = _agentmaterialpercentages as FixupCollection<agentmaterialpercentage>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupagentmaterialpercentages;
                    }
                    _agentmaterialpercentages = value;
                    var newValue = value as FixupCollection<agentmaterialpercentage>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupagentmaterialpercentages;
                    }
                }
            }
        }
        private ICollection<agentmaterialpercentage> _agentmaterialpercentages;
    
        public virtual ICollection<customermaterialmap> customermaterialmaps
        {
            get
            {
                if (_customermaterialmaps == null)
                {
                    var newCollection = new FixupCollection<customermaterialmap>();
                    newCollection.CollectionChanged += Fixupcustomermaterialmaps;
                    _customermaterialmaps = newCollection;
                }
                return _customermaterialmaps;
            }
            set
            {
                if (!ReferenceEquals(_customermaterialmaps, value))
                {
                    var previousValue = _customermaterialmaps as FixupCollection<customermaterialmap>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupcustomermaterialmaps;
                    }
                    _customermaterialmaps = value;
                    var newValue = value as FixupCollection<customermaterialmap>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupcustomermaterialmaps;
                    }
                }
            }
        }
        private ICollection<customermaterialmap> _customermaterialmaps;
    
        public virtual ICollection<dcamaterialallocation> dcamaterialallocations
        {
            get
            {
                if (_dcamaterialallocations == null)
                {
                    var newCollection = new FixupCollection<dcamaterialallocation>();
                    newCollection.CollectionChanged += Fixupdcamaterialallocations;
                    _dcamaterialallocations = newCollection;
                }
                return _dcamaterialallocations;
            }
            set
            {
                if (!ReferenceEquals(_dcamaterialallocations, value))
                {
                    var previousValue = _dcamaterialallocations as FixupCollection<dcamaterialallocation>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupdcamaterialallocations;
                    }
                    _dcamaterialallocations = value;
                    var newValue = value as FixupCollection<dcamaterialallocation>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupdcamaterialallocations;
                    }
                }
            }
        }
        private ICollection<dcamaterialallocation> _dcamaterialallocations;
    
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

        #endregion
        #region Association Fixup
    
        private void Fixupagentmaterialpercentages(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (agentmaterialpercentage item in e.NewItems)
                {
                    item.materialtype = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (agentmaterialpercentage item in e.OldItems)
                {
                    if (ReferenceEquals(item.materialtype, this))
                    {
                        item.materialtype = null;
                    }
                }
            }
        }
    
        private void Fixupcustomermaterialmaps(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (customermaterialmap item in e.NewItems)
                {
                    item.materialtype = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (customermaterialmap item in e.OldItems)
                {
                    if (ReferenceEquals(item.materialtype, this))
                    {
                        item.materialtype = null;
                    }
                }
            }
        }
    
        private void Fixupdcamaterialallocations(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (dcamaterialallocation item in e.NewItems)
                {
                    item.materialtype = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (dcamaterialallocation item in e.OldItems)
                {
                    if (ReferenceEquals(item.materialtype, this))
                    {
                        item.materialtype = null;
                    }
                }
            }
        }
    
        private void Fixupbookings(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (booking item in e.NewItems)
                {
                    item.materialtype = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (booking item in e.OldItems)
                {
                    if (ReferenceEquals(item.materialtype, this))
                    {
                        item.materialtype = null;
                    }
                }
            }
        }

        #endregion
    }
}
