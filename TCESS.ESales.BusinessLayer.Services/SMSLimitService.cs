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
    public class SMSLimitService : ISMSLimitService
    {
        public int SaveSMSLimit(SMSLimitDTO SMSLimitDetail)
        {
            smsbookinglimit smslimitEntity = new smsbookinglimit();
            AutoMapper.Mapper.Map(SMSLimitDetail, smslimitEntity);

            if (SMSLimitDetail.SMSLimit_Id == 0)
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<smsbookinglimit>>().Save(smslimitEntity);
                    IList<SMSLimitDTO> lstSMSLimitDTO = ESalesUnityContainer.Container.Resolve<ISMSLimitService>().GetSMSLimitList(DateTime.Now.Date.AddDays(0));

                    foreach (SMSLimitDTO smsLimitDTO in lstSMSLimitDTO)
                    {
                        if (smsLimitDTO.SMSLimit_Id != smslimitEntity.SMSLimit_Id)
                        {
                            smsbookinglimit upSmsLimitEntity = new smsbookinglimit();
                            smsLimitDTO.SMSLimit_IsActive = false;
                            AutoMapper.Mapper.Map(smsLimitDTO, upSmsLimitEntity);
                            ESalesUnityContainer.Container.Resolve<IGenericRepository<smsbookinglimit>>().Update(upSmsLimitEntity);
                        }
                    }
                    transactionScope.Complete();
                }
            }
            return smslimitEntity.SMSLimit_Id;
        }

        public IList<SMSLimitDTO> GetSMSLimitList(DateTime smsLimitDate)
        {
            List<SMSLimitDTO> lstsmslimits = new List<SMSLimitDTO>();
            //Getting current date
            DateTime crDate = smsLimitDate.Date;

            List<smsbookinglimit> smslimitEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<smsbookinglimit>>()
                .GetQuery().Where(date => date.SMSLimit_Date == crDate)
                                .OrderByDescending(limit => limit.SMSLimit_Id).ToList();

            AutoMapper.Mapper.Map(smslimitEntity, lstsmslimits);

            return lstsmslimits;
        }
    }
}