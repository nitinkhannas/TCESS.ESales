<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="DispatchReport.aspx.cs" Inherits="Reports_DispatchReport" %>
	<%@ Register Src="UserControls/DispatchReportData.ascx" TagName="DispatchReportData"
    TagPrefix="uc1" %>

<%@ Register Src="UserControls/DispatchReport.ascx" TagName="DispatchReport"
    TagPrefix="uc3" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, DailyDispatchandHandlingReport%>"
        CssClass="pageNameContent" />
</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="progressBar" runat="server" AssociatedUpdatePanelID="uplMainPanel"
        DisplayAfter="0">
        <ProgressTemplate>

<style>
#ctl00_MainContent_ucDispatchReport_reportViewer{ z-index:-10000000000000000!important; border:1px solid red!important;}
</style>


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
            <asp:Panel ID="pnlDispatchData" runat="server">
                <uc1:DispatchReportData ID="ucDispatchReportData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlDispatchReport" runat="server">
                <uc3:DispatchReport ID="ucDispatchReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

