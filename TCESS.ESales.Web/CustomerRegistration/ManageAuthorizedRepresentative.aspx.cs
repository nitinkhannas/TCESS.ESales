#region Namespace
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.BusinessLayer.Services;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using System.Linq;
using Resources;
using System.Collections;
using System.IO;
#endregion

public partial class CustomerRegistration_ManageAuthorizedRepresentative : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        //Custom events from Authorized Representative Registration Page
        ucAuthorizedRepresentative.Event_CloseScreen += ucAuthorizedRepresentative_Event_CloseScreen;

        //Custom events from Manage Authorized Representative Page
        ucManageAuthorizedRepresentative.Event_AddAuthRepDetails += ucManageAuthorizedRepresentative_Event_AddAuthRepDetails;
        ucManageAuthorizedRepresentative.Event_EditAuthRepDetails += ucManageAuthorizedRepresentative_Event_EditAuthRepDetails;
    }

	protected void Page_Load(object sender, EventArgs e)
	{
        base.CheckIsUserAuthenticated();

        if (!IsPostBack)
        {
            ShowInitialValues();
        }
	}

    void ucAuthorizedRepresentative_Event_CloseScreen(object sender)
    {
        ShowInitialValues();
    }

    void ucManageAuthorizedRepresentative_Event_AddAuthRepDetails(int customerId, bool isFirstAuthRep, string folderName)
    {
        //Sets visibility of frames that contains user controls
        pnlManageAuthRep.Visible = false;
        pnlAuthRepRegistration.Visible = true;

        ucAuthorizedRepresentative.ShowBlankScreen(customerId, isFirstAuthRep, folderName);
    }

    void ucManageAuthorizedRepresentative_Event_EditAuthRepDetails(int authRepId)
    {
        //Sets visibility of frames that contains user controls       
        pnlManageAuthRep.Visible = false;
        pnlAuthRepRegistration.Visible = true;

        ucAuthorizedRepresentative.PopulateAuthRepData(authRepId);
    }

    /// <summary>
    /// Show Page Values when it initially Loads or Refreshes
    /// </summary>
    private void ShowInitialValues()
    {
        //Sets visibility of frames that contains user controls       
        pnlAuthRepRegistration.Visible = false;
        pnlManageAuthRep.Visible = true;

        ucManageAuthorizedRepresentative.ShowDefaultManageAuthRepScreen();
    }
}