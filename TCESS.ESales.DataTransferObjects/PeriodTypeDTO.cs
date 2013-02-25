using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class PeriodTypeDTO : BaseDTO
    {
        #region Primitive Properties

        public virtual int PeriodType_Id
        {
            get;
            set;
        }

        public virtual string PeriodTypeName
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> CreatedDate
        {
            get;
            set;
        }

        public virtual Nullable<int> CreatedBy
        {
            get;
            set;
        }

        #endregion


    }
}
