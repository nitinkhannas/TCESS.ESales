﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CancelCollectionReceipt.ascx.cs"
    Inherits="Collection_UserControls_CancelCollectionReceipt" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<div style="text-align: center;">
    <table width="100%" cellspacing="0" cellpadding="5">
        <tr align="left">
            <td>
                <asp:Label ID="lblSearchType" runat="server" Text="<%$Resources:Labels, SEARCHTYPE%>" />
            </td>
            <td>
                <asp:DropDownList ID="ddlSearchType" runat="server" CssClass="listmenu">
                    <asp:ListItem Text="Select Search Type" Value="0" />
                    <asp:ListItem Text="Customer Code" Value="1" />
                    <asp:ListItem Text="Receipt Number" Value="2" />
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblSearchValue" runat="server" Text="<%$Resources:Labels, SEARCHVALUE%>" />
            </td>
            <td>
                <asp:TextBox ID="txtSearchValue" runat="server" CssClass="textbox" Wrap="False" MaxLength="15"
                    onkeypress="return runScript(event)" />
                <asp:RequiredFieldValidator ID="SearchValueValidator" ControlToValidate="txtSearchValue"
                    Display="Dynamic" ValidationGroup="ValidateGroup" SetFocusOnError="true" Text="*"
                    CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, REQUIREDSEARCHVALUE %>"
                    runat="server" />
                <ajax:ValidatorCalloutExtender ID="SearchValueValidatorCalloutExtender" runat="server"
                    TargetControlID="SearchValueValidator" />
                <asp:Button ID="btnSearch" runat="server" ValidationGroup="ValidateGroup" Text="<%$Resources:Labels, SEARCH%>"
                    OnClick="btnSearch_Click" CssClass="button" />
            </td>
        </tr>
    </table>
</div>
<div class="clear">
    &nbsp;
</div>
<div style="overflow: auto; width: 100%;">
    <Custom:GridViewAlwaysShow AllowPaging="true" ID="grdCancelCollection" runat="server"
        AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
        Font-Size="Small" Width="100%" HorizontalAlign="Center" CellPadding="5" PageSize="13"
        OnPageIndexChanging="grdCancelCollection_PageIndexChanging" DataKeyNames="PC_Id, PC_PaymentMode, PC_CustId"
        OnRowCancelingEdit="grdCancelCollection_RowCancelingEdit" OnRowEditing="grdCancelCollection_RowEditing"
        OnRowUpdating="grdCancelCollection_RowUpdating" OnRowDataBound="grdCancelCollection_RowDataBound"
        OnRowCommand="grdCancelCollection_RowCommand">
        <EmptyDataTemplate>
            <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="<%$Resources:Labels, ReceiptNo%>">
                <ItemTemplate>
                    <asp:Label ID="lblReceiptId" runat="server" Text='<%# Bind("PC_Id") %>' />
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
            <asp:TemplateField HeaderText="<%$Resources:Labels, ModeOfPayment%>">
                <ItemTemplate>
                    <asp:Label ID="lblInstrumentType" runat="server" Text='<%# Bind("PaymentModeName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, InstrumentNumber%>">
                <ItemTemplate>
                    <asp:Label ID="lblInstrumentNumber" runat="server" Text='<%# Bind("PC_InstrumentNo") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, InstrumentDate%>">
                <ItemTemplate>
                    <%# Eval("PC_InstrumentDate") != null ? Eval("PC_InstrumentDate", "{0:dd/MM/yyyy}") : "NA"%>
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
            <asp:TemplateField HeaderText="<%$Resources:Labels, Amount%>" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("PC_Amount") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, REASON%>">
                <EditItemTemplate>
                    <asp:TextBox ID="txtReason" onkeypress="return runScript(event)" runat="server" />
                    <asp:RequiredFieldValidator ID="ReasonValidator" ControlToValidate="txtReason" Display="Dynamic"
                        ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                        ErrorMessage="<%$ Resources:ErrorMessages, RequiredAmount%>" runat="server" />
                    <ajax:FilteredTextBoxExtender ID="AmountValidatorExtender" runat="server" TargetControlID="txtReason"
                        FilterType="Custom, UppercaseLetters, LowercaseLetters" FilterMode="ValidChars"
                        ValidChars=" " />
                    <ajax:ValidatorCalloutExtender ID="ReasonValidatorCalloutExtender" runat="server"
                        TargetControlID="ReasonValidator" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblReason" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                <EditItemTemplate>
                    <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                        Text="<%$Resources:Labels, Update%>" Font-Underline="False" ValidationGroup="SaveGroup" />
                    <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="<%$Resources:Labels, RETURN%>" Font-Underline="False" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="<%$Resources:Labels, Cancel%>" Font-Underline="false" CommandArgument='<%#Bind("PC_Id") %>' />
                    <asp:LinkButton ID="lnkReIssue" runat="server" CausesValidation="False" CommandName="ReIssue"
                        Text="<%$Resources:Labels, REISSUE%>" Font-Underline="false" CommandArgument='<%#Bind("PC_Id") %>' />
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
<div style="clear: both; height: 20px">
</div>
<div>
    <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
</div>
