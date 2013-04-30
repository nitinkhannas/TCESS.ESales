<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CancelMoneyReceipt.ascx.cs"
    Inherits="Bookings_UserControls_CancelMoneyReceipt" %>
<table width="100%" cellspacing="5" cellpadding="5" class="formtext">
    <tr align="left">
        <td>
            <asp:Label ID="Label1" runat="server" Text="<%$Resources:Labels, ReceiptNo%>" />
        </td>
        <td>
            <asp:TextBox ID="txtReceiptNo" runat="server" CssClass="textbox" />
        </td>
        <td>
            <asp:Label ID="lblBookingNo" runat="server" Text="<%$Resources:Labels, BookingNo%>" />
        </td>
        <td>
            <asp:TextBox ID="txtBookingNo" runat="server" CssClass="textbox" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblCustName" runat="server" Text="<%$Resources:Labels, CustomerName%>" />
        </td>
        <td>
            <asp:TextBox ID="txtCustName" runat="server" CssClass="textbox" />
        </td>
        <td>
            <asp:Label ID="lblBookingDate" runat="server" Text="<%$Resources:Labels, BookingDate%>" />
        </td>
        <td>
            <asp:TextBox ID="txtBookingDate" runat="server" CssClass="textbox"></asp:TextBox>
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblDCAName" runat="server" Text="<%$Resources:Labels, DCAName%>" />
        </td>
        <td>
            <asp:TextBox ID="txtDCAName" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
        <td>
            <asp:Label ID="lblMaterialType" runat="server" Text="<%$Resources:Labels, MaterialType%>" />
        </td>
        <td>
            <asp:TextBox ID="txtMaterialType" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblTruckNo" runat="server" Text="<%$Resources:Labels, TruckNo%>" />
        </td>
        <td>
            <asp:TextBox ID="txtTruckNo" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
        <td>
            <asp:Label ID="lblTruckOwner" runat="server" Text="<%$Resources:Labels, TruckOwner%>" />
        </td>
        <td>
            <asp:TextBox ID="txtTruckOwner" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblDriverName" runat="server" Text="<%$Resources:Labels, DriverName%>" />
        </td>
        <td>
            <asp:TextBox ID="txtDriverName" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
        <td>
            <asp:Label ID="lbltxtAmountPaid" runat="server" Text="<%$Resources:Labels, AdvanceReceived%>" />
        </td>
        <td>
            <asp:TextBox ID="txtAmountPaid" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblRemarks" runat="server" Text="<%$Resources:Labels, Remarks%>" />
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="textarea"
                ReadOnly="true" />
        </td>
    </tr>
    <tr>
            <td colspan="4">
                
                <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
            </td>
        </tr>
</table>
<asp:Panel ID="pnlCancellationDetails" GroupingText="Cancellation Details" runat="server">
    <table width="100%" cellspacing="5" cellpadding="5" class="formtext">
        <tr align="left">
            <td>
                <asp:Label ID="lblRefundAmount" runat="server" Text="<%$Resources:Labels, RefundAmount%>" />
            </td>
            <td>
                <asp:TextBox ID="txtRefundAmt" runat="server" CssClass="textbox" />
                <asp:RequiredFieldValidator ID="RefundAmtValidator" ControlToValidate="txtRefundAmt"
                    Display="Dynamic" ValidationGroup="CancelGroup" SetFocusOnError="true" Text="*"
                    CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredRefundAmount %>"
                    runat="server" />
                <asp:CustomValidator ID="CustomAmountValidator" ControlToValidate="txtRefundAmt"
                    CssClass="failureNotification" ValidationGroup="CancelGroup" SetFocusOnError="true"
                    Text="*" runat="server" OnServerValidate="txtRefundAmt_Validate" ErrorMessage="<%$ Resources:ErrorMessages, InvalidRefundAmount %>" />
                <ajax:ValidatorCalloutExtender ID="CustomAmountValidatorCalloutExtender" runat="server"
                    TargetControlID="CustomAmountValidator" />
                <ajax:ValidatorCalloutExtender ID="RefundAmtValidatorCalloutExtender" runat="server"
                    TargetControlID="RefundAmtValidator" />
                <ajax:FilteredTextBoxExtender ID="FilteredRefundAmtExtender" runat="server" TargetControlID="txtRefundAmt"
                    FilterMode="ValidChars" ValidChars="." FilterType="Numbers,Custom" />
            </td>
            <td>
                <asp:Label ID="lblCancellationRemarks" runat="server" Text="<%$Resources:Labels, Remarks%>" />
            </td>
            <td>
                <asp:TextBox ID="txtCancellationRemarks" runat="server" TextMode="MultiLine" CssClass="textarea" />
                <asp:RequiredFieldValidator ID="txtCancellationRemarksValidator" ControlToValidate="txtCancellationRemarks"
                    Display="Dynamic" ValidationGroup="CancelGroup" SetFocusOnError="true" Text="*"
                    CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredCancellationRemark %>"
                    runat="server" />
                <ajax:ValidatorCalloutExtender ID="txtCancellationRemarksValidatorCalloutExtender"
                    runat="server" TargetControlID="txtCancellationRemarksValidator" />
            </td>
        </tr>
        <tr align="left">
            <td>
                <asp:Label ID="lblBalRefundAmount" runat="server" 
                    Text="Balance After Refund" />
            </td>
            <td>
                <asp:TextBox ID="txtBalRefundAmount" runat="server" CssClass="textbox" />
                
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <asp:Button ID="btnSave" runat="server" ValidationGroup="CancelGroup" Text="<%$Resources:Labels, Save%>"
                    CssClass="button" OnClick="btnSave_Click" />
                &nbsp;
                <asp:Button ID="btnReset" runat="server" Visible=false Text="<%$Resources:Labels, Reset%>" CssClass="button"
                    OnClick="btnReset_Click" />
                &nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="<%$Resources:Labels, Cancel%>" CssClass="button"
                    OnClick="btnCancel_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                
                
            </td>
        </tr>
    </table>
</asp:Panel>
