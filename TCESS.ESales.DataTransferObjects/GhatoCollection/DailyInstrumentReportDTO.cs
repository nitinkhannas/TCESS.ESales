using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class DailyInstrumentReportDTO : BaseDTO
    {
        public int Receipt_No { get; set; }
        public string Bank_Credited { get; set; }
        public string Cust_Code { get; set; }
        public string Customer_Name { get; set; }
        public string CustomerTradeName { get; set; }
        public string Bank_Drawn { get; set; }
        public string Inst_No { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Recd_Amount { get; set; }
        public Nullable<decimal> Bk_Chges { get; set; }
        public Nullable<string> Rejected { get; set; }
        public Nullable<string> Reason { get; set; }
        public Nullable<DateTime> InstrumentDate { get; set; }
        
    }
}
