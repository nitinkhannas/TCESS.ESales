<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AuthorizedRepresentative.ascx.cs"
    Inherits="CustomerRegistration_UserControls_AuthorizedRepresentative" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<table width="100%" cellspacing="5" cellpadding="5" class="formtext">
    <tr align="left">
        <td>
            <asp:Label ID="lblName" runat="server" Text="<%$Resources:Labels, Name%>" />
        </td>
        <td>
            <asp:TextBox ID="txtAuthName" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" MaxLength="50" />
            <asp:RequiredFieldValidator ID="AuthNameValidator" ControlToValidate="txtAuthName"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredAuthName %>"
                runat="server" />

                 <asp:CustomValidator ID="AuthNameCustomValidator" runat="server" ControlToValidate="txtAuthName"
                            Text="*" OnServerValidate="txtAuthName_ServerValidate" CssClass="failureNotification"
                            Display="Dynamic" SetFocusOnError="true" ValidationGroup="SaveGroup" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateAuthName %>" />
            
            <ajax:FilteredTextBoxExtender ID="FilteredtxtAuthName" runat="server" TargetControlID="txtAuthName"
                FilterMode="ValidChars" ValidChars="., " FilterType="LowercaseLetters,UppercaseLetters,Custom" />
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="AuthNameValidator" />
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="AuthNameCustomValidator" />
        </td>
        <td>
            <asp:Label ID="lblAuthFatherName" runat="server" Text="<%$Resources:Labels, FathersName%>" />
        </td>
        <td>
            <asp:TextBox ID="txtAuthFatherName" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" MaxLength="50" />
            <asp:RequiredFieldValidator ID="AuthFatherNameValidator" ControlToValidate="txtAuthFatherName"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredFatherName %>"
                runat="server" />
            <ajax:FilteredTextBoxExtender ID="FilteredtxtAuthFatherName" runat="server" TargetControlID="txtAuthFatherName"
                FilterMode="ValidChars" ValidChars="., " FilterType="LowercaseLetters,UppercaseLetters,Custom" />
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="AuthFatherNameValidator" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblAddress" runat="server" Text="<%$Resources:Labels, Address%>" />
        </td>
        <td>
            <asp:TextBox ID="txtAddress" runat="server" CssClass="textarea" MaxLength="150" TextMode="MultiLine" />
            <asp:RequiredFieldValidator ID="AddressValidator" ControlToValidate="txtAddress"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredRegAddress %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="AddressValidator" />
        </td>
        <td>
            <asp:Label ID="lblMobileNumber" runat="server" Text="<%$Resources:Labels, MobileNo%>" />
        </td>
        <td>
            <asp:TextBox ID="txtMobileNumber" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" MaxLength="13" />
            <asp:RegularExpressionValidator ID="MobileNumberValidator" runat="server" ControlToValidate="txtMobileNumber"
                Display="Dynamic" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup"
                ErrorMessage="<%$ Resources:ErrorMessages, InvalidMobileNumber %>" CssClass="failureNotification"
                ValidationExpression="^((\+)?(\d{2}))?(\d{10}){1}?$" />
            <ajax:ValidatorCalloutExtender ID="MobileNumberValidatorCalloutExtender" runat="server"
                TargetControlID="MobileNumberValidator" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtMobileNumber"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredMobileNumber %>"
                runat="server" />
            <ajax:FilteredTextBoxExtender ID="FilteredtxtMobileNumberExtender" runat="server"
                TargetControlID="txtMobileNumber" FilterMode="ValidChars" ValidChars="+,-" FilterType="Numbers,Custom" />
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="MobileNumberValidator" />
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
            <asp:GridView ID="grdDocument" runat="server" AutoGenerateColumns="False" DataKeyNames="Doc_id"
                BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                AllowPaging="true" PageSize="10" HorizontalAlign="Center" Width="100%" CellPadding="5"
                OnRowDataBound="grdDocument_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkDocID" runat="server" Checked='<%# Bind("Doc_Mandatory") %>'
                                Enabled="False" />
                            <asp:HiddenField ID="hdnAuthRepDocId" runat="server" />
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
                            <ajax:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender1" runat="server"
                                TargetControlID="txtDocExDate" />
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
    &nbsp;
    <asp:CustomValidator ID="gridValidator" Display="Dynamic" runat="server" CssClass="failureNotification" />
    <uc1:MessageBox ID="ucMessageBox" runat="server" />
    <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
</div>
<div>
    <table width="100%">
        <tr>
            <td align="left">
                <asp:Button CssClass="button" ID="btnReturn" runat="server" Text="Customer Relationship Screen"
                    OnClick="btnReturn_Click" />
            </td>
            <td align="right" id="btnArea" runat="server">
                <asp:Button CssClass="button" ID="btnSave" runat="server" Text="Save and Upload"
                    CausesValidation="true" ValidationGroup="SaveGroup" OnClick="btnSave_Click" />
                <asp:Button CssClass="button" ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                <asp:Button ID="btnCancel" runat="server" Visible="false" Text="Cancel" CssClass="button"
                    OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</div>
