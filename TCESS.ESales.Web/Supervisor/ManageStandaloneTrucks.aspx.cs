using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Supervisor_ManageStandaloneTrucks : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucAddEditStandaloneTrucks.Event_CloseScreen += ucAddEditStandaloneTrucks_Event_CloseScreen;
        ucManageStandaloneTrucks.Event_ShowStandaloneTruck += ucManageStandaloneTrucks_Event_ShowStandaloneTruck;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckIsUserAuthenticated();

        if (!IsPostBack)
        {
            ShowInitialValues();
        }
    }

    void ucAddEditStandaloneTrucks_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
        ucManageStandaloneTrucks.FillBlankGrid();
    }

    void ucManageStandaloneTrucks_Event_ShowStandaloneTruck(int truckId)
    {
        pnlManageStandaloneTrucks.Visible = false;
        pnlAddEditStandaloneTrucks.Visible = true;

        if (truckId > 0)
        {
            ucAddEditStandaloneTrucks.PopulateTruckDetails(truckId);
        }
        else
        {
            ucAddEditStandaloneTrucks.ShowBlankScreen();
        }
    }

    /// <summary>
    /// Show Page Values when it initially Loads or Refreshes
    /// </summary>
    private void ShowInitialValues()
    {
        //Sets visibility of frames that contains user controls
        pnlAddEditStandaloneTrucks.Visible = false;
        pnlManageStandaloneTrucks.Visible = true;
    }
}