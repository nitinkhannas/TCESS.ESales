#region Namespace

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
using TCESS.ESales.CommonLayer.Exception;

#endregion

public partial class CustomerRegistration_UserControls_ManageTrucks : BaseUserControl
{
    public ShowDataEventHandler Event_AddTruckDetails;
    public ShowDataByIdEventHandler Event_EditTruckDetails;
 
    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateMandatoryDocumentsList();
        }
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            FillGridWithTruckDetails(Convert.ToInt32(ddlMandatoryDoc.SelectedItem.Value), txtDocNumber.Text.Trim());
            ViewState[Globals.StateMgmtVariables.MANDATORYDOCTYPE] = ddlMandatoryDoc.SelectedItem.Value;
            ViewState[Globals.StateMgmtVariables.MANDATORYDOCNO] = txtDocNumber.Text.Trim();
        }
    }

    protected IEnumerable grdManageCustomers_MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<TruckDetailsDTO>();
    }

    protected void grdManageTrucks_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals(Globals.GridCommandEvents.ADDNEW))
        {
            if (ViewState[Globals.StateMgmtVariables.CUSTOMERID] != null)
            {
                Event_AddTruckDetails(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]), false,
                    Convert.ToString(ViewState[Globals.StateMgmtVariables.CUSTFOLDERNAME]));
            }
            else
            {
                ucMessageBoxForGrid.ShowMessage(ErrorMessages.GetCustomerDetails);
            }
        }
        else if (e.CommandName == Globals.GridCommandEvents.EDITTRUCK)
        {
            Event_EditTruckDetails(Convert.ToInt32(e.CommandArgument));
        }
    }

    protected void grdManageTrucks_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        TruckDetailsDTO truckDetails = ESalesUnityContainer.Container.Resolve<ITruckService>()
            .GetTruckDetailsByTruckId(Convert.ToInt32(grdManageTrucks.DataKeys[e.RowIndex].Value));
        truckDetails.Truck_IsDeleted = true;

        ESalesUnityContainer.Container.Resolve<ITruckService>().DeleteTruck(truckDetails);
        ucMessageBoxForGrid.ShowMessage(Messages.TruckDeletedSuccessfully);
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        ShowDefaultManageTruckScreen();
    }

    private void FillGridWithTruckDetails(int mandatoryDocId, string documentNo)
    {
		CustomerDTO customer = new CustomerDTO();
		try
        {
            ExceptionHandler.AppExceptionManager.Process(() =>
            {
				if (Convert.ToInt32(mandatoryDocId) == 0)
				{
					CustomerDTO customerDetails = new CustomerDTO();
					customerDetails = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetCustomerDetailsByCode(documentNo);
					if (customerDetails.Cust_Id > 0)
					{
						customer = customerDetails;
					}
				}
				else
				{
					CustomerDocDetailsDTO doctype = new CustomerDocDetailsDTO();
					doctype = ESalesUnityContainer.Container.Resolve<ICustomerDocService>().GetCustomerByDocumentId(mandatoryDocId, documentNo);
					if (doctype.Cust_Doc_Customer != null)
					{
						customer = doctype.Cust_Doc_Customer;
					}					
				}

				if (customer.Cust_Id >0)
                {	
                    IList<TruckDetailsDTO> lstTruckDetailsDTO = (ESalesUnityContainer.Container.Resolve<ITruckService>()
						.GetTruckDetailsForCustomer(customer.Cust_Id));

					ViewState[Globals.StateMgmtVariables.CUSTOMERID] = customer.Cust_Id;
					ViewState[Globals.StateMgmtVariables.CUSTFOLDERNAME] = customer.Cust_FolderName;

                    if (lstTruckDetailsDTO.Count > 0)
                    {
                        grdManageTrucks.DataSource = lstTruckDetailsDTO;
                        grdManageTrucks.DataBind();
                    }
                    else
                    {
                        FillBlankGrid();
                    }
                }
                else
                {
                    FillBlankGrid();
                }
            }, Globals.ExceptionTypes.ExceptionShielding.ToString());
        }
        catch (Exception ex)
        {
        }
    }

    private void PopulateMandatoryDocumentsList()
    {
        ddlMandatoryDoc.DataSource = ESalesUnityContainer.Container.Resolve<IDocumentTypeService>().GetUniqueDocumentTypeList();
        ddlMandatoryDoc.DataBind();
        ddlMandatoryDoc.Items.Insert(0, new ListItem(Labels.CustomerCode, "0"));
    }

    private void FillBlankGrid()
    {
        ShowBlankRowInGrid<TruckDetailsDTO>(grdManageTrucks);
    }

    public void ShowDefaultManageTruckScreen()
    {
        FillGridWithTruckDetails(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.MANDATORYDOCTYPE]),
            Convert.ToString(ViewState[Globals.StateMgmtVariables.MANDATORYDOCNO]));
    }
}