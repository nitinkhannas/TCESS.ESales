<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageTrucks.aspx.cs" Inherits="CustomerRegistration_ManageTrucks" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<%@ Register Src="UserControls/ManageTrucks.ascx" TagName="ManageTrucks" TagPrefix="uc1" %>
<%@ Register Src="UserControls/TruckRegistration.ascx" TagName="TruckRegistration"
    TagPrefix="uc2" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManageTrucks%>"
        CssClass="pageNameContent" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uplMainPanel"
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
            <asp:Panel ID="pnlManageTrucks" runat="server">
                <uc1:ManageTrucks ID="ucManageTrucks" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlTruckRegistration" runat="server">
                <uc2:TruckRegistration ID="ucTruckRegistration" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
