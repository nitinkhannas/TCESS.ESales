<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DailyBookingStatusReport.aspx.cs" Inherits="Reports_UserControls_DailyBookingStatusReport" %>

<%@ Register Src="UserControls/DailyBookingStatusData.ascx" TagName="DailyBookingStatusData"
    TagPrefix="uc3" %>   
     
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
<div align ="center">
    <asp:Label ID="lblPageName" runat="server" Text="Booking And Truck Tracker"
        CssClass="pageNameContent" />
        </div>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <%--<asp:UpdateProgress ID="progressBar" runat="server" AssociatedUpdatePanelID="uplMainPanel"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="overlay">
                <div class="ajaxloader">
                    <img src='<%= ResolveClientUrl("~/Images/ajax-loader.gif")%>' style="vertical-align: middle"
                        alt="Processing" />Processing....
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
   <%-- <asp:UpdatePanel runat="server" ID="uplMainPanel" UpdateMode="Conditional">
        <ContentTemplate>--%>
            <asp:Panel ID="pnlDailyBookingStatusData" runat="server">
                <uc3:DailyBookingStatusData ID="ucDailyBookingStatusData" runat="server" />
            </asp:Panel>            
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
