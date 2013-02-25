// -----------------------------------------------------------------------
// <copyright file="UserBaseService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TCESS.ESales.BusinessLayer.Services.Users
{
    #region Using directives

    using System;
    using Microsoft.Practices.Unity;
    using TCESS.ESales.PersistenceLayer.Entity;
    using TCESS.ESales.PersistenceLayer.Interfaces;

    #endregion

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UserBaseService : MarshalByRefObject
    {
        /// <summary>
        /// Property to inject the persistence layer implementation
        /// </summary>
        [Dependency]
        public IGenericRepository<userpaymentmodemapping> UserPaymentModeRepository { get; set; }

        /// <summary>
        /// Property to inject the persistence layer implementation
        /// </summary>
        [Dependency]
        public IGenericRepository<paymentmode> PaymentModeRepository { get; set; }
    }
}