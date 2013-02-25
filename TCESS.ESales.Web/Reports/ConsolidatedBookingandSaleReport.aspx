<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ConsolidatedBookingandSaleReport.aspx.cs" Inherits="Reports_ConsolidatedBookingandSaleReport" %>

<%@ Register Src="UserControls/ConsolidatedBookingandSaleData.ascx" TagName="ConsolidatedBookingandSaleData"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/ConsolidatedBookingandSaleReport.ascx" TagName="ConsolidatedBookingandSaleReport"
    TagPrefix="uc3" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ConsolidatedDailyBookingandSaleReport%>"
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
            <asp:Panel ID="pnlConsolidatedBookingandSaleData" runat="server">
                <uc1:ConsolidatedBookingandSaleData ID="ucConsolidatedBookingandSaleData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlConsolidatedBookingandSaleReport" runat="server">
                <uc3:ConsolidatedBookingandSaleReport ID="ucConsolidatedBookingandSaleReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

