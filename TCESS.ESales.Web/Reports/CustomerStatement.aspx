<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CustomerStatement.aspx.cs" Inherits="Reports_CustomerStatement" %>

<%@ Register Src="UserControls/CustomerStatementData.ascx" TagName="CustomerStatementData"
    TagPrefix="uc3" %>
<%@ Register Src="UserControls/CustomerStatementReport.ascx" TagName="CustomerStatementReport"
    TagPrefix="uc4" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="CUSTOMER COLLECTION AND SETTLEMENT  REPORT"
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
            <asp:Panel ID="pnlCustomerStatementData" runat="server">
                <uc3:CustomerStatementData ID="ucCustomerStatementData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlCustomerStatementReport" runat="server">
                <uc4:CustomerStatementReport ID="ucCustomerStatementReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
