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
    public partial class customermaterialmap : EntityBase
    {
        #region Primitive Properties
    
        public virtual int Cust_Mat_Id
        {
            get;
            set;
        }
    
        public virtual int Cust_Mat_CustId
        {
            get { return _cust_Mat_CustId; }
            set
            {
                if (_cust_Mat_CustId != value)
                {
                    if (customer != null && customer.Cust_Id != value)
                    {
                        customer = null;
                    }
                    _cust_Mat_CustId = value;
                }
            }
        }
        private int _cust_Mat_CustId;
    
        public virtual int Cust_Mat_MaterialId
        {
            get { return _cust_Mat_MaterialId; }
            set
            {
                if (_cust_Mat_MaterialId != value)
                {
                    if (materialtype != null && materialtype.MaterialType_Id != value)
                    {
                        materialtype = null;
                    }
                    _cust_Mat_MaterialId = value;
                }
            }
        }
        private int _cust_Mat_MaterialId;
    
        public virtual int Cust_Mat_AnnualRequirement
        {
            get;
            set;
        }
    
        public virtual int Cust_Mat_AllotedQuantityId
        {
            get { return _cust_Mat_AllotedQuantityId; }
            set
            {
                if (_cust_Mat_AllotedQuantityId != value)
                {
                    if (allotedquantity != null && allotedquantity.Alloted_Id != value)
                    {
                        allotedquantity = null;
                    }
                    _cust_Mat_AllotedQuantityId = value;
                }
            }
        }
        private int _cust_Mat_AllotedQuantityId;
    
        public virtual int Cust_Mat_LiftingLimit
        {
            get;
            set;
        }
    
        public virtual int Cust_Mat_CreatedBy
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> Cust_Mat_CreatedDate
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> Cust_Mat_LastUpdatedDate
        {
            get;
            set;
        }
    
        public virtual bool Cust_Mat_IsDeleted
        {
            get;
            set;
        }
    
        public virtual Nullable<int> Cust_Mat_Timeinterval
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual allotedquantity allotedquantity
        {
            get { return _allotedquantity; }
            set
            {
                if (!ReferenceEquals(_allotedquantity, value))
                {
                    var previousValue = _allotedquantity;
                    _allotedquantity = value;
                    Fixupallotedquantity(previousValue);
                }
            }
        }
        private allotedquantity _allotedquantity;
    
        public virtual customer customer
        {
            get { return _customer; }
            set
            {
                if (!ReferenceEquals(_customer, value))
                {
                    var previousValue = _customer;
                    _customer = value;
                    Fixupcustomer(previousValue);
                }
            }
        }
        private customer _customer;
    
        public virtual materialtype materialtype
        {
            get { return _materialtype; }
            set
            {
                if (!ReferenceEquals(_materialtype, value))
                {
                    var previousValue = _materialtype;
                    _materialtype = value;
                    Fixupmaterialtype(previousValue);
                }
            }
        }
        private materialtype _materialtype;

        #endregion
        #region Association Fixup
    
        private void Fixupallotedquantity(allotedquantity previousValue)
        {
            if (previousValue != null && previousValue.customermaterialmaps.Contains(this))
            {
                previousValue.customermaterialmaps.Remove(this);
            }
    
            if (allotedquantity != null)
            {
                if (!allotedquantity.customermaterialmaps.Contains(this))
                {
                    allotedquantity.customermaterialmaps.Add(this);
                }
                if (Cust_Mat_AllotedQuantityId != allotedquantity.Alloted_Id)
                {
                    Cust_Mat_AllotedQuantityId = allotedquantity.Alloted_Id;
                }
            }
        }
    
        private void Fixupcustomer(customer previousValue)
        {
            if (previousValue != null && previousValue.customermaterialmaps.Contains(this))
            {
                previousValue.customermaterialmaps.Remove(this);
            }
    
            if (customer != null)
            {
                if (!customer.customermaterialmaps.Contains(this))
                {
                    customer.customermaterialmaps.Add(this);
                }
                if (Cust_Mat_CustId != customer.Cust_Id)
                {
                    Cust_Mat_CustId = customer.Cust_Id;
                }
            }
        }
    
        private void Fixupmaterialtype(materialtype previousValue)
        {
            if (previousValue != null && previousValue.customermaterialmaps.Contains(this))
            {
                previousValue.customermaterialmaps.Remove(this);
            }
    
            if (materialtype != null)
            {
                if (!materialtype.customermaterialmaps.Contains(this))
                {
                    materialtype.customermaterialmaps.Add(this);
                }
                if (Cust_Mat_MaterialId != materialtype.MaterialType_Id)
                {
                    Cust_Mat_MaterialId = materialtype.MaterialType_Id;
                }
            }
        }

        #endregion
    }
}
