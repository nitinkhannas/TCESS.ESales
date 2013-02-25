<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="DFormReport.aspx.cs" Inherits="Reports_DFormReport" %>

<%@ Register Src="UserControls/DformReport.ascx" TagName="DformReport" TagPrefix="uc2" %>
<%@ Register Src="UserControls/DformReportData.ascx" TagName="DformReportData" TagPrefix="uc1" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="Label1" runat="server" Text="D Form Report" CssClass="pageNameContent" />
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
            <asp:Panel ID="pnlDformReportData" runat="server">
                <uc1:DformReportData ID="ucDformReportData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlDformReportPrint" runat="server">
                <uc2:DformReport ID="ucDformReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
