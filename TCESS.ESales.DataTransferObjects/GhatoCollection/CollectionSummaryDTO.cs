// -----------------------------------------------------------------------
// <copyright file="CollectionSummaryDTO.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TCESS.ESales.DataTransferObjects.GhatoCollection
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CollectionSummaryDTO : BaseDTO
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal ChequeCollected { get; set; }
        public decimal DDCollected { get; set; }
        public decimal RTGSCollected { get; set; }
    }
}