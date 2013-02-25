<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageCautionListForTrucks.aspx.cs" Inherits="Supervisor_ManageCautionListForTrucks" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManageCautionListforTrucks%>"
        CssClass="pageNameContent" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <ContentTemplate>
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
                        <td>&nbsp;</td>
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
            <div class="clear">&nbsp;</div>
            <div style="overflow: auto; width: 100%;">
                <Custom:GridViewAlwaysShow AllowPaging="true" ID="grdTruckCautionLst" runat="server"
                    DataKeyNames="Truck_Id" AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid"
                    BorderWidth="1px" Font-Size="Small" PageSize="12" Width="100%" HorizontalAlign="Center"
                    CellPadding="5" ShowFooter="True" OnMustAddARow="grdTruckCautionLst_MustAddARow"
                    OnPageIndexChanging="grdTruckCautionLst_PageIndexChanging" OnRowCommand="grdTruckCautionLst_RowCommand"
                    OnRowDataBound="grdTruckCautionLst_RowDataBound" OnRowDeleting="grdTruckCautionLst_RowDeleting">
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecordsFound" runat="server" Text="No records found" />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, TruckNo%>">
                            <ItemTemplate>
                                <asp:Label ID="lblTruckRegNo" runat="server" Text='<%# Bind("Truck_RegNo") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlTruckRegNo" runat="server" DataTextField="Truck_RegNo" DataValueField="Truck_Id" />
                                <asp:RequiredFieldValidator ID="ddlTruckRegNoValidator" ControlToValidate="ddlTruckRegNo"
                                    InitialValue="0" Display="Dynamic" ValidationGroup="AddGroup" SetFocusOnError="true"
                                    Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredTruckRegistrationNo %>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="ddlTruckRegNoValidatorCalloutExtender"
                                    runat="server" TargetControlID="ddlTruckRegNoValidator" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, BlacklistedBy%>">
                            <ItemTemplate>
                                <asp:Label ID="lblTruckBlacklistedBy" runat="server" Text='<%# Bind("Truck_BlacklistedBy") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlBlackListedBy" runat="server">
                                    <asp:ListItem Text="<%$Resources:Messages,SelectUser%>" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="DCA" Value="DCA"></asp:ListItem>
                                    <asp:ListItem Text="TSL" Value="TSL"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="ddlBlackListedByValidator" ControlToValidate="ddlBlackListedBy"
                                    InitialValue="0" Display="Dynamic" ValidationGroup="AddGroup" SetFocusOnError="true"
                                    Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredUser %>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="ddlBlackListedByValidatorCalloutExtender"
                                    runat="server" TargetControlID="ddlBlackListedByValidator" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, OwnerName%>">
                            <ItemTemplate>
                                <asp:Label ID="lblTruckOwnerName" runat="server" Text='<%# Bind("Truck_OwnerName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, DriverName%>">
                            <ItemTemplate>
                                <asp:Label ID="lblDriverName" runat="server" Text='<%# Bind("Truck_DriverName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, CustomerName%>">
                            <ItemTemplate>
                                <asp:Label ID="lblCustomerName" runat="server" Text='<%#(Eval("Truck_Customer")==null)? "":((TCESS.ESales.DataTransferObjects.CustomerDTO)Eval("Truck_Customer")).Cust_FirmName%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                            <FooterTemplate>
                                <asp:Button ID="lnkAdd" runat="server" CausesValidation="true" CommandName="AddNew"
                                    Text="<%$Resources:Labels, Add%>" ValidationGroup="AddGroup" CssClass="button" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text="<%$Resources:Labels, Delete%>" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                                    Font-Underline="False" CommandArgument='<%#Bind("Truck_Id")%>' />
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
