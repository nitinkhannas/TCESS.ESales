<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DistrictWiseSalesReport.aspx.cs" Inherits="Reports_DistrictWiseSalesReport" %>
<%@ Register Src="UserControls/DistrictWiseSalesData.ascx" TagName="DistrictWiseSalesData"
    TagPrefix="uc3" %>
<%@ Register Src="UserControls/DistrictWiseSalesReport.ascx" TagName="DistrictWiseSalesReport"
    TagPrefix="uc1" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, DistrictWiseSalesReport%>"
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
            <asp:Panel ID="pnlDistrictWiseSalesData" runat="server">
                <uc3:DistrictWiseSalesData ID="ucDistrictWiseSalesData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlDistrictWiseSalesReport" runat="server">
                <uc1:DistrictWiseSalesReport ID="ucDistrictWiseSalesReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
