<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageBusinessType.aspx.cs" Inherits="Masters_ManageBusinessType" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManageBusinessType%>"
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
                <Custom:GridViewAlwaysShow AllowPaging="true" ID="grdBusinessType" runat="server"
                    AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                    Font-Size="Small" PageSize="10" Width="100%" HorizontalAlign="Center" CellPadding="5"
                    ShowFooter="True" DataKeyNames="BusinessType_Id" OnRowCommand="grdBusinessType_RowCommand"
                    OnRowDeleting="grdBusinessType_RowDeleting" OnRowEditing="grdBusinessType_RowEditing"
                    OnRowUpdating="grdBusinessType_RowUpdating" OnPageIndexChanging="grdBusinessType_PageIndexChanging"
                    OnMustAddARow="grdBusinessType__MustAddARow" OnRowCancelingEdit="grdBusinessType_RowCancelingEdit">
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, BusinessType%>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBusinessType" runat="server" Text='<%# Bind("BusinessType_Name") %>'
                                    MaxLength="70" />
                                <asp:RequiredFieldValidator ID="txtBusinessTypeValidator" ControlToValidate="txtBusinessType"
                                    Display="Dynamic" ValidationGroup="EditBusinessType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredBusinessType%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtBusinessTypeValidatorCallOut" runat="server"
                                    TargetControlID="txtBusinessTypeValidator" />
                                <asp:CustomValidator ID="txtBusinessTypeCustomValidator" runat="server" ControlToValidate="txtBusinessType"
                                    Text="*" OnServerValidate="EditBusinessType_ServerValidate" CssClass="failureNotification"
                                    SetFocusOnError="true" Display="Dynamic" ValidationGroup="EditBusinessType" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateBusinessType %>" />
                                <ajax:ValidatorCalloutExtender ID="txtBusinessTypeCustomValidatorCalloutExtender"
                                    runat="server" TargetControlID="txtBusinessTypeCustomValidator" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewBusinessType" runat="server" MaxLength="70" />
                                <asp:RequiredFieldValidator ID="txtNewBusinessTypeValidator" ControlToValidate="txtNewBusinessType"
                                    Display="Dynamic" ValidationGroup="AddBusinessType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredBusinessType%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtNewBusinessTypeValidatorCallOut" runat="server"
                                    TargetControlID="txtNewBusinessTypeValidator" />
                                <asp:CustomValidator ID="txtNewBusinessTypeCustomValidator" runat="server" ControlToValidate="txtNewBusinessType"
                                    Text="*" OnServerValidate="AddBusinessType_ServerValidate" CssClass="failureNotification"
                                    SetFocusOnError="true" Display="Dynamic" ValidationGroup="AddBusinessType" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateBusinessType %>" />
                                <ajax:ValidatorCalloutExtender ID="txtNewBusinessTypeCustomValidatorCalloutExtender"
                                    runat="server" TargetControlID="txtNewBusinessTypeCustomValidator" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# Eval("BusinessType_Name")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="<%$Resources:Labels, Update%>" Font-Underline="False" ValidationGroup="EditBusinessType" />
                                <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="<%$Resources:Labels, Cancel%>" Font-Underline="False" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="lnkAdd" runat="server" CausesValidation="true" CommandName="AddNew"
                                    CssClass="button" Text="<%$Resources:Labels, Add%>" ValidationGroup="AddBusinessType" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%#Bind("BusinessType_Id")%>' />
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text="<%$Resources:Labels, Delete%>" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                                    Font-Underline="False" CommandArgument='<%#Bind("BusinessType_Id")%>' />
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
