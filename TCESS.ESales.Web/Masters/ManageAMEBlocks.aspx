<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageAMEBlocks.aspx.cs" Inherits="Masters_ManageAMEBlocks" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManageAMEBlocks%>"
        CssClass="pageNameContent" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="Server">
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
                <Custom:GridViewAlwaysShow AllowPaging="true" ID="grdBlock" runat="server" AutoGenerateColumns="False"
                    BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                    PageSize="10" Width="100%" HorizontalAlign="Center" CellPadding="5" ShowFooter="True"
                    DataKeyNames="Blocks_Id" OnPageIndexChanging="grdBlock_PageIndexChanging" OnRowCommand="grdBlock_RowCommand"
                    OnRowDeleting="grdBlock_RowDeleting" OnRowEditing="grdBlock_RowEditing" OnRowUpdating="grdBlock_RowUpdating"
                    OnRowCancelingEdit="grdBlock_RowCancelingEdit" OnMustAddARow="grdBlocke__MustAddARow">
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, BlockName%>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBlock" runat="server" Text='<%# Bind("Blocks_Name") %>' MaxLength="45" />
                                <asp:RequiredFieldValidator ID="txtBlocksValidator" ControlToValidate="txtBlock"
                                    Display="Dynamic" ValidationGroup="EditBlock" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredBlock%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtBlocksValidatorCallOut" runat="server" TargetControlID="txtBlocksValidator" />
                                <asp:CustomValidator ID="txtBlockCustomValidator" runat="server" ControlToValidate="txtBlock"
                                    Text="*" OnServerValidate="EditAMEBlock_ServerValidate" CssClass="failureNotification"
                                    SetFocusOnError="true" Display="Dynamic" ValidationGroup="EditBlock" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateAMEBlock %>" />
                                <ajax:ValidatorCalloutExtender ID="txtBlockCustomValidatorCalloutExtender" runat="server"
                                    TargetControlID="txtBlockCustomValidator" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewBlock" runat="server" MaxLength="45" />
                                <asp:RequiredFieldValidator ID="txtNewBlocksValidator" ControlToValidate="txtNewBlock"
                                    Display="Dynamic" ValidationGroup="AddBlock" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredBlock%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtNewBlocksValidatorCallOut" runat="server" TargetControlID="txtNewBlocksValidator" />
                                <asp:CustomValidator ID="txtNewBlockCustomValidator" runat="server" ControlToValidate="txtNewBlock"
                                    Text="*" OnServerValidate="AddAMEBlock_ServerValidate" CssClass="failureNotification"
                                    SetFocusOnError="true" Display="Dynamic" ValidationGroup="AddBlock" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateAMEBlock %>" />
                                <ajax:ValidatorCalloutExtender ID="txtNewBlockCustomValidatorCalloutExtender" runat="server"
                                    TargetControlID="txtNewBlockCustomValidator" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# Eval("Blocks_Name")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="<%$Resources:Labels, Update%>" Font-Underline="False" ValidationGroup="EditBlock" />
                                <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="<%$Resources:Labels, Cancel%>" Font-Underline="False" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="lnkAdd" runat="server" CausesValidation="true" CommandName="AddNew"
                                    CssClass="button" Text="<%$Resources:Labels, Add%>" ValidationGroup="AddBlock" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%#Bind("Blocks_Id")%>' />
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text="<%$Resources:Labels, Delete%>" OnClientClick="return confirm('Are you sure you want to delete this block?');"
                                    Font-Underline="False" CommandArgument='<%#Bind("Blocks_Id")%>' />
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
