// -----------------------------------------------------------------------
// <copyright file="PaymentCollection.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TCESS.ESales.DataTransferObjects.GhatoCollection
{
    #region Using directives

    using System;
    using TCESS.ESales.DataTransferObjects.Masters;
    using System.Collections.Generic;
    using TCESS.ESales.DataTransferObjects;

    #endregion

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Serializable]
    public class PaymentCollectionDTO : BaseDTO
    {
        public int PC_Id { get; set; }
        public int PC_CustId { get; set; }
        public string PC_ReceiptNo { get; set; }
        public DateTime PC_ReceiptDate { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerTradeName { get; set; }
        public string CustomerDistrict { get; set; }
        public int PC_PaymentMode { get; set; }
        public string PaymentModeName { get; set; }
        public string PC_InstrumentNo { get; set; }
        public decimal PC_Amount { get; set; }
        public Nullable<int> PC_BankDrawn { get; set; }
        public string BankName { get; set; }
        public string PC_BankBranch { get; set; }
        public string PC_BankIFSCCode { get; set; }
        public Nullable<DateTime> PC_InstrumentDate { get; set; }
        public string PC_Remark { get; set; }
        public string PC_PayerName { get; set; }
        public string PC_MobileNumber { get; set; }
        public Nullable<int> PC_InstrumentStatus { get; set; }
        public Nullable<DateTime> PC_DateOfCredit { get; set; }
        public Nullable<decimal> PC_PreviousAmount { get; set; }
        public Nullable<DateTime> PC_InstrumentRealizationDate { get; set; }
        public Nullable<int> PC_Status { get; set; }
        public Nullable<int> PC_OldReceiptId { get; set; }
        public Nullable<int> PC_NewReceiptId { get; set; }
        public int PC_ReprintCount { get; set; }
        public Nullable<DateTime> PC_LastPrintDate { get; set; }
        public Nullable<int> PC_CreatedBy { get; set; }
        public DateTime PC_CreatedDate { get; set; }
        public Nullable<int> PC_LastUpdatedBy { get; set; }
        public Nullable<DateTime> PC_LastUpdateDate { get; set; }
        public bool PC_IsDeleted { get; set; }

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
                    CustomerDistrict = value.Cust_District_Name;
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