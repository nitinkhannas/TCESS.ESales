// -----------------------------------------------------------------------
// <copyright file="ConsolidatedReport.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
namespace TCESS.ESales.DataTransferObjects.GhatoCollection
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ConsolidatedReportDTO : BaseDTO
    {
        public Nullable<long> CounterId { get; set; }
        public string CounterName { get; set; }
        public int TotalChequeCount { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }   
        public Nullable<decimal> TransferredAmount { get; set; }
        public Nullable<decimal> ChequesCleared { get; set; }
        public Nullable<decimal> ChequesBounced { get; set; }
        public Nullable<decimal> InTransitAmount { get; set; }
        public Nullable<decimal> CashInHand { get; set; }
    }
}