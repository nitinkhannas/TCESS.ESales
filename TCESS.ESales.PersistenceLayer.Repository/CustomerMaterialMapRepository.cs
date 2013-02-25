using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCESS.ESales.PersistenceLayer.Interfaces;
using TCESS.ESales.PersistenceLayer.Entity;
using TCESS.ESales.PersistenceLayer.EF;

namespace TCESS.ESales.PersistenceLayer.Repository
{
    public class CustomerMaterialMapRepository : GenericRepository<customermaterialmap>, ICustomerMaterialMapRepository
    {
        /// <summary>
        /// Get customer material details by Customer Id
        /// </summary>        
        /// <param name="customerId">Int: Customer Id</param>
        /// <returns>The list of the customer material details</returns>
        public IList<customermaterialmap> GetCustomerMaterialDetails(int customerId)
        {
            eSalesEntities entityType = (eSalesEntities)base.ObjectContext;
            IList<customermaterialmap> listCustomerEntity = (from custMatMap in entityType.customermaterialmaps
                                                                 .Include("materialtype") 
                                                             where custMatMap.Cust_Mat_CustId == customerId
                                                             select custMatMap).ToList();

            //return the value
            return listCustomerEntity;
        }
    }
}