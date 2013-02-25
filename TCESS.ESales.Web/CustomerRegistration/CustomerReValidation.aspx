<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CustomerReValidation.aspx.cs" Inherits="CustomerRegistration_CustomerReValidation" %>

<%@ Register Src="~/CustomerRegistration/UserControls/CustomerReValidation.ascx"
    TagName="CustomerReValidation" TagPrefix="uc1" %>
<%@ Register Src="~/CustomerRegistration/UserControls/CustomerDocumentReValidate.ascx"
    TagName="CustomerDocumentReValidate" TagPrefix="uc2" %>
<%@ Register Src="~/CustomerRegistration/UserControls/CustomerPartner.ascx" TagName="CustomerPartner"
    TagPrefix="uc3" %>
<%@ Register Src="UserControls/CustomerRelationshipReport.ascx" TagName="CustomerRelationshipReport"
    TagPrefix="uc5" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" CssClass="pageNameContent" Text="<%$Resources:Labels, CustomerRevalidationProcess%>" />
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
            <asp:Panel ID="pnlCustomerReValidation" runat="server">
                <uc1:CustomerReValidation ID="ucCustomerReValidation" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlCustomerDocumentReValidate" runat="server">
                <uc2:CustomerDocumentReValidate ID="ucCustomerDocumentReValidate" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlCustomerPartner" runat="server">
                <uc3:CustomerPartner ID="ucCustomerPartner" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlCustomerRelationshipReport" Visible="false" runat="server">
                <uc5:CustomerRelationshipReport ID="ucCustomerRelationshipReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
