// -----------------------------------------------------------------------
// <copyright file="BankDTO.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TCESS.ESales.DataTransferObjects.Masters
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BankDTO : BaseDTO
    {
        public int Bank_Id { get; set; }
        public string Bank_Name { get; set; }
        public string Bank_AccountNo { get; set; }
        public int Bank_CreatedBy { get; set; }
        public DateTime Bank_LastUpdatedDate { get; set; }
        public bool Bank_IsDeleted { get; set; }
    }
}