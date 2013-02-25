using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Exception;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

public partial class Masters_Form27CPeriodType : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPeriodType();
        }
        PopulateForm27PeriodTypeHistory();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SavePeriodType();
    }

    private void SavePeriodType()
    {
        Form27PeriodTypeDTO form27CPeriodDetail = new Form27PeriodTypeDTO();
        form27CPeriodDetail = ESalesUnityContainer.Container.Resolve<IForm27CService>().GetForm27PeriodType();

        if (form27CPeriodDetail.form27cPeriodType_Id > 0)
        {         
            form27CPeriodDetail.ModifiedDate = DateTime.Now;
            form27CPeriodDetail.ModifiedBy = GetCurrentUserId();            
            form27CPeriodDetail.PeriodType = "0";

            ESalesUnityContainer.Container.Resolve<IForm27CService>().UpdateForm27PeriodType(form27CPeriodDetail);

            Form27PeriodTypeDTO newForm27CPeriodDetail = new Form27PeriodTypeDTO();
            newForm27CPeriodDetail.CreatedBy = GetCurrentUserId();
            newForm27CPeriodDetail.CreatedDate = DateTime.Now;            
            newForm27CPeriodDetail.PeriodType = "1";
            newForm27CPeriodDetail.PeriodTypeId = Convert.ToInt32(ddlPeriodType.SelectedValue);
           
            int result = ESalesUnityContainer.Container.Resolve<IForm27CService>().SaveForm27PeriodType(newForm27CPeriodDetail);

            if (result > 0)
            {
                ucMessageBox.ShowMessage("Saved Successfully.");
            }
            else
            {
                ucMessageBox.ShowMessage("Not saved.");
            }
        }
        else
        {
            Form27PeriodTypeDTO form27CPeriodDetails = new Form27PeriodTypeDTO();
            form27CPeriodDetails.CreatedDate = DateTime.Now;
            form27CPeriodDetails.CreatedBy = GetCurrentUserId();
            form27CPeriodDetails.PeriodTypeId = Convert.ToInt32(ddlPeriodType.SelectedValue);
            form27CPeriodDetails.PeriodType = "1";

            int result = ESalesUnityContainer.Container.Resolve<IForm27CService>().SaveForm27PeriodType(form27CPeriodDetails);

            if (result > 0)
            {
                ucMessageBox.ShowMessage("Saved Successfully.");
            }
            else
            {
                ucMessageBox.ShowMessage("Not saved.");
            }
        }
    }

    private void BindPeriodType()
    {
        List<PeriodTypeDTO> lstPeriodType = ESalesUnityContainer.Container.Resolve<IForm27CService>().GetPeriodList().ToList();

        ddlPeriodType.DataTextField = "PeriodTypeName";
        ddlPeriodType.DataValueField = "PeriodType_Id";
        ddlPeriodType.DataSource = lstPeriodType;
        ddlPeriodType.DataBind();
        ddlPeriodType.Items.Insert(0, "Select");
    }

    public string PeriodType(string  strIsBlocked)
    {
        string value = string.Empty; ;
        
        if (strIsBlocked=="1")
        {
            return value= "Monthly";
        }
        if (strIsBlocked == "2")
        {
            return value= "Quarterly";
        }
        if (strIsBlocked == "3")
        {
            return value="Half Yearly";
        }
        if (strIsBlocked == "4")
        {
            return value= "Annualy";
        }
        return value;
    }

    public string Status(string strIsBlocked)
    {
        string value = string.Empty; ;

        if (strIsBlocked == "0")
        {
            return value = "InActive";
        }
        if (strIsBlocked == "1")
        {
            return value = "Active";
        }
        return value;
   
    }

    private void PopulateForm27PeriodTypeHistory()
    {
        IList<Form27PeriodTypeDTO> lstForm27CPeriodType = ESalesUnityContainer.Container.Resolve<IForm27CService>()
            .GetForm27PeriodTypeDTOList();

        if (lstForm27CPeriodType.Count > 0)
        {
            lblHistory.Visible = true;
            IList<Form27PeriodTypeDTO> lastFiveItem = lstForm27CPeriodType.OrderByDescending(k => k.form27cPeriodType_Id).Take(5).ToList();
            gridHistory.DataSource = lastFiveItem;
            gridHistory.DataBind();
        }
    }

}