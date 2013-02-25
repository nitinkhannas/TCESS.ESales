<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SMSBookingsAcceptedCompletedLapsedReport.aspx.cs" Inherits="Reports_SMSBookingsAcceptedCompletedLapsedReport" %>

<%@ Register Src="UserControls/SMSBookingsAcceptedCompletedLapsedData.ascx" TagName="SMSBookingsAcceptedCompletedLapsedData"
    TagPrefix="uc3" %>
<%@ Register Src="UserControls/SMSBookingsAcceptedCompletedLapsedReport.ascx" TagName="SMSBookingsAcceptedCompletedLapsedReport"
    TagPrefix="uc1" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, SMSBookingsAcceptedCompletedLapsedReport%>"
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
            <asp:Panel ID="pnlSMSBookingsAcceptedCompletedLapsedData" runat="server">
                <uc3:SMSBookingsAcceptedCompletedLapsedData ID="ucSMSBookingsAcceptedCompletedLapsedData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlSMSBookingsAcceptedCompletedLapsedReport" runat="server">
                <uc1:SMSBookingsAcceptedCompletedLapsedReport ID="ucSMSBookingsAcceptedCompletedLapsedReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

