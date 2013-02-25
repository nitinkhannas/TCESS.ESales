<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DCACollectionSupervisorReport.aspx.cs" Inherits="Reports_DCACollectionSupervisorReport" %>
<%@ Register Src="UserControls/DCACollectionSupervisorData.ascx" TagName="DCACollectionSupervisorData"
    TagPrefix="uc3" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
<div align ="center">
    <asp:Label ID="lblPageName" runat="server" Text="DCA Collection Supervisor"
        CssClass="pageNameContent" />
        </div>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Panel ID="pnlDCACollectionSupervisorData" runat="server">
        <uc3:DCACollectionSupervisorData ID="ucDCACollectionSupervisorData" runat="server" />
    </asp:Panel>
</asp:Content>