using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.BusinessLayer.Services;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using System.Linq;
using System.Collections;
using System.IO;
using System.Drawing.Printing;
using com.epson.pos.driver;
using System.Drawing;
using System.Text;
using System.Drawing.Imaging;

public partial class CustomerRegistration_UserControls_TruckVerification : BaseUserControl
{
    public event ShowPrintReportEventHandler Event_LoadReport;
   
 
    public string TruckNumber = null;
   
   protected void Page_Init(object sender, EventArgs e)
	{
		ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
          
            TruckNumber = null;
			FillBlankGrid();
		}
	}

	protected void btnValidate_Click(object sender, EventArgs e)
	{
		if (Page.IsValid)
		{
			FillGridWithTruckDetails(txtTruckNumber.Text.Trim());
		}
	}
    protected void btnPrint_Click(object sender, CommandEventArgs e)
    {
        
      
        TruckNumber = (string)e.CommandArgument;
        Event_LoadReport(TruckNumber);
     
    }
   
   

	private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
	{
		FillBlankGrid();
	}

	private void FillGridWithTruckDetails(string TruckNo)
	{
		TruckVerificationDTO truckDetails = new TruckVerificationDTO();
		truckDetails = ESalesUnityContainer.Container.Resolve<ITruckService>().GetAllTruckDetails(TruckNo);

		if (truckDetails.type > 0)
		{
			IList<TruckVerificationDTO> lstTruckDetails = new List<TruckVerificationDTO>();
			lstTruckDetails.Add(truckDetails);
			grdViewTruck.DataSource = lstTruckDetails;
			grdViewTruck.DataBind();
		}
		else
		{
			ucMessageBoxForGrid.ShowMessage(Resources.Messages.TruckDetailsDoesNotExist);
		}
	}

	private void FillBlankGrid()
	{
		ShowBlankRowInGrid<TruckVerificationDTO>(grdViewTruck);
	}

	protected IEnumerable grdViewTruck_MustAddARow(IEnumerable data)
	{
		return base.AddBlankRowInGrid<TruckVerificationDTO>();
	}


    protected string GetCustomerCode(string TruckNo)
    {
        if (!string.IsNullOrEmpty(TruckNo))
        {
            SMSRegistrationDTO _smsRegistrationObj = new SMSRegistrationDTO();
            _smsRegistrationObj = ESalesUnityContainer.Container.Resolve<ISMSService>().GetPreviousdDateSMSDetailsByTruckNo(TruckNo);
            return _smsRegistrationObj.SMSReg_Cust_Code == null ? Resources.Messages.NoRecordFound : _smsRegistrationObj.SMSReg_Cust_Code.ToString();
        }

        else
        {
            
            return Resources.Messages.NoRecordFound;
        }

    }

    protected string GetCustomerPhonenumber(string TruckNo)
    {
        if (!string.IsNullOrEmpty(TruckNo))
        {
            SMSRegistrationDTO _smsRegistrationObj = new SMSRegistrationDTO();
            _smsRegistrationObj = ESalesUnityContainer.Container.Resolve<ISMSService>().GetPreviousdDateSMSDetailsByTruckNo(TruckNo);
            return _smsRegistrationObj.SMSReg_Cust_PhoneNumber == null ? Resources.Messages.NoRecordFound : _smsRegistrationObj.SMSReg_Cust_PhoneNumber.ToString();
           
        }

        else
        {

            return Resources.Messages.NoRecordFound;
        }

    }
         

	protected string GetSMSID(string TruckNo)
	{
		if (!string.IsNullOrEmpty(TruckNo))
		{
			SMSRegistrationDTO _smsRegistrationObj = new SMSRegistrationDTO();
			_smsRegistrationObj = ESalesUnityContainer.Container.Resolve<ISMSService>().GetPreviousdDateSMSDetailsByTruckNo(TruckNo);
            if (_smsRegistrationObj.SMSReg_Id == 0)
                grdViewTruck.Columns[9].Visible = false;
            else
                grdViewTruck.Columns[9].Visible = true;
            
			return _smsRegistrationObj.SMSReg_Id == 0 ? Resources.Messages.NoRecordFound : _smsRegistrationObj.SMSReg_Id.ToString();
		}

		else
		{
            grdViewTruck.Columns[9].Visible = false;
			return Resources.Messages.NoRecordFound;
		}
	}

    protected string GetSMSDate(string TruckNo)
    {
        if (!string.IsNullOrEmpty(TruckNo))
        {
            SMSRegistrationDTO _smsRegistrationObj = new SMSRegistrationDTO();
            _smsRegistrationObj = ESalesUnityContainer.Container.Resolve<ISMSService>().GetPreviousdDateSMSDetailsByTruckNo(TruckNo);
            if (_smsRegistrationObj.SMSReg_Id == 0)
                grdViewTruck.Columns[9].Visible = false;
            else
                grdViewTruck.Columns[9].Visible = true;

            return _smsRegistrationObj.SMSReg_Id == 0 ? Resources.Messages.NoRecordFound : DateTime.Now.Date.ToString("dd-MMM-yyyy");
        }
        else
        {
            grdViewTruck.Columns[9].Visible = false;
            return Resources.Messages.NoRecordFound;
        }
    }

   
}