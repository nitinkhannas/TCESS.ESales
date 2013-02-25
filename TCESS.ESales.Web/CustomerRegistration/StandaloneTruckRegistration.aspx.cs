#region Using directives

using System;

#endregion

public partial class Bookings_StandaloneTruckRegistration : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckIsUserAuthenticated();
            ucAddEditStandaloneTrucks.ShowBlankScreen();
        }
    }
}