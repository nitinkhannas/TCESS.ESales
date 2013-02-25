// -----------------------------------------------------------------------
// <copyright file="MasterList.cs" company="Q3 Technologies">
// Copyright 2010 - 2011 - www.q3tech.com. All rights reserved.
// This class is created to provide common methods in the application.
// </copyright>
// -----------------------------------------------------------------------

#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.BusinessLayer.Interfaces.Masters;
using TCESS.ESales.BusinessLayer.Interfaces.Users;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.DataTransferObjects.Masters;

#endregion

namespace TCESS.ESales.CommonLayer.CommonLibrary
{
    public class MasterList
    {
        /// <summary>
        /// Get list of all States.
        /// </summary>
        /// <returns>Returns States List</returns>
        public static void GetStateList(DropDownList ddlStates)
        {
            ddlStates.DataSource = ESalesUnityContainer.Container.Resolve<ILocationService>().GetStateList();
            ddlStates.DataBind();
            ddlStates.Items.Insert(0, new ListItem(Labels_en.SELECTSTATE, "0"));
        }

        /// <summary>
        /// Get list of all District by state id.
        /// </summary>
        /// <returns>Returns District List</returns>
        public static void GetDistrictListByStateId(DropDownList ddlDistricts, int stateId)
        {
            IList<DistrictDTO> listDistrict = ESalesUnityContainer.Container.Resolve<ILocationService>()
                .GetDistrictListByStateId(stateId);

            //Clear dropdown control before initializing any new value
            ddlDistricts.Items.Clear();

            if (listDistrict.Count > 0)
            {
                ddlDistricts.DataSource = listDistrict;
                ddlDistricts.DataBind();
            }
            
            ddlDistricts.Items.Insert(0, new ListItem(Labels_en.SELECTDISTRICT, "0"));
        }

        /// <summary>
        /// Get Truck wheels
        /// </summary>
        /// <param name="ddlTruckWheelers">Dropdownlist control to initialize truck wheels value</param>
        public static void GetTruckWheels(DropDownList ddlWheeler)
        {
            ddlWheeler.DataSource = ESalesUnityContainer.Container.Resolve<ITruckService>().GetTruckWheels();
            ddlWheeler.DataBind();
            ddlWheeler.Items.Insert(0, new ListItem(Labels_en.SELECTTRUCKWHEELS, "0"));
        }

        /// <summary>
        /// Get Truck make list
        /// </summary>
        /// <param name="ddlTruckMake">Dropdownlist control to initialize truck make value</param>
        public static void GetTruckMakelist(DropDownList ddlTruckMake)
        {
            ddlTruckMake.DataSource = ESalesUnityContainer.Container.Resolve<ITruckMakeService>().GetTruckMakelist();
            ddlTruckMake.DataBind();
            ddlTruckMake.Items.Insert(0, new ListItem(Labels_en.SELECTTRUCKMAKE, "0"));
        }

        /// <summary>
        /// Get truck carry capacity and populate drop down controls
        /// </summary>
        /// <param name="ddlTruckMake">Dropdownlist control to initialize truck carry capacity</param>
        public static void GetTruckCarryCapacity(DropDownList ddlCarryCapacity)
        {
            ddlCarryCapacity.DataSource = ESalesUnityContainer.Container.Resolve<ITruckService>().GetTruckCarryCapacity();
            ddlCarryCapacity.DataBind();
            ddlCarryCapacity.Items.Insert(0, new ListItem(Labels_en.SELECTTRUCKCARRYCAPACITY, "0"));
        }

        /// <summary>
        /// Get active DCA list and populate drop down controls
        /// </summary>
        /// <param name="ddlAgentName">Dropdownlist control to initialize agent name</param>
        public static void GetAgentListInDropDown(DropDownList ddlAgentName)
        {
            ddlAgentName.DataSource = GetAgentList();
            ddlAgentName.DataBind();
            ddlAgentName.Items.Insert(0, new ListItem(Labels_en.SELECTAGENT, "0"));
        }

        /// <summary>
        /// Get active DCA list and populate drop down controls
        /// </summary>
        /// <param name="ddlAgentName">Dropdownlist control to initialize agent name</param>
        public static void GetAgentListInDropDownForReports(DropDownList ddlAgentName)
        {
            ddlAgentName.DataSource = GetAgentList();
            ddlAgentName.DataBind();
            ddlAgentName.Items.Insert(0, new ListItem(Labels_en.ALLDCADATA, "0"));
        }

        public static void GetAllotedQuantityInDropDown(DropDownList ddlQuantity)
        {
            ddlQuantity.DataSource = GetAllotedQuantityList();
            ddlQuantity.DataBind();
            ddlQuantity.Items.Insert(0, new ListItem(Labels_en.SELECTQUANTITY, "0"));            
        }

