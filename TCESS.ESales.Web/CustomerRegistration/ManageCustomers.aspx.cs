#region Using directives

using System;

#endregion

public partial class CustomerRegistration_ManageCustomers : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        //Custom events from Manage Customer Page
        ucManageCustomers.Event_ShowCustomerDocumentRegistrationScreen += new BaseUserControl.ShowDataEventHandler(ucManageCustomers_Event_ShowCustomerDocumentRegistrationScreen);
        ucManageCustomers.Event_ShowCustomerRegistrationScreen += new BaseUserControl.ShowDataByIdEventHandler(ucManageCustomers_Event_ShowCustomerRegistrationScreen);

        //Custom events from Customer Registration Page
        ucCustomerRegistration.Event_CloseScreen += ucCustomerRegistration_Event_CloseScreen;

        //Custom events from Customer Document Registration Page
        ucCustomerDocumentRegistration.Event_CloseScreen += ucCustomerDocumentRegistration_Event_CloseScreen;

        ucCustomerDocumentRegistration.Event_ShowCustomerReport += ucCustomerDocumentRegistration_Event_ShowCustomerReport;

        //Custom events from Customer Relationship Report Page
        ucCustomerRelationshipReport.Event_ShowCustomerDocumentScreen += ucCustomerRelationshipReport_Event_ShowCustomerDocumentScreen; 
    }

    void ucCustomerRelationshipReport_Event_ShowCustomerDocumentScreen(int customerId, bool isEdit, string path)
    {
        ShowCustomerDocumentRegistrationScreen(customerId, true);
    }

    void ucCustomerDocumentRegistration_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }

    void ucCustomerRegistration_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }

    void ucCustomerDocumentRegistration_Event_ShowCustomerReport(int customerId)
    {
        //Sets visibility of frames that contains user controls
        pnlCustomerDocumentRegistration.Visible = false;
        pnlManageCustomers.Visible = false;
        pnlCustomerRegistration.Visible = false;
        pnlCustomerRelationshipReport.Visible = true;

        ucCustomerRelationshipReport.ShowCustomerDetails(customerId);
    }

    void ucManageCustomers_Event_ShowCustomerRegistrationScreen(int customerId)
    {
        //Sets visibility of frames that contains user controls
        pnlCustomerDocumentRegistration.Visible = false;
        pnlManageCustomers.Visible = false;
        pnlCustomerRelationshipReport.Visible = false;
        pnlCustomerRegistration.Visible = true;

        ucCustomerRegistration.PopulateCustomerData(customerId);
    }

    void ucManageCustomers_Event_ShowCustomerDocumentRegistrationScreen(int customerId, bool isEdit, string filePath)
    {
        ShowCustomerDocumentRegistrationScreen(customerId, true);
    }

    private void ShowCustomerDocumentRegistrationScreen(int customerId, bool isEdit)
    {
        //Sets visibility of frames that contains user controls       
        pnlCustomerRegistration.Visible = false;
        pnlManageCustomers.Visible = false;
        pnlCustomerRelationshipReport.Visible = false;
        pnlCustomerDocumentRegistration.Visible = true;

        ucCustomerDocumentRegistration.PopulateCustomerDetails(customerId, true);
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
        pnlCustomerRegistration.Visible = false;
        pnlCustomerDocumentRegistration.Visible = false;
        pnlCustomerRelationshipReport.Visible = false;
        pnlManageCustomers.Visible = true;

        ucManageCustomers.ShowDefaultManageCustomerScreen();
    }
}