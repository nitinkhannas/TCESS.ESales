<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageDocumentType.aspx.cs" Inherits="Masters_ManageDocumentType" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManageDocumentType%>"
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
            <div style="overflow: auto;">
                <table align="left">
                    <tr>
                        <td>
                            <asp:Label ID="lblDocumentTypesFor" runat="server" Text="<%$Resources:Labels, DocumentTypesFor%>" />&nbsp;
                            <asp:DropDownList ID="ddldocGroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldocGroup_SelectedIndexChanged">
                                <asp:ListItem Text="Select Group" Value="0" />
                                <asp:ListItem Text="Customers" Value="1" />
                                <asp:ListItem Text="Trucks" Value="2" />
                                <asp:ListItem Text="Authorized Representatives" Value="3" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="txtDocTypeValidator" ControlToValidate="ddldocGroup"
                                Display="Dynamic" InitialValue="true" ValidationGroup="EditDocType" SetFocusOnError="true"
                                Text="*" CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredDocTypeName%>"
                                runat="server" />
                            <ajax:ValidatorCalloutExtender ID="txtDocTypeValidatorCallOut" runat="server"
                                TargetControlID="txtDocTypeValidator" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                &nbsp;
            </div>
            <div style="overflow: auto; width: 100%;">
                <Custom:GridViewAlwaysShow AllowPaging="true" ID="grdDocType" runat="server" AutoGenerateColumns="False"
                    BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                    PageSize="10" Width="100%" HorizontalAlign="Center" CellPadding="5" DataKeyNames="Doc_Id"
                    ShowFooter="true" OnRowCommand="grdDocType_RowCommand" OnMustAddARow="grdDocType_MustAddARow"
                    OnPageIndexChanging="grdDocType_PageIndexChanging" OnRowDeleting="grdDocType_RowDeleting"
                    OnRowEditing="grdDocType_RowEditing" OnRowUpdating="grdDocType_RowUpdating" OnRowCancelingEdit="grdDocType_RowCancelingEdit">
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, DocumentType%>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDocType" runat="server" Text='<%# Bind("Doc_Name") %>' MaxLength="100" />
                                <asp:RequiredFieldValidator ID="txtDocTypeValidator" ControlToValidate="txtDocType"
                                    Display="Dynamic" ValidationGroup="EditDocType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredDocTypeName%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtDocTypeValidatorCallOut" runat="server"
                                    TargetControlID="txtDocTypeValidator" />
                                <asp:CustomValidator ID="txtDocTypeCustomValidator" runat="server" ControlToValidate="txtDocType"
                                    Text="*" OnServerValidate="EditDocType_ServerValidate" CssClass="failureNotification"
                                    SetFocusOnError="true" Display="Dynamic" ValidationGroup="EditDocType" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateDocType %>" />
                                <ajax:ValidatorCalloutExtender ID="txtDocTypeCustomValidatorCalloutExtender"
                                    runat="server" TargetControlID="txtDocTypeCustomValidator" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewDocType" runat="server" MaxLength="100" />
                                <asp:RequiredFieldValidator ID="txtNewDocTypeValidator" ControlToValidate="txtNewDocType"
                                    Display="Dynamic" ValidationGroup="AddDocType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredDocTypeName%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtNewDocTypeValidatorCallOut" runat="server"
                                    TargetControlID="txtNewDocTypeValidator" />
                                <asp:CustomValidator ID="txtNewDocTypeCustomValidator" runat="server" ControlToValidate="txtNewDocType"
                                    Text="*" OnServerValidate="AddDocType_ServerValidate" CssClass="failureNotification"
                                    SetFocusOnError="true" Display="Dynamic" ValidationGroup="AddDocType" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateDocType %>" />
                                <ajax:ValidatorCalloutExtender ID="txtNewDocTypeCustomValidatorCalloutExtender"
                                    runat="server" TargetControlID="txtNewDocTypeCustomValidator" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# Eval("Doc_Name")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Acronym%>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDocAcronym" runat="server" Text='<%# Bind("Doc_Acronym") %>'
                                    MaxLength="10" />
                                <asp:RequiredFieldValidator ID="txtDocAcronymValidator" ControlToValidate="txtDocAcronym"
                                    Display="Dynamic" ValidationGroup="EditDocType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredDocAcronym%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtDocAcronymValidatorCallOut" runat="server"
                                    TargetControlID="txtDocAcronymValidator" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewDocAcronym" runat="server" MaxLength="10" />
                                <asp:RequiredFieldValidator ID="txtNewDocAcronymValidator" ControlToValidate="txtNewDocAcronym"
                                    Display="Dynamic" ValidationGroup="AddDocType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredDocAcronym%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtNewDocAcronymValidatorCallOut" runat="server"
                                    TargetControlID="txtNewDocAcronymValidator" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# Eval("Doc_Acronym")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, IsMandatory%>">
                            <EditItemTemplate>
                                <asp:CheckBox ID="chkMandatory" runat="server" Checked='<%# Bind("Doc_Mandatory")%>' />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:CheckBox ID="chkNewMandatory" runat="server" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# Eval("Doc_Mandatory") != null ? Convert.ToBoolean(Eval("Doc_Mandatory")) == true ? "Yes" : "No" : ""%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, IsUnique%>">
                            <EditItemTemplate>
                                <asp:CheckBox ID="chkUnique" runat="server" Checked='<%# Bind("Doc_IsUnique")%>' />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:CheckBox ID="chkNewUnique" runat="server" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# Eval("Doc_IsUnique") != null ? Convert.ToBoolean(Eval("Doc_IsUnique")) == true ? "Yes" : "No" : ""%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="<%$Resources:Labels, Update%>" Font-Underline="False" ValidationGroup="EditDocType" />
                                <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="<%$Resources:Labels, Cancel%>" Font-Underline="False" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="lnkAdd" runat="server" CausesValidation="true" CommandName="AddNew"
                                    CssClass="button" Text="<%$Resources:Labels, Add%>" ValidationGroup="AddDocType" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%#Bind("Doc_Id")%>' />
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text="<%$Resources:Labels, Delete%>" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                                    Font-Underline="False" CommandArgument='<%#Bind("Doc_Id")%>' />
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
