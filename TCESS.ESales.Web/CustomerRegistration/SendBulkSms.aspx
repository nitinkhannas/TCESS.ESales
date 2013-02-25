<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SendBulkSms.aspx.cs" Inherits="CustomerRegistration_SendBulkSms" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PageNameContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="progressBar" runat="server" AssociatedUpdatePanelID="uplMainPanel"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="overlay">
                <div class="ajaxloader">
                    <img src='<%= ResolveClientUrl("~/Images/ajax-loader.gif")%>' style="vertical-align: middle"
                        alt="Processing" />Processing ...
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="uplMainPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" cellspacing="5">
                <tr>
                    <td>
                        <asp:Label ID="LabelCount" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                        <asp:Label ID="lblAction" runat="server" Text="<%$Resources:Labels, Action%>"></asp:Label>
                        <br />
                        <br />
                        <br />
                        <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAction_SelectedIndexChanged">
                            <asp:ListItem Text="Send Bulk  SMS To All Customer" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Yestarday Booking Not Done" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Today Booking Not Done" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Single Message" Value="4"></asp:ListItem>
                             <asp:ListItem Text="Send Code SMS" Value="5"></asp:ListItem>
                            <asp:ListItem Value="6">Daily Report</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" valign="top">
                        <table width="100%">
                            <tr>
                                <td align="left" valign="top">
                                    <table id="trMobile" width="100%" cellspacing="0" runat="server" visible="false">
                                        <tr>
                                            <td align="left" valign="top" style="padding-bottom: 1px;">
                                                Mobile number
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top">
                                                <asp:TextBox ID="txtMobile" MaxLength="10" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldMobile" ControlToValidate="txtMobile"
                                                    InitialValue="" Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true"
                                                    Text="*" CssClass="failureNotification" ErrorMessage="Mobile number can not be left blank"
                                                    runat="server" />
                                                <ajax:ValidatorCalloutExtender ID="RequiredFieldMobileValidatorCalloutExtender" runat="server"
                                                    TargetControlID="RequiredFieldMobile" />
                                                <asp:RegularExpressionValidator ID="MobileValidator" runat="server" ControlToValidate="txtMobile"
                                                    Display="Dynamic" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup"
                                                    ErrorMessage="<%$ Resources:ErrorMessages, InvalidMobileNumber %>" CssClass="failureNotification"
                                                    ValidationExpression="^((\+)?(\d{2}))?(\d{10}){1}?$" />
                                                <ajax:ValidatorCalloutExtender ID="MobileValidatorCalloutExtender" runat="server"
                                                    TargetControlID="MobileValidator" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMessage" runat="server" Text="Message"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtMessage" MaxLength="10" runat="server" Height="65px" TextMode="MultiLine"
                                        Width="500px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="MessageRequiredValidator" ControlToValidate="txtMessage"
                                        InitialValue="" Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true"
                                        Text="*" CssClass="failureNotification" ErrorMessage="Message can not be left blank"
                                        runat="server" />
                                    <ajax:ValidatorCalloutExtender ID="MessageRequiredValidatorCalloutExtender" runat="server"
                                        TargetControlID="MessageRequiredValidator" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSendSMS" ValidationGroup="SaveGroup" runat="server" Text="Send"
                                        CssClass="button" OnClick="btnSendSMS_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="middle">
                    </td>
                    <td align="left" valign="top">
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
