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
    public partial class standalonetruckdocdetail : EntityBase
    {
        #region Primitive Properties
    
        public virtual int StandaloneTruck_Doc_Id
        {
            get;
            set;
        }
    
        public virtual int StandaloneTruck_Doc_TruckId
        {
            get { return _standaloneTruck_Doc_TruckId; }
            set
            {
                if (_standaloneTruck_Doc_TruckId != value)
                {
                    if (standalonetruck != null && standalonetruck.StandaloneTruck_Id != value)
                    {
                        standalonetruck = null;
                    }
                    _standaloneTruck_Doc_TruckId = value;
                }
            }
        }
        private int _standaloneTruck_Doc_TruckId;
    
        public virtual int StandaloneTruck_Doc_DocId
        {
            get { return _standaloneTruck_Doc_DocId; }
            set
            {
                if (_standaloneTruck_Doc_DocId != value)
                {
                    if (doctype != null && doctype.Doc_Id != value)
                    {
                        doctype = null;
                    }
                    _standaloneTruck_Doc_DocId = value;
                }
            }
        }
        private int _standaloneTruck_Doc_DocId;
    
        public virtual string StandaloneTruck_Doc_DocNo
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> StandaloneTruck_Doc_ExDate
        {
            get;
            set;
        }
    
        public virtual byte[] StandaloneTruck_Doc_File
        {
            get;
            set;
        }
    
        public virtual string StandaloneTruck_Doc_FileName
        {
            get;
            set;
        }
    
        public virtual int StandaloneTruck_Doc_CreatedBy
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> StandaloneTruck_Doc_CreatedDate
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> StandaloneTruck_Doc_LastUpdatedDate
        {
            get;
            set;
        }
    
        public virtual bool StandaloneTruck_Doc_IsDeleted
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual standalonetruck standalonetruck
        {
            get { return _standalonetruck; }
            set
            {
                if (!ReferenceEquals(_standalonetruck, value))
                {
                    var previousValue = _standalonetruck;
                    _standalonetruck = value;
                    Fixupstandalonetruck(previousValue);
                }
            }
        }
        private standalonetruck _standalonetruck;
    
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
    
        private void Fixupstandalonetruck(standalonetruck previousValue)
        {
            if (previousValue != null && previousValue.standalonetruckdocdetails.Contains(this))
            {
                previousValue.standalonetruckdocdetails.Remove(this);
            }
    
            if (standalonetruck != null)
            {
                if (!standalonetruck.standalonetruckdocdetails.Contains(this))
                {
                    standalonetruck.standalonetruckdocdetails.Add(this);
                }
                if (StandaloneTruck_Doc_TruckId != standalonetruck.StandaloneTruck_Id)
                {
                    StandaloneTruck_Doc_TruckId = standalonetruck.StandaloneTruck_Id;
                }
            }
        }
    
        private void Fixupdoctype(doctype previousValue)
        {
            if (previousValue != null && previousValue.standalonetruckdocdetails.Contains(this))
            {
                previousValue.standalonetruckdocdetails.Remove(this);
            }
    
            if (doctype != null)
            {
                if (!doctype.standalonetruckdocdetails.Contains(this))
                {
                    doctype.standalonetruckdocdetails.Add(this);
                }
                if (StandaloneTruck_Doc_DocId != doctype.Doc_Id)
                {
                    StandaloneTruck_Doc_DocId = doctype.Doc_Id;
                }
            }
        }

        #endregion
    }
}
