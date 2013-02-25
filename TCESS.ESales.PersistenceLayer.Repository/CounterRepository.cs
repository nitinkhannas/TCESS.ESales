using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCESS.ESales.PersistenceLayer.Interfaces;
using TCESS.ESales.PersistenceLayer.Entity;
using TCESS.ESales.PersistenceLayer.EF;

namespace TCESS.ESales.PersistenceLayer.Repository
{
    public class CounterRepository : GenericRepository<counter>, ICounterRepository
    {
        /// <summary>
        /// Get counter details
        /// </summary>
        /// <returns>returns list of counter details</returns>
        public IList<counter> GetCounterDetails()
        {
            eSalesEntities entityType = (eSalesEntities)base.ObjectContext;
            List<counter> lstCounter = (from counters in entityType.counters.Include("agentdetail")
                                            .Where(item => item.Counter_IsDeleted == false) select counters).ToList();

            //return the value
            return lstCounter;
        }

        /// <summary>
        /// Get counter by counter Id
        /// </summary>
        /// <param name="counterId"></param>
        /// <returns>returns counter detail</returns>
        public IList<counter> GetCounters(int counterId)
        {
            eSalesEntities entityType = (eSalesEntities)base.ObjectContext;
            List<counter> lstCounter = new List<counter>();
            if (counterId > 0)
            {
                var result = from counters in entityType.counters.Where(item => item.Counter_IsDeleted == false && item.Counter_Id != counterId) select counters;
                lstCounter = result.ToList();
            }
            else
            {
                var result = from counters in entityType.counters.Where(item => item.Counter_IsDeleted == false) select counters;
                lstCounter = result.ToList();
            }
            return lstCounter;
        }
    }
}