using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCESS.ESales.PersistenceLayer.Entity;

namespace TCESS.ESales.PersistenceLayer.Interfaces
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Get customer details by Customer Id
        /// </summary>
        /// <returns>List of customers</returns>
        customer GetCustomerDetails(int customerId);
    }
}