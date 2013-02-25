#region Using directives

using System;
using Resources;

#endregion

public partial class Bookings_Counter : BasePage
{
    /// <summary>
    /// page Init event to register close screen and Show Counter Screen
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void Page_Init(object sender, EventArgs e)
	{
		ucCounterCreation.Event_CloseScreen += ucCounterCreation_Event_CloseScreen;
        ucManageCounter.Event_ShowCounterScreen += ucManageCounter_Event_ShowCounterScreen;
	}
    /// <summary>
    /// method to show Counter Screen by counterId
    /// </summary>
    /// <param name="counterId"></param>
    void ucManageCounter_Event_ShowCounterScreen(int counterId)
	{		
		pnlManageCounter.Visible = false;
        pnlCreateCounter.Visible = true;

        if (counterId > 0)
        {
            lblPageName.Text = Labels.EditCounter;
            ucCounterCreation.PopulateCounterDetails(counterId);
        }
        else
        {
            lblPageName.Text = Labels.CounterCreation;
            ucCounterCreation.PopulateUser(0);
            ucCounterCreation.ShowBlankScreen();
        }
	}
    /// <summary>
    /// method to close the screen and show initial values
    /// </summary>
    /// <param name="sender"></param>
    void ucCounterCreation_Event_CloseScreen(object sender)
    {
        ShowInitialValues();

        lblPageName.Text = Labels.ManageCounters;
        ucManageCounter.FillCounterDetails();
    }
	/// <summary>
	/// Page load event.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void Page_Load(object sender, EventArgs e)
    {
		CheckIsUserAuthenticated();

		if (!IsPostBack)
		{
			ShowInitialValues();
		}
    }
    /// <summary>
    /// Method to set initial visibilty of create counter and manage counter
    /// </summary>
	private void ShowInitialValues()
	{
		pnlCreateCounter.Visible = false;
		pnlManageCounter.Visible = true;
	}
}