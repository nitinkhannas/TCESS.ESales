#region Namespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCESS.ESales.CommonLayer.Reports;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using Microsoft.Practices.Unity;
#endregion

public partial class DataUpdation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		CurrencyConvertor currenyInWords = new CurrencyConvertor();
		string words=currenyInWords.Convertor(Convert.ToString("2159.41"));
				//IList<DcaMaterialAllocationDTO> listMaterialAllocations = new List<DcaMaterialAllocationDTO>();

		//IList<MaterialTypeDTO> lstMaterialTypeDTO = ESalesUnityContainer.Container.Resolve<IMaterialTypeService>().GetMaterialTypeList(true);
		//foreach (MaterialTypeDTO Material in lstMaterialTypeDTO)
		//{
		//    IList<AgentDTO> lstAgentDTO = ESalesUnityContainer.Container.Resolve<IAgentService>().GetAgentList();

		//    if (lstAgentDTO.Count > 0)
		//    {
		//        foreach (AgentDTO item in lstAgentDTO)
		//        {
		//            DcaMaterialAllocationDTO DCAMaterialAllocation = new DcaMaterialAllocationDTO();

		//            DCAMaterialAllocation.DCAMA_Date = System.DateTime.Now.Date.AddDays(0);
		//            DCAMaterialAllocation.DCAMA_Agent_Id = item.Agent_Id;
		//            DCAMaterialAllocation.DCAMA_MaterialType_Id = Material.MaterialType_Id;

		//            IList<AgentMaterialPercentageDTO> lstAgentMaterialPercentageDTO = ESalesUnityContainer.Container
		//              .Resolve<IAgentMaterialPercentageService>().GetAgentMaterialPercentByAgentId(item.Agent_Id);

		//            IList<DcaMaterialAllocationDTO> listMaterial = ESalesUnityContainer.Container.Resolve<IDcaMaterialAllocationService>()
		//            .GetMaterialAllocationDetails(Material.MaterialType_Id, DateTime.Now.Date.AddDays(-1));
		//            DCAMaterialAllocation.DCAMA_TodayPercentage = (from F in lstAgentMaterialPercentageDTO
		//                                                           where F.AMP_MaterialType_Id == Material.MaterialType_Id
		//                                                           select F.AMP_Percentage).FirstOrDefault()
		//                                                            +
		//                                                            (from F in listMaterial
		//                                                             where F.DCAMA_Agent_Id == item.Agent_Id
		//                                                             select F.DCAMA_CurrentVariance).FirstOrDefault()
		//                                                            ;
		//            listMaterialAllocations.Add(DCAMaterialAllocation);
		//        }
		//    }
		//}

		//ESalesUnityContainer.Container.Resolve<IDcaMaterialAllocationService>()
		//                    .SaveAndUpdateDCAMaterialDetails(listMaterialAllocations);
    }
}