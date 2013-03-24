using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class SMSPaymentRegistrationDTO:BaseDTO
    {
       
        public int SMSPay_Id
        {
            get;
            set;
        }

        public int SMSPay_CustId
        {
            get;
            set;
        }
       
        public string SMSPay_CustCode
        {
            get;
            set;
        }

        public decimal SMSPay_Amount
        {
            get;
            set;
        }

        public System.DateTime SMSPay_Date
        {
            get;
            set;
        }

        public Nullable<int> SMSPay_Payment_Id
        {
            get;
            set;
        }
        public Nullable<bool> SMSPay_Status
        {
            get;
            set;
        }

        public string SMSPay_RejectionReason
        {
            get;
            set;
        }

        public int SMSPay_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> SMSPay_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> SMSPay_LastUpdatedDate
        {
            get;
            set;
        }

        public bool SMSPay_IsDeleted
        {
            get;
            set;
        }

        public string SMSPay_Cust_Code
        {
            get;
            set;
        }

        public string SMSPay_Cust_TradeName
        {
            get;
            set;
        }

        public string SMSPay_Cust_UnitName
        {
            get;
            set;
        }

        public string SMSPay_Cust_UnitAddress
        {
            get;
            set;
        }

        public string SMSPay_Cust_District_Name
        {
            get;
            set;
        }

        public string SMSPay_Cust_State_Name
        {
            get;
            set;
        }

        public string SMSPay_Cust_PhoneNumber
        {
            get;
            set;
        }

        public string SMSPay_CustomerBusinessType
        {
            get;
            set;
        }
        public string SMSPay_CustomerName 
        { 
            get; 
            set; 
        }
        #region Navigation Properties
        private CustomerDTO _customer;

        public CustomerDTO customer
        {
            get { return _customer; }
            set
            {
                if (!ReferenceEquals(_customer, value))
                {
                    var previousValue = _customer;
                    _customer = null;
                    SMSPay_Cust_Code = value.Cust_Code;
                    SMSPay_Cust_UnitName = value.Cust_FirmName;
                    SMSPay_Cust_UnitAddress = value.Cust_UnitAddress;
                    SMSPay_Cust_District_Name = value.Cust_District_Name;
                    SMSPay_Cust_State_Name = value.Cust_State_Name;
                    SMSPay_Cust_TradeName = value.Cust_TradeName;
                    SMSPay_Cust_PhoneNumber = value.Cust_MobileNo;
                    SMSPay_CustomerBusinessType = value.Cust_Business_Name;
                    SMSPay_CustomerName = value.Cust_OwnerName;
                }
            }
        }

     
        #endregion

    }
}
