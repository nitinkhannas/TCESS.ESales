<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DailyLoadingAdviceIssuedReportForAll.aspx.cs" Inherits="Reports_DailyLoadingAdviceIssuedReportForAll" %>
<%@ Register Src="UserControls/DailyLoadingAdviceIssuedReportForAllData.ascx" TagName="DailyLoadingAdviceIssuedReportForAllData"
    TagPrefix="uc3" %>
    <%@ Register Src="UserControls/DailyLoadingAdviceIssuedReportForAllReport.ascx" TagName="DailyLoadingAdviceIssuedReportForAllReport"
    TagPrefix="uc1" %>
    <asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, DailyLoadingAdviceIssuedReportForAll%>"
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
            <asp:Panel ID="pnlDailyLoadingAdviceIssuedReportForAllData" runat="server">
               <uc3:DailyLoadingAdviceIssuedReportForAllData ID="ucDailyLoadingAdviceIssuedReportForAllData" runat="server" />
            </asp:Panel>   
            <asp:Panel ID="pnlDailyLoadingAdviceIssuedReportForAllReport" runat="server">
                <uc1:DailyLoadingAdviceIssuedReportForAllReport ID="ucDailyLoadingAdviceIssuedReportForAllReport" runat="server" />
            </asp:Panel>        
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>
