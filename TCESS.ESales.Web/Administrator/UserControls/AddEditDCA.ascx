<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddEditDCA.ascx.cs" Inherits="Administrator_UserControls_AddEditDCA" %>
<table width="100%" cellspacing="5" cellpadding="5" class="formtext">
    <tr align="left">
        <td>
            <asp:Label ID="lblShortName" runat="server" Text="<%$Resources:Labels, ShortName%>" />
        </td>
        <td>
            <asp:TextBox ID="txtShortName" runat="server" CssClass="textbox" MaxLength="45" />
            <asp:RequiredFieldValidator ID="ShortNameValidator" ControlToValidate="txtShortName"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredShortName%>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="AgentAccountValidatorCallOut" runat="server" TargetControlID="ShortNameValidator" />
            <asp:CustomValidator ID="ShortNameCustomValidator" runat="server" ControlToValidate="txtShortName"
                Text="*" OnServerValidate="AgentShortName_ServerValidate" CssClass="failureNotification"
                SetFocusOnError="true" Display="Dynamic" ValidationGroup="SaveGroup" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateShortName %>" />
            <ajax:ValidatorCalloutExtender ID="CustomValidatorCalloutExtender" runat="server"
                TargetControlID="ShortNameCustomValidator" />
        </td>
        <td>
            <asp:Label ID="lblDCAName" runat="server" Text="<%$Resources:Labels, Name%>" />
        </td>
        <td>
            <asp:TextBox ID="txtDCAName" runat="server" CssClass="textbox" MaxLength="45" />
            <asp:RequiredFieldValidator ID="DCANameValidator" ControlToValidate="txtDCAName"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredDCAName %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="DCANameValidatorCallout" runat="server" TargetControlID="DCANameValidator" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblStartDate" runat="server" Text="<%$Resources:Labels, DateOfJoining%>" />
        </td>
        <td>
            <asp:TextBox ID="txtStartDate" runat="server" CssClass="textbox" />
            <ajax:CalendarExtender ID="calStartDate" runat="server" TargetControlID="txtStartDate"
                Format="dd-MMM-yyyy" />
            <ajax:TextBoxWatermarkExtender ID="StartDateWatermarkExtender" runat="server" TargetControlID="txtStartDate"
                WatermarkCssClass="watermark" WatermarkText="<%$Resources:Labels, DateOfJoining%>" />
            <asp:RequiredFieldValidator ID="StartDateValidator" ControlToValidate="txtStartDate"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredStartDate %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="txtStartDateValidatorCalloutExtender" runat="server"
                TargetControlID="StartDateValidator" />
        </td>
        <td>
            <asp:Label ID="lblClosureDate" runat="server" Text="<%$Resources:Labels, DateOfRetirement%>" />
        </td>
        <td>
            <asp:TextBox ID="txtClosureDate" runat="server" CssClass="textbox" />
            <ajax:CalendarExtender ID="calClosureDate" runat="server" TargetControlID="txtClosureDate"
                Format="dd-MMM-yyyy" />
            <ajax:TextBoxWatermarkExtender ID="ClosureDateWatermarkExtender" runat="server" TargetControlID="txtClosureDate"
                WatermarkCssClass="watermark" WatermarkText="<%$Resources:Labels, DateOfRetirement%>" />
            <asp:CustomValidator ID="txtClosureDateValidator" runat="server" ControlToValidate="txtClosureDate"
                Text="*" OnServerValidate="Date_ServerValidate" CssClass="failureNotification"
                Display="Dynamic" SetFocusOnError="true" ValidationGroup="SaveGroup" ErrorMessage="<%$ Resources:ErrorMessages, SelectDateOfRetirement %>" />
            <ajax:ValidatorCalloutExtender ID="UserNameCustomValidatorValidatorCalloutExtender"
                runat="server" TargetControlID="txtClosureDateValidator" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblPanNumber" runat="server" Text="<%$Resources:Labels, PANNumber%>" />
        </td>
        <td>
            <asp:TextBox ID="txtPanNumber" runat="server" CssClass="textbox" MaxLength="15" />
            <ajax:FilteredTextBoxExtender ID="FilteredPanNumberExtender" runat="server" TargetControlID="txtPanNumber"
                FilterMode="ValidChars" FilterType="LowercaseLetters,UppercaseLetters,Numbers" />
            <asp:RequiredFieldValidator ID="PanNumberValidator" ControlToValidate="txtPanNumber"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredPanNumber %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="ValidatorPanNumberCallout" runat="server" TargetControlID="PanNumberValidator" />
        </td>
        <td>
            <asp:Label ID="lblSalesTax" runat="server" Text="<%$Resources:Labels, SalesTax%>" />
        </td>
        <td>
            <asp:TextBox ID="txtSalesTaxNumber" runat="server" CssClass="textbox" MaxLength="15" />
            <ajax:FilteredTextBoxExtender ID="FilteredSalesTaxNumberExtender" runat="server"
                TargetControlID="txtSalesTaxNumber" FilterMode="ValidChars" FilterType="LowercaseLetters,UppercaseLetters,Numbers" />
            <asp:RequiredFieldValidator ID="SalesTaxNumberValidator" ControlToValidate="txtSalesTaxNumber"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredSalesTaxNumber %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="ValidatorSalesTaxNumberCallout" runat="server"
                TargetControlID="SalesTaxNumberValidator" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblTSLCode" runat="server" Text="<%$Resources:Labels, TSLCode%>" />
        </td>
        <td>
            <asp:TextBox ID="txtTSLCode" runat="server" CssClass="textbox" MaxLength="10" />
        </td>
    </tr>
