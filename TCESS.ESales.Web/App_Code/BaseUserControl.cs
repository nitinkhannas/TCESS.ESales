#region Using directives

using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using AlwaysShowHeaderFooter;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.DataTransferObjects;

#endregion

/// <summary>
/// Summary description for BaseUserControl
/// </summary>
public class BaseUserControl : UserControl
{
    public delegate void CloseScreenEventHandler(object sender);
    public delegate void OkMessageBoxEventHandler(object sender, EventArgs args);
    public delegate void YesNoMessageBoxEventHandler(object sender, EventArgs args);
    public delegate void ShowDataByIdEventHandler(int id);
    public delegate void ShowCollectionEventHandler(int collectionId, int paymentMode, int custId);
    public delegate void ShowDataEventHandler(int id, bool isEdit, string path);
    public delegate void ShowReportEventHandler(int id, DateTime fromDate, DateTime toDate);
    public delegate void ShowReportMonthYearEventHandler(int id, int month, int year);
    public delegate void ShowDataByDateEventHandler(DateTime fromDate, DateTime toDate);
    public delegate void ShowMonthEventHandler(int agentId, int month);
    public delegate void ShowDateMonthEventHandler(DateTime fromDate, int month);
    public delegate void ShowMonthYearEventHandler(int agentId, int month, int year);
    public delegate void ShowPrintReportEventHandler(string data);
    public delegate void ShowCustomerwiseReportEventHandler(string code, DateTime fromDate, DateTime toDate);

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
        return Convert.ToInt32(Membership.GetUser().ProviderUserKey);
    }

    public UserAgentMappingDTO GetAgentByUserId()
    {
        int userId = Convert.ToInt32(Membership.GetUser().ProviderUserKey);

        //Gets AgentId by currently logged in UserId
        UserAgentMappingDTO agentMapDetails = MasterList.GetAgentByUserId(userId);
        return agentMapDetails;
    }
}