<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AMEOfficeCustomerAssociation.aspx.cs" Inherits="Administrator_AMEOfficeCustomerAssociation" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, CustomerAMEBlockAssociation%>"
        CssClass="pageNameContent" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="progressBar" runat="server" AssociatedUpdatePanelID="uplMainPanel"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="overlay">
                <div class="ajaxloader">
                    <img src='<%= ResolveClientUrl("~/Images/ajax-loader.gif")%>' style="vertical-align: middle"
                        alt="Processing" />Processing....
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="uplMainPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" cellspacing="5" cellpadding="5" class="formtext">
                <tr>
                    <td align="left">
                        <asp:Label ID="lblAMEOfficesList" runat="server" Text="<%$Resources:Labels, AMEOfficesList%>" />
                    </td>
                    <td align="left">
                        <asp:Label ID="lblCustomerDetails" runat="server" Text="<%$Resources:Labels, CustomerDetailsFor%>" />
                        <asp:Label ID="lblAMEBlocks" runat="server" />
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <Custom:GridViewAlwaysShow ID="grdAMEBlocks" DataKeyNames="Blocks_Id" runat="server"
                            AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                            Font-Size="Small" AllowPaging="true" PageSize="10" Width="100%" HorizontalAlign="Center"
                            CellPadding="5" OnMustAddARow="grdAMEBlocks_MustAddARow" OnRowCommand="grdAMEBlocks_RowCommand"
                            OnPageIndexChanging="grdAMEBlocks_PageIndexChanging">
                            <EmptyDataTemplate>
                                <asp:Label ID="lblNoRecordFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, AMEOffice%>">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkBlockName" runat="server" CommandArgument='<%#Bind("Blocks_Id") %>'
                                            Text='<%# Bind("Blocks_Name") %>' CommandName="ShowCustomer" Font-Underline="False" />
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
                    <td>
                        <Custom:GridViewAlwaysShow ID="grdCustomerDetails" runat="server" AutoGenerateColumns="False"
                            AllowPaging="true" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                            DataKeyNames="Cust_Id" Font-Size="Small" PageSize="10" Width="100%" HorizontalAlign="Center"
                            CellPadding="5" OnRowDataBound="grdCustomerDetails_RowDataBound" OnRowEditing="grdCustomerDetails_RowEditing"
                            OnRowUpdating="grdCustomerDetails_RowUpdating" OnPageIndexChanging="grdCustomerDetails_PageIndexChanging"
                            OnRowCancelingEdit="grdCustomerDetails_RowCancelingEdit" OnMustAddARow="grdCustomerDetails_MustAddARow">
                            <EmptyDataTemplate>
                                <asp:Label ID="lblNoCustomerRecordFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, CustomerName%>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFirmName" Text='<%# Bind("Cust_FirmName") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, AMEOffice%>" Visible="false">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlBlock" runat="server" DataTextField="Blocks_Name" DataValueField="Blocks_Id" />
                                        <asp:RequiredFieldValidator ID="BlockValidator" ControlToValidate="ddlBlock" InitialValue="0"
                                            Display="Dynamic" ValidationGroup="UpdateAMEAssociation" SetFocusOnError="true"
                                            Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredAMEBlock %>"
                                            runat="server" />
                                        <ajax:ValidatorCalloutExtender ID="CustomerValidatorCalloutExtender" runat="server"
                                            TargetControlID="BlockValidator" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                            Text="<%$Resources:Labels, Update%>" Font-Underline="False" ValidationGroup="UpdateAMEAssociation" />
                                        <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                            Text="<%$Resources:Labels, Cancel%>" Font-Underline="False" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                            Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%#Bind("Cust_Id") %>' />
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
            </table>
            <div>
                <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
