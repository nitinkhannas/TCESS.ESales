#region Using directives

using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using System.Collections;
using Resources;
using TCESS.ESales.CommonLayer.Exception;

#endregion

public partial class Administrator_UserControls_ActivateForm27C : BaseUserControl
{
    private int value = 0;
    private string validYear = string.Empty;
    private string validMonth = string.Empty;

    public event ShowDataByIdEventHandler Event_PrintCustomer;

    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Get list of Form27C for activation
            GetForm27CForActivation();
            MonthValidation(0, 0);
        }
    }

    /// <summary>
    /// Get list of Form27C for activation
    /// </summary>
    private void GetForm27CForActivation()
    {
        IList<Form27CDTO> lstForm27C = new List<Form27CDTO>();
        lstForm27C = ESalesUnityContainer.Container.Resolve<IForm27CService>().GetForm27CListToActivate();

        if (lstForm27C.Count > 0)
        {
            grdActivateForm27C.DataSource = lstForm27C;
            grdActivateForm27C.DataBind();
        }
        else
        {
            ShowBlankGrid();
        }
    }

    private void ShowBlankGrid()
    {
        base.ShowBlankRowInGrid<Form27CDTO>(grdActivateForm27C);
    }

    protected void grdActivateForm27C_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == Globals.GridCommandEvents.PRINTCUSTOMER)
        {
            Event_PrintCustomer(Convert.ToInt32(e.CommandArgument));
        }
        if (e.CommandName == Globals.GridCommandEvents.VIEW)
        {
            Session[Globals.StateMgmtVariables.VIEWCUSTOMERSOURCE] = 1;
            Session[Globals.StateMgmtVariables.CUSTOMERID] = e.CommandArgument;
            Response.Redirect("ViewCustomerDetails.aspx");
        }        
    }

    protected void grdActivateForm27C_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(e.NewEditIndex);
    }

    protected void grdActivateForm27C_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1);
    }

    protected void grdActivateForm27C_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            int form27C_Id = Int32.Parse(grdActivateForm27C.DataKeys[e.RowIndex].Value.ToString());

            Form27CDTO form27Details = ESalesUnityContainer.Container.Resolve<IForm27CService>()
            .GetForm27CDetailsByForm27CId(form27C_Id);

            CustomerDTO customerDetails = ESalesUnityContainer.Container.Resolve<ICustomerService>()
            .GetActiveCustomerDetailsByCode(form27Details.Cust_Code);

            GridViewRow gvRow = (GridViewRow)grdActivateForm27C.Rows[e.RowIndex];

            TextBox txtReceivedDate = (TextBox)gvRow.FindControl("txtReceivedDate");
            DropDownList ddlMonth = (DropDownList)gvRow.FindControl("ddlMonths");

            validYear = ddlMonth.SelectedItem.Text.Substring(ddlMonth.SelectedItem.Text.IndexOf(" "));
            validMonth = ddlMonth.SelectedItem.Text.Substring(0, ddlMonth.SelectedItem.Text.IndexOf(" "));

            int ddlMonthCount = ddlMonth.Items.Count;
            int ddlSelectedIndex = ddlMonth.SelectedIndex;

            form27Details.ModifiedDate = DateTime.Now;
            form27Details.ModifiedBy = GetCurrentUserId();
            form27Details.ValidYear = validYear;
            if (txtReceivedDate.Text.Trim() == "Click to select date")
            {
                ucMessageBox.ShowMessage("Enter TSL Recieved Date");
                GridViewRowUpdateFunctions(e.RowIndex);
                return;
            }
            form27Details.AcceptedByTSLDate = Convert.ToDateTime(txtReceivedDate.Text.Trim());

            int currentMonth= CurrentMonthValue(validMonth);
            form27Details.CurrentMonth = currentMonth;
            
            form27Details.ValidMonthCount = form27Details.PeriodType == 1 ? 1 : ddlMonthCount - ddlSelectedIndex;

            int result = ESalesUnityContainer.Container.Resolve<IForm27CService>().UpdateForm27C(form27Details);
            if (result > 0)
            {
                string englishMessage = "Hamen aapke Code " + customerDetails.Cust_Code + " mein " + ddlMonth.SelectedItem.Text + " ka Form 27C prapt hua hai. Aap apne unit mein prayog ke liye Tailings ki bookings karen";
                string mobileNo = customerDetails.Cust_MobileNo;
                SmsUtility.SendSMSForBookings(mobileNo, englishMessage + " .DCA Ghato");
                ucMessageBoxForGrid.ShowMessage("Form 27C Activated Successfully.");
            }
            else
            {
                ucMessageBoxForGrid.ShowMessage("Unable to Activate Form 27C.");
            }
        }
    }


    private int CurrentMonthValue(string validMonth)
    {
        int monthValue = 0;
        switch (validMonth)
        {
            case "January":
                monthValue = 1;
                break;
            case "February":
                monthValue = 2;
                break;
            case "March":
                monthValue = 3;
                break;
            case "April":
                monthValue = 4;
                break;
            case "May":
                monthValue = 5;
                break;
            case "June":
                monthValue = 6;
                break;
            case "July":
                monthValue = 7;
                break;
            case "August":
                monthValue = 8;
                break;
            case "September":
                monthValue = 9;
                break;
            case "October":
                monthValue = 10;
                break;
            case "November":
                monthValue = 11;
                break;
            case "December":
                monthValue = 12;
                break;
        }
        return monthValue;
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1);
    }

    /// <summary>
    /// Row edit/update/cancel function for grid view
    /// </summary>
    /// <param name="rowIndex">rowIndex of gridview</param>
    private void GridViewRowUpdateFunctions(int rowIndex)
    {
        grdActivateForm27C.EditIndex = rowIndex;

        //Get list of customer for activation and code allotement
        GetForm27CForActivation();
    }

    protected void grdActivateForm27C_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grdActivateForm27C.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlMonth = (DropDownList)e.Row.FindControl("ddlMonths");
            TextBox txtReceivedDate = (TextBox)e.Row.FindControl("txtReceivedDate");
            HiddenField hdnPeriodType = (HiddenField)e.Row.FindControl("hdnPerioType");
            HiddenField hdnCurrentMonth = (HiddenField)e.Row.FindControl("hdnCurrentMonth");

            List<MonthsDTO> monthList = MonthValidation(Convert.ToInt32(hdnPeriodType.Value), Convert.ToInt32(hdnCurrentMonth.Value)).ToList();

            ddlMonth.DataSource = monthList;
            ddlMonth.DataBind();
        }
    }

    private IList<MonthsDTO> MonthValidation(int hdnPeriodType, int hdnCurrentMonth)
    {
        List<MonthsDTO> monthList = new List<MonthsDTO>();

        int ddlStartindex = hdnCurrentMonth;
        int ddlStartMonth = hdnCurrentMonth;

        if (hdnPeriodType > 0)
        {
            switch (hdnPeriodType)
            {
                case 1:
                    value = 2;
                    ddlStartindex = hdnCurrentMonth;
                    break;
                case 2:
                    value = 3;
                    if (ddlStartMonth >= 1 && ddlStartMonth <= 3)
                    {
                        ddlStartindex = 1;
                    }
                    if (ddlStartMonth >= 4 && ddlStartMonth <= 6)
                    {
                        ddlStartindex = 4;
                    }
                    if (ddlStartMonth >= 7 && ddlStartMonth <= 9)
                    {
                        ddlStartindex = 7;
                    }
                    if (ddlStartMonth >= 10 && ddlStartMonth <= 12)
                    {
                        ddlStartindex = 10;
                    }
                    break;
                case 3:
                    value = 6;
                    if (ddlStartMonth >= 3 && ddlStartMonth <= 9)
                    {
                        ddlStartindex = 4;
                    }
                    if (ddlStartMonth >= 1 && ddlStartMonth <= 3)
                    {
                        ddlStartindex = 10;
                    }
                    if (ddlStartMonth >= 10 && ddlStartMonth <= 12)
                    {
                        ddlStartindex = 10;
                    }
                    break;
                case 4:
                    value = 12;
                    ddlStartindex = 4;
                    break;
            }


            monthList = Bind_CaseStatus(value, ddlStartindex).ToList();
        }
        return monthList;
    }

    private IList<MonthsDTO> Bind_CaseStatus(int value, int ddlStartMonth)
    {
        List<MonthsDTO> lst = new List<MonthsDTO>();
        int totalNumberOfMonth = 0;
        int currentMonthId = ddlStartMonth;
        if (value > 0)
        {
            List<MonthsDTO> listMonth = ESalesUnityContainer.Container.Resolve<IForm27CService>().GetMonthList().ToList();

            //string selectedDateMonth = txtReceivedDate.Text.Substring(txtReceivedDate.Text.IndexOf("-") + 1, 3);
            //string selectedDateYear = txtReceivedDate.Text.Substring(txtReceivedDate.Text.LastIndexOf("-") + 1);

            int selectedDateYear = DateTime.Now.Year;

            int toLimit = 0;
            totalNumberOfMonth = currentMonthId + value;
            if (totalNumberOfMonth <= 12)
            {
                toLimit = totalNumberOfMonth - 1;
            }
            else
            {
                toLimit = 12;
            }

            for (int i = currentMonthId; i <= toLimit; i++)
            {
                foreach (MonthsDTO data in listMonth)
                {
                    if (data.Months_Id == i)
                    {
                        MonthsDTO dt = new MonthsDTO();

                        dt.MonthName = data.MonthName + " " + selectedDateYear;
                        dt.Months_Id = i;
                        lst.Add(dt);
                    }
                }
            }

            if (totalNumberOfMonth > 12)
            {
                int nextYearMonths = totalNumberOfMonth - 12;

                for (int i = 1; i < nextYearMonths; i++)
                {
                    foreach (MonthsDTO data in listMonth)
                    {
                        if (data.Months_Id == i)
                        {
                            MonthsDTO dt = new MonthsDTO();

                            dt.MonthName = data.MonthName + " " + (Convert.ToInt32(selectedDateYear) + 1);
                            dt.Months_Id = i;
                            lst.Add(dt);
                        }
                    }
                }
            }

            return lst;
        }
        return lst;
    }
}