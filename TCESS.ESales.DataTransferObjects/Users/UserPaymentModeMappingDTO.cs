// -----------------------------------------------------------------------
// <copyright file="UserPaymentModeMappingDTO.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TCESS.ESales.DataTransferObjects.Users
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UserPaymentModeMappingDTO : BaseDTO
    {
        public int UPM_Id { get; set; }
        public int UPM_UserId { get; set; }
        public int UPM_PaymentMode { get; set; }
        public string PaymentModeName { get; set; }
        public int UPM_CreatedBy { get; set; }
        public DateTime UPM_LastUpdatedDate { get { return DateTime.Now; } }
        public bool UPM_IsDeleted { get; set; }
    }
}