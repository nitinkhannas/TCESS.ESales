using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCESS.ESales.CommonLayer.Reports;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.DataTransferObjects;
using Microsoft.Reporting.WebForms;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using Microsoft.Practices.Unity;

public partial class TruckOut_UserControls_HandlingBillReport : BaseUserControl
{
    public CloseScreenEventHandler Event_CloseScreen;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.SetReportEmbeddedResource(reportViewer, "TCESS.ESales.CommonLayer.Reports.HandlingBillReport.rdlc");
        }
    }

    public void GetSettlementOfAccountDetails(int accountId)
    {
        SettlementOfAccountsDTO settlementOfAcct = ESalesUnityContainer.Container.Resolve<ISettlementOfAccountsService>()
            .GetSettlementOfAccountsById(accountId);

        SetReportParameters(settlementOfAcct, reportViewer);

        reportViewer.LocalReport.Refresh();
    }

    private void SetReportParameters(SettlementOfAccountsDTO settlementOfAcct, ReportViewer reportViewer)
    {
        string truckRegNo = string.Empty;
        decimal grossAmount = settlementOfAcct.Account_Quantity *  settlementOfAcct.booking.Booking_MaterialType_HandlingRate;
        decimal serviceTaxOnAmount = (grossAmount * settlementOfAcct.booking.Booking_MaterialType_ServiceTax) / 100;
        decimal educationCessOnAmount = (serviceTaxOnAmount * settlementOfAcct.booking.Booking_MaterialType_EducationCess) / 100;
        decimal higherEducationCessOnAmount = (serviceTaxOnAmount * settlementOfAcct.booking.Booking_MaterialType_HigherEducationCess) / 100;
        decimal netAmount = grossAmount + serviceTaxOnAmount + educationCessOnAmount + higherEducationCessOnAmount;
        
        ReportParameter SNo = new ReportParameter("SNo", settlementOfAcct.booking.Booking_Agent_AgentShortName + "/" + settlementOfAcct.Account_Id);
        ReportParameter Date = new ReportParameter("Date", Convert.ToDateTime(settlementOfAcct.Account_CreatedDate).ToString());
        ReportParameter CustName = new ReportParameter("CustName", settlementOfAcct.booking.Booking_Cust_UnitName);
        ReportParameter Address = new ReportParameter("Address", settlementOfAcct.booking.Booking_Cust_UnitAddress);
        ReportParameter District = new ReportParameter("District", settlementOfAcct.booking.Booking_Cust_District_Name);
        ReportParameter State = new ReportParameter("State", settlementOfAcct.booking.Booking_Cust_State_Name);
        ReportParameter MatTypeName = new ReportParameter("MatTypeName", settlementOfAcct.booking.Booking_MaterialType_MaterialName);
        ReportParameter Quantity = new ReportParameter("Quantity", Convert.ToString(settlementOfAcct.Account_Quantity));
        ReportParameter Rate = new ReportParameter("Rate", Convert.ToString(settlementOfAcct.booking.Booking_MaterialType_HandlingRate));
        ReportParameter Amount = new ReportParameter("Amount", Convert.ToString(String.Format("{0:F2}", grossAmount)));

        if (settlementOfAcct.booking.Booking_TruckType == true)
        {
            truckRegNo = settlementOfAcct.booking.Booking_StandaloneTruck_RegNo;            
        }
        else
        {
            truckRegNo = settlementOfAcct.booking.Booking_Truck_RegNo;
        }

        ReportParameter TruckNo = new ReportParameter("TruckNo", truckRegNo);
        ReportParameter InvoiceNo = new ReportParameter("InvoiceNo", settlementOfAcct.Account_InvoiceNumber);
        ReportParameter ServiceTax = new ReportParameter("ServiceTax", Convert.ToString(String.Format("{0:F2}", settlementOfAcct.booking.Booking_MaterialType_ServiceTax)));
        ReportParameter EducationCess = new ReportParameter("EducationCess", Convert.ToString(String.Format("{0:F2}", settlementOfAcct.booking.Booking_MaterialType_EducationCess)));
        ReportParameter HigherEducationCess = new ReportParameter("HigherEducationCess", Convert.ToString(String.Format("{0:F2}", settlementOfAcct.booking.Booking_MaterialType_HigherEducationCess)));

        CurrencyConvertor currenyInWords = new CurrencyConvertor();
        string amountInWords = currenyInWords.Convertor(Convert.ToString(String.Format("{0:F2}", netAmount)));
        ReportParameter RupeesInWords = new ReportParameter("RupeesInWords", amountInWords);
        ReportParameter TotalAmount = new ReportParameter("TotalAmount", Convert.ToString(String.Format("{0:F2}",netAmount)));

        ReportParameter ServiceTaxAmt = new ReportParameter("ServiceTaxAmt", Convert.ToString(String.Format("{0:F2}", serviceTaxOnAmount)));
        ReportParameter EducationCessAmt = new ReportParameter("EducationCessAmt", Convert.ToString(String.Format("{0:F2}", educationCessOnAmount)));
        ReportParameter HigherEducationCessAmt = new ReportParameter("HigherEducationCessAmt", Convert.ToString(String.Format("{0:F2}", higherEducationCessOnAmount)));

        reportViewer.LocalReport.SetParameters(new ReportParameter[] {SNo, Date, CustName, Address, District, State, 
            MatTypeName, Quantity, Rate, Amount, TruckNo, InvoiceNo, ServiceTax, EducationCess, HigherEducationCess, 
            RupeesInWords, TotalAmount, ServiceTaxAmt, EducationCessAmt, HigherEducationCessAmt});
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Event_CloseScreen(sender);
    }
}