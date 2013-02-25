using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCESS.ESales.DataTransferObjects;

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface ICustAuthorizationService
    {
        void SaveAndUpdateCustomerAuthorizationDetails(CustAuthorizationDetailDTO custAuthorizationDetail);

        /// <summary>
        /// Get customer authorization details by cutomer id
        /// </summary>
        /// <param name="customerId">Int32: customer id</param>
        /// <returns>returns customer authorization details object</returns>
        CustAuthorizationDetailDTO GetCustomerAuthorizationDetails(int customerId);
    }
}