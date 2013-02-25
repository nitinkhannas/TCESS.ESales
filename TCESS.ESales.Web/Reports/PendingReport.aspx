<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PendingReport.aspx.cs" Inherits="Reports_PendingReport" %>

<%@ Register Src="UserControls/PendingReport.ascx" TagName="PendingReport" TagPrefix="uc2" %>
<%@ Register Src="UserControls/PendingReportData.ascx" TagName="PendingReportData"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, PendingReport%>"
        CssClass="pageNameContent" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlPendingReportData" runat="server">
                <uc1:PendingReportData ID="ucPendingReportData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlPendingReport" runat="server">
                <uc2:PendingReport ID="ucPendingReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
