// -----------------------------------------------------------------------
// <copyright file="CustDocTypeInfoDTO.cs" company="Q3 Technologies">
// Copyright 2010 - 2011 - www.q3tech.com. All rights reserved.
// This class is created to provide DTO object for Customer Document Type Information.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TCESS.ESales.DataTransferObjects
{
    public class DocTypeDTO : BaseDTO
    {
        #region Primitive Properties

        public int Doc_Id
        {
            get;
            set;
        }

        public string Doc_Name
        {
            get;
            set;
        }
		public  string Doc_Acronym
		{
			get;
			set;
		}

        public int Doc_Group
        {
            get;
            set;
        }

        public bool Doc_Mandatory
        {
            get;
            set;
        }
		public  Nullable<bool> Doc_IsUnique
		{
			get;
			set;
		}

        public int Doc_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> Doc_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> Doc_LastUpdatedDate
        {
            get;
            set;
        }

        public bool Doc_IsDeleted
        {
			get;
			set;
        }

        #endregion
    }
}