#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.PersistenceLayer.Entity;
using TCESS.ESales.PersistenceLayer.Interfaces;

#endregion

namespace TCESS.ESales.BusinessLayer.Services
{
    public class MembershipService : IMembershipService
    {
        /// <summary>
        /// Get RoleId By UserId
        /// </summary>
        /// <param name="userId">Int32:UserId</param>
        /// <returns></returns>
        public int GetRoleIdByUserId(int userId)
        {
            UsersInRolesDTO userInRoles = new UsersInRolesDTO();

            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<my_aspnet_usersinroles>>()
                .GetSingle(item => item.userId == userId), userInRoles);

            //return Role Id
            return userInRoles.RoleId;
        }

        /// <summary>
        /// Get Page Info By RoleId
        /// </summary>
        /// <param name="userId">Int32:RoleId</param>
        /// <returns></returns>
        public IList<PagesInRoleDTO> GetPageInfoByRoleId(int roleId)
        {
            List<PagesInRoleDTO> lstPagesInRole = new List<PagesInRoleDTO>();

            List<pagesinrole> lstPagesInRolesEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<pagesinrole>>()
                .GetQuery().Where(item => item.Page_Role_RoleId == roleId && item.Page_Role_IsActive==true && item.Page_Role_IsDeleted==false).ToList();
            
            AutoMapper.Mapper.Map(lstPagesInRolesEntity, lstPagesInRole);

            //return Role Id
            return lstPagesInRole;
        }

        /// <summary>
        /// Delete User In Roles by userID
        /// </summary>
        /// <param name="userId"></param>
        public void DeleteUserInRoles(int userId)
        {
            my_aspnet_usersinroles userInRoleEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<my_aspnet_usersinroles>>().GetSingle(item => item.userId == userId);
            ESalesUnityContainer.Container.Resolve<IGenericRepository<my_aspnet_usersinroles>>().Delete(userInRoleEntity);
        }

        /// <summary>
        /// Get Parent Menu Items
        /// </summary>
        /// <param name="lstPagesInRole"></param>
        /// <returns></returns>
        public IList<PageInfoDTO> GetParentMenuItems(IList<PagesInRoleDTO> lstPagesInRole)
        {
            List<PageInfoDTO> lstPageInfo = new List<PageInfoDTO>();

            List<pageinfo> lstPageInfoEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<pageinfo>>().GetQuery()
                .Where(item => item.Page_ParentPageLevelId == 0 && item.Page_IsActive == true && item.Page_IsDeleted == false)
                .OrderBy(item => item.Page_Level).ToList();
            
            AutoMapper.Mapper.Map(lstPageInfoEntity, lstPageInfo);

            lstPageInfo = (from pages in lstPageInfo join pagesInRoles in lstPagesInRole
                           on pages.Page_Id equals pagesInRoles.Page_Role_PageId
                           select pages).ToList();

            //return Role Id
            return lstPageInfo;
        }

        /// <summary>
        /// Get Child Menu Items
        /// </summary>
        /// <param name="lstPagesInRole"></param>
        /// <returns></returns>
        public IList<PageInfoDTO> GetChildMenuItems(IList<PagesInRoleDTO> lstPagesInRole)
        {
            List<PageInfoDTO> lstPageInfo = new List<PageInfoDTO>();

            List<pageinfo> lstPageInfoEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<pageinfo>>().GetQuery()
                .Where(item => item.Page_ParentPageLevelId > 0 && item.Page_IsActive == true && item.Page_IsDeleted == false)
                .OrderBy(item => item.Page_Level).ToList();

            AutoMapper.Mapper.Map(lstPageInfoEntity, lstPageInfo);

            lstPageInfo = (from pages in lstPageInfo
                           join pagesInRoles in lstPagesInRole 
                               on pages.Page_Id equals pagesInRoles.Page_Role_PageId
                           where pagesInRoles.Page_Role_IsDeleted == false && pagesInRoles.Page_Role_IsActive==true
                           select pages).ToList();

            //return Role Id
            return lstPageInfo;
        }

