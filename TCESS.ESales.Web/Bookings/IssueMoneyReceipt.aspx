<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="IssueMoneyReceipt.aspx.cs" Inherits="Bookings_IssueMoneyReceipt" %>

<%@ Register Src="UserControls/TotalMoneyReciepts.ascx" TagName="TotalMoneyReciepts"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/IssueMoneyReceipt.ascx" TagName="IssueMoneyReceipt"
    TagPrefix="uc2" %>
<%@ Register Src="UserControls/LoadingAdviceReport.ascx" TagName="LoadingAdviceReport"
    TagPrefix="uc3" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, CashCollection%>"
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
            <asp:Panel ID="pnlTotalMoneyReceipts" runat="server">
                <uc1:TotalMoneyReciepts ID="ucTotalMoneyReciepts" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlIssueMoneyReceipt" runat="server">
                <uc2:IssueMoneyReceipt ID="ucIssueMoneyReceipt" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlPrintLoadingAdviceReport" runat="server">
                <uc3:LoadingAdviceReport ID="ucLoadingAdviceReport" runat="server" />
            </asp:Panel>
            <uc1:MessageBox ID="ucMessageBox" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
