<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index"
    ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>e-Sales Software :: For Sale of Tailings and By-Products DCAs - Tata Steel Ltd
        West Bokaro Ghatotand Ramgarh [Jharkhand]</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="e-Slale Software - For Sale of Tailings and by-products by DCAs to Tata Steel Ltd Ghatotand West Bokaro Ramgarh [Jharkhand]" />
    <meta name="keywords" content="e-Slale Software, e-Slale, Tata Steel, Bokaro, Software" />
    <link href="Styles/style.css" rel="stylesheet" type="text/css" />
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
            <div class="loginlefttext">
                For Sale of:<br>
                WASHERY TAILINGS
                <br>
                WASHERY REJECTS<br>
                WASHERY MIDDLINGS
            </div>
            <div class="loginbox">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td colspan="2" height="25" align="left" valign="middle" class="account_information">
                            Account Information
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" valign="top" class="account_information2">
                            Sign in to your account
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="left" valign="top" height="10">
                        </td>
                    </tr>
                    <tr>
                        <td height="30" align="left" valign="middle" nowrap="nowrap">
                            <label for="lblUserName">
                                User Name</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="textbox" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="UserNameValidator" ControlToValidate="txtUserName"
                                Display="Dynamic" ValidationGroup="LoginGroup" SetFocusOnError="true" Text="*"
                                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredUserName %>"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="30" align="left" valign="middle" nowrap="nowrap">
                            <label for="lblPassword">
                                Password</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPassword" CssClass="textbox" runat="server" TextMode="Password" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="PasswordValidator" ControlToValidate="txtPassword"
                                Display="Dynamic" ValidationGroup="LoginGroup" SetFocusOnError="true" Text="*"
                                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredPassword %>"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="left" valign="top" height="10">
                            <asp:CustomValidator ID="CustomValidator" Display="Dynamic" Font-Size="Small" CssClass="failureNotification"
                                runat="server" />
                            <asp:ValidationSummary ID="VSummary" runat="server" DisplayMode="List" ForeColor="Red"
                                CssClass="failurenotification" ValidationGroup="LoginGroup" ShowSummary="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td height="25" colspan="0" align="right" valign="middle">
                            <asp:HiddenField runat="server" ID="hdnMacAddress" />
                            <asp:Button ID="btnLogin" CausesValidation="true" ValidationGroup="LoginGroup" runat="server"
                                Text="SIGN IN" CssClass="button" OnClick="btnLogin_Click" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right" colspan="0" valign="top" class="forgot_password">
                            <asp:LinkButton runat="server" ID="lnkChangePassword" Text="Change Password" PostBackUrl="~/UserRegistration/ResetPassword.aspx"
                                Font-Underline="false" ForeColor="Black" />
                        </td>
                        <td style="width: 20px;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div class="footerbanner">
                <span><a href="#">
                    <img src="Images/spot_booking_service.jpg" alt="Spot Booking Service" title="Spot Booking Service" /></a></span>
                <span><a href="#">
                    <img src="Images/smart_card_booking_service.jpg" alt="Smart Card Booking Service"
                        title="Smart Card Booking Service" /></a></span> <span><a href="#">
                            <img src="Images/net_booking_service.jpg" alt="Net Booking Service" title="Net Booking Service" /></a>
                        </span>
            </div>
            <div class="contact">
                <span class="contacttext">Contact</span> <span><a href="#">
                    <img src="Images/call_landline.jpg" alt="Call From Landline Phone" title="Call From Landline Phone" /></a><a
                        href="#"><img src="Images/call_mobile_phone.jpg" alt="Call From Mobile Phone" title="Call From Mobile Phone" /></a><a
                            href="#"><img src="Images/e_sale_email.jpg" alt="Contact By Email" title="Contact By Email" /></a></span>
            </div>
            <div class="footer">
                <span class="copyright">© Copyright e-Sales Software</span></div>
            <div class="powerby_q3tech">
                Powered by <a href="http://www.q3tech.com/" target="_blank">
                    <img src="Images/Q3_technologies.jpg" alt="Q3 technologies, Inc." border="0" align="top"
                        title="Q3 technologies, Inc." /></a></div>
        </div>
    </div>
    </form>
</body>
</html>
