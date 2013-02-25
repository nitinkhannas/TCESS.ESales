#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Exception;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Bookings_UserControls_ManageCounter : BaseUserControl
{
	public event ShowDataByIdEventHandler Event_ShowCounterScreen;
    /// <summary>
    /// Page Init Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }
    /// <summary>
    /// on page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			FillCounterDetails();
		}
	}
    /// <summary>
    /// Get user name
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
	public string GetUserName(object sender)
	{
		if (!(Object.Equals(sender, 0)) && !(Object.Equals(sender, null)))
		{
			MembershipUser objMembershipUser = Membership.GetUser(sender);
			return objMembershipUser.UserName;
		}
		else
		{
			return "1";
		}
	}
    /// <summary>
    /// Function for Fill Counter Details
    /// </summary>
	public void FillCounterDetails()
	{
		IList<CounterDTO> lstCounterDTO = (ESalesUnityContainer.Container.Resolve<ICounterService>().GetCounterDetails());

		if (lstCounterDTO.Count > 0)
		{
			grdManageCounter.DataSource = lstCounterDTO;
			grdManageCounter.DataBind();
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
		ShowBlankRowInGrid<CounterDTO>(grdManageCounter);
	}
    /// <summary>
    /// Event for Grid Row command
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void grdManageCounter_RowCommand(object sender, GridViewCommandEventArgs e)
	{
        if (e.CommandName == Globals.GridCommandEvents.EDITCOUNTER)
		{
            Event_ShowCounterScreen(Convert.ToInt32(e.CommandArgument));
		}
		else if (e.CommandName == Globals.GridCommandEvents.ADDNEW)
		{
            Event_ShowCounterScreen(0);
		}
	}
    /// <summary>
    /// To add blank row in a grid
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
	protected IEnumerable grdManageCounter_MustAddARow(System.Collections.IEnumerable data)
	{
		return base.AddBlankRowInGrid<CounterDTO>();
	}
    /// <summary>
    /// Event for on Row Deleting Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void grdManageCounter_RowDeleting(object sender, GridViewDeleteEventArgs e)
	{
		try
		{
            ExceptionHandler.AppExceptionManager.Process(() =>
            {
                int counterId = Convert.ToInt32(grdManageCounter.DataKeys[e.RowIndex].Value);

                CounterDTO counter = ESalesUnityContainer.Container.Resolve<ICounterService>().GetCounterDetailsById(counterId);
                counter.Counter_IsDeleted = true;
                counter.Counter_User_Id = null;
                counter.Counter_MAC_Id = null;

                ESalesUnityContainer.Container.Resolve<ICounterService>().SaveAndUpdateCounters(counter);
                ucMessageBoxForGrid.ShowMessage(Messages.CounterDeletedSuccessfully);
            }, Globals.ExceptionTypes.AssistingAdministrators.ToString());
		}
		catch (Exception ex)
		{
		}		
	}
    /// <summary>
    /// Event to fill the Counter Detail
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {        
        FillCounterDetails();
    }

    protected void grdManageCounter_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdManageCounter.PageIndex = e.NewPageIndex;
        FillCounterDetails();
    }
}