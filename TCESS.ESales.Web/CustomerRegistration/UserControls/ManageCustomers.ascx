<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManageCustomers.ascx.cs"
    Inherits="CustomerRegistration_UserControls_ManageCustomers" %>
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
                <asp:Label ID="lblDocumentNo" runat="server" Text="<%$Resources:Labels, DocumentNumber%>" />
            </td>
            <td>
                <asp:TextBox ID="txtDocNumber" runat="server" CssClass="textbox" Wrap="False" MaxLength="15"
                    onkeypress="return runScript(event)" />
                <asp:RequiredFieldValidator ID="DocNumberValidator" ControlToValidate="txtDocNumber"
                    Display="Dynamic" ValidationGroup="ValidateGroup" SetFocusOnError="true" Text="*"
                    CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredDocNo %>"
                    runat="server" />
                <ajax:ValidatorCalloutExtender ID="DocNumberValidatorCalloutExtender" runat="server"
                    TargetControlID="DocNumberValidator" />
                <asp:Button ID="btnValidate" runat="server" ValidationGroup="ValidateGroup" Text="Validate"
                    OnClick="btnValidate_Click" CssClass="button" />
            </td>
        </tr>
    </table>
</div>
<div class="clear">
    &nbsp;
</div>
<div style="overflow: auto; width: 100%;">
    <Custom:GridViewAlwaysShow AllowPaging="True" ID="grdManageCustomers" runat="server"
        AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
        Font-Size="Small" PageSize="12" Width="100%" HorizontalAlign="Center" CellPadding="5"
        OnMustAddARow="grdManageCustomers_MustAddARow" DataKeyNames="Cust_ID" OnRowDeleting="grdManageCustomers_RowDeleting"
        OnRowCommand="grdManageCustomers_RowCommand">
        <EmptyDataTemplate>
            <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, TradeName%>">
                <ItemTemplate>
                    <asp:Label ID="lblDefaultFees" runat="server" Text='<%# Bind("Cust_TradeName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, FirmName%>">
                <ItemTemplate>
                    <asp:Label ID="lblFirmName" runat="server" Text='<%# Bind("Cust_FirmName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, OwnerName%>">
                <ItemTemplate>
                    <asp:Label ID="lblOwnerName" runat="server" Text='<%# Bind("Cust_OwnerName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, BusinessType%>">
                <ItemTemplate>
                    <asp:Label ID="lblBusinessType" runat="server" Text='<%# Bind("Cust_Business_Name") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, MobileNo%>">
                <ItemTemplate>
                    <asp:Label ID="lblMobileNo" runat="server" Text='<%# Bind("Cust_MobileNo") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEditDocument" runat="server" CausesValidation="False" CommandName="EditDocument"
                        Text="<%$Resources:Labels, EditDocument%>" Font-Underline="false" CommandArgument='<%#Bind("Cust_Id") %>' />
                    <asp:LinkButton ID="lnkEditCustomer" runat="server" CausesValidation="False" CommandName="EditCustomer"
                        Text="<%$Resources:Labels, EditCustomer%>" Font-Underline="false" CommandArgument='<%#Bind("Cust_Id") %>' />
                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="<%$Resources:Labels, Delete%>" OnClientClick='<%# Eval("Cust_FirmName", "return confirm(\"Are you sure you want to delete {0} ?\")") %>'
                        Font-Underline="false" CommandArgument='<%#Bind("Cust_Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
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
