#region Using directives

using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI.WebControls;
using AlwaysShowHeaderFooter;
using Resources;
using TCESS.ESales.DataTransferObjects;

#endregion

/// <summary>
/// Summary description for BaseClass
/// </summary>
public class BaseClass
{
    internal IEnumerable<T> AddBlankRowInGrid<T>() where T : BaseDTO
    {
        IList<T> lstBlankRow = new List<T>();
        lstBlankRow.Add(Activator.CreateInstance<T>());
        return lstBlankRow;
    }

    internal void ShowBlankRowInGrid<T>(GridViewAlwaysShow customGridView) where T : BaseDTO
    {
        customGridView.DataSource = AddBlankRowInGrid<T>();
        customGridView.DataBind();

        int totalColumns = customGridView.Rows[0].Cells.Count;
        customGridView.Rows[0].Cells.Clear();        
        customGridView.Rows[0].Cells.Add(new TableCell());
        customGridView.Rows[0].Cells[0].ColumnSpan = totalColumns;
        customGridView.Rows[0].Cells[0].Text = Messages.NoRecordFound;
    }

    internal List<string> AddBlankRowInGrid()
    {
        List<String> lstEmpty = new List<String>();
        string strEmpty = string.Empty;
        lstEmpty.Add(strEmpty);
        return lstEmpty;
    }

    internal void ShowBlankRowInGrid(GridViewAlwaysShow customGridView)
    {
        customGridView.DataSource = AddBlankRowInGrid();
        customGridView.DataBind();

        int totalColumns = customGridView.Rows[0].Cells.Count;
        customGridView.Rows[0].Cells.Clear();
        customGridView.Rows[0].Cells.Add(new TableCell());
        customGridView.Rows[0].Cells[0].ColumnSpan = totalColumns;
        customGridView.Rows[0].Cells[0].Text = Messages.NoRecordFound;
    }

    internal int GetCurrentUserId()
    {
        return Convert.ToInt32(Membership.GetUser().ProviderUserKey);
    }
}