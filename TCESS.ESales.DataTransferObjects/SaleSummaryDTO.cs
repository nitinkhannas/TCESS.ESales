using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
   public class SaleSummaryDTO : BaseDTO
    {
        #region Primitive Properties

        public string SaleSummary_Agent_ShortName
        {
            get;
            set;
        }

        public int SaleSummary_TotalNoOfTrcuksBooked
        {
            get;
            set;
        }

        public int SaleSummary_TotalQtyBooked
        {
            get;
            set;
        }

        public int SaleSummary_TotalNoOfTrcuksSale
        {
            get;
            set;
        }
        public decimal SaleSummary_TotalQtySoled
        {
            get;
            set;
        }
        #endregion
    }
}
