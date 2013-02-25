<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NotificationMessageForGrid.ascx.cs"
    Inherits="Common_NotificationMessageForGrid" %>
<ajax:ModalPopupExtender ID="mdlMessageBox" runat="server" TargetControlID="pnlMessageBox"
    PopupControlID="pnlMessageBox" BackgroundCssClass="modalBackground" DropShadow="false"
    CancelControlID="btnOk" OkControlID="btnOk" />
<asp:Panel ID="pnlMessageBox" runat="server" CssClass="modalPopup" Style="display: none;"
    DefaultButton="btnOk">
    <table cellspacing="5" width="95%" align="center" border="0" >
        <tr>
            <td colspan="1" align="left" valign="middle" style=" padding:6px;">
                <b>
                    <asp:Label ID="lblCaption" Text="Esales-Application" runat="server" /></b>
            </td>
        </tr>
        <tr>
            <td valign="top" align="center" height="125" style=" padding:6px;">
                <asp:Label ID="lblMessage" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="1" align="center" valign="bottom">
                <asp:Button ID="btnOk" OnClick="btnOk_Click" runat="server" CssClass="button" Text="Ok"  />
            </td>
        </tr>
    </table>
</asp:Panel>
<script type="text/javascript">
    function fnClickOK(sender, e) {
        __doPostBack(sender, e);
    }
</script>
