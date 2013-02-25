#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Bookings_AdvanceBooking : BasePage
{
    /// <summary>
    /// page laod event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState[Globals.StateMgmtVariables.AGENTID] = null;
            PopulateAdvanceBooking();
        }
    }
    /// <summary>
    /// Method to Populate Advance booking
    /// </summary>
    private void PopulateAdvanceBooking()
    {
        IList<BookingDTO> lstAdvanceBooking = ESalesUnityContainer.Container.Resolve<IBookingService>()
            .GetTodaysAdvanceBooking(DateTime.Now);

        if (lstAdvanceBooking.Count > 0)
        {
            grdBooking.DataSource = lstAdvanceBooking;
            grdBooking.DataBind();
        }
        else
        {
            ShowBlankRowInGrid<BookingDTO>(grdBooking);
        }
    }
    /// <summary>
    /// Event for on row edit event of grid 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdBooking_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == Globals.GridCommandEvents.EDITBOOKING)
        {
            AcceptSave(Convert.ToInt32(e.CommandArgument));
            PopulateAdvanceBooking();
        }
    }
    /// <summary>
    /// Event for Accept Save by booking Id
    /// </summary>
    /// <param name="bookingId"></param>
    private void AcceptSave(int bookingId)
    {
        string counterId = string.Empty;
        int agentId = 0;
        BookingDTO bookingDetails = new BookingDTO();

        bookingDetails = ESalesUnityContainer.Container.Resolve<IBookingService>().GetBookingDetailByBookingId(bookingId, false);
        CustomerDTO customer = ESalesUnityContainer.Container.Resolve<ICustomerService>()
            .GetCustomerDetailsById(bookingDetails.Booking_Cust_Id);
		int smsRegId = 0;
        
        ViewState[Globals.StateMgmtVariables.AGENTID] = customer.Cust_AgentId;
        
        IList<DcaMaterialAllocationDTO> lstMaterialAllocations = ESalesUnityContainer.Container
            .Resolve<IDcaMaterialAllocationService>()
            .GetMaterialAllocationDetails(bookingDetails.Booking_MaterialType_Id, DateTime.Now.Date);

        if (lstMaterialAllocations.Count > 0)
        {
            if (ViewState[Globals.StateMgmtVariables.AGENTID] == null)
            {
                agentId = (from F in lstMaterialAllocations where F.DCAMA_AllocatedQty == 0 
                           select F.DCAMA_Agent_Id).FirstOrDefault();

                if (agentId == 0)
                {
                    agentId = (from F in lstMaterialAllocations orderby F.DCAMA_CurrentVariance descending 
                               select F.DCAMA_Agent_Id).FirstOrDefault();
                }
            }
            else
            {
                agentId = (from F in lstMaterialAllocations 
                           where F.DCAMA_Agent_Id == Convert.ToInt32(ViewState[Globals.StateMgmtVariables.AGENTID])
                           select F.DCAMA_Agent_Id).FirstOrDefault();
            }

            if (agentId != 0)
            {
                int qtySum = (from F in lstMaterialAllocations
                              select F.DCAMA_AllocatedQty).Sum() + Convert.ToInt32(bookingDetails.Booking_Qty);

                foreach (DcaMaterialAllocationDTO item in lstMaterialAllocations)
                {
                    if (item.DCAMA_Agent_Id == agentId)
                    {
                        item.DCAMA_AllocatedQty += Convert.ToInt32(bookingDetails.Booking_Qty);
                        item.DCAMA_LastQty = Convert.ToInt32(bookingDetails.Booking_Qty);
                    }
                    item.DCAMA_CurrentPercentage = (Convert.ToDecimal(item.DCAMA_AllocatedQty) / Convert.ToDecimal(qtySum)) * 100;
                    item.DCAMA_CurrentVariance = item.DCAMA_TodayPercentage - item.DCAMA_CurrentPercentage;
                }

                bookingDetails.Booking_Agent_Id = agentId;
                bookingDetails.Booking_IsAdvanced = false;
                counterId = ESalesUnityContainer.Container.Resolve<IBookingService>()
					.SaveAllBookingInfo(lstMaterialAllocations, bookingDetails, GetCounterID(agentId), smsRegId);

                CounterDTO counterDetails = ESalesUnityContainer.Container.Resolve<ICounterService>().GetCounterDetailsById(Convert.ToInt32(counterId));

                lblCounterNo.Visible = true;
                lblCounterNo.Text = Messages.GoToCounterNo + counterDetails.Counter_Name;
            }
            else
            {
                ucMessageBox.ShowMessage(Messages.AgentNotActive);
            }
        }
        else
        {
            ucMessageBox.ShowMessage(Messages.AgentNotActive);
        }
    }
    /// <summary>
    /// Method to Get Counter Id by agentId
    /// </summary>
    /// <param name="agentID">Int32:agentID</param>
    /// <returns></returns>
    private CounterDetailsDTO GetCounterID(int agentID)
    {
        IList<CounterDetailsDTO> lstcounterDetails = ESalesUnityContainer.Container.Resolve<ICounterService>()
            .GetCounterDailyDetails(agentID);
        CounterDetailsDTO counterDetails = (from item in lstcounterDetails orderby item.CounterDetail_Count ascending 
                                            select item).FirstOrDefault();
        return counterDetails;
    }
}
