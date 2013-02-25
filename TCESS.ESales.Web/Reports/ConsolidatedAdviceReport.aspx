<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ConsolidatedAdviceReport.aspx.cs" Inherits="Reports_ConsolidatedAdviceReport" %>
<%@ Register Src="UserControls/ConsolidatedAdviceData.ascx" TagName="ConsolidatedAdviceData"
    TagPrefix="uc3" %>
    <%@ Register Src="UserControls/ConsolidatedAdviceReport.ascx" TagName="ConsolidatedAdviceReport"
    TagPrefix="uc1" %>
    <asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ConsolidatedAdviceReport%>"
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
            <asp:Panel ID="pnlConsolidatedAdviceData" runat="server">
               <uc3:ConsolidatedAdviceData ID="ucConsolidatedAdviceData" runat="server" />
            </asp:Panel>   
            <asp:Panel ID="pnlConsolidatedAdviceReport" runat="server">
                <uc1:ConsolidatedAdviceReport ID="ucConsolidatedAdviceReport" runat="server" />
            </asp:Panel>        
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>