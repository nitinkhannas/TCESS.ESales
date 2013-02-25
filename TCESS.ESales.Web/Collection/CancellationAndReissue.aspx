<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CancellationAndReissue.aspx.cs" Inherits="Collection_CancellationAndReissue" %>

<%@ Register Src="UserControls/CancelCollectionReceipt.ascx" TagName="CancelCollectionReceipt"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/PaymentCollection.ascx" TagName="PaymentCollection"
    TagPrefix="uc2" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
<script type="text/javascript" language="javascript">
    function fnClickNo(sender, e) {
        __doPostBack(sender, e);
    }
    function fnClickYes(sender, e) {
        __doPostBack(sender, e);
    }
</script>
    <label class="pageNameContent">
        <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, CANCELLATIONANDREISSUE%>"
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
            <asp:Panel ID="pnlCancelCollectionReceipt" runat="server" Visible="true">
                <uc1:CancelCollectionReceipt ID="ucCancelCollectionReceipt" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlReIssue" runat="server" Visible="false">
                <uc2:PaymentCollection ID="ucPaymentCollection" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
