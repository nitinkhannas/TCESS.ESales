﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MonthlySalesStatementandReconciliationReport.aspx.cs" Inherits="Reports_MonthlySalesStatementandReconciliationReport" %>
<%@ Register Src="UserControls/MonthlySalesStatementandReconciliationData.ascx" TagName="MonthlySalesStatementandReconciliationData"
    TagPrefix="uc3" %>
<%@ Register Src="UserControls/MonthlySalesStatementandReconciliationReport.ascx" TagName="MonthlySalesStatementandReconciliationReport"
    TagPrefix="uc1" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, MonthlySalesStatementandReconciliationReport%>"
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
            <asp:Panel ID="pnlMonthlySalesStatementandReconciliationData" runat="server">
                <uc3:MonthlySalesStatementandReconciliationData ID="ucMonthlySalesStatementandReconciliationData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlMonthlySalesStatementandReconciliationReport" runat="server">
                <uc1:MonthlySalesStatementandReconciliationReport ID="ucMonthlySalesStatementandReconciliationReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

