<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="RoadPermitReport.aspx.cs" Inherits="Reports_RoadPermitReport" %>

<%@ Register Src="UserControls/RoadPermitReportData.ascx" TagName="RoadPermitReportData"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/RoadPermitReport.ascx" TagName="RoadPermitReport"
    TagPrefix="uc2" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Road Permit Report" CssClass="pageNameContent" />
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
            <asp:Panel ID="pnlRoadPermitReportData" runat="server">
                <uc1:RoadPermitReportData ID="ucRoadPermitReportData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlRoadPermitReportPrint" runat="server">
                <uc2:RoadPermitReport ID="ucRoadPermitReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
