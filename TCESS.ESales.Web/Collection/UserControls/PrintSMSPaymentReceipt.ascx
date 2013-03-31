<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PrintSMSPaymentReceipt.ascx.cs"
    Inherits="Collection_UserControls_PrintSMSPaymentReceipt" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<%@ Register Src="SMSReceipt.ascx" TagName="SMSReceipt" TagPrefix="uc1" %>
<style type="text/css">
    .style1
    {
        width: 55px;
        
    }
</style>
<div runat="server" id="pnlPaymentCollection">
    <table width="100%" cellspacing="10" cellpadding="5">
        <tr align="left">
            <td nowrap="nowrap">
                <asp:Label ID="lblSearchBy" runat="server" Text="Search By" />
            </td>
            <td nowrap="nowrap">
                <asp:DropDownList ID="ddLSerachType" runat="server" CssClass="listmenu"
                    DataValueField="Doc_Id" AutoPostBack="True" 
                    onselectedindexchanged="ddLSerachType_SelectedIndexChanged">
                    <asp:ListItem Value="1">Customer Code</asp:ListItem>
                    <asp:ListItem Value="2">SMS ID</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td nowrap="nowrap">
                &nbsp;
            </td>
            <td nowrap="nowrap">
                &nbsp;
            </td>
            <td nowrap="nowrap">
                &nbsp;
            </td>
            <td nowrap="nowrap">
                &nbsp;
            </td>
            <td nowrap="nowrap">
                &nbsp;
            </td>
            <td nowrap="nowrap">
                &nbsp;
            </td>
        </tr>
        <tr align="left" runat="server" id="trCustCode">
            <td  nowrap="nowrap">
                <asp:Label ID="lblCustomerCode" runat="server" Text="<%$Resources:Labels, CustomerCode%>" />
            </td>
            <td  nowrap="nowrap">
                <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="textbox" Wrap="False" />
                <asp:RequiredFieldValidator ID="CustomerCodeValidator" runat="server" ControlToValidate="txtCustomerCode"
                    CssClass="failureNotification" Display="Dynamic" ErrorMessage="<%$ Resources:ErrorMessages, RequiredCustomerCode %>"
                    SetFocusOnError="true" Text="*" ValidationGroup="ValidateGroup" />
                <ajax:ValidatorCalloutExtender ID="CustomerCodeValidatorCalloutExtender" runat="server"
                    TargetControlID="CustomerCodeValidator" />
            </td>
            <td nowrap="nowrap">
                &nbsp;
            </td>
            <td nowrap="nowrap">
                <asp:Label ID="lblValidationType" runat="server" Text="<%$Resources:Labels, MandatoryDocuments%>" />
            </td>
            <td nowrap="nowrap">
                <asp:DropDownList ID="ddlValidationType" runat="server" CssClass="listmenu" DataTextField="Doc_Name"
                    DataValueField="Doc_Id" />
                <asp:RequiredFieldValidator ID="ValidationTypeValidator" runat="server" ControlToValidate="ddlValidationType"
                    CssClass="failureNotification" Display="Dynamic" ErrorMessage="<%$ Resources:ErrorMessages, RequiredValidationType%>"
                    InitialValue="0" SetFocusOnError="true" Text="*" ValidationGroup="ValidateGroup" />
                <ajax:ValidatorCalloutExtender ID="ValidationTypeValidatorCallOutExtender" runat="server"
                    TargetControlID="ValidationTypeValidator" />
            </td>
            <td nowrap="nowrap">
                &nbsp;
            </td>
            <td nowrap="nowrap">
                <asp:Label ID="Label3" runat="server" Text="<%$Resources:Labels, DocumentNumber%>" />
            </td>
            <td nowrap="nowrap">
                <asp:TextBox ID="txtValidationValue" onkeypress="return runScript(event)" runat="server"
                    CssClass="textbox" MaxLength="15" />
                <asp:RequiredFieldValidator ID="ValidationValueValidator" ControlToValidate="txtValidationValue"
                    Display="Dynamic" ValidationGroup="ValidateGroup" SetFocusOnError="true" Text="*"
                    CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredValidationType%>"
                    runat="server" />
                <ajax:FilteredTextBoxExtender ID="ValidationValueValidatorExtender" runat="server"
                    TargetControlID="txtValidationValue" FilterType="LowercaseLetters, UppercaseLetters, Numbers" />
                <asp:Button ID="btnValidate" runat="server" CssClass="button" OnClick="btnValidate_Click"
                    Text="Validate" ValidationGroup="ValidateGroup" />
            </td>
        </tr>
        <tr align="left" runat="server" id="trSMS">
            <td class="style1" nowrap="nowrap">
                <asp:Label ID="lblSMSPaymentID" runat="server" Text="SMS ID" />
            </td>
            <td class="style1" nowrap="nowrap">
                <asp:TextBox ID="txtValidationID" onkeypress="return runScript(event)" runat="server"
                    CssClass="textbox" MaxLength="15" />
                <ajax:FilteredTextBoxExtender ID="txtValidationValue0_FilteredTextBoxExtender" runat="server"
                    TargetControlID="txtValidationID" FilterType="LowercaseLetters, UppercaseLetters, Numbers" />
                    <ajax:FilteredTextBoxExtender ID="ValidatorExtender" runat="server" TargetControlID="txtValidationID"
                    FilterType="Custom, Numbers" FilterMode="ValidChars" ValidChars="." />


                <asp:RequiredFieldValidator ID="ValidationValueValidator0" ControlToValidate="txtValidationID"
                    Display="Dynamic" ValidationGroup="ValidateGroup" SetFocusOnError="true" Text="*"
                    CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredValidationType%>"
                    runat="server" />
            </td>
            <td class="style1" nowrap="nowrap">
                &nbsp;
            </td>
            <td nowrap="nowrap" align="left" class="style1">
                <asp:Button ID="btnValidateID" runat="server" CssClass="button" OnClick="btnValidateID_Click"
                    Text="Validate" ValidationGroup="ValidateGroup" />
            </td>
            <td >
                &nbsp;
            </td>
            <td >
                &nbsp;
            </td>
            <td >
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <div style="overflow: auto; width: 100%;">
        <Custom:GridViewAlwaysShow AllowPaging="True" ID="grdCustomersDetails" runat="server"
            AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
            Font-Size="Small" PageSize="12" Width="100%" HorizontalAlign="Center" CellPadding="5"
            DataKeyNames="SMSPay_Id">
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
                        <asp:Label ID="lblDefaultFees" runat="server" Text='<%# Bind("SMSPay_Cust_TradeName") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Labels, FirmName%>">
                    <ItemTemplate>
                        <asp:Label ID="lblFirmName" runat="server" Text='<%# Bind("SMSPay_Cust_UnitName") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Labels, BusinessType%>">
                    <ItemTemplate>
                        <asp:Label ID="lblBusinessType" runat="server" Text='<%# Bind("SMSPay_CustomerBusinessType") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="District Name">
                    <ItemTemplate>
                        <asp:Label ID="lblMobileNo" runat="server" Text='<%# Bind("SMSPay_Cust_District_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="lblPayDate" runat="server" Text='<%#Convert.ToDateTime(Eval("SMSPay_Date")).ToString("dd-MMM-yyyy")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount">
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("SMSPay_Amount") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                    <ItemTemplate>
                        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="button" CausesValidation="false"
                            OnCommand="btnPrint_Click" CommandArgument='<%#Bind("SMSPay_Id") %>' />
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
    <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
    <uc4:YesNoMessageBox ID="ucYesNoMessageBox" runat="server" />
</div>
<div id="pnlSMSReceipt" runat="server">
    <uc1:SMSReceipt ID="ucSMSReceipt" runat="server" />
</div>
