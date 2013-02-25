<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerPartner.ascx.cs"
    Inherits="CustomerRegistration_UserControls_CustomerPartner" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<table width="100%" cellspacing="5" cellpadding="5" class="formtext">
    <tr align="left">
        <td>
            <asp:Label ID="lblPatName" runat="server" Text="Partner Name" />
        </td>
        <td>
            <asp:TextBox ID="txtPatnerName" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" MaxLength="50" />
                <asp:RegularExpressionValidator ID="BankNameValidator1" runat="server" ControlToValidate="txtPatnerName"
                Display="Dynamic" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup"
                ErrorMessage="Enter Valid Patner Name" CssClass="failureNotification"
                ValidationExpression="^[a-zA-Z]+$" />
            <ajax:ValidatorCalloutExtender ID="BankNameValidator_ValidatorCalloutExtender1" runat="server"
                TargetControlID="BankNameValidator1" />

            <asp:RequiredFieldValidator ID="NameValidator" ControlToValidate="txtPatnerName"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="name cannot be left blank"
                runat="server" />
           
            <ajax:FilteredTextBoxExtender ID="FilteredtxtAuthName" runat="server" TargetControlID="txtPatnerName"
                FilterMode="ValidChars" ValidChars="., " FilterType="LowercaseLetters,UppercaseLetters,Custom" />
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" 
                TargetControlID="NameValidator" />
        </td>
        <td>
            <asp:Label ID="lblAuthFatherName" runat="server" Text="<%$Resources:Labels, FathersName%>" />
        </td>
        <td>
            <asp:TextBox ID="txtPatnerFatherName" onkeypress="return runScript(event)" runat="server"
                CssClass="textbox" MaxLength="50" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPatnerName"
                Display="Dynamic" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup"
                ErrorMessage="Enter Valid Patner Father Name" CssClass="failureNotification"
                ValidationExpression="^[a-zA-Z]+$" />
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                TargetControlID="RegularExpressionValidator1" />

            <asp:RequiredFieldValidator ID="FatherNameValidator" ControlToValidate="txtPatnerFatherName"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredFatherName %>"
                runat="server" />
            <ajax:FilteredTextBoxExtender ID="FilteredtxtAuthFatherName" runat="server" TargetControlID="txtPatnerFatherName"
                FilterMode="ValidChars" ValidChars="., " FilterType="LowercaseLetters,UppercaseLetters,Custom" />
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" 
                TargetControlID="FatherNameValidator" />
            &nbsp;
            <asp:Button CssClass="button" ID="btnSave" runat="server" Text="Save" CausesValidation="true"
                ValidationGroup="SaveGroup" OnClick="btnSave_Click" />
                <asp:Button CssClass="button" ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                <asp:Button ID="btnCancel" runat="server"  Text="Back" CssClass="button"
                    OnClick="btnCancel_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <strong>
                <asp:Label runat="server" ID="lblFolderPath" Text="Partners Information" />
            </strong>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <asp:GridView ID="grdPatner" runat="server" AutoGenerateColumns="False" DataKeyNames="Cust_Partner_ID"
                BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                AllowPaging="True" HorizontalAlign="Center" Width="100%" CellPadding="5">
                <Columns>
                    <asp:TemplateField HeaderText="Partner Name">
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("Cust_Partner_Name") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Father's Name">
                        <ItemTemplate>
                            <asp:Label ID="lblFname" runat="server" Text='<%# Bind("Cust_Partner_FatherName") %>' />
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
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
<div>
    &nbsp;
   
    <uc1:MessageBox ID="ucMessageBox" runat="server" />
    <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
</div>
<div>
    <table width="100%">
        <tr>
            <td align="left">
                &nbsp;
            </td>
            <td align="right" id="btnArea" runat="server">
                &nbsp;</td>
        </tr>
    </table>
</div>