        /// <summary>
        /// Get Pages In Role
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<PagesInRoleDTO> GetPagesInRole(int roleId)
        {
            List<PagesInRoleDTO> lstPagesInRole = new List<PagesInRoleDTO>();

            List<pagesinrole> lstPagesInRoleEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<pagesinrole>>()
                .GetQuery().Where(item => item.Page_Role_RoleId == roleId && item.Page_Role_IsDeleted == false).ToList();

            AutoMapper.Mapper.Map(lstPagesInRoleEntity, lstPagesInRole);

            List<PageInfoDTO> lstPageInfo = new List<PageInfoDTO>();

            List<pageinfo> lstPageInfoEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<pageinfo>>().GetQuery()
                .Where(item => item.Page_ParentPageLevelId > 0 && item.Page_IsActive == true && item.Page_IsDeleted == false)
                .OrderBy(item => item.Page_Level).ToList();

            AutoMapper.Mapper.Map(lstPageInfoEntity, lstPageInfo);

            lstPagesInRole = (from childPages in lstPageInfo
                              join pagesInRoles in lstPagesInRole 
                              on childPages.Page_Id equals pagesInRoles.Page_Role_PageId                                                    
                              select new PagesInRoleDTO
                              {
                                  Page_Role_Id = pagesInRoles.Page_Role_Id,
                                  Page_Role_PageId = childPages.Page_Id,
                                  Page_Role_RoleId = pagesInRoles.Page_Role_Id,
                                  Page_Role_PageName = childPages.Page_Name,
                                  Page_Role_IsActive = pagesInRoles.Page_Role_IsActive
                              }).OrderBy(order=>order.Page_Role_PageName).ToList();
            
            //return Role Id
            return lstPagesInRole;
        }

        /// <summary>
        /// Get RoleId By RoleName
        /// </summary>
        /// <param name="roleName">string:rolename</param>
        /// <returns></returns>
        public int GetRoleIdByRoleName(string roleName)
        {
            RolesDTO roles = new RolesDTO();

            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<my_aspnet_roles>>()
                .GetSingle(item => item.name == roleName), roles);

