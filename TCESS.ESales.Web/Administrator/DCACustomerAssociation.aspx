<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="DCACustomerAssociation.aspx.cs" Inherits="Administrator_DCACustomerAssociation" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, CustomerDCAAssociation%>"
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
            <Custom:GridViewAlwaysShow ID="grdDCACustomersAssociation" runat="server" AutoGenerateColumns="False"
                BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                Width="100%" HorizontalAlign="Center" CellPadding="5" DataKeyNames="Cust_ID, Cust_AgentId"
                OnRowCancelingEdit="grdDCACustomersAssociation_RowCancelingEdit" OnRowEditing="grdDCACustomersAssociation_RowEditing"
                OnRowUpdating="grdDCACustomersAssociation_RowUpdating" OnRowDataBound="grdDCACustomersAssociation_RowDataBound">
                <EmptyDataTemplate>
                    <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, CustomerName%>">
                        <ItemTemplate>
                            <asp:Label ID="lblFirmName" runat="server" Text='<%# Bind("Cust_FirmName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, OwnerName%>">
                        <ItemTemplate>
                            <asp:Label ID="lblOwnerName" runat="server" Text='<%# Bind("Cust_OwnerName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, District%>">
                        <ItemTemplate>
                            <asp:Label ID="lblDistrict" runat="server" Text='<%# Bind("Cust_District_Name") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, MobileNo%>">
                        <ItemTemplate>
                            <asp:Label ID="lblMobileNo" runat="server" Text='<%# Bind("Cust_MobileNo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, DCAName%>">
                        <ItemTemplate>
                            <asp:Label ID="lblDCAName" runat="server" Text='<%# Bind("Cust_AgentName") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlDCAName" CssClass="listmenu" runat="server" DataTextField="Agent_Name"
                                DataValueField="Agent_Id" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                        <EditItemTemplate>
                            <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                Text="<%$Resources:Labels, Update%>" Font-Underline="False" ValidationGroup="EditCustomer" />
                            <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="<%$Resources:Labels, Cancel%>" Font-Underline="False" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%# Bind("Cust_ID") %>' />
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
            <div>
                <uc1:MessageBox ID="ucMessageBox" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
