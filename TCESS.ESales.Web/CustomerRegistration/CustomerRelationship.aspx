<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CustomerRelationship.aspx.cs" Inherits="CustomerRegistration_CustomerRelationship" %>

<%@ Register Src="UserControls/CustomerRegistration.ascx" TagName="CustomerRegistration"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/CustomerDocumentRegistration.ascx" TagName="CustomerDocumentRegistration"
    TagPrefix="uc2" %>
<%@ Register Src="UserControls/TruckRegistration.ascx" TagName="TruckRegistration"
    TagPrefix="uc3" %>
<%@ Register Src="UserControls/AuthorizedRepresentative.ascx" TagName="AuthorizedRepresentative"
    TagPrefix="uc4" %>
<%@ Register Src="UserControls/CustomerRelationshipReport.ascx" TagName="CustomerRelationshipReport"
    TagPrefix="uc5" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" CssClass="pageNameContent" Text="<%$Resources:Labels, CustomerRelationshipProcess%>" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uplMainPanel"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="overlay">
                <div class="ajaxloader">
                    <img src='<%= ResolveClientUrl("~/Images/ajax-loader.gif")%>' style="vertical-align: middle"
                        alt="Processing" />Processing ...
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="uplMainPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlCustomerRegistration" runat="server">
                <uc1:CustomerRegistration ID="ucCustomerRegistration" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlCustomerDocumentRegistration" runat="server">
                <uc2:CustomerDocumentRegistration ID="ucCustomerDocumentRegistration" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlTruckRegistration" runat="server">
                <uc3:TruckRegistration ID="ucTruckRegistration" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlAuthRepRegistration" runat="server">
                <uc4:AuthorizedRepresentative ID="ucAuthorizedRepresentative" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlCustomerRelationshipReport" runat="server">
                <uc5:CustomerRelationshipReport ID="ucCustomerRelationshipReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
