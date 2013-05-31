using System;
namespace TCESS.ESales.DataTransferObjects
{
    public class SettlementOfAccountsDTO : BaseDTO
    {
        #region Primitive Properties

        public int Account_Id
        {
            get;
            set;
        }

        public int Account_Booking_Id
        {
            get;
            set;
        }

        public decimal Account_AdvanceReceived
        {
            get;
            set;
        }

        public decimal Account_Quantity
        {
            get;
            set;
        }

        public string Account_CFormNo
        {
            get;
            set;
        }

        public string Account_FormDNumber
        {
            get;
            set;
        }

        public string Account_HGNumber
        {
            get;
            set;
        }

        public string Account_InvoiceNumber
        {
            get;
            set;
        }

        public string Account_GatePassNo
        {
            get;
            set;
        }

        public string Account_RoadPermitNo
        {
            get;
            set;
        }

        public decimal Account_TiscoRate
        {
            get;
            set;
        }

        public decimal Account_HandlingRate
        {
            get;
            set;
        }

        public decimal Account_HandlingServiceTax
        {
            get;
            set;
        }

        public decimal Account_HandlingECess
        {
            get;
            set;
        }

        public decimal Account_HandlingHECess
        {
            get;
            set;
        }

        public decimal Account_TotalAmount
        {
            get;
            set;
        }

        public decimal Account_Balance
        {
            get;
            set;
        }

        public string Account_RateGroup
        {
            get;
            set;
        }

        public int Account_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> Account_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> Account_LastUpdatedDate
        {
            get;
            set;
        }

        public bool Account_IsDeleted
        {
            get;
            set;
        }

        public string Account_Booking_Cust_UnitName
        {
            get;
            set;
        }

        public string Account_Booking_Cust_UnitAddress
        {
            get;
            set;
        }

        public string Account_Booking_Cust_District_Name
        {
            get;
            set;
        }

        public string Account_Booking_Truck_RegNo
        {
            get;
            set;
        }

        public string Account_Booking_StandaloneTruck_RegNo
        {
            get;
            set;
        }

        public Nullable<int> Account_Agent_Id
        {
            get;
            set;
        }

        public int Account_Booking_Cust_Id
        {
            get;
            set;
        }

        public string Account_Booking_Cust_Code
        {
            get;
            set;
        }

        public string Account_Booking_Cust_OwnerName
        {
            get;
            set;
        }

        public string Account_Booking_Cust_FirmName
        {
            get;
            set;
        }

        public string Account_Booking_CounterName
        {
            get;
            set;
        }

        public DateTime Account_Booking_Date
        {
            get;
            set;
        }

        public Nullable<int> Account_Form27CId
        {
            get;
            set;
        }
        #endregion

        #region Navigation Properties
        private CustomerDTO _customer;
        private BookingDTO _booking;

        public virtual BookingDTO booking
        {
            get { return _booking; }
            set
            {
                if (!ReferenceEquals(_booking, value))
                {
                    var previousValue = _booking;
                    _booking = value;
                    Account_Booking_Cust_UnitName = value.Booking_Cust_UnitName;
                    Account_Booking_Cust_UnitAddress = value.Booking_Cust_UnitAddress;
                    Account_Booking_Cust_District_Name = value.Booking_Cust_District_Name;
                    Account_Booking_Truck_RegNo = value.Booking_Truck_RegNo;
                    Account_Booking_StandaloneTruck_RegNo = value.Booking_StandaloneTruck_RegNo;
                    Account_Agent_Id = value.Booking_Agent_Id;
                    Account_Booking_Cust_Id = value.Booking_Cust_Id;
                    Account_Booking_Cust_Code = value.Booking_Cust_Code;
                    Account_Booking_Cust_OwnerName = value.Booking_Cust_OwnerName;
                    Account_Booking_Cust_FirmName = value.Booking_Cust_FirmName;
                    Account_Booking_CounterName = value.Booking_CounterName;
                    Account_Booking_Date = value.Booking_Date;

                }
            }
        }

        //public CustomerDTO customer
        //{
        //    get { return _customer; }
        //    set
        //    {
        //        if (!ReferenceEquals(_customer, value))
        //        {
        //            var previousValue = _customer;
        //            _customer = value;
        //            Account_Customer_Cust_FirmName = value.Cust_FirmName;

        //        }
        //    }
        //}

        #endregion
    }
}
