using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCESS.ESales.PersistenceLayer.Entity;

namespace TCESS.ESales.PersistenceLayer.Interfaces
{
    public interface ICustomerMaterialMapRepository
    {
        /// <summary>
        /// Get customer material details by Customer Id
        /// </summary>        
        /// <param name="customerId">Int: Customer Id</param>
        /// <returns>The list of the customer material details</returns>
        IList<customermaterialmap> GetCustomerMaterialDetails(int customerId);
    }
}