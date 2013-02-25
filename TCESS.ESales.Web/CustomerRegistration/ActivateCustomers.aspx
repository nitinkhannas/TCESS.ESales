﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ActivateCustomers.aspx.cs" Inherits="CustomerRegistration_ActivateCustomers" %>

<%@ Register Src="UserControls/ActivateCustomers.ascx" TagName="ActivateCustomers"
    TagPrefix="uc1" %>
<%@ Register Src="~/CustomerRegistration/UserControls/CustomerAlloc.ascx" TagName="PrintCustomers" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" CssClass="pageNameContent" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlActivateCustomers" runat="server">
                <uc1:ActivateCustomers ID="ucActivateCustomers" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlPrintCustomers" runat="server">
                <uc2:PrintCustomers ID="ucPrintCustomers" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
