#region Using directives

using System.Collections.Generic;
using TCESS.ESales.DataTransferObjects;

#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface IAmeBlockService
    {
        int SaveAndUpdateAmeBlock(AmeBlockDTO ameBlockDetail);
        AmeBlockDTO GetAmeBlockListByBlockId(int blockId);        
        int UpdateCustomerAmeBlock(CustomerDTO customerDetails);
        bool AmeBlockExists(int blockId, string blockName);

        IList<AmeBlockDTO> GetAmeBlockList();

        IList<CustomerDTO> GetCustomerAssociatedForAmeBlock(int blockId);
    }
}