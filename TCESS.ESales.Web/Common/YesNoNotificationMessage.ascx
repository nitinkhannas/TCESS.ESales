<%@ Control Language="C#" AutoEventWireup="true" CodeFile="YesNoNotificationMessage.ascx.cs"
    Inherits="Common_YesNoNotificationMessage" %>
<ajax:ModalPopupExtender ID="mdlMessageBox" runat="server" TargetControlID="pnlYesNoMessageBox"
    PopupControlID="pnlYesNoMessageBox" BackgroundCssClass="modalBackground" DropShadow="false"
    CancelControlID="btnNo" />
<asp:Panel ID="pnlYesNoMessageBox" runat="server" CssClass="modalPopup" Style="display: none;"
    DefaultButton="btnNo">
    <table cellspacing="5" border="0" width="95%" align="left">
        <tr>
            <td align="left" valign="top">
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
            <td  align="center" valign="bottom">
                <asp:Button ID="btnYes" OnClick="btnYes_Click" runat="server" CssClass="button" Text="Yes" />
                <asp:Button ID="btnNo" OnClick="btnNo_Click" runat="server" CssClass="button" Text="No" />
            </td>
        </tr>
    </table>
</asp:Panel>
<script type="text/javascript" language="javascript">
    function fnClickNo(sender, e) {
        __doPostBack(sender, e);
    }
    function fnClickYes(sender, e) {
        __doPostBack(sender, e);
    }
</script>