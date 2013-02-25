// -----------------------------------------------------------------------
// <copyright file="RejectionReasonDTO.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TCESS.ESales.DataTransferObjects.Masters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RejectionReasonDTO : BaseDTO
    {
        public int RR_Id { get; set; }
        public string RR_Name { get; set; }
        public int RR_CreatedBy { get; set; }
        public DateTime RR_LastModifiedDate { get { return DateTime.Now; } }
        public bool RR_IsDeleted { get; set; }
    }
}