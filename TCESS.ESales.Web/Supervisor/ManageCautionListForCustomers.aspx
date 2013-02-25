<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageCautionListForCustomers.aspx.cs" Inherits="Supervisor_ManageCautionListForCustomers" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, CautionListForCustomers%>"
        CssClass="pageNameContent" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uplMainPanel"
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
    <asp:UpdatePanel runat="server" ID="uplMainPanel" UpdateMode="Conditional">
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
                <Custom:GridViewAlwaysShow AllowPaging="true" ID="grdCustCautionLstMaster" runat="server"
                    AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                    Font-Size="Small" PageSize="10" Width="100%" HorizontalAlign="Center" CellPadding="5"
                    DataKeyNames="Cust_Id" ShowFooter="True" OnRowCommand="grdCustCautionLstMaster_RowCommand"
                    OnRowDataBound="grdCustCautionLstMaster_RowDataBound" OnMustAddARow="grdCustCautionLstMaster_MustAddARow"
                    OnPageIndexChanging="grdCustCautionLstMaster_PageIndexChanging" OnRowDeleting="grdCustCautionLstMaster_RowDeleting">
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecordsFound" runat="server" Text="No records found" />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer Code" ItemStyle-Width="12%">
                            <ItemTemplate>
                                <asp:Label ID="lblCustomerCode" runat="server" Text='<%# Bind("Cust_Code") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer Name">
                            <ItemTemplate>
                                <asp:Label ID="lblDefaultFees" runat="server" Text='<%# Bind("Cust_FirmName") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlCustomerName" runat="server" Width="200px" DataTextField="Cust_FirmName"
                                    DataValueField="Cust_Id">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="ddlCustomerNameValidator" ControlToValidate="ddlCustomerName"
                                    InitialValue="0" Display="Dynamic" ValidationGroup="AddCustCautionList" SetFocusOnError="true"
                                    Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredCustomer %>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="ddlCustomerNameValidatorCalloutExtender" runat="server"
                                    TargetControlID="ddlCustomerNameValidator" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, BlacklistedBy%>" ItemStyle-Width="13%">
                            <ItemTemplate>
                                <%#Eval("Cust_BlacklistedBy") %>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlBlackListedBy" runat="server" Width="100px">
                                    <asp:ListItem Text="<%$ Resources:Messages,SelectUser%>" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="DCA" Value="DCA"></asp:ListItem>
                                    <asp:ListItem Text="TSL" Value="TSL"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="ddlBlackListedByValidator" ControlToValidate="ddlBlackListedBy"
                                    InitialValue="0" Display="Dynamic" ValidationGroup="AddCustCautionList" SetFocusOnError="true"
                                    Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredUser %>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="ddlBlackListedByValidatorCalloutExtender" runat="server"
                                    TargetControlID="ddlBlackListedByValidator" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Last Updated Date" ItemStyle-Width="12%">
                            <ItemTemplate>
                                <asp:Label ID="lblCustoemrType" runat="server" Text='<%#Bind("Cust_LastUpdatedDate","{0:dd-MMM-yyyy}")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Block Name" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblAmEBlockName" runat="server" Text='<%#Bind("AMEBlockOffice")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="7%">
                            <FooterTemplate>
                                <asp:Button ID="lnkAdd" runat="server" CausesValidation="true" CommandName="AddNew"
                                    Text="Add" CssClass="button" ValidationGroup="AddCustCautionList" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                                    Font-Underline="False" CommandArgument='<%#Bind("Cust_Id")%>' />
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
