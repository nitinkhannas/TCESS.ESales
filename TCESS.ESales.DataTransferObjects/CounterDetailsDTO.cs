// -----------------------------------------------------------------------
// <copyright file="CounterDetailsDTO.cs" company="Q3 Technologies">
// Copyright 2010 - 2011 - www.q3tech.com. All rights reserved.
// This class is created to provide DTO object for Business Type .
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
	public class CounterDetailsDTO : BaseDTO
	{
		#region Primitive Properties

		public  int CounterDetail_Id
		{
			get;
			set;
		}

		public  int CounterDetail_Counter_ID
		{
			get;
			set;
		}


		public  System.DateTime CounterDetail_Date
		{
			get;
			set;
		}

		public  int CounterDetail_Count
		{
			get;
			set;
		}

		public  int CounterDetail_Agent_Id
		{
			get;
			set;
		}


		public  Nullable<int> CounterDetail_CreatedBy
		{
			get;
			set;
		}

		public  Nullable<System.DateTime> CounterDetail_CreatedDate
		{
			get;
			set;
		}

		public  Nullable<System.DateTime> CounterDetail_LastupdatedDate
		{
			get;
			set;
		}

		#endregion

	}
}