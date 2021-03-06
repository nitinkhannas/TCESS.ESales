<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Refund.aspx.cs" Inherits="GhatoCashCollection_Refund" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <label class="pageNameContent">
        <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, PaymentRefund%>"
            CssClass="pageNameContent" />
    </label>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <%--<asp:UpdateProgress ID="progressBar" runat="server" AssociatedUpdatePanelID="uplMainPanel"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="overlay">
                <div class="ajaxloader">
                    <img src='<%= ResolveClientUrl("~/Images/ajax-loader.gif")%>' style="vertical-align: middle"
                        alt="Processing" />Processing....
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
<table width="100%" cellspacing="10" cellpadding="5">
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="lblCustomerCode" runat="server" Text="<%$Resources:Labels, CustomerCode%>" />
        </td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="textbox" Wrap="False" />
            <asp:RequiredFieldValidator ID="CustomerCodeValidator" runat="server" ControlToValidate="txtCustomerCode"
                CssClass="failureNotification" Display="Dynamic" ErrorMessage="<%$ Resources:ErrorMessages, RequiredCustomerCode %>"
                SetFocusOnError="true" Text="*" ValidationGroup="ValidateGroup" />
            <ajax:ValidatorCalloutExtender ID="CustomerCodeValidatorCalloutExtender" runat="server"
                TargetControlID="CustomerCodeValidator" />
        </td>
        <td nowrap="nowrap">
            &nbsp;
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblValidationType" runat="server" Text="<%$Resources:Labels, MandatoryDocuments%>" />
        </td>
        <td nowrap="nowrap">
            <asp:DropDownList ID="ddlValidationType" runat="server" CssClass="listmenu" DataTextField="Doc_Name"
                DataValueField="Doc_Id" />
            <asp:RequiredFieldValidator ID="ValidationTypeValidator" runat="server" ControlToValidate="ddlValidationType"
                CssClass="failureNotification" Display="Dynamic" ErrorMessage="<%$ Resources:ErrorMessages, RequiredValidationType%>"
                InitialValue="0" SetFocusOnError="true" Text="*" ValidationGroup="ValidateGroup" />
            <ajax:ValidatorCalloutExtender ID="ValidationTypeValidatorCallOutExtender" runat="server"
                TargetControlID="ValidationTypeValidator" />
        </td>
      
        </tr>
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="Label3" runat="server" Text="<%$Resources:Labels, DocumentNumber%>" />
        </td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtValidationValue" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" MaxLength="15" />
            <asp:RequiredFieldValidator ID="ValidationValueValidator" ControlToValidate="txtValidationValue"
                Display="Dynamic" ValidationGroup="ValidateGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredValidationType%>"
                runat="server" />
        </td>
        <td nowrap="nowrap">
            &nbsp;</td>
        <td nowrap="nowrap">
            <asp:Label ID="lblPaymentID" runat="server" 
                Text="Payment ID" />
        </td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtPaymentID" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" MaxLength="15" />
            <ajax:FilteredTextBoxExtender ID="txtPaymentID_FilteredTextBoxExtender" runat="server"
                TargetControlID="txtPaymentID" 
                FilterType="Numbers" />
            <asp:RequiredFieldValidator ID="PaymentIDValidator" ControlToValidate="txtValidationValue"
                Display="Dynamic" ValidationGroup="ValidateGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="Payment ID cannot be left blank."
                runat="server" />
            <asp:Button ID="btnValidate" runat="server" CssClass="button" OnClick="btnValidate_Click"
                Text="Validate" ValidationGroup="ValidateGroup" />
        </td>
      
        </tr>
        </table>
