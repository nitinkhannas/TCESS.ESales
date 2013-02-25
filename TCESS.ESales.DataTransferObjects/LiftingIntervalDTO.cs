using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{

    public class LiftingIntervalDTO : BaseDTO
    {
        #region Primitive Properties

        public int liftinginterval_Id
        {
            get;
            set;
        }

        public Nullable<int> liftinginterval_Interval
        {
            get;
            set;
        }

        public Nullable<System.DateTime> liftinginterval_CreatedDate
        {
            get;
            set;
        }

        public Nullable<int> liftinginterval_CreatedBy
        {
            get;
            set;
        }

        public Nullable<System.DateTime> liftinginterval_LastUpdatedDate
        {
            get;
            set;
        }

        #endregion
    }
}
