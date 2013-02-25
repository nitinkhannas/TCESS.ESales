<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageTruckMake.aspx.cs" Inherits="ManageTruckMake" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManageTruckMake%>"
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
                <Custom:GridViewAlwaysShow AllowPaging="true" ID="grdTruckMake" runat="server" AutoGenerateColumns="False"
                    BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                    PageSize="10" Width="100%" HorizontalAlign="Center" CellPadding="5" ShowFooter="True"
                    OnPageIndexChanging="grdTruckMake_PageIndexChanging" OnRowCancelingEdit="grdTruckMake_RowCancelingEdit"
                    OnRowCommand="grdTruckMake_RowCommand" DataKeyNames="TruckMake_Id,TruckMake_TruckWheeler_Id,TruckMake_TruckCC_Id"
                    OnMustAddARow="grdTruckMake_MustAddARow" OnRowDeleting="grdTruckMake_RowDeleting"
                    OnRowEditing="grdTruckMake_RowEditing" OnRowUpdating="grdTruckMake_RowUpdating"
                    OnRowDataBound="grdTruckMake_RowDataBound">
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, TruckMake%>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtTruckMakeName" runat="server" Text='<%# Bind("TruckMake_Name") %>'
                                    MaxLength="70" />
                                <asp:RequiredFieldValidator ID="txtTruckMakeNameValidator" ControlToValidate="txtTruckMakeName"
                                    Display="Dynamic" ValidationGroup="EditTruckMake" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredTruckMakeName%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtTruckMakeNameValidatorCallOut" runat="server"
                                    TargetControlID="txtTruckMakeNameValidator" />
                                <asp:CustomValidator ID="txtTruckMakeNameCustomValidator" runat="server" ControlToValidate="txtTruckMakeName"
                                    Text="*" OnServerValidate="EditTruckMake_ServerValidate" CssClass="failureNotification"
                                    SetFocusOnError="true" Display="Dynamic" ValidationGroup="EditTruckMake" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateTruckMake %>" />
                                <ajax:ValidatorCalloutExtender ID="txtTruckMakeNameCustomValidatorCalloutExtender"
                                    runat="server" TargetControlID="txtTruckMakeNameCustomValidator" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewTruckMakeName" runat="server" MaxLength="70" />
                                <asp:RequiredFieldValidator ID="txtNewTruckMakeNameValidator" ControlToValidate="txtNewTruckMakeName"
                                    Display="Dynamic" ValidationGroup="AddTruckMake" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredTruckMakeName%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtNewTruckMakeNameValidatorCallOut" runat="server"
                                    TargetControlID="txtNewTruckMakeNameValidator" />
                                <asp:CustomValidator ID="txtNewTruckMakeNameCustomValidator" runat="server" ControlToValidate="txtNewTruckMakeName"
                                    Text="*" OnServerValidate="AddTruckMake_ServerValidate" CssClass="failureNotification"
                                    SetFocusOnError="true" Display="Dynamic" ValidationGroup="AddTruckMake" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateTruckMake %>" />
                                <ajax:ValidatorCalloutExtender ID="txtNewTruckMakeNameCustomValidatorCalloutExtender"
                                    runat="server" TargetControlID="txtNewTruckMakeNameCustomValidator" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# Eval("TruckMake_Name")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, TruckWheeler%>">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlTruckWheeler" runat="server" DataTextField="TruckWheeler_Value"
                                    DataValueField="TruckWheeler_Id">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="ddlTruckWheelerValidator" ControlToValidate="ddlTruckWheeler"
                                    InitialValue="0" Display="Dynamic" ValidationGroup="EditTruckMake" SetFocusOnError="true"
                                    Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredWheeler %>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="ddlTruckWheelerCalloutExtender" runat="server"
                                    TargetControlID="ddlTruckWheelerValidator" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlNewTruckWheeler" runat="server" DataTextField="TruckWheeler_Value"
                                    DataValueField="TruckWheeler_Id">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="ddlNewTruckWheelerValidator" ControlToValidate="ddlNewTruckWheeler"
                                    InitialValue="0" Display="Dynamic" ValidationGroup="AddTruckMake" SetFocusOnError="true"
                                    Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredWheeler %>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="ddlNewTruckWheelerValidatorCalloutExtender"
                                    runat="server" TargetControlID="ddlNewTruckWheelerValidator" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# Eval("TruckMake_TruckWheeler_Value")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, CarryCapacity%>">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlTruckCarryCapacity" runat="server" DataTextField="TruckCC_Value"
                                    DataValueField="TruckCC_Id">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="ddlTruckCarryCapacityValidator" ControlToValidate="ddlTruckCarryCapacity"
                                    InitialValue="0" Display="Dynamic" ValidationGroup="EditTruckMake" SetFocusOnError="true"
                                    Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredCarryCapacity %>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="ddlTruckCarryCapacityValidatorCalloutExtender"
                                    runat="server" TargetControlID="ddlTruckCarryCapacityValidator" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlNewTruckCarryCapacity" runat="server" DataTextField="TruckCC_Value"
                                    DataValueField="TruckCC_Id">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="ddlNewTruckCarryCapacityValidator" ControlToValidate="ddlNewTruckCarryCapacity"
                                    InitialValue="0" Display="Dynamic" ValidationGroup="AddTruckMake" SetFocusOnError="true"
                                    Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredCarryCapacity %>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="ddlNewTruckCarryCapacityValidatorCalloutExtender"
                                    runat="server" TargetControlID="ddlNewTruckCarryCapacityValidator" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# Eval("TruckMake_TruckCC_Value")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="<%$Resources:Labels, Update%>" Font-Underline="False" ValidationGroup="EditTruckMake" />
                                <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="<%$Resources:Labels, Cancel%>" Font-Underline="False" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="lnkAdd" runat="server" CausesValidation="true" CommandName="AddNew"
                                    CssClass="button" Text="<%$Resources:Labels, Add%>" ValidationGroup="AddTruckMake" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%#Bind("TruckMake_Id")%>' />
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text="<%$Resources:Labels, Delete%>" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                                    Font-Underline="False" CommandArgument='<%#Bind("TruckMake_Id")%>' />
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
