#region Using directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.BusinessLayer.Interfaces.GhatoCollection;

#endregion

namespace TCESS.ESales.CommonLayer.CommonLibrary
{
    public class SmsUtility
    {
        public static void SendSMS(string phoneNumber, string message)
        {
            string url = ConfigurationManager.AppSettings["SendSMS"];
            //WebRequest wr = WebRequest.Create("http://www.nimbusit.in/binapi/pushsms.php?usr=667968&pwd=nimbusit&sndr=Test&ph=" + phoneNumber + "&text=" + message + "&rpt=1&type=1");
            string sendUrl = url.FormatWith(phoneNumber, message);
            WebRequest wr = WebRequest.Create(sendUrl);
            wr.Timeout = 1000;
            try
            {
                HttpWebResponse response = (HttpWebResponse)wr.GetResponse();
            }
            catch (Exception ex)
            {
            }
        }
        public static void SendSMSForBookings(string phoneNumber, string message)
        {
            string url = ConfigurationManager.AppSettings["SendSMS"];
            string sendUrl = url.FormatWith(phoneNumber, message);
            WebRequest wr = WebRequest.Create(sendUrl);
            wr.Timeout = 1500;
            try
            {
                HttpWebResponse response = (HttpWebResponse)wr.GetResponse();
            }
            catch (Exception ex)
            {
            }
        }

        public static string RespondSms(string phoneNumber, string message, string messageTruck)
        {
            string custPhoneNumber = phoneNumber.Substring(2, phoneNumber.Length - 2);
            IList<CustomerDTO> lstCustomer = ESalesUnityContainer.Container.Resolve<ICustomerService>()
                .GetCustomerDetailsByMobileNumber(custPhoneNumber);

            if (lstCustomer.Count > 0)
            {
                foreach (CustomerDTO customer in lstCustomer)
                {
                    if (customer.Cust_Code == message)
                    {
                        string bookingId = ESalesUnityContainer.Container.Resolve<ISMSService>()
                            .SaveAndUpdateSMSDetails(InitializeSMSRegDetails(customer.Cust_Id, message, messageTruck)).ToString();
                        return Messages.BookingRequestAccepted;
                    }
                    else
                    {

                    }
                }
                return Messages.PhoneNoNotFound;

            }
            else
            {
                return Messages.PhoneNoNotFound;
            }
        }

