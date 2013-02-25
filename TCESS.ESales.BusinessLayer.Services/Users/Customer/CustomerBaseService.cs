// -----------------------------------------------------------------------
// <copyright file="CustomerBaseService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TCESS.ESales.BusinessLayer.Services.Customer
{
    #region Using directives

    using Microsoft.Practices.Unity;
    using TCESS.ESales.PersistenceLayer.Entity;
    using TCESS.ESales.PersistenceLayer.Interfaces;

    #endregion

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CustomerBaseService
    {
        [Dependency]
        public IGenericRepository<customer> CustomerRepository { get; set; }

        [Dependency]
        public IGenericRepository<customerdocdetail> CustomerDocRepository { get; set; }
    }
}