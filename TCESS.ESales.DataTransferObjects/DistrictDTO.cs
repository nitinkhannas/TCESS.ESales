// -----------------------------------------------------------------------
// <copyright file="StateDTO.cs" company="Q3 Technologies">
// Copyright 2010 - 2011 - www.q3tech.com. All rights reserved.
// This class is created to provide DTO object for District Details.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TCESS.ESales.DataTransferObjects
{
    public class DistrictDTO : BaseDTO
    {
        #region Primitive Properties

        public int Dist_Id
        {
            get;
            set;
        }

        public int Dist_StateId
        {
            get;
            set;
        }

        public string Dist_Name
        {
            get;
            set;
        }

        public int Dist_Createdby
        {
            get;
            set;
        }

        public Nullable<DateTime> Dist_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> Dist_LastUpdatedDate
        {
            get;
            set;
        }

        public bool Dist_IsDeleted
        {
            get;
            set;
        }

        #endregion
    }
}