        private static SMSRegistrationDTO InitializeSMSRegDetails(int custId, string message, string truckNumber)
        {
            SMSRegistrationDTO smsRegDetails = new SMSRegistrationDTO();
            smsRegDetails.SMSReg_CustId = custId;
            smsRegDetails.SMSReg_Msg = message;
            smsRegDetails.SMSReg_TruckNo = truckNumber;
            smsRegDetails.SMSReg_BookingStatus = false;
            smsRegDetails.SMSReg_CreatedDate = DateTime.Now;
            smsRegDetails.SMSReg_LastUpdatedDate = DateTime.Now;
            smsRegDetails.SMSReg_Date = DateTime.Now.Date;
            return smsRegDetails;
        }
        public static BookingDTO UpdateGateInformation(int gateLocation, int bookingId)
        {
            BookingDTO bookingDetails = ESalesUnityContainer.Container.Resolve<IBookingService>().GetBookingDetailByBookingId(bookingId, true);

            if (bookingDetails.Booking_Id > 0 && gateLocation == 1)
            {
                bookingDetails.Booking_TruckIn = true;
                bookingDetails.Booking_TruckInTime = DateTime.Now;
                ESalesUnityContainer.Container.Resolve<IBookingService>().SaveAndUpdateBookingDetail(bookingDetails);
                return ESalesUnityContainer.Container.Resolve<IBookingService>().GetBookingDetailByBookingId(bookingId, true);
            }
            else if (bookingDetails.Booking_Id > 0 && gateLocation == 2)
            {
                bookingDetails.Booking_TruckMatLifted = true;
                bookingDetails.Booking_TruckMatLiftedTime = DateTime.Now;
                ESalesUnityContainer.Container.Resolve<IBookingService>().SaveAndUpdateBookingDetail(bookingDetails);
                return ESalesUnityContainer.Container.Resolve<IBookingService>().GetBookingDetailByBookingId(bookingId, true);

            }
            else
            {
                return new BookingDTO();
            }


        }
        public static int GetTruckCountForDateBarcode(DateTime currentDate, int truckStatus)
        {
            if (truckStatus == 1)
            {
                return ESalesUnityContainer.Container.Resolve<IBookingService>().GetTruckCountForDateBarcode(currentDate, truckStatus);
            }
            else if (truckStatus == 2)
            {
                return ESalesUnityContainer.Container.Resolve<IBookingService>().GetTruckCountForDateBarcode(currentDate, truckStatus);

            }
            else
            {
                return 0;
            }
        }
        public static string UpdateDCAPercentage()
        {
            IList<DcaMaterialAllocationDTO> listAllMaterial = ESalesUnityContainer.Container.Resolve<IDcaMaterialAllocationService>()
                        .GetAllMaterialAllocationDetails(0, DateTime.Now.Date);
            if (listAllMaterial.Count == 0)
            {
                UpdatePreviousDayaActualPercenatge();
                try
                {
                    IList<DcaMaterialAllocationDTO> listMaterialAllocations = new List<DcaMaterialAllocationDTO>();

                    IList<MaterialTypeDTO> lstMaterialTypeDTO = ESalesUnityContainer.Container.Resolve<IMaterialTypeService>().GetMaterialTypeList(true);
                    foreach (MaterialTypeDTO Material in lstMaterialTypeDTO)
                    {
                        IList<AgentDTO> lstAgentDTO = ESalesUnityContainer.Container.Resolve<IAgentService>().GetAgentList();

                        if (lstAgentDTO.Count > 0)
                        {
                            foreach (AgentDTO item in lstAgentDTO)
                            {

                                DcaMaterialAllocationDTO DCAMaterialAllocation = new DcaMaterialAllocationDTO();

                                DCAMaterialAllocation.DCAMA_Date = System.DateTime.Now.Date.AddDays(0);
                                DCAMaterialAllocation.DCAMA_Agent_Id = item.Agent_Id;
                                DCAMaterialAllocation.DCAMA_MaterialType_Id = Material.MaterialType_Id;

                                IList<AgentMaterialPercentageDTO> lstAgentMaterialPercentageDTO = ESalesUnityContainer.Container
                                  .Resolve<IAgentMaterialPercentageService>().GetAgentMaterialPercentByAgentId(item.Agent_Id);

                                IList<DcaMaterialAllocationDTO> listMaterial = ESalesUnityContainer.Container.Resolve<IDcaMaterialAllocationService>()
                                .GetAllMaterialAllocationDetails(Material.MaterialType_Id, DateTime.Now.Date.AddDays(-1));

                                DCAMaterialAllocation.DCAMA_TodayPercentage = (from F in lstAgentMaterialPercentageDTO
                                                                               where F.AMP_MaterialType_Id == Material.MaterialType_Id
                                                                               select F.AMP_Percentage).FirstOrDefault()
                                                                                +
                                                                                (from F in listMaterial
                                                                                 where F.DCAMA_Agent_Id == item.Agent_Id
                                                                                 select F.DCAMA_ActualVariance).FirstOrDefault()
                                                                                ;
                                DCAMaterialAllocation.DCAMA_CreatedDate = DateTime.Now;
                                DCAMaterialAllocation.DCAMA_LastUpdatedDate = DateTime.Now;
                                listMaterialAllocations.Add(DCAMaterialAllocation);
                            }
                        }
                    }

                    ESalesUnityContainer.Container.Resolve<IDcaMaterialAllocationService>()
                                        .SaveAndUpdateDCAMaterialDetails(listMaterialAllocations);
                }

                catch (Exception ex)
                {

                }
                return "Updation Compleated";
            }
            else
            {
                return "Updation Not Compleated";
            }

        }
        public static void UpdatePreviousDayaActualPercenatge()
        {
            IList<MaterialTypeDTO> lstMaterialTypeDTO = ESalesUnityContainer.Container.Resolve<IMaterialTypeService>().GetMaterialTypeList(true);
            foreach (MaterialTypeDTO Material in lstMaterialTypeDTO)
            {
                IList<DcaMaterialAllocationDTO> listMaterial = ESalesUnityContainer.Container.Resolve<IDcaMaterialAllocationService>()
                            .GetAllMaterialAllocationDetails(Material.MaterialType_Id, DateTime.Now.Date.AddDays(-1));
                foreach (var item in listMaterial)
                {
                    decimal todaysTotalQuantity, todaysDCAQty, previousTotalQuantity, previousDCAQty, dcaQty, todaysQty, actualPercentage = 0;
                    IList<AgentMaterialPercentageDTO> lstMaterialPercentage = ESalesUnityContainer.Container.Resolve<IAgentMaterialPercentageService>().GetAgentMaterialPercentByMaterialTypeId(Material.MaterialType_Id);
                    DateTime fromDate = Convert.ToDateTime(lstMaterialPercentage[0].AMP_LastUpdatedDate);
                    IList<SettlementOfAccountsDTO> lstBookingCompleated = ESalesUnityContainer.Container.Resolve<ISettlementOfAccountsService>()
                .GetSettlementDetailsForDay(fromDate);
                    if (lstBookingCompleated.Count > 0)
                    {
                        todaysDCAQty = (from F in lstBookingCompleated
                                        where F.booking.Booking_Agent_Id == item.DCAMA_Agent_Id
                                            && F.booking.Booking_MaterialType_Id == Material.MaterialType_Id
                                        select F.Account_Quantity).Sum();



                        todaysTotalQuantity = (from F in lstBookingCompleated
                                               where F.booking.Booking_MaterialType_Id == Material.MaterialType_Id
                                               select F.Account_Quantity).Sum();
                        dcaQty = Convert.ToDecimal((todaysDCAQty));
                        todaysQty = Convert.ToDecimal((todaysTotalQuantity));

                        IList<AgentMaterialPercentageDTO> lstAgentPercentage = ESalesUnityContainer.Container.Resolve<IAgentMaterialPercentageService>().GetAgentMaterialPercentByAgentId(item.DCAMA_Agent_Id);

                        if (lstAgentPercentage.Count > 0)
                        {
                            actualPercentage = (from F in lstAgentPercentage
                                                where F.AMP_MaterialType_Id == Material.MaterialType_Id
                                                select F.AMP_Percentage).FirstOrDefault();
                        }



                        item.DCAMA_ActualPercentage = dcaQty;
                        if (todaysQty > 0 && dcaQty > 0)
                        {
                            item.DCAMA_ActualVariance = actualPercentage - ((dcaQty / todaysQty) * 100);
                        }
                        item.DCAMA_LastUpdatedDate = DateTime.Now;
                    }
                }
                ESalesUnityContainer.Container.Resolve<IDcaMaterialAllocationService>()
                                    .SaveAndUpdateDCAMaterialDetails(listMaterial);
            }
        }

