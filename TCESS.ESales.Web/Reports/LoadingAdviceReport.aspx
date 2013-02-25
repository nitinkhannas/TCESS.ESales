<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LoadingAdviceReport.aspx.cs" Inherits="Reports_LoadingAdviceReport" %>

<%@ Register Src="UserControls/LoadingAdviceData.ascx" TagName="LoadingAdviceData"
    TagPrefix="uc3" %>
<%@ Register Src="UserControls/LoadingAdviceReport.ascx" TagName="LoadingAdviceReport"
    TagPrefix="uc1" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, DailyBookingReport%>"
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
            <asp:Panel ID="pnlLoadingAdviceData" runat="server">
                <uc3:LoadingAdviceData ID="ucLoadingAdviceData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlLoadingAdviceReport" runat="server">
                <uc1:LoadingAdviceReport ID="ucLoadingAdviceReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
