<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="BookingsForTheDayDisplay.aspx.cs" Inherits="Reports_BookingsForTheDayDisplay" %>

<%@ Register Src="UserControls/BookingsForTheDayDisplay.ascx" TagName="BookingsForTheDayDisplay"
    TagPrefix="uc3" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, Bookings%>"
        CssClass="pageNameContent" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="progressBar" runat="server" AssociatedUpdatePanelID="uplMainPanel"
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
    <asp:UpdatePanel runat="server" ID="uplMainPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlBookingsForTheDayDisplay" runat="server">
                <uc3:BookingsForTheDayDisplay ID="ucBookingsForTheDayDisplay" runat="server" />
            </asp:Panel>            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
