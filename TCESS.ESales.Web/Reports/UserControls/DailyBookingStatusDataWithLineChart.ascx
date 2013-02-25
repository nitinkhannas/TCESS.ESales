<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DailyBookingStatusDataWithLineChart.ascx.cs"
    Inherits="Reports_UserControls_DailyBookingStatusDataWithLineChart" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
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
<asp:Timer runat="server" ID="UpdateTimer" Interval="30000" OnTick="UpdateTimer_Tick" />
<asp:UpdatePanel runat="server" ID="TimedPanel" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="UpdateTimer" EventName="Tick" />
    </Triggers>
    <ContentTemplate>
        <asp:Label runat="server" ID="DateStampLabel" />
        <table width="100%">
            <tr>
                <td align="center">
                    <Custom:GridViewAlwaysShow ID="grdDailyBookingStatus" runat="server" AutoGenerateColumns="False"
                        BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                        AllowPaging="false" HorizontalAlign="Center" Width="100%" CellPadding="5">
                        <Columns>
                            <asp:TemplateField HeaderText="SMS Accepted">
                                <ItemTemplate>
                                    <%#Convert.ToInt32(Eval("smsAccepted"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Labels, Bookings%>">
                                <ItemTemplate>
                                    <%#Convert.ToInt32(Eval("bookings"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Labels, LoadingAdviceIssue%>">
                                <ItemTemplate>
                                    <%#Convert.ToInt32(Eval("loadingAdvIssue"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Gate In">
                                <ItemTemplate>
                                    <%#Convert.ToInt32(Eval("truckIn"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Loaded">
                                <ItemTemplate>
                                    <%#Convert.ToInt32(Eval("material"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="<%$Resources:Labels, TruckOut%>">
                                <ItemTemplate>
                                    <%#Convert.ToInt32(Eval("truckOuts"))%>
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
                    <br />
                </td>
            </tr>

            <tr>
                <td align="center">
                    <asp:Chart ID="Chart1" runat="server" Width="585px">
                        <Titles>
                            <asp:Title Text=""></asp:Title>
                        </Titles>
                        <Series>
                            <asp:Series ChartType="Line"
                            ChartArea="ChartArea1" IsValueShownAsLabel="true">
                                <Points>
                                    <asp:DataPoint MarkerStyle="Square" AxisLabel="SMS Accepted" XValue="10" Color="Red" />
                                    <asp:DataPoint MarkerStyle="Square" AxisLabel="Bookings" XValue="30" Color="Orange"/>
                                    <asp:DataPoint MarkerStyle="Square" AxisLabel="Loading Advice Issue" XValue="50" Color="Green" />
                                    <asp:DataPoint MarkerStyle="Square" AxisLabel="Gate In" XValue="70" Color="Blue" />
                                    <asp:DataPoint MarkerStyle="Square" AxisLabel="Loaded" XValue="90" Color="DarkSlateGray" />
                                    <asp:DataPoint MarkerStyle="Square" AxisLabel="Truck Out" XValue="110" Color="Brown" />
                                </Points>
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1">
                                <AxisX LineColor="64,4,64,64">
                                <MajorGrid Enabled="false" />
                                <LabelStyle Font="12pt" />
                                </AxisX>
                                <AxisY LineColor="DarkGray">
                                <MajorGrid Enabled="false" />
                                <LabelStyle Font="12pt" />
                                </AxisY>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </td>
            </tr>
            <tr>
                <td align="left">
                 <asp:Label ID="lblPageName" runat="server" Text="Booking Allocated"
        CssClass="pageNameContent" Visible="false" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <Custom:GridViewAlwaysShow ID="grdBookingBreakup" runat="server" AutoGenerateColumns="False"
                        BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                        AllowPaging="false" HorizontalAlign="Center" Width="100%" CellPadding="5">
                        <Columns>
                            <asp:TemplateField HeaderText="<%$Resources:Labels, DCA%>">
                                <ItemTemplate>
                                    <%#(Eval("agent"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Count">
                                <ItemTemplate>
                                    <%#Convert.ToInt32(Eval("Counts"))%>
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
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
