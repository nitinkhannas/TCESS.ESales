// -----------------------------------------------------------------------
// <copyright file="TruckVerificationDTO.cs" company="Q3 Technologies">
// Copyright 2010 - 2011 - www.q3tech.com. All rights reserved.
// This class is created to provide DTO object for Truck Verification.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TCESS.ESales.DataTransferObjects
{
	public class TruckVerificationDTO : BaseDTO
	{
        #region Primitive Properties
        
        public virtual long type
        {
            get;
            set;
        }

        public virtual int Truck_Id
        {
            get;
            set;
        }

        public virtual string Truck_RegNo
        {
            get;
            set;
        }

        public virtual string Truck_OwnerName
        {
            get;
            set;
        }

        public virtual string Truck_DriverName
        {
            get;
            set;
        }

        public virtual string Truck_Address
        {
            get;
            set;
        }

        public virtual int Truck_State
        {
            get;
            set;
        }

        public virtual string Truck_MobileNo
        {
            get;
            set;
        }

        public virtual string Truck_PhoneNo
        {
            get;
            set;
        }

        public virtual int Truck_Make
        {
            get;
            set;
        }

        public virtual sbyte Truck_IsBlacklisted
        {
            get;
            set;
        }

        public virtual string Truck_BlacklistedBy
        {
            get;
            set;
        }

        public virtual int Truck_CreatedBy
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> Truck_CreatedDate
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> Truck_LastUpdatedDate
        {
            get;
            set;
        }

        public virtual sbyte Truck_IsDeleted
        {
            get;
            set;
        }

        public virtual sbyte Truck_IsSuspended
        {
            get;
            set;
        }

        public virtual long id
        {
            get;
            set;
        }

        #endregion

	}
}
