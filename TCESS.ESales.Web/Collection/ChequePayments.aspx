<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ChequePayments.aspx.cs" Inherits="GhatoCollection_ChequePayments" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <label class="pageNameContent">
        <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, CHEQUEPAYMENTS%>"
            CssClass="pageNameContent" />
    </label>
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
            <table width="100%" cellspacing="10" cellpadding="5">
                <tr align="left">
                    <td nowrap="nowrap">
                        <asp:Label ID="lblTotalAmount" runat="server" Text="<%$Resources:Labels, TotalAmountCollected%>" />
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
                        <asp:Label ID="lblCashInHand" runat="server" Text="<%$Resources:Labels, CHEQUEAMOUNTINHAND%>" />
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
            <div style="overflow: auto; width: 100%;">
                <Custom:GridViewAlwaysShow ID="grdConsolidatedReport" runat="server" AutoGenerateColumns="False"
                    BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                    Width="100%" HorizontalAlign="Center" CellPadding="5" DataKeyNames="CounterId">
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, CounterName%>">
                            <ItemTemplate>
                                <asp:Label ID="lblCounterNumber" runat="server" Text='<%# Bind("CounterName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, TOTALCHEQUECOUNT%>">
                            <ItemTemplate>
                                <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Bind("TotalChequeCount") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, TotalAmountCollected%>" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Bind("TotalAmount") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, TransferredAmount%>" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblTransferredAmount" runat="server" Text='<%# Bind("TransferredAmount") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, CHEQUESCLEARED%>" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblChequesCleared" runat="server" Text='<%# Bind("ChequesCleared") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, CHEQUESBOUNCED%>" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblChequesBounced" runat="server" Text='<%# Bind("ChequesBounced") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:TemplateField HeaderText="<%$Resources:Labels, InTransitAmount%>" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblInTransitAmount" runat="server" Text='<%# Bind("InTransitAmount") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, CHEQUEAMOUNTINHAND%>" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblCashInHand" runat="server" Text='<%# Bind("CashInHand") %>' />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
