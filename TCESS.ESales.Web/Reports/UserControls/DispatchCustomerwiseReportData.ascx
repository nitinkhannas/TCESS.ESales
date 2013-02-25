<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DispatchCustomerwiseReportData.ascx.cs"
    Inherits="Reports_UserControls_DispatchCustomerwiseReportData" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>

<script type ="text/javascript" language ="javascript">
    function ValidateFilter(id) {
        var ddlfilter = document.getElementById('<%=ddlFilter.ClientID%>');
        var txtfilter = document.getElementById('<%=txtFilter.ClientID%>');
        var filterval = ddlfilter.options[ddlfilter.selectedIndex].text;

        if (filterval == "Select to Filter" && txtfilter.value == "" && id == "btnGenerate") {
            alert("Customer Code or Truck No has to be selected to continue");
            ddlfilter.focus();
            return false;
        }
        else {
            if (filterval == "Customer Code" && txtfilter.value == "") {
                alert("Customer Code cannot be left blank");
                txtfilter.focus();
                return false;
            }
            else if (filterval == "Truck No." && txtfilter.value == "") {
                alert("Truck No. cannot be left blank");
                txtfilter.focus();
                return false;
            }
            else {
                return true;
            }
        }
    }
</script>
<table width="100%" cellspacing="5" cellpadding="5">
    <tr align="left">
        <td colspan="5">
            <asp:CheckBox ID="chkDateRange" runat="server" Text="<%$Resources:Labels, EnableMultiDateSelection%>"
                OnCheckedChanged="chkDateRange_CheckedChanged" AutoPostBack="true" />
        </td>
        <td>
            <asp:Label ID="lblFilter" runat="server" Text="Select" />
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlFilter" CssClass="listmenu" ClientIDMode ="Static">
                <asp:ListItem Selected="True" Text="Select to Filter" Value="0" />
                <asp:ListItem Text="Customer Code" Value="1" />
                <asp:ListItem Text="Truck No." Value="2" />
            </asp:DropDownList>
        </td>
        <td>
            <asp:TextBox ID="txtFilter" runat="server" Wrap="False" CssClass="textbox" MaxLength="15" ClientIDMode ="Static"
                onkeypress="return runScript(event)" />
            <%--<asp:RequiredFieldValidator ID="TruckNumberValidator" ControlToValidate="txtFilter"
                Display="Dynamic" ValidationGroup="GenerateGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="TextBox Cannot be left Blank"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="TruckNumberValidatorCallout" runat="server" TargetControlID="TruckNumberValidator" />--%>
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="lblFromDate" runat="server" Text="<%$Resources:Labels, FromDate%>" />
        </td>
        <td>
            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" Enabled="false" />
            <ajax:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFromDate"
                Format="dd-MMM-yyyy" />
            <ajax:TextBoxWatermarkExtender ID="FromDate_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtFromDate" WatermarkCssClass="watermark" WatermarkText="<%$Resources:Labels, SelectDate%>" />
            <asp:RequiredFieldValidator ID="txtFromDateValidator" ControlToValidate="txtFromDate"
                Display="Dynamic" ValidationGroup="GenerateGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredFromDate %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="txtFromDateValidatorCalloutExtender" runat="server"
                TargetControlID="txtFromDateValidator" />
        </td>
        <td>
            <asp:Label ID="lblToDate" runat="server" Text="<%$Resources:Labels, ToDate%>" />
        </td>
        <td>
            <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" Enabled="false" />
            <ajax:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtToDate"
                Format="dd-MMM-yyyy" />
            <ajax:TextBoxWatermarkExtender ID="ToDate_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtToDate" WatermarkCssClass="watermark" WatermarkText="<%$Resources:Labels, SelectDate%>" />
            <asp:RequiredFieldValidator ID="txtToDateValidator" ControlToValidate="txtToDate"
                Display="Dynamic" ValidationGroup="GenerateGroup" SetFocusOnError="true" Text="*"
                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredToDate %>"
                runat="server" />
            <ajax:ValidatorCalloutExtender ID="txtToDateValidatorCalloutExtender" runat="server"
                TargetControlID="txtToDateValidator" />
        </td>
        <td align="right">
            &nbsp;
        </td>
        <td align="right">
            <asp:Button ID="btnGenerate" runat="server" OnClientClick="return ValidateFilter('btnGenerate');"
                Text="<%$Resources:Labels, Generate%>" CssClass="button" ValidationGroup="GenerateGroup"
                OnClick="btnGenerate_Click" />
        </td>
        <td>
            <asp:Button ID="btnPrint" runat="server" OnClientClick="return ValidateFilter('btnPrint');"
                Text="<%$Resources:Labels, Print%>" CssClass="button" OnClick="btnPrint_Click"
                ValidationGroup="GenerateGroup" />
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <Custom:GridViewAlwaysShow ID="grdDispatch" runat="server" AutoGenerateColumns="False"
                BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                AllowPaging="false" HorizontalAlign="Center" Width="100%" CellPadding="5">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, Date%>">
                        <ItemTemplate>
                             <%#Convert.ToDateTime(Eval("Booking_Date")).ToString("dd-MM-yyyy")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Loading Adv No">
                        <ItemTemplate>
                            <%# Eval("LoadingAdvNo")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TSL Inv No">
                        <ItemTemplate>
                            <%# Eval("TSLInvNo")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Truck No">
                        <ItemTemplate>
                            <%# Eval("TruckNo")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cust Code">
                        <ItemTemplate>
                            <%# Eval("CustCode")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer Name">
                        <ItemTemplate>
                            <%# Eval("UnitName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="District">
                        <ItemTemplate>
                            <%# Eval("District")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty LiftedMts">
                        <ItemTemplate>
                            <%# Eval("QtyLiftedMts")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TSL Amount">
                        <ItemTemplate>
                            <%# Math.Round(Convert.ToDecimal(Eval("TSLAmount")),2) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DCA Bill Handling">
                        <ItemTemplate>
                            <%#  Math.Round(Convert.ToDecimal(Eval("DCABillHandling")), 2)%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Service Tax">
                        <ItemTemplate>
                            <%#  Math.Round(Convert.ToDecimal(Eval("ServiceTax")), 2)%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="E Cess 2 %">
                        <ItemTemplate>
                            <%#  Math.Round(Convert.ToDecimal(Eval("ECess2")), 2)%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="HE Cess 1%">
                        <ItemTemplate>
                            <%#  Math.Round(Convert.ToDecimal(Eval("HECess1")), 2)%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Stax">
                        <ItemTemplate>
                            <%# Math.Round(Convert.ToDecimal(Eval("ServiceTax")), 2) + Math.Round(Convert.ToDecimal(Eval("ECess2")), 2) + Math.Round(Convert.ToDecimal(Eval("HECess1")), 2)%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Amt Due">
                        <ItemTemplate>
                            <%# Math.Round(Convert.ToDecimal(Eval("ServiceTax")), 2) + Math.Round(Convert.ToDecimal(Eval("ECess2")), 2) + Math.Round(Convert.ToDecimal(Eval("HECess1")), 2) + Math.Round(Convert.ToDecimal(Eval("DCABillHandling")), 2) + Math.Round(Convert.ToDecimal(Eval("TSLAmount")), 2)%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Advance Received">
                        <ItemTemplate>
                            <%#  Math.Round(Convert.ToDecimal(Eval("AdvanceReceived")), 2)%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Balance in Account">
                        <ItemTemplate>
                            <%# (Math.Round(Convert.ToDecimal(Eval("ServiceTax")), 2) + Math.Round(Convert.ToDecimal(Eval("ECess2")), 2) + Math.Round(Convert.ToDecimal(Eval("HECess1")), 2) + Math.Round(Convert.ToDecimal(Eval("DCABillHandling")), 2) + Math.Round(Convert.ToDecimal(Eval("TSLAmount")), 2)) - Math.Round(Convert.ToDecimal(Eval("AdvanceReceived")), 2)%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Full Amount Cases">
                        <ItemTemplate>
                            <%#Eval("Booking_IsFullAmount")%>
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
