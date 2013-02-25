<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SystemAlerts.aspx.cs" Inherits="CustomerDevelopment_SystemAlerts" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolKit" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PageNameContent" runat="Server">
    <h2>
        <label>
            Customer Visit Alerts
        </label>
    </h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <div style="z-index: 105; position: relative; top: 30%; text-align: center;">
                <img src='<%= ResolveClientUrl("~/Images/ajax-loader.gif")%>' style="vertical-align: middle"
                    alt="Processing" />Processing ...
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="overflow: auto; width: 100%;">
                <table width="100%">
                    <tr>
                        <td>
                            <h4>
                                <label>
                                    Executive List</label></h4>
                        </td>
                        <td>
                            <h4>
                                <label>
                                    Customer Visit Details For</label>
                                <asp:Label ID="lblConcessionName" runat="server" Text="AME1" />
                            </h4>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            <Custom:GridViewAlwaysShow ID="grdConcessionName" runat="server" AutoGenerateColumns="False"
                                BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                                AllowPaging="true" PageSize="12" Width="100%" HorizontalAlign="Center" CellPadding="5"
                                ShowFooter="True">
                                <EmptyDataTemplate>
                                    No Record Found.
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Executive Name">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkConcessionName" runat="server" Text='<%# Bind("AmeName") %>'
                                                CommandName="ConcessionName" Font-Underline="False" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                <RowStyle BackColor="White" ForeColor="#003399" Font-Size="Small" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                <SortedDescendingHeaderStyle BackColor="#002876" />
                            </Custom:GridViewAlwaysShow>
                        </td>
                        <td>
                            <Custom:GridViewAlwaysShow ID="grdFeeDetails" runat="server" AutoGenerateColumns="False"
                                AllowPaging="true" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                                Font-Size="Small" PageSize="8" Width="100%" HorizontalAlign="Center" CellPadding="5"
                                ShowFooter="True">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblNoRecordsFound" runat="server" Text="No records found" />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFeeName" Text='<%# Bind("CustomerName") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Visit Due Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFeeName" Text='<%# Bind("SystemDate") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="SuspendAlert"
                                                Text="Suspend" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                                                Font-Underline="False" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                <RowStyle BackColor="White" ForeColor="#003399" Font-Size="Small" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                <SortedDescendingHeaderStyle BackColor="#002876" />
                            </Custom:GridViewAlwaysShow>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:ValidationSummary ID="EditSummary" runat="server" CssClass="failureNotification"
                    ValidationGroup="VGEdit" ShowMessageBox="true" ShowSummary="false" />
                <asp:ValidationSummary ID="AddSummary" runat="server" CssClass="failureNotification"
                    ValidationGroup="VGAdd" ShowMessageBox="true" ShowSummary="false" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>