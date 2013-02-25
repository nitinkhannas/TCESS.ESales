#region Using directives

using System.Collections.Generic;
using TCESS.ESales.DataTransferObjects;

#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface IAuthRepService
    {
        int SaveAndUpdateAuthRepDetailsForCustomer(AuthRepDTO authRepDetails);
        IList<AuthRepDTO> GetAuthRepDetailsForCustomer(int custId);
        AuthRepDTO GetAuthRepById(int authRepId);
        AuthRepDTO GetAuthRepByName(string authRepName);
        void SaveAndUpdateAuthRepDocDetails(IList<AuthRepDocDetailDTO> lstAuthRepDocDetails, 
            IList<AuthRepDocumentsDTO> listAuthRepDocument);
        void DeleteAuthRep(AuthRepDTO authRepDetails);

        IList<AuthRepDocDetailDTO> GetAuthRepDocDetailsByAuthRepId(int authRepId);
        AuthRepDocDetailDTO GetAuthRepDocDetailsByAuthRepIdAndDocId(int authRepId,int docId);
        
        AuthRepDocumentsDTO GetAuthRepDocDetailsByDocId(int authRepDocId);
        bool AuthRepDocumentNoExists(int authRepDocId, int docId, string docNo);
        
    }
}