</table>
<table width="100%" cellspacing="5" cellpadding="5" class="formtext">
    <tr align="left">
        <td colspan="2" align="center">
            <strong>
                <asp:Label ID="lblRegisteredAdd" runat="server" Text="<%$Resources:Labels, RegisteredAddress%>" />
            </strong>
        </td>
        <td colspan="2" align="center">
            <strong>
                <asp:Label ID="lblLocalAdd" runat="server" Text="<%$Resources:Labels, LocalAddress%>" />
            </strong>
        </td>
        <td colspan="2" align="center">
            <strong>
                <asp:Label ID="lblCommunicationAdd" runat="server" Text="<%$Resources:Labels, CommunicationAddress%>" />
            </strong>
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblRegContactPerson" runat="server" Text="<%$Resources:Labels, ContactPerson%>" />
        </td>
        <td>
            <asp:TextBox ID="txtRegContactPerson" runat="server" CssClass="textbox" MaxLength="45" />
            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtRegContactPerson"
                ValidChars=" " FilterMode="ValidChars" FilterType="LowercaseLetters,UppercaseLetters,Custom" />
        </td>
        <td>
            <asp:Label ID="lblLocalContactPerson" runat="server" Text="<%$Resources:Labels, ContactPerson%>" />
        </td>
        <td>
            <asp:TextBox ID="txtLocalContactPerson" runat="server" CssClass="textbox" MaxLength="45" />
            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtLocalContactPerson"
                ValidChars=" " FilterMode="ValidChars" FilterType="LowercaseLetters,UppercaseLetters,Custom" />
        </td>
        <td>
            <asp:Label ID="lblComContactPerson" runat="server" Text="<%$Resources:Labels, ContactPerson%>" />
        </td>
        <td>
            <asp:TextBox ID="txtComContactPerson" runat="server" CssClass="textbox" MaxLength="45" />
            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtComContactPerson"
                ValidChars=" " FilterMode="ValidChars" FilterType="LowercaseLetters,UppercaseLetters,Custom" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblRegAddress" runat="server" Text="<%$Resources:Labels, Address%>" />
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
            <ajax:ValidatorCalloutExtender ID="RegAddressValidatorCallOut" runat="server" TargetControlID="RegAddressValidator" />
        </td>
        <td>
            <asp:Label ID="lblLocalAddress" runat="server" Text="<%$Resources:Labels, Address%>" />
        </td>
        <td>
            <asp:TextBox ID="txtLocalAddress" runat="server" TextMode="MultiLine" CssClass="textarea"
                MaxLength="150" />
            <asp:RegularExpressionValidator ID="LocalAddressValidator" runat="server" ControlToValidate="txtLocalAddress"
                Display="Dynamic" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup"
                ErrorMessage="<%$ Resources:ErrorMessages, LengthExceeded %>" CssClass="failureNotification"
                ValidationExpression="^[\s\S]{0,150}$" />
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="LocalAddressValidator" />
        </td>
        <td>
            <asp:Label ID="lblComAddress" runat="server" Text="<%$Resources:Labels, Address%>" />
        </td>
        <td>
            <asp:TextBox ID="txtComAddress" runat="server" TextMode="MultiLine" CssClass="textarea"
                MaxLength="150" />
            <asp:RegularExpressionValidator ID="ComAddressValidator" runat="server" ControlToValidate="txtComAddress"
                Display="Dynamic" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup"
                ErrorMessage="<%$ Resources:ErrorMessages, LengthExceeded %>" CssClass="failureNotification"
                ValidationExpression="^[\s\S]{0,150}$" />
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="ComAddressValidator" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblRegState" runat="server" Text="<%$Resources:Labels, State%>" />
        </td>
        <td>
            <asp:DropDownList ID="ddlRegState" runat="server" CssClass="listmenu" DataTextField="State_Name"
                DataValueField="State_Id" OnSelectedIndexChanged="ddlRegState_SelectedIndexChanged"
                AutoPostBack="true" />
            <asp:RequiredFieldValidator ID="RegStateValidator" ControlToValidate="ddlRegState"
                InitialValue="0" Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true"
                Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredState %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="ddlStateValidatorCalloutExtender" runat="server"
                TargetControlID="RegStateValidator" />
        </td>
        <td>
            <asp:Label ID="lblLocalState" runat="server" Text="<%$Resources:Labels, State%>" />
        </td>
        <td>
            <asp:DropDownList ID="ddlLocalState" runat="server" CssClass="listmenu" DataTextField="State_Name"
                DataValueField="State_Id" OnSelectedIndexChanged="ddlLocalState_SelectedIndexChanged"
                AutoPostBack="true" />
        </td>
        <td>
            <asp:Label ID="lblComState" runat="server" Text="<%$Resources:Labels, State%>" />
        </td>
        <td>
            <asp:DropDownList ID="ddlComState" runat="server" CssClass="listmenu" DataTextField="State_Name"
                DataValueField="State_Id" OnSelectedIndexChanged="ddlComState_SelectedIndexChanged"
                AutoPostBack="true" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblRegDistrict" runat="server" Text="<%$Resources:Labels, District%>" />
        </td>
        <td>
            <asp:DropDownList ID="ddlRegDistrict" runat="server" CssClass="listmenu" DataTextField="Dist_Name"
                DataValueField="Dist_Id" />
            <asp:RequiredFieldValidator ID="RegDistrictValidator" ControlToValidate="ddlRegDistrict"
                InitialValue="0" Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true"
                Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredDistrict %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="ddlDistrictValidatorCalloutExtender" runat="server"
                TargetControlID="RegDistrictValidator" />
        </td>
        <td>
            <asp:Label ID="lblLocalDistrict" runat="server" Text="<%$Resources:Labels, District%>" />
        </td>
        <td>
            <asp:DropDownList ID="ddlLocalDistrict" runat="server" CssClass="listmenu" DataTextField="Dist_Name"
                DataValueField="Dist_Id" />
        </td>
        <td>
            <asp:Label ID="lblComDistrict" runat="server" Text="<%$Resources:Labels, District%>" />
        </td>
        <td>
            <asp:DropDownList ID="ddlComDistrict" runat="server" CssClass="listmenu" DataTextField="Dist_Name"
                DataValueField="Dist_Id" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblRegPinCode" runat="server" Text="<%$Resources:Labels, PINCode%>" />
        </td>
        <td>
            <asp:TextBox ID="txtRegPinCode" runat="server" CssClass="textbox" MaxLength="6" />
            <ajax:FilteredTextBoxExtender ID="LocalPinCodeTextBoxExtender" runat="server" TargetControlID="txtRegPinCode"
                FilterMode="ValidChars" FilterType="Numbers" />
            <asp:RegularExpressionValidator ID="RegPinCodeValidator" runat="server" ControlToValidate="txtRegPinCode"
                Display="Dynamic" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup"
                ErrorMessage="<%$ Resources:ErrorMessages, InvalidPinCode%>" CssClass="failureNotification"
                ValidationExpression="^\d{6}$" />
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RegPinCodeValidator" />
        </td>
        <td>
            <asp:Label ID="lblLocalPinCode" runat="server" Text="<%$Resources:Labels, PINCode%>" />
        </td>
        <td>
            <asp:TextBox ID="txtLocalPinCode" runat="server" CssClass="textbox" MaxLength="6" />
            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtLocalPinCode"
                FilterMode="ValidChars" FilterType="Numbers" />
        </td>
        <td>
            <asp:Label ID="lblComPinCode" runat="server" Text="<%$Resources:Labels, PINCode%>" />
        </td>
        <td>
            <asp:TextBox ID="txtComPinCode" runat="server" CssClass="textbox" MaxLength="6" />
            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtComPinCode"
                FilterMode="ValidChars" FilterType="Numbers" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblRegMobileNo" runat="server" Text="<%$Resources:Labels, MobileNo%>" />
        </td>
        <td>
            <asp:TextBox ID="txtRegMobileNo" runat="server" CssClass="textbox" MaxLength="13" />
            <asp:RegularExpressionValidator ID="RegMobileNoValidator" runat="server" ControlToValidate="txtRegMobileNo"
                Display="Dynamic" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup"
                ErrorMessage="<%$ Resources:ErrorMessages, InvalidMobileNumber %>" CssClass="failureNotification"
                ValidationExpression="^((\+)?(\d{2}))?(\d{10}){1}?$" />
            <ajax:ValidatorCalloutExtender ID="RegMobileNoValidatorCalloutExtender" runat="server"
                TargetControlID="RegMobileNoValidator" />
            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtRegMobileNo"
                FilterMode="ValidChars" ValidChars="+,-" FilterType="Numbers,Custom" />
        </td>
        <td>
            <asp:Label ID="lblLocalMobileNo" runat="server" Text="<%$Resources:Labels, MobileNo%>" />
        </td>
        <td>
            <asp:TextBox ID="txtLocalMobileNo" runat="server" CssClass="textbox" MaxLength="13" />
            <asp:RegularExpressionValidator ID="LocalMobileNoValidator" runat="server" ControlToValidate="txtLocalMobileNo"
                Display="Dynamic" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup"
                ErrorMessage="<%$ Resources:ErrorMessages, InvalidMobileNumber %>" CssClass="failureNotification"
                ValidationExpression="^((\+)?(\d{2}))?(\d{10}){1}?$" />
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="LocalMobileNoValidator" />
            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtLocalMobileNo"
                ValidChars="+" FilterMode="ValidChars" FilterType="Numbers,Custom" />
        </td>
        <td>
            <asp:Label ID="lblComMobileNo" runat="server" Text="<%$Resources:Labels, MobileNo%>" />
        </td>
        <td>
            <asp:TextBox ID="txtComMobileNo" runat="server" CssClass="textbox" MaxLength="13" />
            <asp:RegularExpressionValidator ID="ComMobileNoValidator" runat="server" ControlToValidate="txtComMobileNo"
                Display="Dynamic" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup"
                ErrorMessage="<%$ Resources:ErrorMessages, InvalidMobileNumber %>" CssClass="failureNotification"
                ValidationExpression="^((\+)?(\d{2}))?(\d{10}){1}?$" />
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="ComMobileNoValidator" />
            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtComMobileNo"
                ValidChars="+" FilterMode="ValidChars" FilterType="Numbers,Custom" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblRegPhoneNo" runat="server" Text="<%$Resources:Labels, PhoneNumber%>" />
        </td>
        <td>
            <asp:TextBox ID="txtRegPhoneNo" runat="server" CssClass="textbox" MaxLength="15" />
            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtRegPhoneNo"
                FilterMode="ValidChars" ValidChars="+,-" FilterType="Numbers,Custom" />
        </td>
        <td>
            <asp:Label ID="lblLocalPhoneNo" runat="server" Text="<%$Resources:Labels, PhoneNumber%>" />
        </td>
        <td>
            <asp:TextBox ID="txtLocalPhoneNo" runat="server" CssClass="textbox" MaxLength="15" />
            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtLocalPhoneNo"
                FilterMode="ValidChars" ValidChars="+,-" FilterType="Numbers,Custom" />
        </td>
        <td>
            <asp:Label ID="lblComPhoneNo" runat="server" Text="<%$Resources:Labels, PhoneNumber%>" />
        </td>
        <td>
            <asp:TextBox ID="txtComPhoneNo" runat="server" CssClass="textbox" MaxLength="15" />
            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtComPhoneNo"
                FilterMode="ValidChars" ValidChars="+,-" FilterType="Numbers,Custom" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblRegEmail" runat="server" Text="<%$Resources:Labels, Email%>" />
        </td>
        <td>
            <asp:TextBox ID="txtRegEmail" runat="server" CssClass="textbox" MaxLength="45" />
            <asp:RegularExpressionValidator ID="RegEmailValidator" runat="server" ControlToValidate="txtRegEmail"
                Display="Dynamic" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup"
                ErrorMessage="<%$ Resources:ErrorMessages, InvalidEmail %>" CssClass="failureNotification"
                ValidationExpression="^[A-Za-z0-9_\+-]+(\.[A-Za-z0-9_\+-]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*\.([A-Za-z]{2,4})$" />
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="RegEmailValidator" />
        </td>
        <td>
            <asp:Label ID="lblLocalEmail" runat="server" Text="<%$Resources:Labels, Email%>" />
        </td>
        <td>
            <asp:TextBox ID="txtLocalEmail" runat="server" CssClass="textbox" MaxLength="45" />
            <asp:RegularExpressionValidator ID="LocalEmailValidator" runat="server" ControlToValidate="txtLocalEmail"
                Display="Dynamic" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup"
                ErrorMessage="<%$ Resources:ErrorMessages, InvalidEmail %>" CssClass="failureNotification"
                ValidationExpression="^[A-Za-z0-9_\+-]+(\.[A-Za-z0-9_\+-]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*\.([A-Za-z]{2,4})$" />
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="LocalEmailValidator" />
        </td>
        <td>
            <asp:Label ID="lblComEmail" runat="server" Text="<%$Resources:Labels, Email%>" />
        </td>
        <td>
            <asp:TextBox ID="txtComEmail" runat="server" CssClass="textbox" MaxLength="45" />
            <asp:RegularExpressionValidator ID="ComEmailValidator" runat="server" ControlToValidate="txtComEmail"
                Display="Dynamic" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup"
                ErrorMessage="<%$ Resources:ErrorMessages, InvalidEmail %>" CssClass="failureNotification"
                ValidationExpression="^[A-Za-z0-9_\+-]+(\.[A-Za-z0-9_\+-]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*\.([A-Za-z]{2,4})$" />
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="ComEmailValidator" />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
            <uc1:MessageBox ID="ucMessageBox" runat="server" />
            <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
        </td>
    </tr>
    <tr>
        <td align="left" colspan="3">
            <asp:Button ID="btnAllocationPercentage" runat="server" Text="<%$Resources:Labels, ManageAllocationPercentage%>"
                CssClass="button" CausesValidation="true" Enabled="false" PostBackUrl="~/Supervisor/ManageAllocatePercentage.aspx" />
        </td>
        <td align="right" colspan="3">
            <asp:Button ID="btnSave" runat="server" Text="<%$Resources:Labels, Save%>" CssClass="button"
                ValidationGroup="SaveGroup" OnClick="btnSave_Click" />
            <asp:Button ID="btnReset" runat="server" Text="<%$Resources:Labels, Reset%>" CausesValidation="false"
                CssClass="button" OnClick="btnReset_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="<%$Resources:Labels, Cancel%>" CausesValidation="false"
                CssClass="button" OnClick="btnCancel_Click" />
        </td>
    </tr>
</table>
