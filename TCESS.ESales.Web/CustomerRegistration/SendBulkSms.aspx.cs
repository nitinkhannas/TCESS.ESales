#region Namespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.BusinessLayer.Services;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class CustomerRegistration_SendBulkSms : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // CheckIsUserAuthenticated();
    }

    private void SendToSingle()
    {
        string Smstext = txtMessage.Text.Trim();
        string mobileNo = txtMobile.Text.Trim();
        SmsUtility.SendSMS(mobileNo, Smstext);
    }

    protected void SendYestarday()
    {
        IList<SMSRegistrationDTO> lstCustomerDTO = ESalesUnityContainer.Container.Resolve<ISMSService>().GetTotalSMSDetailsForDate(DateTime.Now.Date.AddDays(-1));
        IList<SMSRegistrationDTO> filteredCustomerDTO = (from F in lstCustomerDTO
                                                         where F.SMSReg_IsDeleted == false && F.SMSReg_BookingStatus == true
                                                         select F).ToList();
        foreach (SMSRegistrationDTO customer in filteredCustomerDTO)
        {
            string mobileNo = customer.SMSReg_Cust_PhoneNumber;
            SmsUtility.SendSMS(mobileNo, txtMessage.Text.Trim());
        }
    }

    protected void SendToday()
    {
        int count = 0;
        IList<SMSRegistrationDTO> lstCustomerDTO = ESalesUnityContainer.Container.Resolve<ISMSService>().GetTotalSMSDetailsForDate(DateTime.Now.Date.AddDays(0));

        foreach (SMSRegistrationDTO customer in lstCustomerDTO)
        {
            string mobileNo = customer.SMSReg_Cust_PhoneNumber;
            SmsUtility.SendSMS(mobileNo, txtMessage.Text.Trim());
            count = count + 1;
            LabelCount.Text = "Count=" + count;
        }
    }

    private void SendToAllCustomer()
    {
        IList<CustomerDTO> listMaterialAllocations = new List<CustomerDTO>();
        int count = 0;
        IList<CustomerDTO> lstCustomerDTO = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetCustomerForSMSSending();
        foreach (CustomerDTO customer in lstCustomerDTO)
        {
            string mobileNo = customer.Cust_MobileNo;
            SmsUtility.SendSMS(mobileNo, txtMessage.Text.Trim());
            count = count + 1;
            LabelCount.Text = "Count=" + count;
        }
    }

    private void sendCodeAllocSMS()
    {
        IList<CustomerDTO> listMaterialAllocations = new List<CustomerDTO>();
        int count = 0;
        IList<CustomerDTO> lstCustomerDTO = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetNewCustomerForSMSSending();
        foreach (CustomerDTO customer in lstCustomerDTO)
        {
            string smsText = "Apke Unit " + customer.Cust_TradeName + " ka Code Number " + customer.Cust_Code + " hai. SMS Booking Sewa shuru ki ja rahi hai. Hamara sales office aapse jald hi sampark karega. DCA GHATO";
            string newSmstext1 = "Tailings ki sale SMS booking se shuru ho rahi hain. SMS karen BOOK Code T TruckNumber." +
                     "Jaise BOOK " + customer.Cust_Code + " T BR02A3456 aur 56677 per bheje.";
            string newSmstext2 = "Iske jawab mein aapko Ghato se booking number prapt hoga, jis per booking ki jayegi. DCAGhato.";
            string mobileNo = customer.Cust_MobileNo;
            SmsUtility.SendSMS(mobileNo, smsText);
            SmsUtility.SendSMS(mobileNo, newSmstext1);
            SmsUtility.SendSMS(mobileNo, newSmstext2);
            customer.Cust_SmsSent = true;
            customer.Cust_LastUpdatedDate = DateTime.Now;
            ESalesUnityContainer.Container.Resolve<ICustomerService>().UpdateCustomerDetails(customer);
            count = count + 1;
            LabelCount.Text = "Count=" + count;
        }
    }

    protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAction.SelectedValue == "4" || ddlAction.SelectedValue == "6")
        {
            trMobile.Visible = true;
            if (ddlAction.SelectedValue == "6")
            {
                trMobile.Visible = false;
                txtMessage.Visible = false;
            }
        }
        else
        {
            trMobile.Visible = false;
        }
    }

    protected void btnSendSMS_Click(object sender, EventArgs e)
    {
        if (ddlAction.SelectedValue == "1")
        {
            SendToAllCustomer();
        }
        else if (ddlAction.SelectedValue == "2")
        {
            SendYestarday();
        }
        else if (ddlAction.SelectedValue == "3")
        {
            SendToday();
        }
        else if (ddlAction.SelectedValue == "4")
        {
            SendToSingle();
        }
        else if (ddlAction.SelectedValue == "5")
        {
            sendCodeAllocSMS();
        }
        else if (ddlAction.SelectedValue == "6")
        {
            SmsUtility.GetTodayReport();
        }
    }
}