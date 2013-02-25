using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class PaymentModeDTO : BaseDTO
    {
        public int Paymentmode_Id { get; set; }
        public string Paymentmode_Name { get; set; }
        public bool Paymentmode_IsGhatoCollection { get; set; }
        public int Paymentmode_CreatedBy { get; set; }
        public Nullable<DateTime> Paymentmode_CreatedDate { get; set; }
        public Nullable<DateTime> Paymentmode_LastupdatedDate { get; set; }
        public bool Paymentmode_IsDeleted { get; set; }
    }
}