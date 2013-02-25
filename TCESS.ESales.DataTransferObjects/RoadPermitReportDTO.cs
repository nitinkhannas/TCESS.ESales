// -----------------------------------------------------------------------
// <copyright file="DispatchReportDTO.cs" company="Q3 Technologies">
// Copyright 2010 - 2011 - www.q3tech.com. All rights reserved.
// This class is created to provide DTO object for Truck Verification.
// </copyright>
// -----------------------------------------------------------------------

using System;


namespace TCESS.ESales.DataTransferObjects
{
    public class RoadPermitReportDTO : BaseDTO
    {
        #region Primitive Properties

        public Nullable<System.DateTime> SettlementDate
        {
            get;
            set;
        }

        public string RoadPermitNo
        {
            get;
            set;
        }

        public string CustomerName
        {
            get;
            set;
        }

        public string CustomerAddress
        {
            get;
            set;
        }

        public string District
        {
            get;
            set;
        }

        public string TSLInvoiceNo
        {
            get;
            set;
        }

        public decimal QuantityLifted
        {
            get;
            set;
        }

        public string TruckNo
        {
            get;
            set;
        }
        
        #endregion
    }
}
