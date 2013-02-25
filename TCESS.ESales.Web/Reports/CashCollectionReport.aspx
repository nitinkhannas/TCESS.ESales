<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CashCollectionReport.aspx.cs" Inherits="Reports_CashCollectionReport" %>

<%@ Register Src="UserControls/CashCollectionData.ascx" TagName="CashCollectionData"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/CashCollectionReport.ascx" TagName="CashCollectionReport"
    TagPrefix="uc3" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, CashCollectionReport%>"
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
            <asp:Panel ID="pnlCashCollectionData" runat="server">
                <uc1:CashCollectionData ID="ucCashCollectionData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlCashCollectionDataReport" runat="server">
                <uc3:CashCollectionReport ID="ucCashCollectionReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
