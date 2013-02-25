<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MonthlyHandlingIncomeAndServiceTaxStatementReport.aspx.cs" Inherits="Reports_MonthlyHandlingIncomeAndServiceTaxStatementReport" %>

<%@ Register Src="UserControls/MonthlyHandlingIncomeAndServiceTaxStatementData.ascx" TagName="MonthlyHandlingIncomeAndServiceTaxStatementData"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/MonthlyHandlingIncomeAndServiceTaxStatementReport.ascx" TagName="MonthlyHandlingIncomeAndServiceTaxStatementReport"
    TagPrefix="uc3" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, MonthlyHandlingIncomeAndServiceTaxStatementReport%>"
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
            <asp:Panel ID="pnlMonthlyHandlingIncomeAndServiceTaxStatementData" runat="server">
                <uc1:MonthlyHandlingIncomeAndServiceTaxStatementData ID="ucMonthlyHandlingIncomeAndServiceTaxStatementData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlMonthlyHandlingIncomeAndServiceTaxStatementReport" runat="server">
                <uc3:MonthlyHandlingIncomeAndServiceTaxStatementReport ID="ucMonthlyHandlingIncomeAndServiceTaxStatementReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

