<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ActivateCustomers.ascx.cs"
    Inherits="CustomerRegistration_UserControls_ActivateCustomers" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<div style="text-align: center;">
    <table width="100%" cellspacing="0" cellpadding="5">
        <tr>
            <td>
                <Custom:GridViewAlwaysShow ID="grdActivateCustomers" runat="server" AutoGenerateColumns="False"
                    BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                    Width="100%" HorizontalAlign="Center" CellPadding="5" DataKeyNames="Cust_ID,Cust_AgentId"
                    OnRowCommand="grdActivateCustomers_RowCommand" OnRowCancelingEdit="grdActivateCustomers_RowCancelingEdit"
                    OnRowEditing="grdActivateCustomers_RowEditing" OnRowUpdating="grdActivateCustomers_RowUpdating"
                    OnRowDataBound="grdActivateCustomers_RowDataBound">
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, FirmName%>">
                            <ItemTemplate>
                                <asp:Label ID="lblFirmName" runat="server" Text='<%# Bind("Cust_FirmName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, UnitAddress%>">
                            <ItemTemplate>
                                <asp:Label ID="lblOwnerName" runat="server" Text='<%# Bind("Cust_UnitAddress") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, District%>">
                            <ItemTemplate>
                                <asp:Label ID="lblDistrict" runat="server" Text='<%# Bind("Cust_District_Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TIN No">
                            <ItemTemplate>
                                <asp:Label ID="lblTINNo" runat="server" Text='<%# Bind("Cust_TinNo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, AnnualRequirement%>">
                            <ItemTemplate>
                                <asp:Label ID="lblAnnualRequirement" runat="server" Text='<%# Bind("Cust_Mat_AnnualRequirement") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, CustomerCode%>" ItemStyle-Width="120px">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCustomerCode" Width="100px" runat="server" MaxLength="7" />
                                <asp:RequiredFieldValidator ID="CustomerCodeValidator" ControlToValidate="txtCustomerCode"
                                    Display="Dynamic" ValidationGroup="EditCustomer" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredCustomerCode%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="CustomerCodeValidatorCallout" runat="server" TargetControlID="CustomerCodeValidator" />
                            </EditItemTemplate>

<ItemStyle Width="120px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, AllottedQuantity%>">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlAllotedQty" runat="server" DataTextField="Alloted_Quantity"
                                    DataValueField="Alloted_Id" />
                                <asp:RequiredFieldValidator ID="AllotedQtyValidator" ControlToValidate="ddlAllotedQty"
                                    Display="Dynamic" ValidationGroup="EditCustomer" SetFocusOnError="true" Text="*"
                                    InitialValue="0" CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, SelectAllotedQuantity%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="AllotedQtyValidatorCallout" runat="server" TargetControlID="AllotedQtyValidator" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="<%$Resources:Labels, Activate%>" Font-Underline="False" ValidationGroup="EditCustomer" />
                                <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="<%$Resources:Labels, Cancel%>" Font-Underline="False" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkView" runat="server" CausesValidation="False" CommandName="View"
                                    Text="<%$Resources:Labels, View%>" Font-Underline="False" 
									CommandArgument='<%# Bind("Cust_ID") %>' Visible="False" />
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%# Bind("Cust_ID") %>' />
                                <asp:LinkButton ID="lnkPrint" runat="server" CommandName="PrintCustomer" CommandArgument='<%# Bind("Cust_ID") %>'
                                    Text="<%$Resources:Labels, Print%>" Font-Underline="False" />
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
            </td>
        </tr>
        <tr>
            <td>
                <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
                <uc1:MessageBox ID="ucMessageBox" runat="server" />
            </td>
        </tr>
    </table>
</div>
