// -----------------------------------------------------------------------
// <copyright file="PaymentTransitDTO.cs" company="">
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
    public class PaymentTransitDTO : BaseDTO
    {
        public int PaymentTransit_Id { get; set; }
        public int PaymentTransit_CollectionId { get; set; }
        public int PaymentTransit_BatchId { get; set; }
        public int PaymentTransit_CreatedBy { get; set; }
        public DateTime PaymentTransit_CreatedDate { get { return DateTime.Now; } }
    }
}