<div style="overflow: auto; width: 100%;">
    <Custom:GridViewAlwaysShow AllowPaging="True" ID="grdCustomersDetails" runat="server"
        AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
        Font-Size="Small" PageSize="12" Width="100%" HorizontalAlign="Center" CellPadding="5"
        DataKeyNames="Cust_ID">
        <EmptyDataTemplate>
            <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, TradeName%>" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblDefaultFees" runat="server" Text='<%# Bind("Cust_TradeName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, FirmName%>" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblFirmName" runat="server" Text='<%# Bind("Cust_FirmName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, OwnerName%>" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblOwnerName" runat="server" Text='<%# Bind("Cust_OwnerName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, BusinessType%>">
                <ItemTemplate>
                    <asp:Label ID="lblBusinessType" runat="server" Text='<%# Bind("Cust_Business_Name") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="District Name">
                <ItemTemplate>
                    <asp:Label ID="lblMobileNo" runat="server" Text='<%# Bind("Cust_District_Name") %>' />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Balance Avaliable" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblBalanceAvaliable" runat="server" Text='<%# BalanceAvaliable() %>'/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle BackColor="#397dbc" Font-Bold="True" ForeColor="#FFFFFF" Height="20px" />
        <PagerStyle BackColor="#397dbc" ForeColor="#FFFFFF" HorizontalAlign="Left" />
        <RowStyle BackColor="White" ForeColor="#003399" Font-Size="Small" Height="20px" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <SortedAscendingCellStyle BackColor="#EDF6F6" />
        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
        <SortedDescendingCellStyle BackColor="#D6DFDF" />
        <SortedDescendingHeaderStyle BackColor="#002876" />
    </Custom:GridViewAlwaysShow>
