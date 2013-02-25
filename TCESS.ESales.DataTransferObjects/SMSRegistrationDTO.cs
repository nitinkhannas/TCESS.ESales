using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
	public class SMSRegistrationDTO : BaseDTO
	{
		public int SMSReg_Id
		{
			get;
			set;
		}

		public int SMSReg_CustId
		{
			get;
			set;
		}

		public string SMSReg_TruckNo
		{
			get;
			set;
		}

		public string SMSReg_Msg
		{
			get;
			set;
		}

		public Nullable<int> SMSReg_Booking_Id
		{
			get;
			set;
		}

		public Nullable<bool> SMSReg_BookingStatus
		{
			get;
			set;
		}

        public Nullable<decimal> SMSReg_Booking_AdvancePaid
        {
            get;
            set;
        }

		public int SMSReg_CreatedBy
		{
			get;
			set;
		}

		public Nullable<DateTime> SMSReg_CreatedDate
		{
			get;
			set;
		}

		public Nullable<DateTime> SMSReg_LastUpdatedDate
		{
			get;
			set;
		}

		public bool SMSReg_IsDeleted
		{
			get;
			set;
		}

		public string SMSReg_Cust_Code
		{
			get;
			set;
		}

		public string SMSReg_Cust_TradeName
		{
			get;
			set;
		}

		public string SMSReg_Cust_UnitName
		{
			get;
			set;
		}

		public string SMSReg_Cust_UnitAddress
		{
			get;
			set;
		}

		public string SMSReg_Cust_District_Name
		{
			get;
			set;
		}

		public string SMSReg_Cust_State_Name
		{
			get;
			set;
		}

		public string SMSReg_Cust_PhoneNumber
		{
			get;
			set;
		}

		public DateTime SMSReg_Date
		{
			get;
			set;
		}

        public Nullable<int> SMSReg_Truck_Id
        {
            get;
            set;
        }

        public string SMSReg_Truck_RegNo
        {
            get;
            set;
        }

        public string SMSReg_Truck_OwnerName
        {
            get;
            set;
        }

        public string SMSReg_Truck_DriverName
        {
            get;
            set;
        }

        public Nullable<int> SMSReg_Agent_Id
        {
            get;
            set;
        }

        public string SMSReg_Agent_AgentShortName
        {
            get;
            set;
        }

        public string SMSReg_Agent_AgentName
        {
            get;
            set;
        }

		public string SMSReg_RejectionReason
		{
			get;
			set;
		}

        public string SMSReg_Booking_RejectionReson
        {
            get;
            set;
        }

        public string SMSReg_Last_Settlement_CreatedDate
        {
            get;
            set;
        }

        public string SMSReg_Last_Settlement_Dist
        {
            get;
            set;
        }

        public int SMSReg_CustomerBusinessType
        {
            get;
            set;
        }
		
		#region Navigation Properties
		private CustomerDTO _customer;       
        private AgentDTO _agentdetail;
        private BookingDTO _booking;

		public CustomerDTO customer
		{
			get { return _customer; }
			set
			{
				if (!ReferenceEquals(_customer, value))
				{
					var previousValue = _customer;
					_customer = null;
					SMSReg_Cust_Code = value.Cust_Code;
					SMSReg_Cust_UnitName = value.Cust_FirmName;
					SMSReg_Cust_UnitAddress = value.Cust_UnitAddress;
					SMSReg_Cust_District_Name = value.Cust_District_Name;
					SMSReg_Cust_State_Name = value.Cust_State_Name;
					SMSReg_Cust_TradeName = value.Cust_TradeName;
					SMSReg_Cust_PhoneNumber = value.Cust_MobileNo;
                    SMSReg_CustomerBusinessType = value.Cust_BusinessType;
				}
			}
		}

        public AgentDTO agentdetail
        {
            get { return _agentdetail; }
            set
            {
                if (!ReferenceEquals(_agentdetail, value))
                {
                    var previousValue = _agentdetail;
                    _agentdetail = null;
                    SMSReg_Agent_Id = value.Agent_Id;
                    SMSReg_Agent_AgentShortName = value.Agent_ShortName;
                    SMSReg_Agent_AgentName = value.Agent_Name;
                }
            }
        }
        public BookingDTO booking
        {
            get { return _booking; }
            set
            {
                if (!ReferenceEquals(_booking, value))
                {
                    var previousValue = _booking;
                    _booking = null;
                    SMSReg_Booking_AdvancePaid = value.Booking_AdvanceAmount;
                    SMSReg_Booking_RejectionReson = value.Booking_RejectionReson;
                   
                }
            }
        }
		#endregion
	}
}