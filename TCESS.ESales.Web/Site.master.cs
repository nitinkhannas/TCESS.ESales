#region Namespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using TCESS.ESales.CommonLayer.Unity;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.DataTransferObjects;
using Resources;

#endregion

public partial class SiteMaster : MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.User.Identity.IsAuthenticated)
        {
            Response.Write(Messages.UserNotAuthenticated);
            Response.End();
        }

        BindMenu();

        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
    }

    private void BindMenu()
    {
        navigationMenu.Items.Clear();

        //Gets RoleId from current logged in Username
        int roleId = MenuBuilderLogic.GetRoleIdByUserId();

        //Gets Page information from RoleId
        IList<PagesInRoleDTO> lstPagesInRole = MenuBuilderLogic.GetPagesInRole(roleId);
        
        IList<PageInfoDTO> lstParentMenu = MenuBuilderLogic.GetParentMenuItems(lstPagesInRole);

        IList<PageInfoDTO> lstPagesInfo = MenuBuilderLogic.GetChildMenuItems(lstPagesInRole);

        foreach (PageInfoDTO parentPage in lstParentMenu)
        {
            navigationMenu.Items.Add(new MenuItem()
            {
                Text = parentPage.Page_Name,
                Value = parentPage.Page_Id.ToString(),
                NavigateUrl = parentPage.Page_NavigateURL,
                Selectable = false
            });

            GenerateDynamicChildMenus(lstPagesInfo, navigationMenu.Items[navigationMenu.Items.Count - 1]);
        }

        MenuItem menuHome = new MenuItem("Home", "Home", null, "~/Administrator/Home.aspx");
        navigationMenu.Items.AddAt(0, menuHome);

        MenuItem menuLogOut = new MenuItem("Log Out", "LogOut", null, "~/LogOut.aspx");
        menuLogOut.Selectable = true;
        navigationMenu.Items.Add(menuLogOut);
    }

    private void GenerateDynamicChildMenus(IList<PageInfoDTO> lstPagesInfo, MenuItem menuItem)
    {
        List<PageInfoDTO> lstChildrenItems = lstPagesInfo.Where((PageInfoDTO Page) => Page.Page_ParentPageLevelId.ToString()
            .Equals(menuItem.Value)).ToList();

        foreach (PageInfoDTO child in lstChildrenItems)
        {
            menuItem.ChildItems.Add(new MenuItem()
            {
                Text = child.Page_Name,
                Value = child.Page_Id.ToString(),
                NavigateUrl = child.Page_NavigateURL,
                Selectable = child.Page_NavigateURL == null ? false : true
            });

            GenerateDynamicChildMenus(lstPagesInfo, menuItem.ChildItems[menuItem.ChildItems.Count - 1]);
        }
    }

    protected void navigationMenu_MenuItemClick(object sender, MenuEventArgs e)
    {
        if (e.Item.Value == "LogOut")
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Index.aspx");
        }
    }
}