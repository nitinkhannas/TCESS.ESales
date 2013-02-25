<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageCustomers.aspx.cs" Inherits="CustomerRegistration_ManageCustomers" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<%@ Register Src="UserControls/CustomerRegistration.ascx" TagName="CustomerRegistration"
    TagPrefix="uc5" %>
<%@ Register Src="UserControls/CustomerDocumentRegistration.ascx" TagName="CustomerDocumentRegistration"
    TagPrefix="uc6" %>
<%@ Register Src="UserControls/ManageCustomers.ascx" TagName="ManageCustomers" TagPrefix="uc3" %>
<%@ Register Src="UserControls/CustomerRelationshipReport.ascx" TagName="CustomerRelationshipReport"
    TagPrefix="uc4" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManageCustomers%>"
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
            <asp:Panel ID="pnlManageCustomers" runat="server">
                <uc3:ManageCustomers ID="ucManageCustomers" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlCustomerRegistration" runat="server">
                <uc5:CustomerRegistration ID="ucCustomerRegistration" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlCustomerDocumentRegistration" runat="server">
                <uc6:CustomerDocumentRegistration ID="ucCustomerDocumentRegistration" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlCustomerRelationshipReport" runat="server">
                <uc4:CustomerRelationshipReport ID="ucCustomerRelationshipReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
