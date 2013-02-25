using System;
using TCESS.ESales.CommonLayer.Exception;
using TCESS.ESales.CommonLayer.CommonLibrary;

public partial class CustomerRegistration_CustomerRelationship : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        //Custom events from Customer Registration Page
        ucCustomerRegistration.Event_ShowCustomerDocumentScreen += ucCustomerRegistration_Event_ShowCustomerDocumentScreen;

        //Custom events from Customer Document Registration Page
        ucCustomerDocumentRegistration.Event_ShowCustomerRegistrationScreen += ucCustomerDocumentRegistration_Event_ShowCustomerRegistrationScreen;
        ucCustomerDocumentRegistration.Event_ShowAddTruckScreen += ucCustomerDocumentRegistration_Event_ShowAddTruckScreen;
        ucCustomerDocumentRegistration.Event_ShowAddAuthRepScreen += ucCustomerDocumentRegistration_Event_ShowAddAuthRepScreen;
        ucCustomerDocumentRegistration.Event_ShowCustomerReport+=ucCustomerDocumentRegistration_Event_ShowCustomerReport;

        //Custom events from Truck Registration Page
        ucTruckRegistration.Event_ShowCustomerDocumentRegistrationScreen += ucTruckRegistration_Event_ShowCustomerDocumentRegistrationScreen;

        //Custom events from AuthRep Registration Page
        ucAuthorizedRepresentative.Event_ShowCustomerDocumentRegistrationScreen += ucAuthorizedRepresentative_Event_ShowCustomerDocumentRegistrationScreen;

        //Custom events from Customer Relationship Report Page
        ucCustomerRelationshipReport.Event_ShowCustomerDocumentScreen += ucCustomerRelationshipReport_Event_ShowCustomerDocumentScreen;
    }

    void ucCustomerRelationshipReport_Event_ShowCustomerDocumentScreen(int customerId, bool isEdit, string filePath)
    {
        ShowCustomerDocumentScreen(customerId, isEdit);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckIsUserAuthenticated();

        if (!IsPostBack)
        {
            ShowInitialValues();
        }
    }

    void ucAuthorizedRepresentative_Event_ShowCustomerDocumentRegistrationScreen(int customerId, bool isEdit, string filePath)
    {
        ShowCustomerDocumentScreen(customerId, isEdit);
    }

    void ucTruckRegistration_Event_ShowCustomerDocumentRegistrationScreen(int customerId, bool isEdit, string filePath)
    {
        ShowCustomerDocumentScreen(customerId, isEdit);
    }

    void ucCustomerDocumentRegistration_Event_ShowCustomerReport(int customerId)
    {
        //Sets visibility of frames that contains user controls
        pnlCustomerRegistration.Visible = false;
        pnlCustomerDocumentRegistration.Visible = false;
        pnlTruckRegistration.Visible = false;
        pnlAuthRepRegistration.Visible = false;
        pnlCustomerRelationshipReport.Visible = true;

        ucCustomerRelationshipReport.ShowCustomerDetails(customerId);
    }

    void ucCustomerDocumentRegistration_Event_ShowAddAuthRepScreen(int customerId, bool isFirstAuthRep, string filePath)
    {
        //Sets visibility of frames that contains user controls
        pnlCustomerRegistration.Visible = false;
        pnlCustomerRelationshipReport.Visible = false;
        pnlCustomerDocumentRegistration.Visible = false;
        pnlTruckRegistration.Visible = false;
        pnlAuthRepRegistration.Visible = true;

        ucAuthorizedRepresentative.ShowBlankScreen(customerId, isFirstAuthRep, filePath);
    }

    void ucCustomerDocumentRegistration_Event_ShowAddTruckScreen(int customerId, bool isFirstTruck, string filePath)
    {
        //Sets visibility of frames that contains user controls
        pnlCustomerRegistration.Visible = false;        
        pnlAuthRepRegistration.Visible = false;
        pnlCustomerRelationshipReport.Visible = false;
        pnlCustomerDocumentRegistration.Visible = false;
        pnlTruckRegistration.Visible = true;

        ucTruckRegistration.ShowBlankScreen(customerId, true, filePath);
    }

    void ucCustomerDocumentRegistration_Event_ShowCustomerRegistrationScreen(object sender)
    {
        ShowInitialValues();
    }

    private void ucCustomerRegistration_Event_ShowCustomerDocumentScreen(int customerId, bool isEdit, string filePath)
    {
        ShowCustomerDocumentScreen(customerId, isEdit);
    }

    private void ShowCustomerDocumentScreen(int customerId, bool isEdit)
    {
        //Sets visibility of frames that contains user controls
        pnlCustomerRegistration.Visible = false;
        pnlTruckRegistration.Visible = false;
        pnlAuthRepRegistration.Visible = false;
        pnlCustomerRelationshipReport.Visible = false;
        pnlCustomerDocumentRegistration.Visible = true;

        ucCustomerDocumentRegistration.PopulateCustomerDetails(customerId, isEdit);
    }

    /// <summary>
    /// Show Page Values when it initially Loads or Refreshes
    /// </summary>
    private void ShowInitialValues()
    {
        //Sets visibility of frames that contains user controls
        pnlCustomerDocumentRegistration.Visible = false;
        pnlTruckRegistration.Visible = false;
        pnlAuthRepRegistration.Visible = false;
        pnlCustomerRelationshipReport.Visible = false;
        pnlCustomerRegistration.Visible = true;

        ucCustomerRegistration.ShowBlankScreen();
    }
}