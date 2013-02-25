<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManageAuthorizedRepresentative.ascx.cs"
    Inherits="CustomerRegistration_UserControls_ManageAuthorizedRepresentative" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<div style="text-align: center;">
    <table width="100%" cellspacing="0" cellpadding="5">
        <tr align="left">
            <td>
                <asp:Label ID="lblMandatorDoc" runat="server" Text="<%$Resources:Labels, MandatoryDocuments%>" />
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
                <asp:Button ID="btnValidate" runat="server" Text="Validate" ValidationGroup="ValidateGroup"
                    CssClass="button" OnClick="btnValidate_Click" />
            </td>
        </tr>
    </table>
</div>
<div class="clear">
    &nbsp;
</div>
<div style="overflow: auto; width: 100%;">
    <Custom:GridViewAlwaysShow AllowPaging="True" ID="grdManageAuthRep" runat="server"
        AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
        Font-Size="Small" PageSize="12" Width="100%" HorizontalAlign="Center" CellPadding="5"
        OnMustAddARow="grdManageAuthRep_MustAddARow" DataKeyNames="AuthRep_Id" OnRowCommand="grdManageAuthRep_RowCommand"
        ShowFooter="true" OnRowDeleting="grdManageAuthRep_RowDeleting">
        <EmptyDataTemplate>
            <asp:Label ID="lblNoRecordsFound" runat="server" Text="No records found" />
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, RepName%>">
                <ItemTemplate>
                    <asp:Label ID="lblDefaultFees" runat="server" Text='<%# Bind("AuthRep_Name") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, FathersName%>">
                <ItemTemplate>
                    <asp:Label ID="lblFirmName" runat="server" Text='<%# Bind("AuthRep_FatherName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, Address%>">
                <ItemTemplate>
                    <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("AuthRep_Address") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, MobileNo%>">
                <ItemTemplate>
                    <asp:Label ID="lblMobileNo" runat="server" Text='<%# Bind("AuthRep_Mobile") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="EditAuthRep"
                        Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%#Bind("AuthRep_Id") %>' />
                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="<%$Resources:Labels, Delete%>" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                        Font-Underline="False" CommandArgument='<%#Bind("AuthRep_Id") %>' />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Button ID="lnkAddTruck" runat="server" CausesValidation="true" CommandName="AddNew"
                        Text="<%$Resources:Labels, Add%>" CssClass="button" ValidationGroup="VGAddAuthRep" />
                </FooterTemplate>
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
    &nbsp;
    <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
</div>