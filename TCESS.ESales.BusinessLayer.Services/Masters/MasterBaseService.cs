// -----------------------------------------------------------------------
// <copyright file="MasterBaseService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TCESS.ESales.BusinessLayer.Services.Masters
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
    public class MasterBaseService : MarshalByRefObject
    {
        /// <summary>
        /// Property to inject the persistence layer implementation for BusinessType
        /// </summary>
        [Dependency]
        public IGenericRepository<businesstype> BusinessTypeRepository { get; set; }

        /// <summary>
        /// Property to inject the persistence layer implementation for OwnershipStatus
        /// </summary>
        [Dependency]
        public IGenericRepository<ownershipstatu> OwnershipStatusRepository { get; set; }

        /// <summary>
        /// Property to inject the persistence layer implementation for PaymentMode
        /// </summary>
        [Dependency]
        public IGenericRepository<paymentmode> PaymentModeRepository { get; set; }

        /// <summary>
        /// Property to inject the persistence layer implementation for Banks
        /// </summary>
        [Dependency]
        public IGenericRepository<bank> BankRepository { get; set; }

        /// <summary>
        /// Property to inject the persistence layer implementation for Banks
        /// </summary>
        [Dependency]
        public IGenericRepository<rejectionreason> RejectionReasonRepository { get; set; }
    }
}