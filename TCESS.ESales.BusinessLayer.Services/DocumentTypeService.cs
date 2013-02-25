#region Using directives

using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.BusinessLayer.Services.Documents;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.PersistenceLayer.Entity;
using TCESS.ESales.PersistenceLayer.Interfaces;

#endregion

namespace TCESS.ESales.BusinessLayer.Services
{
    public class DocumentTypeService : DocumentsBaseService, IDocumentTypeService
    {
        /// <summary>
        /// Get list of all document required during customer registration
        /// </summary>
        /// <returns>returns documentlist required during customer registration</returns>
        public IList<DocTypeDTO> GetDocumentTypeListForCustomers()
        {
            List<DocTypeDTO> lstCustomerDocType = new List<DocTypeDTO>();

            List<doctype> lstCustomerDocTypesEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<doctype>>()
                .GetQuery().Where(item => item.Doc_IsDeleted == false && item.Doc_Group == 1).ToList();

            AutoMapper.Mapper.Map(lstCustomerDocTypesEntity, lstCustomerDocType);

            //return the value
            return lstCustomerDocType;
        }

        /// <summary>
        /// Get list of all documents required during Truck registration
        /// </summary>
        /// <returns>A list of documents required Truck registration</returns>
        public IList<DocTypeDTO> GetDocumentTypeListForTrucks()
        {
            List<DocTypeDTO> lstTruckDocType = new List<DocTypeDTO>();

            List<doctype> lstTruckDocTypesEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<doctype>>()
                .GetQuery().Where(item => item.Doc_IsDeleted == false && item.Doc_Group == 2).ToList();

            AutoMapper.Mapper.Map(lstTruckDocTypesEntity, lstTruckDocType);

            //return the value
            return lstTruckDocType;
        }

        /// <summary>
        /// Get list of all documents required during auth rep registration
        /// </summary>
        /// <returns>A list of documents required  auth rep registration</returns>
        public IList<DocTypeDTO> GetDocumentTypeListForAuthRep()
        {
            List<DocTypeDTO> lstAuthRepDocType = new List<DocTypeDTO>();

            List<doctype> lstAuthRepDocTypesEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<doctype>>()
                .GetQuery().Where(item => item.Doc_IsDeleted == false && item.Doc_Group == 3).ToList();

            AutoMapper.Mapper.Map(lstAuthRepDocTypesEntity, lstAuthRepDocType);

            //return the value
            return lstAuthRepDocType;
        }

        /// <summary>
        /// Get Unique Document Type List
        /// </summary>
        /// <returns></returns>
        public IList<DocTypeDTO> GetUniqueDocumentTypeList()
        {
            List<DocTypeDTO> lstUniqueDocType = new List<DocTypeDTO>();

            List<doctype> lstUniqueDocTypesEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<doctype>>()
                .GetQuery().Where(item => item.Doc_IsDeleted == false && item.Doc_Group == 1
                    && item.Doc_IsUnique == true).ToList();

            AutoMapper.Mapper.Map(lstUniqueDocTypesEntity, lstUniqueDocType);

            //return the value
            return lstUniqueDocType;
        }

        /// <summary>
        /// Save Cust Document Type Info
        /// </summary>
        /// <param name="docTypeDetails"></param>
        /// <returns></returns>
        public int SaveCustDocumentTypeInfo(DocTypeDTO docTypeDetails)
        {
            doctype doctypeEntity = new doctype();
            AutoMapper.Mapper.Map(docTypeDetails, doctypeEntity);

            ESalesUnityContainer.Container.Resolve<IGenericRepository<doctype>>().Save(doctypeEntity);

            //return value
            return doctypeEntity.Doc_Id;
        }

