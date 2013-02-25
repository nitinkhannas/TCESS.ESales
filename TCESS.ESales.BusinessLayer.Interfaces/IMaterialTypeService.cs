#region Using directives

using System.Collections.Generic;
using TCESS.ESales.DataTransferObjects;

#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface IMaterialTypeService
    {
        IList<MaterialTypeDTO> GetMaterialTypeList(bool isActiveOnly);
		IList<MaterialTypeDTO> GetMaterialTypeHistoryList();
        MaterialTypeDTO GetMaterialTypeById(int materialTypeId);
        int SaveMaterialType(MaterialTypeDTO materialTypeDetails);
        int UpdateMaterialType(MaterialTypeDTO materialTypeDetails);
        void DeleteMaterialType(int materialTypeId);
    }
}