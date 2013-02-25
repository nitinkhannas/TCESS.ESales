<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RoadPermitsUtilisationStatementForTheMonthReport.aspx.cs" Inherits="Reports_RoadPermitsUtilisationStatementForTheMonthReport" %>

<%@ Register Src="UserControls/RoadPermitsUtilisationStatementForTheMonthData.ascx" TagName="RoadPermitsUtilisationStatementForTheMonthData"
    TagPrefix="uc3" %>
<%@ Register Src="UserControls/RoadPermitsUtilisationStatementForTheMonthReport.ascx" TagName="RoadPermitsUtilisationStatementForTheMonthReport"
    TagPrefix="uc1" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, RoadPermitsUtilisationStatementForTheMonthReport%>"
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
            <asp:Panel ID="pnlRoadPermitsUtilisationStatementForTheMonthData" runat="server">
                <uc3:RoadPermitsUtilisationStatementForTheMonthData ID="ucRoadPermitsUtilisationStatementForTheMonthData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlRoadPermitsUtilisationStatementForTheMonthReport" runat="server">
                <uc1:RoadPermitsUtilisationStatementForTheMonthReport ID="ucRoadPermitsUtilisationStatementForTheMonthReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

