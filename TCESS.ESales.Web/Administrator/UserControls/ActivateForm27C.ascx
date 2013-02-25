<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ActivateForm27C.ascx.cs"
    Inherits="Administrator_UserControls_ActivateForm27C" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<div style="text-align: center;">
    <table width="100%" cellspacing="0" cellpadding="5">
        <tr>
            <td>
                <Custom:GridViewAlwaysShow ID="grdActivateForm27C" runat="server" AutoGenerateColumns="False"
                    BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                    DataKeyNames="Form27C_Id" Width="100%" HorizontalAlign="Center" CellPadding="5"
                    OnRowCommand="grdActivateForm27C_RowCommand" OnRowCancelingEdit="grdActivateForm27C_RowCancelingEdit"
                    OnRowEditing="grdActivateForm27C_RowEditing" OnRowUpdating="grdActivateForm27C_RowUpdating"
                    OnRowDataBound="grdActivateForm27C_RowDataBound">
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cust Code">
                            <ItemTemplate>
                                <asp:Label ID="lblCustCode" runat="server" Text='<%# Bind("Cust_Code") %>' />
                                <asp:HiddenField ID="hdnPerioType" runat="server" Value='<%# Bind("PeriodType") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trade Name">
                            <ItemTemplate>
                                <asp:Label ID="lblCust_UnitName" runat="server" Text='<%# Bind("Cust_UnitName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Form 27C Received date">
                            <ItemTemplate>
                                <asp:Label ID="lblOwnerName" runat="server" Text='<%#Convert.ToDateTime(Eval("ReceivedDate")).ToString("dd-MM-yyyy")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Month for which valid">
                            <ItemTemplate>
                                <asp:Label ID="lblValidMonth" runat="server" Text='<%# Bind("ValidMonth") %>' />
                                <asp:HiddenField ID="hdnCurrentMonth" runat="server" Value='<%# Bind("CurrentMonth") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TSL Received Date" ItemStyle-Width="120px">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtReceivedDate" runat="server" CssClass="textbox" />
                                <ajax:CalendarExtender ID="AMEVisitDateCalendarExtender" Format="dd-MMM-yyyy" 
                                    runat="server" TargetControlID="txtReceivedDate" />
                                <ajax:TextBoxWatermarkExtender ID="txtReceivedDate_TextBoxWatermarkExtender" runat="server"
                                    Enabled="True" TargetControlID="txtReceivedDate" WatermarkCssClass="watermark"
                                    WatermarkText="Click to select date">
                                </ajax:TextBoxWatermarkExtender>
                                <asp:RequiredFieldValidator ID="AMEVisitDateValidator" ControlToValidate="txtReceivedDate"
                                    Display="Dynamic" ValidationGroup="EditCustomer" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="Enter TSL Received Date" runat="server" />
                                <ajax:ValidatorCalloutExtender ID="AMEVisitDateValidatorCalloutExtender" runat="server"
                                    TargetControlID="AMEVisitDateValidator" />
                            </EditItemTemplate>
                            <ItemStyle Width="120px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Period">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlMonths" runat="server" DataTextField="MonthName" DataValueField="Months_Id" />
                                <asp:RequiredFieldValidator ID="ddlMonthsValidator" ControlToValidate="ddlMonths"
                                    Display="Dynamic" ValidationGroup="EditCustomer" SetFocusOnError="true" Text="*"
                                    InitialValue="0" CssClass="failureNotification" ErrorMessage="Select Month" runat="server" />
                                <ajax:ValidatorCalloutExtender ID="ddlMonthsValidatorCallout" runat="server" TargetControlID="ddlMonthsValidator" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="<%$Resources:Labels, Activate%>" CommandArgument='<%# Bind("Form27C_Id") %>'
                                    Font-Underline="False" ValidationGroup="EditCustomer" />
                                <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="<%$Resources:Labels, Cancel%>" Font-Underline="False" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkView" runat="server" CausesValidation="False" CommandName="View"
                                    Text="<%$Resources:Labels, View%>" Font-Underline="False" CommandArgument='<%# Bind("Cust_ID") %>'
                                    Visible="False" />
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%# Bind("Cust_ID") %>' />
                                <%--<asp:LinkButton ID="lnkPrint" runat="server" CommandName="PrintCustomer" CommandArgument='<%# Bind("Cust_ID") %>'
                                    Text="<%$Resources:Labels, Print%>" Font-Underline="False" />--%>
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
