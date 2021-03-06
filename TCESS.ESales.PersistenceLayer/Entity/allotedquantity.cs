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
    public partial class allotedquantity : EntityBase
    {
        #region Primitive Properties
    
        public virtual int Alloted_Id
        {
            get;
            set;
        }
    
        public virtual string Alloted_Quantity
        {
            get;
            set;
        }
    
        public virtual int Alloted_CreatedBy
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> Alloted_CreatedDate
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> Alloted_LastUpdatedDate
        {
            get;
            set;
        }
    
        public virtual bool Alloted_IsDeleted
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
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

        #endregion
        #region Association Fixup
    
        private void Fixupcustomermaterialmaps(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (customermaterialmap item in e.NewItems)
                {
                    item.allotedquantity = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (customermaterialmap item in e.OldItems)
                {
                    if (ReferenceEquals(item.allotedquantity, this))
                    {
                        item.allotedquantity = null;
                    }
                }
            }
        }

        #endregion
    }
}
