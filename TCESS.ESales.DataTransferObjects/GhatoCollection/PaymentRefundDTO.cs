using System;

namespace TCESS.ESales.DataTransferObjects.GhatoCollection
{
    public class PaymentRefundDTO : BaseDTO
    {
        public int PR_ID { get; set; }
        public int PR_CustID { get; set; }
        public string PR_VadidatedID { get; set; }
        public decimal PR_Amount { get; set; }
        public int PR_PaymentMode { get; set; }
        public string PR_InstrumentNo { get; set; }
        public Nullable<int> PR_BankDrawn { get; set; }
        public string PR_Remarks { get; set; }
        public string PR_BankBranch { get; set; }
        public Nullable<DateTime> PR_InstrumentDate { get; set; }
        public string PR_ReceiverName { get; set; }
        public string PR_MobileNumber { get; set; }
        public int PR_CreatedBy { get; set; }
        public Nullable<System.DateTime> PR_CreatedDate { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerTradeName { get; set; }
        public string PaymentModeName { get; set; }
        public string PR_BankIFSCCode { get; set; }

        #region Navigation Properties

        private CustomerDTO _customer;
        private PaymentModeDTO _paymentmode;

        public virtual CustomerDTO customer
        {
            get { return _customer; }
            set
            {
                if (!ReferenceEquals(_customer, value))
                {
                    var previousValue = _customer;
                    _customer = null;
                    CustomerCode = value.Cust_Code;
                    CustomerTradeName = value.Cust_TradeName;
                    CustomerName = value.Cust_OwnerName;
                }
            }
        }

        public virtual PaymentModeDTO paymentmode
        {
            get { return _paymentmode; }
            set
            {
                if (!ReferenceEquals(_paymentmode, value))
                {
                    var previousValue = _paymentmode;
                    _paymentmode = null;
                    PaymentModeName = value.Paymentmode_Name;
                }
            }
        }

        #endregion
    }
}