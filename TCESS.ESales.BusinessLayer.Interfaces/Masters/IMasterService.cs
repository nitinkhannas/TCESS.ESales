// -----------------------------------------------------------------------
// <copyright file="IMasterService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TCESS.ESales.BusinessLayer.Interfaces.Masters
{
    #region Using directives

    using System.Collections.Generic;
    using TCESS.ESales.DataTransferObjects;
    using TCESS.ESales.DataTransferObjects.Masters;

    #endregion

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IMasterService
    {
        IList<BusinessTypeDTO> GetBusinessTypeList();
        BusinessTypeDTO GetBusinessTypeListByTypeId(int businessTypeId);
        int SaveAndUpdateBusinessType(BusinessTypeDTO businessTypeDetails);
        void DeleteBusinessType(int businessTypeId);
        bool IsBusinessTypeExists(int businessTypeId, string businessType);

        int SaveAndUpdateOwnershipStatus(OwnershipStatusDTO ownershipDetail);
        IList<OwnershipStatusDTO> GetOwnershipStatusList();
        void DeleteOwnershipStatus(int ownershipStatusId);
        OwnershipStatusDTO GetOwnershipStatusListById(int ownershipStatusId);
        bool IsOwnershipStatusExists(int ownershipStatusId, string ownershipStatus);

        void SaveAndUpdateBankDetails(BankDTO BankDetails);
        
        /// <summary>
        /// Get list of banks from database
        /// </summary>
        /// <returns>returns bank details, else blank datatype</returns>
        IList<BankDTO> GetBankDetails();
        
        BankDTO GetBanksDetailsById(int bankId);
        void DeleteBank(int bankId);

        /// <summary>
        /// Get list of active payment modes
        /// </summary>
        /// <param name="isGhatoCollection">Get Payment mode for Ghato Collection</param>
        /// <returns>returns customer details if exists, else blank datatype</returns>
        IList<PaymentModeDTO> GetListOfPaymentMode(bool isGhatoCollection);

        IList<RejectionReasonDTO> GetRejectionReasons();        
    }
}