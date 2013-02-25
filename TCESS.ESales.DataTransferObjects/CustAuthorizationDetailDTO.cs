// -----------------------------------------------------------------------
// <copyright file="CustAuthorizationDetailDTO.cs" company="Q3 Technologies">
// Copyright 2010 - 2011 - www.q3tech.com. All rights reserved.
// This class is created to provide DTO object for Customer Authorization Details.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TCESS.ESales.DataTransferObjects
{
 public	class CustAuthorizationDetailDTO : BaseDTO
	{
		#region Primitive Properties

		public  int CustAuth_Id
		{
			get;
			set;
		}

		public  int CustAuth_CustId
		{
			get;
			set;
		}		

		public Nullable<DateTime> CustAuth_Date
		{
			get;
			set;
		}

		public byte[] CustAuth_File
		{
			get;
			set;
		}

        public string CustAuth_FileName
        {
            get;
            set;
        }

		public bool CustAuth_Status
		{
			get;
			set;
		}

		public string CustAuth_Remarks
		{
			get;
			set;
		}

		public int CustAuth_CreatedBy
		{
			get;
			set;
		}

		public Nullable<DateTime> CustAuth_CreatedDate
		{
			get;
			set;
		}

		public Nullable<DateTime> CustAuth_LastUpdateDate
		{
			get;
			set;
		}

		public bool CustAuth_IsDeleted
		{
			get;
			set;
		}

		#endregion
	}
}