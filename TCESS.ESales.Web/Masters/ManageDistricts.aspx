<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageDistricts.aspx.cs" Inherits="Masters_ManageDistricts" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManageDistricts%>"
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
                        <asp:Label ID="lblState" runat="server" Text="<%$Resources:Labels, State%>" />
                    </td>
                    <td align="left">
                        <asp:Label ID="lblDistrict" runat="server" Text="<%$Resources:Labels, DistrictsFor%>" />
                        <asp:Label ID="lblDistrictName" runat="server" />
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <Custom:GridViewAlwaysShow ID="grdState" DataKeyNames="State_Id" runat="server" AutoGenerateColumns="False"
                            BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                            AllowPaging="true" PageSize="10" Width="100%" HorizontalAlign="Center" CellPadding="5"
                            OnRowCommand="grdState_RowCommand" OnMustAddARow="grdState_MustAddARow" OnPageIndexChanging="grdState_PageIndexChanging">
                            <EmptyDataTemplate>
                                <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, StateName%>">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkState" runat="server" CommandArgument='<%#Bind("State_Id") %>'
                                            Text='<%# Bind("State_Name") %>' CommandName="ShowDistrict" Font-Underline="False" />
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
                        <Custom:GridViewAlwaysShow ID="grdDistrict" runat="server" AutoGenerateColumns="False"
                            ShowFooter="true" AllowPaging="true" BorderColor="#3366CC" BorderStyle="Solid"
                            BorderWidth="1px" DataKeyNames="Dist_Id" Font-Size="Small" PageSize="10" Width="100%"
                            HorizontalAlign="Center" CellPadding="5" OnMustAddARow="grdDistrict_MustAddARow"
                            OnRowCommand="grdDistrict_RowCommand" OnPageIndexChanging="grdDistrict_PageIndexChanging"
                            OnRowCancelingEdit="grdDistrict_RowCancelingEdit" OnRowDeleting="grdDistrict_RowDeleting"
                            OnRowEditing="grdDistrict_RowEditing" OnRowUpdating="grdDistrict_RowUpdating">
                            <EmptyDataTemplate>
                                <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, DistrictName%>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDistrict" runat="server" Text='<%# Bind("Dist_Name") %>' MaxLength="100" />
                                        <asp:RequiredFieldValidator ID="txtDistrictValidator" ControlToValidate="txtDistrict"
                                            Display="Dynamic" ValidationGroup="EditDistrict" SetFocusOnError="true" Text="*"
                                            CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredDistrictName%>"
                                            runat="server" />
                                        <ajax:ValidatorCalloutExtender ID="txtDistrictValidatorCallOut" runat="server"
                                            TargetControlID="txtDistrictValidator" />
                                        <asp:CustomValidator ID="txtDistrictCustomValidator" runat="server" ControlToValidate="txtDistrict"
                                            Text="*" CssClass="failureNotification" OnServerValidate="EditDistrict_ServerValidate"
                                            SetFocusOnError="true" Display="Dynamic" ValidationGroup="EditDistrict" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateDistrict %>" />
                                        <ajax:ValidatorCalloutExtender ID="txtDistrictCustomValidatorCalloutExtender"
                                            runat="server" TargetControlID="txtDistrictCustomValidator" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtNewDistrict" runat="server" MaxLength="100" />
                                        <asp:RequiredFieldValidator ID="txtNewDistrictValidator" ControlToValidate="txtNewDistrict"
                                            Display="Dynamic" ValidationGroup="AddDistrict" SetFocusOnError="true" Text="*"
                                            CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredDistrictName%>"
                                            runat="server" />
                                        <ajax:ValidatorCalloutExtender ID="txtNewDistrictValidatorCallOut" runat="server"
                                            TargetControlID="txtNewDistrictValidator" />
                                        <asp:CustomValidator ID="txtNewDistrictCustomValidator" runat="server" ControlToValidate="txtNewDistrict"
                                            Text="*" OnServerValidate="AddDistrict_ServerValidate" CssClass="failureNotification"
                                            SetFocusOnError="true" Display="Dynamic" ValidationGroup="AddDistrict" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateDistrict %>" />
                                        <ajax:ValidatorCalloutExtender ID="txtNewDistrictCustomValidatorCalloutExtender"
                                            runat="server" TargetControlID="txtNewDistrictCustomValidator" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDistrictName" Text='<%# Bind("Dist_Name") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                            Text="<%$Resources:Labels, Update%>" Font-Underline="False" ValidationGroup="EditDistrict" />
                                        <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                            Text="<%$Resources:Labels, Cancel%>" Font-Underline="False" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="lnkAdd" runat="server" CausesValidation="true" CommandName="AddNew"
                                            CssClass="button" Text="<%$Resources:Labels, Add%>" ValidationGroup="AddDistrict" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                            Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%#Bind("Dist_Id")%>' />
                                        <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                            Text="<%$Resources:Labels, Delete%>" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                                            Font-Underline="False" CommandArgument='<%#Bind("Dist_Id")%>' />
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
						<uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
