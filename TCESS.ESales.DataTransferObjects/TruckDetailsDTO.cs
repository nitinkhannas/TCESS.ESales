// -----------------------------------------------------------------------
// <copyright file="TruckDetailsDTO.cs" company="Q3 Technologies">
// Copyright 2010 - 2011 - www.q3tech.com. All rights reserved.
// This class is created to provide DTO object for Truck Details.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TCESS.ESales.DataTransferObjects
{
    public class TruckDetailsDTO : BaseDTO
    {
        #region Primitive Properties

        public int Truck_Id
        {
            get;
            set;
        }

        public int Truck_CustomerId
        {
            get;
            set;
        }

        public CustomerDTO Truck_Customer
        {
            get;
            set;
        }

        public string Truck_RegNo
        {
            get;
            set;
        }

        public string Truck_OwnerName
        {
            get;
            set;
        }

        public string Truck_OwnerShortAdd
        {
            get;
            set;
        }

        public string Truck_DriverName
        {
            get;
            set;
        }

        public string Truck_DriverShortAdd
        {
            get;
            set;
        }

        public string Truck_Address
        {
            get;
            set;
        }

        public int Truck_State
        {
            get;
            set;
        }

        public string Truck_State_Name
        {
            get;
            set;
        }

        public string Truck_MobileNo
        {
            get;
            set;
        }

        public string Truck_PhoneNo
        {
            get;
            set;
        }

        public int Truck_Make
        {
            get;
            set;
        }

        public string TruckMake_Name
        {
            get;
            set;
        }

        public string TruckWheeler_Type
        {
            get;
            set;
        }

        public string TruckCarryCapacity_Type
        {
            get;
            set;
        }

        public bool Truck_IsBlacklisted
        {
            get;
            set;
        }

        public string Truck_BlacklistedBy
        {
            get;
            set;
        }

        public int Truck_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> Truck_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> Truck_LastUpdatedDate
        {
            get;
            set;
        }

        public bool Truck_IsDeleted
        {
            get;
            set;
        }

        public  Nullable<int> Truck_RegType
        {
            get;
            set;
        }

        public virtual bool Truck_IsSuspended
        {
            get;
            set;
        }

        #endregion
    
        #region Navigation Properties

        private TruckMakeDTO _truckmake;
        private CustomerDTO _customer;
		private StateDTO _state;

        public CustomerDTO Customer
        {
            get{return _customer;}
            set
            {
                if(!ReferenceEquals(_customer,value))
                {
                    var previousvalue =_customer;
                    _customer = null;
                    Truck_Customer = value;
                }
            }
        }

        public TruckMakeDTO TruckMake
        {
            get { return _truckmake; }
            set
            {
                if (!ReferenceEquals(_truckmake, value))
                {
                    var previousValue = _truckmake;
                    _truckmake = null;
                    TruckMake_Name = value.TruckMake_Name;
                    TruckCarryCapacity_Type = value.TruckMake_TruckCC_Value;
                    TruckWheeler_Type = value.TruckMake_TruckWheeler_Value;
                }
            }
        }

		public StateDTO State
		{
			get { return _state; }
			set
			{
                if (!ReferenceEquals(_state, value))
				{
					var previousValue = _state;
					_state = null;
					Truck_State_Name = value.State_Name;
				}
			}
		}
    
       #endregion
    }
}