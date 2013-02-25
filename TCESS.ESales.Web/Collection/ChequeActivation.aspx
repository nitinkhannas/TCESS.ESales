<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ChequeActivation.aspx.cs" Inherits="GhatoCashCollection_ChequeActivation" %>

<%@ Register Src="UserControls/ChequeActivation.ascx" TagName="ChequeActivation" TagPrefix="uc2" %>
<%@ Register src="UserControls/EditCheque.ascx" tagname="EditCheque" tagprefix="uc1" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <label class="pageNameContent">
        <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ChequeActivation%>"
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
            <asp:Panel ID="pnlChequeActivation" runat="server">
                <uc2:ChequeActivation ID="ucChequeActivation" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlEditCheque" runat="server">
                <uc1:EditCheque ID="ucEditCheque" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>