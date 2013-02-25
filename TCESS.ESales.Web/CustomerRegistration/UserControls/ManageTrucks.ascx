<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManageTrucks.ascx.cs"
    Inherits="CustomerRegistration_UserControls_ManageTrucks" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<div style="text-align: center;">
    <table width="100%" cellspacing="0" cellpadding="5">
        <tr align="left">
            <td>
                <asp:Label ID="lblMandatoryDoc" runat="server" Text="<%$Resources:Labels, MandatoryDocuments%>" />
            </td>
            <td>
                <asp:DropDownList ID="ddlMandatoryDoc" runat="server" DataTextField="Doc_Name" DataValueField="Doc_Id"
                    CssClass="listmenu" />

            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblAT" runat="server" Text="<%$Resources:Labels, DocumentNumber%>" />
            </td>
            <td>
                <asp:TextBox ID="txtDocNumber" runat="server" CssClass="textbox" Wrap="False" onkeypress="return runScript(event)" />
                <asp:RequiredFieldValidator ID="DocNumberValidator" ControlToValidate="txtDocNumber"
                    Display="Dynamic" ValidationGroup="ValidateGroup" SetFocusOnError="true" Text="*"
                    CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredDocNo %>"
                    runat="server" />
                <ajax:ValidatorCalloutExtender ID="DocNumberValidatorCalloutExtender" runat="server"
                    TargetControlID="DocNumberValidator" />
                <asp:Button ID="btnValidate" runat="server" UseSubmitBehavior="false" ValidationGroup="ValidateGroup"
                    Text="Validate" CssClass="button" OnClick="btnValidate_Click" />
            </td>
        </tr>
    </table>
</div>
<div class="clear">
    &nbsp;
</div>
<div style="overflow: auto; width: 100%;">
    <Custom:GridViewAlwaysShow AllowPaging="True" ID="grdManageTrucks" runat="server"
        AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
        Font-Size="Small" PageSize="12" Width="100%" HorizontalAlign="Center" CellPadding="5"
        OnMustAddARow="grdManageCustomers_MustAddARow" DataKeyNames="Truck_Id" OnRowCommand="grdManageTrucks_RowCommand"
        ShowFooter="true" OnRowDeleting="grdManageTrucks_RowDeleting">
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
                    <asp:Label ID="lblDefaultFees" runat="server" Text='<%# Bind("Truck_RegNo") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, OwnerName%>">
                <ItemTemplate>
                    <asp:Label ID="lblFirmName" runat="server" Text='<%# Bind("Truck_OwnerName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, DriverName%>">
                <ItemTemplate>
                    <asp:Label ID="lblMobileNo" runat="server" Text='<%# Bind("Truck_DriverName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="EditTruck"
                        Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%#Bind("Truck_Id") %>' />
                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="<%$Resources:Labels, Delete%>" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                        Font-Underline="False" CommandArgument='<%#Bind("Truck_Id") %>' />
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
</div>
<div>
    &nbsp;
    <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
</div>
