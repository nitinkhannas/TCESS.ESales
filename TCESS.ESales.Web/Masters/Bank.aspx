<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Bank.aspx.cs" Inherits="Masters_Bank" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="Bank" CssClass="pageNameContent" />
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
                <Custom:GridViewAlwaysShow AllowPaging="true" ID="grdBank" runat="server" AutoGenerateColumns="False"
                    BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                    PageSize="10" Width="100%" HorizontalAlign="Center" CellPadding="5" ShowFooter="True"
                    DataKeyNames="Bank_ID" OnMustAddARow="grdBank_MustAddARow" OnPageIndexChanging="grdBank_PageIndexChanging"
                    OnRowCancelingEdit="grdBank_RowCancelingEdit" OnRowCommand="grdBank_RowCommand"
                    OnRowDeleting="grdBank_RowDeleting" OnRowEditing="grdBank_RowEditing" OnRowUpdating="grdBank_RowUpdating">
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bank Name">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBankName" runat="server" Text='<%# Bind("Bank_Name") %>' MaxLength="70" />
                                <asp:RequiredFieldValidator ID="txtBankNameValidator" ControlToValidate="txtBankName"
                                    Display="Dynamic" ValidationGroup="EditBank" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="Required Bank Name%>" runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtBankNameValidatorCallOut" runat="server" TargetControlID="txtBankNameValidator" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewBankName" runat="server" MaxLength="70" />
                                <asp:RequiredFieldValidator ID="txtNewBankNameValidator" ControlToValidate="txtNewBankName"
                                    Display="Dynamic" ValidationGroup="AddBank" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                                    ErrorMessage="Required Bank Name%>" runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtNewBankNameValidatorCallOut" runat="server"
                                    TargetControlID="txtNewBankNameValidator" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# Eval("Bank_Name")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bank Account No">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBankAccountNo" runat="server" Text='<%# Bind("Bank_AccountNo") %>'
                                    MaxLength="70" />
                                <asp:RequiredFieldValidator ID="txtBankAccountNoValidator" ControlToValidate="txtBankAccountNo"
                                    Display="Dynamic" ValidationGroup="EditBank" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="Required Bank AccountNo>" runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtBankAccountNoValidatorCallOut" runat="server"
                                    TargetControlID="txtBankAccountNoValidator" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewBankAccountNo" runat="server" MaxLength="70" />
                                <asp:RequiredFieldValidator ID="txtNewBankAccountNoValidator" ControlToValidate="txtNewBankAccountNo"
                                    Display="Dynamic" ValidationGroup="AddBank" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                                    ErrorMessage="Required Bank AccountNo>" runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtNewBankAccountNoValidatorCallOut" runat="server"
                                    TargetControlID="txtNewBankAccountNoValidator" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# Eval("Bank_AccountNo")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="<%$Resources:Labels, Update%>" Font-Underline="False" ValidationGroup="EditBank" />
                                <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="<%$Resources:Labels, Cancel%>" Font-Underline="False" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="lnkAdd" runat="server" CausesValidation="true" CommandName="AddNew"
                                    CssClass="button" Text="<%$Resources:Labels, Add%>" ValidationGroup="AddBank" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%#Bind("Bank_ID")%>' />
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text="<%$Resources:Labels, Delete%>" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                                    Font-Underline="False" CommandArgument='<%#Bind("Bank_ID")%>' />
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
