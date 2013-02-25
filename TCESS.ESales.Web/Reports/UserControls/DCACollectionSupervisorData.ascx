<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DCACollectionSupervisorData.ascx.cs"
    Inherits="Reports_UserControls_DCACollectionSupervisorData" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<style type="text/css">
    table.tableizer-table
    {
        border: 1px solid #3366CC;
        font-family: Times New Roman, Times, serif;
        font-size: 13px;
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
                AutoPostBack="true" Visible="false" />
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
                Visible="false" />
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
        <div id="trackerDiv" runat="server">
            <table align="center" class="tableizer-table" width="75%">
                <tr>
                    <td align="center">
                        <div>
                            <Custom:GridViewAlwaysShow ID="grdDCACollection" runat="server" AutoGenerateColumns="False"
                                BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                                AllowPaging="false" HorizontalAlign="Center" Width="100%" CellPadding="5">
                                <Columns>
                                    <asp:TemplateField HeaderText="<%$Resources:Labels, DCACounterName%>">
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("countername"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$Resources:Labels, DCAName%>">
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("dcaname"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bookings">
                                        <ItemTemplate>
                                            <%#Convert.ToInt32(Eval("total"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Loading Advice Issue">
                                        <ItemTemplate>
                                            <%#Convert.ToInt32(Eval("loadingAdvIssue"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Truck Out">
                                        <ItemTemplate>
                                            <%#Convert.ToInt32(Eval("completed"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pending">
                                        <ItemTemplate>
                                            <%#Convert.ToInt32(Eval("pending"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#397dbc" Font-Bold="True" ForeColor="#FFFFFF" Height="20px" />
                                <PagerStyle BackColor="#397dbc" ForeColor="#FFFFFF" HorizontalAlign="Left" />
                                <RowStyle BackColor="White" ForeColor="#003399" Font-Size="Small" Height="20px" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                <SortedDescendingHeaderStyle BackColor="#002876" />
                            </Custom:GridViewAlwaysShow>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script language="javascript" type="text/javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();

    prm.add_endRequest(endRequest);
    prm.add_beginRequest(beginRequest);

    function beginRequest(sender, args) {
        var updateProgressDiv = document.getElementById('ctl00_MainContent_ucDCACollectionSupervisorData_progressBar');
        updateProgressDiv.style.display = 'inline';
    }

    function endRequest(sender, args) {
        var updateProgressDiv = document.getElementById('ctl00_MainContent_ucDCACollectionSupervisorData_progressBar');
        updateProgressDiv.style.display = 'none';
    }
</script>
