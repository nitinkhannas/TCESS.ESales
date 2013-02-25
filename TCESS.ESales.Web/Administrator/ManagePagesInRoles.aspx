<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManagePagesInRoles.aspx.cs" Inherits="Administrator_ManagePagesInRoles" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManagePagesInRoles%>"
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
            <div style="overflow: auto;">
                <table align="left">
                    <tr>
                        <td>
                            <asp:Label ID="lblRoleName" runat="server" Text="<%$Resources:Labels, RoleNames%>" />
                            &nbsp;
                            <asp:DropDownList ID="ddlRoleName" ValidationGroup="VGSelectRole" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlRoleName_SelectedIndexChanged" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                &nbsp;
            </div>
            <div style="overflow: auto; width: 100%;">
                <Custom:GridViewAlwaysShow ID="grdPageInRoles" runat="server" AutoGenerateColumns="False"
                    AllowPaging="true" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                    Font-Size="Small" PageSize="11" Width="100%" HorizontalAlign="Center" CellPadding="5"
                    DataKeyNames="Page_Role_Id,Page_Role_PageId,Page_Role_RoleId" OnMustAddARow="grdPageInRoles_MustAddARow"
                    OnPageIndexChanging="grdPageInRoles_PageIndexChanging" OnRowCancelingEdit="grdPageInRoles_RowCancelingEdit"
                    OnRowDeleting="grdPageInRoles_RowDeleting" OnRowEditing="grdPageInRoles_RowEditing"
                    OnRowUpdating="grdPageInRoles_RowUpdating" OnRowDataBound="grdPageInRoles_RowDataBound"
                    OnRowCommand="grdPageInRoles_RowCommand">
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, PageName%>">
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlPageName" runat="server" DataTextField="Page_Name" DataValueField="Page_Id" />
                                <asp:CustomValidator ID="PageNameCustomValidator" runat="server" ControlToValidate="ddlPageName"
                                    Text="*" OnServerValidate="PageName_ServerValidate" CssClass="failureNotification"
                                    SetFocusOnError="true" Display="Dynamic" ValidationGroup="VGAddRole" ErrorMessage="<%$ Resources:ErrorMessages, DuplicatePageName %>" />
                                <asp:RequiredFieldValidator ID="PageNameValidator" runat="server" ControlToValidate="ddlPageName"
                                    CssClass="failureNotification" InitialValue="0" SetFocusOnError="true" Display="Dynamic"
                                    Text="*" ErrorMessage="<%$ Resources:ErrorMessages, RequiredPageName %>" ValidationGroup="VGAddRole" />
                                <ajax:ValidatorCalloutExtender ID="NewRoleNameValidatorCalloutExtender" runat="server"
                                    TargetControlID="PageNameValidator" />
                                <ajax:ValidatorCalloutExtender ID="RoleNameCustomValidatorCalloutExtender" runat="server"
                                    TargetControlID="PageNameCustomValidator" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPageName" Text='<%#Bind("Page_Role_PageName") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Active%>">
                            <EditItemTemplate>
                                <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Bind("Page_Role_IsActive")%>' />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:CheckBox ID="chkNewActive" runat="server" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# Eval("Page_Role_IsActive") != null ? Convert.ToBoolean(Eval("Page_Role_IsActive")) == true ? "Yes" : "No" : ""%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                            <FooterTemplate>
                                <asp:Button ID="lnkAdd" runat="server" CausesValidation="true" CommandName="AddNew"
                                    CssClass="button" Text="<%$Resources:Labels, Add%>" ValidationGroup="VGAddRole" />
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="false" CommandName="Update"
                                    Text="<%$Resources:Labels, Update%>" Font-Underline="False" ValidationGroup="VGEditAllotedQuantity" />
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
                <asp:CustomValidator ID="customValidator" Display="Dynamic" runat="server" CssClass="failureNotification" />
                <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
