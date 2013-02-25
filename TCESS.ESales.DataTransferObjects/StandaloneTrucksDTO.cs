// -----------------------------------------------------------------------
// <copyright file="StandaloneTrucksDTO.cs" company="Q3 Technologies">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TCESS.ESales.DataTransferObjects
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class StandaloneTrucksDTO : BaseDTO
    {
        #region Primitive Properties

        public int StandaloneTruck_Id
        {
            get;
            set;
        }

        public string StandaloneTruck_RegNo
        {
            get;
            set;
        }

        public string StandaloneTruck_OwnerName
        {
            get;
            set;
        }

        public string StandaloneTruck_OwnerShortAdd
        {
            get;
            set;
        }

        public string StandaloneTruck_DriverName
        {
            get;
            set;
        }

        public string StandaloneTruck_DriverShortAdd
        {
            get;
            set;
        }

        public string StandaloneTruck_Address
        {
            get;
            set;
        }

        public int StandaloneTruck_State
        {
            get;
            set;
        }

        public string StandaloneTruck_State_Name
        {
            get;
            set;
        }
        
        public string StandaloneTruck_MobileNo
        {
            get;
            set;
        }

        public string StandaloneTruck_PhoneNo
        {
            get;
            set;
        }

        public int StandaloneTruck_Make
        {
            get;
            set;
        }

        public string StandaloneTruckMake_Name
        {
            get;
            set;
        }

        public string StandaloneTruckCarryCapacity_Type
        {
            get;
            set;
        }
        public string StandaloneTruckWheeler_Type
        {
            get;
            set;
        }

        public bool StandaloneTruck_IsBlacklisted
        {
            get;
            set;
        }

        public string StandaloneTruck_Blacklistedby
        {
            get;
            set;
        }

        public int StandaloneTruck_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> StandaloneTruck_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> StandaloneTruck_LastUpdatedDate
        {
            get;
            set;
        }

        public bool StandaloneTruck_IsDeleted
        {
            get;
            set;
        }

        public Nullable<int> StandaloneTruck_RegType
        {
            get;
            set;
        }

        public string StandaloneTruck_CustCode
        {
            get;
            set;
        }

        public virtual bool StandaloneTruck_IsSuspended
        {
            get;
            set;
        }

        #endregion

		#region Navigation Properties

		private TruckMakeDTO _truckmake;
		private StateDTO _state;

		public TruckMakeDTO truckmake
		{
			get { return _truckmake; }
			set
			{
				if (!ReferenceEquals(_truckmake, value))
				{
					var previousValue = _truckmake;
					_truckmake = null;
					StandaloneTruckMake_Name = value.TruckMake_Name;
                    StandaloneTruckCarryCapacity_Type = value.TruckMake_TruckCC_Value;
                    StandaloneTruckWheeler_Type = value.TruckMake_TruckWheeler_Value;
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
					StandaloneTruck_State_Name = value.State_Name;
				}
			}
		}

		#endregion
	}
}