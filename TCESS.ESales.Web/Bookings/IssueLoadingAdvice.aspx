<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="IssueLoadingAdvice.aspx.cs" Inherits="Bookings_IssueLoadingAdvice"
    ValidateRequest="false" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, AuthorizeBooking%>"
        CssClass="pageNameContent" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
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
            <table width="100%" cellspacing="10" cellpadding="5">
                <tr align="left">
                    <td nowrap="nowrap">
                        <asp:Label ID="lblMobileNumber" runat="server" Text="<%$Resources:Labels, MobileNo%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox ID="txtPhoneNumber" onkeypress="return runScript(event)" runat="server"
                            CssClass="textbox" MaxLength="15" />
                        <asp:RequiredFieldValidator ID="RFVtxtPhoneNumber" ControlToValidate="txtPhoneNumber"
                            Display="Dynamic" ValidationGroup="Customer" SetFocusOnError="true" Text="*"
                            CssClass="failureNotification" ErrorMessage="Enter Phone Number" runat="server" />
                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPhoneNumber"
                            FilterMode="ValidChars" ValidChars="+,-" FilterType="Numbers,Custom" />
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="lblReferenceId" runat="server" Text="<%$Resources:Labels, ReferanceID%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox ID="txtSmsRegNo" runat="server" CssClass="textbox" Wrap="False" />
                        <asp:RequiredFieldValidator ID="RFVtxtSmsRegNo" ControlToValidate="txtSmsRegNo" Display="Dynamic"
                            ValidationGroup="Customer" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                            ErrorMessage="Enter Referance ID" runat="server" />
                        <asp:Button ID="smsValidate" runat="server" CssClass="button" Text="SMS Validate"
                            OnClick="smsValidate_Click" ValidationGroup="Customer" />
                    </td>
                </tr>
                <tr align="left">
                    <td nowrap="nowrap">
                        <asp:Label ID="lblCustomerCode" runat="server" Text="<%$Resources:Labels, CustomerCode%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="textbox" Wrap="False" />
                        <asp:RequiredFieldValidator ID="CustomerCodeValidator" runat="server" ControlToValidate="txtCustomerCode"
                            CssClass="failureNotification" Display="Dynamic" ErrorMessage="<%$ Resources:ErrorMessages, RequiredCustomerCode %>"
                            SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup" />
                        <ajax:ValidatorCalloutExtender ID="CustomerCodeValidatorCalloutExtender" runat="server"
                            TargetControlID="CustomerCodeValidator" />
                        <asp:Button ID="btnValidate" runat="server" CssClass="button" OnClick="btnValidate_Click"
                            Text="Validate" Visible="false" />
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="lblMaterialType" runat="server" Text="<%$Resources:Labels, MaterialType%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:DropDownList ID="ddlMaterial" runat="server" AutoPostBack="true" CssClass="listmenu"
                            DataTextField="Cust_Mat_MaterialName" DataValueField="Cust_Mat_MaterialId" OnSelectedIndexChanged="ddlMaterial_SelectedIndexChanged" />
                        <asp:RequiredFieldValidator ID="MaterialValidator" runat="server" ControlToValidate="ddlMaterial"
                            CssClass="failureNotification" Display="Dynamic" ErrorMessage="<%$ Resources:ErrorMessages, RequiredMaterialType%>"
                            InitialValue="0" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup" />
                        <ajax:ValidatorCalloutExtender ID="MaterialValidatorCallOutExtender" runat="server"
                            TargetControlID="MaterialValidator" />
                    </td>
                </tr>
                <tr align="left">
                    <td nowrap="nowrap">
                        <asp:Label ID="lblBookingType" runat="server" Text="<%$Resources:Labels, BookingType%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox CssClass="textbox" ID="txtBookingMode" ReadOnly="true" runat="server" />
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="lblBookingDate" runat="server" Text="Booking Date" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox CssClass="textbox" ID="txtBookingDate" ReadOnly="true" runat="server"
                            Text="Booking Date" />
                    </td>
                </tr>
                <tr align="left">
                    <td nowrap="nowrap">
                        <asp:Label ID="lblAllowedBooking" runat="server" Text="Total Bookings Allowed" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox CssClass="textbox" ID="txtAllowedBooking" ReadOnly="true" runat="server" />
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="lblTotalBookingDone" runat="server" Text="Booking Completed" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox CssClass="textbox" ID="txtTotalBookings" ReadOnly="true" runat="server" />
                    </td>
                </tr>
                <tr align="left">
                    <td nowrap="nowrap">
                        <asp:Label ID="lblTradeName" runat="server" Text="<%$Resources:Labels, TradeName%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox CssClass="textbox" ID="txtTradeName" runat="server" ReadOnly="true" />
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="lblFirmName" runat="server" Text="<%$Resources:Labels, FirmName%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox CssClass="textbox" ID="txtFirmName" runat="server" ReadOnly="true" />
                    </td>
                </tr>
                <tr align="left">
                    <td nowrap="nowrap">
                        <asp:Label ID="lblUnitAddress" runat="server" Text="<%$Resources:Labels, UnitAddress%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="textarea" MaxLength="150" ReadOnly="true"
                            TextMode="MultiLine" />
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="lblMobileNo" runat="server" Text="<%$Resources:Labels, MobileNo%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox CssClass="textbox" ID="txtMobileNo" runat="server" ReadOnly="true" />
                    </td>
                </tr>
                <tr align="left">
                    <td nowrap="nowrap">
                        <asp:Label ID="lblAdvanceAmount" runat="server" Text="<%$Resources:Labels, BookingAdvance%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox ID="txtAdvanceAmount" runat="server" CssClass="textbox" />
                        <ajax:FilteredTextBoxExtender ID="FilteredAdvanceAmountExtender" runat="server" TargetControlID="txtAdvanceAmount"
                            FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="." />
                        <asp:RequiredFieldValidator ID="AdvanceAmountValidator" ControlToValidate="txtAdvanceAmount"
                            Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                            CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredAdvanceAmount %>"
                            runat="server" />
                        <ajax:ValidatorCalloutExtender ID="AdvanceAmountValidatorCallOutExtender" runat="server"
                            TargetControlID="AdvanceAmountValidator" />
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="lblStandalone" runat="server" Text="<%$Resources:Labels, AuthorizedTruck%>" />
                    </td>
                    <td nowrap="nowrap">
                        <table align="left">
                            <tr>
                                <td align="left">
                                    <asp:RadioButtonList ID="rdStandAlone" runat="server" RepeatDirection="Horizontal"
                                        CssClass="radioButtons" OnSelectedIndexChanged="rdStandAlone_SelectedIndexChanged"
                                        AutoPostBack="true">
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr align="left">
                    <td nowrap="nowrap">
                        <asp:Label ID="lblTruckNo" runat="server" Text="<%$Resources:Labels, TruckNo%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:DropDownList ID="ddlTruck" runat="server" AutoPostBack="True" CssClass="listmenu"
                            DataTextField="Truck_RegNo" DataValueField="Truck_Id" OnSelectedIndexChanged="ddlTruck_SelectedIndexChanged"
                            Enabled="false" />
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="lblStandaloneTruckRegNo" runat="server" Text="<%$Resources:Labels, StandaloneTruckRegNo%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox ID="txtStandaloneTruck" runat="server" CssClass="textbox" Wrap="False" />
                        <asp:Button ID="btnGetStandAlone" runat="server" CssClass="button" OnClick="btnGetStandAlone_Click"
                            Text="Validate" />
                    </td>
                </tr>
                <tr align="left">
                    <td nowrap="nowrap">
                        <asp:Label ID="lblNumberofWheel" runat="server" Text="<%$Resources:Labels, TruckWheeler%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox CssClass="textbox" ID="txtNumberofWheel" runat="server" ReadOnly="true" />
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="lblCarryCapacity" runat="server" Text="<%$Resources:Labels, CarryCapacity%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox CssClass="textbox" ID="txtCarryCapacity" runat="server" ReadOnly="true" />
                    </td>
                </tr>
                <tr align="left">
                    <td nowrap="nowrap">
                        <asp:Label ID="lblDriverDetails" runat="server" Text="<%$Resources:Labels, DriverDetails%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox CssClass="textbox" ID="txtDriverDetails" runat="server" MaxLength="150"
                            ReadOnly="true" />
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="lblMaxLiftQty" runat="server" Text="<%$Resources:Labels, MaximumLiftingLimit%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox CssClass="textbox" ID="txtMaxLiftQty" runat="server" ReadOnly="true" />
                    </td>
                </tr>
                <tr align="left">
                    <td nowrap="nowrap">
                        <asp:Label ID="lblCurrentQty" runat="server" Text="<%$Resources:Labels, CurrentQuantity%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox CssClass="textbox" ID="txtCurrentQty" runat="server" ReadOnly="True" />
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="lblTotalIssuedQty" runat="server" Text="<%$Resources:Labels, TotalQuantityIssued%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox CssClass="textbox" ID="txtTotalIssuedQty" runat="server" ReadOnly="true" />
                    </td>
                </tr>
                <tr align="left">
                    <td nowrap="nowrap">
                        <asp:CheckBox ID="chkSignValid" runat="server" Enabled="false" Text="Customer Validated" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:Button ID="btnSign" runat="server" CssClass="button" OnClick="btnSign_Click"
                            Text="<%$Resources:Labels, ViewSignature%>" />
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                </tr>
                <tr align="left">
                    <td nowrap="nowrap">
                        <asp:Label ID="lblBookingStatus" runat="server" Text="<%$Resources:Labels, BookingStatus%>" />
                    </td>
                    <td nowrap="nowrap">
                        <table align="left">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rdBookingStatus" runat="server" RepeatDirection="Horizontal"
                                        CssClass="radioButtons" AutoPostBack="true" OnSelectedIndexChanged="rdBookingStatus_SelectedIndexChanged">
                                        <asp:ListItem Value="1">Accept </asp:ListItem>
                                        <asp:ListItem Value="2" Selected="True">Reject</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="lblRejectionReason" runat="server" Text="<%$Resources:Labels, RejectionReason%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox ID="txtRejectionReason" runat="server" CssClass="textarea" MaxLength="150"
                            TextMode="MultiLine" />
                        <asp:RegularExpressionValidator ID="RejectionReasonValidator" runat="server" ControlToValidate="txtRejectionReason"
                            CssClass="failureNotification" Display="Dynamic" ErrorMessage="<%$ Resources:ErrorMessages, LengthExceeded %>"
                            SetFocusOnError="true" Text="*" ValidationExpression="^[\s\S]{0,150}$" ValidationGroup="SaveGroup" />
                        <ajax:ValidatorCalloutExtender ID="RejectionReasonValidatorCalloutExtender" runat="server"
                            TargetControlID="RejectionReasonValidator" />
                    </td>
                </tr>
                <tr align="left">
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                </tr>
                <tr align="center">
                    <td nowrap="nowrap" colspan="5">
                        <asp:Label ID="lblCounterNo" runat="server" CssClass="pageNameContent" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5" align="center">
                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="<%$Resources:Labels, Save%>"
                            OnClick="btnSave_Click" ValidationGroup="SaveGroup" />
                        &nbsp;<asp:Button ID="btnReset" CssClass="button" runat="server" Text="<%$Resources:Labels, Reset%>"
                            OnClick="btnReset_Click" />
                    </td>
                </tr>
            </table>
            <uc1:MessageBox ID="ucMessageBox" runat="server" />
            <uc3:ViewImage ID="ucViewImage" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
