// -----------------------------------------------------------------------
// <copyright file="GhatoCollectionBaseService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TCESS.ESales.BusinessLayer.Services.GhatoCollection
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
    public class GhatoCollectionBaseService : MarshalByRefObject
    {
        /// <summary>
        /// Property to inject the persistence layer implementation for Payment Collection
        /// </summary>
        [Dependency]
        public IGenericRepository<paymentcollection> PaymentCollectionRepository { get; set; }

        /// <summary>
        /// Property to inject the persistence layer implementation for Customer
        /// </summary>
        [Dependency]
        public IGenericRepository<customer> CustomerRepository { get; set; }

        /// <summary>
        /// Property to inject the persistence layer implementation for Payment modes
        /// </summary>
        [Dependency]
        public IGenericRepository<paymentmode> PaymentModeRepository { get; set; }

        /// <summary>
        /// Property to inject the persistence layer implementation for banks
        /// </summary>
        [Dependency]
        public IGenericRepository<bank> BankRepository { get; set; }

        /// <summary>
        /// Property to inject the persistence layer implementation for Batch transfers
        /// </summary>
        [Dependency]
        public IGenericRepository<batchtransfer> BatchTransferRepository { get; set; }

        /// <summary>
        /// Property to inject the persistence layer implementation for Counters
        /// </summary>
        [Dependency]
        public IGenericRepository<counter> CounterRepository { get; set; }

        /// <summary>
        /// Property to inject the persistence layer implementation for Counter details
        /// </summary>
        [Dependency]
        public IGenericRepository<counterdetail> CounterDetailRepository { get; set; }

        /// <summary>
        /// Property to inject the persistence layer implementation for Payment Transits
        /// </summary>
        [Dependency]
        public IGenericRepository<my_aspnet_users> UserRepository { get; set; }

        /// <summary>
        /// Property to inject the persistence layer implementation for Payment Transits
        /// </summary>
        [Dependency]
        public IGenericRepository<paymenttransit> PaymentTransitRepository { get; set; }

        /// <summary>
        /// Property to inject the persistence layer implementation for Payment Transits
        /// </summary>
        [Dependency]
        public IGenericRepository<vwnonchequepayment> NonChequePaymentRepository { get; set; }

        /// <summary>
        /// Property to inject the persistence layer implementation for Payment Transits
        /// </summary>
        [Dependency]
        public IGenericRepository<vwchequepayment> ChequePaymentRepository { get; set; }
        /// <summary>
        /// Property to inject the persistence layer implementation for Payment Transits
        /// </summary>
        [Dependency]
        public IGenericRepository<paymentrefund> PaymentRefundRepository { get; set; }
    }
}