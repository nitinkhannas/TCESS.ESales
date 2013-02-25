using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using Microsoft.Practices.Unity;
using System.Web.Security;
using TCESS.ESales.CommonLayer.CommonLibrary;

public partial class Bookings_UserControls_TotalMoneyReciepts : BaseUserControl
{
    public event ShowDataByIdEventHandler Event_ShowMoneyReceipt;
    /// <summary>
    /// For page load To Get total money Reciepts
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAllMoneyReceipts();
            GetTotalCount();
          
        }
    }
    public void GetTotalCount()
    {
        int UserId = Convert.ToInt32(Membership.GetUser().ProviderUserKey);
        IList<object> dayCollection = ESalesUnityContainer.Container.Resolve<IMoneyReceiptService>().GetMoneyRecepitCount(UserId);
       lblCount.Text = dayCollection[0].ToString();
       lblTotalAmount.Text =  dayCollection[1].ToString();
    }
   
    /// <summary>
    /// Method for Get All Money Receipts
    /// </summary>
    public void GetAllMoneyReceipts()
    {
        int counterId = ESalesUnityContainer.Container.Resolve<ICounterService>()
            .GetCounterDetailsByUserId(Convert.ToInt32(Membership.GetUser().ProviderUserKey));
        
        IList<BookingDTO> lstTotalMoneyReciepts = ESalesUnityContainer.Container.Resolve<IBookingService>()
               .GetCounterWiseAcceptedBookingsForAgent(base.GetAgentByUserId().UAM_Agent_Id, counterId);

        if (lstTotalMoneyReciepts.Count > 0)
        {
            grdTotalMoneyReceipts.DataSource = lstTotalMoneyReciepts;
            grdTotalMoneyReceipts.DataBind();
        }
        else
        {
            FillBlankGrid();
        }
    }

    /// <summary>
    /// Gets AgentId by currently logged in UserId
    /// </summary>
    /// <param name="userId">currently logged in UserId</param>
    /// <returns>returns AgentId</returns>
    private UserAgentMappingDTO GetAgentByUserId(int userId)
    {
        //Gets AgentId by UserId and return the value
        return MasterList.GetAgentByUserId(userId);
    }

    /// <summary>
    /// Get Fill Blank Grid 
    /// </summary>
    private void FillBlankGrid()
    {
        ShowBlankRowInGrid<BookingDTO>(grdTotalMoneyReceipts);
    }
    /// <summary>
    /// Event for Loading Issue money reciept and Refresh button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdTotalMoneyReceipts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == Globals.GridCommandEvents.ISSUEMONEYRECEIPT)
        {
            Event_ShowMoneyReceipt(Convert.ToInt32(e.CommandArgument));
        }
        else if (e.CommandName == Globals.GridCommandEvents.REFRESH)
        {
            GetAllMoneyReceipts();
        }
    }
}