</div>
<table width="100%" cellspacing="10" cellpadding="5">
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="lblAmount" runat="server" Text="<%$Resources:Labels, Amount%>" />
        </td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtAmount" onkeypress="return runScript(event)" runat="server" CssClass="textbox" />
            <asp:RequiredFieldValidator ID="AmountValidator" ControlToValidate="txtAmount" Display="Dynamic"
                ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                ErrorMessage="<%$ Resources:ErrorMessages, RequiredAmount%>" runat="server" />
            <ajax:FilteredTextBoxExtender ID="AmountValidatorExtender" runat="server" TargetControlID="txtAmount"
                FilterType="Custom, Numbers" FilterMode="ValidChars" ValidChars="." />
            <ajax:ValidatorCalloutExtender ID="AmountValidatorCalloutExtender" runat="server"
                TargetControlID="AmountValidator" />
            <asp:Button ID="btnValidateAmount" runat="server" Enabled="false" CssClass="button"
                OnClick="btnValidateAmount_Click" Text="Validate" />
        </td>
        <td nowrap="nowrap">
            &nbsp;
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblPayerName" runat="server" Text="Receiver Name" />
        </td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtPayerName" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" MaxLength="50" />
            <asp:RequiredFieldValidator ID="PayerNameValidator" ControlToValidate="txtPayerName"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredPayerName%>"
                runat="server" />
            <ajax:FilteredTextBoxExtender ID="PayerNameValidatorExtender" runat="server" TargetControlID="txtPayerName"
                FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars=" " FilterMode="ValidChars" />
            <ajax:ValidatorCalloutExtender ID="PayerNameValidatorCalloutExtender" runat="server"
                TargetControlID="PayerNameValidator" />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="lblMobileNo" runat="server" Text="Receiver Mobile Number" />
        </td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtMobileNo" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" MaxLength="15" />
            <asp:RequiredFieldValidator ID="MobileNoValidator" ControlToValidate="txtMobileNo"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredMobileNumber%>"
                runat="server" />
            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMobileNo"
                FilterMode="ValidChars" ValidChars="+,-" FilterType="Numbers,Custom" />
            <ajax:ValidatorCalloutExtender ID="MobileNoValidatorCalloutExtender" runat="server"
                TargetControlID="MobileNoValidator" />
        </td>
        <td nowrap="nowrap">
            &nbsp;
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblRemarks" runat="server" Text="<%$Resources:Labels, Remarks%>" />
        </td>
        <td nowrap="nowrap" colspan="4">
            <asp:TextBox ID="txtRemarks" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" />
            <asp:RequiredFieldValidator ID="RemarksValidator" ControlToValidate="txtRemarks"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredRemarks%>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="RemarksValidator_ValidatorCalloutExtender" runat="server"
                TargetControlID="RemarksValidator" />
        </td>
    </tr>
    <tr align="left" id="trInstrumentType" runat="server" visible="false">
        <td nowrap="nowrap">
            <asp:Label ID="lblInstrumentNumber" runat="server" Text="<%$Resources:Labels, InstrumentNumber%>" />
        </td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtInstrumentNumber" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" />
            <asp:RequiredFieldValidator ID="InstrumentNumberValidator" runat="server" ControlToValidate="txtInstrumentNumber"
                CssClass="failureNotification" Display="Dynamic" ErrorMessage="<%$ Resources:ErrorMessages, RequiredInstrumentNumber%>"
                SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup" />
            <ajax:ValidatorCalloutExtender ID="InstrumentNumberValidatorCalloutExtender" runat="server"
                TargetControlID="InstrumentNumberValidator" />
        </td>
        <td nowrap="nowrap">
            &nbsp;
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblInstrumentDate" runat="server" Text="<%$Resources:Labels, InstrumentDate%>" />
        </td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtInstrumentDate" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" MaxLength="15" />
            <asp:RequiredFieldValidator ID="InstrumentDateValidator" ControlToValidate="txtInstrumentDate"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredInstrumentDate%>"
                runat="server" />
            <ajax:CalendarExtender ID="InstrumentDateCalendarExtender" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="checkInstrumentDate"
                runat="server" TargetControlID="txtInstrumentDate" />
            <ajax:TextBoxWatermarkExtender ID="InstrumentDate_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtInstrumentDate" WatermarkCssClass="watermark"
                WatermarkText="Click to select date" />
            <ajax:ValidatorCalloutExtender ID="InstrumentDateCalendarCalloutExtender" runat="server"
                TargetControlID="InstrumentDateValidator" />
        </td>
    </tr>
    <tr align="left" id="trBankDrawn" runat="server" visible="false">
        <td nowrap="nowrap">
            <asp:Label ID="lblBankDrawn" runat="server" Text="<%$Resources:Labels, BANKNAME%>" />
        </td>
        <td nowrap="nowrap">
            <asp:DropDownList ID="ddlBankDrawn" runat="server" CssClass="listmenu" DataTextField="Bank_Name"
                DataValueField="Bank_Id" />
            <asp:RequiredFieldValidator ID="BankDrawnValidator" ControlToValidate="ddlBankDrawn"
                Display="Dynamic" InitialValue="0" ValidationGroup="SaveGroup" SetFocusOnError="true"
                Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, SelectBankDrawn%>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="BankDrawnValidatorCalloutExtender" runat="server"
                TargetControlID="BankDrawnValidator" />
        </td>
        <td nowrap="nowrap">
            &nbsp;
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblBranchName" runat="server" Text="<%$Resources:Labels, BRANCHNAME%>" />
        </td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtBranchName" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" MaxLength="50" />
            <ajax:FilteredTextBoxExtender ID="BranchNameFilteredTextBoxExtender" runat="server"
                TargetControlID="txtBranchName" FilterMode="ValidChars" ValidChars=" " FilterType="Custom, UppercaseLetters, LowercaseLetters" />
            <asp:RequiredFieldValidator ID="BranchNameValidator" ControlToValidate="txtBranchName"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, REQUIREDBRANCHNAME%>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="BranchNameValidatorCalloutExtender" runat="server"
                TargetControlID="BranchNameValidator" />
        </td>
    </tr>
    <tr align="left" id="trIFSCCode" runat="server" visible="false">
        <td nowrap="nowrap">
            <asp:Label ID="lblIFSCCode" runat="server" Text="<%$Resources:Labels, IFSCCODE%>" />
        </td>
        <td nowrap="nowrap" colspan="4">
            <asp:TextBox ID="txtIFSCCode" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" />
            <ajax:FilteredTextBoxExtender ID="IFSCCodeFilteredTextBoxExtender" runat="server"
                TargetControlID="txtIFSCCode" FilterMode="ValidChars" ValidChars=" " FilterType="Custom, UppercaseLetters, LowercaseLetters" />
            <asp:RequiredFieldValidator ID="IFSCCodeValidator" runat="server" ControlToValidate="txtIFSCCode"
                CssClass="failureNotification" Display="Dynamic" ErrorMessage="<%$ Resources:ErrorMessages, RequiredIFSCCode%>"
                SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup" />
            <ajax:ValidatorCalloutExtender ID="IFSCCodeValidatorCalloutExtender" runat="server"
                TargetControlID="IFSCCodeValidator" />
        </td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="5" align="center">
            <asp:Button ID="btnAccept" CssClass="button" runat="server" Text="<%$Resources:Labels, AcceptPayment%>"
                OnClick="btnAccept_Click" Enabled="false" ValidationGroup="SaveGroup" />
            &nbsp;<asp:Button ID="btnReset" CssClass="button" runat="server" Text="<%$Resources:Labels, Reset%>"
                OnClick="btnReset_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="5" align="center">
            <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
            <uc4:YesNoMessageBox ID="ucYesNoMessageBox" runat="server" />
        </td>
    </tr>
</table>
</asp:Content>
