<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="RegisterUser.aspx.cs" Inherits="UserRegistration_RegisterUser" ValidateRequest="false" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <label class="pageNameContent">
        <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, UserRegistration%>"
            CssClass="pageNameContent" />
    </label>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="progressBar" runat="server" AssociatedUpdatePanelID="uplMainPanel"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="overlay">
                <div class="ajaxloader">
                    <img src='<%= ResolveClientUrl("~/Images/ajax-loader.gif")%>' style="vertical-align: middle"
                        alt="Processing" />Processing....
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="uplMainPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" cellspacing="5" cellpadding="5" class="formtext">
                <tr align="left">
                    <td>
                        <asp:Label ID="lblFirstName" runat="server" Text="<%$Resources:Labels, FirstName%>" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="textbox" MaxLength="35" />
                        <asp:RequiredFieldValidator ID="txtFirstNameValidator" ControlToValidate="txtFirstName"
                            Display="Dynamic" ValidationGroup="AddUser" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                            ErrorMessage="<%$Resources:ErrorMessages, RequiredFirstName%>" runat="server" />
                        <ajax:ValidatorCalloutExtender ID="AgentAccountValidatorCallOut" runat="server" TargetControlID="txtFirstNameValidator" />
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblLastName" runat="server" Text="<%$Resources:Labels, LastName%>" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="textbox" MaxLength="35" />
                    </td>
                </tr>
                <tr align="left">
                    <td>
                        <asp:Label ID="lblUserName" runat="server" Text="<%$Resources:Labels, UserName%>" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="textbox" MaxLength="20" />
                        <asp:RequiredFieldValidator ID="txtUserNameValidator" ControlToValidate="txtUserName"
                            Display="Dynamic" ValidationGroup="AddUser" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                            ErrorMessage="<%$ Resources:ErrorMessages, RequiredUserName %>" runat="server" />
                        <ajax:ValidatorCalloutExtender ID="txtUserNameValidatorCalloutExtender" runat="server"
                            TargetControlID="txtUserNameValidator" />
                        <asp:CustomValidator ID="UserNameCustomValidator" runat="server" ControlToValidate="txtUserName"
                            Text="*" OnServerValidate="UserName_ServerValidate" CssClass="failureNotification"
                            Display="Dynamic" SetFocusOnError="true" ValidationGroup="AddUser" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateUserName %>" />
                        <ajax:ValidatorCalloutExtender ID="UserNameCustomValidatorValidatorCalloutExtender"
                            runat="server" TargetControlID="UserNameCustomValidator" />
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblDCAGroup" runat="server" Text="<%$Resources:Labels, DCAName%>" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDCAGroup" runat="server" CssClass="listmenu" DataTextField="Agent_Name"
                            DataValueField="Agent_Id" />
                        <asp:RequiredFieldValidator ID="ddlDCAGroupValidator" ControlToValidate="ddlDCAGroup"
                            InitialValue="0" Display="Dynamic" ValidationGroup="AddUser" SetFocusOnError="true"
                            Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredDCAName %>"
                            runat="server" />
                        <ajax:ValidatorCalloutExtender ID="ddlDCAGroupValidatorCalloutExtender" runat="server"
                            TargetControlID="ddlDCAGroupValidator" />
                    </td>
                </tr>
                <tr align="left">
                    <td>
                        <asp:Label ID="lblPassword" runat="server" Text="<%$Resources:Labels, Password%>" />
                    </td>
                    <td colspan="2" align="left">
                        <asp:TextBox ToolTip="Only @,#,$,%,^,&,+,= special characters are accepted" ID="txtPassword"
                            runat="server" CssClass="textbox" TextMode="Password" MaxLength="15" />
                        <asp:RequiredFieldValidator ID="PasswordValidator" ControlToValidate="txtPassword"
                            Display="Dynamic" ValidationGroup="AddUser" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                            ErrorMessage="<%$ Resources:ErrorMessages, RequiredPassword %>" runat="server" />
                        <asp:RegularExpressionValidator ID="PasswordRegularExpressionValidator" ControlToValidate="txtPassword"
                            runat="server" CssClass="failureNotification" SetFocusOnError="true" ErrorMessage="<%$ Resources:ErrorMessages, IncorrectFormat %>"
                            ValidationExpression="^.*(?=.{7,15})(?=.*[@#$%^&+=]).*$" ValidationGroup="AddUser"
                            Text="*" />
                        <ajax:ValidatorCalloutExtender ID="PasswordValidatorCalloutExtender" runat="server"
                            TargetControlID="PasswordRegularExpressionValidator" />
                        <ajax:ValidatorCalloutExtender ID="txtPasswordValidatorCalloutExtender" runat="server"
                            TargetControlID="PasswordValidator" />
                        <asp:Label ID="lblPasswordLength" CssClass="failurenotification" runat="server" Text="Minimum 7 characters, 1 special character" />
                    </td>
                    <td>
                        <asp:Label ID="lblConfirmPwd" runat="server" Text="<%$Resources:Labels, ConfirmPassword%>" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="textbox passwordEntry"
                            TextMode="Password" MaxLength="15" />
                        <asp:RequiredFieldValidator ID="ConfirmPasswordValidator" runat="server" ControlToValidate="txtConfirmPassword"
                            CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredConfirmPassword %>"
                            ValidationGroup="AddUser" Text="*" />
                        <asp:CompareValidator ID="CompareValidator" runat="server" CssClass="failureNotification"
                            ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" ValidationGroup="AddUser"
                            Text="*" ErrorMessage="<%$ Resources:ErrorMessages, InvalidConfirmPassword %>" />
                        <ajax:ValidatorCalloutExtender ID="CompareValidatorCalloutExtender" runat="server"
                            TargetControlID="CompareValidator" />
                        <ajax:ValidatorCalloutExtender ID="ConfirmPasswordValidatorCalloutExtender" runat="server"
                            TargetControlID="ConfirmPasswordValidator" />
                    </td>
                </tr>
                <tr align="left">
                    <td>
                        <asp:Label ID="lblUserType" runat="server" Text="<%$Resources:Labels, UserType%>" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlUserType" runat="server" CssClass="listmenu" />
                        <asp:RequiredFieldValidator ID="ddlUserTypeValidator" ControlToValidate="ddlUserType"
                            InitialValue="0" Display="Dynamic" ValidationGroup="AddUser" SetFocusOnError="true"
                            Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredUserType %>"
                            runat="server" />
                        <ajax:ValidatorCalloutExtender ID="ddlUserTypeValidatorCalloutExtender" runat="server"
                            TargetControlID="ddlUserTypeValidator" />
                    </td>
                    <td nowrap="nowrap" colspan="2">
                        &nbsp;
                    </td>                    
                </tr>
                <tr>
                    <td colspan="5">
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="5">
                        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="button" ValidationGroup="AddUser"
                            CausesValidation="true" OnClick="btnAdd_Click" />
                        &nbsp;
                        <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="false" CssClass="button"
                            OnClick="btnReset_Click" />
                    </td>
                </tr>
            </table>
            <uc1:MessageBox ID="ucMessageBox" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
