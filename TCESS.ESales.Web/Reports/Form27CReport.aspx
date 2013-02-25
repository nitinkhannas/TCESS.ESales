<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Form27CReport.aspx.cs" Inherits="Reports_Form27CReport" %>

<%@ Register Src="UserControls/Form27CReport.ascx" TagName="Form27CReport" TagPrefix="uc2" %>
<%@ Register Src="UserControls/Form27CReportData.ascx" TagName="Form27CReportData"
    TagPrefix="uc1" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Form 27C Report" CssClass="pageNameContent" />
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
            <asp:Panel ID="pnlForm27CReportData" runat="server">
                <uc1:Form27CReportData ID="ucForm27CReportData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlForm27CReportPrint" runat="server">
                <uc2:Form27CReport ID="ucForm27CReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
