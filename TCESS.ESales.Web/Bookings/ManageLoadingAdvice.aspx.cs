#region Namespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCESS.ESales.CommonLayer.CommonLibrary;
using System.Web.Security;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using Microsoft.Practices.Unity;
using System.Collections;

#endregion

public partial class Bookings_ManageLoadingAdvice : BasePage
{
    /// <summary>
    /// Event for Page Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			FillGridWithTruckDetails();
		}
	}
    /// <summary>
    /// Method for Fill Grid With Truck Details
    /// </summary>
	public void FillGridWithTruckDetails()
	{
		IList<BookingDTO> listBooking = ESalesUnityContainer.Container.Resolve<IBookingService>().GetRejectedBookingsForAgents();

		if (listBooking.Count > 0)
		{
			grdManageLoadingAdvice.DataSource = listBooking;
			grdManageLoadingAdvice.DataBind();
		}
		else
		{
			FillBlankGrid();
		}
	}
    /// <summary>
    /// Method for Fill Blank Grid
    /// </summary>
	private void FillBlankGrid()
	{
		ShowBlankRowInGrid<BookingDTO>(grdManageLoadingAdvice);
	}
    /// <summary>
    /// Event for Add Blank Row in grid.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
	protected IEnumerable grdManageLoadingAdvice_MustAddARow(IEnumerable data)
	{
		//return the value
		return base.AddBlankRowInGrid<BookingDTO>();
	}
    /// <summary>
    /// Event for Loading Advice
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void grdManageLoadingAdvice_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		if (e.CommandName == Globals.GridCommandEvents.EDITBOOKING)
		{
			Session[Globals.StateMgmtVariables.BOOKINGID] = e.CommandArgument;
			Response.Redirect("IssueLoadingAdvice.aspx");
		}
	}

    protected void grdManageLoadingAdvice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdManageLoadingAdvice.PageIndex = e.NewPageIndex;
        FillGridWithTruckDetails();
    }
}