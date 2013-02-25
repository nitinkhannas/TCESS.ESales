<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageMoneyReceipt.aspx.cs" Inherits="Bookings_ManageMoneyReceipt" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<%@ Register src="UserControls/ManageMoneyReceipt.ascx" tagname="ManageMoneyReceipt" tagprefix="uc1" %>
<%@ Register src="UserControls/CancelMoneyReceipt.ascx" tagname="CancelMoneyReceipt" tagprefix="uc2" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, CancelRefundMoneyReceipt%>"
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
            <asp:Panel ID="pnlManageMoneyReceipts" runat="server">
                <uc1:ManageMoneyReceipt ID="ucManageMoneyReceipt" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlCancelMoneyReceipt" runat="server">
                <uc2:CancelMoneyReceipt ID="ucCancelMoneyReceipt" runat="server" />
            </asp:Panel>
            <uc1:MessageBox ID="ucMessageBox" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>