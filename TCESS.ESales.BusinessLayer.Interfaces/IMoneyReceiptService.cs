#region Using directives
using System;
using System.Collections.Generic;
using TCESS.ESales.DataTransferObjects;

#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface IMoneyReceiptService
    {
        int SaveAndUpdateMoneyReceipt(MoneyReceiptDTO moneyreceiptDetail);
        IList<MoneyReceiptDTO> GetAllMoneyReceipts(int agentId);
        MoneyReceiptDTO GetMoneyReceiptById(int moneyReceiptId, int bookingId);
        IList<object> GetMoneyRecepitCount(int userId);
       
    }
}