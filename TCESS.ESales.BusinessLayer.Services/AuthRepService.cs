#region Using directives

using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.PersistenceLayer.Entity;
using TCESS.ESales.PersistenceLayer.Interfaces;

#endregion

namespace TCESS.ESales.BusinessLayer.Services
{
    public class AuthRepService : IAuthRepService
    {
        /// <summary>
        /// Will save the details of authorized representative 
        /// </summary>
        /// <param name="authRepDetails"></param>
        public int SaveAndUpdateAuthRepDetailsForCustomer(AuthRepDTO authRepDetails)
        {
            int authRepId = 0;
            authrepdetail authrepdetailEntity = new authrepdetail();
            AutoMapper.Mapper.Map(authRepDetails, authrepdetailEntity);

            if (authRepDetails.AuthRep_Id == 0)
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<authrepdetail>>().Save(authrepdetailEntity);
            }
            else
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<authrepdetail>>().Update(authrepdetailEntity);
            }

            authRepId = authrepdetailEntity.AuthRep_Id;

            //return the details
            return authRepId;
        }

        /// <summary>
        /// Save and Update AuthRepDoc Deatail
        /// </summary>
        /// <param name="listAuthRepDocDetails"></param>
        /// <param name="listAuthRepDocument"></param>
        public void SaveAndUpdateAuthRepDocDetails(IList<AuthRepDocDetailDTO> listAuthRepDocDetails, 
            IList<AuthRepDocumentsDTO> listAuthRepDocument)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                for (int i = 0; i < listAuthRepDocDetails.Count; i++)
                {
                    authrepdocdetail authrepdocdetailEntity = new authrepdocdetail();

                    AuthRepDocDetailDTO authRepDocDetails = GetAuthRepDocDetailsByAuthRepIdAndDocId(
                        listAuthRepDocDetails[i].AuthRep_Doc_AuthId, listAuthRepDocDetails[i].AuthRep_Doc_DocId);

                    if (authRepDocDetails.AuthRep_Doc_AuthId > 0)
                    {
                        AutoMapper.Mapper.Map(authRepDocDetails, authrepdocdetailEntity);

                        authrepdocdetailEntity.AuthRep_Doc_DocNo = listAuthRepDocDetails[i].AuthRep_Doc_DocNo;
                        authrepdocdetailEntity.AuthRep_Doc_FileName = listAuthRepDocDetails[i].AuthRep_Doc_FileName;
                        authrepdocdetailEntity.AuthRep_Doc_ExDate = listAuthRepDocDetails[i].AuthRep_Doc_ExDate;
                        ESalesUnityContainer.Container.Resolve<IGenericRepository<authrepdocdetail>>().Update(authrepdocdetailEntity);
                    }
                    else
                    {
                        AutoMapper.Mapper.Map(listAuthRepDocDetails[i], authrepdocdetailEntity);
                        ESalesUnityContainer.Container.Resolve<IGenericRepository<authrepdocdetail>>().Save(authrepdocdetailEntity);
                    }

                    AuthRepDocumentsDTO authRepDocument = GetAuthRepDocDetailsByDocId(authrepdocdetailEntity.AuthRep_Doc_Id);
                    authrepdocument authrepdocumentEntity = new authrepdocument();

                    if (authRepDocument.AuthRepDoc_Id > 0)
                    {
                        AutoMapper.Mapper.Map(authRepDocument, authrepdocumentEntity);

                        if (listAuthRepDocument[i].AuthRepDoc_File == null)
                        {
                            authrepdocdetailEntity.AuthRep_Doc_IsDeleted = true;
                        }
                        else
                        {
                            authrepdocumentEntity.AuthRepDoc_File = listAuthRepDocument[i].AuthRepDoc_File;
                        }

                        ESalesUnityContainer.Container.Resolve<IGenericRepository<authrepdocument>>().Update(authrepdocumentEntity);
                    }
                    else
                    {
                        AutoMapper.Mapper.Map(listAuthRepDocument[i], authrepdocumentEntity);

                        authrepdocumentEntity.AuthRepDoc_Doc_Id = authrepdocdetailEntity.AuthRep_Doc_Id;

                        ESalesUnityContainer.Container.Resolve<IGenericRepository<authrepdocument>>().Save(authrepdocumentEntity);
                    }
                }
                
                transactionScope.Complete();
            }
        }

        /// <summary>
        /// Get Auth Rep Details by customer Id
        /// </summary>
        /// <param name="custId"></param>
        /// <returns></returns>
        public IList<AuthRepDTO> GetAuthRepDetailsForCustomer(int custId)
        {
            List<AuthRepDTO> lstAuthRepDTO = new List<AuthRepDTO>();

            List<authrepdetail> lstAuthRepEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<authrepdetail>>()
                .GetQuery().Where(item => item.AuthRep_CustomerId == custId && item.AuthRep_IsDeleted == false).ToList();

            AutoMapper.Mapper.Map(lstAuthRepEntity, lstAuthRepDTO);
			
            //return the value
            return lstAuthRepDTO;
        }

        /// <summary>
        /// Get Auth Rep  by authRepId
        /// </summary>
        /// <param name="authRepId"></param>
        /// <returns></returns>
        public AuthRepDTO GetAuthRepById(int authRepId)
        {
            AuthRepDTO objAuthRepDTO = new AuthRepDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<authrepdetail>>()
                .GetSingle(item => item.AuthRep_Id == authRepId), objAuthRepDTO);

            //return the value
            return objAuthRepDTO;
        }

        /// <summary>
        /// Get Auth Rep Doc Details By AuthRepId
        /// </summary>
        /// <param name="authRepId"></param>
        /// <returns></returns>
        public IList<AuthRepDocDetailDTO> GetAuthRepDocDetailsByAuthRepId(int authRepId)
        {
            List<AuthRepDocDetailDTO> objAuthRepDocDetailDTO = new List<AuthRepDocDetailDTO>();

            List<authrepdocdetail> lstAuthRepDocsEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<authrepdocdetail>>().GetQuery()
               .Where(item => item.AuthRep_Doc_AuthId == authRepId).ToList();

            AutoMapper.Mapper.Map(lstAuthRepDocsEntity, objAuthRepDocDetailDTO);

            //return the value
            return objAuthRepDocDetailDTO;
        }

        /// <summary>
        /// Get Auth Rep Doc Details By DocId
        /// </summary>
        /// <param name="authRepDocId"></param>
        /// <returns></returns>
        public AuthRepDocumentsDTO GetAuthRepDocDetailsByDocId(int authRepDocId)
        {
            AuthRepDocumentsDTO authRepDocument = new AuthRepDocumentsDTO();

            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<authrepdocument>>()
            .GetSingle(item => item.AuthRepDoc_Doc_Id == authRepDocId && item.AuthRepDoc_IsDeleted == false), authRepDocument);

            return authRepDocument;
        }

        /// <summary>
        /// Delete Auth Rep Doc Details
        /// </summary>
        /// <param name="authRepDocs"></param>
        private static void DeleteAuthRepDocDetails(AuthRepDocDetailDTO authRepDocs)
        {
            authrepdocdetail authRepDocEntity = new authrepdocdetail();
            AutoMapper.Mapper.Map(authRepDocs, authRepDocEntity);

            ESalesUnityContainer.Container.Resolve<IGenericRepository<authrepdocdetail>>().Update(authRepDocEntity);
        }

        /// <summary>
        /// Delete Auth Rep
        /// </summary>
        /// <param name="truckDetails"></param>
        public void DeleteAuthRep(AuthRepDTO authRepDetails)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                IList<AuthRepDocDetailDTO> lstAuthRepDocDetails = GetAuthRepDocDetailsByAuthRepId(authRepDetails.AuthRep_Id);

                (from authRepDocDetail in lstAuthRepDocDetails select authRepDocDetail).Update(
                    authRepDocDetail => authRepDocDetail.AuthRep_Doc_IsDeleted = true);

                foreach (var authRepDocs in lstAuthRepDocDetails)
                {
                    DeleteAuthRepDocDetails(authRepDocs);
                }

                authrepdetail authRepEntity = new authrepdetail();
                AutoMapper.Mapper.Map(authRepDetails, authRepEntity);

                ESalesUnityContainer.Container.Resolve<IGenericRepository<authrepdetail>>().Update(authRepEntity);

                transactionScope.Complete();
            }
        }

        /// <summary>
        /// Get Auth Rep Doc Details By AuthRepId And DocId
        /// </summary>
        /// <param name="authRepId"></param>
        /// <param name="docId"></param>
        /// <returns></returns>
        public AuthRepDocDetailDTO GetAuthRepDocDetailsByAuthRepIdAndDocId(int authRepId, int docId)
        {
            AuthRepDocDetailDTO authRepDocDetails = new AuthRepDocDetailDTO();

            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<authrepdocdetail>>()
           .GetSingle(item => item.AuthRep_Doc_DocId == docId && item.AuthRep_Doc_AuthId == authRepId
               && item.AuthRep_Doc_IsDeleted == false), authRepDocDetails);

            //return value
            return authRepDocDetails;
        } 

        /// <summary>
        /// To check AuthRep Document No Exists or not
        /// </summary>
        /// <param name="authRepDocId"></param>
        /// <param name="docId"></param>
        /// <param name="docNo"></param>
        /// <returns></returns>
        public bool AuthRepDocumentNoExists(int authRepDocId, int docId, string docNo)
        {
            AuthRepDocDetailDTO objAuthRepDocDetailDTO = new AuthRepDocDetailDTO();
            if (authRepDocId > 0)
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<authrepdocdetail>>().GetSingle
                    (item => item.AuthRep_Doc_Id != authRepDocId && item.AuthRep_Doc_DocId == docId && item.AuthRep_Doc_DocNo  == docNo &&
                        item.AuthRep_Doc_IsDeleted == false), objAuthRepDocDetailDTO);
            }
            else
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<authrepdocdetail>>().GetSingle
                     (item => item.AuthRep_Doc_DocId == docId && item.AuthRep_Doc_DocNo == docNo &&
                         item.AuthRep_Doc_IsDeleted == false), objAuthRepDocDetailDTO);
            }
            return objAuthRepDocDetailDTO.AuthRep_Doc_Id > 0 ? true : false;          
        }
        
        /// <summary>
        /// function to check if dublicacy while creating new Authorize Representative.
        /// </summary>
        /// <param name="authRepName"></param>
        /// <returns></returns>
        public AuthRepDTO GetAuthRepByName(string authRepName)
        {
            AuthRepDTO objAuthRepDTO = new AuthRepDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<authrepdetail>>()
                .GetSingle(item => item.AuthRep_Name == authRepName && item.AuthRep_IsDeleted == false), objAuthRepDTO);

            //return the value
            return objAuthRepDTO;
        }      
    }
}