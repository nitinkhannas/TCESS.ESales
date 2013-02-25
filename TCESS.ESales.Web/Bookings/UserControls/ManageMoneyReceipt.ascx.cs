#region Namespace

using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Bookings_UserControls_ManageMoneyReceipt : BaseUserControl
{
    public event ShowDataByIdEventHandler Event_ShowCanceMoneyReceiptScreen;
    /// <summary>
    /// Event for GetAllMoneyReceiptsForAgents
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Get all money receipt for an agent issued to a specific counter
            GetAllMoneyReceiptsForAgents();
        }        
    }

    /// <summary>
    /// Get all money receipt for an agent issued to a specific counter
    /// </summary>
    public void GetAllMoneyReceiptsForAgents()
    {
        //Gets AgentId by currently logged in UserId
        UserAgentMappingDTO agentMapDetails = GetAgentIdByUserId(Convert.ToInt32(Membership.GetUser().ProviderUserKey));

        IList<MoneyReceiptDTO> lstMoneyReceipt = ESalesUnityContainer.Container.Resolve<IMoneyReceiptService>()
            .GetAllMoneyReceipts(agentMapDetails.UAM_Agent_Id);

        if (lstMoneyReceipt.Count > 0)
        {
            grdManageMoneyReceipt.DataSource = lstMoneyReceipt;
            grdManageMoneyReceipt.DataBind();
        }
        else
        {
            FillBlankGrid();
        }
    }
    /// <summary>
    /// Function for Fill Blank Grid
    /// </summary>
    private void FillBlankGrid()
    {
        ShowBlankRowInGrid<MoneyReceiptDTO>(grdManageMoneyReceipt);
    }

    /// <summary>
    /// Gets AgentId by currently logged in UserId
    /// </summary>
    /// <param name="userId">currently logged in UserId</param>
    /// <returns>returns AgentId</returns>
    private UserAgentMappingDTO GetAgentIdByUserId(int userId)
    {   
        //Gets AgentId by UserId and return the value
        return MasterList.GetAgentByUserId(userId);
    }

    /// <summary>
    /// To Show CanceMoneyReceiptScreen
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdManageMoneyReceipt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == Globals.GridCommandEvents.CANCEL)
        {
            Event_ShowCanceMoneyReceiptScreen(Convert.ToInt32(e.CommandArgument));
        }
    }
}