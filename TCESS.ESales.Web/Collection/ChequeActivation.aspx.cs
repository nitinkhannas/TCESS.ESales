#region Using directives

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces.GhatoCollection;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects.GhatoCollection;

#endregion

public partial class GhatoCashCollection_ChequeActivation : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucChequeActivation.Event_ShowEditScreen += ucChequeActivation_Event_ShowEditScreen;
        ucEditCheque.Event_ShowChequeActivationScreen += ucEditCheque_Event_ShowChequeActivationScreen;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            base.CheckIsUserAuthenticated();
            ShowInitialValues();
        }
    }

    void ucEditCheque_Event_ShowChequeActivationScreen(object sender)
    {
        ShowInitialValues();
        ucChequeActivation.ShowChequeDetails();
    }

    void ucChequeActivation_Event_ShowEditScreen(int chequeId)
    {
        //Sets visibility of frames that contains user controls
        pnlChequeActivation.Visible = false;
        pnlEditCheque.Visible = true;
        ucEditCheque.ShowChequeDetails(chequeId);
    }

    /// <summary>
    /// Show Page Values when it initially Loads or Refreshes
    /// </summary>
    private void ShowInitialValues()
    {
        //Sets visibility of frames that contains user controls
        pnlEditCheque.Visible = false;
        pnlChequeActivation.Visible = true;
    }
}