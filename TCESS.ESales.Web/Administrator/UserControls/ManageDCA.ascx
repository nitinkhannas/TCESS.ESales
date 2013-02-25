<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManageDCA.ascx.cs" Inherits="Administrator_UserControls_ManageDCA" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<table width="100%">
    <tr>
        <td align="center">
            <Custom:GridViewAlwaysShow ID="grdDCA" runat="server" AutoGenerateColumns="False"
                DataKeyNames="Agent_Id" AllowPaging="true" PageSize="12" BorderColor="#397dbc"
                BorderStyle="Solid" BorderWidth="1px" Font-Size="Small" Width="100%" HorizontalAlign="Center"
                CellPadding="5" OnRowDeleting="grdDCA_RowDeleting" OnMustAddARow="grdDCA_MustAddARow"
                OnPageIndexChanging="grdDCA_PageIndexChanging" OnRowCommand="grdDCA_RowCommand"
                ShowFooter="true">
                <EmptyDataTemplate>
                    <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, ShortName%>">
                        <ItemTemplate>
                            <%# Eval("Agent_ShortName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, DCAName%>">
                        <ItemTemplate>
                            <%# Eval("Agent_Name") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, TSLCode%>">
                        <ItemTemplate>
                            <%# Eval("Agent_TSLCode")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, DateOfJoining%>">
                        <ItemTemplate>
                            <%# Convert.ToDateTime(Eval("Agent_StartDate")).ToString("dd MMM yyyy") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="EditDCA"
                                Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%#Bind("Agent_Id") %>' />
                            <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="true" CommandName="Delete"
                                Text="<%$Resources:Labels, Retire%>" Font-Underline="False" ValidationGroup="VGDelete"
                                OnClientClick='<%# Eval("Agent_Name", "return confirm(\"Are you sure you want to permanent retire {0} ?\")") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="lnkAdd" runat="server" CommandName="AddNew" Text="<%$Resources:Labels, Add%>"
                                CssClass="button" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#397dbc" ForeColor="#FFFFFF" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#397dbc" Font-Bold="True" ForeColor="#FFFFFF" Height="20px" />
                <PagerStyle BackColor="#397dbc" ForeColor="#FFFFFF" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Font-Size="Small" Height="20px" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </Custom:GridViewAlwaysShow>
        </td>
    </tr>
</table>
<div>
    <asp:CustomValidator ID="customValidator" Display="Dynamic" runat="server" CssClass="failureNotification" />
    <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
</div>
