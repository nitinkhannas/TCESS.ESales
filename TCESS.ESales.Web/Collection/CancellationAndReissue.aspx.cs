#region Using directives

using System;

#endregion

public partial class Collection_CancellationAndReissue : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucCancelCollectionReceipt.Event_ShowCollectionScreen += ucCancelCollectionReceipt_Event_ShowCollectionScreen;
        ucPaymentCollection.Event_ShowCancellationScreen += ucPaymentCollection_Event_ShowCancellationScreen;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            base.CheckIsUserAuthenticated();
        }
    }

    private void ucCancelCollectionReceipt_Event_ShowCollectionScreen(int collectionId, 
        int paymentModeId, int custId)
    {
        //Sets visibility of frames that contains user controls
        pnlCancelCollectionReceipt.Visible = false;
        pnlReIssue.Visible = true;
        ucPaymentCollection.SetPaymentModeForUser(collectionId, paymentModeId, custId);
    }

    private void ucPaymentCollection_Event_ShowCancellationScreen(object sender)
    {
        //Sets visibility of frames that contains user controls
        pnlReIssue.Visible = false;
        pnlCancelCollectionReceipt.Visible = true;
        ucCancelCollectionReceipt.ShowCollectionDetailsForCancellation();
    }
}