<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IssueMoneyReceipt.ascx.cs"
    Inherits="Bookings_UserControls_IssueMoneyReceipt" %>
<table width="100%" cellspacing="5" cellpadding="5" class="formtext">
    <tr align="left">
        <td>
            <asp:Label ID="lblBookingNo" runat="server" Text="<%$Resources:Labels, BookingNo%>" />
        </td>
        <td>
            <asp:TextBox ID="txtBookingNo" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
        <td>
            <asp:Label ID="lblCustomerName" runat="server" Text="<%$Resources:Labels, CustomerName%>" />
        </td>
        <td>
            <asp:TextBox ID="txtCustName" runat="server" CssClass="textbox" ReadOnly="true" />
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
            <asp:Label ID="lblBookingDate" runat="server" Text="<%$Resources:Labels, BookingDate%>" />
        </td>
        <td>
            <asp:TextBox ID="txtBookingDate" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblMaterialType" runat="server" Text="<%$Resources:Labels, MaterialType%>" />
        </td>
        <td>
            <asp:TextBox ID="txtMaterialType" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
        <td>
            <asp:Label ID="lblTruckNo" runat="server" Text="<%$Resources:Labels, TruckNo%>" />
        </td>
        <td>
            <asp:TextBox ID="txtTruckNo" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblTruckOwner" runat="server" Text="<%$Resources:Labels, TruckOwner%>" />
        </td>
        <td>
            <asp:TextBox ID="txtTruckOwner" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
        <td>
            <asp:Label ID="lblDriverName" runat="server" Text="<%$Resources:Labels, DriverName%>" />
        </td>
        <td>
            <asp:TextBox ID="txtDriverName" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblAdvanceAmount0" runat="server" 
                Text="<%$Resources:Labels, TotalBookingAdvance%>" />
        </td>
        <td>
                       
                        <asp:TextBox ID="txtTotalBookingAdvance" runat="server" CssClass="textbox" 
                            ReadOnly="True" />
                        <ajax:FilteredTextBoxExtender ID="txtTotalBookingAdvance_FilteredTextBoxExtender" 
                            runat="server" FilterMode="ValidChars" FilterType="Custom, Numbers" 
                            TargetControlID="txtTotalBookingAdvance" ValidChars="." />
                       
                    </td>
        <td>
            <asp:Label ID="lblAdvAmount" runat="server" Text="<%$Resources:Labels, CurrentBookingAdvance%>" />
        </td>
        <td>
            <asp:TextBox ID="txtAdvAmount" runat="server" CssClass="textbox" MaxLength="6" 
                ReadOnly="True" />
            <asp:RequiredFieldValidator ID="AdvAmountValidator" ControlToValidate="txtAdvAmount"
                Display="Dynamic" ValidationGroup="AddGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredAdvanceAmount %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="AdvAmountCallout" runat="server" TargetControlID="AdvAmountValidator" />
            <asp:CustomValidator ControlToValidate="txtAdvAmount" ID="AdvAmountCustomValidator"
                Display="Dynamic" OnServerValidate="AdvAmount_ServerValidate" runat="server"
                ValidationGroup="AddGroup" ErrorMessage="<%$ Resources:ErrorMessages, InvalidAmount %>"
                Text="*" CssClass="failureNotification" />
            <ajax:ValidatorCalloutExtender ID="AdvAmountCustomValidatorCallout" runat="server"
                TargetControlID="AdvAmountCustomValidator" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblAdvanceAmount1" runat="server" 
                Text="<%$Resources:Labels, BalanceAdvance%>" />
        </td>
        <td>
                        <asp:TextBox ID="txtBalanceAdvance" runat="server" CssClass="textbox" 
                            ReadOnly="True" />
                       
                    </td>
        <td>
            <asp:Label ID="lblPaymentMode" runat="server" Text="<%$Resources:Labels, PaymentMode%>" />
        </td>
        <td>
            <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="listmenu" DataTextField="Paymentmode_Name"
                DataValueField="Paymentmode_Id" />
            <asp:RequiredFieldValidator ID="PaymentModeValidator" ControlToValidate="ddlPaymentMode"
                Display="Dynamic" InitialValue="1" ValidationGroup="AddGroup" SetFocusOnError="true"
                Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredPaymentMode %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="PaymentModeValidatorCallout" runat="server" TargetControlID="PaymentModeValidator" />
        </td>
    </tr>
    <tr align="left"  visible="false">
        <td>
            <asp:Label ID="lblInstrumentNo" runat="server" Text="<%$Resources:Labels, InstrumentNo%>" />
        </td>
        <td>
            <asp:TextBox ID="txtInstrumentNo" runat="server" CssClass="textbox" 
                MaxLength="16"  Text="0"></asp:TextBox>
            <asp:RequiredFieldValidator ID="InstrumentNoValidator" ControlToValidate="txtInstrumentNo"
                Display="Dynamic" ValidationGroup="AddGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredInstrumentNo %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="InstrumentNoValidatorCallout" runat="server" TargetControlID="InstrumentNoValidator" />
        </td>
        <td>
            <asp:Label ID="lblAccountName" runat="server" Text="<%$Resources:Labels, AccountName%>" />
        </td>
        <td>
            <asp:TextBox ID="txtAccountName" runat="server" CssClass="textbox" MaxLength="50" />
            <asp:CustomValidator ControlToValidate="txtAccountName" ID="CustomAccountNameValidator"
                Display="Dynamic" OnServerValidate="AccountName_ServerValidate" runat="server"
                ValidationGroup="AddGroup" ErrorMessage="<%$ Resources:ErrorMessages, RequiredAccountName %>"
                Text="*" CssClass="failureNotification" />
            <ajax:ValidatorCalloutExtender ID="CustomAccountNameValidatorCallout" runat="server"
                TargetControlID="CustomAccountNameValidator" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblRemarks" runat="server" Text="<%$Resources:Labels, Remarks%>" />
        </td>
        <td>
            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="textarea"
                MaxLength="150" />
            <asp:RegularExpressionValidator ID="RemarksValidator" runat="server" ControlToValidate="txtRemarks"
                Display="Dynamic" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup"
                ErrorMessage="<%$ Resources:ErrorMessages, LengthExceeded %>" CssClass="failureNotification"
                ValidationExpression="^[\s\S]{0,150}$" />
            <ajax:ValidatorCalloutExtender ID="RemarksValidatorCalloutExtender" runat="server"
                TargetControlID="RemarksValidator" />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
            <uc1:MessageBox ID="ucMessageBox" runat="server" />
        </td>
    </tr>
    <tr>
        <td align="center" colspan="4">
            <asp:Button ID="btnSave" runat="server" Text="<%$Resources:Labels, Save%>" CssClass="button"
                OnClick="btnCollect_Click" ValidationGroup="AddGroup" />&nbsp;
            <asp:Button ID="btnReset" runat="server" Text="<%$Resources:Labels, Reset%>" CssClass="button"
                OnClick="btnReset_Click" />
            &nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="<%$Resources:Labels, Cancel%>" CssClass="button"
                OnClick="btnCancel_Click" />
            &nbsp;
            <asp:Button ID="btnPrint" runat="server" Text="<%$Resources:Labels, PrintMoneyReceipt%>"
                Enabled="false" CssClass="button" OnClick="btnPrint_Click" />
        </td>
    </tr>
</table>
