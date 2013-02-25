#region Using directives

using System;
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
    public class Form27CService : IForm27CService
    {
        public int SaveForm27C(Form27CDTO form27CDetails)
        {
            form27c form27CEntity = new form27c();
            form27c_history form27C_historyEntity = new form27c_history();
            using (TransactionScope transactionScope = new TransactionScope())
            {
                AutoMapper.Mapper.Map(form27CDetails, form27CEntity);
                if (form27CDetails.Form27C_Id == 0)
                {
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<form27c>>().Save(form27CEntity);
                }

                AutoMapper.Mapper.Map(form27CDetails, form27C_historyEntity);
                if (form27CDetails.Form27C_Id == 0)
                {
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<form27c_history>>().Save(form27C_historyEntity);
                }
                transactionScope.Complete();
            }
            return form27CEntity.Form27C_Id;
        }

        public Form27CDTO GetForm27CDetailsByCustId(int CustId)
        {
            Form27CDTO Form27CDetails = new Form27CDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<form27c>>()
                .GetSingle(item => item.Cust_Id == CustId), Form27CDetails);

            return Form27CDetails;
        }

        public IList<Form27CDTO> GetForm27CDetailsByCustIdList(int CustId)
        {
            List<Form27CDTO> lstForm27C = new List<Form27CDTO>();
            List<form27c> Form27CEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<form27c>>()
                .GetQuery().Where(data => data.Cust_Id == CustId).ToList();

            AutoMapper.Mapper.Map(Form27CEntity, lstForm27C);

            return lstForm27C;
        }

        public IList<Form27CDTO> GetForm27CList()
        {
            List<Form27CDTO> lstForm27C = new List<Form27CDTO>();
            List<form27c> Form27CEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<form27c>>()
                .GetQuery().ToList();

            AutoMapper.Mapper.Map(Form27CEntity, lstForm27C);

            return lstForm27C;
        }

        public int UpdateForm27C(Form27CDTO Form27CDetails)
        {
            form27c form27cEntity = new form27c();
            form27c_history form27chistoryEntity = new form27c_history();
            int form27cid;
            using (TransactionScope transactionScope = new TransactionScope())
            {
                AutoMapper.Mapper.Map(Form27CDetails, form27cEntity);
                ESalesUnityContainer.Container.Resolve<IGenericRepository<form27c>>().Update(form27cEntity);
                form27cid = Form27CDetails.Form27C_Id;

                //Form27CDetails.Form27C_Id = 0;
                //AutoMapper.Mapper.Map(Form27CDetails, form27chistoryEntity);
                //ESalesUnityContainer.Container.Resolve<IGenericRepository<form27c_history>>().Save(form27chistoryEntity);
                transactionScope.Complete();
            }
            return form27cid;
        }

        public IList<PeriodTypeDTO> GetPeriodList()
        {
            List<PeriodTypeDTO> PeriodTypeList = new List<PeriodTypeDTO>();
            List<periodtype> periodTypeEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<periodtype>>()
                .GetQuery().Where(data => data.PeriodType_Id > 0).ToList();

            AutoMapper.Mapper.Map(periodTypeEntity, PeriodTypeList);
            return PeriodTypeList;
        }

        public IList<MonthsDTO> GetMonthList()
        {
            List<MonthsDTO> monthList = new List<MonthsDTO>();

            List<month> monthEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<month>>()
                .GetQuery().Where(data => data.Months_Id > 0).ToList();

            AutoMapper.Mapper.Map(monthEntity, monthList);
            return monthList;
        }

        public IList<Form27PeriodTypeDTO> GetForm27PeriodTypeDTOList()
        {
            List<Form27PeriodTypeDTO> form27CPeriodTypeList = new List<Form27PeriodTypeDTO>();

            List<form27cperiodtype> form27CPeriodTypeEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<form27cperiodtype>>()
                .GetQuery().Where(data => data.form27cPeriodType_Id > 0).ToList();

            AutoMapper.Mapper.Map(form27CPeriodTypeEntity, form27CPeriodTypeList);
            return form27CPeriodTypeList;
        }

        public Form27PeriodTypeDTO GetForm27PeriodType()
        {
            Form27PeriodTypeDTO Form27PeriodTypeDetails = new Form27PeriodTypeDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<form27cperiodtype>>()
                .GetSingle(item => item.form27cPeriodType_Id > 0 && item.PeriodType == "1"), Form27PeriodTypeDetails);

            return Form27PeriodTypeDetails;
        }

        public int SaveForm27PeriodType(Form27PeriodTypeDTO form27CPeriodType)
        {
            form27cperiodtype form27CPerioTypeEntity = new form27cperiodtype();

            using (TransactionScope transactionScope = new TransactionScope())
            {
                AutoMapper.Mapper.Map(form27CPeriodType, form27CPerioTypeEntity);
                if (form27CPeriodType.form27cPeriodType_Id == 0)
                {
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<form27cperiodtype>>().Save(form27CPerioTypeEntity);
                }
                transactionScope.Complete();
            }
            return form27CPerioTypeEntity.form27cPeriodType_Id;

        }

        public int UpdateForm27PeriodType(Form27PeriodTypeDTO Form27CPeriodTypeDetails)
        {
            form27cperiodtype form27cperiodtypeEntity = new form27cperiodtype();
            int form27cPeriodType_Id;
            using (TransactionScope transactionScope = new TransactionScope())
            {
                AutoMapper.Mapper.Map(Form27CPeriodTypeDetails, form27cperiodtypeEntity);
                ESalesUnityContainer.Container.Resolve<IGenericRepository<form27cperiodtype>>().Update(form27cperiodtypeEntity);
                form27cPeriodType_Id = Form27CPeriodTypeDetails.form27cPeriodType_Id;

                transactionScope.Complete();
            }
            return form27cPeriodType_Id;
        }

        public Boolean UpdateForm27PeriodTypeList(List<Form27PeriodTypeDTO> lstForm27CPeriodType)
        {
            List<form27cperiodtype> lstForm27cperiodtypeEntity = new List<form27cperiodtype>();

            using (TransactionScope transactionScope = new TransactionScope())
            {
                AutoMapper.Mapper.Map(lstForm27CPeriodType, lstForm27cperiodtypeEntity);
                if (lstForm27cperiodtypeEntity.Count() > 0)
                {
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<form27cperiodtype>>().SaveAndUpdateSMSDetailsList<form27cperiodtype>(lstForm27cperiodtypeEntity);
                }

                //if (lstForm27cperiodtypeEntity.Where(x => x.PeriodType == "0") != null)
                //{
                //    ESalesUnityContainer.Container.Resolve<IGenericRepository<form27cperiodtype>>().SaveAndUpdateSMSDetailsList<form27cperiodtype>(lstForm27cperiodtypeEntity);
                //}
                transactionScope.Complete();
            }
            return true;
        }
               
        public IList<Form27CDTO> GetForm27CListToActivate()
        {
            List<Form27CDTO> lstForm27C = new List<Form27CDTO>();
            List<form27c> Form27CEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<form27c>>()
                .GetQuery().Where(x=>x.AcceptedByTSLDate==null).ToList();

            AutoMapper.Mapper.Map(Form27CEntity, lstForm27C);
            return lstForm27C;
        }

        public Form27CDTO GetForm27CDetailsByForm27CId(int Form27CId)
        {
            Form27CDTO Form27CDetails = new Form27CDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<form27c>>()
                .GetSingle(item => item.Form27C_Id == Form27CId), Form27CDetails);

            return Form27CDetails;
        }
    }
}