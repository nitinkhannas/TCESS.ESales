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
    public class AffidavitDetailsService : IAffidavitDetails
    {
        public int SaveAffidavitDetails(AffidavitDetailsDTO affidavitdetail)
        {
            affidavitdetail affidavitdetailEntity = new affidavitdetail();
            using (TransactionScope transactionScope = new TransactionScope())
            {
                AutoMapper.Mapper.Map(affidavitdetail, affidavitdetailEntity);
                if (affidavitdetail.AffidavitDetailsId == 0)
                {
                    ESalesUnityContainer.Container.Resolve<IGenericRepository<affidavitdetail>>().Save(affidavitdetailEntity);
                }
                transactionScope.Complete();
            }
            return affidavitdetailEntity.AffidavitDetailsId;
        }

        public AffidavitDetailsDTO GetAffidavitDetailsByCustId(int CustId)
        {
            AffidavitDetailsDTO affidavitDetails = new AffidavitDetailsDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<affidavitdetail>>()
                .GetSingle(item => item.Affidavit_CustID == CustId), affidavitDetails);

            return affidavitDetails;
        }

        public IList<AffidavitDetailsDTO> GetAffidavitDetailsByCustIdList(int CustId)
        {
            List<AffidavitDetailsDTO> lstAffidavitDetails = new List<AffidavitDetailsDTO>();
            List<affidavitdetail> AffidavitDetailsEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<affidavitdetail>>()
                .GetQuery().Where(data => data.Affidavit_CustID == CustId).ToList();

            AutoMapper.Mapper.Map(AffidavitDetailsEntity, lstAffidavitDetails);

            return lstAffidavitDetails;
        }

        public int UpdateAffidavitDetails(AffidavitDetailsDTO AffidavitDetails)
        {
            affidavitdetail affidavitDetailsEntity = new affidavitdetail();
            int affidavitDetailsId;
            using (TransactionScope transactionScope = new TransactionScope())
            {
                AutoMapper.Mapper.Map(AffidavitDetails, affidavitDetailsEntity);
                ESalesUnityContainer.Container.Resolve<IGenericRepository<affidavitdetail>>().Update(affidavitDetailsEntity);
                affidavitDetailsId = AffidavitDetails.AffidavitDetailsId;
                
                transactionScope.Complete();
            }

            return affidavitDetailsId;
        }
    }
}