<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ProspectiveCustomer.aspx.cs" Inherits="CustomerRegistration_ProspectiveCustomer" %>

<%@ Register Src="~/CustomerRegistration/UserControls/CustomerRegistration.ascx"
    TagName="CustomerRegistration" TagPrefix="uc1" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" CssClass="pageNameContent" Text="<%$Resources:Labels, ProspectiveCustomer%>" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="progressBar" runat="server" AssociatedUpdatePanelID="uplMainPanel"
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
            <uc1:CustomerRegistration ID="ucCustomerRegistration" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
