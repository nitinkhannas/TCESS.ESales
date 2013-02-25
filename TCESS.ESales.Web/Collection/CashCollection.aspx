<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="CashCollection.aspx.cs"
    Inherits="GhatoCashCollection_CashCollection" ValidateRequest="false" %>

<%@ Register src="UserControls/PaymentCollection.ascx" tagname="PaymentCollection" tagprefix="uc1" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <label class="pageNameContent">
        <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, CASHCOLLECTION%>"
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
            <uc1:PaymentCollection ID="ucPaymentCollection" runat="server" />            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>