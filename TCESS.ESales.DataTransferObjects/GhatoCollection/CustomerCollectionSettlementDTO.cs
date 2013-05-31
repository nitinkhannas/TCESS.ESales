using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects.GhatoCollection
{
    public class CustomerCollectionSettlementDTO:BaseDTO
    {
        public string DateReceived { get; set; }
        public string DateActivated { get; set; }
        public string TransactionType { get; set; }
        public string InstTruckNo { get; set; }
        public string ReceiptNo { get; set; }
        public Nullable<decimal> Refund { get; set; }
        public Nullable<decimal> Settlement { get; set; }
        public Nullable<decimal> Amount { get; set; }
    }
}
