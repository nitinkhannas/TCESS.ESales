<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DailyBookingPendingReportForAll.aspx.cs" Inherits="Reports_DailyBookingPendingReportForAll" %>

<%@ Register Src="UserControls/DailyBookingPendingReportForAllReport.ascx" TagName="DailyBookingPendingReportForAllReport"
    TagPrefix="uc2" %>
<%@ Register Src="UserControls/DailyBookingPendingReportForAllData.ascx" TagName="DailyBookingPendingReportForAllData"
    TagPrefix="uc3" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, DailyBookingPendingReportForAll%>"
        CssClass="pageNameContent" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="overlay">
                <div class="ajaxloader">
                    <img src='<%= ResolveClientUrl("~/Images/ajax-loader.gif")%>' style="vertical-align: middle"
                        alt="Processing" />Processing....
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlDailyBookingPendingReportForAllData" runat="server">
                <uc3:DailyBookingPendingReportForAllData ID="ucDailyBookingPendingReportForAllData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlDailyBookingPendingReportForAllReport" runat="server">
                &nbsp;<uc2:DailyBookingPendingReportForAllReport ID="ucDailyBookingPendingReportForAllReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

