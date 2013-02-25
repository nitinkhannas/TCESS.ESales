<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LiftingRestrictions.aspx.cs" Inherits="Supervisor_LiftingRestrictions" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManageLiftingRestrictionForCustomers%>"
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
            <div style="overflow: auto;">
                <table align="left">
                    <tr>
                        <td>
                            <strong>
                                <asp:Label ID="lblMaterialType" runat="server" Text="<%$Resources:Messages, SelectMaterialType%>" /></strong>&nbsp;
                            <asp:DropDownList ID="ddlMaterialType" DataValueField="MaterialType_Id" DataTextField="MaterialType_Name"
                                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMaterialType_SelectedIndexChanged" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                &nbsp;
            </div>
            <div style="overflow: auto; width: 100%;">
                <Custom:GridViewAlwaysShow ID="grdCustAllotedQuantity" runat="server" AutoGenerateColumns="False"
                    AllowPaging="true" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                    Font-Size="Small" PageSize="11" Width="100%" HorizontalAlign="Center" CellPadding="5"
                    DataKeyNames="Cust_Mat_Id, Cust_Mat_CustId, Cust_Mat_AllotedQuantityId,Cust_Mat_AllotedQuantity"
                    OnRowDataBound="grdCustAllotedQuantity_RowDataBound" OnPageIndexChanging="grdCustAllotedQuantity_PageIndexChanging"
                    OnRowEditing="grdCustAllotedQuantity_RowEditing" OnRowUpdating="grdCustAllotedQuantity_RowUpdating"
                    OnRowCancelingEdit="grdCustAllotedQuantity_RowCancelingEdit" OnMustAddARow="grdCustAllotedQuantity_MustAddARow">
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
                                <asp:Label ID="lblCustName" Text='<%#Bind("CustomerFirmName") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, AnnualRequirement%>">
                            <ItemTemplate>
                                <asp:Label ID="lblAnnualRequirement" Text='<%#Bind("Cust_Mat_AnnualRequirement") %>'
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, CustomerType%>">
                            <EditItemTemplate>
                                <asp:RadioButtonList ID="rdbCustomerRegistrationType" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="CustomerRegType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Provisional" Value="0" />
                                    <asp:ListItem Text="Registered" Value="1" />
                                </asp:RadioButtonList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:RadioButtonList ID="rdbCustomerRegType" runat="server" RepeatDirection="Horizontal"
                                    Enabled="false" SelectedValue='<%#  CheckNull( Eval("CustomerRegType")) %>'>
                                    <asp:ListItem Text="Provisional" Value="0" />
                                    <asp:ListItem Text="Registered" Value="1" />
                                </asp:RadioButtonList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, AllottedQuantity%>">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlQuantity" runat="server" DataTextField="Alloted_Quantity"
                                    DataValueField="Alloted_Id" Enabled="false">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="QuantityValidator" runat="server" ControlToValidate="ddlQuantity"
                                    CssClass="failureNotification" InitialValue="0" SetFocusOnError="true" Display="Dynamic"
                                    Text="*" ErrorMessage="<%$ Resources:ErrorMessages, SelectAllotedQuantity %>"
                                    ValidationGroup="EditGroup" />
                                <ajax:ValidatorCalloutExtender ID="QuantityValidatorCallout" runat="server" TargetControlID="QuantityValidator" />
                                <asp:CustomValidator ID="CustomQuantityValidator" runat="server" ControlToValidate="ddlQuantity"
                                    Text="*" CssClass="failureNotification" OnServerValidate="EditAllotedQuantity_ServerValidate"
                                    SetFocusOnError="true" Display="Dynamic" ValidationGroup="EditGroup" ErrorMessage="<%$ Resources:ErrorMessages, MaxLimitRestriction %>" />
                                <ajax:ValidatorCalloutExtender ID="CustomQuantityValidatorCallout" runat="server"
                                    TargetControlID="CustomQuantityValidator" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblQuantity" Text='<%# Bind("Cust_Mat_AllotedQuantity") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, LiftingLimit%>">
                            <ItemTemplate>
                                <asp:Label ID="lblLiftingLimit" Text='<%#Bind("Cust_Mat_LiftingLimit") %>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLiftingLimit" Text='<%#Bind("Cust_Mat_LiftingLimit") %>' runat="server" />
                                <ajax:FilteredTextBoxExtender ID="LiftingLimitFilteredExtender" runat="server" TargetControlID="txtLiftingLimit"
                                    FilterMode="ValidChars" FilterType="Numbers" />
                                <asp:RequiredFieldValidator ID="LiftingLimitValidator" runat="server" ControlToValidate="txtLiftingLimit"
                                    CssClass="failureNotification" SetFocusOnError="true" Display="Dynamic" Text="*"
                                    ErrorMessage="<%$ Resources:ErrorMessages, RequiredLiftingLimit %>" ValidationGroup="EditGroup" />
                                <ajax:ValidatorCalloutExtender ID="LiftingLimitValidatorCallout" runat="server" TargetControlID="LiftingLimitValidator" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="<%$Resources:Labels, Update%>" Font-Underline="False" ValidationGroup="EditGroup" />
                                <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="<%$Resources:Labels, Cancel%>" Font-Underline="False" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="<%$Resources:Labels, Edit%>" Font-Underline="False" />
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
            </div>
            <div>
             <uc1:MessageBox ID="ucMessageBox" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
