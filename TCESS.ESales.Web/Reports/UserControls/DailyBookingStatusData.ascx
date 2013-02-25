<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DailyBookingStatusData.ascx.cs"
    Inherits="Reports_UserControls_DailyBookingStatusData" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<style type="text/css">
    table.tableizer-table
    {
        border: 1px solid #3366CC;
        font-family: Times New Roman, Times, serif;
        font-size: 16px;
    }
    .tableizer-table td
    {
        padding: 1px;
        margin: 2px;
        border: 1px solid #3366CC;
        font-weight: bold;
    }
    .tableizer-table th
    {
        background-color: #397dbc;
        color: #FFFFFF;
        font-weight: bold;
    }
</style>
<table width="100%" cellspacing="5">
    <tr align="left">
        <td colspan="5">
            <asp:CheckBox ID="chkDateRange" runat="server" Text="<%$Resources:Labels, EnableMultiDateSelection%>"
                OnCheckedChanged="chkDateRange_CheckedChanged" AutoPostBack="true" Visible="false" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblFromDate" Visible="false" runat="server" Text="<%$Resources:Labels, FromDate%>" />
        </td>
        <td>
            <asp:TextBox ID="txtFromDate" Visible="false" runat="server" CssClass="textbox" Enabled="false" />
            <ajax:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFromDate"
                Format="dd-MMM-yyyy" />
            <ajax:TextBoxWatermarkExtender ID="FromDate_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtFromDate" WatermarkCssClass="watermark" WatermarkText="<%$Resources:Labels, SelectDate%>" />
            <asp:RequiredFieldValidator ID="txtFromDateValidator" ControlToValidate="txtFromDate"
                Display="Dynamic" ValidationGroup="LoadingAdvRpt" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredFromDate %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="txtFromDateValidatorCalloutExtender" runat="server"
                TargetControlID="txtFromDateValidator" />
        </td>
        <td>
            <asp:Label ID="lblToDate" Visible="false" runat="server" Text="<%$Resources:Labels, ToDate%>" />
        </td>
        <td>
            <asp:TextBox ID="txtToDate" Visible="false" runat="server" CssClass="textbox" Enabled="false" />
            <ajax:TextBoxWatermarkExtender ID="ToDate_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtToDate" WatermarkCssClass="watermark" WatermarkText="<%$Resources:Labels, SelectDate%>" />
            <ajax:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtToDate"
                Format="dd-MMM-yyyy" />
            <asp:RequiredFieldValidator ID="txtToDateValidator" ControlToValidate="txtToDate"
                Display="Dynamic" ValidationGroup="LoadingAdvRpt" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredToDate %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="txtToDateValidatorCalloutExtender" runat="server"
                TargetControlID="txtToDateValidator" />
        </td>
        <td align="right">
            <asp:Button ID="btnGenerate" runat="server" OnClientClick="javascript:return CompareDate();"
                Text="<%$Resources:Labels, Generate%>" CssClass="button" ValidationGroup="LoadingAdvRpt"
                OnClick="btnGenerate_Click" Visible="false" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
<asp:UpdateProgress ID="progressBar" runat="server" AssociatedUpdatePanelID="TimedPanel"
    DisplayAfter="0">
    <ProgressTemplate>
        <div class="overlay">
            <div class="ajaxloader">
                <img src="../../Images/ajax-loader.gif" style="vertical-align: middle" alt="Processing" />Processing....
            </div>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:Timer runat="server" ID="UpdateTimer" Interval="60000" OnTick="UpdateTimer_Tick" />
<asp:UpdatePanel runat="server" ID="TimedPanel" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="UpdateTimer" EventName="Tick" />
    </Triggers>
    <ContentTemplate>  
          <asp:Label runat="server" ID="DateStampLabel" />  
    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>

        
        <table align="center" class="tableizer-table" style="display:none" >
            <tr class="tableizer-firstrow">
                <th style="width: 220px;">
                    <asp:Label ID="lblBooking" runat="server" Text="BOOKINGS"></asp:Label>
                </th>
                <th style="width: 70px;">
                    &nbsp;
                </th>
                <th>
                    &nbsp;
                </th>
                <th style="width: 220px;">
                    <asp:Label ID="lblTrucks" runat="server" Text="TRUCKS"></asp:Label>
                </th>
                <th style="width: 70px;">
                    &nbsp;
                </th>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblSmsaccepted" runat="server" Text="SMS Accepted"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblSmsaccepteddata" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center">
                    <asp:Label ID="lblOpenings" runat="server" Text="Opening Pendings"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblOpeningsData" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblTotalbooking" runat="server" Text="Bookings"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblTotalbookingdata" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center">
                    &nbsp;</td>
                <td align="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblalocated" runat="server" Text="Allocated to:"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center">
                    <asp:Label ID="lblLoadingadv" runat="server" Text="Loading Advice Issue"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblLoadingadvdata" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblGapl" runat="server" Text="GAPL"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblGapldata" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center">
                    <asp:Label ID="lblGatein" runat="server" Text="Gate In"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblGateindata" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblGsa" runat="server" Text="GSA"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblGsadata" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center">
                    <asp:Label ID="lblLoaded" runat="server" Text="Loaded"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblloadeddata" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblMvs" runat="server" Text="MVS"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblMvsdata" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center">
                    <asp:Label ID="lblTruckout" runat="server" Text="Truck Out"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblTruckoutdata" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblNkcpl" runat="server" Text="NKCPL"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblNkcpldata" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center">
                    &nbsp;</td>
                <td align="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblTIPL" runat="server" Text="TIPL"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblTipldata" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center">
                    <asp:Label ID="lblPendings" runat="server" Text="Closing Pendings"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblPendingsData" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<script language="javascript" type="text/javascript">


    var prm = Sys.WebForms.PageRequestManager.getInstance();

    prm.add_endRequest(endRequest);
    prm.add_beginRequest(beginRequest);

    function beginRequest(sender, args) {
        var updateProgressDiv = document.getElementById('ctl00_MainContent_ucDailyBookingStatusData_progressBar');
        updateProgressDiv.style.display = 'inline';
    }

    function endRequest(sender, args) {
        var updateProgressDiv = document.getElementById('ctl00_MainContent_ucDailyBookingStatusData_progressBar');
        updateProgressDiv.style.display = 'none';
    }
</script>
