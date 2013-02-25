<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerRegistration.ascx.cs"
    Inherits="CustomerRegistration_UserControls_CustomerRegistration" %>
<table width="100%" cellspacing="5" cellpadding="5" class="formlabel">
    <tr align="left">
        <td>
            <asp:Label ID="lblTradeName" runat="server" Text="<%$Resources:Labels, TradeName%>" />
        </td>
        <td>
            <asp:TextBox ID="txtTradeName" runat="server" CssClass="textbox" MaxLength="50" />
            <asp:RequiredFieldValidator ID="TradeNameValidator" ControlToValidate="txtTradeName"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredTradeName %>"
                runat="server" />
            <ajax:FilteredTextBoxExtender ValidChars=" ,.,&,(,),-" ID="FilteredTextBoxExtender4"
                runat="server" TargetControlID="txtTradeName" FilterMode="ValidChars" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" />
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="TradeNameValidator" />
        </td>
        <td>
            <asp:Label ID="lblFirmName" runat="server" Text="<%$Resources:Labels, FirmName%>" />
        </td>
        <td>
            <asp:TextBox ID="txtFirmName" runat="server" CssClass="textbox" MaxLength="50" />
            <asp:RequiredFieldValidator ID="FirmNameValidator" ControlToValidate="txtFirmName"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredFirmName %>"
                runat="server" />
            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtFirmName"
                FilterMode="ValidChars" ValidChars=" ,.,&,(,),-" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" />
            <ajax:ValidatorCalloutExtender ID="FirmNameValidatorCalloutExtender" runat="server"
                TargetControlID="FirmNameValidator" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblOwnershipStatus" runat="server" Text="<%$Resources:Labels, OwnershipStatus%>" />
        </td>
        <td>
            <asp:DropDownList ID="ddlOwnershipStatus" DataTextField="OwnershipStatus_Name" DataValueField="OwnershipStatus_id"
                runat="server" CssClass="listmenu" />
            <asp:RequiredFieldValidator ID="OwnershipStatusValidator" ControlToValidate="ddlOwnershipStatus"
                InitialValue="0" Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true"
                Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredOwnershipStatus %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="OwnershipStatusValidatorCalloutExtender" runat="server"
                TargetControlID="OwnershipStatusValidator" />
        </td>
        <td>
            <asp:Label ID="lblOwnerName" runat="server" Text="<%$Resources:Labels, OwnerPartnerName%>" />
        </td>
        <td>
            <asp:TextBox ID="txtOwnerName" runat="server" CssClass="textbox" MaxLength="50" />
            <asp:RequiredFieldValidator ID="OwnerNameValidator" ControlToValidate="txtOwnerName"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredOwnerName %>"
                runat="server" />
            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtOwnerName"
                ValidChars=" ,.,&,(,),-" FilterMode="ValidChars" FilterType="LowercaseLetters,UppercaseLetters,Custom" />
            <ajax:ValidatorCalloutExtender ID="OwnerNameValidatorCalloutExtender" runat="server"
                TargetControlID="OwnerNameValidator" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblPartnerMobileNumber" runat="server" Text="<%$Resources:Labels, OwnerPartnerMobileNo%>" />
        </td>
        <td>
            <asp:TextBox ID="txtPartnerMobile" CssClass="textbox" runat="server" MaxLength="13" />
            <asp:RegularExpressionValidator ID="PartnerMobileValidator" runat="server" ControlToValidate="txtPartnerMobile"
                Display="Dynamic" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup"
                ErrorMessage="<%$ Resources:ErrorMessages, InvalidMobileNumber %>" CssClass="failureNotification"
                ValidationExpression="^((\+)?(\d{2}))?(\d{10}){1}?$" />
            <ajax:ValidatorCalloutExtender ID="PartnerMobileValidatorCalloutExtender" runat="server"
                TargetControlID="PartnerMobileValidator" />
            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtPartnerMobile"
                FilterMode="ValidChars" ValidChars="+,-" FilterType="Numbers,Custom" />
        </td>
        <td>
            <asp:Label ID="lblFatherName" runat="server" Text="<%$Resources:Labels, FathersName%>" />
        </td>
        <td>
            <asp:TextBox ID="txtFatherName" runat="server" CssClass="textbox" MaxLength="50" />
            <asp:RequiredFieldValidator ID="FatherNameValidator" ControlToValidate="txtFatherName"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredFatherName %>"
                runat="server" />
            <ajax:FilteredTextBoxExtender ValidChars=" ,.,&,(,),-" ID="FilteredTextBoxExtender7"
                runat="server" TargetControlID="txtFatherName" FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters" />
            <ajax:ValidatorCalloutExtender ID="FatherNameValidatorCalloutExtender" runat="server"
                TargetControlID="FatherNameValidator" />
        </td>
    </tr>
    <tr align="left">
        <td rowspan="2">
            <asp:Label ID="lblRegisteredAddress" runat="server" Text="<%$Resources:Labels, RegisteredAddress%>" />
        </td>
        <td rowspan="2">
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
            <asp:Label ID="lblUnitAddress" runat="server" Text="<%$Resources:Labels, UnitAddress%>" />
        </td>
        <td>
            <asp:TextBox ID="txtUnitAddress" runat="server" CssClass="textbox" MaxLength="150" />
            <asp:RegularExpressionValidator ID="UnitAddressExpressionValidator" runat="server"
                ControlToValidate="txtUnitAddress" Display="Dynamic" SetFocusOnError="true" Text="*"
                ValidationGroup="SaveGroup" ErrorMessage="<%$ Resources:ErrorMessages, LengthExceeded %>"
                CssClass="failureNotification" ValidationExpression="^[\s\S]{0,150}$" />
            <ajax:ValidatorCalloutExtender ID="UnitAddressExpressionValidatorCalloutExtender"
                runat="server" TargetControlID="UnitAddressExpressionValidator" />
            <asp:RequiredFieldValidator ID="UnitAddressValidator" ControlToValidate="txtUnitAddress"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredUnitAddress %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="UnitAddressValidatorCalloutExtender" runat="server"
                TargetControlID="UnitAddressValidator" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblPost" runat="server" Text="<%$Resources:Labels, Post%>" />
        </td>
        <td>
            <asp:TextBox ID="txtPost" runat="server" CssClass="textbox" MaxLength="150" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblLandmark" runat="server" Text="<%$Resources:Labels, Landmark%>" />
        </td>
        <td>
            <asp:TextBox ID="txtLandMark" runat="server" TextMode="MultiLine" CssClass="textarea"
                MaxLength="50" />
            <asp:RegularExpressionValidator ID="LandMarkValidator" runat="server" ControlToValidate="txtLandMark"
                Display="Dynamic" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup"
                ErrorMessage="<%$ Resources:ErrorMessages, LandmarkLengthExceeded %>" CssClass="failureNotification"
                ValidationExpression="^[\s\S]{0,50}$" />
            <ajax:ValidatorCalloutExtender ID="LandMarkValidatorCalloutextender" runat="server"
                TargetControlID="LandMarkValidator" />
        </td>
        <td>
            <asp:Label ID="lblAMEOffice" runat="server" Text="<%$Resources:Labels, AMEOffice%>" />
        </td>
        <td>
            <asp:DropDownList ID="ddlAME" DataTextField="Blocks_Name" DataValueField="Blocks_Id"
                runat="server" CssClass="listmenu" />
            <asp:RequiredFieldValidator ID="AMEValidator" ControlToValidate="ddlAME" Display="Dynamic"
                InitialValue="0" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredAME %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="AMEValidatorCalloutExtender" runat="server" TargetControlID="AMEValidator" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblState" runat="server" Text="<%$Resources:Labels, State%>" />
        </td>
        <td>
            <asp:DropDownList ID="ddlState" DataTextField="State_Name" DataValueField="State_Id"
                runat="server" CssClass="listmenu" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" />
            <asp:RequiredFieldValidator ID="StateValidator" ControlToValidate="ddlState" Display="Dynamic"
                InitialValue="0" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredState %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="StateValidatorCalloutExtender" runat="server"
                TargetControlID="StateValidator" />
        </td>
        <td>
            <asp:Label ID="lblDistrict" runat="server" Text="<%$Resources:Labels, District%>" />
        </td>
        <td>
            <asp:DropDownList ID="ddlDist" runat="server" DataValueField="Dist_Id" DataTextField="Dist_Name"
                CssClass="listmenu" />
            <asp:RequiredFieldValidator ID="DistValidator" ControlToValidate="ddlDist" Display="Dynamic"
                InitialValue="0" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredDistrict %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="DistValidatorCalloutExtender" runat="server" TargetControlID="DistValidator" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblPinCode" runat="server" Text="<%$Resources:Labels, PINCode%>" />
        </td>
        <td>
            <asp:TextBox ID="txtPincode" runat="server" CssClass="textbox" MaxLength="6" />
            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPincode"
                FilterMode="ValidChars" FilterType="Numbers" />
            <asp:RegularExpressionValidator ID="txtPincodeValidator" runat="server" ControlToValidate="txtPincode"
                Display="Dynamic" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup"
                ErrorMessage="<%$ Resources:ErrorMessages, InvalidPinCode%>" CssClass="failureNotification"
                ValidationExpression="^\d{6}$" />
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="txtPincodeValidator" />
        </td>
        <td>
            <asp:Label ID="lblBusinessType" runat="server" Text="<%$Resources:Labels, BusinessType%>" />
        </td>
        <td>
            <asp:DropDownList ID="ddlBusinessType" DataTextField="BusinessType_Name" DataValueField="BusinessType_Id"
                runat="server" CssClass="listmenu" />
            <asp:RequiredFieldValidator ID="BusinessTypeValidator" ControlToValidate="ddlBusinessType"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                InitialValue="0" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredBusinessType %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="BusinessTypeValidatorCalloutExtender" runat="server"
                TargetControlID="BusinessTypeValidator" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblNoOfChimney" runat="server" Text="<%$Resources:Labels, NoOfChimneys%>" />
        </td>
        <td>
            <asp:TextBox ID="txtNoOfChimney" runat="server" CssClass="textbox" MaxLength="2" />
            <ajax:FilteredTextBoxExtender ID="FilteredNoOfChimneyExtender" runat="server" TargetControlID="txtNoOfChimney"
                FilterMode="ValidChars" FilterType="Numbers" />
            <asp:RequiredFieldValidator ID="NoOfChimneyValidator" ControlToValidate="txtNoOfChimney"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredNoOfChimney %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="NoOfChimneyCallout" runat="server" TargetControlID="NoOfChimneyValidator" />
        </td>
        <td>
            <asp:Label ID="lblBrickCapacity" runat="server" Text="<%$Resources:Labels, BrickCapacity%>" />
        </td>
        <td>
            <asp:TextBox ID="txtBrickCapacity" runat="server" CssClass="textbox" MaxLength="6" />
            <ajax:FilteredTextBoxExtender ID="FilteredBrickCapacityExtender" runat="server" TargetControlID="txtBrickCapacity"
                FilterMode="ValidChars" FilterType="Numbers" />
            <asp:RequiredFieldValidator ID="BrickCapacityValidator" ControlToValidate="txtBrickCapacity"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredBrickCapacity %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="BrickCapacityCallout" runat="server" TargetControlID="BrickCapacityValidator" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblMobileNo" runat="server" Text="<%$Resources:Labels, MobileNumberforSMS%>" />
        </td>
        <td>
            <asp:TextBox ID="txtMobile" runat="server" CssClass="textbox" MaxLength="13" />
            <asp:RegularExpressionValidator ID="MobileExpressionValidator" runat="server" ControlToValidate="txtMobile"
                Display="Dynamic" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup"
                ErrorMessage="<%$ Resources:ErrorMessages, InvalidMobileNumber %>" CssClass="failureNotification"
                ValidationExpression="^((\+)?(\d{2}))?(\d{10}){1}?$" />
            <ajax:ValidatorCalloutExtender ID="MobileExpressionValidatorCalloutExtender" runat="server"
                TargetControlID="MobileExpressionValidator" />
            <asp:RequiredFieldValidator ID="MobileValidator" ControlToValidate="txtMobile" Display="Dynamic"
                ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                ErrorMessage="<%$ Resources:ErrorMessages, RequiredMobileNumber %>" runat="server" />
            <ajax:ValidatorCalloutExtender ID="MobileValidatorCalloutExtender" runat="server"
                TargetControlID="MobileValidator" />
            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtMobile"
                FilterMode="ValidChars" ValidChars="+,-" FilterType="Numbers,Custom" />
        </td>
        <td>
            <asp:Label ID="lblPhoneNo" runat="server" Text="<%$Resources:Labels, PhoneNumberwithSTDcode%>" />
        </td>
        <td>
            <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="textbox" MaxLength="15" />
            <ajax:FilteredTextBoxExtender ID="FilteredPhoneNoExtender" runat="server" TargetControlID="txtPhoneNo"
                FilterMode="ValidChars" ValidChars="+,-" FilterType="Numbers,Custom" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblAMEVisitDate" runat="server" Text="<%$Resources:Labels, AMEVisitDate%>" />
        </td>
        <td>
            <asp:TextBox ID="txtAMEVisitDate" runat="server" CssClass="textbox" />
            <ajax:CalendarExtender ID="AMEVisitDateCalendarExtender" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="checkDate"
                runat="server" TargetControlID="txtAMEVisitDate" />
            <ajax:TextBoxWatermarkExtender ID="txtAMEVisitDate_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtAMEVisitDate" WatermarkCssClass="watermark"
                WatermarkText="Click to select date">
            </ajax:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="AMEVisitDateValidator" ControlToValidate="txtAMEVisitDate"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredAMEVisitDate %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="AMEVisitDateValidatorCalloutExtender" runat="server"
                TargetControlID="AMEVisitDateValidator" />
        </td>
        <td>
            <asp:Label ID="lblAMEVisitDate2" runat="server" Text="Name of AME/Visiting Executive" />
        </td>
        <td>
            <asp:TextBox ID="txtAMEName" runat="server" CssClass="textbox" />
            <asp:RequiredFieldValidator ID="AMENameValidator" ControlToValidate="txtAMEName"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="AME name cannot be left blank" runat="server" />
            <ajax:ValidatorCalloutExtender ID="AMENameValidator_ValidatorCalloutExtender" runat="server"
                TargetControlID="AMENameValidator" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblSalesType" runat="server" Text="<%$Resources:Labels, SalesType%>" />
        </td>
        <td>
            <asp:DropDownList ID="ddlSaleType" runat="server" CssClass="listmenu">
                <asp:ListItem Value="0" Text="Select Sales Type" Selected="True"></asp:ListItem>
                <asp:ListItem Value="1" Text="Within Jharkhand"></asp:ListItem>
                <asp:ListItem Value="2" Text="Outside Jharkhand"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="SalesTypeValidator" ControlToValidate="ddlSaleType"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                InitialValue="0" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredSaleType %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="SalesTypeValidatorCalloutExtender" runat="server"
                TargetControlID="SalesTypeValidator" />
        </td>
        <td>
            <asp:Label ID="lblExciseRange" runat="server" Text="<%$Resources:Labels, ExciseRange%>" />
        </td>
        <td>
            <asp:TextBox ID="txtExciseRange" runat="server" CssClass="textbox" MaxLength="50" />
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblExciseDiv" runat="server" Text="<%$Resources:Labels, ExciseDiv%>" />
        </td>
        <td>
            <asp:TextBox ID="txtExciseDiv" runat="server" CssClass="textbox" MaxLength="50" />
        </td>
        <td>
            &nbsp;
            <asp:Label ID="lblExciseComm" runat="server" Text="<%$Resources:Labels, ExciseComm%>" />
        </td>
        <td>
            <asp:TextBox ID="txtExciseComm" runat="server" CssClass="textbox" MaxLength="50" />
            &nbsp;
        </td>
    </tr>
        <tr align="left" runat="server" id="Row1">
        <td colspan="4" style="text-align: center">
            Bank Account Details
        </td>
    </tr>
    <tr align="left" runat="server" id="Row2">
        <td>
            <asp:Label ID="lblBankName" runat="server" Text="Name of Customer Bank" />
        </td>
        <td>
            <asp:TextBox ID="txtBankName" runat="server" CssClass="textbox" />
            <asp:RegularExpressionValidator ID="BankNameValidator1" runat="server" ControlToValidate="txtBankName"
                Display="Dynamic" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup"
                ErrorMessage="Enter Valid Bank Name" CssClass="failureNotification" ValidationExpression="^[a-zA-Z\s]+$" />
            <ajax:ValidatorCalloutExtender ID="BankNameValidator_ValidatorCalloutExtender1" runat="server"
                TargetControlID="BankNameValidator1" />
            <asp:RequiredFieldValidator ID="BankNameValidator" ControlToValidate="txtBankName"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="Bank Name cannot be left blank"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="BankNameValidator_ValidatorCalloutExtender" runat="server"
                TargetControlID="BankNameValidator" />
        </td>
        <td>
            <asp:Label ID="lblAccountNo" runat="server" Text="Account No" />
        </td>
        <td>
            <asp:TextBox ID="txtAccountNo" runat="server" CssClass="textbox" />
            <asp:RequiredFieldValidator ID="AccountNoValidato" ControlToValidate="txtAccountNo"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="Accout No cannot be left blank"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="AccountNoValidato_ValidatorCalloutExtender" runat="server"
                TargetControlID="AccountNoValidato" />
            <asp:RegularExpressionValidator ID="AccnoValidator" runat="server" ControlToValidate="txtAccountNo"
                CssClass="failureNotification" Display="Dynamic" ErrorMessage="Required Numbers "
                SetFocusOnError="true" Text="*" ValidationExpression="\d+\.?\d*" ValidationGroup="EditMaterialType" />
            <ajax:ValidatorCalloutExtender ID="AccnoValidatorCallOut" runat="server" TargetControlID="AccnoValidator" />
        </td>
    </tr>
    <tr align="left" runat="server" id="Row3">
        <td>
            <asp:Label ID="lblBankBranch" runat="server" Text="Bank Branch" />
        </td>
        <td>
            <asp:TextBox ID="txtBankBranch" runat="server" CssClass="textbox" />
            <asp:RequiredFieldValidator ID="BankBranchValidator" ControlToValidate="txtBankBranch"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="Bank branch cannot be left blank"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="BankBranchValidator_ValidatorCalloutExtender"
                runat="server" TargetControlID="BankBranchValidator" />
        </td>
        <td>
            <asp:Label ID="lblChequeNo" runat="server" Text="Cancelled Cheque No" />
        </td>
        <td>
            <asp:TextBox ID="txtChequeNo" runat="server" CssClass="textbox" />
            <asp:RequiredFieldValidator ID="ChequeNoValidator" ControlToValidate="txtChequeNo"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="Cheque no cannot be left blank"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="ChequeNoValidator_ValidatorCalloutExtender" runat="server"
                TargetControlID="ChequeNoValidator" />
            <asp:RegularExpressionValidator ID="CheNoValidator" runat="server" ControlToValidate="txtChequeNo"
                CssClass="failureNotification" Display="Dynamic" ErrorMessage="Required Numbers "
                SetFocusOnError="true" Text="*" ValidationExpression="\d+\.?\d*" ValidationGroup="SaveGroup" />
            <ajax:ValidatorCalloutExtender ID="CheNoValidator_ValidatorCalloutExtender" runat="server"
                TargetControlID="CheNoValidator" />
        </td>
    </tr>
    <tr align="left" runat="server" id="Row4">
        <td>
            <asp:Label ID="lblAccountType" runat="server" Text="Type of Account" />
        </td>
        <td>
            <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="listmenu">
                <asp:ListItem Value="0" Text="Select Account Type" Selected="True"></asp:ListItem>
                <asp:ListItem Value="1" Text="Saving"></asp:ListItem>
                <asp:ListItem Value="2" Text="Current"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="AccountTypeValidato" ControlToValidate="ddlAccountType"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                InitialValue="0" CssClass="failureNotification" ErrorMessage="Select Account Type"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="AccountTypeValidato_ValidatorCalloutExtender"
                runat="server" TargetControlID="AccountTypeValidato" />
        </td>
        <td>
            <asp:Label ID="lblIFSCCode" runat="server" Text="IFSC Code" />
        </td>
        <td>
            <asp:TextBox ID="txtICFAICode" runat="server" CssClass="textbox" />
            <asp:RequiredFieldValidator ID="IFSCValidator" ControlToValidate="txtICFAICode" Display="Dynamic"
                ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                ErrorMessage="IFSC code cannot be left blank" runat="server" />
            <ajax:ValidatorCalloutExtender ID="IFSCValidator_ValidatorCalloutExtender" runat="server"
                TargetControlID="IFSCValidator" />
        </td>
    </tr>
    <tr align="left" runat="server" id="Row7">
        <td colspan="4" style="text-align: center">
            Inspection Details</td>
    </tr>
    <tr align="left" runat="server" id="Row8">
        <td>
            <asp:Label ID="lblAMEVisitDate0" runat="server" Text="Annual VAT Return Filed  on" />
        </td>
        <td>
            <asp:TextBox ID="txtVatFiledOn" runat="server" CssClass="textbox" />
            <ajax:CalendarExtender ID="txtVatFiledOn_CalendarExtender" Format="dd-MMM-yyyy" runat="server"
                TargetControlID="txtVatFiledOn" />
            <ajax:TextBoxWatermarkExtender ID="txtVatFiledOn_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtVatFiledOn" WatermarkCssClass="watermark"
                WatermarkText="Click to select date">
            </ajax:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="AMEVisitDateValidator0" ControlToValidate="txtAMEVisitDate"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredAMEVisitDate %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="AMEVisitDateValidator0_ValidatorCalloutExtender"
                runat="server" TargetControlID="AMEVisitDateValidator0" />
        </td>
        <td>
            <asp:Label ID="lblAMEVisitDate1" runat="server" Text="Unit Status" />
        </td>
        <td>
            <asp:DropDownList ID="ddlUnitStatus" runat="server" CssClass="listmenu">
                <asp:ListItem Value="0" Text="Select Unit Status" Selected="True"></asp:ListItem>
                <asp:ListItem Value="1" Text="Working"></asp:ListItem>
                <asp:ListItem Value="2" Text="Not Working"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="AccountTypeValidato0" ControlToValidate="ddlAccountType"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                InitialValue="0" CssClass="failureNotification" ErrorMessage="Select Account Type"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="AccountTypeValidato0_ValidatorCalloutExtender"
                runat="server" TargetControlID="AccountTypeValidato0" />
        </td>
    </tr>
    <tr align="left">
        <td colspan="2">
            <asp:GridView ID="grdCustomerMaterialMapping" runat="server" AutoGenerateColumns="False"
                HorizontalAlign="Left" DataKeyNames="MaterialType_ID" BorderColor="#3366CC" BorderStyle="Solid"
                BorderWidth="1px" Font-Size="Small" AllowPaging="true" PageSize="10" Width="369px"
                CellPadding="5">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkMaterialType" runat="server" AutoPostBack="true" OnCheckedChanged="chkMaterialType_Checked" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material Type">
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("MaterialType_Name") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Annual Coal Required per year<br />(In Tonnes)" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlAnnualRequirements" runat="server" CssClass="listmenu" Enabled="false">
                                <asp:ListItem Text="Select Requirements" Value="0" />
                                <asp:ListItem Text="200" Value="200" />
                                <asp:ListItem Text="500" Value="500" />
                                <asp:ListItem Text="600" Value="600" />
                                <asp:ListItem Text="800" Value="800" />
                                <asp:ListItem Text="1000" Value="1000" />
                                <asp:ListItem Text="1500" Value="1500" />
                                <asp:ListItem Text="2000" Value="2000" />
                                <asp:ListItem Text="3000" Value="3000" />
                                <asp:ListItem Text="5000" Value="5000" />
                            </asp:DropDownList>
                            <asp:CustomValidator ControlToValidate="ddlAnnualRequirements" ID="AnnualRequirementsValidator"
                                Display="Dynamic" OnServerValidate="AnnualRequirements_ServerValidate" runat="server"
                                ValidationGroup="SaveGroup" ErrorMessage="<%$ Resources:ErrorMessages, RequiredAnnualRequirements %>"
                                Text="*" CssClass="failureNotification" />
                            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="AnnualRequirementsValidator" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#397dbc" HorizontalAlign="Center" Font-Bold="True" ForeColor="#FFFFFF"
                    Height="20px" />
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

    </table>
<div>
    &nbsp;
    <uc1:MessageBox ID="ucMessageBox" runat="server" />
    <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
</div>
<div>
    <asp:CustomValidator ID="gridValidator" Display="Dynamic" runat="server" CssClass="failureNotification" />
</div>
<div id="Buttons" align="center">
    <asp:Button ID="btnCreateFolder" CssClass="button" runat="server" Text="Create Folder"
        CausesValidation="true" ValidationGroup="SaveGroup" OnClick="btnSave_Click" />
    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" OnClick="btnReset_Click" />
    <asp:Button ID="btnCancel" Visible="false" CssClass="button" runat="server" Text="Cancel"
        OnClick="btnCancel_Click" />
</div>
<div>
    &nbsp;
</div>
