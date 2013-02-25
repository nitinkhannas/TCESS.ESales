#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.CommonLayer.Unity;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using Resources;

#endregion

public partial class Administrator_DCACustomerAssociation : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateMandatoryDocuments();
            ShowBlankGrid();
            //  LoadAllCustomers();
        }
    }

    private void PopulateMandatoryDocuments()
    {
        ddlMandatoryDoc.DataSource = ESalesUnityContainer.Container.Resolve<IDocumentTypeService>().GetUniqueDocumentTypeList();
        ddlMandatoryDoc.DataBind();
        ddlMandatoryDoc.Items.Insert(0, new ListItem(Labels.CustomerCode, "0"));
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            FillGridWithCustomerDetails(Convert.ToInt32(ddlMandatoryDoc.SelectedItem.Value), txtDocNumber.Text.Trim());
            ViewState[Globals.StateMgmtVariables.MANDATORYDOCTYPE] = ddlMandatoryDoc.SelectedItem.Value;
            ViewState[Globals.StateMgmtVariables.MANDATORYDOCNO] = txtDocNumber.Text.Trim();
        }
    }
    private void FillGridWithCustomerDetails(int mandatoryDocId, string documentNo)
    {
        IList<CustomerDTO> lstCustomerDTO = new List<CustomerDTO>();

        if (Convert.ToInt32(mandatoryDocId) == 0)
        {
            CustomerDTO customerDetails = new CustomerDTO();
            customerDetails = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetCustomerDetailsByCode(documentNo);

            if (customerDetails.Cust_Id > 0)
            {
                int agentId = Convert.ToInt32(customerDetails.Cust_AgentId);

                switch (agentId)
                {
                    case 20:
                        customerDetails.Cust_AgentName = ESalesUnityContainer.Container.Resolve<IAgentService>().GetAgentShortNameByAgentId(agentId);
                        break;
                    case 21:
                        customerDetails.Cust_AgentName = ESalesUnityContainer.Container.Resolve<IAgentService>().GetAgentShortNameByAgentId(agentId);
                        break;
                    case 22:
                        customerDetails.Cust_AgentName = ESalesUnityContainer.Container.Resolve<IAgentService>().GetAgentShortNameByAgentId(agentId);
                        break;
                    case 23:
                        customerDetails.Cust_AgentName = ESalesUnityContainer.Container.Resolve<IAgentService>().GetAgentShortNameByAgentId(agentId);
                        break;
                    case 24:
                        customerDetails.Cust_AgentName = ESalesUnityContainer.Container.Resolve<IAgentService>().GetAgentShortNameByAgentId(agentId);
                        break;
                }

                lstCustomerDTO.Add(customerDetails);
            }

        }
        else
        {
            CustomerDocDetailsDTO docDetails = new CustomerDocDetailsDTO();
            docDetails = ESalesUnityContainer.Container.Resolve<ICustomerDocService>().GetCustomerByDocumentId(mandatoryDocId, documentNo);

            if (docDetails.Cust_Doc_Customer != null)
            {
                lstCustomerDTO.Add(docDetails.Cust_Doc_Customer);
            }
        }

        if (lstCustomerDTO.Count > 0)
        {

            grdDCACustomersAssociation.DataSource = lstCustomerDTO;
            grdDCACustomersAssociation.DataBind();

        }
        else
        {
            ShowBlankGrid();
        }
    }
    private void LoadAllCustomers()
    {
        IList<CustomerDTO> lstCustomer = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetCustomerForDCAAssociation();

        if (lstCustomer.Count > 0)
        {
            grdDCACustomersAssociation.DataSource = lstCustomer;
            grdDCACustomersAssociation.DataBind();
        }
        else
        {
            ShowBlankGrid();
        }
    }

    private void ShowBlankGrid()
    {
        base.ShowBlankRowInGrid<CustomerDTO>(grdDCACustomersAssociation);
    }

    protected void grdDCACustomersAssociation_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1);
    }

    protected void grdDCACustomersAssociation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grdDCACustomersAssociation.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
        {
            // To populate DCA name dropdowm list for update            
            DropDownList ddlDCAName = (DropDownList)e.Row.FindControl("ddlDCAName");

            //Gets list of alloted quantity
            MasterList.GetAgentListInDropDown(ddlDCAName);

            //To fetch particular Agent Id            
            ddlDCAName.SelectedValue = Convert.ToString(grdDCACustomersAssociation.DataKeys[e.Row.RowIndex]["Cust_AgentId"]);
        }
    }

    protected void grdDCACustomersAssociation_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(e.NewEditIndex);
    }

    protected void grdDCACustomersAssociation_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int customerId = Convert.ToInt32(grdDCACustomersAssociation.DataKeys[e.RowIndex]["Cust_ID"]);

        //Gets customer details by customer id
        CustomerDTO customerDetails = MasterList.GetCustomerDetailsById(customerId);
        customerDetails.Cust_LastUpdatedDate = DateTime.Now;

        //Gets the DCA Id from dropdown box
        int dcaId = Convert.ToInt32(((DropDownList)grdDCACustomersAssociation.Rows[e.RowIndex]
            .FindControl("ddlDCAName")).SelectedValue);

        //If dropdown doesnt contain a valid DCA name, update null to database
        customerDetails.Cust_AgentId = dcaId == 0 ? (Nullable<int>)null : dcaId;

        //To update customer details with agent mapping 
        ESalesUnityContainer.Container.Resolve<ICustomerService>().SaveAndUpdateCustomerDetails(customerDetails, null);

        if (dcaId > 0)
        {
            ucMessageBox.ShowMessage(Messages.CustomerDCAAssociationCreated);
        }
        else
        {
            ucMessageBox.ShowMessage(Messages.CustomerDCAAssociationRemoved);
        }

        GridViewRowUpdateFunctions(-1);
        ucMessageBox.ShowMessage(Resources.Messages.CustomerDCAUpdatedSuccessfully);
    }

    /// <summary>
    /// Row edit/update/cancel function for grid view
    /// </summary>
    /// <param name="rowIndex">rowIndex of gridview</param>
    private void GridViewRowUpdateFunctions(int rowIndex)
    {
        grdDCACustomersAssociation.EditIndex = rowIndex;

        FillGridWithCustomerDetails(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.MANDATORYDOCTYPE].ToString()), ViewState[Globals.StateMgmtVariables.MANDATORYDOCNO].ToString());


        //Populate Customer with agent mapping
        // LoadAllCustomers();
    }
}
