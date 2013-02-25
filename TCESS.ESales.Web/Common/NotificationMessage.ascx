<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NotificationMessage.ascx.cs"
    Inherits="Common_NotificationMessage" %>
<ajax:ModalPopupExtender ID="mdlMessageBox" runat="server" TargetControlID="pnlMessageBox"
    PopupControlID="pnlMessageBox" BackgroundCssClass="modalBackground" DropShadow="false"
    CancelControlID="btnOk" />
<asp:Panel ID="pnlMessageBox" runat="server" CssClass="modalPopup" Style="display: none;"
    DefaultButton="btnOk">
    <table cellspacing="5" border="0" width="95%" align="left">
        <tr>
            <td colspan="1" align="left" valign="top">
                <b>
                    <asp:Label ID="lblCaption" Text="Esales-Application" runat="server" /></b>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="center" height="65">
                <asp:Label ID="lblMessage" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="1" align="center" valign="bottom">
                <asp:Button ID="btnOk" OnClick="btnOk_Click" runat="server" CssClass="button" Text="Ok" />
            </td>
        </tr>
    </table>
</asp:Panel>
<script type="text/javascript">
    function fnClickOK(sender, e) {
        __doPostBack(sender, e);
    }
</script>
