// -----------------------------------------------------------------------
// <copyright file="IUserPaymentModeService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TCESS.ESales.BusinessLayer.Interfaces.Users
{
    #region Using directives

    using TCESS.ESales.DataTransferObjects.Users;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IUserPaymentModeService
    {
        /// <summary>
        /// Get user payment mode mapping by user id
        /// </summary>
        /// <param name="userPaymentModeId"></param>
        /// <returns></returns>
        IList<UserPaymentModeMappingDTO> GetPaymentModesByUserId(int userPaymentModeId);

        /// <summary>
        /// Save or update user payment mode details
        /// </summary>
        /// <param name="userPaymentModeMapDTO"></param>
        void SaveUserPaymentModeDetails(IList<UserPaymentModeMappingDTO> lstUserPaymentModeMap);

        IList<int> GetUserPaymentModesByUserId(int userId);

        void DeleteUserPaymentModeDetails(int userId);
    }
}