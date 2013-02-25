#region Using directives

using System.Collections.Generic;
using TCESS.ESales.DataTransferObjects;

#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface ICustomerMaterialService
    {
        void SaveAndUpdateCustomerMaterialDetails(IList<CustomerMaterialMapDTO> listCustomerMaterial);
        CustomerMaterialMapDTO GetCustomerMaterialById(int customerMaterialId);
        void DeleteCustomerMaterials(CustomerMaterialMapDTO customerMaterials);

        IList<CustomerMaterialMapDTO> GetCustomerMaterialDetailsByMaterialId(int materialId);
        IList<CustomerMaterialMapDTO> GetCustomerMaterialDetailsByCustomerId(int customerId);
        IList<CustomerMaterialMapDTO> GetCustomerMaterialDetails(int customerId);
		IList<CustomerMaterialMapDTO> GetCustomerMaterialDetailsByCustomerCode(string customerCode);
        CustomerMaterialMapDTO GetCustomerMaterialByCustomerAndMaterialId(int customerId, int materialTypeId);
        List<CustomerMaterialMapDTO> GetCustomerMaterialDetailsList();
        bool SaveAndUpdateCustomerMaterial(IList<CustomerMaterialMapDTO> listCustomerMaterial);
    }
}