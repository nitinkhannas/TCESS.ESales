<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageStates.aspx.cs" Inherits="Masters_ManageStates" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManageStates%>"
        CssClass="pageNameContent" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="progressBar" runat="server" AssociatedUpdatePanelID="uplMainPanel"
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
            <div style="overflow: auto; width: 100%;">
                <Custom:GridViewAlwaysShow AllowPaging="true" ID="grdState" runat="server" AutoGenerateColumns="False"
                    BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                    PageSize="10" Width="100%" HorizontalAlign="Center" CellPadding="5" ShowFooter="True"
                    DataKeyNames="State_Id" OnRowCommand="grdState_RowCommand" OnRowCancelingEdit="grdState_RowCancelingEdit"
                    OnRowDeleting="grdState_RowDeleting" OnRowUpdating="grdState_RowUpdating" OnMustAddARow="grdState_MustAddARow"
                    OnPageIndexChanging="grdState_PageIndexChanging" OnRowEditing="grdState_RowEditing">
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
                            <EditItemTemplate>
                                <asp:TextBox ID="txtState" runat="server" Text='<%# Bind("State_Name") %>' MaxLength="70" />
                                <asp:RequiredFieldValidator ID="txtStateValidator" ControlToValidate="txtState" Display="Dynamic"
                                    ValidationGroup="EditState" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                                    ErrorMessage="<%$Resources:ErrorMessages, RequiredStateName%>" runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtStateValidatorCallOut" runat="server"
                                    TargetControlID="txtStateValidator" />
                                <asp:CustomValidator ID="txtStateCustomValidator" runat="server" ControlToValidate="txtState"
                                    Text="*" OnServerValidate="EditState_ServerValidate" CssClass="failureNotification"
                                    SetFocusOnError="true" Display="Dynamic" ValidationGroup="EditState" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateState %>" />
                                <ajax:ValidatorCalloutExtender ID="txtStateCustomValidatorCalloutExtender"
                                    runat="server" TargetControlID="txtStateCustomValidator" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewState" runat="server" MaxLength="70" />
                                <asp:RequiredFieldValidator ID="txtNewStateValidator" ControlToValidate="txtNewState"
                                    Display="Dynamic" ValidationGroup="AddState" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredStateName%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtNewStateValidatorCallOut" runat="server"
                                    TargetControlID="txtNewStateValidator" />
                                <asp:CustomValidator ID="txtNewStateCustomValidator" runat="server" ControlToValidate="txtNewState"
                                    Text="*" OnServerValidate="AddState_ServerValidate" CssClass="failureNotification"
                                    SetFocusOnError="true" Display="Dynamic" ValidationGroup="AddState" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateState %>" />
                                <ajax:ValidatorCalloutExtender ID="txtNewStateCustomValidatorCalloutExtender"
                                    runat="server" TargetControlID="txtNewStateCustomValidator" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# Eval("State_Name")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="<%$Resources:Labels, Update%>" Font-Underline="False" ValidationGroup="EditState" />
                                <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="<%$Resources:Labels, Cancel%>" Font-Underline="False" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="lnkAdd" runat="server" CausesValidation="true" CommandName="AddNew"
                                    CssClass="button" Text="<%$Resources:Labels, Add%>" ValidationGroup="AddState" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%#Bind("State_Id")%>' />
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text="<%$Resources:Labels, Delete%>" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                                    Font-Underline="False" CommandArgument='<%#Bind("State_Id")%>' />
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
