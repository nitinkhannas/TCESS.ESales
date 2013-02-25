using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCESS.ESales.PersistenceLayer.Entity;

namespace TCESS.ESales.PersistenceLayer.Interfaces
{
    public interface ICounterRepository
    {
        /// <summary>
        /// Get counter details
        /// </summary>
        /// <returns>Returns list of counters</returns>
        IList<counter> GetCounterDetails();
        IList<counter> GetCounters(int counterId);
    }
}