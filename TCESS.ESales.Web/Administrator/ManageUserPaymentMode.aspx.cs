#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces.Users;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.DataTransferObjects.Users;
using System.Linq;

#endregion

public partial class Administrator_ManageUserPaymentMode : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckIsUserAuthenticated();

        if (!IsPostBack)
        {
            ////Load payment mode details
            LoadPaymentModeDetails();

            ////Load user details from membership provider
            LoadUsers();
        }
    }

    protected IEnumerable grdUserPaymentMode_MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<PaymentModeDTO>();
    }

    protected void grdUserPaymentMode_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdUserPaymentMode.PageIndex = e.NewPageIndex;

        ////Load payment mode details
        LoadPaymentModeDetails();
    }

    protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUsers.SelectedIndex > 0)
        {            
            //Get pages with selected role
            GetPaymentModesByUserId();
        }
    }    

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!CheckSelectedGridRows())
        {
            customValidator.IsValid = false;
            ucMessageBox.ShowMessage(ErrorMessages.NOPAYMENTMODESELECTED);
            return;
        }

        if (Page.IsValid)
        {
            ESalesUnityContainer.Container.Resolve<IUserPaymentModeService>().DeleteUserPaymentModeDetails(GetUserIdByUserName(ddlUsers.SelectedValue));

            IList<UserPaymentModeMappingDTO> lstUserPaymentModeMap = new List<UserPaymentModeMappingDTO>();

            //Iterate through the Products.Rows property
            foreach (GridViewRow row in grdUserPaymentMode.Rows)
            {
                UserPaymentModeMappingDTO userPaymentModeMapDTO = new UserPaymentModeMappingDTO();

                //Access the CheckBox
                CheckBox chkBox = (CheckBox)row.FindControl("chkSelectPaymentMode");
                if (chkBox.Checked)
                {
                    userPaymentModeMapDTO.UPM_UserId = GetUserIdByUserName(ddlUsers.SelectedValue);
                    userPaymentModeMapDTO.UPM_PaymentMode = Convert.ToInt32(grdUserPaymentMode.DataKeys[row.RowIndex]["PaymentMode_Id"]);
                    userPaymentModeMapDTO.UPM_CreatedBy = base.GetCurrentUserId();

                    lstUserPaymentModeMap.Add(userPaymentModeMapDTO);
                }
            }

            ESalesUnityContainer.Container.Resolve<IUserPaymentModeService>().SaveUserPaymentModeDetails(lstUserPaymentModeMap);

            //Shows message box to user
            ucMessageBox.ShowMessage(Messages.PAYMENTMODEMAPPINGSAVED);
                        
            GetPaymentModesByUserId();
        }
    }

    /// <summary>
    /// Get pages with selected role
    /// </summary>
    /// <param name="roleName">String: role name</param>
    private void GetPaymentModesByUserId()
    {
        //Get pages in role from database
        IList<int> lstPaymentModes = ESalesUnityContainer.Container.Resolve<IUserPaymentModeService>()
            .GetUserPaymentModesByUserId(GetUserIdByUserName(ddlUsers.SelectedValue));

        foreach (GridViewRow row in grdUserPaymentMode.Rows)
        {
            int id = Convert.ToInt32(grdUserPaymentMode.DataKeys[row.RowIndex].Value);
            bool payModeExists = (from item in lstPaymentModes where item == id  select true).FirstOrDefault();

            CheckBox chkbox = (CheckBox)row.FindControl("chkSelectPaymentMode");
            chkbox.Checked = false;
            
            if (payModeExists)
            {                
                chkbox.Checked = true;
            }            
        }
    }

    private int GetUserIdByUserName(string userName)
    {
        MembershipUser user = Membership.GetUser(userName);
        return Convert.ToInt32(user.ProviderUserKey);
    }

    private bool CheckSelectedGridRows()
    {
        bool counter = false;
        
        //Iterate through the Payment Transit grid rows
        foreach (GridViewRow row in grdUserPaymentMode.Rows)
        {
            //Access the CheckBox
            CheckBox chkBox = (CheckBox)row.FindControl("chkSelectPaymentMode");
            if (chkBox.Checked)
            {
                counter = true;
            }
        }
        
        return counter;
    }

    private void LoadUsers()
    {
        ddlUsers.DataSource = Membership.GetAllUsers();
        ddlUsers.DataBind();
        ddlUsers.Items.Insert(0, new ListItem(Labels.SELECTUSER, "0"));
    }

    /// <summary>
    /// Load payment mode details
    /// </summary>
    private void LoadPaymentModeDetails()
    {
        IList<PaymentModeDTO> lstPaymentMode = MasterList.GetListOfPaymentMode(true);

        if (lstPaymentMode.Count > 0)
        {
            grdUserPaymentMode.DataSource = lstPaymentMode;
            grdUserPaymentMode.DataBind();
        }
        else
        {
            base.ShowBlankRowInGrid<PaymentModeDTO>(grdUserPaymentMode);
        }
    }
}