using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects.GhatoCollection
{
   public class ConsolidatedCustomerCollectionReportDTO:BaseDTO
    {
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDistrict { get; set; }
        public Nullable<decimal> OpeningBalance { get; set; }
        public Nullable<decimal> CollectionActive { get; set; }
        public Nullable<decimal> TotalBalAvailable { get; set; }
        public Nullable<decimal> TotalSettlement { get; set; }
        public Nullable<decimal> HoldForPendingBooking { get; set; }
        public Nullable<decimal> HoldForPendingLA { get; set; }
        public Nullable<decimal> HoldForActivation { get; set; }
        public Nullable<decimal> Refund { get; set; }
        public Nullable<decimal> ClosingBalance { get; set; }
    }
}
