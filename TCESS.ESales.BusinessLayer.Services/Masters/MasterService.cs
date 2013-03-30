// -----------------------------------------------------------------------
// <copyright file="MasterService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TCESS.ESales.BusinessLayer.Services.Masters
{
    #region Using directives

    using System.Collections.Generic;
    using System.Linq;
    using TCESS.ESales.BusinessLayer.Interfaces.Masters;
    using TCESS.ESales.DataTransferObjects;
    using TCESS.ESales.PersistenceLayer.Entity;
    using TCESS.ESales.DataTransferObjects.Masters;
using System.Transactions;

    #endregion

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MasterService : MasterBaseService, IMasterService
    {
        /// <summary>
        /// Get Business Type List By Type Id
        /// </summary>
        /// <param name="businessTypeId">Int32:businessTypeId</param>
        /// <returns></returns>
        public BusinessTypeDTO GetBusinessTypeListByTypeId(int businessTypeId)
        {
            BusinessTypeDTO objBusinessTypeDTO = new BusinessTypeDTO();
            AutoMapper.Mapper.Map(base.BusinessTypeRepository.GetSingle(item => item.BusinessType_Id == businessTypeId), objBusinessTypeDTO);
            return objBusinessTypeDTO;
        }

        /// <summary>
        /// Save And Update Bussiness Type
        /// </summary>
        /// <param name="businessTypeDetails"></param>
        /// <returns></returns>
        public int SaveAndUpdateBusinessType(BusinessTypeDTO businessTypeDetails)
        {
            businesstype businessTypeEntity = new businesstype();
            AutoMapper.Mapper.Map(businessTypeDetails, businessTypeEntity);

            if (businessTypeEntity.BusinessType_Id == 0)
            {
                base.BusinessTypeRepository.Save(businessTypeEntity);
            }
            else
            {
                base.BusinessTypeRepository.Update(businessTypeEntity);
            }
            return businessTypeEntity.BusinessType_Id;
        }
        
        /// <summary>
        /// Get the list of all active Business Type
        /// </summary>
        /// <returns>List of  Business Type</returns>
        IList<BusinessTypeDTO> IMasterService.GetBusinessTypeList()
        {
            List<BusinessTypeDTO> lstBusinessTypes = new List<BusinessTypeDTO>();
            List<businesstype> lstBusinessTypesEntity = base.BusinessTypeRepository.GetQuery().Where(item => item.BusinessType_IsDeleted == false)
                .OrderBy(order => order.BusinessType_Name).ToList();

            AutoMapper.Mapper.Map(lstBusinessTypesEntity, lstBusinessTypes);
            return lstBusinessTypes;
        }

        /// <summary>
        /// Delete Business Type
        /// </summary>
        /// <param name="businessTypeId">Int32:businessTypeId</param>
        void IMasterService.DeleteBusinessType(int businessTypeId)
        {
            BusinessTypeDTO businessType = GetBusinessTypeListByTypeId(businessTypeId);
            businessType.BusinessType_IsDeleted = true;

            SaveAndUpdateBusinessType(businessType);
        }

        // <summary>
        /// Get the list of all active Ownership Status 
        //// </summary>
        /// <returns>list of Ownership Status</returns>
        IList<OwnershipStatusDTO> IMasterService.GetOwnershipStatusList()
        {
            List<OwnershipStatusDTO> lstOwnershipStatusDetails = new List<OwnershipStatusDTO>();
            List<ownershipstatu> lstOwnershipStatusDetailsEntity = base.OwnershipStatusRepository.GetQuery()
                .Where(item => item.OwnershipStatus_IsDeleted == false)
                .OrderBy(order => order.OwnershipStatus_Name).ToList();

            AutoMapper.Mapper.Map(lstOwnershipStatusDetailsEntity, lstOwnershipStatusDetails);
            return lstOwnershipStatusDetails;
        }

        /// <summary>
        /// Save And Update Ownership Status
        /// </summary>
        /// <param name="ownershipDetail"></param>
        /// <returns></returns>
        public int SaveAndUpdateOwnershipStatus(OwnershipStatusDTO ownershipDetail)
        {
            ownershipstatu ownershipStatusEntity = new ownershipstatu();
            AutoMapper.Mapper.Map(ownershipDetail, ownershipStatusEntity);

            if (ownershipDetail.OwnershipStatus_Id == 0)
            {
                base.OwnershipStatusRepository.Save(ownershipStatusEntity);
            }
            else
            {
                base.OwnershipStatusRepository.Update(ownershipStatusEntity);
            }
            return ownershipStatusEntity.OwnershipStatus_Id;
        }

        /// <summary>
        /// Delete Ownership Status
        /// </summary>
        /// <param name="ownershipStatusId">Int32:ownershipStatusId</param>
        void IMasterService.DeleteOwnershipStatus(int ownershipStatusId)
        {
            OwnershipStatusDTO ownershipStatus = GetOwnershipStatusListById(ownershipStatusId);
            ownershipStatus.OwnershipStatus_IsDeleted = true;

            SaveAndUpdateOwnershipStatus(ownershipStatus);
        }

        /// <summary>
        /// Get Ownership Status List By Id
        /// </summary>
        /// <param name="ownershipStatusId">int32:ownershipStatusId</param>
        /// <returns></returns>
        public OwnershipStatusDTO GetOwnershipStatusListById(int ownershipStatusId)
        {
            OwnershipStatusDTO ownershipStatusDetails = new OwnershipStatusDTO();
            AutoMapper.Mapper.Map(base.OwnershipStatusRepository.GetSingle(item => item.OwnershipStatus_Id == ownershipStatusId), ownershipStatusDetails);
            return ownershipStatusDetails;
        }

        /// <summary>
        /// Verify Business Type Exists or not by businessTypeId and businessType
        /// </summary>
        /// <param name="businessTypeId">Int32:businessTypeId</param>
        /// <param name="businessType">String:businessType</param>
        /// <returns></returns>
        bool IMasterService.IsBusinessTypeExists(int businessTypeId, string businessType)
        {
            BusinessTypeDTO businessTypeDetails = new BusinessTypeDTO();
            bool result = false;

            if (businessTypeId == 0)
            {
                AutoMapper.Mapper.Map(base.BusinessTypeRepository.GetSingle(item => item.BusinessType_Name == businessType 
                    && item.BusinessType_IsDeleted == false), businessTypeDetails);
            }
            else
            {
                AutoMapper.Mapper.Map(base.BusinessTypeRepository.GetSingle(item => item.BusinessType_Name == businessType 
                    && item.BusinessType_IsDeleted == false && item.BusinessType_Id != businessTypeId), 
                    businessTypeDetails);
            }

            if (businessTypeDetails.BusinessType_Id > 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Verify Ownership Status Exists or not by ownershipStatusId and ownershipStatus
        /// </summary>
        /// <param name="ownershipStatusId"></param>
        /// <param name="ownershipStatus"></param>
        /// <returns></returns>
        bool IMasterService.IsOwnershipStatusExists(int ownershipStatusId, string ownershipStatus)
        {
            OwnershipStatusDTO ownershipDetail = new OwnershipStatusDTO();
            bool result = false;

            if (ownershipStatusId == 0)
            {
                AutoMapper.Mapper.Map(base.OwnershipStatusRepository.GetSingle(item => item.OwnershipStatus_Name == ownershipStatus
                        && item.OwnershipStatus_IsDeleted == false), ownershipDetail);
            }
            else
            {
                AutoMapper.Mapper.Map(base.OwnershipStatusRepository.GetSingle(item => item.OwnershipStatus_Name == ownershipStatus
                        && item.OwnershipStatus_IsDeleted == false
                        && item.OwnershipStatus_Id != ownershipStatusId), ownershipDetail);
            }

            if (ownershipDetail.OwnershipStatus_Id > 0)
            {
                result = true;
            }
            return result;
        }

        void IMasterService.SaveAndUpdateBankDetails(BankDTO bankDetails)
        {
            bank bankEntity = new bank();
            using (TransactionScope transactionScope = new TransactionScope())
            {
                AutoMapper.Mapper.Map(bankDetails, bankEntity);

                if (bankDetails.Bank_Id == 0)
                {
                    base.BankRepository.Save(bankEntity);
                }
                else
                {
                    base.BankRepository.Update(bankEntity);
                }
                transactionScope.Complete();
            }
        }

        /// <summary>
        /// Get list of banks from database
        /// </summary>
        /// <returns>returns bank details, else blank datatype</returns>
        IList<BankDTO> IMasterService.GetBankDetails()
        {
            List<BankDTO> lstBankDTO = new List<BankDTO>();
            
            AutoMapper.Mapper.Map(base.BankRepository.GetQuery().Where(item => item.Bank_IsDeleted == false)
                    .OrderBy(order => order.Bank_Name).ToList(), lstBankDTO);
            return lstBankDTO;
        }

        void IMasterService.DeleteBank(int bankId)
        {
            bank bankEntity = base.BankRepository.GetSingle(f => f.Bank_Id == bankId);

            if (bankEntity.Bank_Id > 0)
            {
                bankEntity.Bank_IsDeleted = true;
                base.BankRepository.Update(bankEntity);
            }
        }

        /// <summary>
        /// Get bank details by bank id
        /// </summary>
        /// <param name="bankId">bank id to retreive bank details</param>
        /// <returns>returns BankDTO object</returns>
        BankDTO IMasterService.GetBanksDetailsById(int bankId)
        {
            BankDTO bankDTO = new BankDTO();
            AutoMapper.Mapper.Map(base.BankRepository.GetSingle(f => f.Bank_Id == bankId), bankDTO);
            return bankDTO;
        }

        /// <summary>
        /// Get list of active payment modes
        /// </summary>
        /// <param name="isGhatoCollection">Get Payment mode for Ghato Collection</param>
        /// <returns>returns customer details if exists, else blank datatype</returns>
        IList<PaymentModeDTO> IMasterService.GetListOfPaymentMode(bool isGhatoCollection)
        {
            List<PaymentModeDTO> lstPaymentMode = new List<PaymentModeDTO>();

            List<paymentmode> lstPaymentModeEntity = base.PaymentModeRepository.GetQuery().Where(item => item.Paymentmode_IsDeleted == false)
                    .OrderBy(order => order.Paymentmode_Name).ToList();

            if (isGhatoCollection)
            {
                lstPaymentModeEntity = lstPaymentModeEntity.Where(item => item.Paymentmode_IsGhatoCollection == true).ToList();
            }

            AutoMapper.Mapper.Map(lstPaymentModeEntity, lstPaymentMode);
            return lstPaymentMode;
        }

        /// <summary>
        /// Get all active rejection reasons from database
        /// </summary>
        /// <returns>list of rejection reasons</returns>
        IList<RejectionReasonDTO> IMasterService.GetRejectionReasons()
        {
            List<RejectionReasonDTO> lstRejectionsDTO = new List<RejectionReasonDTO>();                        
            AutoMapper.Mapper.Map(base.RejectionReasonRepository.GetQuery().Where(item => item.RR_IsDeleted == false)
                    .OrderBy(order => order.RR_Name).ToList(), lstRejectionsDTO);

            return lstRejectionsDTO;
        }
    }
}