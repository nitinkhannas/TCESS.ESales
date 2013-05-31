// -----------------------------------------------------------------------
// <copyright file="PaymentCollectionService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TCESS.ESales.BusinessLayer.Services.GhatoCollection
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Transactions;
    using Microsoft.Practices.Unity;
    using TCESS.ESales.BusinessLayer.Interfaces.GhatoCollection;
    using TCESS.ESales.CommonLayer.CommonLibrary;
    using TCESS.ESales.CommonLayer.Unity;
    using TCESS.ESales.DataTransferObjects.GhatoCollection;
    using TCESS.ESales.DataTransferObjects.Masters;
    using TCESS.ESales.PersistenceLayer.Entity;
    using TCESS.ESales.PersistenceLayer.Interfaces;
    using TCESS.ESales.DataTransferObjects;

    #endregion

    public class PaymentService : GhatoCollectionBaseService, IPaymentService
    {
        int IPaymentService.SaveOrUpdateCollection(PaymentCollectionDTO paymentCollection)
        {
            paymentcollection paymentcollectionEntity = new paymentcollection();
            AutoMapper.Mapper.Map(paymentCollection, paymentcollectionEntity);

            if (paymentCollection.PC_Id == 0)
            {
                base.PaymentCollectionRepository.Save(paymentcollectionEntity);
            }
            else
            {
                base.PaymentCollectionRepository.Update(paymentcollectionEntity);
            }
            return paymentcollectionEntity.PC_Id;
        }

        IList<PaymentCollectionDTO> IPaymentService.GetCollectionDetails(string searchValue, Nullable<bool> isNumeric, Nullable<int> userId)
        {
            DateTime currentDate = DateTime.Now.Date;

            //To retrive payment collection from database for transit to head cashier
            List<PaymentCollectionDTO> lstPaymentCollection = (from pcItem in base.PaymentCollectionRepository.GetQuery().Where(item => item.PC_IsDeleted == false
                                                                      && item.PC_ReceiptDate == currentDate && item.PC_Status == null)
                                                               join custItem in base.CustomerRepository.GetQuery().Where(item => item.Cust_IsDeleted == false)
                                                               on pcItem.PC_CustId equals custItem.Cust_Id
                                                               join payModeItem in base.PaymentModeRepository.GetQuery()
                                                               on pcItem.PC_PaymentMode equals payModeItem.Paymentmode_Id
                                                               join bankItem in base.BankRepository.GetQuery()
                                                               on pcItem.PC_BankDrawn equals bankItem.Bank_Id into item
                                                               from subItem in item.DefaultIfEmpty()
                                                               select new PaymentCollectionDTO
                                                               {
                                                                   PC_Id = pcItem.PC_Id,
                                                                   PC_ReceiptNo = pcItem.PC_ReceiptNo,
                                                                   PC_ReceiptDate = pcItem.PC_ReceiptDate,
                                                                   CustomerCode = custItem.Cust_Code,
                                                                   CustomerName = custItem.Cust_TradeName,
                                                                   PaymentModeName = payModeItem.Paymentmode_Name,
                                                                   PC_InstrumentNo = pcItem.PC_InstrumentNo ?? "NA",
                                                                   PC_Amount = pcItem.PC_Amount,
                                                                   PC_BankDrawn = pcItem.PC_BankDrawn,
                                                                   BankName = subItem == null ? "NA" : subItem.Bank_Name,
                                                                   PC_BankBranch = pcItem.PC_BankBranch ?? "NA",
                                                                   PC_InstrumentDate = pcItem.PC_InstrumentDate,
                                                                   PC_CreatedBy = pcItem.PC_CreatedBy
                                                               }).OrderBy(item => item.PC_Id).ToList();

            if (!string.IsNullOrEmpty(searchValue))
            {
                if (!(bool)isNumeric)
                {
                    lstPaymentCollection = lstPaymentCollection.Where(item => item.CustomerCode.ToLower() == searchValue.ToLower()).ToList();
                }
                else
                {
                    int searchParameter = Convert.ToInt32(searchValue);
                    lstPaymentCollection = lstPaymentCollection.Where(item => item.PC_Id == searchParameter).ToList();
                }
            }

            if (userId != null && userId > 1)
            {
                lstPaymentCollection = lstPaymentCollection.Where(item => item.PC_CreatedBy == userId).ToList();
            }
            return lstPaymentCollection;
        }

        void IPaymentService.SendPaymentToHeadCashier(BatchTransferDTO batchTransferDTO)
        {
            batchtransfer batchtransferEntity = new batchtransfer();
            AutoMapper.Mapper.Map(batchTransferDTO, batchtransferEntity);

            using (TransactionScope transactionScope = new TransactionScope())
            {
                base.BatchTransferRepository.Save(batchtransferEntity);

                foreach (PaymentTransitDTO item in batchTransferDTO.PaymentTransits)
                {
                    PaymentCollectionDTO paymentCollectionDTO = GetCollectionDetailsById(item.PaymentTransit_CollectionId);
                    paymentCollectionDTO.PC_Status = (int)Globals.CollectionStatus.SENTTOCASHIER;

                    paymentcollection paymentcollectionEntity = new paymentcollection();
                    AutoMapper.Mapper.Map(paymentCollectionDTO, paymentcollectionEntity);

                    base.PaymentCollectionRepository.Update(paymentcollectionEntity);
                }
                transactionScope.Complete();
            }
        }

        /// <summary>
        /// Get Business Type List By Type Id
        /// </summary>
        /// <param name="businessTypeId">Int32:businessTypeId</param>
        /// <returns></returns>
        public PaymentCollectionDTO GetCollectionDetailsById(int collectionId)
        {
            var query = (from pcItem in base.PaymentCollectionRepository.GetQuery()
                             .Where(item => item.PC_Id == collectionId)
                         join bankItem in base.BankRepository.GetQuery()
                         on pcItem.PC_BankDrawn equals bankItem.Bank_Id into item
                         from subItem in item.DefaultIfEmpty()
                         select new PaymentCollectionDTO
                         {
                             PC_Id = pcItem.PC_Id,
                             PC_CustId = pcItem.PC_CustId,
                             PC_ReceiptNo = pcItem.PC_ReceiptNo,
                             PC_ReceiptDate = pcItem.PC_ReceiptDate,
                             CustomerCode = pcItem.customer.Cust_Code,
                             CustomerName = pcItem.customer.Cust_OwnerName,
                             CustomerTradeName = pcItem.customer.Cust_TradeName,
                             CustomerDistrict = pcItem.customer.district.Dist_Name,
                             PC_PaymentMode = pcItem.PC_PaymentMode,
                             PaymentModeName = pcItem.paymentmode.Paymentmode_Name,
                             PC_InstrumentNo = pcItem.PC_InstrumentNo,
                             PC_Amount = pcItem.PC_Amount,
                             PC_BankDrawn = pcItem.PC_BankDrawn,
                             BankName = subItem == null ? string.Empty : subItem.Bank_Name,
                             PC_BankBranch = pcItem.PC_BankBranch,
                             PC_BankIFSCCode = pcItem.PC_BankIFSCCode,
                             PC_InstrumentDate = pcItem.PC_InstrumentDate,
                             PC_CreatedDate = pcItem.PC_CreatedDate,
                             PC_Remark = pcItem.PC_Remark,
                             PC_PayerName = pcItem.PC_PayerName,
                             PC_MobileNumber = pcItem.PC_MobileNumber,
                             PC_InstrumentStatus = pcItem.PC_InstrumentStatus,
                             PC_InstrumentRealizationDate = pcItem.PC_InstrumentRealizationDate,
                             PC_LastPrintDate = pcItem.PC_LastPrintDate,
                             PC_ReprintCount = pcItem.PC_ReprintCount,
                             PC_CreatedBy = pcItem.PC_CreatedBy,
                             PC_Status = pcItem.PC_Status,
                         }).ToList();

            PaymentCollectionDTO paymentCollectionDTO = query[0];
            return paymentCollectionDTO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="receiptNo"></param>
        /// <returns></returns>
        public PaymentCollectionDTO GetCollectionDetailsByReceiptNo(string receiptNo)
        {
            var query = (from pcItem in base.PaymentCollectionRepository.GetQuery()
                             .Where(item => item.PC_ReceiptNo == receiptNo)
                         join bankItem in base.BankRepository.GetQuery()
                         on pcItem.PC_BankDrawn equals bankItem.Bank_Id into item
                         from subItem in item.DefaultIfEmpty()
                         select new PaymentCollectionDTO
                         {
                             PC_Id = pcItem.PC_Id,
                             PC_CustId = pcItem.PC_CustId,
                             PC_ReceiptNo = pcItem.PC_ReceiptNo,
                             PC_ReceiptDate = pcItem.PC_ReceiptDate,
                             CustomerCode = pcItem.customer.Cust_Code,
                             CustomerName = pcItem.customer.Cust_OwnerName,
                             CustomerTradeName = pcItem.customer.Cust_TradeName,
                             CustomerDistrict = pcItem.customer.district.Dist_Name,
                             PC_PaymentMode = pcItem.PC_PaymentMode,
                             PaymentModeName = pcItem.paymentmode.Paymentmode_Name,
                             PC_InstrumentNo = pcItem.PC_InstrumentNo,
                             PC_Amount = pcItem.PC_Amount,
                             PC_BankDrawn = pcItem.PC_BankDrawn,
                             BankName = subItem == null ? string.Empty : subItem.Bank_Name,
                             PC_BankBranch = pcItem.PC_BankBranch,
                             PC_BankIFSCCode = pcItem.PC_BankIFSCCode,
                             PC_InstrumentDate = pcItem.PC_InstrumentDate,
                             PC_Remark = pcItem.PC_Remark,
                             PC_PayerName = pcItem.PC_PayerName,
                             PC_MobileNumber = pcItem.PC_MobileNumber,
                             PC_InstrumentStatus = pcItem.PC_InstrumentStatus,
                             PC_InstrumentRealizationDate = pcItem.PC_InstrumentRealizationDate,
                             PC_LastPrintDate = pcItem.PC_LastPrintDate,
                             PC_ReprintCount = pcItem.PC_ReprintCount,
                             PC_CreatedBy = pcItem.PC_CreatedBy,
                             PC_Status = pcItem.PC_Status,
                         }).ToList();

            PaymentCollectionDTO paymentCollectionDTO = query[0];
            return paymentCollectionDTO;
        }


        PaymentCollectionDTO IPaymentService.GetCollectionDetailsByOldReceiptNo(int oldReceiptNo)
        {
            PaymentCollectionDTO paymentCollection = new PaymentCollectionDTO();
            AutoMapper.Mapper.Map(base.PaymentCollectionRepository.GetSingle(item =>
                item.PC_OldReceiptId == oldReceiptNo), paymentCollection);
            return paymentCollection;
        }

        IList<BatchTransferDTO> IPaymentService.GetBatchDetails(Nullable<int> userId,
            Nullable<int> paymentMode)
        {
            //To retrive payment collection from database for transit to head cashier
            List<BatchTransferDTO> lstBatchTransfer = (from batchItem in base.BatchTransferRepository.GetQuery()
                                                       .Where(item => item.BT_ApprovedBy == null)
                                                       join payTransit in base.PaymentTransitRepository.GetQuery()
                                                       on batchItem.BT_ID equals payTransit.PaymentTransit_BatchId
                                                       join payColItem in base.PaymentCollectionRepository.GetQuery()
                                                       .Where(item => item.PC_IsDeleted == false)
                                                       on payTransit.PaymentTransit_CollectionId equals payColItem.PC_Id
                                                       join userItem in base.UserRepository.GetQuery()
                                                       on batchItem.BT_CreatedBy equals userItem.id
                                                       join counterItem in base.CounterRepository.GetQuery()
                                                       on batchItem.BT_CreatedBy equals counterItem.Counter_User_Id
                                                       //Commented as per request by Mr Arora
                                                       //For Batch specific details, counter should be used as users
                                                       //join counterDetailItem in base.CounterDetailRepository.GetQuery()
                                                       //.Where(item => item.CounterDetail_Date == DateTime.Today)
                                                       //on counterItem.Counter_Id equals counterDetailItem.CounterDetail_Counter_ID
                                                       let item = new
                                                       {
                                                           BatchId = batchItem.BT_ID,
                                                           BatchStatus = batchItem.BT_Status,
                                                           CounterName = counterItem.Counter_Name,
                                                           CreatedBy = batchItem.BT_CreatedBy,
                                                           CreatedDate = batchItem.BT_CreatedDate,
                                                           PaymentMode = payColItem.PC_PaymentMode
                                                       }
                                                       group payColItem by item into groupByItem
                                                       select new BatchTransferDTO
                                                       {
                                                           BT_Id = groupByItem.Key.BatchId,
                                                           CounterName = groupByItem.Key.CounterName,
                                                           Amount = groupByItem.Sum(sum => sum.PC_Amount),
                                                           BT_CreatedBy = groupByItem.Key.CreatedBy,
                                                           BT_Status = groupByItem.Key.BatchStatus,
                                                           BT_CreatedDate = groupByItem.Key.CreatedDate,
                                                           PaymentMode = groupByItem.Key.PaymentMode
                                                       }).ToList();

            if (userId != null)
            {
                lstBatchTransfer = lstBatchTransfer.Where(item => item.BT_CreatedBy == userId).ToList();
            }

            if (paymentMode != null)
            {
                int cash = (int)Globals.PaymentModes.CASH;

                if (paymentMode == cash)
                {
                    lstBatchTransfer = lstBatchTransfer.Where(item => item.PaymentMode == cash).ToList();
                }
                else
                {
                    lstBatchTransfer = lstBatchTransfer.Where(item => item.PaymentMode != cash).ToList();
                }
            }
            return lstBatchTransfer;
        }

        IList<BatchTransferDTO> IPaymentService.GetBatchDetailsForCollectionScreen(
            Nullable<int> userId, Nullable<int> paymentMode)
        {
            DateTime currentDate = DateTime.Now.Date;

            //To retrive payment collection from database for transit to head cashier
            List<BatchTransferDTO> lstBatchTransfer = (from batchItem in base.BatchTransferRepository.GetQuery().
                                                       Where(item => ((item.BT_Status == 1 && item.BT_CreatedDate <= currentDate.Date)
                                                           || (item.BT_Status == 2 && item.BT_CreatedDate == currentDate.Date)))
                                                       join payTransit in base.PaymentTransitRepository.GetQuery()
                                                       on batchItem.BT_ID equals payTransit.PaymentTransit_BatchId
                                                       join payColItem in base.PaymentCollectionRepository.GetQuery()
                                                       .Where(item => item.PC_IsDeleted == false)
                                                       on payTransit.PaymentTransit_CollectionId equals payColItem.PC_Id
                                                       join userItem in base.UserRepository.GetQuery()
                                                       on batchItem.BT_CreatedBy equals userItem.id
                                                       join counterItem in base.CounterRepository.GetQuery()
                                                       on batchItem.BT_CreatedBy equals counterItem.Counter_User_Id
                                                       let item = new
                                                       {
                                                           BatchId = batchItem.BT_ID,
                                                           BatchStatus = batchItem.BT_Status,
                                                           CounterName = counterItem.Counter_Name,
                                                           CreatedBy = batchItem.BT_CreatedBy,
                                                           CreatedDate = batchItem.BT_CreatedDate,
                                                           PaymentMode = payColItem.PC_PaymentMode
                                                       }
                                                       group payColItem by item into groupByItem
                                                       select new BatchTransferDTO
                                                       {
                                                           BT_Id = groupByItem.Key.BatchId,
                                                           CounterName = groupByItem.Key.CounterName,
                                                           Amount = groupByItem.Sum(sum => sum.PC_Amount),
                                                           BT_CreatedBy = groupByItem.Key.CreatedBy,
                                                           BT_Status = groupByItem.Key.BatchStatus,
                                                           BT_CreatedDate = groupByItem.Key.CreatedDate,
                                                           PaymentMode = groupByItem.Key.PaymentMode
                                                       }).ToList();

            if (userId != null)
            {
                lstBatchTransfer = lstBatchTransfer.Where(item => item.BT_CreatedBy == userId).ToList();
            }

            if (paymentMode != null)
            {
                int cash = (int)Globals.PaymentModes.CASH;

                if (paymentMode == cash)
                {
                    lstBatchTransfer = lstBatchTransfer.Where(item => item.PaymentMode == cash).ToList();
                }
                else
                {
                    lstBatchTransfer = lstBatchTransfer.Where(item => item.PaymentMode != cash).ToList();
                }
            }
            return lstBatchTransfer;
        }

        IList<PaymentCollectionDTO> IPaymentService.GetCollectionDetailsFromBatchId(int batchId)
        {
            List<BankDTO> lstBankDTO = new List<BankDTO>();
            AutoMapper.Mapper.Map((from bankItem in BankRepository.GetQuery() select bankItem).ToList(), lstBankDTO);

            //To retrive payment collection from database for transit to head cashier
            List<PaymentCollectionDTO> lstPaymentCollectionDTO = (from transitItem in base.PaymentTransitRepository.GetQuery()
                                                                      .Where(item => item.PaymentTransit_BatchId == batchId
                                                                      && item.paymentcollection.PC_IsDeleted == false)
                                                                  let bankId = transitItem.paymentcollection.PC_BankDrawn
                                                                  join bankItem in base.BankRepository.GetQuery()
                                                                  on bankId equals bankItem.Bank_Id into item
                                                                  from subItem in item.DefaultIfEmpty()
                                                                  select new PaymentCollectionDTO
                                                                  {
                                                                      PC_Id = transitItem.paymentcollection.PC_Id,
                                                                      PC_ReceiptNo = transitItem.paymentcollection.PC_ReceiptNo,
                                                                      PC_ReceiptDate = transitItem.paymentcollection.PC_ReceiptDate,
                                                                      CustomerCode = transitItem.paymentcollection.customer.Cust_Code,
                                                                      CustomerName = transitItem.paymentcollection.customer.Cust_TradeName,
                                                                      PaymentModeName = transitItem.paymentcollection.paymentmode.Paymentmode_Name ?? "NA",
                                                                      PC_InstrumentNo = transitItem.paymentcollection.PC_InstrumentNo ?? "NA",
                                                                      PC_Amount = transitItem.paymentcollection.PC_Amount,
                                                                      BankName = subItem == null ? "NA" : subItem.Bank_Name,
                                                                      PC_BankBranch = transitItem.paymentcollection.PC_BankBranch ?? "NA",
                                                                      PC_InstrumentDate = transitItem.paymentcollection.PC_InstrumentDate
                                                                  }).ToList();
            return lstPaymentCollectionDTO;
        }

        BatchTransferDTO IPaymentService.GetBatchByBatchId(int batchId)
        {
            BatchTransferDTO batchTransferDTO = new BatchTransferDTO();
            AutoMapper.Mapper.Map(base.BatchTransferRepository.GetSingle(item => item.BT_ID == batchId), batchTransferDTO);
            return batchTransferDTO;
        }

        void IPaymentService.UpdateBatchDetails(BatchTransferDTO batchTransferDTO)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                batchtransfer batchTransferEntity = new batchtransfer();
                AutoMapper.Mapper.Map(batchTransferDTO, batchTransferEntity);

                base.BatchTransferRepository.Update(batchTransferEntity);

                foreach (PaymentTransitDTO item in batchTransferDTO.PaymentTransits)
                {
                    PaymentCollectionDTO paymentCollectionDTO = GetCollectionDetailsById(item.PaymentTransit_CollectionId);
                    paymentCollectionDTO.PC_Status = (int)Globals.CollectionStatus.ACCEPTEDBYCASHIER;

                    ////Instrument status and Instrument Realization date to be updated
                    ////in case payment mode is cheque
                    if (paymentCollectionDTO.PC_PaymentMode == (int)Globals.PaymentModes.CHEQUE)
                    {
                        paymentCollectionDTO.PC_InstrumentStatus = (int)Globals.InstrumentStatus.PENDING;
                        paymentCollectionDTO.PC_InstrumentRealizationDate = DateTime.Now;
                    }
                    else
                    {
                        paymentCollectionDTO.PC_InstrumentStatus = 0;
                    }

                    paymentcollection paymentcollectionEntity = new paymentcollection();
                    AutoMapper.Mapper.Map(paymentCollectionDTO, paymentcollectionEntity);

                    base.PaymentCollectionRepository.Update(paymentcollectionEntity);
                }
                transactionScope.Complete();
            }
        }

        IList<PaymentCollectionDTO> IPaymentService.GetChequeDetailsForActivation(int paymentModeId, string chequeNumber)
        {
            IList<PaymentCollectionDTO> lstPaymentCollection = (from pcItem in base.PaymentCollectionRepository.GetQuery()
                                                                    .Where(item => item.PC_IsDeleted == false
                                                                        && item.PC_PaymentMode == paymentModeId
                                                                        && item.PC_InstrumentStatus == (int)Globals.InstrumentStatus.PENDING)
                                                                join custItem in base.CustomerRepository.GetQuery()
                                                                .Where(item => item.Cust_IsDeleted == false)
                                                                on pcItem.PC_CustId equals custItem.Cust_Id
                                                                join bankItem in base.BankRepository.GetQuery()
                                                                on pcItem.PC_BankDrawn equals bankItem.Bank_Id
                                                                select new PaymentCollectionDTO
                                                                {
                                                                    PC_Id = pcItem.PC_Id,
                                                                    PC_ReceiptNo = pcItem.PC_ReceiptNo,
                                                                    PC_ReceiptDate = pcItem.PC_ReceiptDate,
                                                                    CustomerCode = custItem.Cust_Code,
                                                                    CustomerName = custItem.Cust_TradeName,
                                                                    PC_InstrumentNo = pcItem.PC_InstrumentNo,
                                                                    PC_Amount = pcItem.PC_Amount,
                                                                    BankName = bankItem.Bank_Name,
                                                                    PC_BankBranch = pcItem.PC_BankBranch,
                                                                    PC_InstrumentDate = pcItem.PC_InstrumentDate
                                                                }).ToList();

            if (!string.IsNullOrEmpty(chequeNumber))
            {
                lstPaymentCollection = lstPaymentCollection.Where(item => item.PC_InstrumentNo == chequeNumber).ToList();
            }

            return lstPaymentCollection;
        }

        IList<ConsolidatedReportDTO> IPaymentService.GetReportForNonChequePayments()
        {
            //To retrive payment collection from database for transit to head cashier
            List<ConsolidatedReportDTO> lstConsolidatedReport = (from nonChqItem in base.NonChequePaymentRepository.GetQuery()
                                                                 let item = new
                                                                 {
                                                                     CounterId = nonChqItem.CounterId,
                                                                     CounterName = nonChqItem.CounterName
                                                                 }
                                                                 group nonChqItem by item into groupByItem
                                                                 select new ConsolidatedReportDTO
                                                                 {
                                                                     CounterId = groupByItem.Key.CounterId,
                                                                     CounterName = groupByItem.Key.CounterName,
                                                                     TotalAmount = groupByItem.Sum(sum => sum.TotalAmount),
                                                                     TransferredAmount = groupByItem.Sum(sum => sum.TransferedAmount),
                                                                     InTransitAmount = groupByItem.Sum(sum => sum.InTransitAmount),
                                                                     CashInHand = groupByItem.Sum(sum => sum.CashInHand)
                                                                 }).ToList();

            return lstConsolidatedReport;
        }

        IList<ConsolidatedReportDTO> IPaymentService.GetReportForChequePayments()
        {
            //To retrive payment collection from database for transit to head cashier
            List<ConsolidatedReportDTO> lstConsolidatedReport = (from chqItem in base.ChequePaymentRepository.GetQuery()
                                                                 let item = new
                                                                 {
                                                                     CounterId = chqItem.CounterId,
                                                                     CounterName = chqItem.CounterName
                                                                 }
                                                                 group chqItem by item into groupByItem
                                                                 select new ConsolidatedReportDTO
                                                                 {
                                                                     CounterId = groupByItem.Key.CounterId,
                                                                     TotalChequeCount = groupByItem.Count(),
                                                                     CounterName = groupByItem.Key.CounterName,
                                                                     TotalAmount = groupByItem.Sum(sum => sum.TotalAmount),
                                                                     ChequesCleared = groupByItem.Sum(sum => sum.ChequesCleared),
                                                                     ChequesBounced = groupByItem.Sum(sum => sum.ChequesBounced),
                                                                     TransferredAmount = groupByItem.Sum(sum => sum.TransferedAmount),
                                                                     InTransitAmount = groupByItem.Sum(sum => sum.InTransitAmount),
                                                                     CashInHand = groupByItem.Sum(sum => sum.CashInHand),
                                                                 }).ToList();

            return lstConsolidatedReport;
        }

        IList<object> IPaymentService.GetCollectionHeaderForReports(int paymentModeId, int paymentHeaderFor)
        {
            DateTime currentDate = DateTime.Now.Date;
            IList<PaymentCollectionDTO> lstPaymentCollectionDTO = null;

            IList<paymentcollection> lstPaymentCollection = (from payItem in base.PaymentCollectionRepository.GetQuery()
                                                             where payItem.PC_ReceiptDate == currentDate &&
                                                             payItem.PC_IsDeleted == false
                                                             select payItem).ToList();

            if (paymentHeaderFor == (int)Globals.PaymentHeader.FORREPORTSCREEN)
            {
                lstPaymentCollectionDTO = GetCollectionHeaderForReports(paymentModeId, lstPaymentCollection);
            }
            else
            {
                lstPaymentCollectionDTO = GetCollectionHeaderForSupervisor(paymentModeId, lstPaymentCollection);
            }

            IList<object> paymentDetails = GetHeaderDetails(lstPaymentCollectionDTO, true);
            return paymentDetails;
        }

        private IList<PaymentCollectionDTO> GetCollectionHeaderForSupervisor(int paymentModeId, IList<paymentcollection> lstPaymentCollection)
        {
            int cash = (int)Globals.PaymentModes.CASH;
            IList<PaymentCollectionDTO> lstPaymentCollectionDTO = new List<PaymentCollectionDTO>();

            if (paymentModeId != cash)
            {
                AutoMapper.Mapper.Map((from payItem in lstPaymentCollection
                                       where payItem.PC_PaymentMode != cash
                                       select payItem).ToList(), lstPaymentCollectionDTO);
            }
            else
            {
                AutoMapper.Mapper.Map((from payItem in lstPaymentCollection
                                       where payItem.PC_PaymentMode == cash
                                       select payItem).ToList(), lstPaymentCollectionDTO);
            }
            return lstPaymentCollectionDTO;
        }

        private IList<PaymentCollectionDTO> GetCollectionHeaderForReports(int paymentModeId, IList<paymentcollection> lstPaymentCollection)
        {
            int cheque = (int)Globals.PaymentModes.CHEQUE;
            IList<PaymentCollectionDTO> lstPaymentCollectionDTO = new List<PaymentCollectionDTO>();

            if (paymentModeId != cheque)
            {
                AutoMapper.Mapper.Map((from payItem in lstPaymentCollection
                                       where payItem.PC_PaymentMode != cheque
                                       select payItem).ToList(), lstPaymentCollectionDTO);
            }
            else
            {
                AutoMapper.Mapper.Map((from payItem in lstPaymentCollection
                                       where payItem.PC_PaymentMode == cheque
                                       select payItem).ToList(), lstPaymentCollectionDTO);
            }
            return lstPaymentCollectionDTO;
        }

        IList<object> IPaymentService.GetCollectionHeaderForTransit(bool isApprovePayments, Nullable<int> userId)
        {
            DateTime currentDate = DateTime.Now.Date;

            IList<PaymentCollectionDTO> lstPaymentCollection = new List<PaymentCollectionDTO>();

            if (isApprovePayments)
            {
                AutoMapper.Mapper.Map((from payItem in base.PaymentCollectionRepository.GetQuery()
                                       where payItem.PC_ReceiptDate == currentDate &&
                                       payItem.PC_IsDeleted == false
                                       select payItem).ToList(), lstPaymentCollection);
            }
            else
            {
                AutoMapper.Mapper.Map((from payItem in base.PaymentCollectionRepository.GetQuery()
                                       where payItem.PC_IsDeleted == false &&
                                       payItem.PC_ReceiptDate == currentDate
                                       select payItem).ToList(), lstPaymentCollection);
            }

            if (userId != null && userId > 1)
            {
                lstPaymentCollection = lstPaymentCollection.Where(item => item.PC_CreatedBy == userId).ToList();
            }

            IList<object> paymentDetails = GetHeaderDetails(lstPaymentCollection, isApprovePayments);
            return paymentDetails;
        }

        private IList<object> GetHeaderDetails(IList<PaymentCollectionDTO> lstPaymentCollection, bool isApprovePayments)
        {
            decimal cashInHand = 0;
            decimal transitAmount = 0;
            decimal transferredAmount = 0;
            decimal totalAmount = 0;
            int totalTransactions = 0;
            int pendingTransactions = 0;
            object[] paymentDetails = null;

            if (lstPaymentCollection.Count > 0)
            {
                //Sum of cash in hand at counter
                cashInHand = lstPaymentCollection.Where(item => item.PC_Status == null).Select(item => item.PC_Amount).Sum();

                //Transit amount sent to head cashier
                transitAmount = lstPaymentCollection.Where(item => item.PC_Status == (int)Globals.CollectionStatus.SENTTOCASHIER)
                    .Select(item => item.PC_Amount).Sum();

                //Amount recieved by head cashier
                transferredAmount = lstPaymentCollection.Where(item => item.PC_Status == (int)Globals.CollectionStatus.ACCEPTEDBYCASHIER)
                    .Select(item => item.PC_Amount).Sum();

                //Total amount recieved at payment counter
                totalAmount = lstPaymentCollection.Select(item => item.PC_Amount).Sum();

                //Total transactions done at payment counter
                totalTransactions = lstPaymentCollection.Select(item => item.PC_Id).Count();

                //Transactions pending at payment counter
                pendingTransactions = lstPaymentCollection.Where(item => item.PC_Status == null).Select(item => item.PC_Amount).Count();
            }

            if (isApprovePayments)
            {
                paymentDetails = new object[] { cashInHand, transitAmount, transferredAmount, totalAmount, totalTransactions, pendingTransactions };
            }
            else
            {
                paymentDetails = new object[] { cashInHand, transitAmount, transferredAmount, totalAmount };
            }
            return paymentDetails;
        }

        IList<CollectionSummaryDTO> IPaymentService.GetCollectionSummaryDetails()
        {
            IList<CollectionSummaryDTO> lstCollectionSummary = new List<CollectionSummaryDTO>();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<vwcollectionsummary>>()
                                                       .GetQuery(), lstCollectionSummary);
            return lstCollectionSummary;
        }

        decimal IPaymentService.GetPaymentMadeByCustomer(int customerID, DateTime fromDate, DateTime toDate)
        {
            List<paymentcollection> lstPaymentCollectionEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<paymentcollection>>().GetQuery().Where(item =>
                    item.PC_CustId == customerID && item.PC_Status == 2
                    && (item.PC_InstrumentStatus == 0 || item.PC_InstrumentStatus == 1 &&
                    (item.PC_CreatedDate <= toDate && item.PC_CreatedDate >= fromDate))).ToList();

            return lstPaymentCollectionEntity.Sum(total => total.PC_Amount);
        }

        public int SavePaymentRefund(PaymentRefundDTO paymentRefund)
        {
            paymentrefund paymentRefundEntity = new paymentrefund();
            AutoMapper.Mapper.Map(paymentRefund, paymentRefundEntity);
            base.PaymentRefundRepository.Save(paymentRefundEntity);
            return paymentRefundEntity.PR_ID;
        }

        IList<PaymentRefundDTO> IPaymentService.GetCustomerPaymentRefundList(int customerID)
        {
            IList<PaymentRefundDTO> paymentRefund = new List<PaymentRefundDTO>();
            List<paymentrefund> lstPaymentRefundEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<paymentrefund>>()
                                                       .GetQuery().Where(item => item.PR_CustID == customerID).ToList();

            AutoMapper.Mapper.Map(lstPaymentRefundEntity, paymentRefund);
            return paymentRefund;
        }

        IList<PaymentRefundDTO> IPaymentService.GetCustomerRefundList(int customerID, DateTime fromDate, DateTime toDate)
        {
            IList<PaymentRefundDTO> paymentRefund = new List<PaymentRefundDTO>();
            List<paymentrefund> lstPaymentRefundEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<paymentrefund>>()
                                                       .GetQuery().Where(item => item.PR_CustID == customerID && ((item.PR_CreatedDate >= fromDate && item.PR_CreatedDate <= toDate))).ToList();

            AutoMapper.Mapper.Map(lstPaymentRefundEntity, paymentRefund);
            return paymentRefund;
        }

        PaymentRefundDTO IPaymentService.GetPaymentRefundDetails(int refundID)
        {
            PaymentRefundDTO paymentRefundDetails = new PaymentRefundDTO();
            paymentrefund lstPaymentRefundEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<paymentrefund>>()
                                                       .GetSingle(item => item.PR_ID == refundID);

            AutoMapper.Mapper.Map(paymentRefundDetails, lstPaymentRefundEntity);
            return paymentRefundDetails;
        }

        IList<PaymentCollectionDTO> IPaymentService.GetCollectionDetailsForPrint(string searchValue, bool? isNumeric, int? userId)
        {
            //To retrive payment collection from database for transit to head cashier
            List<PaymentCollectionDTO> lstPaymentCollection =
                (from pcItem in base.PaymentCollectionRepository.GetQuery()
                     .Where(item => item.PC_IsDeleted == false)
                 join custItem in base.CustomerRepository.GetQuery()
                 .Where(item => item.Cust_IsDeleted == false)
                 on pcItem.PC_CustId equals custItem.Cust_Id
                 join payModeItem in base.PaymentModeRepository.GetQuery()
                 on pcItem.PC_PaymentMode equals payModeItem.Paymentmode_Id
                 join bankItem in base.BankRepository.GetQuery()
                 on pcItem.PC_BankDrawn equals bankItem.Bank_Id into item
                 from subItem in item.DefaultIfEmpty()
                 select new PaymentCollectionDTO
                 {
                     PC_Id = pcItem.PC_Id,
                     PC_ReceiptNo = pcItem.PC_ReceiptNo,
                     PC_ReceiptDate = pcItem.PC_ReceiptDate,
                     CustomerCode = custItem.Cust_Code,
                     CustomerName = custItem.Cust_TradeName,
                     PaymentModeName = payModeItem.Paymentmode_Name,
                     PC_InstrumentNo = pcItem.PC_InstrumentNo ?? "NA",
                     PC_Amount = pcItem.PC_Amount,
                     PC_BankDrawn = pcItem.PC_BankDrawn,
                     BankName = subItem == null ? "NA" : subItem.Bank_Name,
                     PC_BankBranch = pcItem.PC_BankBranch ?? "NA",
                     PC_LastPrintDate = pcItem.PC_LastPrintDate,
                     PC_ReprintCount = pcItem.PC_ReprintCount,
                     PC_InstrumentDate = pcItem.PC_InstrumentDate,
                     PC_CreatedBy = pcItem.PC_CreatedBy
                 }).ToList();

            if (!string.IsNullOrEmpty(searchValue))
            {
                if (!(bool)isNumeric)
                {
                    lstPaymentCollection = lstPaymentCollection.Where(item =>
                        item.CustomerCode.ToLower() == searchValue.ToLower()).
                        OrderByDescending(item => item.PC_Id).Take(5).ToList();
                }
                else
                {
                    int searchParameter = Convert.ToInt32(searchValue);
                    lstPaymentCollection = lstPaymentCollection.Where(item =>
                        item.PC_Id == searchParameter).OrderByDescending(item =>
                            item.PC_Id).Take(5).ToList();
                }
            }

            if (userId != null && userId > 1)
            {
                lstPaymentCollection = lstPaymentCollection.Where(item => item.PC_CreatedBy == userId).ToList();
            }
            return lstPaymentCollection;
        }

        IList<PaymentCollectionDTO> IPaymentService.GetCollectionDetailsForCancelAndReIssue(string searchValue,
            bool? isNumeric, int? userId)
        {
            //To retrive payment collection from database for transit to head cashier
            List<PaymentCollectionDTO> lstPaymentCollection =
                (from pcItem in base.PaymentCollectionRepository.GetQuery()
                     .Where(item => (item.PC_Status == null ||
                         item.PC_Status == (int)Globals.CollectionStatus.CANCELLED)
                         && item.PC_PaymentMode != (int)Globals.PaymentModes.CASH)
                 join custItem in base.CustomerRepository.GetQuery()
                 .Where(item => item.Cust_IsDeleted == false)
                 on pcItem.PC_CustId equals custItem.Cust_Id
                 join payModeItem in base.PaymentModeRepository.GetQuery()
                 on pcItem.PC_PaymentMode equals payModeItem.Paymentmode_Id
                 join bankItem in base.BankRepository.GetQuery()
                 on pcItem.PC_BankDrawn equals bankItem.Bank_Id into item
                 from subItem in item.DefaultIfEmpty()
                 select new PaymentCollectionDTO
                 {
                     PC_Id = pcItem.PC_Id,
                     PC_CustId = custItem.Cust_Id,
                     PC_ReceiptNo = pcItem.PC_ReceiptNo,
                     PC_ReceiptDate = pcItem.PC_ReceiptDate,
                     CustomerCode = custItem.Cust_Code,
                     CustomerName = custItem.Cust_TradeName,
                     PC_PaymentMode = pcItem.PC_PaymentMode,
                     PaymentModeName = payModeItem.Paymentmode_Name,
                     PC_InstrumentNo = pcItem.PC_InstrumentNo ?? "NA",
                     PC_Amount = pcItem.PC_Amount,
                     PC_BankDrawn = pcItem.PC_BankDrawn,
                     BankName = subItem == null ? "NA" : subItem.Bank_Name,
                     PC_BankBranch = pcItem.PC_BankBranch ?? "NA",
                     PC_InstrumentDate = pcItem.PC_InstrumentDate,
                     PC_Status = pcItem.PC_Status,
                     PC_CreatedBy = pcItem.PC_CreatedBy
                 }).ToList();

            if (!string.IsNullOrEmpty(searchValue))
            {
                if (!(bool)isNumeric)
                {
                    lstPaymentCollection = lstPaymentCollection.Where(item =>
                        item.CustomerCode.ToLower() == searchValue.ToLower()).ToList();
                }
                else
                {
                    int searchParameter = Convert.ToInt32(searchValue);
                    lstPaymentCollection = lstPaymentCollection.Where(item =>
                        item.PC_Id == searchParameter).ToList();
                }
            }

            if (userId != null && userId > 1)
            {
                lstPaymentCollection = lstPaymentCollection.Where(item => item.PC_CreatedBy == userId).ToList();
            }
            return lstPaymentCollection;
        }

        public int SaveAndUpdateSMSPaymentDetails(SMSPaymentRegistrationDTO smsPaymentDetails)
        {
            int smsPay_Id = 0;
            smspaymentregistration smsPaymentegistrationEntity = new smspaymentregistration();
            AutoMapper.Mapper.Map(smsPaymentDetails, smsPaymentegistrationEntity);

            if (smsPaymentDetails.SMSPay_Id == 0)
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<smspaymentregistration>>().Save(smsPaymentegistrationEntity);
            }
            else
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<smspaymentregistration>>().Update(smsPaymentegistrationEntity);
            }

            smsPay_Id = smsPaymentegistrationEntity.SMSPay_Id;

            //return the details
            return smsPay_Id;
        }

        public IList<SMSPaymentRegistrationDTO> GetCustomerSMSPaymentList(int? customerID, int? smsPaymentID, int validDays)
        {
            DateTime previousday = DateTime.Now.AddDays(-validDays);
            List<smspaymentregistration> lstPaymentRegEntity = null;
            IList<SMSPaymentRegistrationDTO> smsPaymentRegDetails = new List<SMSPaymentRegistrationDTO>();
            if (customerID != null)
            {
                lstPaymentRegEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<smspaymentregistration>>()
                                                         .GetQuery().Where(item => item.SMSPay_CustId == customerID && item.SMSPay_Date >= previousday.Date && item.SMSPay_Status == false).ToList();
            }
            else if (smsPaymentID != null)
            {
                lstPaymentRegEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<smspaymentregistration>>()
                                                         .GetQuery().Where(item => item.SMSPay_Id == smsPaymentID && item.SMSPay_Date >= previousday.Date && item.SMSPay_Status == false).ToList();
            }
            AutoMapper.Mapper.Map(lstPaymentRegEntity, smsPaymentRegDetails);
            return smsPaymentRegDetails;
        }

        public SMSPaymentRegistrationDTO GetSMSPaymentDetails(int smsID, int validDays)
        {
            DateTime previousday = DateTime.Now.AddDays(-validDays);

            SMSPaymentRegistrationDTO smsPaymentRegDetails = new SMSPaymentRegistrationDTO();
            smspaymentregistration smsPaymentRegEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<smspaymentregistration>>()
                                                        .GetSingle(item => item.SMSPay_Id == smsID && item.SMSPay_Status == false
                                                        && item.SMSPay_Date >= previousday.Date);

            AutoMapper.Mapper.Map(smsPaymentRegEntity, smsPaymentRegDetails);
            return smsPaymentRegDetails;
        }
        public SMSPaymentRegistrationDTO GetSMSPaymentDetailsByID(int smsID)
        {


            SMSPaymentRegistrationDTO smsPaymentRegDetails = new SMSPaymentRegistrationDTO();
            smspaymentregistration smsPaymentRegEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<smspaymentregistration>>()
                                                        .GetSingle(item => item.SMSPay_Id == smsID);

            AutoMapper.Mapper.Map(smsPaymentRegEntity, smsPaymentRegDetails);
            return smsPaymentRegDetails;
        }


        public IList<PaymentCollectionDTO> GetActiveCollectionForPeriod(DateTime fromDate, DateTime toDate)
        {
            List<PaymentCollectionDTO> lstPaymentCollection = new List<PaymentCollectionDTO>();
            List<paymentcollection> lstPaymentCollectionEntity = ESalesUnityContainer.Container
                 .Resolve<IGenericRepository<paymentcollection>>().GetQuery().Where(item =>
                      item.PC_Status == 2
                     && (item.PC_InstrumentStatus == 0 || item.PC_InstrumentStatus == 1 &&
                     (item.PC_LastUpdateDate >= fromDate && item.PC_LastUpdateDate <= toDate))).ToList();

            AutoMapper.Mapper.Map(lstPaymentCollectionEntity, lstPaymentCollection);
            return lstPaymentCollection;
        }
        public IList<PaymentCollectionDTO> GetHoldActiveCollectionForPeriod(DateTime fromDate, DateTime toDate)
        {
            List<PaymentCollectionDTO> lstPaymentCollection = new List<PaymentCollectionDTO>();
            List<paymentcollection> lstPaymentCollectionEntity = ESalesUnityContainer.Container
                 .Resolve<IGenericRepository<paymentcollection>>().GetQuery().Where(item =>
                      item.PC_Status == 1
                     && (item.PC_InstrumentStatus == 0 || item.PC_InstrumentStatus == 2 &&
                     (item.PC_LastUpdateDate >= fromDate && item.PC_LastUpdateDate <= toDate))).ToList();

            AutoMapper.Mapper.Map(lstPaymentCollectionEntity, lstPaymentCollection);
            return lstPaymentCollection;
        }


        public IList<PaymentCollectionDTO> GetPaymentByCustomer(int customerID, DateTime fromDate, DateTime toDate)
        {
            List<PaymentCollectionDTO> lstPaymentCollection = new List<PaymentCollectionDTO>();
            List<paymentcollection> lstPaymentCollectionEntity = ESalesUnityContainer.Container
                .Resolve<IGenericRepository<paymentcollection>>().GetQuery().Where(item =>
                    item.PC_CustId == customerID && item.PC_Status == 2
                    && (item.PC_InstrumentStatus == 0 || item.PC_InstrumentStatus == 1) &&
                    (item.PC_CreatedDate <= toDate && item.PC_CreatedDate >= fromDate)).ToList();
            AutoMapper.Mapper.Map(lstPaymentCollectionEntity, lstPaymentCollection);
            return lstPaymentCollection;
        }
    }
}