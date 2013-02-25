<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChequeActivation.ascx.cs"
    Inherits="Collection_UserControls_ChequeActivation" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<table width="100%" cellspacing="0" cellpadding="5">
    <tr align="left">
        <td>
            <asp:Label ID="lblChequeNumber" runat="server" Text="<%$Resources:Labels, ENTERCHEQUENUMBER%>" />
            &nbsp;
            <asp:TextBox ID="txtChequeNumber" runat="server" CssClass="textbox" MaxLength="15" />
            <asp:RequiredFieldValidator ID="ChequeNumberValidator" ControlToValidate="txtChequeNumber"
                Display="Dynamic" ValidationGroup="SearchGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredChequeNumber %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="ChequeNumberValidatorCalloutExtender" runat="server"
                TargetControlID="ChequeNumberValidator" />
            &nbsp;
            <asp:Button ID="btnSearch" runat="server" ValidationGroup="SearchGroup" Text="<%$Resources:Labels, SEARCH%>"
                OnClick="btnSearch_Click" CssClass="button" />
        </td>
    </tr>
</table>
<div>
    &nbsp;
</div>
<div style="overflow: auto; width: 100%;">
    <Custom:GridViewAlwaysShow ID="grdChequeActivation" runat="server" AutoGenerateColumns="False"
        BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
        OnPageIndexChanging="grdChequeActivation_PageIndexChanging" OnRowCommand="grdChequeActivation_RowCommand"
        Width="100%" HorizontalAlign="Center" CellPadding="5" DataKeyNames="PC_Id">
        <EmptyDataTemplate>
            <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, ReceiptNo%>">
                <ItemTemplate>
                    <asp:Label ID="lblReceiptNo" runat="server" Text='<%# Bind("PC_Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, ReceiptDate%>">
                <ItemTemplate>
                    <asp:Label ID="lblReceiptDate" runat="server" Text='<%# Bind("PC_ReceiptDate", "{0:dd/MM/yyyy}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, CustomerCode%>">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerCode" runat="server" Text='<%# Bind("CustomerCode") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, CustomerName%>" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerName" runat="server" Text='<%# Bind("CustomerName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, ChequeNumber%>">
                <ItemTemplate>
                    <asp:Label ID="lblChequeNumber" runat="server" Text='<%# Bind("PC_InstrumentNo") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, ChequeDate%>">
                <ItemTemplate>
                    <asp:Label ID="lblChequeDate" runat="server" Text='<%# Bind("PC_InstrumentDate", "{0:dd/MM/yyyy}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, Amount%>" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("PC_Amount") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, BankName%>">
                <ItemTemplate>
                    <asp:Label ID="lblBankDrawn" runat="server" Text='<%# Bind("BankName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, BranchName%>">
                <ItemTemplate>
                    <asp:Label ID="lblBranchName" runat="server" Text='<%# Bind("PC_BankBranch") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="EditCheque"
                        Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%# Bind("PC_Id") %>' />
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
    <asp:CustomValidator ID="customValidator" Display="Dynamic" runat="server" CssClass="failureNotification" />
    <uc1:MessageBox ID="ucMessageBox" runat="server" />
    <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
</div>
