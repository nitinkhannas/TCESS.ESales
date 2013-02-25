<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CounterCreation.ascx.cs"
    Inherits="Bookings_UserControls_CounterCreation" %>
<table width="100%" cellspacing="5" cellpadding="5" class="formtext">
    <tr align="left">
        <td>
            <asp:Label ID="lblCounterName" runat="server" Text="<%$Resources:Labels, CounterName%>" />
        </td>
        <td>
            <asp:TextBox ID="txtCounterName" runat="server" CssClass="textbox" />
            <asp:RequiredFieldValidator ID="CounterNameValidator" ControlToValidate="txtCounterName"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredCounterName%>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="CounterNameValidatorCalloutExtender" runat="server"
                TargetControlID="CounterNameValidator" />
        </td>
        <td>
            <asp:Label ID="lblMacId" runat="server" Text="<%$Resources:Labels, MacId%>" />
        </td>
        <td>
            <asp:TextBox ID="txtMacId" runat="server" CssClass="textbox" />
            <asp:RequiredFieldValidator ID="MacIDValidator" ControlToValidate="txtMacId" Display="Dynamic"
                ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                ErrorMessage="<%$ Resources:ErrorMessages, RequiredMacId%>" runat="server" />
            <ajax:ValidatorCalloutExtender ID="MacIDValidatorCalloutExtender" runat="server"
                TargetControlID="MacIDValidator" />
            <asp:CustomValidator ID="txtMacIdCustomValidator" runat="server" ControlToValidate="txtMacId"
                Text="*" OnServerValidate="CheckMACId_ServerValidate" CssClass="failureNotification"
                SetFocusOnError="true" Display="Dynamic" ValidationGroup="SaveGroup" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateMACId %>" />
            <ajax:ValidatorCalloutExtender ID="txtMacIdCustomValidatorCalloutExtender"
                runat="server" TargetControlID="txtMacIdCustomValidator" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblCustomerName" runat="server" Text="<%$Resources:Labels, UserName%>" />
        </td>
        <td>
            <asp:DropDownList AutoPostBack="true" ID="ddlUser" runat="server" CssClass="listmenu"
                OnSelectedIndexChanged="ddlUser_SelectedIndexChanged" />
            <asp:RequiredFieldValidator ID="UserValidator" ControlToValidate="ddlUser" InitialValue="0"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredUser %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="UserValidatorCalloutExtender" runat="server" TargetControlID="UserValidator" />
        </td>
        <td>
            <asp:Label ID="lblAgent" runat="server" Text="<%$Resources:Labels, DCAName%>" />
        </td>
        <td>
            <asp:TextBox ID="txtAgent" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <uc1:MessageBox ID="ucMessageBox" runat="server" />
        </td>
    </tr>
    <tr>
        <td align="center" colspan="4">
            <asp:Button ID="btnSave" runat="server" Text="<%$Resources:Labels, Save%>" CssClass="button"
                OnClick="btnSave_Click" ValidationGroup="SaveGroup" />&nbsp;
            <asp:Button ID="btnReset" runat="server" Text="<%$Resources:Labels, Reset%>" CssClass="button"
                OnClick="btnReset_Click" />
            &nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="<%$Resources:Labels, Cancel%>" CssClass="button"
                OnClick="btnCancel_Click" />
            &nbsp;
        </td>
    </tr>
</table>
