<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageAuthorizedRepresentative.aspx.cs" Inherits="CustomerRegistration_ManageAuthorizedRepresentative" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<%@ Register Src="UserControls/ManageAuthorizedRepresentative.ascx" TagName="ManageAuthorizedRepresentative"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/AuthorizedRepresentative.ascx" TagName="AuthorizedRepresentative"
    TagPrefix="uc2" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManageAuthorizedRepresentative%>"
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
            <asp:Panel ID="pnlManageAuthRep" runat="server">
                <uc1:ManageAuthorizedRepresentative ID="ucManageAuthorizedRepresentative" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlAuthRepRegistration" runat="server">
                <uc2:AuthorizedRepresentative ID="ucAuthorizedRepresentative" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
