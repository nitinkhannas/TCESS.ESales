#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.BusinessLayer.Interfaces.Users;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Administrator_UserControls_ManageDCA : BaseUserControl
{
    public ShowDataByIdEventHandler Event_ShowDCAScreen;

    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Get all active DCA details
            PopulateDCADetails();
        }
    }

    /// <summary>
    /// Get all active DCA details
    /// </summary>
    public void PopulateDCADetails()
    {
        IList<AgentDTO> lstAgent = MasterList.GetAgentList();

        if (lstAgent.Count > 0)
        {
            grdDCA.DataSource = lstAgent;
            grdDCA.DataBind();
        }
        else
        {
            base.ShowBlankRowInGrid<AgentDTO>(grdDCA);
        }
    }

    protected void grdDCA_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int agentId = Convert.ToInt32(grdDCA.DataKeys[e.RowIndex].Value);
        bool isAgentReferenced = ESalesUnityContainer.Container.Resolve<IUserAgentService>()
            .CheckIfAgentNotReferenced(agentId);

        if (isAgentReferenced)
        {
            customValidator.IsValid = false;
            ucMessageBoxForGrid.ShowMessage(Messages.DCAAlreadyUsed);
        }

        if (Page.IsValid)
        {
            //Delete the agent by Agent Id
            ESalesUnityContainer.Container.Resolve<IAgentService>().DeleteAgent(agentId);
            ucMessageBoxForGrid.ShowMessage(Messages.DCADetailsDeletedSuccessfully);
        }
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        //Get all active DCA details
        PopulateDCADetails();
    }

    protected IEnumerable grdDCA_MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<CustomerDTO>();
    }

    protected void grdDCA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDCA.PageIndex = e.NewPageIndex;

        //Binds DCA Details from database
        PopulateDCADetails();
    }

    protected void grdDCA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == Globals.GridCommandEvents.EDITDCA)
        {
            Event_ShowDCAScreen(Convert.ToInt32(e.CommandArgument));
        }
        else if (e.CommandName == Globals.GridCommandEvents.ADDNEW)
        {
            Event_ShowDCAScreen(0);
        }
    }
}