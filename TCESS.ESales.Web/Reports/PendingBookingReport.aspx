<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PendingBookingReport.aspx.cs" Inherits="Reports_BookingPendingReport" %>

<%@ Register Src="UserControls/PendingBookingDataReport.ascx" TagName="BookingPendingDataReport"
    TagPrefix="uc2" %>
<%@ Register Src="UserControls/PendingBookingData.ascx" TagName="BookingPendingData"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, PendingBookingreport%>"
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
            <asp:Panel ID="pnlBookingPendingData" runat="server">
                <uc1:BookingPendingData ID="ucBookingPendingData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlBookingPendingDataReport" runat="server">
                &nbsp;<uc2:BookingPendingDataReport ID="ucBookingPendingDataReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
