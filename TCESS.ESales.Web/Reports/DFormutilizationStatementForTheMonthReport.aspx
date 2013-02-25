<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DFormutilizationStatementForTheMonthReport.aspx.cs" Inherits="Reports_DFormutilizationStatementForTheMonthReport" %>

<%@ Register Src="UserControls/DFormutilizationStatementForTheMonthData.ascx" TagName="DFormutilizationStatementForTheMonthData"
    TagPrefix="uc3" %>
<%@ Register Src="UserControls/DFormutilizationStatementForTheMonthReport.ascx" TagName="DFormutilizationStatementForTheMonthReport"
    TagPrefix="uc1" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, DFormutilizationStatementForTheMonthReport%>"
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
            <asp:Panel ID="pnlDFormutilizationStatementForTheMonthData" runat="server">
                <uc3:DFormutilizationStatementForTheMonthData ID="ucDFormutilizationStatementForTheMonthData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlDFormutilizationStatementForTheMonthReport" runat="server">
                <uc1:DFormutilizationStatementForTheMonthReport ID="ucDFormutilizationStatementForTheMonthReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


