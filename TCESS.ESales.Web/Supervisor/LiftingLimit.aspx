<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LiftingLimit.aspx.cs" Inherits="Supervisor_LiftingLimit" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="Manage Lifting Limit" CssClass="pageNameContent" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uplMainPanel"
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
                <Custom:GridViewAlwaysShow AllowPaging="false" ID="grdCustCautionLstMaster" runat="server"
                    AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                    Font-Size="Small" Width="100%" HorizontalAlign="Center" CellPadding="5" DataKeyNames="LiftingLimit_ID"
                    ShowFooter="True" OnRowCommand="grdCustCautionLstMaster_RowCommand" OnRowDataBound="grdCustCautionLstMaster_RowDataBound"
                    OnRowDeleting="grdCustCautionLstMaster_RowDeleting" OnRowEditing="grdCustCautionLstMaster_RowEditing"
                    OnRowUpdating="grdCustCautionLstMaster_RowUpdating" OnRowCancelingEdit="grdCustCautionLstMaster_RowCancelingEdit">
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, BusinessType%>">
                            <EditItemTemplate>
                                <asp:DropDownList ID="txtBusinessType" runat="server" Width="200px" DataTextField="BusinessType_Name"
                                    DataValueField="BusinessType_Id">
                                </asp:DropDownList>
                                <%-- <asp:TextBox ID="txtBusinessType" runat="server" Text='<%# Bind("LiftingLimit_Business_Name") %>'
                                    MaxLength="5" Width="50px" />--%>
                                <asp:RequiredFieldValidator ID="txtBusinessTypeValidator" ControlToValidate="txtBusinessType"
                                    Display="Dynamic" ValidationGroup="EditMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredMaterialTypeCode%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtBusinessTypeValidatorCallOut" runat="server"
                                    TargetControlID="txtBusinessTypeValidator" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblBusinessType" runat="server" Text='<%# Bind("LiftingLimit_Business_Name") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlBusinessType" runat="server" Width="200px" DataTextField="BusinessType_Name"
                                    DataValueField="BusinessType_Id">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="ddlCustomerNameValidator" ControlToValidate="ddlBusinessType"
                                    InitialValue="0" Display="Dynamic" ValidationGroup="AddCustCautionList" SetFocusOnError="true"
                                    Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredBusinessType %>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="ddlCustomerNameValidatorCalloutExtender" runat="server"
                                    TargetControlID="ddlCustomerNameValidator" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Liftinglimitdata%>">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlLiftingLimit" runat="server" Width="200px" DataTextField="liftinginterval_Interval"
                                    DataValueField="liftinginterval_Id" />
                                <%--<asp:TextBox ID="txtLiftingLimit" runat="server" Text='<%# Bind("LiftingLimit_Limit") %>'
                                    MaxLength="5" Width="50px" />--%>
                                <asp:RequiredFieldValidator ID="ddlLiftingLimitValidator" ControlToValidate="ddlLiftingLimit"
                                    Display="Dynamic" ValidationGroup="EditMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredMaterialTypeCode%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="ddlLiftingLimitValidatorCallOut" runat="server"
                                    TargetControlID="ddlLiftingLimitValidator" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <%#Eval("LiftingLimit_Limit")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <%--<asp:TextBox ID="tbLiftingLimit" runat="server" MaxLength="3"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlLiftingLimit" runat="server" Width="200px" DataTextField="liftinginterval_Interval"
                                    DataValueField="liftinginterval_Id" />
                                <asp:RequiredFieldValidator ID="tbLiftingLimitValidator" ControlToValidate="ddlLiftingLimit"
                                    Display="Dynamic" ValidationGroup="AddCustCautionList" SetFocusOnError="true"
                                    Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredLiftingLimit %>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="tbTimeIntervalValidatorCallOut1" runat="server"
                                    TargetControlID="tbLiftingLimitValidator" />
                                <%--<ajax:FilteredTextBoxExtender ID="smsLimitTextBoxExtender1" runat="server" TargetControlID="ddlLiftingLimit"
                                    FilterMode="ValidChars" FilterType="Numbers" />--%>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, TimeInterval%>">
                            <EditItemTemplate>
                                <%--<asp:TextBox ID="txtTimeInterval" runat="server" Text='<%# Bind("LiftingLimit_Timeinterval") %>'
                                    MaxLength="5" Width="50px" />--%>
                                <asp:DropDownList ID="ddlTimeInterval" runat="server" Width="200px" DataTextField="liftinginterval_Interval"
                                    DataValueField="liftinginterval_Id" />
                                <asp:RequiredFieldValidator ID="ddlTimeIntervalValidator" ControlToValidate="ddlTimeInterval"
                                    Display="Dynamic" ValidationGroup="EditMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredMaterialTypeCode%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="ddlTimeIntervalValidatorCallOut" runat="server"
                                    TargetControlID="ddlTimeIntervalValidator" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <%#Eval("LiftingLimit_Timeinterval")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <%--<asp:TextBox ID="tbTimeInterval" runat="server" MaxLength="2"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlTimeInterval" runat="server" Width="200px" DataTextField="liftinginterval_Interval"
                                    DataValueField="liftinginterval_Id" />
                                <asp:RequiredFieldValidator ID="tbTimeIntervalValidator" ControlToValidate="ddlTimeInterval"
                                    Display="Dynamic" ValidationGroup="AddCustCautionList" SetFocusOnError="true"
                                    Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredTimeInterval %>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="tbTimeIntervalValidatorCallOut" runat="server"
                                    TargetControlID="tbTimeIntervalValidator" />
                                <%--<ajax:FilteredTextBoxExtender ID="smsLimitTextBoxExtender" runat="server" TargetControlID="ddlTimeInterval"
                                    FilterMode="ValidChars" FilterType="Numbers" />--%>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Allotted Quantity(In Tonnes)">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBookingLimit" runat="server" Text='<%# Bind("Annual_LiftingLimit_Limit") %>'
                                    MaxLength="5" Width="50px" />
                                <asp:RequiredFieldValidator ID="txtBookingLimitValidator" ControlToValidate="txtBookingLimit"
                                    Display="Dynamic" ValidationGroup="EditMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredMaterialTypeCode%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtBookingLimitValidatorCallOut" runat="server"
                                    TargetControlID="txtBookingLimitValidator" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <%#Eval("Annual_LiftingLimit_Limit")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="tbBookingLimit" runat="server" MaxLength="3"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="tbBookingLimitValidator" ControlToValidate="tbBookingLimit"
                                    Display="Dynamic" ValidationGroup="AddCustCautionList" SetFocusOnError="true"
                                    Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredLiftingLimit %>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="tbBookingLimitCallOut1" runat="server" TargetControlID="tbBookingLimitValidator" />
                                <ajax:FilteredTextBoxExtender ID="smsLimitTextBoxExtender11" runat="server" TargetControlID="tbBookingLimit"
                                    FilterMode="ValidChars" FilterType="Numbers" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Truck Type">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlTruckType" runat="server" Width="200px" DataTextField="TruckRegType_Name"
                                    DataValueField="TruckRegType_Id">
                                </asp:DropDownList>
                                <%--<asp:TextBox ID="txtTruckType" runat="server" Text='<%# Bind("LiftingLimit_TruckRegType_Name") %>'
                                    MaxLength="5" Width="50px" />--%>
                                <asp:RequiredFieldValidator ID="ddlTruckTypeValidator" ControlToValidate="ddlTruckType"
                                    Display="Dynamic" ValidationGroup="EditMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredMaterialTypeCode%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="ddlTruckTypeValidatorCallOut" runat="server" TargetControlID="ddlTruckTypeValidator" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTruckType" runat="server" Text='<%# Bind("LiftingLimit_TruckRegType_Name") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlTruckType" runat="server" Width="200px" DataTextField="TruckRegType_Name"
                                    DataValueField="TruckRegType_Id">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="ddlTruckTypeValidator" ControlToValidate="ddlTruckType"
                                    InitialValue="0" Display="Dynamic" ValidationGroup="AddCustCautionList" SetFocusOnError="true"
                                    Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredBusinessType %>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="ddlTruckTypeValidatorCalloutExtender" runat="server"
                                    TargetControlID="ddlTruckTypeValidator" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="<%$Resources:Labels, Update%>" Font-Underline="False" ValidationGroup="EditMaterialType" />
                                <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="<%$Resources:Labels, Cancel%>" Font-Underline="False" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="lnkAdd" runat="server" CausesValidation="true" CommandName="AddNew"
                                    Text="Add" CssClass="button" ValidationGroup="AddCustCautionList" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%#Bind("LiftingLimit_ID")%>' />
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text="<%$Resources:Labels, Delete%>" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                                    Font-Underline="False" CommandArgument='<%#Bind("LiftingLimit_ID")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#397dbc" ForeColor="#003399" HorizontalAlign="Center" />
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
            <div runat="server" id="divHistory">
                <asp:Label ID="lblHistory" runat="server" Text="<%$Resources:Labels, History%>" CssClass="pageNameContent" />
                <Custom:GridViewAlwaysShow AllowPaging="true" ID="gridHistory" runat="server" AutoGenerateColumns="False"
                    BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                    PageSize="10" Width="100%" HorizontalAlign="Center" CellPadding="5" ShowFooter="True"
                    DataKeyNames="LiftingLimit_ID">
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, BusinessType%>">
                            <ItemTemplate>
                                <asp:Label ID="lblBusinessType" runat="server" Text='<%# Bind("LiftingLimit_Business_Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Liftinglimitdata%>">
                            <ItemTemplate>
                                <%#Eval("LiftingLimit_Limit")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, TimeInterval%>">
                            <ItemTemplate>
                                <%#Eval("LiftingLimit_Timeinterval")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Allotted Quantity(In Tonnes)">
                            <ItemTemplate>
                                <%#Eval("Annual_LiftingLimit_Limit")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Truck Type">
                            <ItemTemplate>
                                <asp:Label ID="lblBusinessType" runat="server" Text='<%# Bind("LiftingLimit_TruckRegType_Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#397dbc" ForeColor="#003399" HorizontalAlign="Center" />
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
            <div runat="server" id="divNote">
                <asp:Label ID="lblNote" runat="server" Text="Note: For Time Interval, 1 is for daily Booking 2 is for after a day and so on."></asp:Label>
            </div>
            <div runat="server" id="div1">
                <asp:Label ID="lbloteLiftingLimit" runat="server" Text="Note: For Lifting Limit, How many trucks can lift the material in a day for a particular customer."></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
