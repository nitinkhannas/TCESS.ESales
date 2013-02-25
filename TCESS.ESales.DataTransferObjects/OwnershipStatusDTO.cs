// -----------------------------------------------------------------------
// <copyright file="OwnershipDTO.cs" company="Q3 Technologies">
// Copyright 2010 - 2011 - www.q3tech.com. All rights reserved.
// This class is created to provide DTO object for Customer Ownership details.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TCESS.ESales.DataTransferObjects
{
    public class OwnershipStatusDTO : BaseDTO
    {
        #region Primitive Properties

        public int OwnershipStatus_Id
        {
            get;
            set;
        }

        public string OwnershipStatus_Name
        {
            get;
            set;
        }

        public int OwnershipStatus_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> OwnershipStatus_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> OwnershipStatus_LastUpdatedDate
        {
            get;
            set;
        }

        public bool OwnershipStatus_IsDeleted
        {
			get;
			set;
        }

        #endregion
    }
}