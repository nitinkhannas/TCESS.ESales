<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" 
        CodeFile="RTGSRefund.aspx.cs" Inherits="Collection_RTGSRefund" ValidateRequest="false" %>

<%@ Register src="UserControls/PaymentRefund.ascx" tagname="PaymentRefund" tagprefix="uc1" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <label class="pageNameContent">
        <asp:Label ID="lblPageName" runat="server" Text="RTGS Refund"
            CssClass="pageNameContent" />
    </label>
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
            <uc1:PaymentRefund ID="ucPaymentRefund" runat="server" />            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

