<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Form27CReportData.ascx.cs"
    Inherits="Reports_UserControls_Form27CReportData" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<table width="100%" cellspacing="5" cellpadding="5">
    <tr align="left">
        <td colspan="5">
            <asp:CheckBox ID="chkDateRange" runat="server" Text="<%$Resources:Labels, EnableMultiDateSelection%>"
                OnCheckedChanged="chkDateRange_CheckedChanged" AutoPostBack="true" />
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
            <asp:Button ID="btnGenerate" runat="server" Text="<%$Resources:Labels, Generate%>"
                CssClass="button" ValidationGroup="GenerateGroup" OnClick="btnGenerate_Click" />
            &nbsp;
            <asp:Button ID="btnPrint" runat="server" Text="<%$Resources:Labels, Print%>" CssClass="button"
                OnClick="btnPrint_Click" ValidationGroup="GenerateGroup" />
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <Custom:GridViewAlwaysShow ID="grdForm27C" runat="server" AutoGenerateColumns="false"
                BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                AllowPaging="False" HorizontalAlign="Center" Width="100%" CellPadding="5">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer Code" HeaderStyle-Width="80px">
                        <ItemTemplate>
                            <%# Eval("Cust_code")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer Name" HeaderStyle-Width="150px">
                        <ItemTemplate>
                            <%# Eval("Customer_Name")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="District" HeaderStyle-Width="150px">
                        <ItemTemplate>
                            <%# Eval("Name_of_District")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PAN Number" HeaderStyle-Width="150px">
                        <ItemTemplate>
                            <%# Eval("PAN")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Received Date" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                        <%#Eval("Received_Date", "{0:dd/MM/yyyy}")%>                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TSL Accepted Date">
                        <ItemTemplate>
                        <%#Eval("Accepted_Date", "{0:dd/MM/yyyy}")%>                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valid Month">
                        <ItemTemplate>
                            <%# Eval("Valid_Month")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valid Year">
                        <ItemTemplate>
                            <%# Eval("Valid_Year")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Number Of Trips">
                        <ItemTemplate>
                            <%# Eval("Trips")%>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="Quantity Lifted">
                        <ItemTemplate>
                            <%# Eval("Quantity")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TSL Value">
                        <ItemTemplate>
                            <%# Eval("TSL_Value")%>
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
