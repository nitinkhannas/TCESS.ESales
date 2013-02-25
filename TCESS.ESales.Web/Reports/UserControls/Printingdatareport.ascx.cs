using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Microsoft.Reporting.WebForms;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Reports;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

public partial class Reports_UserControls_Printingdatareport : BaseUserControl 
{
    public event CloseScreenEventHandler Event_CloseScreen;
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    public void LoadReport(string Truckno)
    {
        SMSRegistrationDTO _smsRegistrationObj = new SMSRegistrationDTO();
         _smsRegistrationObj = ESalesUnityContainer.Container.Resolve<ISMSService>().GetPreviousdDateSMSDetailsByTruckNo(Truckno);
         SettlementOfAccountsDTO lastSettlementdetails = ESalesUnityContainer.Container.Resolve<ISettlementOfAccountsService>().GetLastSettlementOfAccountsByTruckNo(Truckno);
         if (lastSettlementdetails.Account_Id > 0)
         {
             _smsRegistrationObj.SMSReg_Last_Settlement_CreatedDate = lastSettlementdetails.Account_CreatedDate.ToString();
             _smsRegistrationObj.SMSReg_Last_Settlement_Dist = lastSettlementdetails.Account_Booking_Cust_District_Name;
         }
         AffidavitDetailsDTO _affidavitDetails = new AffidavitDetailsDTO();
         _affidavitDetails = ESalesUnityContainer.Container.Resolve<IAffidavitDetails>().GetAffidavitDetailsByCustId(_smsRegistrationObj.SMSReg_CustId);
        List<SMSRegistrationDTO> lstLoadingSMSBookingRpt = new List<SMSRegistrationDTO>();
        lstLoadingSMSBookingRpt.Add(_smsRegistrationObj);
        
        
        //Reset report viewer control
        reportViewer.Reset();

        //Initializes report viewer and set report as embedded resource
        Common.SetReportEmbeddedResource(reportViewer, "TCESS.ESales.CommonLayer.Reports.Printingreport.rdlc");

        ReportDataSource loadingSMSBookingDataSource = new ReportDataSource("dsSMSBookingReport", lstLoadingSMSBookingRpt);
        reportViewer.LocalReport.DataSources.Add(loadingSMSBookingDataSource);

        string affidavitStatus = string.Empty;

        if (_affidavitDetails.Affidavit_CustID > 0)
        {
            affidavitStatus = "Yes";
        }
        else
        {
            affidavitStatus = "No";
        }

        ReportParameter Affidavit = new ReportParameter("Affidavit", affidavitStatus);

        reportViewer.LocalReport.SetParameters(new ReportParameter[] { Affidavit });
    }

    protected void btnReturn_Click(object sender, System.EventArgs e)
    {
        //Returns to loading advice data screen
        Event_CloseScreen(sender);
    }
}
