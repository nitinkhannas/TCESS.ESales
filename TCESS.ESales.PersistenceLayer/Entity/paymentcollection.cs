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
    public partial class paymentcollection : EntityBase
    {
        #region Primitive Properties
    
        public virtual int PC_Id
        {
            get;
            set;
        }
    
        public virtual int PC_CustId
        {
            get { return _pC_CustId; }
            set
            {
                if (_pC_CustId != value)
                {
                    if (customer != null && customer.Cust_Id != value)
                    {
                        customer = null;
                    }
                    _pC_CustId = value;
                }
            }
        }
        private int _pC_CustId;
    
        public virtual string PC_ReceiptNo
        {
            get;
            set;
        }
    
        public virtual System.DateTime PC_ReceiptDate
        {
            get;
            set;
        }
    
        public virtual int PC_PaymentMode
        {
            get { return _pC_PaymentMode; }
            set
            {
                if (_pC_PaymentMode != value)
                {
                    if (paymentmode != null && paymentmode.Paymentmode_Id != value)
                    {
                        paymentmode = null;
                    }
                    _pC_PaymentMode = value;
                }
            }
        }
        private int _pC_PaymentMode;
    
        public virtual string PC_InstrumentNo
        {
            get;
            set;
        }
    
        public virtual decimal PC_Amount
        {
            get;
            set;
        }
    
        public virtual Nullable<int> PC_BankDrawn
        {
            get;
            set;
        }
    
        public virtual string PC_BankBranch
        {
            get;
            set;
        }
    
        public virtual string PC_BankIFSCCode
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> PC_InstrumentDate
        {
            get;
            set;
        }
    
        public virtual string PC_PayerName
        {
            get;
            set;
        }
    
        public virtual string PC_MobileNumber
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> PC_InstrumentRealizationDate
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> PC_DateOfCredit
        {
            get;
            set;
        }
    
        public virtual Nullable<int> PC_CompanyBankName
        {
            get;
            set;
        }
    
        public virtual Nullable<decimal> PC_PreviousAmount
        {
            get;
            set;
        }
    
        public virtual Nullable<int> PC_InstrumentStatus
        {
            get;
            set;
        }
    
        public virtual Nullable<int> PC_Status
        {
            get;
            set;
        }
    
        public virtual Nullable<int> PC_OldReceiptId
        {
            get;
            set;
        }
    
        public virtual Nullable<int> PC_NewReceiptId
        {
            get;
            set;
        }
    
        public virtual int PC_ReprintCount
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> PC_LastPrintDate
        {
            get;
            set;
        }
    
        public virtual string PC_Remark
        {
            get;
            set;
        }
    
        public virtual Nullable<int> PC_CreatedBy
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> PC_CreatedDate
        {
            get;
            set;
        }
    
        public virtual Nullable<int> PC_LastUpdatedBy
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> PC_LastUpdateDate
        {
            get;
            set;
        }
    
        public virtual bool PC_IsDeleted
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
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
    
        public virtual ICollection<paymenttransit> paymenttransits
        {
            get
            {
                if (_paymenttransits == null)
                {
                    var newCollection = new FixupCollection<paymenttransit>();
                    newCollection.CollectionChanged += Fixuppaymenttransits;
                    _paymenttransits = newCollection;
                }
                return _paymenttransits;
            }
            set
            {
                if (!ReferenceEquals(_paymenttransits, value))
                {
                    var previousValue = _paymenttransits as FixupCollection<paymenttransit>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixuppaymenttransits;
                    }
                    _paymenttransits = value;
                    var newValue = value as FixupCollection<paymenttransit>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixuppaymenttransits;
                    }
                }
            }
        }
        private ICollection<paymenttransit> _paymenttransits;
    
        public virtual paymentmode paymentmode
        {
            get { return _paymentmode; }
            set
            {
                if (!ReferenceEquals(_paymentmode, value))
                {
                    var previousValue = _paymentmode;
                    _paymentmode = value;
                    Fixuppaymentmode(previousValue);
                }
            }
        }
        private paymentmode _paymentmode;
    
        public virtual ICollection<smspaymentregistration> smspaymentregistrations
        {
            get
            {
                if (_smspaymentregistrations == null)
                {
                    var newCollection = new FixupCollection<smspaymentregistration>();
                    newCollection.CollectionChanged += Fixupsmspaymentregistrations;
                    _smspaymentregistrations = newCollection;
                }
                return _smspaymentregistrations;
            }
            set
            {
                if (!ReferenceEquals(_smspaymentregistrations, value))
                {
                    var previousValue = _smspaymentregistrations as FixupCollection<smspaymentregistration>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupsmspaymentregistrations;
                    }
                    _smspaymentregistrations = value;
                    var newValue = value as FixupCollection<smspaymentregistration>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupsmspaymentregistrations;
                    }
                }
            }
        }
        private ICollection<smspaymentregistration> _smspaymentregistrations;

        #endregion
        #region Association Fixup
    
        private void Fixupcustomer(customer previousValue)
        {
            if (previousValue != null && previousValue.paymentcollections.Contains(this))
            {
                previousValue.paymentcollections.Remove(this);
            }
    
            if (customer != null)
            {
                if (!customer.paymentcollections.Contains(this))
                {
                    customer.paymentcollections.Add(this);
                }
                if (PC_CustId != customer.Cust_Id)
                {
                    PC_CustId = customer.Cust_Id;
                }
            }
        }
    
        private void Fixuppaymentmode(paymentmode previousValue)
        {
            if (previousValue != null && previousValue.paymentcollections.Contains(this))
            {
                previousValue.paymentcollections.Remove(this);
            }
    
            if (paymentmode != null)
            {
                if (!paymentmode.paymentcollections.Contains(this))
                {
                    paymentmode.paymentcollections.Add(this);
                }
                if (PC_PaymentMode != paymentmode.Paymentmode_Id)
                {
                    PC_PaymentMode = paymentmode.Paymentmode_Id;
                }
            }
        }
    
        private void Fixuppaymenttransits(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (paymenttransit item in e.NewItems)
                {
                    item.paymentcollection = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (paymenttransit item in e.OldItems)
                {
                    if (ReferenceEquals(item.paymentcollection, this))
                    {
                        item.paymentcollection = null;
                    }
                }
            }
        }
    
        private void Fixupsmspaymentregistrations(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (smspaymentregistration item in e.NewItems)
                {
                    item.paymentcollection = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (smspaymentregistration item in e.OldItems)
                {
                    if (ReferenceEquals(item.paymentcollection, this))
                    {
                        item.paymentcollection = null;
                    }
                }
            }
        }

        #endregion
    }
}
