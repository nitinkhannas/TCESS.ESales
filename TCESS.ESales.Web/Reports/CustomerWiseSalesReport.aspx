﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CustomerWiseSalesReport.aspx.cs" Inherits="Reports_CustomerWiseSalesReport" %>
<%@ Register Src="UserControls/CustomerWiseSalesData.ascx" TagName="CustomerWiseSalesData"
    TagPrefix="uc3" %>
<%@ Register Src="UserControls/CustomerWiseSalesReport.ascx" TagName="CustomerWiseSalesReport"
    TagPrefix="uc1" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, CustomerWiseSalesReport%>"
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
            <asp:Panel ID="pnlCustomerWiseSalesData" runat="server">
                <uc3:CustomerWiseSalesData ID="ucCustomerWiseSalesData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlCustomerWiseSalesReport" runat="server">
                <uc1:CustomerWiseSalesReport ID="ucCustomerWiseSalesReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

