<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageOwnershipStatus.aspx.cs" Inherits="Masters_ManageOwnershipStatus" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManageOwnershipStatus%>"
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
                <Custom:GridViewAlwaysShow AllowPaging="true" ID="grdOwnershipStatus" runat="server"
                    AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                    Font-Size="Small" PageSize="10" Width="100%" HorizontalAlign="Center" CellPadding="5"
                    ShowFooter="True" DataKeyNames="OwnershipStatus_Id" OnPageIndexChanging="grdOwnershipStatus_PageIndexChanging"
                    OnRowCancelingEdit="grdOwnershipStatus_RowCancelingEdit" OnRowCommand="grdOwnershipStatus_RowCommand"
                    OnRowDeleting="grdOwnershipStatus_RowDeleting" OnRowEditing="grdOwnershipStatus_RowEditing"
                    OnRowUpdating="grdOwnershipStatus_RowUpdating" OnMustAddARow="grdOwnershipStatus_MustAddARow">
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, OwnershipStatus%>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtOwnershipStatus" runat="server" Text='<%# Bind("OwnershipStatus_Name") %>'
                                    MaxLength="100" />
                                <asp:RequiredFieldValidator ID="txtOwnershipStatusValidator" ControlToValidate="txtOwnershipStatus"
                                    Display="Dynamic" ValidationGroup="EditOwnershipStatus" SetFocusOnError="true"
                                    Text="*" CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredMasterOwnershipStatus%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtOwnershipStatusValidatorCallOut" runat="server"
                                    TargetControlID="txtOwnershipStatusValidator" />
                                <asp:CustomValidator ID="txtOwnershipStatusCustomValidator" runat="server" ControlToValidate="txtOwnershipStatus"
                                    Text="*" OnServerValidate="EditOwnershipStatus_ServerValidate" CssClass="failureNotification"
                                    SetFocusOnError="true" Display="Dynamic" ValidationGroup="EditOwnershipStatus"
                                    ErrorMessage="<%$ Resources:ErrorMessages, DuplicateOwnershipStaus %>" />
                                <ajax:ValidatorCalloutExtender ID="txtOwnershipStatusCustomValidatorCalloutExtender"
                                    runat="server" TargetControlID="txtOwnershipStatusCustomValidator" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewOwnershipStatus" runat="server" MaxLength="100" />
                                <asp:RequiredFieldValidator ID="txtNewOwnershipStatusValidator" ControlToValidate="txtNewOwnershipStatus"
                                    Display="Dynamic" ValidationGroup="AddOwnershipStatus" SetFocusOnError="true"
                                    Text="*" CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredMasterOwnershipStatus%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtNewOwnershipStatusValidatorCallOut"
                                    runat="server" TargetControlID="txtNewOwnershipStatusValidator" />
                                <asp:CustomValidator ID="txtNewOwnershipStatusCustomValidator" runat="server" ControlToValidate="txtNewOwnershipStatus"
                                    Text="*" OnServerValidate="AddOwnershipStatus_ServerValidate" CssClass="failureNotification"
                                    SetFocusOnError="true" Display="Dynamic" ValidationGroup="AddOwnershipStatus"
                                    ErrorMessage="<%$ Resources:ErrorMessages, DuplicateOwnershipStaus %>" />
                                <ajax:ValidatorCalloutExtender ID="txtNewOwnershipStatusCustomValidatorCalloutExtender"
                                    runat="server" TargetControlID="txtNewOwnershipStatusCustomValidator" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# Eval("OwnershipStatus_Name")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="<%$Resources:Labels, Update%>" Font-Underline="False" ValidationGroup="EditOwnershipStatus" />
                                <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="<%$Resources:Labels, Cancel%>" Font-Underline="False" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="lnkAdd" runat="server" CausesValidation="true" CommandName="AddNew"
                                    CssClass="button" Text="<%$Resources:Labels, Add%>" ValidationGroup="AddOwnershipStatus" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%#Bind("OwnershipStatus_Id")%>' />
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text="<%$Resources:Labels, Delete%>" OnClientClick="return confirm('Are you sure you want to delete this Ownership status type?');"
                                    Font-Underline="False" CommandArgument='<%#Bind("OwnershipStatus_Id")%>' />
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
