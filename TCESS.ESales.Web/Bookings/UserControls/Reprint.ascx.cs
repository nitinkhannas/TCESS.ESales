using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using Microsoft.Practices.Unity;
using System.Web.Security;
using TCESS.ESales.CommonLayer.CommonLibrary;

public partial class Bookings_UserControls_Reprint : BaseUserControl
{
    public event ShowDataByIdEventHandler Event_LoadReportForMoneyReceipt;
    public event ShowDataByIdEventHandler Event_LoadReportForHandlingBill;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillBlankGrid();
        }
    }


    /// <summary>
    /// Method for Get Booking detail by booking id
    /// </summary>
    public void GetBookingDetailByID(Int32 bookingID)
    {
        IList<BookingDTO> lstBookingDetail = new List<BookingDTO>();

        BookingDTO bookingDetails = ESalesUnityContainer.Container.Resolve<IBookingService>().GetBookingDetailForReprint(bookingID);
        lstBookingDetail.Add(bookingDetails);

        if (lstBookingDetail.Count > 0)
        {
            grdReprint.DataSource = lstBookingDetail;
            grdReprint.DataBind();
        }
        else
        {
            FillBlankGrid();
        }
    }

    /// <summary>
    /// Get Fill Blank Grid 
    /// </summary>
    private void FillBlankGrid()
    {
        ShowBlankRowInGrid<BookingDTO>(grdReprint);
    }

    protected void btnPopulateGrd_Click(object sender, EventArgs e)
    {
        Int32 _bookingID = Convert.ToInt32(txtBookingID.Text);
        GetBookingDetailByID(_bookingID);
    }

    protected void grdReprint_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == Globals.GridCommandEvents.ISSUEMONEYRECEIPT)
        {
            Event_LoadReportForMoneyReceipt(Convert.ToInt32(e.CommandArgument));
        }
        else if (e.CommandName == Globals.GridCommandEvents.PRINBILL)
        {
            SettlementOfAccountsDTO settlementOfAcct = ESalesUnityContainer.Container.Resolve<ISettlementOfAccountsService>()
           .GetSettlementOfAccountsByBookingId(Convert.ToInt32(e.CommandArgument));
            Event_LoadReportForHandlingBill(settlementOfAcct.Account_Id);
        }
    }
    protected void grdReprint_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbPrintMoney = (LinkButton)e.Row.FindControl("LinkButtonPrintMoney");
            LinkButton lbHandleBill = (LinkButton)e.Row.FindControl("LinkButtonHandleBill");

            BookingDTO row = (BookingDTO)e.Row.DataItem;
            if (row.Booking_MoneyReceiptIssued)
            {
                
                if (row.Booking_AccountSettled)
                {
                    lbHandleBill.Visible = true;
                    lbPrintMoney.Visible = false;
                }
                else
                {
                    lbPrintMoney.Visible = true;
                    lbHandleBill.Visible = false;
                }
            }
            else
            {
                lbPrintMoney.Visible = false;
                lbHandleBill.Visible = false;
            }


        }
    }
}