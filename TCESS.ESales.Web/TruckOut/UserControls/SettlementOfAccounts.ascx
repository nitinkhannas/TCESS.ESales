<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SettlementOfAccounts.ascx.cs"
    Inherits="TruckOut_UserControls_SettlementOfAccounts" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<style type="text/css">
    .style1
    {
        width: 130px;
    }
</style>
<table width="100%" cellspacing="5" cellpadding="5" class="formtext">
    <tr align="left">
        <td>
            <asp:Label ID="lblBooking" runat="server" Text="Total Settlement" Font-Bold="true" />
        </td>
        <td>
            <asp:TextBox ID="lblCount" runat="server" BorderStyle="Solid" BorderColor="Black"
                CssClass="textbox" ReadOnly="true" Enabled="false"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="lbltotalcash" runat="server" Text="Total Amount" Font-Bold="true" />
        </td>
        <td class="style1">
            <asp:TextBox ID="lblTotalcashcollected" runat="server" BorderStyle="Solid" BorderColor="Black"
                CssClass="textbox" ReadOnly="true" Enabled="false"></asp:TextBox>
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblBookingNo" runat="server" Text="<%$Resources:Labels, BookingNo%>" />
        </td>
        <td>
            <asp:TextBox ID="txtBookingNo" runat="server" CssClass="textbox" />
            <asp:RequiredFieldValidator ID="BookingNoValidator" ControlToValidate="txtBookingNo"
                Display="Dynamic" ValidationGroup="RetrieveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredBookingNo%>"
                runat="server" />
            <ajaxToolkit:ValidatorCalloutExtender ID="BookingNoValidatorCallOut" runat="server"
                TargetControlID="BookingNoValidator" />
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click"
                ValidationGroup="RetrieveGroup" />
        </td>
        <td>
            <asp:Label ID="lblSmsId" runat="server" Text="SMS Id" />
        </td>
        <td class="style1">
            <asp:TextBox ID="txtSmsId" runat="server" CssClass="textbox" />
            <asp:RequiredFieldValidator ID="SmsIdValidator" ControlToValidate="txtSmsId" Display="Dynamic"
                ValidationGroup="RetrieveGroup" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                ErrorMessage="<%$Resources:ErrorMessages, RequiredSmsId%>" runat="server" />
            <ajaxToolkit:ValidatorCalloutExtender ID="SmsIdValidatorCallout" runat="server" TargetControlID="SmsIdValidator" />
            <asp:Button ID="btnValidate" runat="server" Text="Validate" CssClass="button" OnClick="btnValidate_Click"
                ValidationGroup="RetrieveGroup" />
        </td>
    </tr>
    <tr align="left">
        <td>
        </td>
        <td>
        </td>
        <td>
            Rate Applicable
        </td>
        <td class="style1">
            <asp:DropDownList ID="ddlRate" runat="server" OnSelectedIndexChanged="ddlRate_SelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem Value="1">VAT</asp:ListItem>
                <asp:ListItem Value="2">C Form</asp:ListItem>
                <asp:ListItem Value="3">CST</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblBookingDate" runat="server" Text="<%$Resources:Labels, BookingDate%>" />
        </td>
        <td>
            <asp:TextBox ID="txtBookingDate" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
        <td>
            <asp:Label ID="Label1" runat="server" Text="<%$Resources:Labels, CustomerCode%>" />
        </td>
        <td class="style1">
            <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblCustomerName" runat="server" Text="<%$Resources:Labels, CustomerName%>" />
        </td>
        <td>
            <asp:TextBox ID="txtCustomerName" runat="server" ReadOnly="true" CssClass="textbox" />
        </td>
        <td>
            <asp:Label ID="lblDCAName" runat="server" Text="<%$Resources:Labels, DCAName%>" />
        </td>
        <td class="style1">
            <asp:TextBox ID="txtDCAName" runat="server" CssClass="textbox" ReadOnly="true" />
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
            <asp:Label ID="lblMaterialType" runat="server" Text="<%$Resources:Labels, MaterialType%>" />
        </td>
        <td class="style1">
            <asp:TextBox ID="txtMaterialType" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblInvoiceNo" runat="server" Text="<%$Resources:Labels, InvoiceNumber%>" />
        </td>
        <td>
            <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="textbox" MaxLength="15" />
            <asp:RequiredFieldValidator ID="InvoiceNoValidator" ControlToValidate="txtInvoiceNo"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" CssClass="failureNotification"
                ErrorMessage="<%$Resources:ErrorMessages, RequiredInvoiceNo%>" runat="server"
                Text="*" />
            <ajaxToolkit:ValidatorCalloutExtender ID="InvoiceNoValidatorCalloutExtender" runat="server"
                TargetControlID="InvoiceNoValidator" />
        </td>
        <td>
            <asp:Label ID="lblCFormNo" runat="server" Text="<%$Resources:Labels, CFormNo%>" />
        </td>
        <td class="style1">
            <asp:TextBox ID="txtCFormNo" runat="server" CssClass="textbox" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblFormDNo" runat="server" Text="<%$Resources:Labels, FormDNumber%>" />
        </td>
        <td>
            <asp:TextBox ID="txtFormDNo" runat="server" CssClass="textbox" MaxLength="15" />
            <asp:RequiredFieldValidator ID="FormDNoValidator" ControlToValidate="txtFormDNo"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" CssClass="failureNotification"
                ErrorMessage="<%$Resources:ErrorMessages, RequiredFormDNo%>" runat="server" Text="*" />
            <ajaxToolkit:ValidatorCalloutExtender ID="txtFormDNoValidatorCalloutExtender1" runat="server"
                TargetControlID="FormDNoValidator" />
        </td>
        <td>
            <asp:Label ID="lblHologramNumber" runat="server" Text="<%$Resources:Labels, HologramNumber%>" />
        </td>
        <td class="style1">
            <asp:TextBox ID="txtHologramNumber" runat="server" CssClass="textbox" MaxLength="20" />
            <asp:RequiredFieldValidator ID="HologramNumberValidator" ControlToValidate="txtHologramNumber"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" CssClass="failureNotification"
                ErrorMessage="<%$Resources:ErrorMessages, RequiredHGNo%>" runat="server" Text="*" />
            <ajaxToolkit:ValidatorCalloutExtender ID="HologramNumberValidatorCalloutExtender"
                runat="server" TargetControlID="HologramNumberValidator" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblRoadPermitNo" runat="server" Text="<%$Resources:Labels, RoadPermitNo%>" />
        </td>
        <td>
            <asp:TextBox ID="txtRoadPermitNo" runat="server" CssClass="textbox" />
        </td>
        <td>
            <asp:Label ID="lblGatePassNo" runat="server" Text="<%$Resources:Labels, GatePassNo%>" />
        </td>
        <td class="style1">
            <asp:TextBox ID="txtGatePassNo" runat="server" CssClass="textbox" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblQuantity" runat="server" Text="<%$Resources:Labels, TSLQuantity%>" />
        </td>
        <td>
            <asp:TextBox ID="txtQuantity" runat="server" CssClass="textbox" />
            <asp:RegularExpressionValidator ID="txtCFormRateRegExpValidator" runat="server" ControlToValidate="txtQuantity"
                ValidationExpression="^[0-9]+$" Display="Dynamic" ValidationGroup="EditMaterialType"
                SetFocusOnError="true" Text="*" CssClass="failureNotification" ErrorMessage="Enter Number only"></asp:RegularExpressionValidator>
            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                TargetControlID="txtCFormRateRegExpValidator" />
            <asp:Button ID="btnCalculate" runat="server" Text="Calculate" CssClass="button" OnClick="btnCalculate_Click" />
        </td>
        <td>
            &nbsp;
        </td>
        <td class="style1">
            &nbsp;
        </td>
    </tr>
    <tr align="center">
        <td colspan="2">
            <strong>
                <asp:Label ID="lblMaterial" runat="server" Text="<%$Resources:Labels, Material%>" /></strong>
        </td>
        <td colspan="2">
            <strong>
                <asp:Label ID="lblHandling" runat="server" Text="<%$Resources:Labels, Handling%>" /></strong>
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblTiscoRate" runat="server" Text="<%$Resources:Labels, TiscoRate%>" />
        </td>
        <td>
            <asp:TextBox ID="txtTiscoRate" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
        <td>
            <asp:Label ID="lblHandlingRate" runat="server" Text="<%$Resources:Labels, HandlingRate%>" />
        </td>
        <td class="style1">
            <asp:TextBox ID="txtHandlingRate" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblGrossAmount" runat="server" Text="<%$Resources:Labels, Amount%>" />
        </td>
        <td>
            <asp:TextBox ID="txtGrossAmount" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
        <td>
            <asp:Label ID="lblHndGrossAmount" runat="server" Text="<%$Resources:Labels, Amount%>" />
        </td>
        <td class="style1">
            <asp:TextBox ID="txtHndGrossAmount" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblTotalMatAmount" runat="server" Text="<%$Resources:Labels, MaterialAmount%>" />
        </td>
        <td>
            <asp:TextBox ID="txtTotalMatAmount" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
        <td>
            <asp:Label ID="lblHndServiceTax" runat="server" Text="<%$Resources:Labels, ServiceTax%>" />
        </td>
        <td class="style1">
            <asp:TextBox ID="txtHndServiceTax" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblTotalAmount" runat="server" Text="<%$Resources:Labels, TotalAmount%>" />
        </td>
        <td>
            <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
        <td>
            <asp:Label ID="lblHndEducationCess" runat="server" Text="<%$Resources:Labels, EducationCess%>" />
        </td>
        <td class="style1">
            <asp:TextBox ID="txtHndEducationCess" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblAmtDeposited" runat="server" Text="<%$Resources:Labels, AdvanceReceived%>" />
        </td>
        <td>
            <asp:TextBox ID="txtAmtDeposited" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
        <td>
            <asp:Label ID="lblHndHigherEducationCess" runat="server" Text="<%$Resources:Labels, HigherEducationCess%>" />
        </td>
        <td class="style1">
            <asp:TextBox ID="txtHndHigherEducationCess" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblBalance" runat="server" Text="<%$Resources:Labels, Balance%>" />
        </td>
        <td>
            <asp:TextBox ID="txtBalance" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
        <td>
            <asp:Label ID="lblTotalHndAmount" runat="server" Text="<%$Resources:Labels, TotalHandlingAmount%>" />
        </td>
        <td class="style1">
            <asp:TextBox ID="txtTotalHndAmount" runat="server" CssClass="textbox" ReadOnly="true" />
        </td>
    </tr>
    <tr>
        <td colspan="4" align="center">
            <uc1:MessageBox ID="ucMessageBox" runat="server" />
        </td>
    </tr>
    <tr>
        <td align="center" colspan="4">
            <asp:Button ID="btnSave" runat="server" ValidationGroup="SaveGroup" Text="<%$Resources:Labels, Save%>"
                CssClass="button" OnClick="btnSave_Click" Enabled="false" />
            &nbsp;
            <asp:Button ID="btnReset" runat="server" Text="<%$Resources:Labels, Reset%>" CssClass="button"
                OnClick="btnReset_Click" />
            &nbsp;
            <asp:Button ID="btnPrint" runat="server" Text="<%$Resources:Labels, Print%>" Enabled="false"
                CssClass="button" OnClick="btnPrint_Click" />
        </td>
    </tr>
</table>
