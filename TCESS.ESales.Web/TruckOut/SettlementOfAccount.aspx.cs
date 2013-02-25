using System;

public partial class TruckOut_SettlementOfAccount : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucSettlementOfAccounts.Event_ShowHandlingBillReport += ucSettlementOfAccounts_Event_ShowHandlingBillReport;
        ucHandlingBillReport.Event_CloseScreen += ucHandlingBillReport_Event_CloseScreen;
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            ShowInitialValues();
        }
    }

    void ucHandlingBillReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }

    void ucSettlementOfAccounts_Event_ShowHandlingBillReport(int accountId)
    {
        //Sets visibility of frames that contains user controls
        pnlSettlementOfAccounts.Visible = false;
        pnlHandlingBillReport.Visible = true;

        ucHandlingBillReport.GetSettlementOfAccountDetails(accountId);
    }

    /// <summary>
    /// Show Page Values when it initially Loads or Refreshes
    /// </summary>
    private void ShowInitialValues()
    {
        //Sets visibility of frames that contains user controls
        pnlHandlingBillReport.Visible = false;
        pnlSettlementOfAccounts.Visible = true;
    }
}