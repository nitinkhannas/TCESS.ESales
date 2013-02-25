// -----------------------------------------------------------------------
// <copyright file="CustomerDocumentBaseService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TCESS.ESales.BusinessLayer.Services.Documents
{
    #region Using directives

    using Microsoft.Practices.Unity;
    using TCESS.ESales.PersistenceLayer.Entity;
    using TCESS.ESales.PersistenceLayer.Interfaces;

    #endregion

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DocumentsBaseService
    {
        [Dependency]
        public IGenericRepository<doctype> DocumentRepository { get; set; }
    }
}