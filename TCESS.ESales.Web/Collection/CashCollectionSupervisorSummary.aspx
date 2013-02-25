<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CashCollectionSupervisorSummary.aspx.cs" Inherits="Collection_CashCollectionSupervisorSummary" %>
<%@ Register src="UserControls/CollectionSupervisorSummary.ascx" tagname="CollectionSupervisorSummary" tagprefix="uc1" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <label class="pageNameContent">
        <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, CASHCOLLECTIONSUPERVISORSUMMARY%>"
            CssClass="pageNameContent" />
    </label>
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
            <uc1:CollectionSupervisorSummary ID="ucCollectionSupervisorSummary" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>