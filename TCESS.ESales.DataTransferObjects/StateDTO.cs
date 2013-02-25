// -----------------------------------------------------------------------
// <copyright file="StateDTO.cs" company="Q3 Technologies">
// Copyright 2010 - 2011 - www.q3tech.com. All rights reserved.
// This class is created to provide DTO object for State Details.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TCESS.ESales.DataTransferObjects
{
    public class StateDTO : BaseDTO
    {
        #region Primitive Properties

        public int State_Id
        {
            get;
            set;
        }

        public string State_Name
        {
            get;
            set;
        }

        public int State_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> State_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> State_LastUpdatedDate
        {
            get;
            set;
        }

        public bool State_IsDeleted
        {
			get;
			set;
        }

        #endregion
    }
}