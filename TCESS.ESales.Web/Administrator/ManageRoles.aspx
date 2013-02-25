<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageRoles.aspx.cs" Inherits="Administrator_ManageRoles" ValidateRequest="false" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManageRoles%>"
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
                <Custom:GridViewAlwaysShow AllowPaging="true" ID="grdRoles" runat="server" AutoGenerateColumns="False"
                    BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                    PageSize="10" Width="100%" HorizontalAlign="Center" CellPadding="5" ShowFooter="True"
                    OnRowCommand="grdRoles_RowCommand" OnPageIndexChanging="grdRoles_PageIndexChanging"
                    OnRowDeleting="grdRoles_RowDeleting" OnMustAddARow="grdRoles_MustAddARow">
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, RoleName%>">
                            <ItemTemplate>
                                <asp:Label ID="lblRoleName" runat="server" Text='<%# Container.DataItem.ToString() %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtRoleName" runat="server" MaxLength="30" CssClass="textbox" />
                                <ajax:FilteredTextBoxExtender ID="NewRoleNameTextBoxExtender" runat="server" FilterMode="ValidChars"
                                    TargetControlID="txtRoleName" ValidChars=" " FilterType="LowercaseLetters,UppercaseLetters,Custom" />
                                <asp:RequiredFieldValidator ID="NewRoleNameValidator" runat="server" ControlToValidate="txtRoleName"
                                    CssClass="failureNotification" SetFocusOnError="true" Display="Dynamic" Text="*"
                                    ErrorMessage="<%$ Resources:ErrorMessages, RequiredRoleName %>" ValidationGroup="SaveGroup" />
                                <asp:CustomValidator ID="RoleNameCustomValidator" runat="server" ControlToValidate="txtRoleName"
                                    Text="*" OnServerValidate="RoleName_ServerValidate" CssClass="failureNotification"
                                    SetFocusOnError="true" Display="Dynamic" ValidationGroup="SaveGroup" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateRoleName %>" />
                                <ajax:ValidatorCalloutExtender ID="NewRoleNameValidatorCalloutExtender" runat="server"
                                    TargetControlID="NewRoleNameValidator" />
                                <ajax:ValidatorCalloutExtender ID="RoleNameCustomValidatorCalloutExtender" runat="server"
                                    TargetControlID="RoleNameCustomValidator" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <FooterTemplate>
                                <asp:Button ID="lnkAdd" runat="server" CausesValidation="true" CommandName="AddNew"
                                    CssClass="button" Text="<%$Resources:Labels, Add%>" Font-Underline="False" ValidationGroup="SaveGroup"
                                    Font-Bold="true" ForeColor="White" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="true" CommandName="Delete"
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
                <asp:CustomValidator ID="customValidator" Display="Dynamic" runat="server" CssClass="failureNotification" />
                <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
