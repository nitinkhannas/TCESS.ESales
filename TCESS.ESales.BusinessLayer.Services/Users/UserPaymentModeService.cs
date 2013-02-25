// -----------------------------------------------------------------------
// <copyright file="UserPaymentModeService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TCESS.ESales.BusinessLayer.Services.Users
{
    #region Using directives

    using TCESS.ESales.BusinessLayer.Interfaces.Users;
    using TCESS.ESales.DataTransferObjects.Users;
    using TCESS.ESales.CommonLayer.Unity;
    using TCESS.ESales.PersistenceLayer.Interfaces;
    using Microsoft.Practices.Unity;
    using TCESS.ESales.PersistenceLayer.Entity;
    using System.Collections.Generic;
    using System.Transactions;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UserPaymentModeService : UserBaseService, IUserPaymentModeService
    {
        /// <summary>
        /// Get user payment mode mapping by user id
        /// </summary>
        /// <param name="userPaymentModeId"></param>
        /// <returns></returns>
        IList<UserPaymentModeMappingDTO> IUserPaymentModeService.GetPaymentModesByUserId(int userId)
        {
            //To select agent by agent id
            UserPaymentModeMappingDTO userPaymentModeDetails = new UserPaymentModeMappingDTO();

            IList<UserPaymentModeMappingDTO> lstUserPaymentMapping = (from upmItem in base.UserPaymentModeRepository.GetQuery().
                                                                          Where(item => item.UPM_UserId == userId)
                                                                      join payItem in base.PaymentModeRepository.GetQuery()
                                                                      on upmItem.UPM_PaymentMode equals payItem.Paymentmode_Id into item
                                                                      from subItem in item.DefaultIfEmpty()
                                                                      select new UserPaymentModeMappingDTO
                                                                      {
                                                                          UPM_Id = upmItem.UPM_Id,
                                                                          UPM_UserId = upmItem.UPM_UserId,
                                                                          UPM_PaymentMode = upmItem.UPM_PaymentMode,
                                                                          PaymentModeName = subItem == null ? string.Empty : subItem.Paymentmode_Name
                                                                      }).ToList();            
            //return agent details
            return lstUserPaymentMapping;
        }

        /// <summary>
        /// Save or update user payment mode details
        /// </summary>
        /// <param name="userPaymentModeMapDTO"></param>
        void IUserPaymentModeService.SaveUserPaymentModeDetails(IList<UserPaymentModeMappingDTO> lstUserPaymentModeMap)
        {
            IList<userpaymentmodemapping> lstUserPaymentModeMapEntity = new List<userpaymentmodemapping>();
            AutoMapper.Mapper.Map(lstUserPaymentModeMap, lstUserPaymentModeMapEntity);
            
            using (TransactionScope transactionScope = new TransactionScope())
            {
                foreach (userpaymentmodemapping item in lstUserPaymentModeMapEntity)
                {
                    base.UserPaymentModeRepository.Save(item);
                }
                transactionScope.Complete();
            }
        }

        void IUserPaymentModeService.DeleteUserPaymentModeDetails(int userId)
        {
            IList<userpaymentmodemapping> lstUserPaymentModeMappingEntity = base.UserPaymentModeRepository.GetQuery()
                .Where(item => item.UPM_UserId == userId).ToList();
            
            foreach (userpaymentmodemapping item in lstUserPaymentModeMappingEntity)
            {
                base.UserPaymentModeRepository.Delete(item);
            }
        }

        IList<int> IUserPaymentModeService.GetUserPaymentModesByUserId(int userId)
        {
            List<int> lstPaymentModes = base.UserPaymentModeRepository.GetQuery().Where(item => item.UPM_UserId  == userId)
                    .Select(item => item.UPM_PaymentMode).ToList();

            return lstPaymentModes;
        }
    }
}