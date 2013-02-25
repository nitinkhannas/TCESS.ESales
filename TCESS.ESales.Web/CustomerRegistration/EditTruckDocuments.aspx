<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="EditTruckDocuments.aspx.cs" Inherits="CustomerRegistration_EditTruckDocuments" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <label class="pageNameContent">
        Edit Trucks
    </label>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="100%" cellspacing="5" cellpadding="5">
        <tr align="left">
            <td width="7%">
                <asp:Label ID="lblTruckNo" runat="server" Text="Truck No" />
            </td>
            <td width="20%">
                <asp:TextBox ID="txtTruckNo" runat="server" CssClass="textbox" Wrap="False" MaxLength="15" />
                <asp:RequiredFieldValidator ID="TruckNoRequiredFieldValidator" ControlToValidate="txtTruckNo"
                    Display="Dynamic" ValidationGroup="ValidateGroup" SetFocusOnError="true" Text="*"
                    CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredTruckRegNo %>"
                    runat="server" />
                <ajax:ValidatorCalloutExtender ID="TruckNoValidatorCalloutExtender" runat="server"
                    TargetControlID="TruckNoRequiredFieldValidator" />
            </td>
            <td align="right" width="12%">
                <asp:Label ID="lblTruckDoc" runat="server" Text="Truck Documents" />
            </td>
            <td width="20%">
                <asp:DropDownList ID="ddlTruckDoc" runat="server" DataTextField="Doc_Name" DataValueField="Doc_Id"
                    CssClass="listmenu" />
            </td>
            <td width="17%">
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnValidate" runat="server" ValidationGroup="ValidateGroup" Text="Validate"
                    OnClick="btnValidate_Click" CssClass="button" Width="80px" />
            </td>
        </tr>
    </table>
    <table width="100%" cellspacing="0" cellpadding="5">
        <tr align="left">
            <td>
                <Custom:GridViewAlwaysShow AllowPaging="false" ID="grdManageTrucks" runat="server"
                    AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                    Font-Size="Small" PageSize="12" Width="100%" HorizontalAlign="Center" CellPadding="5"
                    DataKeyNames="Truck_RegNo" OnRowEditing="grdManageTrucks_RowEditing" OnRowCancelingEdit="grdManageTrucks_RowCancelingEdit"
                    OnRowUpdating="grdManageTrucks_RowUpdating">
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, TruckNo%>">
                            <ItemTemplate>
                                <asp:Label ID="lblTruckNumber" runat="server" Text='<%# Bind("Truck_RegNo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, OwnerName%>">
                            <ItemTemplate>
                                <asp:Label ID="lblOwnerName" runat="server" Text='<%# Bind("Truck_OwnerName") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtOwnerName" MaxLength="20" runat="server" Text='<%# Bind("Truck_OwnerName") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, DriverName%>">
                            <ItemTemplate>
                                <asp:Label ID="lblDriverName" runat="server" Text='<%# Bind("Truck_DriverName") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDriverName" MaxLength="20" runat="server" Text='<%# Bind("Truck_DriverName") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, RegisteredAddress%>">
                            <ItemTemplate>
                                <asp:Label ID="lblRegisteredAddress" runat="server" Text='<%# Bind("Truck_Address") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRegisteredAddress" MaxLength="20" runat="server" Text='<%# Bind("Truck_Address") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, MobileNo%>">
                            <ItemTemplate>
                                <asp:Label ID="lblMobileNo" runat="server" Text='<%# Bind("Truck_MobileNo") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMobileNo" MaxLength="20" runat="server" Text='<%# Bind("Truck_MobileNo") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%#Bind("Truck_RegNo")%>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="<%$Resources:Labels, Update%>" Font-Underline="False" ValidationGroup="EditTrucks" />
                                <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="<%$Resources:Labels, Cancel%>" Font-Underline="False" />
                            </EditItemTemplate>
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
    <table width="100%" cellspacing="0" cellpadding="5">
        <tr align="left">
            <td>
                <Custom:GridViewAlwaysShow ID="grdDocument" runat="server" DataKeyNames="Doc_Id"
                    AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="5" Font-Size="Small" HorizontalAlign="Center" Width="100%">
                    <EmptyDataTemplate>
                        No Record Found.
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, DocumentName%>">
                            <ItemTemplate>
                                <asp:Label ID="lblDocName" runat="server" Text='<%# Bind("Doc_Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Acronym%>">
                            <ItemTemplate>
                                <asp:Label ID="lblDocAcronymName" runat="server" Text='<%# Bind("Doc_Acronym") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, DocumentNumber%>" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:TextBox ID="txtDocNo" MaxLength="20" runat="server" />
                                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtDocNo"
                                    FilterMode="ValidChars" ValidChars="-" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, DocumentExpiryDate%>" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:TextBox ID="txtDocExDate" runat="server" />
                                <ajax:CalendarExtender ID="CalendarExtender1" Format="dd MMM yyyy" runat="server"
                                    TargetControlID="txtDocExDate" />
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
    <table width="100%" cellspacing="5" cellpadding="5">
        <tr>
            <td align="left" runat="server" id="UploadFileArea" width="81%">
                <label for="lblTradeName" class="formlabel">
                    Upload File</label>
                &nbsp;
                <asp:FileUpload ID="filAuthDoc" runat="server" />
            </td>
            <td>
                <asp:HiddenField ID="hdnFlag" runat="server" />
            </td>
            <td align="left" runat="server" id="ButtonArea">
                <asp:Button ID="btnSaveAndUpload" CssClass="button" runat="server" Text="Save and Upload"
                    OnClick="btnSaveAndUpload_Click" Width="113px" />
            </td>
            <td>
                <asp:Button ID="btnClose" CssClass="button" runat="server" Text="Close" OnClick="btnClose_Click"
                    Width="60px" />
            </td>
        </tr>
    </table>
    <div>
        <asp:CustomValidator ID="customValidator" Display="Dynamic" runat="server" CssClass="failureNotification" />
        <uc1:MessageBox ID="ucMessageBox" runat="server" />
        <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
    </div>
</asp:Content>
