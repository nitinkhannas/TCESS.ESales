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
        /// <summary>
        /// Get the list of all active Business Type
        /// </summary>
        /// <returns>List of  Business Type</returns>
        IList<BusinessTypeDTO> GetBusinessTypeList();

        /// <summary>
        /// Get Business Type List By Type Id
        /// </summary>
        /// <param name="businessTypeId">Int32:businessTypeId</param>
        /// <returns></returns>
        BusinessTypeDTO GetBusinessTypeListByTypeId(int businessTypeId);

        /// <summary>
        /// Save And Update Bussiness Type
        /// </summary>
        /// <param name="businessTypeDetails"></param>
        /// <returns></returns>
        int SaveAndUpdateBusinessType(BusinessTypeDTO businessTypeDetails);

        /// <summary>
        /// Delete Business Type
        /// </summary>
        /// <param name="businessTypeId">Int32:businessTypeId</param>
        void DeleteBusinessType(int businessTypeId);

        /// <summary>
        /// Verify Business Type Exists or not by businessTypeId and businessType
        /// </summary>
        /// <param name="businessTypeId">Int32:businessTypeId</param>
        /// <param name="businessType">String:businessType</param>
        /// <returns></returns>
        bool IsBusinessTypeExists(int businessTypeId, string businessType);

        /// <summary>
        /// Save And Update Ownership Status
        /// </summary>
        /// <param name="ownershipDetail"></param>
        /// <returns></returns>
        int SaveAndUpdateOwnershipStatus(OwnershipStatusDTO ownershipDetail);

        // <summary>
        /// Get the list of all active Ownership Status 
        //// </summary>
        /// <returns>list of Ownership Status</returns>
        IList<OwnershipStatusDTO> GetOwnershipStatusList();

        /// <summary>
        /// Delete Ownership Status
        /// </summary>
        /// <param name="ownershipStatusId">Int32:ownershipStatusId</param>
        void DeleteOwnershipStatus(int ownershipStatusId);

        /// <summary>
        /// Get Ownership Status List By Id
        /// </summary>
        /// <param name="ownershipStatusId">int32:ownershipStatusId</param>
        /// <returns></returns>
        OwnershipStatusDTO GetOwnershipStatusListById(int ownershipStatusId);

        /// <summary>
        /// Verify Ownership Status Exists or not by ownershipStatusId and ownershipStatus
        /// </summary>
        /// <param name="ownershipStatusId"></param>
        /// <param name="ownershipStatus"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get all active rejection reasons from database
        /// </summary>
        /// <returns>list of rejection reasons</returns>
        IList<RejectionReasonDTO> GetRejectionReasons();        
    }
}