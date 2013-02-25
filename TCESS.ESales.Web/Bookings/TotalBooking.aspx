<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="TotalBooking.aspx.cs" Inherits="Bookings_TotalBooking" %>

    <%@ Register Src="UserControls/Reprint.ascx" TagName="Reprint" TagPrefix="uc1" %>
<%@ Register Src="UserControls/LoadingAdviceReport.ascx" TagName="MoneyReceiptRpt"
    TagPrefix="uc2" %>
<%@ Register Src="~/TruckOut/UserControls/HandlingBillReport.ascx" TagName="HandleBillRpt"
    TagPrefix="uc3" %>


<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="Re Print Documents"
        CssClass="pageNameContent" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
            <asp:Panel ID="pnlReprint" runat="server">
                <uc1:Reprint ID="ucReprint" runat="server" />
                
            </asp:Panel>
            <asp:Panel ID="PnlMoneyReceipt" runat="server">
                <uc2:MoneyReceiptRpt ID="ucMoneyReceiptReport" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PnlHandleBillReport" runat="server">
                <uc3:HandleBillRpt ID="ucHandleBillRpt" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
