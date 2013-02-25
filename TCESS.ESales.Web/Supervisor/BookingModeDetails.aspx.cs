#region Using directives

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Exception;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Supervisor_BookingModeDetails : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Get Booking type details by booking type
            PopulateBookingTypeDetails();
        }
    }

    /// <summary>
    /// Get Booking type details by booking type
    /// </summary>
    /// <param name="bookingTypeId">Int32: bookingId</param>
    private void PopulateBookingTypeDetails()
    {
        IList<BookingModeDetailDTO> lstBookingModeDetails = ESalesUnityContainer.Container.Resolve<IBookingModeService>()
            .GetBookingModeDetails();

        //If list of booking mode details contains values
        if (lstBookingModeDetails.Count > 0)
        {
            grdBookingModeDetails.DataSource = lstBookingModeDetails;
            grdBookingModeDetails.DataBind();
        }
        else
        {
            //Show blank grid with default row
            ShowBlankRowInGrid();
        }
    }

    /// <summary>
    /// Show blank grid with default row
    /// </summary>
    private void ShowBlankRowInGrid()
    {
        ShowBlankRowInGrid<BookingModeDetailDTO>(grdBookingModeDetails);
    }

    protected void grdBookingModeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            DropDownList ddlBookingMode = (DropDownList)e.Row.FindControl("ddlBookingMode");
            DropDownList ddlStartTime = (DropDownList)e.Row.FindControl("ddlStartTime");
            DropDownList ddlEndTime = (DropDownList)e.Row.FindControl("ddlEndTime");
            TextBox txtTimeInterval = (TextBox)e.Row.FindControl("txtTimeInterval");
            CustomValidator validator = (CustomValidator)e.Row.FindControl("timeValidator");
            
            //Retrives all active booking types from database
            ddlBookingMode.DataSource = ESalesUnityContainer.Container.Resolve<IBookingModeService>().GetBookingTypes();
            ddlBookingMode.DataBind();
            ddlBookingMode.Items.Insert(0, new ListItem(Messages.SelectBookingMode, "0"));

            //Add an onchange event to the ddlBookingMode drop down list which is in footer row of the grid view  
            //The javascript function will take as parameters Ids of 
            //the drop down list and the textbox that should be enabled/disabled
            string jsToggle = "ToggleTimeInterval('" + ddlBookingMode.ClientID + "','" + txtTimeInterval.ClientID + "')";
            ddlBookingMode.Attributes.Add("onchange", jsToggle);

            //The custom validation control will validate the drop down list if end time is smaller than start time
            //Here we are adding ids of the ddlStartTime and ddlEndTime as client attributes of the validation control  
            //These attributes can be accessed from the client side function to help perform custom validation
            validator.Attributes.Add("ddlStartTime", ddlStartTime.ClientID);
            validator.Attributes.Add("ddlEndTime", ddlEndTime.ClientID);
        }
    }

    /// <summary>
    /// Checks page validity by verifying booking mode values
    /// </summary>
    private void CheckIfPageIsValid(GridViewRow row)
    {        
        string timeIntervalValue = ((TextBox)row.FindControl("txtTimeInterval")).Text;
        CustomValidator timeIntervalValidator = (CustomValidator)row.FindControl("timeIntervalValidator");

        //If user has entered time interval
        if (String.IsNullOrEmpty(timeIntervalValue))
        {            
            timeIntervalValidator.IsValid = false;
        }
        else
        {
            int startTimeValue = Convert.ToInt32(((DropDownList)row.FindControl("ddlStartTime")).SelectedItem.Value);
            int endTimeValue = Convert.ToInt32(((DropDownList)row.FindControl("ddlEndTime")).SelectedItem.Value);
            string endTime = ((DropDownList)row.FindControl("ddlEndTime")).SelectedItem.Text;

            TimeSpan bookingEndTimeSpan = new TimeSpan(Convert.ToDateTime(endTime).Hour, Convert.ToDateTime(endTime).Minute, 0);

            // Get today's date at booking end time
            DateTime endTimeWithInterval = DateTime.Today.Add(bookingEndTimeSpan).AddHours(Convert.ToInt32(timeIntervalValue));

            //Compare if booking end time with time interval is within today's hours
            if (DateTime.Compare(endTimeWithInterval, DateTime.Today.AddDays(1)) > 0)
            {
                timeIntervalValidator.IsValid = false;
                timeIntervalValidator.ErrorMessage = ErrorMessages.InvalidTimeInterval;
            }
        }
    }

    protected void grdBookingModeDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == Globals.GridCommandEvents.ADDNEW)
        {
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            
            //Gets booking mode from dropdown control
            int bookingMode = Convert.ToInt32(((DropDownList)row.FindControl("ddlBookingMode")).SelectedItem.Value);

            if (bookingMode == 2)
            {
                //Checks page validity by verifying booking mode values
                CheckIfPageIsValid(row);
            }

            if (Page.IsValid)
            {
                try
                {
                    ExceptionHandler.AppExceptionManager.Process(() =>
                    {
                        //Initialize booking mode detail values
                        BookingModeDetailDTO bookingModeDetails = InitializeBookingModeDetails(row);

                        //Save booking mode details in database
                        ESalesUnityContainer.Container.Resolve<IBookingModeService>().SaveBookingModeDetails(bookingModeDetails);
                    }, Globals.ExceptionTypes.ExceptionShielding.ToString());
                }
                catch (Exception ex)
                {
                }                
                //Get Booking type details by booking type
                PopulateBookingTypeDetails();
                ucMessageBoxForGrid.ShowMessage(Resources.Messages.BookingModeDetailsAddedSuccessfully);
            }
        }
    }

    /// <summary>
    /// Initialize booking mode detail values
    /// </summary>
    /// <param name="row">Footer row from Gridview</param>
    /// <returns>returns booking mode detail object</returns>
    private BookingModeDetailDTO InitializeBookingModeDetails(GridViewRow row)
    {
        BookingModeDetailDTO bookingModeDetails = new BookingModeDetailDTO();
        bookingModeDetails.BookingDetails_Mode_Id = Convert.ToInt32(((DropDownList)row.FindControl("ddlBookingMode")).SelectedItem.Value);
        bookingModeDetails.BookingDetails_Date = DateTime.Today;
        string startTime = ((DropDownList)row.FindControl("ddlStartTime")).SelectedItem.Text;
        bookingModeDetails.BookingDetails_StartTime = new TimeSpan(Convert.ToDateTime(startTime).Hour, Convert.ToDateTime(startTime).Minute, 0);
        string endTime = ((DropDownList)row.FindControl("ddlEndTime")).SelectedItem.Text;
        bookingModeDetails.BookingDetails_EndTime = new TimeSpan(Convert.ToDateTime(endTime).Hour, Convert.ToDateTime(endTime).Minute, 0);
        string timeInterval = ((TextBox)row.FindControl("txtTimeInterval")).Text;
        if (!string.IsNullOrEmpty(timeInterval))
        {
            bookingModeDetails.BookingDetails_TimeInterval = Convert.ToInt32(((TextBox)row.FindControl("txtTimeInterval")).Text);
        }
        bookingModeDetails.BookingDetails_Trucks = Convert.ToInt32(((TextBox)row.FindControl("txtTruckLimit")).Text);
        return bookingModeDetails;
    }

    protected void BookingMode_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        int bookingModeId = Convert.ToInt32(((DropDownList)grdBookingModeDetails.FooterRow.FindControl("ddlBookingMode")).SelectedItem.Value);
        
        //Verifies if booking mode id already exists for a day
        if (ESalesUnityContainer.Container.Resolve<IBookingModeService>().VerifyDuplicateBookingMode(bookingModeId))
        {
            args.IsValid = false;         
        }
    }

    protected void AddBookingModeDetails_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        DropDownList ddlStartTime = (DropDownList)grdBookingModeDetails.FooterRow.FindControl("ddlStartTime");
        DropDownList ddlEndTime = (DropDownList)grdBookingModeDetails.FooterRow.FindControl("ddlEndTime");
        string startTime = ddlStartTime.SelectedItem.Text;
        string endTime = ddlEndTime.SelectedItem.Text;
        if (ESalesUnityContainer.Container.Resolve<IBookingModeService>()
            .BookingModeExists
            (new TimeSpan(Convert.ToDateTime(startTime).Hour, Convert.ToDateTime(startTime).Minute, 0)
            , new TimeSpan(Convert.ToDateTime(endTime).Hour, Convert.ToDateTime(endTime).Minute, 0)))
        {
            args.IsValid = false;            
        }
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        PopulateBookingTypeDetails();
    }
}