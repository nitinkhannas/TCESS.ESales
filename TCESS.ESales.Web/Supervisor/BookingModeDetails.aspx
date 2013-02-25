<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="BookingModeDetails.aspx.cs" Inherits="Supervisor_BookingModeDetails" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, BookingModeDetails%>"
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
            <div style="overflow: auto; width: 100%;">
                <Custom:GridViewAlwaysShow ID="grdBookingModeDetails" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="BookingDetails_Id" AllowPaging="true" PageSize="10" BorderColor="#3366CC"
                    BorderStyle="Solid" BorderWidth="1px" Font-Size="Small" Width="100%" HorizontalAlign="Center"
                    CellPadding="5" ShowFooter="true" OnRowDataBound="grdBookingModeDetails_RowDataBound"
                    OnRowCommand="grdBookingModeDetails_RowCommand">
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, BookingDate%>" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <%# Convert.ToDateTime(Eval("BookingDetails_Date")).ToString("dd MMM yyyy") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, BookingMode%>" ItemStyle-Width="220px">
                            <ItemTemplate>
                                <asp:Label ID="lblBookingMode" runat="server" Text='<%# Bind("BookingDetails_Mode_Name") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlBookingMode" CssClass="listmenu" runat="server" DataValueField="BookingMode_Id"
                                    DataTextField="BookingMode_Name" />
                                <asp:RequiredFieldValidator ID="BookingModeValidator" ControlToValidate="ddlBookingMode"
                                    Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                                    InitialValue="0" CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredBookingMode%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="BookingModeValidatorCallOut" runat="server" TargetControlID="BookingModeValidator" />
                                <asp:CustomValidator ID="CustomBookingModeValidator" runat="server" ControlToValidate="ddlBookingMode"
                                    Text="*" CssClass="failureNotification" OnServerValidate="BookingMode_ServerValidate"
                                    SetFocusOnError="true" Display="Dynamic" ValidationGroup="SaveGroup" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateBookingMode %>" />
                                <ajax:ValidatorCalloutExtender ID="CustomBookingModeValidatorCallout" runat="server"
                                    TargetControlID="CustomBookingModeValidator" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, StartTime%>">
                            <ItemTemplate>
                                <asp:Label ID="lblStartTime" runat="server" Text='<%# Bind("BookingDetails_StartTime") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlStartTime" runat="server">
                                    <asp:ListItem Text="Select Start Time" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="00:00" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="01:00" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="02:00" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="03:00" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="04:00" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="05:00" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="06:00" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="07:00" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="08:00" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="09:00" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="10:00" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="11:00" Value="12"></asp:ListItem>
                                    <asp:ListItem Text="12:00" Value="13"></asp:ListItem>
                                    <asp:ListItem Text="13:00" Value="14"></asp:ListItem>
                                    <asp:ListItem Text="14:00" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="15:00" Value="16"></asp:ListItem>
                                    <asp:ListItem Text="16:00" Value="17"></asp:ListItem>
                                    <asp:ListItem Text="17:00" Value="18"></asp:ListItem>
                                    <asp:ListItem Text="18:00" Value="19"></asp:ListItem>
                                    <asp:ListItem Text="19:00" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="20:00" Value="21"></asp:ListItem>
                                    <asp:ListItem Text="21:00" Value="22"></asp:ListItem>
                                    <asp:ListItem Text="22:00" Value="23"></asp:ListItem>
                                    <asp:ListItem Text="23:00" Value="24"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="StartTimeValidator" ControlToValidate="ddlStartTime"
                                    Display="Dynamic" InitialValue="0" ValidationGroup="SaveGroup" SetFocusOnError="true"
                                    Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredStartTime %>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="StartTimeValidatorCalloutExtender" runat="server"
                                    TargetControlID="StartTimeValidator" />
                                <asp:CustomValidator ID="ddlStartTimeCustomValidator" runat="server" ControlToValidate="ddlStartTime"
                                    Text="*" CssClass="failureNotification" OnServerValidate="AddBookingModeDetails_ServerValidate"
                                    SetFocusOnError="true" Display="Dynamic" ValidationGroup="SaveGroup" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateBookingModetime %>" />
                                <ajax:ValidatorCalloutExtender ID="ddlStartTimeCustomValidatorCustomValidatorCalloutExtender"
                                    runat="server" TargetControlID="ddlStartTimeCustomValidator" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, EndTime%>">
                            <ItemTemplate>
                                <asp:Label ID="lblEndTime" runat="server" Text='<%# Bind("BookingDetails_EndTime") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlEndTime" runat="server">
                                    <asp:ListItem Text="Select End Time" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="00:00" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="01:00" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="02:00" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="03:00" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="04:00" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="05:00" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="06:00" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="07:00" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="08:00" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="09:00" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="10:00" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="11:00" Value="12"></asp:ListItem>
                                    <asp:ListItem Text="12:00" Value="13"></asp:ListItem>
                                    <asp:ListItem Text="13:00" Value="14"></asp:ListItem>
                                    <asp:ListItem Text="14:00" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="15:00" Value="16"></asp:ListItem>
                                    <asp:ListItem Text="16:00" Value="17"></asp:ListItem>
                                    <asp:ListItem Text="17:00" Value="18"></asp:ListItem>
                                    <asp:ListItem Text="18:00" Value="19"></asp:ListItem>
                                    <asp:ListItem Text="19:00" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="20:00" Value="21"></asp:ListItem>
                                    <asp:ListItem Text="21:00" Value="22"></asp:ListItem>
                                    <asp:ListItem Text="22:00" Value="23"></asp:ListItem>
                                    <asp:ListItem Text="23:00" Value="24"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="EndTimeValidator" ControlToValidate="ddlEndTime"
                                    Display="Dynamic" InitialValue="0" ValidationGroup="SaveGroup" SetFocusOnError="true"
                                    Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredEndTime %>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="EndTimeValidatorCalloutExtender" runat="server"
                                    TargetControlID="EndTimeValidator" />
                                <asp:CustomValidator ID="timeValidator" runat="server" ControlToValidate="ddlEndTime"
                                    CssClass="failureNotificaiton" ClientValidationFunction="ValidateEndTime" Text="*"
                                    SetFocusOnError="true" ErrorMessage="<%$ Resources:ErrorMessages, InvalidBookingModeTime %>" />
                                <ajax:ValidatorCalloutExtender ID="TimeValidatorExtender" runat="server" TargetControlID="TimeValidator" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, TimeInterval%>" ItemStyle-Width="80px">
                            <ItemTemplate>
                                <asp:Label ID="lblTimeInterval" runat="server" Text='<%# Bind("BookingDetails_TimeInterval") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="txtTimeInterval" Width="80px" MaxLength="1" />
                                <asp:CustomValidator ID="timeIntervalValidator" runat="server" ControlToValidate="txtTimeInterval"
                                    CssClass="failureNotificaiton" Text="*" SetFocusOnError="true" ErrorMessage="<%$ Resources:ErrorMessages, RequiredTimeInterval %>" />
                                <ajax:ValidatorCalloutExtender ID="TimeIntervalValidatorCalloutExtender" runat="server"
                                    TargetControlID="TimeIntervalValidator" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, TruckLimit%>" ItemStyle-Width="80px">
                            <ItemTemplate>
                                <asp:Label ID="lblTruckLimit" runat="server" Text='<%# Bind("BookingDetails_Trucks") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="txtTruckLimit" Width="80px" MaxLength="4" />
                                <asp:RequiredFieldValidator ID="TruckLimitValidator" ControlToValidate="txtTruckLimit"
                                    Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredTruckLimit %>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="TruckLimitValidatorCallout" runat="server" TargetControlID="TruckLimitValidator" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                            <FooterTemplate>
                                <asp:Button ID="lnkAdd" runat="server" CausesValidation="true" CommandName="AddNew"
                                    Text="<%$Resources:Labels, Add%>" CssClass="button" ValidationGroup="SaveGroup" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#397dbc" ForeColor="#336600" HorizontalAlign="Center" />
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
            <div>
             <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
            </div>
            <div id="validationdiv" runat="server" visible="false">
                <asp:CustomValidator ID="timeValidator" runat="server" ErrorMessage="CustomValidator" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
