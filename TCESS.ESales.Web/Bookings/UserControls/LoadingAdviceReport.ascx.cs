using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCESS.ESales.CommonLayer.Reports;
using Microsoft.Reporting.WebForms;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using Microsoft.Practices.Unity;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.BarCodeGen;
using System.Drawing.Printing;

public partial class Bookings_UserControls_LoadingAdviceReport : BaseUserControl
{
	public event CloseScreenEventHandler Event_CloseScreen;
	/// <summary>
	/// Page Load event
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			Common.SetReportEmbeddedResource(reportViewer, "TCESS.ESales.CommonLayer.Reports.LoadingAdviceReport.rdlc");
		}
	}
	/// <summary>
	/// Function to get booking details by bookingId
	/// </summary>
	/// <param name="bookingId">Int32:BookinhId</param>
	public void GetBookingDetails(int bookingId)
	{
        SetReportParameters(MasterList.GetBookingDetailByBookingId(bookingId, true), reportViewer);

		reportViewer.LocalReport.Refresh();
	}
	/// <summary>
	/// Set Report Parameters by bookingDetails and reportViewer
	/// </summary>
	/// <param name="bookingDetails"></param>
	/// <param name="reportViewer"></param>
    private void SetReportParameters(BookingDTO bookingDetails, ReportViewer reportViewer)
    {
        string truckRegNo = string.Empty;
        string truckOwnerName = string.Empty;
        string truckDriverName = string.Empty;
        string truckOwnerShortAdd = string.Empty;
        string truckDriverShortAdd = string.Empty;
        BarcodeDTO b = new BarcodeDTO();
        SMSRegistrationDTO smsDetail = new SMSRegistrationDTO();
        smsDetail = ESalesUnityContainer.Container.Resolve<ISMSService>().GetSmsDetailsByBookingId(bookingDetails.Booking_Id);

        ReportParameter loadingAdviceFor = new ReportParameter("LoadingAdviceFor", bookingDetails.Booking_MaterialType_MaterialName);
        ReportParameter loadingAdviceNo = new ReportParameter("LoadingAdviceNo", bookingDetails.Booking_Agent_AgentShortName
            + "-" + bookingDetails.Booking_Id);
        ReportParameter sNO = new ReportParameter("SNo", Convert.ToString(bookingDetails.Booking_Id));
        ReportParameter bookingDate = new ReportParameter("BookingDate", Convert.ToDateTime(bookingDetails.Booking_Date).ToString("dd/MMM/yyyy"));
        ReportParameter matTypeName = new ReportParameter("MatTypeName", bookingDetails.Booking_MaterialType_MaterialName);
        ReportParameter matCode = new ReportParameter("MatCode", bookingDetails.Booking_MaterialType_Code);
        ReportParameter custCode = new ReportParameter("CustCode", bookingDetails.Booking_Cust_Code);
        ReportParameter smsId1 = new ReportParameter("SMSId1", Convert.ToString(smsDetail.SMSReg_Id));
        ReportParameter smsId2 = new ReportParameter("SMSId2", Convert.ToString(smsDetail.SMSReg_Id));

        if (bookingDetails.Booking_TruckType == true)
        {
            truckRegNo = bookingDetails.Booking_StandaloneTruck_RegNo;
            truckOwnerName = bookingDetails.Booking_StandaloneTruck_OwnerName + ',' + bookingDetails.Booking_StandaloneTruck_OwnerShortAdd;
            truckDriverName = bookingDetails.Booking_StandaloneTruck_DriverName + ',' + bookingDetails.Booking_StandaloneTruck_DriverShortAdd;
        }
        else
        {
            truckRegNo = bookingDetails.Booking_Truck_RegNo;
            truckOwnerName = bookingDetails.Booking_Truck_OwnerName + ',' + bookingDetails.Booking_Truck_OwnerShortAdd;
            truckDriverName = bookingDetails.Booking_Truck_DriverName + ',' + bookingDetails.Booking_Truck_DriverShortAdd;
        }

        ReportParameter truckNo = new ReportParameter("TruckNo", truckRegNo);
        ReportParameter truckOwner = new ReportParameter("TruckOwner", truckOwnerName);
        ReportParameter truckDriver = new ReportParameter("TruckDriver", truckDriverName);
        ReportParameter custName = new ReportParameter("CustName", bookingDetails.Booking_Cust_UnitName);
        ReportParameter address = new ReportParameter("Address", bookingDetails.Booking_Cust_UnitAddress);
        ReportParameter district = new ReportParameter("District", bookingDetails.Booking_Cust_District_Name);
        ReportParameter state = new ReportParameter("State", bookingDetails.Booking_Cust_State_Name);
        ReportParameter approxQty = new ReportParameter("AppQty", Convert.ToString(bookingDetails.Booking_Qty) + " M");

        CurrencyConvertor currencyConvertor = new CurrencyConvertor();

        MoneyReceiptDTO moneyReceiptDetails = MasterList.GetMoneyReceiptById(0, bookingDetails.Booking_Id);

        ReportParameter advanceAmount = new ReportParameter("AdvanceAmount",
            Convert.ToString(moneyReceiptDetails.MoneyReceipt_AmountPaid));
        ReportParameter amountInWords = new ReportParameter("AmountInWords",
            currencyConvertor.Convertor(moneyReceiptDetails.MoneyReceipt_AmountPaid.ToString()));
        ReportParameter moneyReceiptDate = new ReportParameter("MoneyReceiptDate",
            Convert.ToDateTime(moneyReceiptDetails.MoneyReceipt_CreateDate).ToString("dd/MMM/yyyy"));

        ReportParameter moneyReceiptSNo = new ReportParameter("MoneyReceiptSNo", Convert.ToString(moneyReceiptDetails.MoneyReceipt_Id));

        GenerateBarcode(bookingDetails.Booking_Id.ToString(), ref b);

        List<BarcodeDTO> barcodes = new List<BarcodeDTO>();
        barcodes.Add(b);
        reportViewer.LocalReport.DataSources.Clear();
        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", barcodes));

        reportViewer.LocalReport.SetParameters(new ReportParameter[] { loadingAdviceFor, loadingAdviceNo, sNO, 
			bookingDate, matTypeName, matCode, custCode, truckNo, truckOwner, truckDriver, custName, address, district, state,
			approxQty, advanceAmount, amountInWords, moneyReceiptSNo, moneyReceiptDate, smsId1, smsId2});
    }

	private void GenerateBarcode(string data, ref BarcodeDTO b)
	{
		int W = 200;
		int H = 100;
		AlignmentPositions Align = AlignmentPositions.CENTER;
		TYPE type = TYPE.CODE39;
		try
		{
			Barcode _barcode = new Barcode(data, type);
			if (type != TYPE.UNSPECIFIED)
			{
				_barcode.IncludeLabel = true;
				_barcode.Alignment = Align;
				_barcode.RotateFlipType = System.Drawing.RotateFlipType.RotateNoneFlipNone;
				_barcode.LabelPosition = LabelPositions.BOTTOMCENTER;
				_barcode.Width = W;
				_barcode.Height = H;
				_barcode.BackColor = System.Drawing.Color.White;
				_barcode.ForeColor = System.Drawing.Color.Black;
				b.BarcodeImage = _barcode.GetImageData(SaveTypes.GIF);
				b.BarcodeValue = _barcode.EncodedValue;
                
			}

		}//try
		catch (Exception ex)
		{

		}//catch
	}
	/// <summary>
	/// Event for return button click
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void btnReturn_Click(object sender, EventArgs e)
	{
		Event_CloseScreen(sender);
	}
}