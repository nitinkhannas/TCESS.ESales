#region Namespace

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.CommonLayer.CommonLibrary;

#endregion

public partial class CustomerRegistration_ViewCustomer : BasePage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		CheckIsUserAuthenticated();

		if (!IsPostBack)
		{
			LoadAllCustomers();
		}
	}

	private void LoadAllCustomers()
	{
		IList<CustomerDTO> lstCustomer = new List<CustomerDTO>();
        lstCustomer = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetCustomersByCustomerStatus(false);

        if (lstCustomer.Count > 0)
		{
            grdManageCustomers.DataSource = lstCustomer;
			grdManageCustomers.DataBind();
		}
		else
		{
			ShowBlankGrid();
		}
	}

	private void ShowBlankGrid()
	{
		ShowBlankRowInGrid<CustomerDTO>(grdManageCustomers);
	}

	protected void grdManageCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		if (e.CommandName == Globals.GridCommandEvents.VIEW)
		{
            Session[Globals.StateMgmtVariables.CUSTOMERID] = e.CommandArgument;
            Session[Globals.StateMgmtVariables.VIEWCUSTOMERSOURCE] = 2;
			Response.Redirect("ViewCustomerDetails.aspx");
		}
	}
}