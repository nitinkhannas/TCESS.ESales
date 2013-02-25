<%@ Page Language="C#"  MasterPageFile="~/Site.master" AutoEventWireup="true" 
CodeFile="ActivateForm27C.aspx.cs" Inherits="Administrator_ActivateForm27C" %>

<%@ Register Src="UserControls/ActivateForm27C.ascx" TagName="ActivateForm27C"
    TagPrefix="uc1" %>
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
                <uc1:ActivateForm27C ID="ucActivateCustomers" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
