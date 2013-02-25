<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ConsolidatedSMSReport.aspx.cs" Inherits="Reports_ConsolidatedSMSBookingReport" %>
<%@ Register Src="UserControls/ConsolidatedSMSData.ascx" TagName="ConsolidatedSMSData"
    TagPrefix="uc3" %>
<%@ Register Src="UserControls/ConsolidatedSMSReport.ascx" TagName="ConsolidatedSMSReport"
    TagPrefix="uc1" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ConsolidatedSMSReport%>"
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
            <asp:Panel ID="pnlConsolidatedSMSData" runat="server">
                <uc3:ConsolidatedSMSData ID="ucConsolidatedSMSData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlConsolidatedSMSReport" runat="server">
                <uc1:ConsolidatedSMSReport ID="ucConsolidatedSMSReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
