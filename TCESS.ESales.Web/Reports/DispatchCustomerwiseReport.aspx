<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DispatchCustomerwiseReport.aspx.cs" Inherits="Reports_DispatchCustomerwiseReport" %>
<%@ Register Src="UserControls/DispatchCustomerwiseReportData.ascx" TagName="DispatchReportData"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/DispatchCustomerwiseReport.ascx" TagName="DispatchReport"
    TagPrefix="uc3" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, DispatchReportCustomerwise%>"
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
            <asp:Panel ID="pnlDispatchData" runat="server">
                <uc1:DispatchReportData ID="ucDispatchReportData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlDispatchReport" runat="server">
                <uc3:DispatchReport ID="ucDispatchReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