        public static string GetTodayReport()
        {
            //TimeSpan scheduledTimeSpan = new TimeSpan(0, 1, 0);
            //System.Threading.TimerCallback _timerDelegate = rpt;
            //int timeBetweenCalls = (int)new TimeSpan(0, 1, 0).TotalMilliseconds;

            //System.Threading.Timer t = new System.Threading.Timer(_timerDelegate, null, 0, timeBetweenCalls);
            ////object obj = new object();
            //rpt(obj);
            string reportData = "";

            IList<object> lstBookingStatusData = ESalesUnityContainer.Container.Resolve<IReportService>()
                .GetDailyBookingStatusReportForBarChart(1, Convert.ToDateTime(DateTime.Now), Convert.ToDateTime(DateTime.Now)).ToList();

            if (lstBookingStatusData.Count > 0)
            {

                reportData = " SMS Accepted:" + lstBookingStatusData[0] +
                               " Bookings:" + lstBookingStatusData[1] +
                               " Loading Advice Issue:" + lstBookingStatusData[2] +
                               " Truck Out:" + lstBookingStatusData[3] +
                               " Opening Pendings:" + lstBookingStatusData[4] +
                               " Closing Pendings:" + lstBookingStatusData[5];

                IList<SmsExecutiveListDTO> lstSmsExecutive = ESalesUnityContainer.Container.Resolve<IReportService>().GetSMSSendingList().ToList();
                if (lstSmsExecutive.Count > 0)
                {
                    foreach (SmsExecutiveListDTO item in lstSmsExecutive)
                    {
                        SendSMS(item.SmsExecutiveList_PhoneNumber, reportData.Trim());
                    }

                }
            }

            return "SMS Sent";
        }
        public static IList<LapsedBookingDTO> GetPendingMessages()
        {
            IList<LapsedBookingDTO> lstLapsedBookingDTO = new List<LapsedBookingDTO>();
            IList<SMSRegistrationDTO> lstSMSRegistration = ESalesUnityContainer.Container.Resolve<IReportService>()
                .GetPendingSMSList().ToList();
            foreach (SMSRegistrationDTO item in lstSMSRegistration)
            {
                LapsedBookingDTO lb = new LapsedBookingDTO();
                lb.SMS_Order_No = item.SMSReg_Id;
                lb.Truck_No = item.SMSReg_TruckNo;
                lb.Customer_Code = item.SMSReg_Cust_Code;
                lb.Customer_Name = item.SMSReg_Cust_UnitName;
                lb.Mobile_No = item.SMSReg_Cust_PhoneNumber;
                lb.Distt = item.SMSReg_Cust_District_Name;
                lstLapsedBookingDTO.Add(lb);
            }
            return lstLapsedBookingDTO;
        }

