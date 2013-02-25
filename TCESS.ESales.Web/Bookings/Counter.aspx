<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Counter.aspx.cs" Inherits="Bookings_Counter" %>

<%@ Register Src="~/Bookings/UserControls/CounterCreation.ascx" TagName="CounterCreation"
    TagPrefix="uc1" %>
<%@ Register Src="~/Bookings/UserControls/ManageCounter.ascx" TagName="EditDeleteCreation"
    TagPrefix="uc2" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManageCounters%>"
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
            <asp:Panel ID="pnlManageCounter" runat="server">
                <uc2:EditDeleteCreation ID="ucManageCounter" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlCreateCounter" runat="server">
                <uc1:CounterCreation ID="ucCounterCreation" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
