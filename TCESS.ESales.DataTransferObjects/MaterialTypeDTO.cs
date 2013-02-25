// -----------------------------------------------------------------------
// <copyright file="MaterialTypeDTO.cs" company="Q3 Technologies">
// Copyright 2010 - 2011 - www.q3tech.com. All rights reserved.
// This class is created to provide DTO object for Material Type.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;

namespace TCESS.ESales.DataTransferObjects
{  
    public class MaterialTypeDTO : BaseDTO
    {
        #region Primitive Properties

        public int MaterialType_Id
        {
            get;
            set;
        }

        public string MaterialType_Code
        {
            get;
            set;
        }

        public string MaterialType_Name
        {
            get;
            set;
        }

        public decimal MaterialType_TiscoRate
        {
            get;
            set;
        }

        public decimal MaterialType_CSTRate
        {
            get;
            set;
        }

        public decimal MaterialType_CFormRate
        {
            get;
            set;
        }

        public decimal MaterialType_HandlingRate
        {
            get;
            set;
        }

        public decimal MaterialType_ServiceTax
        {
            get;
            set;
        }

        public decimal MaterialType_EducationCess
        {
            get;
            set;
        }

        public decimal MaterialType_HigherEducationCess
        {
            get;
            set;
        }
    
        public bool MaterialType_IsActive
        {
            get;
            set;
        }        
        
        public int MaterialType_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> MaterialType_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> MaterialType_LastUpdatedDate
        {
            get;
            set;
        }

        public bool MaterialType_IsDeleted
        {
            get;
            set;
        }

        #endregion
    }
}