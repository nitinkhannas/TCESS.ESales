#region Using directives

using System.Collections.Generic;
using TCESS.ESales.DataTransferObjects;

#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface IDocumentTypeService
    {
        int SaveCustDocumentTypeInfo(DocTypeDTO docTypeDetails);
        int UpdateCustomerDocumentTypeInfo(DocTypeDTO docTypeDetails);
        IList<DocTypeDTO> GetDocumentTypeListByDocGroupId(int groupId);
        DocTypeDTO GetDocumentTypeListByDocId(int documentId);
        bool DocTypeExists(int groupId, int docTypeId, string docTypeName);
        IList<DocTypeDTO> GetDocumentTypeListForCustomers();
        IList<DocTypeDTO> GetDocumentTypeListForTrucks();
        IList<DocTypeDTO> GetDocumentTypeListForAuthRep();
        IList<DocTypeDTO> GetUniqueDocumentTypeList();
        IList<DocTypeDTO> GetTruckDocumentTypeList();

        /// <summary>
        /// Get Document Type List for Ghato payment collection
        /// </summary>
        /// <returns></returns>
        IList<DocTypeDTO> GetDocumentTypeForGhatoCollection();
    }
}