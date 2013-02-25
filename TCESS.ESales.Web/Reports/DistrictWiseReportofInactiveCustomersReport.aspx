<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DistrictWiseReportofInactiveCustomersReport.aspx.cs" Inherits="Reports_DistrictWiseReportofInactiveCustomersReport" %>
<%@ Register Src="UserControls/DistrictWiseReportofInactiveCustomersData.ascx" TagName="DistrictWiseReportofInactiveCustomersData"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/DistrictWiseReportofInactiveCustomersReport.ascx" TagName="DistrictWiseReportofInactiveCustomersReport"
    TagPrefix="uc3" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, DistrictWiseReportofInactiveCustomers%>"
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
            <asp:Panel ID="pnlDistrictWiseReportofInactiveCustomersData" runat="server">
                <uc1:DistrictWiseReportofInactiveCustomersData ID="ucDistrictWiseReportofInactiveCustomersData" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlDistrictWiseReportofInactiveCustomersReport" runat="server">
                <uc3:DistrictWiseReportofInactiveCustomersReport ID="ucDistrictWiseReportofInactiveCustomersReport" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


