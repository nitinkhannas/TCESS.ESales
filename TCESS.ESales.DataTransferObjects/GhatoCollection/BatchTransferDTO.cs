// -----------------------------------------------------------------------
// <copyright file="BatchTransferDTO.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TCESS.ESales.DataTransferObjects.GhatoCollection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BatchTransferDTO : BaseDTO
    {
        public int BT_Id { get; set; }
        public string CounterName { get; set; }
        public string UserName { get; set; }
        public decimal Amount { get; set; }
        public int PaymentMode { get; set; }
        public int BT_Status { get; set; }
        public IList<PaymentTransitDTO> PaymentTransits { get; set; }
        public int BT_CreatedBy { get; set; }
        public DateTime BT_CreatedDate { get { return DateTime.Now; } }
        public Nullable<int> BT_ApprovedBy { get; set; }
        public Nullable<DateTime> BT_ApprovedDate { get; set; }
        public Nullable<int> BT_BankName { get; set; }
        public string BT_BankBranch { get; set; }
    }
}