            //return Role Id
            return roles.Id;
        }

        /// <summary>
        /// Get All Pages by roleId
        /// </summary>
        /// <param name="roleId">Int32:roleId</param>
        /// <returns></returns>
        public IList<PageInfoDTO> GetAllPages(int roleId)
        {
            //To retrive all agents of the system
            List<PageInfoDTO> lstPageDetails = new List<PageInfoDTO>();
            
            List<pageinfo> lstPageDetailsEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<pageinfo>>()
                .GetQuery().Where(item => item.Page_IsDeleted == false && item.Page_IsActive == true 
                    && item.Page_ParentPageLevelId > 0).ToList();

            AutoMapper.Mapper.Map(lstPageDetailsEntity, lstPageDetails);

            IList<PagesInRoleDTO> lstPagesInRoleDetails = new List<PagesInRoleDTO>();

            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<pagesinrole>>()
                .RetrieveAll(), lstPagesInRoleDetails);

            var pg = from pages in lstPageDetails
                        where !(from pageinrole in lstPagesInRoleDetails
                                where (pageinrole.Page_Role_RoleId == roleId && pageinrole.Page_Role_IsDeleted==false)
                                select pageinrole.Page_Role_PageId).Any(tagId => tagId == pages.Page_Id)
                       select pages;

            return pg.OrderBy(order => order.Page_Name).ToList();
        }

        /// <summary>
        /// Add Pages To Roles
        /// </summary>
        /// <param name="pagesInRolesDetails"></param>
        public void AddPagesToRoles(PagesInRoleDTO pagesInRolesDetails)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                pagesinrole pagesInRoleEntity = new pagesinrole();
                AutoMapper.Mapper.Map(pagesInRolesDetails, pagesInRoleEntity);
                ESalesUnityContainer.Container.Resolve<IGenericRepository<pagesinrole>>().Save(pagesInRoleEntity);

                AddDynamicParentItems(pagesInRolesDetails.Page_Role_PageId, pagesInRolesDetails.Page_Role_RoleId, pagesInRolesDetails.Page_Role_IsActive);

                transactionScope.Complete();
            }
        }

        /// <summary>
        /// Add Dynamic Parent Items by pageId,roleId and isActive
        /// </summary>
        /// <param name="pageId">Int32:pageId</param>
        /// <param name="roleId">Int32:roleId</param>
        /// <param name="isActive">Bool:isActive</param>
        private void AddDynamicParentItems(int pageId, int roleId,bool isActive)
        {
            PageInfoDTO page = GetPageDetailsByPageId(pageId);

            if (page.Page_ParentPageLevelId > 0)
            {
                PagesInRoleDTO pageRoleDetails = GetPageDetailsByParentPageIdNRoleId(page.Page_ParentPageLevelId, roleId);
                PagesInRoleDTO parentPagesDetails = new PagesInRoleDTO();
                pagesinrole parentPagesEntity = new pagesinrole();
                parentPagesDetails.Page_Role_PageId = page.Page_ParentPageLevelId;
                parentPagesDetails.Page_Role_IsActive = isActive;
                parentPagesDetails.Page_Role_RoleId = roleId;
                parentPagesDetails.Page_Role_CreatedDate = DateTime.Now;

                AutoMapper.Mapper.Map(parentPagesDetails, parentPagesEntity);
                if (pageRoleDetails.Page_Role_PageId == 0)
                {                    
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<pagesinrole>>().Save(parentPagesEntity);
                }

                AddDynamicParentItems(page.Page_ParentPageLevelId, roleId,page.Page_IsActive);
                UpdateDynamicParentItemsStatus(pageId, roleId, isActive);
            }
        }

        /// <summary>
        /// Update Dynamic Parent Items Status by pageId,roleId and isActive
        /// </summary>
        /// <param name="pageId">Int32:pageId</param>
        /// <param name="roleId">Int32:roleId</param>
        /// <param name="isActive">bool:isActive</param>
        public void UpdateDynamicParentItemsStatus(int pageId, int roleId, bool isActive)
        {
            PageInfoDTO page = GetPageDetailsByPageId(pageId);

            if (pageId != 0)
            {
                List<PageInfoDTO> lstPageInfoDTO = new List<PageInfoDTO>();
                List<PagesInRoleDTO> lstPageInRole = new List<PagesInRoleDTO>();

                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<pagesinrole>>().GetQuery().Where(item => item.Page_Role_IsActive == true
                   && item.Page_Role_IsDeleted == false && item.Page_Role_RoleId == roleId && item.Page_Role_PageId != pageId), lstPageInRole);

                List<pageinfo> lstPageInfoEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<pageinfo>>().GetQuery()
                     .Where(item => item.Page_ParentPageLevelId == page.Page_ParentPageLevelId && item.Page_IsActive == true && item.Page_IsDeleted == false)
                     .OrderBy(item => item.Page_Level).ToList();
                AutoMapper.Mapper.Map(lstPageInfoEntity, lstPageInfoDTO);


                List<pageinfo> result = new List<pageinfo>();
                result = (from pg in lstPageInfoEntity
                          where (from role in lstPageInRole select role.Page_Role_PageId).Contains(pg.Page_Id)
                          select pg).ToList();

                if (result.Count == 0)
                {
                    PagesInRoleDTO pageRoleDetails = GetPageDetailsByParentPageIdNRoleId(page.Page_ParentPageLevelId, roleId);
                    pageRoleDetails.Page_Role_IsActive = isActive;
                    pageRoleDetails.Page_Role_LastUpdatedDate = DateTime.Now;
                    pagesinrole pageInRoleEntity = new pagesinrole();
                    AutoMapper.Mapper.Map(pageRoleDetails, pageInRoleEntity);
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<pagesinrole>>().Update(pageInRoleEntity);
                    UpdateDynamicParentItemsStatus(page.Page_ParentPageLevelId, roleId, isActive);
                }
            }
        }

        /// <summary>
        /// Get Page Details By Parent PageId and RoleId
        /// </summary>
        /// <param name="pageId">Int32:pageId</param>
        /// <param name="RoleId">int32:RoleId</param>
        /// <returns></returns>
        public PagesInRoleDTO GetPageDetailsByParentPageIdNRoleId(int pageId, int RoleId)
        {
            //To select agent by agent id
            PagesInRoleDTO pagesInRoleDetails = new PagesInRoleDTO();

            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<pagesinrole>>()
                .GetSingle(item => item.Page_Role_PageId == pageId && item.Page_Role_RoleId == RoleId
            && item.Page_Role_IsDeleted == false), pagesInRoleDetails);
                       
            //return the value
            return pagesInRoleDetails;
        }

        /// <summary>
        /// verify If Page Exists InRole or not by PageId and RoleId
        /// </summary>
        /// <param name="pageId">Int32:pageId</param>
        /// <param name="roleId">int32:roleId</param>
        /// <returns></returns>
        public bool CheckIfPageExistsInRole(int pageId, int roleId)
        {
            bool isPageExists = false;
            PagesInRoleDTO pagesInRoles = new PagesInRoleDTO();

            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<pagesinrole>>()
                .GetSingle(item => item.Page_Role_PageId == pageId && item.Page_Role_RoleId == roleId 
                    && item.Page_Role_IsDeleted == false && item.pageinfo.Page_IsActive == true), pagesInRoles);

            //return Role Id
            if (pagesInRoles.Page_Role_Id > 0)
            {
                isPageExists = true;
            }

            //return the value
            return isPageExists;
        }

        /// <summary>
        /// Get Page Role Details By PageRoleId
        /// </summary>
        /// <param name="pageRoleId">Int32:pageRoleId</param>
        /// <returns></returns>
        public PagesInRoleDTO GetPageRoleDetailsByPageRoleId(int pageRoleId)
        {
            //To select agent by agent id
            PagesInRoleDTO pageRoleDTO = new PagesInRoleDTO();

            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<pagesinrole>>()
                .GetSingle(item => item.Page_Role_Id == pageRoleId && item.Page_Role_IsDeleted == false), pageRoleDTO);

            //return agent details
            return pageRoleDTO;
        }

        /// <summary>
        /// Get Page Details By PageId
        /// </summary>
        /// <param name="pageId">Int32:pageId</param>
        /// <returns></returns>
        public PageInfoDTO GetPageDetailsByPageId(int pageId)
        {
            //To select agent by agent id
            PageInfoDTO pageDetails = new PageInfoDTO();

            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<pageinfo>>()
                .GetSingle(item => item.Page_Id == pageId && item.Page_IsDeleted == false), pageDetails);

            //return agent details
            return pageDetails;
        }

        /// <summary>
        /// Update Page Status by pageId,loggedInUserId,roleId and isPageActive
        /// </summary>
        /// <param name="pageId">Int32:pageId</param>
        /// <param name="loggedInUserId">int32:loggedInUserId</param>
        /// <param name="roleId">int32:roleId</param>
        /// <param name="isPageActive">bool:isPageActive</param>
        public void UpdatePageStatus(int pageId, int loggedInUserId, int roleId, bool isPageActive)
        {
            PagesInRoleDTO pageInRoleDetails = GetPageDetailsByParentPageIdNRoleId(pageId, roleId);
            pageInRoleDetails.Page_Role_IsActive = isPageActive;
            pageInRoleDetails.Page_Role_CreatedBy = loggedInUserId;
            pageInRoleDetails.Page_Role_CreatedDate= DateTime.Now;

            pagesinrole pageInRoleEntity = new pagesinrole();
            AutoMapper.Mapper.Map(pageInRoleDetails, pageInRoleEntity);

            ESalesUnityContainer.Container.Resolve<IGenericRepository<pagesinrole>>().Update(pageInRoleEntity);
            UpdateDynamicParentItemsStatus (pageId, roleId, isPageActive);
        }

        /// <summary>
        /// Delete Pages From Role by pageRoleId and loggedInUserId
        /// </summary>
        /// <param name="pageRoleId">int32:pageRoleId</param>
        /// <param name="loggedInUserId">int32:loggedInUserId</param>
        public void DeletePagesFromRole(int pageRoleId, int loggedInUserId)
        {
            PagesInRoleDTO pageRoleDTO = GetPageRoleDetailsByPageRoleId(pageRoleId);
            pageRoleDTO.Page_Role_IsDeleted = true;
            pageRoleDTO.Page_Role_CreatedBy = loggedInUserId;
            pageRoleDTO.Page_Role_CreatedDate = DateTime.Now;

            pagesinrole pageRoleEntity = new pagesinrole();
            AutoMapper.Mapper.Map(pageRoleDTO, pageRoleEntity);

            ESalesUnityContainer.Container.Resolve<IGenericRepository<pagesinrole>>().Update(pageRoleEntity);
            DeleteDynamicParentItemsStatus(pageRoleDTO.Page_Role_PageId, pageRoleDTO.Page_Role_RoleId);
        }

        /// <summary>
        /// Delete Dynamic Parent Items Status by pageId and roleId
        /// </summary>
        /// <param name="pageId">Int32:pageId</param>
        /// <param name="roleId">Int32:roleId</param>
        public void DeleteDynamicParentItemsStatus(int pageId, int roleId)
        {
            PageInfoDTO page = GetPageDetailsByPageId(pageId);

            if (pageId != 0)
            {
                List<PageInfoDTO> lstPageInfoDTO = new List<PageInfoDTO>();
                List<PagesInRoleDTO> lstPageInRole = new List<PagesInRoleDTO>();

                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<pagesinrole>>().GetQuery().Where(item => item.Page_Role_IsActive == true
                   && item.Page_Role_IsDeleted == false && item.Page_Role_RoleId == roleId && item.Page_Role_PageId != pageId), lstPageInRole);

                List<pageinfo> lstPageInfoEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<pageinfo>>().GetQuery()
                     .Where(item => item.Page_ParentPageLevelId == page.Page_ParentPageLevelId && item.Page_IsActive == true && item.Page_IsDeleted == false)
                     .OrderBy(item => item.Page_Level).ToList();
                AutoMapper.Mapper.Map(lstPageInfoEntity, lstPageInfoDTO);


                List<pageinfo> result = new List<pageinfo>();
                result = (from pg in lstPageInfoEntity
                          where (from role in lstPageInRole select role.Page_Role_PageId).Contains(pg.Page_Id)
                          select pg).ToList();

                if (result.Count == 0)
                {
                    PagesInRoleDTO pageRoleDetails = GetPageDetailsByParentPageIdNRoleId(page.Page_ParentPageLevelId, roleId);
                    pageRoleDetails.Page_Role_IsDeleted = true;
                    pageRoleDetails.Page_Role_LastUpdatedDate = DateTime.Now;
                    pagesinrole pageInRoleEntity = new pagesinrole();
                    AutoMapper.Mapper.Map(pageRoleDetails, pageInRoleEntity);
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<pagesinrole>>().Update(pageInRoleEntity);
                    DeleteDynamicParentItemsStatus(page.Page_ParentPageLevelId, roleId);
                }

            }
        }
	}
}