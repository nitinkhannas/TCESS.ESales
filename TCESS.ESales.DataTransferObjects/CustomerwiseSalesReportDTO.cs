using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class CustomerwiseSalesReportDTO:BaseDTO
    {
        //#region Primitive Properties

        //public string NewSalesReport_District
        //{
        //    get;
        //    set;
        //}

        ////public int SalesReport_Pre_Qty
        ////{
        ////    get;
        ////    set;
        ////}

        ////public int SalesReport_Pre_Trip
        ////{
        ////    get;
        ////    set;
        ////}

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

        
        ////public int SalesReport_PreDay_Qty
        ////{
        ////    get;
        ////    set;
        ////}
        ////public int SalesReport_PreDay_Trip
        ////{
        ////    get;
        ////    set;
        ////}
        ////public int SalesReport_CurrDay_Qty
        ////{
        ////    get;
        ////    set;
        ////}
        ////public int SalesReport_CurrDay_Trip
        ////{
        ////    get;
        ////    set;
        ////}
        ////public string SalesReport_DCA
        ////{
        ////    get;
        ////    set;
        ////}

        ////public decimal SalesReport_TodayPercentage
        ////{
        ////    get;
        ////    set;
        ////}
        
        //#endregion

        public int Booking_Id
        {
            get;
            set;
        }

        public string CustomerCode
        {
            get;
            set;
        }

        public string CustomerName
        {
            get;
            set;
        }

        public string NameofDistrict
        {
            get;
            set;
        }

        public Nullable<DateTime> Booking_Date
        {
            get;
            set;
        }

        public int AnnualQtyRequirement
        {
            get;
            set;
        }

        public string QtyLimits
        {
            get;
            set;
        }

        public int Cust_Id
        {
            get;
            set;
        }

        public Nullable<int> Booking_Month
        {
            get;
            set;
        }

        public Nullable<int> Booking_Year
        {
            get;
            set;
        }

        public long Trips
        {
            get;
            set;
        }

        public Nullable<decimal> Quantity
        {
            get;
            set;
        }
    }
}
