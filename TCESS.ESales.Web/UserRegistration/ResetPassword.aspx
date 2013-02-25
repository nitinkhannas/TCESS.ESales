<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs"
    Inherits="ResetPassword" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>e-Sales Software :: For Sale of Tailings and By-Products DCAs - Tata Steel Ltd
        West Bokaro Ghatotand Ramgarh [Jharkhand]</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="e-Slale Software - For Sale of Tailings and by-products by DCAs to Tata Steel Ltd Ghatotand West Bokaro Ramgarh [Jharkhand]" />
    <meta name="keywords" content="e-Slale Software, e-Slale, Tata Steel, Bokaro, Software" />
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="mainlogincontainer">
        <div class="e-Salelogo">
            <span class="punchline" title="For Sale of Tailings and by-products
by DCAs to Tata Steel Ltd West Bokaro Ghatotand  Ramgarh [Jharkhand]">For Sale of Tailings and By-Products
                <br />
                <span class="punchline2">DCAs - Tata Steel Ltd West Bokaro Ghatotand Ramgarh [Jharkhand]</span></span>
        </div>
        <div class="login_bg">
            <div class="resetLeftText">
                <div style="float: left">
                 <label class="pageNameContent">
                    Default Landing page for Reset Password
                    </label> 
                </div>
            </div>
            <div class="resetBox">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td colspan="2" height="25" align="left" valign="middle" class="account_information">
                        <label class="pageNameContent">
                            Reset Password</label> 
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="left" valign="top" height="10">
                            <asp:Label ID="lblMessage" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="30" align="left" valign="middle" nowrap="nowrap" style="width: 120px;">
                            <asp:Label ID="lblUserName" runat="server" AssociatedControlID="txtUserName" Text="User Name" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="textbox" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="UserNameValidator" runat="server" ControlToValidate="txtUserName"
                                Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredUserName %>"
                                ValidationGroup="LoginUserValidationGroup" />
                            <ajax:ValidatorCalloutExtender ID="UserNameValidatorCalloutExtender" runat="server"
                                TargetControlID="UserNameValidator" />
                        </td>
                    </tr>
                    <tr>
                        <td height="30" align="left" valign="middle" nowrap="nowrap" style="width: 120px;">
                            <asp:Label ID="lblOldPassword" runat="server" AssociatedControlID="txtOldPassword"
                                Text="Old Password" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtOldPassword" runat="server" CssClass="textbox" TextMode="Password" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="OldPasswordValidator" runat="server" ControlToValidate="txtOldPassword"
                                Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredOldPassword %>"
                                ValidationGroup="LoginUserValidationGroup" />
                            <ajax:ValidatorCalloutExtender ID="OldPasswordValidatorCalloutExtender" runat="server"
                                TargetControlID="OldPasswordValidator" />
                        </td>
                    </tr>
                    <tr>
                        <td height="30" align="left" valign="middle" nowrap="nowrap" style="width: 120px;">
                            <asp:Label ID="lblNewPassword" runat="server" AssociatedControlID="txtNewPassword"
                                Text="New Password" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewPassword" MaxLength="15" runat="server" CssClass="textbox passwordEntry"
                                TextMode="Password" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="PasswordValidator" runat="server" ControlToValidate="txtNewPassword"
                                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredNewPassword %>"
                                ValidationGroup="LoginUserValidationGroup" Text="*" />
                            <asp:RegularExpressionValidator ID="PasswordRegularExpressionValidator" ControlToValidate="txtNewPassword"
                                runat="server" CssClass="failureNotification" SetFocusOnError="true" ErrorMessage="<%$ Resources:ErrorMessages, IncorrectFormat %>"
                                ValidationExpression="^.*(?=.{7,15})(?=.*[@#$%^&+=]).*$" ValidationGroup="LoginUserValidationGroup"
                                Text="*" />
                            <ajax:ValidatorCalloutExtender ID="PasswordValidatorCalloutExtender" runat="server"
                                TargetControlID="PasswordValidator" />
                            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                TargetControlID="PasswordRegularExpressionValidator" />
                        </td>
                    </tr>
                    <tr>
                        <td height="30" align="left" valign="middle" nowrap="nowrap" style="width: 120px;">
                            <asp:Label ID="lblConfirmPassword" runat="server" AssociatedControlID="txtConfirmPassword"
                                Text="Confirm Password" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtConfirmPassword" MaxLength="15" runat="server" CssClass="textbox passwordEntry"
                                TextMode="Password" />
                        </td>
                        <td>
                            <asp:CompareValidator ID="CompareValidator" runat="server" CssClass="failureNotification"
                                ControlToValidate="txtConfirmPassword" ControlToCompare="txtNewPassword" ValidationGroup="LoginUserValidationGroup"
                                Text="*" ErrorMessage="<%$ Resources:ErrorMessages, InvalidConfirmPassword %>" />
                            <asp:RegularExpressionValidator ID="ConfirmPasswordRegularExpressionValidator" ControlToValidate="txtConfirmPassword"
                                runat="server" CssClass="failureNotification" SetFocusOnError="true" ErrorMessage="<%$ Resources:ErrorMessages, IncorrectFormat %>"
                                ValidationExpression="^.*(?=.{7,15})(?=.*[@#$%^&+=]).*$" ValidationGroup="LoginUserValidationGroup"
                                Text="*" />
                            <asp:RequiredFieldValidator ID="ConfirmPasswordValidator" runat="server" ControlToValidate="txtConfirmPassword"
                                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredConfirmPassword %>"
                                ValidationGroup="LoginUserValidationGroup" Text="*" />
                            <ajax:ValidatorCalloutExtender ID="CompareValidatorCalloutExtender" runat="server"
                                TargetControlID="CompareValidator" />
                            <ajax:ValidatorCalloutExtender ID="ConfirmPasswordValidatorCalloutExtender"
                                runat="server" TargetControlID="ConfirmPasswordValidator" />
                            <ajax:ValidatorCalloutExtender ID="ConfirmPasswordRegularExpressionValidatorCalloutExtender"
                                runat="server" TargetControlID="ConfirmPasswordRegularExpressionValidator" />
                        </td>
                    </tr>
                    <tr>
                        <td height="25" colspan="3" align="center">
                            <asp:Button ID="btnSubmit" runat="server" CommandName="Login" CssClass="button" Text="Submit"
                                ValidationGroup="LoginUserValidationGroup" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" runat="server" CommandName="Login" CssClass="button" PostBackUrl="~/Index.aspx" Text="Cancel" />                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="30" colspan="3" align="left" valign="top" class="account_information2 failureNotification">
                            Instructions
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="left" valign="middle" nowrap="nowrap">
                            <label for="lblUserName" class="failureNotification">
                                Password length minimum: 7</label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="left" valign="middle" nowrap="nowrap">
                            <label for="lblUserName" class="failureNotification">
                                Non-alphanumeric characters required: 1</label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div class="footerbanner">
            </div>
            <div class="contact">
            </div>
            <div class="footer">
                <span class="copyright">© Copyright e-Sales Software</span></div>
            <div class="powerby_q3tech">
                Powered by <a href="http://www.q3tech.com/" target="_blank">
                    <img src="../Images/Q3_technologies.jpg" alt="Q3 technologies, Inc." border="0" align="top"
                        title="Q3 technologies, Inc." /></a></div>
        </div>
    </div>
    </form>
</body>
</html>
