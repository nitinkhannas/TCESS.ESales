<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManageInactiveTrucks.ascx.cs"
    Inherits="Supervisor_UserControls_ManageInactiveTrucks" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<div style="text-align: left;">
    <asp:Label ID="lblTruckNumber" runat="server" Text="<%$Resources:Labels, TruckNo%>" />
    <asp:TextBox ID="txtTruckNumber" runat="server" CssClass="textbox" Wrap="False" MaxLength="15"
        onkeypress="return runScript(event)" />
    <asp:RequiredFieldValidator ID="TruckNumberValidator" ControlToValidate="txtTruckNumber"
        Display="Dynamic" ValidationGroup="ValidateGroup" SetFocusOnError="true" Text="*"
        CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredTruckRegNo %>"
        runat="server" />
    <ajax:ValidatorCalloutExtender ID="TruckNumberValidatorCallout" runat="server" TargetControlID="TruckNumberValidator" />
    <asp:Button ID="btnValidate" runat="server" ValidationGroup="ValidateGroup" Text="Validate"
        OnClick="btnValidate_Click" CssClass="button" />
</div>
<div class="clear">
    &nbsp;
</div>
<div style="overflow: auto; width: 100%;">
    <Custom:GridViewAlwaysShow AllowPaging="True" ID="grdManageInactiveTrucks" runat="server"
        AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
        Font-Size="Small" PageSize="10" Width="100%" HorizontalAlign="Center" CellPadding="5"
        ShowFooter="true" OnPageIndexChanging="grdManageInactiveTrucks_PageIndexChanging"
        DataKeyNames="type" OnRowCommand="grdManageInactiveTrucks_RowCommand" OnRowDeleting="grdManageInactiveTrucks_RowDeleting">
        <EmptyDataTemplate>
            <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, TruckNo%>">
                <ItemTemplate>
                    <%# Eval("Truck_RegNo") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, OwnerName%>">
                <ItemTemplate>
                    <%# Eval("Truck_OwnerName")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, DriverName%>">
                <ItemTemplate>
                    <%# Eval("Truck_DriverName")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkSuspend" runat="server" CausesValidation="False" CommandName="Activate"
                        Text="<%$Resources:Labels, Activate%>" OnClientClick="return confirm('Are you sure you want to activate this item?');"
                        Font-Underline="False" CommandArgument='<%#Eval("type") + ";" +Eval("Truck_RegNo")%>' />
                </ItemTemplate>
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
    <div>
        <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
    </div>
</div>
