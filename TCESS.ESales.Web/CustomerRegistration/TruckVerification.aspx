<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="TruckVerification.aspx.cs" Inherits="CustomerRegistration_TruckVerification_" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<%@ Register Src="UserControls/TruckVerification.ascx" TagName="VerifyTrucks" TagPrefix="uc1" %>
<%@ Register Src="~/Reports/UserControls/Printingdatareport.ascx" TagName="PrintSlip" TagPrefix="uc3" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, TruckVerification%>"
        CssClass="pageNameContent" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uplMainPanel"
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
            <asp:Panel ID="pnlManageTrucks" runat="server">
                <uc1:VerifyTrucks ID="ucVerifyTrucks" runat="server" />
            </asp:Panel>
              <asp:Panel ID="pnlManageTrucksReport" runat="server">             
              <uc3:PrintSlip ID="ucPrintBookingSlip" runat ="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
