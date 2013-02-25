#region Using directives

using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.PersistenceLayer.Entity;
using TCESS.ESales.PersistenceLayer.Interfaces;

#endregion

namespace TCESS.ESales.BusinessLayer.Services
{
    public class CustAuthorizationService : ICustAuthorizationService
    {
        /// <summary>
        /// Get customer authorization details by cutomer id
        /// </summary>
        /// <param name="customerId">Int32: customer id</param>
        /// <returns>returns customer authorization details object</returns>
        public CustAuthorizationDetailDTO GetCustomerAuthorizationDetails(int customerId)
        {
            CustAuthorizationDetailDTO custAuthDetails = new CustAuthorizationDetailDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<custauthorizationdetail>>()
            .GetSingle(item => item.CustAuth_CustId == customerId), custAuthDetails);
            return custAuthDetails;
        }

        /// <summary>
        /// Save And Update Customer Authorization Details
        /// </summary>
        /// <param name="custAuthorizationDetail"></param>
        public void SaveAndUpdateCustomerAuthorizationDetails(CustAuthorizationDetailDTO custAuthorizationDetail)
        {
            custauthorizationdetail custAuthorizationEntity = new custauthorizationdetail();
            AutoMapper.Mapper.Map(custAuthorizationDetail, custAuthorizationEntity);

            if (custAuthorizationDetail.CustAuth_Id == 0)
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<custauthorizationdetail>>()
                    .Save(custAuthorizationEntity);
            }
            else
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<custauthorizationdetail>>()
                    .Update(custAuthorizationEntity);
            }
        }
    }
}