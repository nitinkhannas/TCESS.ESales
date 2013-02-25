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
    public partial class moneyreceipt : EntityBase
    {
        #region Primitive Properties
    
        public virtual int MoneyReceipt_Id
        {
            get;
            set;
        }
    
        public virtual int MoneyReceipt_Booking_Id
        {
            get { return _moneyReceipt_Booking_Id; }
            set
            {
                if (_moneyReceipt_Booking_Id != value)
                {
                    if (booking != null && booking.Booking_Id != value)
                    {
                        booking = null;
                    }
                    _moneyReceipt_Booking_Id = value;
                }
            }
        }
        private int _moneyReceipt_Booking_Id;
    
        public virtual decimal MoneyReceipt_AmountPaid
        {
            get;
            set;
        }
    
        public virtual int MoneyReceipt_PaymentmodeId
        {
            get { return _moneyReceipt_PaymentmodeId; }
            set
            {
                if (_moneyReceipt_PaymentmodeId != value)
                {
                    if (paymentmode != null && paymentmode.Paymentmode_Id != value)
                    {
                        paymentmode = null;
                    }
                    _moneyReceipt_PaymentmodeId = value;
                }
            }
        }
        private int _moneyReceipt_PaymentmodeId;
    
        public virtual string MoneyReceipt_InstrumentNo
        {
            get;
            set;
        }
    
        public virtual string MoneyReceipt_AccountName
        {
            get;
            set;
        }
    
        public virtual string MoneyReceipt_Remark
        {
            get;
            set;
        }
    
        public virtual Nullable<decimal> MoneyReceipt_RefundAmount
        {
            get;
            set;
        }
    
        public virtual string MoneyReceipt_CancelReceiptNo
        {
            get;
            set;
        }
    
        public virtual string MoneyReceipt_CancellationRemarks
        {
            get;
            set;
        }
    
        public virtual int MoneyReceipt_CreatedBy
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> MoneyReceipt_CreateDate
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> MoneyReceipt_LastUpdateDare
        {
            get;
            set;
        }
    
        public virtual bool MoneyReceipt_IsDeleted
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual booking booking
        {
            get { return _booking; }
            set
            {
                if (!ReferenceEquals(_booking, value))
                {
                    var previousValue = _booking;
                    _booking = value;
                    Fixupbooking(previousValue);
                }
            }
        }
        private booking _booking;
    
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

        #endregion
        #region Association Fixup
    
        private void Fixupbooking(booking previousValue)
        {
            if (previousValue != null && previousValue.moneyreceipts.Contains(this))
            {
                previousValue.moneyreceipts.Remove(this);
            }
    
            if (booking != null)
            {
                if (!booking.moneyreceipts.Contains(this))
                {
                    booking.moneyreceipts.Add(this);
                }
                if (MoneyReceipt_Booking_Id != booking.Booking_Id)
                {
                    MoneyReceipt_Booking_Id = booking.Booking_Id;
                }
            }
        }
    
        private void Fixuppaymentmode(paymentmode previousValue)
        {
            if (previousValue != null && previousValue.moneyreceipts.Contains(this))
            {
                previousValue.moneyreceipts.Remove(this);
            }
    
            if (paymentmode != null)
            {
                if (!paymentmode.moneyreceipts.Contains(this))
                {
                    paymentmode.moneyreceipts.Add(this);
                }
                if (MoneyReceipt_PaymentmodeId != paymentmode.Paymentmode_Id)
                {
                    MoneyReceipt_PaymentmodeId = paymentmode.Paymentmode_Id;
                }
            }
        }

        #endregion
    }
}
