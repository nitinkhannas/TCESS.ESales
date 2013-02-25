<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Form27C.aspx.cs" Inherits="Administrator_Form27C" ValidateRequest="false" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="Form 27C" CssClass="pageNameContent" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <script language="javascript" type="text/javascript">
        function ChangeUpdateProgress(message) {
            document.getElementById('lblProgress').innerText = message;
        }
    </script>
    <asp:UpdateProgress ID="progressBar" runat="server" AssociatedUpdatePanelID="uplMainPanel"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="overlay">
                <div id="div1" runat="server" class="ajaxloader">
                    <img src='<%= ResolveClientUrl("~/Images/ajax-loader.gif")%>' style="vertical-align: middle"
                        alt="Processing" />
                    <asp:Label ID="lblProgress" ClientIDMode="Static" Text="Processing..." runat="server">
                    </asp:Label>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="uplMainPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" cellspacing="10" cellpadding="5">
                <tr align="left">
                    <td nowrap="nowrap">
                        <asp:Label ID="Label3" runat="server" Text="<%$Resources:Labels, CustomerCode%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="textbox" MaxLength="15"
                            Wrap="False" />
                        <asp:RequiredFieldValidator ID="RFVtxtCustomerCode" ControlToValidate="txtCustomerCode"
                            Display="Dynamic" ValidationGroup="Customer" SetFocusOnError="true" Text="*"
                            CssClass="failureNotification" ErrorMessage="Enter Customer Code" runat="server" />
                        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RFVtxtCustomerCode" />
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="Label4" runat="server" Text="<%$Resources:Labels, PAN%>" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox ID="txtPANNo" runat="server" CssClass="textbox" Wrap="False" />
                        <asp:RequiredFieldValidator ID="RFVtxtPANNo" ControlToValidate="txtPANNo" Display="Dynamic"
                            ValidationGroup="Customer" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                            ErrorMessage="Enter PAN Number" runat="server" />
                        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RFVtxtPANNo" />
                        <asp:Button ID="validate" runat="server" CssClass="button" Text="Validate" ValidationGroup="Customer"
                            OnClick="validate_Click" />
                    </td>
                </tr>
            </table>
            <div style="overflow: auto; width: 100%;">
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
                        <asp:TemplateField HeaderText="District Name">
                            <ItemTemplate>
                                <asp:Label ID="lblMobileNo" runat="server" Text='<%# Bind("Cust_District_Name") %>' />
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
            <table width="100%" cellspacing="10" cellpadding="5">
                <tr align="left">
                    <td nowrap="nowrap">
                        <asp:Label ID="lblAffidavit" runat="server" Text="Affidavit :" />
                    </td>
                    <td nowrap="nowrap">
                        <table align="left">
                            <tr>
                                <td align="left">
                                    <asp:RadioButtonList ID="rdAffidavit" runat="server" RepeatDirection="Horizontal"
                                        CssClass="radioButtons" OnSelectedIndexChanged="rdAffidavit_SelectedIndexChanged"
                                        AutoPostBack="true" Enabled="false">
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="lblApprovedBy" runat="server" Text="Affidavit Approved By:" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox ID="txtApprovedBy" runat="server" CssClass="textbox" Wrap="False" Enabled="false" />
                        <asp:RequiredFieldValidator ID="RFVtxtApprovedBy" ControlToValidate="txtApprovedBy"
                            Display="Dynamic" ValidationGroup="Affidavit" SetFocusOnError="true" Text="*"
                            CssClass="failureNotification" ErrorMessage="Enter Approved By" runat="server" />
                        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RFVtxtApprovedBy" />
                    </td>
                </tr>
                <tr align="left">
                    <td>
                        <asp:Label ID="lblReceivedDate" runat="server" Text="<%$Resources:Labels, ReceivedDate%>" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtReceivedDate" runat="server" CssClass="textbox" Enabled="false" />
                        <ajax:CalendarExtender ID="AMEVisitDateCalendarExtender" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="checkDate"
                            runat="server" TargetControlID="txtReceivedDate" />
                        <ajax:TextBoxWatermarkExtender ID="txtReceivedDate_TextBoxWatermarkExtender" runat="server"
                            Enabled="True" TargetControlID="txtReceivedDate" WatermarkCssClass="watermark"
                            WatermarkText="Click to select date">
                        </ajax:TextBoxWatermarkExtender>
                        <asp:RequiredFieldValidator ID="AMEVisitDateValidator" ControlToValidate="txtReceivedDate"
                            Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                            CssClass="failureNotification" ErrorMessage="Enter Received Date" runat="server" />
                        <ajax:ValidatorCalloutExtender ID="AMEVisitDateValidatorCalloutExtender" runat="server"
                            TargetControlID="AMEVisitDateValidator" />
                    </td>
                    <td nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td align="left">
                        <asp:Label ID="Label1" runat="server" Text="Rejection Reason" />
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlRejectionReason" runat="server" AutoPostBack="false" CssClass="listmenu"
                            OnSelectedIndexChanged="ddlRejectionReason_SelectedIndexChanged" Enabled="false">
                            <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                            <asp:ListItem Value="1">PAN not valid</asp:ListItem>
                            <asp:ListItem Value="2">SIGN not valid</asp:ListItem>
                            <%--<asp:ListItem Value="3">Customer not valid</asp:ListItem>--%>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr align="left">
                    <td>
                        <asp:Label ID="lblTSLAcceptedDate" runat="server" Text="<%$Resources:Labels, DateAcceptedByTSL%>"
                            Visible="false" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtTSLAcceptedDate" runat="server" CssClass="textbox" Enabled="false"
                            Visible="false" OnTextChanged="txtTSLAcceptedDate_TextChanged" onkeypress="javascript:__doPostBack('txtTSLAcceptedDate','')" />
                        <ajax:CalendarExtender ID="CalendarExtender1" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="checkDate"
                            runat="server" TargetControlID="txtTSLAcceptedDate" />
                        <%--<ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" Enabled="True"
                            TargetControlID="txtTSLAcceptedDate" WatermarkCssClass="watermark" WatermarkText="Click to select date">
                        </ajax:TextBoxWatermarkExtender>
                        <asp:RequiredFieldValidator ID="RFVTSLAcceptedDate" ControlToValidate="txtTSLAcceptedDate"
                            Display="Dynamic" ValidationGroup="SaveGroup" SetFocusOnError="true" Text="*"
                            CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredAMEVisitDate %>"
                            runat="server" />
                        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RFVTSLAcceptedDate" />--%>
                    </td>
                </tr>
                <tr align="left">
                    <td>
                        <asp:Label ID="lblValidMonth" runat="server" Text="<%$Resources:Labels, MonthForWhichValid%>" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="false" CssClass="listmenu"
                            Enabled="false" />
                        <asp:RequiredFieldValidator ID="ddlMonthValidator" runat="server" ControlToValidate="ddlMonth"
                            CssClass="failureNotification" Display="Dynamic" ErrorMessage="Select Month"
                            InitialValue="Select" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup" />
                        <ajax:ValidatorCalloutExtender ID="ddlMonthValidatorCallOutExtender" runat="server"
                            TargetControlID="ddlMonthValidator" />
                    </td>
                </tr>
                <tr align="left">
                    <td nowrap="nowrap">
                        <asp:CheckBox ID="chkSignValid" runat="server" Enabled="false" Text="Customer Validated" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:Button ID="btnSign" Enabled="false" runat="server" CssClass="button" OnClick="btnSign_Click"
                            Text="<%$Resources:Labels, ViewSignature%>" />
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
                <tr>
                    <td colspan="5" align="center">
                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Submit" OnClick="btnSave_Click"
                            ValidationGroup="SaveGroup" Enabled="false" />
                        &nbsp;<asp:Button ID="btnReject" CssClass="button" runat="server" Text="<%$Resources:Labels, Reject%>"
                            OnClick="btnReject_Click" OnClientClick="ChangeUpdateProgress('Sending..')" Enabled="false" />
                        &nbsp;<asp:Button ID="btnReset" CssClass="button" runat="server" Text="<%$Resources:Labels, Reset%>"
                            OnClick="btnReset_Click" />
                        &nbsp;<asp:Button ID="btnAffidavit" CssClass="button" runat="server" Text="Save Affidavit"
                            OnClick="btnAffidavit_Click" Enabled="false" ValidationGroup="Affidavit" />
                    </td>
                </tr>
            </table>
            <div>
                <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
            </div>
            <div runat="server" id="divHistory">
                <asp:Label ID="lblHistory" Visible="false" runat="server" Text="<%$Resources:Labels, History%>"
                    CssClass="pageNameContent" />
                <Custom:GridViewAlwaysShow AllowPaging="true" ID="gridHistory" runat="server" AutoGenerateColumns="False"
                    BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                    PageSize="10" Width="100%" HorizontalAlign="Center" CellPadding="5" ShowFooter="True">
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Form Received Date">
                            <ItemTemplate>
                                <asp:Label ID="lblReceivedDate" runat="server" Text='<%#Eval("ReceivedDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Accepted By TSL Date">
                            <ItemTemplate>
                                <asp:Label ID="lblAcceptedByTSLDate" runat="server" Text='<%#Eval("AcceptedByTSLDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valid Month">
                            <ItemTemplate>
                                <%#Eval("ValidMonth")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valid Year">
                            <ItemTemplate>
                                <%#Eval("ValidYear")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#397dbc" ForeColor="#003399" HorizontalAlign="Center" />
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
            <uc1:MessageBox ID="ucMessageBox" runat="server" />
            <uc3:ViewImage ID="ucViewImage" runat="server" />
            <asp:HiddenField ID="desd" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
