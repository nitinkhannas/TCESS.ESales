#region Namespace
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using System.Collections;
using System.Collections.Specialized;
using Resources;
using System.Data;
using System.Transactions;
#endregion

public partial class Supervisor_ManageAllocatePercentage : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckIsUserAuthenticated();

        if (!IsPostBack)
        {
            //Populate material type drop down list
            PopuldateMaterialTypeDropdown();
            
            //Populate material percentage
            PopulateMaterialPercentage();
        }
    }

    private void PopuldateMaterialTypeDropdown()
    {
        ddlMaterialType.DataSource = ESalesUnityContainer.Container.Resolve<IMaterialTypeService>().GetMaterialTypeList(true);
        ddlMaterialType.DataBind();

        if (ddlMaterialType.Items.Count > 0)
        {
            ddlMaterialType.SelectedIndex = 0;
            lblMaterialname.Text = "For " + ddlMaterialType.SelectedItem.Text;
        }
    }

    private void PopulateMaterialPercentage()
    {
        if (ddlMaterialType.SelectedIndex > -1)
        {
            IList<AgentMaterialPercentageDTO> lstAgentMaterialPercentageDTO = ESalesUnityContainer.Container
                .Resolve<IAgentMaterialPercentageService>()
                .GetAgentMaterialPercentByMaterialTypeId(Convert.ToInt32(ddlMaterialType.SelectedValue));

            if (lstAgentMaterialPercentageDTO.Count > 0)
            {
                grdMaterialPercentage.DataSource = lstAgentMaterialPercentageDTO;
                grdMaterialPercentage.DataBind();
            }
            else
            {
                base.ShowBlankRowInGrid<AgentMaterialPercentageDTO>(grdMaterialPercentage);
                grdMaterialPercentage.FooterRow.Style.Add("display", "none");
            }

            if (ddlMaterialType.Items.Count > 0)
            {
                lblMaterialname.Text = "For " + ddlMaterialType.SelectedItem.Text;
                updHeader.Update();
            }
        }
    }

    protected void grdMaterialPercentage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Page.IsValid)
        {
            List<AgentMaterialPercentageDTO> lstAgentMaterialPercentageDTO = new List<AgentMaterialPercentageDTO>();

            for (int i = 0; i < grdMaterialPercentage.Rows.Count; i++)
            {
                AgentMaterialPercentageDTO objAgentMaterialPercentageDTO = new AgentMaterialPercentageDTO();
                GridViewRow r = grdMaterialPercentage.Rows[i];
                objAgentMaterialPercentageDTO.AMP_Percentage = Math.Round(Convert.ToDecimal(((TextBox)grdMaterialPercentage.Rows[i].FindControl("txtPercentage")).Text), 2);
                objAgentMaterialPercentageDTO.AMP_Id = Convert.ToInt32(grdMaterialPercentage.DataKeys[i].Value);
                lstAgentMaterialPercentageDTO.Add(objAgentMaterialPercentageDTO);
            }

            ESalesUnityContainer.Container.Resolve<IAgentMaterialPercentageService>()
                .UpdateAgentpercentage(lstAgentMaterialPercentageDTO);
            
            PopulateMaterialPercentage();
            ucMessageBox.ShowMessage(Resources.Messages.MaterialPercentageAllocatedSuccessfully);
        }
    }

    protected void ddlMaterialType_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateMaterialPercentage();
    }
}