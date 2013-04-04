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
    public partial class customerdocdetail : EntityBase
    {
        #region Primitive Properties
    
        public virtual int Cust_Doc_Id
        {
            get;
            set;
        }
    
        public virtual int Cust_Doc_CustId
        {
            get { return _cust_Doc_CustId; }
            set
            {
                if (_cust_Doc_CustId != value)
                {
                    if (customer != null && customer.Cust_Id != value)
                    {
                        customer = null;
                    }
                    _cust_Doc_CustId = value;
                }
            }
        }
        private int _cust_Doc_CustId;
    
        public virtual int Cust_Doc_DocId
        {
            get { return _cust_Doc_DocId; }
            set
            {
                if (_cust_Doc_DocId != value)
                {
                    if (doctype != null && doctype.Doc_Id != value)
                    {
                        doctype = null;
                    }
                    _cust_Doc_DocId = value;
                }
            }
        }
        private int _cust_Doc_DocId;
    
        public virtual string Cust_Doc_No
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> Cust_Doc_ExDate
        {
            get;
            set;
        }
    
        public virtual string Cust_Doc_FileName
        {
            get;
            set;
        }
    
        public virtual int Cust_Doc_CreatedBy
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> Cust_Doc_CreatedDate
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> Cust_Doc_LastUpdatedDate
        {
            get;
            set;
        }
    
        public virtual bool Cust_Doc_IsDeleted
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
    
        public virtual doctype doctype
        {
            get { return _doctype; }
            set
            {
                if (!ReferenceEquals(_doctype, value))
                {
                    var previousValue = _doctype;
                    _doctype = value;
                    Fixupdoctype(previousValue);
                }
            }
        }
        private doctype _doctype;

        #endregion
        #region Association Fixup
    
        private void Fixupcustomer(customer previousValue)
        {
            if (previousValue != null && previousValue.customerdocdetails.Contains(this))
            {
                previousValue.customerdocdetails.Remove(this);
            }
    
            if (customer != null)
            {
                if (!customer.customerdocdetails.Contains(this))
                {
                    customer.customerdocdetails.Add(this);
                }
                if (Cust_Doc_CustId != customer.Cust_Id)
                {
                    Cust_Doc_CustId = customer.Cust_Id;
                }
            }
        }
    
        private void Fixupdoctype(doctype previousValue)
        {
            if (previousValue != null && previousValue.customerdocdetails.Contains(this))
            {
                previousValue.customerdocdetails.Remove(this);
            }
    
            if (doctype != null)
            {
                if (!doctype.customerdocdetails.Contains(this))
                {
                    doctype.customerdocdetails.Add(this);
                }
                if (Cust_Doc_DocId != doctype.Doc_Id)
                {
                    Cust_Doc_DocId = doctype.Doc_Id;
                }
            }
        }

        #endregion
    }
}
