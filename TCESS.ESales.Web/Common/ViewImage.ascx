<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewImage.ascx.cs" Inherits="Common_ViewImage" %>
<ajax:ModalPopupExtender ID="mdlMessageBox" runat="server" TargetControlID="pnlMessageBox"
    PopupControlID="pnlMessageBox" BackgroundCssClass="modalBackground" DropShadow="false"
    CancelControlID="btnOk" />
<asp:Panel ID="pnlMessageBox" runat="server" HorizontalAlign="Center" Width="700" Height="450" CssClass="ImagePopup" Style="display: none;vertical-align:middle;"
    DefaultButton="btnOk">
    <table cellspacing="5" border="0" width="95%" align="left">
        <tr>
            <td colspan="1" align="left" valign="top">
                <b>
                    <asp:Label ID="lblCaption" Text="Esales-Application" runat="server" /></b>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="center">
                <asp:Image runat="server" ID="imgBlob" Width="685" Height="380" />
            </td>
        </tr>
        <tr>
            <td colspan="1" align="right" valign="bottom">
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