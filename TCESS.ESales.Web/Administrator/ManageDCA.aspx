<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageDCA.aspx.cs" Inherits="Administrator_ManageDCA" %>

<%@ Register Src="UserControls/ManageDCA.ascx" TagName="ManageDCA" TagPrefix="uc2" %>
<%@ Register src="UserControls/AddEditDCA.ascx" tagname="AddEditDCA" tagprefix="uc1" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManageDCA%>"
        CssClass="pageNameContent" />
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
            <asp:Panel ID="pnlManageDCA" runat="server">
                <uc2:ManageDCA ID="ucManageDCA" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlAddEditDCA" runat="server">
                <uc1:AddEditDCA ID="ucAddEditDCA" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
