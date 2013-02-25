#region Namespace
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using System.Collections.Generic;
using System.Web.UI.DataVisualization.Charting;
using System.Data;
#endregion

public partial class Supervisor_TestChartaspx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DrawChart();
        }
    }

    private void DrawChart()
    {
        IList<MaterialTypeDTO> LstMaterialTypeDTO = ESalesUnityContainer.Container.Resolve<IMaterialTypeService>().GetMaterialTypeList(false);
        IList<AgentDTO> lstAgMatPercentage = ESalesUnityContainer.Container.Resolve<IAgentService>().GetAgentList();
        string[] sdf = { "sdf", "gd", "dfgdf", "sfs", "rty" };
        for (int i = 0; i <4; i++)
        {
            Series s = new Series();
            s.ChartType = SeriesChartType.Column;
            for (int j = 0; j < 6; j++)
            {
                //try
                //{
                    //AgentMaterialPercentageDTO objAgentMaterialPercentageDTO = ESalesUnityContainer.Container.Resolve<IAgentService>().GetAgentMaterialPercentageByIds
                    //    (Convert.ToInt32(lstAgMatPercentage[j].Agent_Id), Convert.ToInt32(LstMaterialTypeDTO[i].MaterialType_Id));
                    //s.Points.AddXY(lstAgMatPercentage[j].Agent_Name, objAgentMaterialPercentageDTO.AMP_Percentage);
                //}
                //catch
                //{
                //}

                s.Points.AddXY(i, j);
            }
           
            SalesChart.Series.Add(s);
        }
        
        //MaterialTypeDTO ddsf=new MaterialTypeDTO();


   
        

        

      

        //SalesChart.DataSource = lstAgMatPercentage;

        //SalesChart.Series[0].LegendText = SalesChart.Series[0].Name;
        //SalesChart.Series[0].XValueMember = "AgentName";
        //SalesChart.Series[0].YValueMembers = "MaterialTypeName";
        
        //SalesChart.Series[0].Label = "#PERCENT{P1}";
      
        //SalesChart.DataBindCrossTable(lstAgMatPercentage, "AgentName", "AgentName", "AMP_Percentage", null);


        ////SalesChart.DataBind();
        ////SalesChart.ChartAreas["Area1"].AxisY.Interval = 1;

        //foreach (Series s in SalesChart.Series)
        //{
        //    s.ChartType = SeriesChartType.StackedColumn;
        //    s.IsValueShownAsLabel = true;
        //    //s.IsXValueIndexed = true;
        //}

        //SalesChart.ChartAreas[0].AxisX.Title = "AgentName";
        //SalesChart.ChartAreas[0].AxisX.Interval = 1;
        //SalesChart.ChartAreas[0].AxisY.Title = "AMP_Percentage";

    }




    protected void btn_Click(object sender, EventArgs e)
    {

    }
    protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl.SelectedValue == "0")
        {
            string[] df = { "A", "AA", "AAA", "AAAA" };
            gv.DataSource = df;
            gv.DataBind();
        }
        else
        {
            string[] df = { "B", "AB", "ABB", "BBBB" };
            gv.DataSource = df;
            gv.DataBind();
        }
    }
}