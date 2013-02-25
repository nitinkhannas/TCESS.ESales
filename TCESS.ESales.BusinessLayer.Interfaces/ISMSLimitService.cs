using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCESS.ESales.DataTransferObjects;

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface ISMSLimitService
    {
        int SaveSMSLimit(SMSLimitDTO SMSLimitDetails);
        IList<SMSLimitDTO> GetSMSLimitList(DateTime smsLimitDate);
    }
}