        /// <summary>
        /// Generates the unique transaction id.
        /// </summary>
        /// <returns></returns>
        public static Int64 GetUniqueTransactionId()
        {
            string day = Convert.ToString(String.Format("{0:dd}", DateTime.Now));
            string months = String.Format("{0:MM}", DateTime.Now);
            string hours = String.Format("{0:HH}", DateTime.Now);
            string minutes = String.Format("{0:mm}", DateTime.Now);
            string seconds = String.Format("{0:ss}", DateTime.Now);
            string miliseconds = String.Format("{0:fff}", DateTime.Now);
            return Convert.ToInt64(day + months + hours + minutes + seconds + miliseconds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grdDocuments"></param>
        /// <param name="checkboxId"></param>
        /// <returns></returns>
        public static bool CheckIfNoDocumentSelected(GridView grdDocuments, string checkboxId)
        {
            bool isChecked = false;

            foreach (GridViewRow row in grdDocuments.Rows)
            {
                Int32 rowIndex = row.RowIndex;
                CheckBox chkScanCompleted = ((CheckBox)grdDocuments.Rows[rowIndex].FindControl(checkboxId));

                if (chkScanCompleted.Checked == true)
                {
                    isChecked = true;
                }
            }

            //return the value
            return isChecked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grdDocuments"></param>
        /// <param name="checkboxId"></param>
        /// <returns></returns>
        public static bool CheckMandatoryDocumentList(GridView grdDocuments, string mandatoryCheckId, string checkboxId)
        {
            bool isChecked = true;

            foreach (GridViewRow row in grdDocuments.Rows)
            {
                Int32 rowIndex = row.RowIndex;
                CheckBox chkMandatoryDocs = ((CheckBox)grdDocuments.Rows[rowIndex].FindControl(mandatoryCheckId));

                if (chkMandatoryDocs.Checked == true)
                {
                    CheckBox chkScanCompleted = ((CheckBox)grdDocuments.Rows[rowIndex].FindControl(checkboxId));

                    if (chkScanCompleted.Checked == false)
                    {
                        isChecked = false;
                    }
                }
            }
            return isChecked;
        }

        public static string CheckIfFolderExists(string folderName, string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (!Directory.Exists(folderName))
            {
                folderName = folderPath + folderName;
                Directory.CreateDirectory(folderName);
            }
            return folderName;
        }

        public static BookingDTO GetBookingDetailByBookingId(int bookingId, bool isMoneyReceiptIssued)
        {
            BookingDTO bookingDetail = new BookingDTO();
            bookingDetail = ESalesUnityContainer.Container.Resolve<IBookingService>().GetBookingDetailByBookingId(bookingId, 
                isMoneyReceiptIssued);
            return bookingDetail;
        }

        public static UserAgentMappingDTO GetAgentByUserId(int userId)
        {
            UserAgentMappingDTO agentMapDetails = new UserAgentMappingDTO();
            agentMapDetails = ESalesUnityContainer.Container.Resolve<IUserAgentService>().GetAgentByUserId(userId);
            return agentMapDetails;
        }

        public static MoneyReceiptDTO GetMoneyReceiptById(int moneyReceiptId, int bookingId)
        {
            MoneyReceiptDTO moneyReceiptDetails = new MoneyReceiptDTO();
            moneyReceiptDetails = ESalesUnityContainer.Container.Resolve<IMoneyReceiptService>()
                .GetMoneyReceiptById(moneyReceiptId, bookingId);
            return moneyReceiptDetails;
        }

        
        /// <summary>
        /// Get all active business types from database
        /// </summary>
        /// <returns>List of active business types</returns>
        public static IList<BusinessTypeDTO> GetBusinessTypeList()
        {
            IList<BusinessTypeDTO> listBusinessType = new List<BusinessTypeDTO>();
            listBusinessType = ESalesUnityContainer.Container.Resolve<IMasterService>().GetBusinessTypeList();
            return listBusinessType;
        }

        /// <summary>
        /// Get all active business types from database
        /// </summary>
        /// <returns>List of active business types</returns>
        public static IList<TruckRegTypeDTO> GetTruckregTypeList()
        {
            IList<TruckRegTypeDTO> listBusinessType = new List<TruckRegTypeDTO>();
            listBusinessType = ESalesUnityContainer.Container.Resolve<ICustomerMastersService>().GetTruckRegTypeList();
            return listBusinessType;
        }

        /// <summary>
        /// Get all active Ownership Status from database
        /// </summary>
        /// <returns>List of active ownership status</returns>
        public static IList<OwnershipStatusDTO> GetOwnershipStatusList()
        {
            IList<OwnershipStatusDTO> listOwnershipStatus = new List<OwnershipStatusDTO>();
            listOwnershipStatus = ESalesUnityContainer.Container.Resolve<IMasterService>().GetOwnershipStatusList();
            return listOwnershipStatus;
        }

        /// <summary>
        /// Get all active AME blocks from database
        /// </summary>
        /// <returns>List of active AME blocks</returns>
        public static IList<AmeBlockDTO> GetAmeBlockList()
        {
            IList<AmeBlockDTO> listAmeBlock = new List<AmeBlockDTO>();
            listAmeBlock = ESalesUnityContainer.Container.Resolve<IAmeBlockService>().GetAmeBlockList();
            return listAmeBlock;
        }

        /// <summary>
        /// Get list of active agents
        /// </summary>
        /// <returns></returns>
        public static IList<AgentDTO> GetAgentList()
        {
            IList<AgentDTO> listAgent = new List<AgentDTO>();
            listAgent = ESalesUnityContainer.Container.Resolve<IAgentService>().GetAgentList();
            return listAgent;
        }

        public static IList<AllotedQuantityDTO> GetAllotedQuantityList()
        {
            IList<AllotedQuantityDTO> listAllotedQty = new List<AllotedQuantityDTO>();
            listAllotedQty = ESalesUnityContainer.Container.Resolve<ICustomerMastersService>().GetAllotedQuantityList();
            return listAllotedQty;
        }

        /// <summary>
        /// Get customer details by customer id
        /// </summary>
        /// <param name="customerId">Int32: customer id</param>
        /// <returns>returns customer details if exists, else blank datatype</returns>
        public static CustomerDTO GetCustomerDetailsById(int customerId)
        {
            CustomerDTO customerDetails = new CustomerDTO();
            customerDetails = ESalesUnityContainer.Container.Resolve<ICustomerService>().GetCustomerDetailsById(customerId);
            return customerDetails;
        }

        /// <summary>
        /// Get Truck Make details by truck make id
        /// </summary>
        /// <param name="truckMakeId">Int32: truck make id</param>
        /// <returns>returns truck make details if exists, else blank datatype</returns>
        public static TruckMakeDTO GetTruckMakeById(int truckMakeId)
        {
            TruckMakeDTO truckMakeDetails = new TruckMakeDTO();
            truckMakeDetails = ESalesUnityContainer.Container.Resolve<ITruckMakeService>().GetTruckMakeById(truckMakeId);
            return truckMakeDetails;
        }

		/// <summary>
		/// returns the mac address of the first operation nic found.
		/// </summary>
		/// <returns></returns>
		public static string GetMacAddress()
		{
			string macAddresses = string.Empty;

			foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
			{
				if (nic.OperationalStatus == OperationalStatus.Up)
				{
					macAddresses += nic.GetPhysicalAddress().ToString();
					break;
				}
			}
			return macAddresses;
		}

        /// <summary>
        /// Get list of active payment modes
        /// </summary>
        /// <param name="isGhatoCollection">Get Payment mode for Ghato Collection</param>
        /// <returns>returns customer details if exists, else blank datatype</returns>
        public static IList<PaymentModeDTO> GetListOfPaymentMode(bool isGhatoCollection)
        {
            IList<PaymentModeDTO> lstPaymentMode = new List<PaymentModeDTO>();
            lstPaymentMode = ESalesUnityContainer.Container.Resolve<IMasterService>().GetListOfPaymentMode(isGhatoCollection);
            return lstPaymentMode;
        }

        /// <summary>
        /// Get list of active payment modes
        /// </summary>
        /// <param name="isGhatoCollection">Get Payment mode for Ghato Collection</param>
        /// <returns>returns customer details if exists, else blank datatype</returns>
        public static void FillDropdownForBanks(DropDownList ddlBankDrawn)
        {
            ddlBankDrawn.DataSource = ESalesUnityContainer.Container.Resolve<IMasterService>().GetBankDetails();
            ddlBankDrawn.DataBind();
            ddlBankDrawn.Items.Insert(0, new ListItem(Labels_en.SELECTBANK, "0"));            
        }

        public static void FillDropdownForRejectionReasons(DropDownList ddlRejectionReason)
        {
            ddlRejectionReason.DataSource = ESalesUnityContainer.Container.Resolve<IMasterService>().GetRejectionReasons();
            ddlRejectionReason.DataBind();
            ddlRejectionReason.Items.Insert(0, new ListItem(Labels_en.SELECTREJECTIONREASON, "0"));
        }

        public static string GetMonthName(int month)
        {
            if (month == 1)
            {
                return "January";
            }
            else if (month == 2)
            {
                return "February";
            }
            else if (month == 3)
            {
                return "March";
            }
            else if (month == 4)
            {
                return "April";
            }
            else if (month == 5)
            {
                return "May";
            }
            else if (month == 6)
            {
                return "June";
            }
            else if (month == 7)
            {
                return "July";
            }
            else if (month == 8)
            {
                return "August";
            }
            else if (month == 9)
            {
                return "September";
            }
            else if (month == 10)
            {
                return "October";
            }
            else if (month == 11)
            {
                return "November";
            }
            else if (month == 12)
            {
                return "December";
            }
            return "0";
        }

        public static IList<LiftingIntervalDTO> GetLiftingIntervalList()
        {
            IList<LiftingIntervalDTO> lstLiftingInterval = new List<LiftingIntervalDTO>();
            lstLiftingInterval = ESalesUnityContainer.Container.Resolve<ICustomerMastersService>().GetLiftingIntervalList();
            return lstLiftingInterval;
        }
    }
}