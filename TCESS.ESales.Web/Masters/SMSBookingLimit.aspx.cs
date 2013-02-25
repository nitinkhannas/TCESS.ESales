# region Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCESS.ESales.CommonLayer.CommonLibrary;
using System.Web.Security;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using Microsoft.Practices.Unity;
using System.Collections;
using TCESS.ESales.PersistenceLayer.Entity;
using TCESS.ESales.PersistenceLayer.Interfaces;

# endregion

public partial class Bookings_SMSBookingLimit : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Getting referring page's URL
            ViewState["PreviousPage"] = Request.UrlReferrer;
            GetSMSLimit();
        }
    }

    private void GetSMSLimit()
    {
        IList<SMSLimitDTO> LstSMSLimitDTO = ESalesUnityContainer.Container.Resolve<ISMSLimitService>().GetSMSLimitList(DateTime.Now.Date.AddDays(0));

        if (LstSMSLimitDTO.Count > 0)
        {
            grdSMSLimit.DataSource = LstSMSLimitDTO;
            grdSMSLimit.DataBind();
        }
        else
        {
            string previousPage = ViewState["PreviousPage"].ToString();
            ShowBlankRowInGrid<SMSLimitDTO>(grdSMSLimit);

            if ((previousPage != null) && !(previousPage.Contains("Bookings/SMSConfirmation.aspx")))
            {
                ucMessageBoxForGrid.ShowMessage("SMS Limit not set for today. Please set the limit");
            }
        }
        GetTodaysDateAndSmsDetails();
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
    }

    protected void grdSMSLimit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals(Globals.GridCommandEvents.ADDNEW))
        {
            if (Page.IsValid)
            {
                IList<SMSRegistrationDTO> lstCustomerDTO = ESalesUnityContainer.Container.Resolve<ISMSService>().GetTotalSMSDetailsForDate(DateTime.Now.Date.AddDays(0));
                int accepted = lstCustomerDTO.Count(F => F.SMSReg_BookingStatus == true);

                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                SMSLimitDTO SMSLimitDetails = new SMSLimitDTO();

                if (Convert.ToInt32(((TextBox)row.FindControl("txtNewSMSLimit")).Text) > accepted)
                {
                    SMSLimitDetails.SMSLimit_Limit = Convert.ToInt32(((TextBox)row.FindControl("txtNewSMSLimit")).Text);
                    SMSLimitDetails.SMSLimit_Date = DateTime.Now.Date;
                    SMSLimitDetails.SMSLimit_IsActive = true;
                    SMSLimitDetails.SMSLimit_CreatedDate = DateTime.Now;
                    SMSLimitDetails.SMSLimit_LastUpdatedDate = DateTime.Now;
                    SMSLimitDetails.SMSLimit_CreatedBy = GetCurrentUserId();
                    SMSLimitDetails.SMSLimit_AuthorizedBy = ((TextBox)row.FindControl("txtAuthorizedBy")).Text;

                    ESalesUnityContainer.Container.Resolve<ISMSLimitService>().SaveSMSLimit(SMSLimitDetails);
                    ucMessageBoxForGrid.ShowMessage(Resources.Messages.SMSLimitSavedSuccessfully);
                    GetSMSLimit();
                }
                else
                {
                    ucMessageBoxForGrid.ShowMessage(Resources.Messages.InvalidSMSLimit);
                }
            }
        }
    }

    private void GetTodaysDateAndSmsDetails()
    {
        lblCurrentDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
        lblNextDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now.Date.AddDays(1));

        IList<SMSRegistrationDTO> lstCustomerDTO = ESalesUnityContainer.Container.Resolve<ISMSService>().GetTotalSMSDetailsForDate(DateTime.Now.Date.AddDays(0));
        lblSmsReceived.Text = Convert.ToString(lstCustomerDTO.Count);
        lblSmsAccepted.Text = Convert.ToString(lstCustomerDTO.Count(F => F.SMSReg_BookingStatus == true));
    }
}
