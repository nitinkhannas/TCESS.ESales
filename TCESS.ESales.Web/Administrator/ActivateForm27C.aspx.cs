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
using Resources;

#endregion

public partial class Administrator_ActivateForm27C : BasePage
{  
    protected void Page_Init(object sender, EventArgs e)
    {
        ucActivateCustomers.Event_PrintCustomer += ucActivateCustomers_Event_PrintCustomer;        
    }

    private void ucPrintCustomers_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }

    private void ucActivateCustomers_Event_PrintCustomer(int customerId)
    {
        lblPageName.Text = string.Empty;

        //Sets visibility of frames that contains user controls
        pnlActivateCustomers.Visible = false;        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckIsUserAuthenticated();

        if (!IsPostBack)
        {
            ShowInitialValues();
        }
    }

    /// <summary>
    /// Show Page Values when it initially Loads or Refreshes
    /// </summary>
    private void ShowInitialValues()
    {
        //Sets visibility of frames that contains user controls
        pnlActivateCustomers.Visible = true;

        lblPageName.Text = "Activate Form 27C";
    }

}