#region Namespace

using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Bookings_ManageMoneyReceipt : BasePage
{

    protected void Page_Init(object sender, EventArgs e)
    {
        ucManageMoneyReceipt.Event_ShowCanceMoneyReceiptScreen += ucManageMoneyReceipt_Event_ShowCanceMoneyReceiptScreen;
        ucCancelMoneyReceipt.Event_CloseScreen += ucCancelMoneyReceipt_Event_CloseScreen;
    }    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowInitialValues();
        }
    }

    void ucCancelMoneyReceipt_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }

    void ucManageMoneyReceipt_Event_ShowCanceMoneyReceiptScreen(int moneyReceiptId)
    {
        //Sets visibility of frames that contains user controls
        pnlManageMoneyReceipts.Visible = false;
        pnlCancelMoneyReceipt.Visible = true;
        
        ucCancelMoneyReceipt.PopulateMoneyReceiptDetail(moneyReceiptId);        
    }

    /// <summary>
    /// Show Page Values when it initially Loads or Refreshes
    /// </summary>
    private void ShowInitialValues()
    {
        //Sets visibility of frames that contains user controls
        pnlCancelMoneyReceipt.Visible = false;
        pnlManageMoneyReceipts.Visible = true;

        ucManageMoneyReceipt.GetAllMoneyReceiptsForAgents();
    }
}