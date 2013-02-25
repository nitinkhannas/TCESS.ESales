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

public partial class CustomerRegistration_ReActivateCustomers : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.CheckIsUserAuthenticated();

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
        lblPageName.Text = "Re" + Labels.ActivateCustomers;
    }
}