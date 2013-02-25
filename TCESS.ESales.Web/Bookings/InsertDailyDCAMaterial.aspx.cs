#region Namespace

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

using System.Linq;
#endregion

public partial class Bookings_InsertDailyDCAMaterial : BasePage
{
	/// <summary>
	/// Page Load event
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getDCAData();
        }
    }

    private void getDCAData()
    {
        IList<DcaMaterialAllocationDTO> listAllMaterial = ESalesUnityContainer.Container.Resolve<IDcaMaterialAllocationService>()
                         .GetAllMaterialAllocationDetails(0, DateTime.Now.Date);
        if (listAllMaterial.Count > 0)
        {
            grdManageCounter.DataSource = listAllMaterial;
            grdManageCounter.DataBind();
        }
        else
        {
            FillBlankGrid();
        }

    }
    private void FillBlankGrid()
    {
        base.ShowBlankRowInGrid<DcaMaterialAllocationDTO>(grdManageCounter);
    }
	/// <summary>
	/// To load the material allocation list
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void Button1_Click(object sender, EventArgs e)
	{
		//SmsUtility.UpdatePreviousDayaActualPercenatge();
		SmsUtility.UpdateDCAPercentage();
        getDCAData();
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
		//            .GetMaterialAllocationDetails(Material.MaterialType_Id,DateTime.Now.Date.AddDays(-1));
		//            DCAMaterialAllocation.DCAMA_TodayPercentage = (from F in lstAgentMaterialPercentageDTO
		//                                                            where F.AMP_MaterialType_Id == Material.MaterialType_Id
		//                                                            select F.AMP_Percentage).FirstOrDefault()
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