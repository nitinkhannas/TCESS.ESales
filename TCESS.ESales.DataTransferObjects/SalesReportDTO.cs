using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class SalesReportDTO:BaseDTO
    {
        #region Primitive Properties

        public string SalesReport_District
        {
            get;
            set;
        }

        public int SalesReport_Pre_Qty
        {
            get;
            set;
        }

        public int SalesReport_Pre_Trip
        {
            get;
            set;
        }

        public int SalesReport_CrrMt_Qty
        {
            get;
            set;
        }
        public int SalesReport_CrrMt_Trip
        {
            get;
            set;
        }
        public int SalesReport_Crr_Qty
        {
            get;
            set;
        }
        public int SalesReport_Crr_Trip
        {
            get;
            set;
        }

        public string SalesReport_Cust_Code
        {
            get;
            set;
        }

        public string SalesReport_Cust_Name
        {
            get;
            set;
        }

        public int SalesReport_Qty_Limit
        {
            get;
            set;
        }
        public int SalesReport_PreDay_Qty
        {
            get;
            set;
        }
        public int SalesReport_PreDay_Trip
        {
            get;
            set;
        }
        public int SalesReport_CurrDay_Qty
        {
            get;
            set;
        }
        public int SalesReport_CurrDay_Trip
        {
            get;
            set;
        }
        public string SalesReport_DCA
        {
            get;
            set;
        }

        public decimal SalesReport_TodayPercentage
        {
            get;
            set;
        }
        public int SalesReport_Qty_Demand
        {
            get;
            set;
        }
        #endregion
    }
}
