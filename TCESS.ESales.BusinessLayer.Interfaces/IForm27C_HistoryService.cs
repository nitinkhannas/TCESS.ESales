using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCESS.ESales.DataTransferObjects;

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface IForm27C_HistoryService
    {
        int SaveForm27CHistory(Form27C_HistoryDTO form27CHistory);
        Form27C_HistoryDTO GetForm27CHistoryDetailsByCustId(int CustId);
        IList<Form27C_HistoryDTO> GetForm27CHistoryDetailsByCustIdList(int CustId);
        IList<Form27C_HistoryDTO> GetForm27CList();
    }
}
