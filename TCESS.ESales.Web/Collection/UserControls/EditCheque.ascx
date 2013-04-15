<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditCheque.ascx.cs" Inherits="Collection_UserControls_EditCheque" %>
<table width="100%" cellspacing="10" cellpadding="5">
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="lblChequeNumber" runat="server" Text="<%$Resources:Labels, ChequeNumber%>" />
        </td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtChequeNumber" ReadOnly="true" onkeypress="return runScript(event)"
                runat="server" CssClass="textbox" />
        </td>
        <td nowrap="nowrap">
            &nbsp;
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblChequeDate" runat="server" Text="<%$Resources:Labels, ChequeDate%>" />
        </td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtChequeDate" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" ReadOnly="true" />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="lblCustomerCode" runat="server" Text="<%$Resources:Labels, CustomerCode%>" />
        </td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtCustomerCode" onkeypress="return runScript(event)" runat="server"
                ReadOnly="true" CssClass="textbox" />
        </td>
        <td nowrap="nowrap">
            &nbsp;
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblCustomerName" runat="server" Text="<%$Resources:Labels, CustomerName%>" />
        </td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtCustomerName" onkeypress="return runScript(event)" runat="server"
                ReadOnly="true" CssClass="textbox" />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="lblIssueBankName" runat="server" Text="<%$Resources:Labels, BankName%>" />
        </td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtBankName" onkeypress="return runScript(event)" ReadOnly="true"
                runat="server" CssClass="textbox" />
        </td>
        <td nowrap="nowrap">
            &nbsp;
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblBranchName" runat="server" Text="<%$Resources:Labels, BranchName%>" />
        </td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtBranchName" onkeypress="return runScript(event)" runat="server"
                ReadOnly="true" CssClass="textbox" />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="lblAmount" runat="server" Text="<%$Resources:Labels, Amount%>" />
        </td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtAmount" onkeypress="return runScript(event)" ReadOnly="true"
                runat="server" CssClass="textbox" />
        </td>
        <td nowrap="nowrap">
            &nbsp;
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblChequeStatus" runat="server" Text="<%$Resources:Labels, ChequeStatus%>" />
        </td>
        <td nowrap="nowrap">
            <asp:DropDownList ID="ddlChequeStatus" runat="server" CssClass="listmenu" AutoPostBack="true"
                OnSelectedIndexChanged="ddlChequeStatus_SelectedIndexChanged">
                <asp:ListItem Text="Select Cheque Status" Value="0" />
                <asp:ListItem Text="Accept" Value="1" />
                <asp:ListItem Text="Reject" Value="3" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="ChequeStatusValidator" ControlToValidate="ddlChequeStatus"
                Display="Dynamic" InitialValue="0" ValidationGroup="SaveGroup" SetFocusOnError="true"
                Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, SelectChequeStatus%>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="ChequeStatusValidatorCalloutExtender" runat="server"
                TargetControlID="ChequeStatusValidator" />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="lblBankName" runat="server" Text="<%$Resources:Labels, CompanyBankName%>" />
        </td>
        <td nowrap="nowrap">
            <asp:DropDownList ID="ddlBank" runat="server" CssClass="listmenu" DataTextField="Bank_Name"
                DataValueField="Bank_Id" />
        </td>
        <td nowrap="nowrap">
            &nbsp;
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblRejectionReason" runat="server" Text="<%$Resources:Labels, RejectionReason%>" />
        </td>
        <td nowrap="nowrap">
            <asp:DropDownList ID="ddlRejectionReason" runat="server" CssClass="listmenu" DataTextField="RR_Name"
                DataValueField="RR_Id" Enabled="false" />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="lblDateOfCredit" runat="server" Text="<%$Resources:Labels, DATEOFCREDIT%>" />
        </td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtDateOfCredit" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" />
            <ajax:CalendarExtender ID="DateOfCreditCalendarExtender" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="checkInstrumentDate"
                runat="server" TargetControlID="txtDateOfCredit" />
            <ajax:TextBoxWatermarkExtender ID="DateOfCredit_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtDateOfCredit" WatermarkCssClass="watermark"
                WatermarkText="Click to select date" />
        </td>
        <td nowrap="nowrap">
            &nbsp;
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblAmountCredited" runat="server" Text="<%$Resources:Labels, AMOUNTCREDITED%>" />
        </td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtAmountCredited" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" />
            <asp:RequiredFieldValidator ID="AmountCreditedValidator" ControlToValidate="txtAmountCredited"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true"
                Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, REQUIREDAMOUNTCREDITED%>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="AmountCreditedValidatorCalloutExtender" runat="server" TargetControlID="AmountCreditedValidator" />
        </td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="5" align="center">
            <asp:Button ID="btnAccept" CssClass="button" runat="server" Text="<%$Resources:Labels, Save%>"
                OnClick="btnAccept_Click" ValidationGroup="SaveGroup" />
            &nbsp;<asp:Button ID="btnReturn" CssClass="button" runat="server" Text="<%$Resources:Labels, Cancel%>"
                OnClick="btnReturn_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="5" align="center">
            <asp:CustomValidator ID="customValidator" Display="Dynamic" runat="server" CssClass="failureNotification" />
            <uc1:MessageBox ID="ucMessageBox" runat="server" />
        </td>
    </tr>
</table>
