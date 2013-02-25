#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCESS.ESales.DataTransferObjects;
#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
    public interface IForm27CService
    {
        int SaveForm27C(Form27CDTO form27C);
        Form27CDTO GetForm27CDetailsByCustId(int custId);
        IList<Form27CDTO> GetForm27CDetailsByCustIdList(int custId);
        IList<Form27CDTO> GetForm27CList();
        int UpdateForm27C(Form27CDTO form27CDetails);
        IList<PeriodTypeDTO> GetPeriodList();
        int SaveForm27PeriodType(Form27PeriodTypeDTO form27CPeriodType);        
        int UpdateForm27PeriodType(Form27PeriodTypeDTO form27CPeriodTypeDetails);
        Boolean UpdateForm27PeriodTypeList(List<Form27PeriodTypeDTO> lstForm27CPeriodType);
        Form27PeriodTypeDTO GetForm27PeriodType();
        IList<Form27PeriodTypeDTO> GetForm27PeriodTypeDTOList();
        IList<MonthsDTO> GetMonthList();        
        IList<Form27CDTO> GetForm27CListToActivate();
        Form27CDTO GetForm27CDetailsByForm27CId(int form27CId);
    }
}
