#region Using directives

using System.Collections.Generic;
using TCESS.ESales.DataTransferObjects;

#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface IMembershipService
    {
        int GetRoleIdByUserId(int userId);
        int GetRoleIdByRoleName(string roleName);
        void DeleteUserInRoles(int userId);

        IList<PageInfoDTO> GetParentMenuItems(IList<PagesInRoleDTO> lstPagesInRole);
        IList<PageInfoDTO> GetChildMenuItems(IList<PagesInRoleDTO> lstPagesInRole);
        IList<PageInfoDTO> GetAllPages(int roleId);

        IList<PagesInRoleDTO> GetPageInfoByRoleId(int roleId);
        IList<PagesInRoleDTO> GetPagesInRole(int roleId);
        PagesInRoleDTO GetPageDetailsByParentPageIdNRoleId(int pageId, int RoleId);
        void AddPagesToRoles(PagesInRoleDTO pagesInRolesDetails);
        
        bool CheckIfPageExistsInRole(int pageId, int roleId);
        void DeletePagesFromRole(int pageRoleId, int loggedInUserId);
        void UpdatePageStatus(int pageId, int loggedInUserId, int roleId, bool isPageActive);
        void DeleteDynamicParentItemsStatus(int pageId, int roleId);
        void UpdateDynamicParentItemsStatus(int pageId, int roleId, bool isActive);
    }
}