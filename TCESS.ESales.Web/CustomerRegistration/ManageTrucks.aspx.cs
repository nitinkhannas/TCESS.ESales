#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class CustomerRegistration_ManageTrucks : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        //Custom events from Truck Registration Page
        ucTruckRegistration.Event_CloseScreen += ucTruckRegistration_Event_CloseScreen;

        //Custom events from Manage Truck Page
        ucManageTrucks.Event_AddTruckDetails += ucManageTrucks_Event_AddTruckDetails;
        ucManageTrucks.Event_EditTruckDetails += ucManageTrucks_Event_EditTruckDetails;
    }

	protected void Page_Load(object sender, EventArgs e)
	{
        base.CheckIsUserAuthenticated();
        
        if (!IsPostBack)
		{
            ShowInitialValues();
		}
	}

    void ucTruckRegistration_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }

    void ucManageTrucks_Event_AddTruckDetails(int customerId, bool isFirstTruck, string folderName)
    {
        //Sets visibility of frames that contains user controls       
        pnlManageTrucks.Visible = false;
        pnlTruckRegistration.Visible = true;

        ucTruckRegistration.ShowBlankScreen(customerId, isFirstTruck, folderName);
    }

    void ucManageTrucks_Event_EditTruckDetails(int truckId)
    {
        //Sets visibility of frames that contains user controls       
        pnlManageTrucks.Visible = false;
        pnlTruckRegistration.Visible = true;

        ucTruckRegistration.PopulateTruckData(truckId);
    }

    /// <summary>
    /// Show Page Values when it initially Loads or Refreshes
    /// </summary>
    private void ShowInitialValues()
    {
        //Sets visibility of frames that contains user controls       
        pnlTruckRegistration.Visible = false;
        pnlManageTrucks.Visible = true;

        ucManageTrucks.ShowDefaultManageTruckScreen();
    }
}