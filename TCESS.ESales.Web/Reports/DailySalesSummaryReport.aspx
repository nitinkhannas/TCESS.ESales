<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DailySalesSummaryReport.aspx.cs" Inherits="Reports_DailySalesSummaryReport" %>

<%@ Register Src="UserControls/DailySalesSummaryData.ascx" TagName="DailySalesSummaryData"
    TagPrefix="uc3" %>
    <%@ Register Src="UserControls/DailySalesSummaryReport.ascx" TagName="DailySalesSummaryReport"
    TagPrefix="uc1" %>
    <asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, DailySalesSummaryReport%>"
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
            <asp:Panel ID="pnlDailySalesSummaryData" runat="server">
               <uc3:DailySalesSummaryData ID="ucDailySalesSummaryData" runat="server" />
            </asp:Panel>   
            <asp:Panel ID="pnlDailySalesSummaryReport" runat="server">
                <uc1:DailySalesSummaryReport ID="ucDailySalesSummaryReport" runat="server" />
            </asp:Panel>        
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>

