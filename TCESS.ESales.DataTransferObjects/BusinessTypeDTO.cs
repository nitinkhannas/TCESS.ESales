// -----------------------------------------------------------------------
// <copyright file="BusinessTypeDTO.cs" company="Q3 Technologies">
// Copyright 2010 - 2011 - www.q3tech.com. All rights reserved.
// This class is created to provide DTO object for Business Type .
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TCESS.ESales.DataTransferObjects
{
    public class BusinessTypeDTO : BaseDTO
    {
        #region Primitive Properties

        public int BusinessType_Id
        {
            get;
            set;
        }

        public string BusinessType_Name
        {
            get;
            set;
        }

        public int BusinessType_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> BusinessType_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> BusinessType_LastUpdatedDate
        {
            get;
            set;
        }

        public bool BusinessType_IsDeleted
        {
			get;
			set;
        }

        #endregion
    }
}