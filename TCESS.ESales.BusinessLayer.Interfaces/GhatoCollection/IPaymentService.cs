// -----------------------------------------------------------------------
// <copyright file="IPaymentCollectionService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TCESS.ESales.BusinessLayer.Interfaces.GhatoCollection
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using TCESS.ESales.DataTransferObjects.GhatoCollection;
    using TCESS.ESales.DataTransferObjects;

    #endregion

    public interface IPaymentService
    {
        int SaveOrUpdateCollection(PaymentCollectionDTO paymentCollection);
        IList<PaymentCollectionDTO> GetCollectionDetails(string searchValue, Nullable<bool> isNumeric, Nullable<int> userId);
        IList<PaymentCollectionDTO> GetChequeDetailsForActivation(int paymentModeId, string chequeNumber);
        PaymentCollectionDTO GetCollectionDetailsById(int collectionId);
        PaymentCollectionDTO GetCollectionDetailsByReceiptNo(string receiptNo);
        void SendPaymentToHeadCashier(BatchTransferDTO batchTransferDTO);
        IList<object> GetCollectionHeaderForTransit(bool isApprovePayments, Nullable<int> userId);
        IList<BatchTransferDTO> GetBatchDetails(Nullable<int> userId,
            Nullable<int> paymentMode);
        IList<BatchTransferDTO> GetBatchDetailsForCollectionScreen(Nullable<int> userId,
            Nullable<int> paymentMode);
        IList<PaymentCollectionDTO> GetCollectionDetailsFromBatchId(int batchId);
        BatchTransferDTO GetBatchByBatchId(int batchId);
        void UpdateBatchDetails(BatchTransferDTO batchTransferDTO);
        IList<ConsolidatedReportDTO> GetReportForNonChequePayments();
        IList<ConsolidatedReportDTO> GetReportForChequePayments();
        IList<object> GetCollectionHeaderForReports(int paymentModeId, int paymentHeaderFor);
        decimal GetPaymentMadeByCustomer(int customerID, DateTime fromDate, DateTime toDate);
        int SavePaymentRefund(PaymentRefundDTO paymentRefund);
        IList<PaymentRefundDTO> GetCustomerPaymentRefundList(int customerID);
        PaymentRefundDTO GetPaymentRefundDetails(int refundID);
        IList<CollectionSummaryDTO> GetCollectionSummaryDetails();
        IList<PaymentCollectionDTO> GetCollectionDetailsForPrint(string searchValue, bool? isNumeric, int? userId);
        IList<PaymentCollectionDTO> GetCollectionDetailsForCancelAndReIssue(string searchValue, bool? isNumeric, int? userId);
        PaymentCollectionDTO GetCollectionDetailsByOldReceiptNo(int oldReceiptNo);
        SMSPaymentRegistrationDTO GetSMSPaymentDetailsByID(int smsID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="smsPaymentDetails"></param>
        /// <returns></returns>
        int SaveAndUpdateSMSPaymentDetails(SMSPaymentRegistrationDTO smsPaymentDetails);
        IList<SMSPaymentRegistrationDTO> GetCustomerSMSPaymentList(int? customerID, int? smsPaymentID, int validDays);

        SMSPaymentRegistrationDTO GetSMSPaymentDetails(int smsPaymentId, int validDays);
    }
}