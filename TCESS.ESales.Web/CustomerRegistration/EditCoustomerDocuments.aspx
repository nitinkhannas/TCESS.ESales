<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="EditCoustomerDocuments.aspx.cs" Inherits="CustomerRegistration_EditCoustomerDocuments" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <label class="pageNameContent">
        Edit Customer
    </label>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="100%" cellspacing="0" cellpadding="5">
        <tr align="left">
            <td>
                <asp:Label ID="lblMandatoryDoc0" runat="server" Text="All Documents" />
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:DropDownList ID="ddlAllDocument" runat="server" DataTextField="Doc_Name" DataValueField="Doc_Id"
                    CssClass="listmenu" />
                <asp:RequiredFieldValidator ID="MaterialValidator" runat="server" ControlToValidate="ddlAllDocument"
                    CssClass="failureNotification" Display="Dynamic" ErrorMessage="Please Select Document"
                    InitialValue="0" SetFocusOnError="true" Text="*" ValidationGroup="ValidateGroup" />
                <ajax:ValidatorCalloutExtender ID="MaterialValidatorCallOutExtender" runat="server"
                    TargetControlID="MaterialValidator" />
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblMandatoryDoc" runat="server" Text="<%$Resources:Labels, MandatoryDocuments%>" />
            </td>
            <td>
                &nbsp;
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
                <asp:TextBox ID="txtDocNumber" runat="server" CssClass="textbox" Wrap="False" Width="120px"
                    MaxLength="15" />
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
    <table width="100%" cellspacing="0" cellpadding="5">
        <tr align="left">
            <td>
                <Custom:GridViewAlwaysShow AllowPaging="True" ID="grdManageCustomers" runat="server"
                    AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                    Font-Size="Small" PageSize="12" Width="100%" HorizontalAlign="Center" CellPadding="5"
                    DataKeyNames="Cust_ID">
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, TradeName%>">
                            <ItemTemplate>
                                <asp:Label ID="lblDefaultFees" runat="server" Text='<%# Bind("Cust_TradeName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, FirmName%>">
                            <ItemTemplate>
                                <asp:Label ID="lblFirmName" runat="server" Text='<%# Bind("Cust_FirmName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, OwnerName%>">
                            <ItemTemplate>
                                <asp:Label ID="lblOwnerName" runat="server" Text='<%# Bind("Cust_OwnerName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, BusinessType%>">
                            <ItemTemplate>
                                <asp:Label ID="lblBusinessType" runat="server" Text='<%# Bind("Cust_Business_Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, MobileNo%>">
                            <ItemTemplate>
                                <asp:Label ID="lblMobileNo" runat="server" Text='<%# Bind("Cust_MobileNo") %>' />
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
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkDocID" runat="server" Checked='<%# Bind("Doc_Mandatory") %>'
                                    Enabled="False" />
                                <asp:HiddenField ID="hdnCustDocId" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, DocumentName%>">
                            <ItemTemplate>
                                <asp:Label ID="lblname" runat="server" Text='<%# Bind("Doc_Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Acronym%>">
                            <ItemTemplate>
                                <asp:Label ID="lblAcronymName" runat="server" Text='<%# Bind("Doc_Acronym") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, DocumentNumber%>" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:TextBox ID="txtDocNo" MaxLength="20" runat="server" />
                                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtDocNo"
                                    FilterMode="ValidChars" ValidChars="-" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" />
                                <asp:CustomValidator ControlToValidate="txtDocNo" ID="DocNoValidator" Display="Dynamic"
                                    ValidateEmptyText="true" OnServerValidate="DocNo_ServerValidate" runat="server"
                                    ValidationGroup="SaveGroup" ErrorMessage="<%$ Resources:ErrorMessages, RequiredDocNo %>"
                                    Text="*" CssClass="failureNotification" />
                                <ajax:ValidatorCalloutExtender ID="DocNoValidatorCalloutExtender" runat="server"
                                    TargetControlID="DocNoValidator" />
                                <asp:CustomValidator ControlToValidate="txtDocNo" ID="txtDocNoCustomValidator" Display="Dynamic"
                                    ValidateEmptyText="true" OnServerValidate="DocNoExist_ServerValidate" runat="server"
                                    ValidationGroup="SaveGroup" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateDocumentNumber %>"
                                    Text="*" CssClass="failureNotification" />
                                <ajax:ValidatorCalloutExtender ID="txtDocNoCustomValidatorCalloutExtender" runat="server"
                                    TargetControlID="txtDocNoCustomValidator" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, DocumentExpiryDate%>" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:TextBox ID="txtDocExDate" runat="server" />
                                <ajax:CalendarExtender ID="CalendarExtender1" Format="dd MMM yyyy" runat="server"
                                    TargetControlID="txtDocExDate" />
                                <asp:CustomValidator ControlToValidate="txtDocExDate" ID="DocExDateValidator" OnServerValidate="DocExDate_ServerValidate"
                                    Text="*" Display="Dynamic" ValidateEmptyText="true" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, RequiredDocExpiryDate %>"
                                    ValidationGroup="SaveGroup" CssClass="failureNotification" />
                                <ajax:ValidatorCalloutExtender ID="DocExDateValidatorCalloutExtender" runat="server"
                                    TargetControlID="DocExDateValidator" />
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
    <table width="100%" cellspacing="10" cellpadding="5">
        <tr>
            <td align="left" runat="server" id="UploadFileArea">
                <label for="lblTradeName" class="formlabel">
                    Upload File</label>
                &nbsp;
                <asp:FileUpload ID="filAuthDoc" runat="server" />
            </td>
            <td align="right" runat="server" id="ButtonArea">
                <asp:Button ID="btnSaveAndUpload" CssClass="button" runat="server" Text="Save and Upload"
                    OnClick="btnSaveAndUpload_Click" />
                &nbsp;<asp:Button ID="btnClose" CssClass="button" runat="server" Text="Close" OnClick="btnClose_Click" />
            </td>
        </tr>
    </table>
    <div>
        <asp:CustomValidator ID="customValidator" Display="Dynamic" runat="server" CssClass="failureNotification" />
        <uc1:MessageBox ID="ucMessageBox" runat="server" />
        <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
    </div>
</asp:Content>
