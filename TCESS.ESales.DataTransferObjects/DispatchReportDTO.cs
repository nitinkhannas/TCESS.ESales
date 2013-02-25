// -----------------------------------------------------------------------
// <copyright file="DispatchReportDTO.cs" company="Q3 Technologies">
// Copyright 2010 - 2011 - www.q3tech.com. All rights reserved.
// This class is created to provide DTO object for Truck Verification.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TCESS.ESales.DataTransferObjects
{
	public class DispatchReportDTO : BaseDTO
	{
		#region Primitive Properties

        public  int bookingid
        {
            get;
            set;
        }
        public  string LoadingAdvNo
		{
			get;
			set;
		}

		public  string TSLInvNo
		{
			get;
			set;
		}

		public  string TruckNo
		{
			get;
			set;
		}

		public  string CustCode
		{
			get;
			set;
		}

        public string CustomerName
        {
            get;
            set;
        }

		public  string UnitName
		{
			get;
			set;
		}

		public  string District
		{
			get;
			set;
		}

		public  decimal QtyLiftedMts
		{
			get;
			set;
		}

		public  decimal TSLAmount
		{
			get;
			set;
		}

		public  decimal DCABillHandling
		{
			get;
			set;
		}

		public  decimal ServiceTax
		{
			get;
			set;
		}

		public  decimal ECess2
		{
			get;
			set;
		}

		public  decimal HECess1
		{
			get;
			set;
		}

		public  decimal AdvanceReceived
		{
			get;
			set;
		}

		public  Nullable<System.DateTime> Booking_Date
		{
			get;
			set;
		}

		public  Nullable<int> Booking_Agent_Id
		{
			get;
			set;
		}
        public  decimal RateGroup
        {
            get;
            set;
        }
        public  Nullable<bool> Booking_IsFullAmount
        {
            get;
            set;
        }
        public  decimal Booking_Qty
        {
            get;
            set;
        }
		#endregion
	}
}
