<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManageInactiveTrucks.aspx.cs"
    Inherits="Supervisor_ManageInactiveTrucks" %>

<%@ Register Src="UserControls/ManageInactiveTrucks.ascx" TagName="ManageInactiveTrucks"
    TagPrefix="uc2" %>
<asp:content id="PageContent" contentplaceholderid="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManageInactiveTrucks%>"
        CssClass="pageNameContent" />
</asp:content>
<asp:content id="BodyContent" contentplaceholderid="MainContent" runat="Server">
    <asp:updateprogress id="progressBar" runat="server" associatedupdatepanelid="uplMainPanel"
    displayafter="0">
        <ProgressTemplate>
            <div class="overlay">
                <div class="ajaxloader">
                    <img src='<%= ResolveClientUrl("~/Images/ajax-loader.gif")%>' style="vertical-align: middle"
                        alt="Processing" />Processing....
                </div>
            </div>
        </ProgressTemplate>
    </asp:updateprogress>
    <asp:UpdatePanel runat="server" ID="uplMainPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlManageStandaloneTrucks" runat="server">
                <uc2:ManageInactiveTrucks ID="ucManageStandaloneTrucks" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>