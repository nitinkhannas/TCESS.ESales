#region Using directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Exception;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class CustomerRegistration_UserControls_CustomerPartner : BaseUserControl
{
    public ShowDataEventHandler Event_ShowCustomerDocumentReValidationScreen;
    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }
    public void ShowBlankScreen(int customerId, bool isFirstAuthRep, string folderName)
    {
        ViewState[Globals.StateMgmtVariables.CUSTOMERID] = customerId;
        ResetFields();
        BindList();
    }
    

    private void ResetFields()
    {
        txtPatnerName.Text = string.Empty;
        txtPatnerFatherName.Text = string.Empty;
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetFields();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Event_ShowCustomerDocumentReValidationScreen(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]), false, string.Empty);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        CustomerPartnerDTO customerPartner = new CustomerPartnerDTO();
        customerPartner.Cust_Partner_FatherName = txtPatnerFatherName.Text.Trim();
        customerPartner.Cust_Partner_Name = txtPatnerName.Text.Trim();
        customerPartner.Cust_Partner_CustId = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]);
        ESalesUnityContainer.Container.Resolve<ICustomerService>().SaveAndUpdateCustomerPatner(customerPartner);
        BindList();
        ResetFields();
    }


    private void BindList()
    {
        grdPatner.DataSource = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetPatnerList(Convert.ToInt32(ViewState[Globals.StateMgmtVariables.CUSTOMERID]));
        grdPatner.DataBind();
    }
    protected void grdPatner_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}