<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageUsers.aspx.cs" Inherits="Administrator_ManageUsers" ValidateRequest="false" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManageUsers%>"
        CssClass="pageNameContent" />
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
            <div style="overflow: auto; width: 100%;">
                <Custom:GridViewAlwaysShow AllowPaging="true" ID="grdManageUsers" runat="server"
                    AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                    Font-Size="Small" PageSize="13" Width="100%" HorizontalAlign="Center" CellPadding="5"
                    OnPageIndexChanging="grdManageUsers_PageIndexChanging" OnRowDeleting="grdManageUsers_RowDeleting"
                    OnMustAddARow="grdManageUsers_MustAddARow" OnRowCancelingEdit="grdManageUsers_RowCancelingEdit"
                    OnRowDataBound="grdManageUsers_RowDataBound" OnRowEditing="grdManageUsers_RowEditing"
                    OnRowUpdating="grdManageUsers_RowUpdating" DataKeyNames="UAM_Id, UAM_Agent_Id, UAM_User_Id, UPM_PaymentModeId">
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, UserName%>">
                            <ItemTemplate>
                                <asp:Label ID="lblUserName" runat="server" Text='<%# FindUserNameByUserId(Eval("UAM_User_Id")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, RoleName%>">
                            <ItemTemplate>
                                <asp:Label ID="lblRoleName" runat="server" Text='<%# FindRoleNameByUserId(Eval("UAM_User_Id")) %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlRoleName" runat="server" />
                                <asp:RequiredFieldValidator ID="RoleNameValidator" ControlToValidate="ddlRoleName"
                                    InitialValue="0" Display="Dynamic" ValidationGroup="VGEdit" SetFocusOnError="true"
                                    Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, SelectRoleName %>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="RoleNameValidatorCalloutExtender" runat="server"
                                    TargetControlID="RoleNameValidator" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, AgentName%>">
                            <ItemTemplate>
                                <asp:Label ID="lblAgentName" runat="server" Text='<%#Bind("UAM_Agent_Name") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlAgentName" runat="server" DataTextField="Agent_Name" DataValueField="Agent_Id"
                                    CssClass="listmenu" />
                                <asp:RequiredFieldValidator ID="AgentNameValidator" ControlToValidate="ddlAgentName"
                                    InitialValue="0" Display="Dynamic" ValidationGroup="VGEdit" SetFocusOnError="true"
                                    Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredAMEBlock %>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="AgentNameValidatorCalloutExtender" runat=    "server"
                                    TargetControlID="AgentNameValidator" />
                            </EditItemTemplate>
                        </asp:TemplateField>                        
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Password%>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNewPassword" MaxLength="15" runat="server" TextMode="Password" />
                                <asp:RegularExpressionValidator ID="PasswordRegularExpressionValidator" ControlToValidate="txtNewPassword"
                                    runat="server" CssClass="failureNotification" SetFocusOnError="true" ErrorMessage="<%$ Resources:ErrorMessages, IncorrectFormat %>"
                                    ValidationExpression="^.*(?=.{7,15})(?=.*[@#$%^&+=]).*$" ValidationGroup="VGEdit"
                                    Text="*" />
                                <ajax:ValidatorCalloutExtender ID="PasswordRegularExpressionCalloutExtender" runat="server"
                                    TargetControlID="PasswordRegularExpressionValidator" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPassword" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, ConfirmPassword%>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtConfirmPassword" MaxLength="15" runat="server" TextMode="Password" />
                                <asp:CompareValidator ID="CompareValidator" runat="server" CssClass="failureNotification"
                                    ControlToValidate="txtConfirmPassword" ControlToCompare="txtNewPassword" ValidationGroup="VGEdit"
                                    Text="*" ErrorMessage="<%$ Resources:ErrorMessages, InvalidConfirmPassword %>" />
                                <ajax:ValidatorCalloutExtender ID="CompareValidatorCalloutExtender" runat="server"
                                    TargetControlID="CompareValidator" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblConfirmPassword" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="<%$Resources:Labels, Update%>" Font-Underline="False" ValidationGroup="VGEdit" />
                                <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="<%$Resources:Labels, Cancel%>" Font-Underline="False" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="<%$Resources:Labels, Edit%>" Font-Underline="False" />
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                    Text="<%$Resources:Labels, Delete%>" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                                    Font-Underline="False" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
