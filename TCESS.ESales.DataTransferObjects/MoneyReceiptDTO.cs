using System;

namespace TCESS.ESales.DataTransferObjects
{
    public class MoneyReceiptDTO : BaseDTO
    {
        #region Primitive Properties

        public int MoneyReceipt_Id
        {
            get;
            set;
        }

        public int MoneyReceipt_Booking_Id
        {
            get;
            set;
        }

        public DateTime MoneyReceipt_Booking_Date
        {
            get;
            set;
        }

        public int MoneyReceipt_Booking_Qty
        {

            get;
            set;
        }

        public string MoneyReceipt_InvoiceNo
        {
            get;
            set;
        }

        public string MoneyReceipt_AgentName
        {
            get;
            set;
        }

        public string MoneyReceipt_AgentShortName
        {
            get;
            set;
        }

        public string MoneyReceipt_MaterialName
        {
            get;
            set;
        }

        public string MoneyReceipt_Cust_FirmName
        {
            get;
            set;
        }
       
        public string MoneyReceipt_Cust_Code
        {
            get;
            set;
        }

        public string MoneyReceipt_District
        {
            get;
            set;
        }

        public string MoneyReceipt_Truck_RegNo
        {
            get;
            set;
        }

        public string MoneyReceipt_Truck_DriverName
        {
            get;
            set;
        }

        public string MoneyReceipt_Truck_OwnerName
        {
            get;
            set;
        }

        public decimal MoneyReceipt_BookingAdvance
        {
            get;
            set;
        }

        public decimal MoneyReceipt_AmountPaid
        {
            get;
            set;
        }

        public int MoneyReceipt_PaymentmodeId
        {
            get;
            set;
        }

        public string MoneyReceipt_InstrumentNo
        {
            get;
            set;
        }

        public string MoneyReceipt_AccountName
        {
            get;
            set;
        }

        public string MoneyReceipt_Remarks
        {
            get;
            set;
        }

        public Nullable<decimal> MoneyReceipt_RefundAmount
        {
            get;
            set;
        }

        public string MoneyReceipt_CancelReceiptNo
        {
            get;
            set;
        }

        public string MoneyReceipt_CancellationRemarks
        {
            get;
            set;
        }

        public int MoneyReceipt_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> MoneyReceipt_CreateDate
        {
            get;
            set;
        }

        public Nullable<DateTime> MoneyReceipt_LastUpdateDate
        {
            get;
            set;
        }

        public bool MoneyReceipt_IsDeleted
        {
            get;
            set;
        }
        public string MoneyReceipt_PaymentMode_PaymentmodeName
        {
            get;
            set;
        }
        
        #endregion

        #region Navigation Properties

        private BookingDTO _booking;
        private PaymentModeDTO _paymentMode;
        public BookingDTO booking
        {
            get { return _booking; }
            set
            {
                if (!ReferenceEquals(_booking, value))
                {
                    var previousValue = _booking;
                    _booking = null;
                    MoneyReceipt_Booking_Date = value.Booking_Date;
                    MoneyReceipt_Booking_Qty = value.Booking_Qty;
                    MoneyReceipt_Cust_Code = value.Booking_Cust_Code;
                    MoneyReceipt_Cust_FirmName = value.Booking_Cust_UnitName;
                    MoneyReceipt_District = value.Booking_Cust_District_Name;
                    MoneyReceipt_Truck_RegNo = value.Booking_TruckType == false ? value.Booking_Truck_RegNo : value.Booking_StandaloneTruck_RegNo;
                    MoneyReceipt_Truck_DriverName = value.Booking_TruckType == false ? value.Booking_Truck_RegNo : value.Booking_StandaloneTruck_RegNo;
                    MoneyReceipt_Truck_OwnerName = value.Booking_TruckType == false ? value.Booking_Truck_RegNo : value.Booking_StandaloneTruck_RegNo;
                    MoneyReceipt_InvoiceNo = value.Booking_Agent_AgentShortName + "-" + value.Booking_Id;
                    MoneyReceipt_BookingAdvance = (decimal)value.Booking_AdvanceAmount;
                    MoneyReceipt_AgentName = value.Booking_Agent_AgentName;
                    MoneyReceipt_AgentShortName = value.Booking_Agent_AgentShortName;
                    MoneyReceipt_MaterialName = value.Booking_MaterialType_MaterialName;
                    MoneyReceipt_AgentShortName = value.Booking_Agent_AgentShortName;
                    MoneyReceipt_AgentName = value.Booking_Agent_AgentName;                   
                   
                }
            }
        }
        public PaymentModeDTO paymentMode
        {
            get { return _paymentMode; }
            set
            {
                if (!ReferenceEquals(_booking, value))
                {
                    var previousValue = _paymentMode;
                    _paymentMode = null;
                    MoneyReceipt_PaymentMode_PaymentmodeName = value.Paymentmode_Name;
                    

                }
            }
        }
        #endregion
    }
}
