<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DailyBookingStatusReportWithBarChart.aspx.cs" Inherits="Reports_UserControls_DailyBookingStatusReportWithBarChart" %>

<%@ Register Src="UserControls/DailyBookingStatusDataWithBarChart.ascx" TagName="DailyBookingStatusDataWithBarChart"
    TagPrefix="uc3" %>   
     
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
<div align ="center">
    <asp:Label ID="lblPageName" runat="server" Text="Booking And Truck Tracker"
        CssClass="pageNameContent" />
        </div>
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
            <asp:Panel ID="pnlDailyBookingStatusDataWithBarChart" runat="server">
                <uc3:DailyBookingStatusDataWithBarChart ID="ucDailyBookingStatusDataWithBarChart" runat="server" />
            </asp:Panel>            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
