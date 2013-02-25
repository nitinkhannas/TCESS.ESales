#region Using directives

using System;
using System.Web.Security;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

/// <summary>
/// Summary description for MenuBuilderLogic
/// </summary>
public class MenuBuilderLogic
{
    public static IList<PageInfoDTO> GetParentMenuItems(IList<PagesInRoleDTO> lstPagesInRole)
    {
        return ESalesUnityContainer.Container.Resolve<IMembershipService>().GetParentMenuItems(lstPagesInRole);
    }

    public static IList<PageInfoDTO> GetChildMenuItems(IList<PagesInRoleDTO> lstPagesInRole)
    {
        return ESalesUnityContainer.Container.Resolve<IMembershipService>().GetChildMenuItems(lstPagesInRole);
    }

    /// <summary>
    /// Gets RoleId from current logged in Username
    /// </summary>
    /// <returns>int</returns>
    public static int GetRoleIdByUserId()
    {
        //Gets UserId from current logged in Username
        int userID = Convert.ToInt32(Membership.GetUser().ProviderUserKey);

        //Gets RoleId from current logged in Username
        int roleId = ESalesUnityContainer.Container.Resolve<IMembershipService>().GetRoleIdByUserId(userID);
        return roleId;
    }

    /// <summary>
    /// Gets Page information from RoleId
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public static IList<PagesInRoleDTO> GetPagesInRole(int roleId)
    {
        //Gets Page information from RoleId
        IList<PagesInRoleDTO> lstPagesInRole = ESalesUnityContainer.Container.Resolve<IMembershipService>()
            .GetPageInfoByRoleId(roleId);
        return lstPagesInRole;
    }
}