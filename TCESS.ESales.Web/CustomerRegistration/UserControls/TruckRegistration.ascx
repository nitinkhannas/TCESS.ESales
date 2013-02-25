<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TruckRegistration.ascx.cs"
    Inherits="CustomerRegistration_UserControls_TruckRegistration" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<table width="100%" cellspacing="5" cellpadding="5">
    <tr align="left">
        <td>
            <asp:Label ID="lblRegistrationNumber" runat="server" Text="<%$Resources:Labels, RegistrationNumber%>" />
        </td>
        <td>
            <asp:TextBox ID="txtTruckRegNo" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" MaxLength="20" />
            <asp:RequiredFieldValidator ID="TruckRegNoValidator" ControlToValidate="txtTruckRegNo"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredTruckRegNo %>"
                runat="server" />
            <asp:CustomValidator ID="TruckRegNoCustomValidator" runat="server" ControlToValidate="txtTruckRegNo"
                Text="*" OnServerValidate="txtTruckRegNo_ServerValidate" CssClass="failureNotification"
                Display="Dynamic" SetFocusOnError="true" ValidationGroup="SaveGroup" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateTruckRegNo %>" />
            <ajax:ValidatorCalloutExtender ID="TruckRegNoValidatorCalloutExtender" runat="server"
                TargetControlID="TruckRegNoValidator" />
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="TruckRegNoCustomValidator" />
        </td>
        <td>
            <asp:Label ID="lblTruckMake" runat="server" Text="<%$Resources:Labels, TruckMake%>" />
        </td>
        <td>
            <asp:DropDownList ID="ddlTruckMake" DataTextField="TruckMake_Name" DataValueField="TruckMake_Id"
                runat="server" AutoPostBack="true" CssClass="listmenu" OnSelectedIndexChanged="ddlTruckMake_SelectedIndexChanged" />
            <asp:RequiredFieldValidator ID="TruckMakeValidator" ControlToValidate="ddlTruckMake"
                InitialValue="0" Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true"
                Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredTruckMake %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="TruckMakeValidatorCalloutExtender" runat="server"
                TargetControlID="TruckMakeValidator" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblWheeler" runat="server" Text="<%$Resources:Labels, NumberofWheels%>" />
        </td>
        <td>
            <asp:TextBox ID="txtWheeler" ReadOnly="true" runat="server" CssClass="textbox" />
        </td>
        <td>
            <asp:Label ID="lblCarryCapacity" runat="server" Text="<%$Resources:Labels, CarryCapacity%>" />
        </td>
        <td>
            <asp:TextBox ID="txtCarryCapacity" ReadOnly="true" runat="server" CssClass="textbox" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblOwnerName" runat="server" Text="<%$Resources:Labels, OwnerName%>" />
        </td>
        <td>
            <asp:TextBox ID="txtOwnerName" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" Rows="50" />
            <ajax:FilteredTextBoxExtender ID="FilteredOwnerName" runat="server" TargetControlID="txtOwnerName"
                FilterMode="ValidChars" ValidChars="., " FilterType="LowercaseLetters,UppercaseLetters,Custom" />
            <asp:RequiredFieldValidator ID="OwnerNameValidator" ControlToValidate="txtOwnerName"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredOwnerName %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="OwnerNameValidatorCalloutExtender" runat="server"
                TargetControlID="OwnerNameValidator" />
        </td>
        <td>
            <asp:Label ID="lblDriverName" runat="server" Text="<%$Resources:Labels, DriverName%>" />
        </td>
        <td>
            <asp:TextBox ID="txtDriverName" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" MaxLength="50" />
            <ajax:FilteredTextBoxExtender ID="FilteredDriverName" runat="server" TargetControlID="txtDriverName"
                FilterMode="ValidChars" ValidChars="., " FilterType="LowercaseLetters,UppercaseLetters,Custom" />
            <asp:RequiredFieldValidator ID="DriverNameValidator" ControlToValidate="txtDriverName"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredDriverName %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="DriverNameValidatorCalloutExtender" runat="server"
                TargetControlID="DriverNameValidator" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblRegisteredAddress" runat="server" Text="<%$Resources:Labels, RegisteredAddress%>" />
        </td>
        <td>
            <asp:TextBox ID="txtRegAddress" runat="server" TextMode="MultiLine" CssClass="textarea"
                MaxLength="150" />
            <asp:RegularExpressionValidator ID="RegAddressExpressionValidator" runat="server"
                ControlToValidate="txtRegAddress" Display="Dynamic" SetFocusOnError="true" Text="*"
                ValidationGroup="SaveGroup" ErrorMessage="<%$ Resources:ErrorMessages, LengthExceeded %>"
                CssClass="failureNotification" ValidationExpression="^[\s\S]{0,150}$" />
            <ajax:ValidatorCalloutExtender ID="RegAddressExpressionValidatorCalloutExtender"
                runat="server" TargetControlID="RegAddressExpressionValidator" />
            <asp:RequiredFieldValidator ID="RegAddressValidator" ControlToValidate="txtRegAddress"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredRegAddress %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="RegAddressValidatorCalloutExtender" runat="server"
                TargetControlID="RegAddressValidator" />
        </td>
        <td>
            <asp:Label ID="lblState" runat="server" Text="<%$Resources:Labels, State%>" />
        </td>
        <td>
            <asp:DropDownList ID="ddlStates" DataTextField="State_Name" DataValueField="State_Id"
                runat="server" CssClass="listmenu" />
            <asp:RequiredFieldValidator ID="StatesValidator" ControlToValidate="ddlStates" InitialValue="0"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredState %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="StatesRequiredValidatorCalloutExtender" runat="server"
                TargetControlID="StatesValidator" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblOwnerShortAdd" runat="server" Text="<%$Resources:Labels, OwnerShortAdd%>" />
        </td>
        <td>
            <asp:TextBox ID="txtOwnerShortAdd" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" Rows="20" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorOwnerShortAdd" runat="server"
                ControlToValidate="txtOwnerShortAdd" Display="Dynamic" SetFocusOnError="true" Text="*"
                ValidationGroup="SaveGroup" ErrorMessage="<%$ Resources:ErrorMessages, LengthExceeded %>"
                CssClass="failureNotification" ValidationExpression="^[\s\S]{0,20}$" />
            <asp:RequiredFieldValidator ID="OwnerShortAddValidator" ControlToValidate="txtOwnerShortAdd"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredOwnerShortAdd %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="OwnerShortAddValidatorCalloutExtender" runat="server"
                TargetControlID="OwnerShortAddValidator" />
        </td>
        <td>
            <asp:Label ID="lblDriverShortAdd" runat="server" Text="<%$Resources:Labels, DriverShortAdd%>" />
        </td>
        <td>
            <asp:TextBox ID="txtDriverShortAdd" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" Rows="20" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                ControlToValidate="txtDriverShortAdd" Display="Dynamic" SetFocusOnError="true" Text="*"
                ValidationGroup="SaveGroup" ErrorMessage="<%$ Resources:ErrorMessages, LengthExceeded %>"
                CssClass="failureNotification" ValidationExpression="^[\s\S]{0,20}$" />
            <asp:RequiredFieldValidator ID="DriverShortAddValidator" ControlToValidate="txtDriverShortAdd"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredDriverShortAdd %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="DriverShortAddValidatorCalloutExtender" runat="server"
                TargetControlID="DriverShortAddValidator" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblMobileNumber" runat="server" Text="<%$Resources:Labels, MobileNo%>" />
        </td>
        <td>
            <asp:TextBox ID="txtMobileNo" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" MaxLength="13" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMobileNo"
                Display="Dynamic" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup"
                ErrorMessage="<%$ Resources:ErrorMessages, InvalidMobileNumber %>" CssClass="failureNotification"
                ValidationExpression="^((\+)?(\d{2}))?(\d{10}){1}?$" />
            <ajax:FilteredTextBoxExtender ID="FilteredtxtPhoneNoExtender" runat="server" TargetControlID="txtMobileNo"
                FilterMode="ValidChars" ValidChars="+,-" FilterType="Numbers,Custom" />
            <asp:RequiredFieldValidator ID="PhoneNoValidator" ControlToValidate="txtMobileNo"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredMobileNumber %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="PhoneNoValidatorCalloutExtender" runat="server"
                TargetControlID="PhoneNoValidator" />
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RegularExpressionValidator1" />
        </td>
        <td>
            <asp:Label ID="lblPhoneNo" runat="server" Text="<%$Resources:Labels, PhoneNumber%>" />
        </td>
        <td>
            <asp:TextBox ID="txtPhoneNo" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" MaxLength="15" />
            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPhoneNo"
                FilterMode="ValidChars" ValidChars="+,-" FilterType="Numbers,Custom" />
        </td>
    </tr>
     <tr align ="left" >
    <td>
    <asp:Label ID="lblTruckreg" runat="server" Text= "Truck Type" />
    </td>
    <td>
     
            <asp:DropDownList ID="ddltruckregistration" DataTextField="TruckRegType_Name" DataValueField="TruckRegType_Id"
                runat="server" CssClass="listmenu" />
            <asp:RequiredFieldValidator ID="Trucktypevalidatior" ControlToValidate="ddltruckregistration" InitialValue="0"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredTruckType %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                TargetControlID="Trucktypevalidatior" />
        </td>
    
    </tr>
    <tr>
        <td colspan="4">
            <strong>
                <asp:Label runat="server" ID="lblFolderPath" Text="Save all scanned files in " />
                <asp:Label runat="server" ID="lnkOpenFolder" />
            </strong>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <asp:GridView ID="grdDocument" runat="server" AutoGenerateColumns="False" DataKeyNames="Doc_Id"
                BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                AllowPaging="true" PageSize="10" HorizontalAlign="Center" Width="100%" CellPadding="5"
                OnRowDataBound="grdDocument_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkDocID" runat="server" Checked='<%# Bind("Doc_Mandatory") %>'
                                Enabled="False" />
                            <asp:HiddenField ID="hdnTruckDocId" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, DocumentName%>">
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("Doc_Name") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, Acronym%>">
                        <ItemTemplate>
                            <asp:Label ID="lblAcronymName" runat="server" Text='<%# Bind("Doc_Acronym") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, DocumentNumber%>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDocNo" MaxLength="20" runat="server" />
                            <ajax:FilteredTextBoxExtender ID="txtDocNoFilteredExtender" runat="server" TargetControlID="txtDocNo"
                                FilterMode="ValidChars" ValidChars="-" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" />
                            <asp:CustomValidator ID="DocNoValidator" Display="Dynamic" OnServerValidate="DocNo_ServerValidate"
                                ControlToValidate="txtDocNo" ValidateEmptyText="true" runat="server" ValidationGroup="SaveGroup"
                                ErrorMessage="<%$ Resources:ErrorMessages, RequiredDocNo %>" Text="*" CssClass="failureNotification" />
                            <ajax:ValidatorCalloutExtender ID="DocNoValidatorCalloutExtender" runat="server"
                                TargetControlID="DocNoValidator" />
                            <asp:CustomValidator ControlToValidate="txtDocNo" ID="txtDocNoCustomValidator" Display="Dynamic"
                                ValidateEmptyText="true" OnServerValidate="DocNoExist_ServerValidate" runat="server"
                                ValidationGroup="SaveGroup" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateDocumentNumber %>"
                                Text="*" CssClass="failureNotification" />
                            <ajax:ValidatorCalloutExtender ID="txtDocNoCustomValidatorCalloutExtender" runat="server"
                                TargetControlID="txtDocNoCustomValidator" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, DocumentExpiryDate%>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDocExDate" runat="server" />
                            <ajax:CalendarExtender ID="CalendarExtender1" Format="dd MMM yyyy" runat="server"
                                TargetControlID="txtDocExDate" />
                            <asp:CustomValidator ID="DocExDateValidator" OnServerValidate="DocExDate_ServerValidate"
                                ControlToValidate="txtDocExDate" ValidateEmptyText="true" Text="*" Display="Dynamic"
                                runat="server" ErrorMessage="<%$ Resources:ErrorMessages, RequiredDocExpiryDate %>"
                                ValidationGroup="SaveGroup" CssClass="failureNotification" />
                            <ajax:ValidatorCalloutExtender ID="DocExDateValidatorCalloutExtender" runat="server"
                                TargetControlID="DocExDateValidator" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, ScanAndSave%>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkScanComplete" runat="server" AutoPostBack="true" OnCheckedChanged="chkScanComplete_Checked" />
                            <asp:CustomValidator ID="ScanCompleteValidator" Display="Dynamic" runat="server"
                                ValidationGroup="CheckBoxGroup" Text="*" CssClass="failureNotification" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, FileName%>" HeaderStyle-Width="160px">
                        <ItemTemplate>
                            <asp:Label ID="lblFileName" runat="server" />
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
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
<div>
    <asp:CustomValidator ID="gridValidator" Display="Dynamic" runat="server" CssClass="failureNotification" />
    <uc1:MessageBox ID="ucMessageBox" runat="server" />
    <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
</div>
<div>
    <table width="100%">
        <tr>
            <td align="left">
                <asp:Button ID="btnReturn" CssClass="button" runat="server" Text="Customer Relationship Screen"
                    OnClick="btnReturn_Click" />
            </td>
            <td align="right" id="btnArea" runat="server">
                <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save and Upload"
                    CausesValidation="true" ValidationGroup="SaveGroup" OnClick="btnSave_Click" />
                <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" OnClick="btnReset_Click" />
                <asp:Button ID="btnCancel" Visible="false" CssClass="button" runat="server" Text="Cancel"
                    OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</div>
<div>
    &nbsp;
</div>
