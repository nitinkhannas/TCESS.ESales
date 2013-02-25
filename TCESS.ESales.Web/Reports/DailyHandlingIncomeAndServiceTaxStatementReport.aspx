<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DailyHandlingIncomeAndServiceTaxStatementReport.aspx.cs" Inherits="Reports_DailyHandlingIncomeAndServiceTaxStatementReport" %>

<%@ Register Src="UserControls/DailyHandlingIncomeAndServiceTaxStatementData.ascx" TagName="DailyHandlingIncomeAndServiceTaxStatementData"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/DailyHandlingIncomeAndServiceTaxStatementReport.ascx" TagName="DailyHandlingIncomeAndServiceTaxStatementReport"
    TagPrefix="uc3" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, DailyHandlingIncomeAndServiceTaxStatementReport%>"
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
            <asp:Panel ID="pnlDailyHandlingIncomeAndServiceTaxStatementData" runat="server">
                <uc1:DailyHandlingIncomeAndServiceTaxStatementData ID="ucDailyHandlingIncomeAndServiceTaxStatementData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlDailyHandlingIncomeAndServiceTaxStatementReport" runat="server">
                <uc3:DailyHandlingIncomeAndServiceTaxStatementReport ID="ucDailyHandlingIncomeAndServiceTaxStatementReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

