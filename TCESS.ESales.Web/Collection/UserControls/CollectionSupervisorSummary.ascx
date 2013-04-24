<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CollectionSupervisorSummary.ascx.cs"
    Inherits="Collection_UserControls_CollectionSupervisorSummary" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<table width="100%" cellspacing="10" cellpadding="5">
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="lblTotalAmount" runat="server" Text="<%$Resources:Labels, TotalCashCollected%>" />
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblTotalAmountCollected" runat="server" />
        </td>
        <td nowrap="nowrap">
            &nbsp;
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblTransferred" runat="server" Text="<%$Resources:Labels, TransferredAmount%>" />
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblTransferredAmount" runat="server" />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="lblInTransit" runat="server" Text="<%$Resources:Labels, InTransitAmount%>" />
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblInTransitAmount" runat="server" />
        </td>
        <td nowrap="nowrap">
            &nbsp;
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblCashInHand" runat="server" Text="<%$Resources:Labels, COUNTERBALANCE%>" />
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblCashInHandAmount" runat="server" />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="lblTransactions" runat="server" Text="<%$Resources:Labels, TotalTransactions%>" />
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblTotalTransactions" runat="server" />
        </td>
        <td nowrap="nowrap">
            &nbsp;
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblPendingTransactions" runat="server" Text="<%$Resources:Labels, PendingTransactionsAtCounter%>" />
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblPendingTransactionsAtCounter" runat="server" />
        </td>
    </tr>
</table>
<div id="divCollection" runat="server" visible="false">
    <Custom:GridViewAlwaysShow AllowPaging="true" ID="grdCollection" runat="server" AutoGenerateColumns="False"
        BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
        Width="100%" HorizontalAlign="Center" CellPadding="5" PageSize="13" DataKeyNames="Id"
        OnMustAddARow="grdCollection_MustAddARow">
        <EmptyDataTemplate>
            <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
        </EmptyDataTemplate>
        <Columns>            
            <asp:TemplateField HeaderText="<%$Resources:Labels, DDCOLLECTED%>">
                <ItemTemplate>
                    <asp:Label ID="lblDDCollected" runat="server" Text='<%# Bind("DDCollected") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, ChequeCollected%>">
                <ItemTemplate>
                    <asp:Label ID="lblChequeCollected" runat="server" Text='<%# Bind("ChequeCollected") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, RTGSCollected%>">
                <ItemTemplate>
                    <asp:Label ID="lblRTGSCollected" runat="server" Text='<%# Bind("RTGSCollected") %>' />
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
<div style="clear: both; height: 20px" runat="server" id="divAfterCollection" visible="false">
</div>
<div>
    <Custom:GridViewAlwaysShow AllowPaging="true" ID="grdBatchPayments" runat="server"
        AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
        Font-Size="Small" Width="100%" HorizontalAlign="Center" CellPadding="5" PageSize="13"
        OnPageIndexChanging="grdBatchPayments_PageIndexChanging" DataKeyNames="BT_Id"
        OnMustAddARow="grdBatchPayments_MustAddARow" OnRowCommand="grdBatchPayments_RowCommand">
        <EmptyDataTemplate>
            <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, BatchNo%>">
                <ItemTemplate>
                    <asp:Label ID="lblFirmName" runat="server" Text='<%# Bind("BT_Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, Date%>">
                <ItemTemplate>
                    <asp:Label ID="lblPayDate" runat="server" Text='<%#Convert.ToDateTime(Eval("BT_CreatedDate")).ToString("dd-MMM-yyyy")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, CounterName%>">
                <ItemTemplate>
                    <asp:Label ID="lblOwnerName" runat="server" Text='<%# Bind("CounterName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, Amount%>" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblDistrict" runat="server" Text='<%# Bind("Amount") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkViewDetails" runat="server" CausesValidation="False" CommandName="View"
                        Text="<%$Resources:Labels, View%>" Font-Underline="False" CommandArgument='<%#Bind("BT_Id") %>' />
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
<table width="100%" id="divPaymentCollectionLabel" runat="server" visible="false">
    <tr align="left">
        <td>
            <asp:Label ID="lblPaymentCollection" runat="server" Text="<%$Resources:Labels, PAYMENTCOLLECTIONFORBATCH%>" />
            <asp:Label ID="lblBatchId" runat="server" />
        </td>
    </tr>
</table>
<div>
    <Custom:GridViewAlwaysShow ID="grdPaymentDetails" runat="server" AutoGenerateColumns="False"
        BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
        DataKeyNames="PC_Id" Width="100%" HorizontalAlign="Center" CellPadding="5" OnMustAddARow="grdPaymentDetails_MustAddARow">
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
                    <asp:Label ID="lblFirmName" runat="server" Text='<%# Bind("CustomerCode") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, CustomerName%>" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblOwnerName" runat="server" Text='<%# Bind("CustomerName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, ModeOfPayment%>">
                <ItemTemplate>
                    <asp:Label ID="lblInstrumentType" runat="server" Text='<%# Bind("PaymentModeName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, InstrumentNumber%>">
                <ItemTemplate>
                    <asp:Label ID="lblInstrumentNo" runat="server" Text='<%# Bind("PC_InstrumentNo") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, InstrumentDate%>">
                <ItemTemplate>
                    <%# Eval("PC_InstrumentDate") != null ? Eval("PC_InstrumentDate", "{0:dd/MM/yyyy}") : "NA"%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, BankName%>">
                <ItemTemplate>
                    <asp:Label ID="lblAnnualRequirement" runat="server" Text='<%# Bind("BankName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, BranchName%>">
                <ItemTemplate>
                    <asp:Label ID="lblBranchName" runat="server" Text='<%# Bind("PC_BankBranch") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, Amount%>" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblAnnualRequirement" runat="server" Text='<%# Bind("PC_Amount") %>' />
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
<table width="100%" cellspacing="10" cellpadding="5">
    <tr id="Tr1" align="left" runat="server" visible="false">
        <td nowrap="nowrap">
            <asp:Label ID="lblBankName" runat="server" Text="<%$Resources:Labels, BANKNAME%>" />
        </td>
        <td nowrap="nowrap">
            <asp:DropDownList ID="ddlBankName" runat="server" CssClass="listmenu" DataTextField="Bank_Name"
                DataValueField="Bank_Id" />
            <asp:RequiredFieldValidator ID="BankDrawnValidator" ControlToValidate="ddlBankName"
                Display="Dynamic" InitialValue="0" ValidationGroup="SaveGroup" SetFocusOnError="true"
                Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, SELECTBANKNAME%>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="BankDrawnValidatorCalloutExtender" runat="server"
                TargetControlID="BankDrawnValidator" />
        </td>
        <td nowrap="nowrap">
            &nbsp;
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblBranchName" runat="server" Text="<%$Resources:Labels, BRANCHNAME%>" />
        </td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtBranchName" runat="server" />
            <asp:RequiredFieldValidator ID="BranchNameValidator" ControlToValidate="txtBranchName"
                Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, REQUIREDBRANCHNAME%>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="BranchNameValidatorCalloutExtender" runat="server"
                TargetControlID="BranchNameValidator" />
        </td>
    </tr>
</table>
<div style="clear: both; height: 20px">
</div>
<div style="align: center;">
    <asp:Button ID="btnActivatePayment" CssClass="button" runat="server" Text="<%$Resources:Labels, ACCEPTBATCH%>"
        ValidationGroup="SaveGroup" OnClick="btnActivatePayment_Click" />
</div>
<div style="clear: both; height: 10px">
</div>
<uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