        public static string AcceptPayment(string phoneNumber, string customerCode, Decimal amount)
        {
            string custPhoneNumber = phoneNumber.Substring(2, phoneNumber.Length - 2);
            IList<CustomerDTO> lstCustomer = ESalesUnityContainer.Container.Resolve<ICustomerService>()
                .GetCustomerDetailsByMobileNumber(custPhoneNumber);

            if (lstCustomer.Count > 0)
            {
                foreach (CustomerDTO customer in lstCustomer)
                {
                    if (customer.Cust_Code == customerCode)
                    {
                        string paymentId = ESalesUnityContainer.Container.Resolve<IPaymentService>()
                            .SaveAndUpdateSMSPaymentDetails(InitializeSMSPaymentDetails(customer.Cust_Id, customerCode, amount)).ToString();
                        string smsMessage = ConfigurationManager.AppSettings["PaymentSMS"]; ;
                        return smsMessage.FormatWith(paymentId);
                    }
                    else
                    {

                    }
                }
                return Messages.PhoneNoNotFound;

            }
            else
            {
                return Messages.PhoneNoNotFound;
            }
        }

        private static SMSPaymentRegistrationDTO InitializeSMSPaymentDetails(int custId, string customerCode, Decimal amount)
        {
            SMSPaymentRegistrationDTO smsPayDetails = new SMSPaymentRegistrationDTO();
            smsPayDetails.SMSPay_CustId = custId;
            smsPayDetails.SMSPay_Cust_Code = customerCode;
            smsPayDetails.SMSPay_Amount = amount;
            smsPayDetails.SMSPay_Status= false;
            smsPayDetails.SMSPay_CreatedDate = DateTime.Now;
            smsPayDetails.SMSPay_LastUpdatedDate = DateTime.Now;
            smsPayDetails.SMSPay_Date = DateTime.Now.Date;
            return smsPayDetails;
        }

    }
}