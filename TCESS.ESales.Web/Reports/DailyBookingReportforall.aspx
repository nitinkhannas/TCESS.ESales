<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DailyBookingReportforall.aspx.cs" Inherits="Reports_DailyBookingReportforall" %>

<%@ Register Src="UserControls/DailyBookingReportforallData.ascx" TagName="DailyBookingReportforallData"
    TagPrefix="uc3" %>
    <%@ Register Src="UserControls/DailyBookingReportforallReport.ascx" TagName="DailyBookingReportforallDCAsReport"
    TagPrefix="uc1" %>
    <asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, DailyBookingReportforallReport%>"
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
            <asp:Panel ID="pnlDailyBookingReportforallData" runat="server">
               <uc3:DailyBookingReportforallData ID="ucDailyBookingReportforallData" runat="server" />
            </asp:Panel>   
            <asp:Panel ID="pnlDailyBookingReportforallReport" runat="server">
                <uc1:DailyBookingReportforallDCAsReport ID="ucDailyBookingReportforallReport" runat="server" />
            </asp:Panel>        
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>