        /// <summary>
        /// Update Customer Document Type Info
        /// </summary>
        /// <param name="docTypeDetails"></param>
        /// <returns></returns>
        public int UpdateCustomerDocumentTypeInfo(DocTypeDTO docTypeDetails)
        {
            doctype doctypeEntity = new doctype();
            AutoMapper.Mapper.Map(docTypeDetails, doctypeEntity);

            ESalesUnityContainer.Container.Resolve<IGenericRepository<doctype>>().Update(doctypeEntity);

            //return value
            return doctypeEntity.Doc_Id;
        }

        /// <summary>
        /// Get Document TypeList By DocGroupId
        /// </summary>
        /// <param name="groupId">Int32:groupId</param>
        /// <returns></returns>
        public IList<DocTypeDTO> GetDocumentTypeListByDocGroupId(int groupId)
        {
            List<DocTypeDTO> lstDocType = new List<DocTypeDTO>();

            List<doctype> listDocTypeEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<doctype>>().GetQuery()
               .Where(item => item.Doc_IsDeleted == false && item.Doc_Group == groupId).ToList();

            AutoMapper.Mapper.Map(listDocTypeEntity, lstDocType);

            //return the value
            return lstDocType;
        }

        /// <summary>
        /// Get Document Type List By DocId
        /// </summary>
        /// <param name="documentId">Int32:documentId</param>
        /// <returns></returns>
        public DocTypeDTO GetDocumentTypeListByDocId(int documentId)
        {
            DocTypeDTO docTypeDetails = new DocTypeDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<doctype>>().GetSingle(
               item => item.Doc_IsDeleted == false && item.Doc_Id == documentId), docTypeDetails);

            //return the value
            return docTypeDetails;
        }

        /// <summary>
        /// verify Doc type exists or not by groupId,docTypeId and docTypeName
        /// </summary>
        /// <param name="groupId">Int32:groupId</param>
        /// <param name="docTypeId">int32:docTypeId</param>
        /// <param name="docTypeName">String:docTypeName</param>
        /// <returns></returns>
        public bool DocTypeExists(int groupId, int docTypeId, string docTypeName)
        {
            DocTypeDTO docTypeDetails = new DocTypeDTO();
            bool result = false;

            if (docTypeId == 0)
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<doctype>>()
                    .GetSingle(item => item.Doc_Group == groupId && item.Doc_Name == docTypeName && item.Doc_IsDeleted == false), docTypeDetails);
            }
            else
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<doctype>>()
                    .GetSingle(item => item.Doc_Group == groupId && item.Doc_Id != docTypeId
                        && item.Doc_Name == docTypeName && item.Doc_IsDeleted == false), docTypeDetails);
            }

            if (docTypeDetails.Doc_Id > 0)
            {
                result = true;
            }

            //return the value
            return result;
        }

        /// <summary>
        /// Get Truck Document Type List
        /// </summary>
        /// <returns></returns>
        public IList<DocTypeDTO> GetTruckDocumentTypeList()
        {
            List<DocTypeDTO> lstTruckDocType = new List<DocTypeDTO>();

            List<doctype> lstTruckDocTypesEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<doctype>>()
                .GetQuery().Where(item => item.Doc_IsDeleted == false && item.Doc_Group == 2).ToList();

            AutoMapper.Mapper.Map(lstTruckDocTypesEntity, lstTruckDocType);

            return lstTruckDocType;
        }

        /// <summary>
        /// Get Document Type List for Ghato payment collection
        /// </summary>
        /// <returns></returns>
        public IList<DocTypeDTO> GetDocumentTypeForGhatoCollection()
        {
            List<DocTypeDTO> lstGhatoCollectionDocTypeDTO = new List<DocTypeDTO>();

            List<doctype> lstGhatoCollectionDocTypesEntity = base.DocumentRepository.GetQuery().Where(item => item.Doc_IsDeleted == false 
                && item.Doc_Group == 1 && item.Doc_IsGhatoCollection == true).ToList();

            AutoMapper.Mapper.Map(lstGhatoCollectionDocTypesEntity, lstGhatoCollectionDocTypeDTO);

            //return the value
            return lstGhatoCollectionDocTypeDTO;
        }
    }
}