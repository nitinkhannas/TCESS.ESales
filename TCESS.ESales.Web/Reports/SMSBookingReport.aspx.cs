using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

public partial class Reports_SMSBookingReport :BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucLoadingSMSBookingData.Event_LoadReport += ucLoadingSMSBookingData_Event_LoadReport;
        ucLoadingSMSBookingReport.Event_CloseScreen += ucLoadingSMSBookingReport_Event_CloseScreen;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //base.CheckIsUserAuthenticated();
        if (!IsPostBack)
        {
            ShowInitialValues();
            GetSmsDetails();
        }
    }

    /// <summary>
    /// Show Page Values when it initially Loads or Refreshes
    /// </summary>
    private void ShowInitialValues()
    {
        //Sets visibility of frames that contains user controls
        pnlLoadingSMSBookingData.Visible = true;
        pnlLoadingSMSBookingReport.Visible = false;
    }

    public void ucLoadingSMSBookingData_Event_LoadReport(int agentId, DateTime fromDate, DateTime toDate)
    {
        DropDownList ddlSmsStatus = (DropDownList)ucLoadingSMSBookingData.FindControl("ddlSmsStatus");
        pnlLoadingSMSBookingData.Visible = false;
        pnlLoadingSMSBookingReport.Visible = true;
        ucLoadingSMSBookingReport.LoadReport(agentId, fromDate, toDate, Convert.ToInt32(ddlSmsStatus.SelectedItem.Value));
    }

    public void ucLoadingSMSBookingReport_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }

    /// <summary>
    /// Will get the details like SMSReceived, Daily SMS Limit, SMS Accepted and SMS Balance
    /// </summary>
    /// <param></param>
    /// <returns></returns>
    private void GetSmsDetails()
    {
        IList<SMSLimitDTO> lstSMSLimitDTO = ESalesUnityContainer.Container.Resolve<ISMSLimitService>().GetSMSLimitList(DateTime.Now.Date.AddDays(0));
        IList<SMSRegistrationDTO> lstCustomerDTO = ESalesUnityContainer.Container.Resolve<ISMSService>().GetTotalSMSDetailsForDate(DateTime.Now.Date.AddDays(0));

        lblSmsReceived.Text = Convert.ToString(lstCustomerDTO.Count);
        lblSmsAccepted.Text = Convert.ToString(lstCustomerDTO.Count(F => F.SMSReg_BookingStatus == true));
        int limit = 0;
        if (lstSMSLimitDTO.Count > 0)
        {
            limit = (from F in lstSMSLimitDTO where F.SMSLimit_IsActive == true select F.SMSLimit_Limit).FirstOrDefault();
        }
        lblSmsLimit.Text = Convert.ToString(limit);
        lblSmsBalance.Text = Convert.ToString(limit - Convert.ToInt32(lblSmsAccepted.Text));
    }
}