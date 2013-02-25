using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCESS.ESales.PersistenceLayer.Interfaces;
using TCESS.ESales.PersistenceLayer.Entity;
using TCESS.ESales.PersistenceLayer.EF;
using System.Data.Objects;

namespace TCESS.ESales.PersistenceLayer.Repository
{
    public class CustomerRepository : GenericRepository<customer>, ICustomerRepository
    {
        /// <summary>
        /// Get customer details by Customer Id
        /// </summary>
        /// <param name="customerId">int: Customer Id</param>
        /// <returns>returns customer details</returns>
        public customer GetCustomerDetails(int customerId)
        {
            eSalesEntities entityType = (eSalesEntities)base.ObjectContext;
            customer customerEntity = (from cust in entityType.customers.Include("ameblock").Include("businesstype")
                                       .Include("ownershipstatu").Include("district").Include("state")
                                       where cust.Cust_Id == customerId
                                       select cust).FirstOrDefault();

            //return the value
            return customerEntity;
        }
    }
}