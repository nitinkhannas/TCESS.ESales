<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="DayCollectionCounterSummary.aspx.cs" Inherits="Collection_DayCollectionCounterSummary" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <label class="pageNameContent">
        <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, DAYCOLLECTIONCOUNTERSUMMARY%>"
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
                        <asp:Label ID="lblCashInHand" runat="server" Text="<%$Resources:Labels, AMOUNTINHAND%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="lblCashInHandAmount" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <Custom:GridViewAlwaysShow AllowPaging="true" ID="grdPaymentTransit" runat="server"
                            AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                            Font-Size="Small" Width="100%" HorizontalAlign="Center" CellPadding="5" PageSize="13"
                            OnPageIndexChanging="grdPaymentTransit_PageIndexChanging" DataKeyNames="PC_Id"
                            OnMustAddARow="grdPaymentTransit_MustAddARow" OnRowDataBound="grdPaymentTransit_RowDataBound">
                            <EmptyDataTemplate>
                                <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="30px">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="True" OnCheckedChanged="chkHeader_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkItem" AutoPostBack="true" OnCheckedChanged="chkItem_CheckedChanged" />
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
                <tr style="clear: both;">
                    <td>
                    </td>
                </tr>
                <tr align="left">
                    <td nowrap="nowrap">
                        <asp:Label ID="lblTransit" runat="server" Text="<%$Resources:Labels, AmountToTransit%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox ID="txtAmountToTransit" runat="server" ReadOnly="true" />
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="lblComments" runat="server" Text="<%$Resources:Labels, Remarks%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox ID="txtComments" runat="server" CssClass="textbox" Wrap="False" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5" align="center">
                        <asp:CustomValidator ID="customValidator" Display="Dynamic" runat="server" CssClass="failureNotification" />
                        <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="<%$Resources:Labels, SubmitPayment%>"
                            OnClick="btnSubmit_Click" ValidationGroup="SaveGroup" />
                    </td>
                </tr>
            </table>
            <div style="clear: both; height: 10px">
            </div>
            <div>
                <Custom:GridViewAlwaysShow AllowPaging="true" ID="grdBatchPayments" runat="server"
                    AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                    Font-Size="Small" Width="100%" HorizontalAlign="Center" CellPadding="5" PageSize="13"
                    OnPageIndexChanging="grdBatchPayments_PageIndexChanging" DataKeyNames="BT_Id"
                    OnMustAddARow="grdBatchPayments_MustAddARow">
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, RITNo%>">
                            <ItemTemplate>
                                <asp:Label ID="lblFirmName" runat="server" Text='<%# Bind("BT_Id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Date%>">
                            <ItemTemplate>
                                <asp:Label ID="lblPayDate" runat="server" Text='<%#Convert.ToDateTime(Eval("BT_CreatedDate")).ToString("dd-MMM-yyyy")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Amount%>">
                            <ItemTemplate>
                                <asp:Label ID="lblOwnerName" runat="server" Text='<%# Bind("Amount") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, STATUS%>">
                            <ItemTemplate>
                                <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("BT_Status").ToString() == "1" ? "Pending": "Accepted" %>' />
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
            <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
