#region Using directives

using System;

#endregion

public partial class Administrator_ManageDCA : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucManageDCA.Event_ShowDCAScreen += ucManageDCA_Event_ShowDCAScreen;
        ucAddEditDCA.Event_CloseScreen += ucAddEditDCA_Event_CloseScreen;
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckIsUserAuthenticated();

        if (!IsPostBack)
        {
            ShowInitialValues();
        }
    }

    void ucAddEditDCA_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }

    void ucManageDCA_Event_ShowDCAScreen(int agentId)
    {
        //Sets visibility of frames that contains user controls
        pnlManageDCA.Visible = false;
        pnlAddEditDCA.Visible = true;

        if (agentId > 0)
        {
            ucAddEditDCA.PopulateDCADetails(agentId);
        }
        else
        {
            ucAddEditDCA.ShowBlankScreen();
        }
    }
    
    /// <summary>
    /// Show Page Values when it initially Loads or Refreshes
    /// </summary>
    private void ShowInitialValues()
    {
        //Sets visibility of frames that contains user controls
        pnlAddEditDCA.Visible = false;
        pnlManageDCA.Visible = true;

        ucManageDCA.PopulateDCADetails();
    }
}