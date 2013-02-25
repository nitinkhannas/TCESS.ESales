#region Using directives

using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.PersistenceLayer.Entity;
using TCESS.ESales.PersistenceLayer.Interfaces;

#endregion

namespace TCESS.ESales.BusinessLayer.Services
{
    public class Form27C_HistoryService : IForm27C_HistoryService
    {
        public int SaveForm27CHistory(Form27C_HistoryDTO form27CHistoryDetails)
        {
            form27c_history form27CHistoryEntity = new form27c_history();
            using (TransactionScope transactionScope = new TransactionScope())
            {
                AutoMapper.Mapper.Map(form27CHistoryDetails, form27CHistoryEntity);
                if (form27CHistoryDetails.Form27c_History_Id == 0)
                {
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<form27c_history>>().Save(form27CHistoryEntity);
                }
                transactionScope.Complete();
            }
            return form27CHistoryEntity.Form27c_History_Id;
        }

        public Form27C_HistoryDTO GetForm27CHistoryDetailsByCustId(int CustId)
        {
            Form27C_HistoryDTO Form27CHistoryDetails = new Form27C_HistoryDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<form27c_history>>()
                .GetSingle(item => item.Cust_Id == CustId), Form27CHistoryDetails);

            return Form27CHistoryDetails;
        }

        public IList<Form27C_HistoryDTO> GetForm27CHistoryDetailsByCustIdList(int CustId)
        {
            List<Form27C_HistoryDTO> lstForm27CHistory = new List<Form27C_HistoryDTO>();
            List<form27c_history> Form27CEntityHistory = ESalesUnityContainer.Container.Resolve<IGenericRepository<form27c_history>>()
                .GetQuery().Where(data => data.Cust_Id == CustId).ToList();

            AutoMapper.Mapper.Map(Form27CEntityHistory, lstForm27CHistory);

            return lstForm27CHistory;
        }

        public IList<Form27C_HistoryDTO> GetForm27CList()
        {
            List<Form27C_HistoryDTO> lstForm27CHistory = new List<Form27C_HistoryDTO>();
            List<form27c_history> Form27CEntityHistory = ESalesUnityContainer.Container.Resolve<IGenericRepository<form27c_history>>()
                .GetQuery().Where(data => data.Form27c_History_Id > 0).ToList();

            AutoMapper.Mapper.Map(Form27CEntityHistory, lstForm27CHistory);

            return lstForm27CHistory;
        }
    }
}