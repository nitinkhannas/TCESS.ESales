<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SMSBookingReport.aspx.cs" Inherits="Reports_SMSBookingReport" %>

<%@ Register Src="UserControls/SMSBookingData.ascx" TagName="SMSBookingData" TagPrefix="uc3" %>
<%@ Register Src="UserControls/SMSBookingReport.ascx" TagName="SMSBookingReport"
    TagPrefix="uc1" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <table width="100%" cellspacing="10" cellpadding="5">
        <tr>
            <td>
                <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, SMSBookingReport%>"
                    CssClass="pageNameContent" />
            </td>
            <td>
                <asp:Label ID="lblSmsR" runat="server" Text="<%$Resources:Labels, SMSReceived%>"
                    class="formlabel" />
                <asp:Label ID="lblSmsReceived" runat="server" />
            </td>
            <td>
                <asp:Label ID="lblSmsL" runat="server" Text="<%$Resources:Labels, SMSLimit%>" class="formlabel" />
                <asp:Label ID="lblSmsLimit" runat="server" />
            </td>
            <td>
                <asp:Label ID="lblSmsA" runat="server" Text="<%$Resources:Labels, SMSAccepted%>"
                    class="formlabel" />
                <asp:Label ID="lblSmsAccepted" runat="server" />
            </td>
            <td>
                <asp:Label ID="lblSmsB" runat="server" Text="<%$Resources:Labels, SMSBalance%>" class="formlabel" />
                <asp:Label ID="lblSmsBalance" runat="server" />
            </td>
        </tr>
    </table>
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
            <asp:Panel ID="pnlLoadingSMSBookingData" runat="server">
                <uc3:SMSBookingData ID="ucLoadingSMSBookingData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlLoadingSMSBookingReport" runat="server">
                <uc1:SMSBookingReport ID="ucLoadingSMSBookingReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
