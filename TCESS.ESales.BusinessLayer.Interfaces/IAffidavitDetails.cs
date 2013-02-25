#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCESS.ESales.DataTransferObjects;
#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface IAffidavitDetails
    {
        int SaveAffidavitDetails(AffidavitDetailsDTO AffidavitDetails); 
        AffidavitDetailsDTO GetAffidavitDetailsByCustId(int CustId);
        IList<AffidavitDetailsDTO> GetAffidavitDetailsByCustIdList(int CustId);
        int UpdateAffidavitDetails(AffidavitDetailsDTO AffidavitDetails);
    }
}
