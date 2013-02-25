using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CustomerRegistration_CustomerReValidation : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucCustomerReValidation.Event_ShowCustomerDocumentReValidationScreen += ShowCustomerDocumentReValidationScreen;
        ucCustomerDocumentReValidate.Event_ShowCustomerReport += ucCustomerDocumentRegistration_Event_ShowCustomerReport;
        ucCustomerDocumentReValidate.Event_ShowParnetScreen += ShowParnetScreen;
        ucCustomerDocumentReValidate.Event_CloseScreen += ucCustomerDocumentReValidate_Event_CloseScreen;
        ucCustomerRelationshipReport.Event_ShowCustomerDocumentScreen += ShowDocumentScreen;

        ucCustomerPartner.Event_ShowCustomerDocumentReValidationScreen += ShowCustomerDocumentReValidationScreen;

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckIsUserAuthenticated();

        if (!IsPostBack)
        {
            ShowInitialValues();
        }
    }
    private void ShowInitialValues()
    {
        //Sets visibility of frames that contains user controls
        pnlCustomerReValidation.Visible = true;
        pnlCustomerDocumentReValidate.Visible = false;
        pnlCustomerPartner.Visible = false;
        ucCustomerRelationshipReport.Visible = false;
        ucCustomerReValidation.ShowBlankScreen();
    }
    private void ShowCustomerDocumentReValidationScreen(int customerId, bool isEdit, string filePath)
    {
        pnlCustomerReValidation.Visible = false;
        pnlCustomerDocumentReValidate.Visible = true;
        pnlCustomerPartner.Visible = false;
        ucCustomerDocumentReValidate.PopulateCustomerDetails(customerId, true);
    }
    public void ShowParnetScreen(int customerId, bool isEdit, string filePath)
    {
        pnlCustomerPartner.Visible = true;
        pnlCustomerReValidation.Visible = false;
        pnlCustomerDocumentReValidate.Visible = false;
        ucCustomerRelationshipReport.Visible = false;
        ucCustomerPartner.ShowBlankScreen(customerId, isEdit, filePath);


    }
    public void ucCustomerDocumentReValidate_Event_CloseScreen(object sender )
    {
        //ShowInitialValues();
        Response.Redirect(Request.Url.AbsolutePath);
    }
    void ucCustomerDocumentRegistration_Event_ShowCustomerReport(int customerId)
    {
        //Sets visibility of frames that contains user controls
        pnlCustomerReValidation.Visible = false;
        pnlCustomerDocumentReValidate.Visible = false;
        pnlCustomerPartner.Visible = false;
        pnlCustomerRelationshipReport.Visible = true;
        ucCustomerRelationshipReport.Visible = true;
        ucCustomerRelationshipReport.ShowCustomerDetails(customerId);
    }

    void ShowDocumentScreen(int customerId, bool isEdit, string filePath)
    {
        pnlCustomerReValidation.Visible = false;
        pnlCustomerDocumentReValidate.Visible = true;
        pnlCustomerPartner.Visible = false;
        ucCustomerRelationshipReport.Visible = false;
        ucCustomerDocumentReValidate.PopulateCustomerDetails(customerId, true);
    }
}