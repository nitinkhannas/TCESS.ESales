#region Using directives

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using AlwaysShowHeaderFooter;
using Resources;
using TCESS.ESales.DataTransferObjects;

#endregion

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : Page
{
    protected void CheckIsUserAuthenticated()
    {
        bool isAuthenticated = true;     
        if (!User.Identity.IsAuthenticated)
        {
            isAuthenticated = false;    
        }

        if (User.Identity.IsAuthenticated)
        {
            //Gets RoleId from current logged in Username
            int roleId = MenuBuilderLogic.GetRoleIdByUserId();

            //Gets Page information from RoleId
            IList<PagesInRoleDTO> lstPagesInRole = MenuBuilderLogic.GetPagesInRole(roleId);

            IList<PageInfoDTO> lstPagesInfo = MenuBuilderLogic.GetChildMenuItems(lstPagesInRole);

            lstPagesInfo = (from page in lstPagesInfo
                            where page.Page_NavigateURL != null
                            select page).ToList();
            string currentPage = Path.GetFileName(Request.PhysicalPath);

            if (currentPage != "Home.aspx" && currentPage != "ViewCustomerDetails.aspx")
            {
                //Checks if current user is assigned the selected page
                int pageId = (from page in lstPagesInfo
                              where page.Page_NavigateURL.Contains(Path.GetFileName(Request.PhysicalPath))
                              select page.Page_Id).FirstOrDefault();

                isAuthenticated = pageId > 0 ? true : false;
            }
        }

        //If user is not authenticated to view this page, return to log-in page
        if (!isAuthenticated)
        {
            Response.Write(Messages.UserNotAuthenticated);
            Response.End();
        }
    }

    protected IEnumerable<T> AddBlankRowInGrid<T>() where T : BaseDTO
    {
        BaseClass baseClass = new BaseClass();
        return baseClass.AddBlankRowInGrid<T>();
    }

    protected void ShowBlankRowInGrid<T>(GridViewAlwaysShow customGridView) where T : BaseDTO
    {
        BaseClass baseClass = new BaseClass();
        baseClass.ShowBlankRowInGrid<T>(customGridView);
    }

    protected List<string> AddBlankRowInGrid()
    {
        BaseClass baseClass = new BaseClass();
        return baseClass.AddBlankRowInGrid();
    }

    protected void ShowBlankRowInGrid(GridViewAlwaysShow customGridView)
    {
        BaseClass baseClass = new BaseClass();
        baseClass.ShowBlankRowInGrid(customGridView);
    }

    protected int GetCurrentUserId()
    {
        BaseClass baseClass = new BaseClass();
        return baseClass.GetCurrentUserId();
    }
}