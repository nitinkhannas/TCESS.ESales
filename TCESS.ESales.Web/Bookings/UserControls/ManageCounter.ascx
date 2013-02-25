<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManageCounter.ascx.cs"
    Inherits="Bookings_UserControls_ManageCounter" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<div style="overflow: auto; width: 100%;">
    <Custom:GridViewAlwaysShow AllowPaging="True" ID="grdManageCounter" runat="server"
        AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
        Font-Size="Small" PageSize="5" Width="100%" HorizontalAlign="Center" CellPadding="5"
        OnMustAddARow="grdManageCounter_MustAddARow" DataKeyNames="Counter_Id" OnRowCommand="grdManageCounter_RowCommand"
        ShowFooter="true" OnRowDeleting="grdManageCounter_RowDeleting" OnPageIndexChanging="grdManageCounter_PageIndexChanging">
        <EmptyDataTemplate>
            <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, CounterName%>">
                <ItemTemplate>
                    <asp:Label ID="lblCounterName" runat="server" Text='<%# Bind("Counter_Name") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, UserName%>">
                <ItemTemplate>
                    <asp:Label ID="lblOwnerName" runat="server" Text='<%#  GetUserName( Eval("Counter_User_Id")) %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, DCAName%>">
                <ItemTemplate>
                    <asp:Label ID="lblDriverName" runat="server" Text='<%# Bind("Counter_Agent_Name") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="EditCounter"
                        Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%#Bind("Counter_Id") %>' />
                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="<%$Resources:Labels, Delete%>" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                        Font-Underline="False" CommandArgument='<%#Bind("Counter_Id") %>' />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Button ID="lnkAddTruck" runat="server" CausesValidation="true" CommandName="AddNew"
                        Text="<%$Resources:Labels, Add%>" CssClass="button" ValidationGroup="VGAddTruck" />
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
    <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
</div>
