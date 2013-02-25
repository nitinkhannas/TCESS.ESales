// -----------------------------------------------------------------------
// <copyright file="AMEBlockDTO.cs" company="Q3 Technologies">
// Copyright 2010 - 2011 - www.q3tech.com. All rights reserved.
// This class is created to provide DTO object for AME Blocks.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TCESS.ESales.DataTransferObjects
{
    public class AmeBlockDTO : BaseDTO
    {
        #region Primitive Properties

        public int Blocks_Id
        {
            get;
            set;
        }

        public string Blocks_Name
        {
            get;
            set;
        }

        public int Blocks_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> Blocks_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> Blocks_LastUpdatedDate
        {
            get;
            set;
        }

        public bool Blocks_IsDeleted
        {
			get;
			set;
        }

        #endregion
    }
}