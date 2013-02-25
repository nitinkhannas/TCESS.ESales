<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DailyBookingReportforallDCAsReport.aspx.cs" Inherits="Reports_DailyBookingReportforallDCAsReport" %>

<%@ Register Src="UserControls/DailyBookingReportforallDCAsData.ascx" TagName="DailyBookingReportforallDCAsData"
    TagPrefix="uc3" %>
    <%@ Register Src="UserControls/DailyBookingReportforallDCAsReport.ascx" TagName="DailyBookingReportforallDCAsReport"
    TagPrefix="uc1" %>
    <asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, DailyBookingReportforallDCAsReport%>"
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
            <asp:Panel ID="pnlDailyBookingReportforallDCAsData" runat="server">
               <uc3:DailyBookingReportforallDCAsData ID="ucDailyBookingReportforallDCAsData" runat="server" />
            </asp:Panel>   
            <asp:Panel ID="pnlDailyBookingReportforallDCAsReport" runat="server">
                <uc1:DailyBookingReportforallDCAsReport ID="ucDailyBookingReportforallDCAsReport" runat="server" />
            </asp:Panel>        
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>

