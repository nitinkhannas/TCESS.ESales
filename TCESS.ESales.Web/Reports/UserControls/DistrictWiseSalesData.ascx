<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DistrictWiseSalesData.ascx.cs"
    Inherits="Reports_UserControls_DistrictWiseSalesData" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<table width="100%" cellspacing="5" cellpadding="5">
    <tr align="left">
        <td>
            <asp:Label ID="lblDCAName" runat="server" Text="<%$Resources:Labels, DCAName%>" />
        </td>
        <td colspan="4">
            <asp:Label ID="lblAgentName" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr align="left">
        <td>
            Month
        </td>
        <td>
            <asp:DropDownList ID="ddlMonth" runat="server" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem Text="January" Value="1" Selected="true"></asp:ListItem>
                <asp:ListItem Text="February" Value="2" Selected="false"></asp:ListItem>
                <asp:ListItem Text="March" Value="3" Selected="false"></asp:ListItem>
                <asp:ListItem Text="April" Value="4" Selected="false"></asp:ListItem>
                <asp:ListItem Text="May" Value="5" Selected="false"></asp:ListItem>
                <asp:ListItem Text="June" Value="6" Selected="false"></asp:ListItem>
                <asp:ListItem Text="July" Value="7" Selected="false"></asp:ListItem>
                <asp:ListItem Text="August" Value="8" Selected="false"></asp:ListItem>
                <asp:ListItem Text="September" Value="9" Selected="false"></asp:ListItem>
                <asp:ListItem Text="October" Value="10" Selected="false"></asp:ListItem>
                <asp:ListItem Text="November" Value="11" Selected="false"></asp:ListItem>
                <asp:ListItem Text="December" Value="12" Selected="false"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Button ID="btnGenerate" runat="server" Text="<%$Resources:Labels, Generate%>"
                CssClass="button" ValidationGroup="GenerateGroup" OnClick="btnGenerate_Click" />
            &nbsp;
            <asp:Button ID="btnPrint" runat="server" Text="<%$Resources:Labels, Print%>" CssClass="button"
                OnClick="btnPrint_Click" Width="55px" ValidationGroup="LoadingAdvRpt" />
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <Custom:GridViewAlwaysShow ID="grdDistrictWiseSales" runat="server" AutoGenerateColumns="false"
                BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                AllowPaging="false"  HorizontalAlign="Center" Width="100%" CellPadding="5">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name of District">
                        <ItemTemplate>
                            <%# Eval("SalesReport_District")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Trips(pri.month)">
                        <ItemTemplate>
                            <%# Eval("SalesReport_Pre_Trip")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty(pri.month)">
                        <ItemTemplate>
                            <%# Eval("SalesReport_Pre_Qty")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Trips(Selected month)">
                        <ItemTemplate>
                            <%# Eval("SalesReport_CrrMt_Trip")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty(Selected month)">
                        <ItemTemplate>
                            <%# Eval("SalesReport_CrrMt_Qty")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Trips(upto Date)">
                        <ItemTemplate>
                            <%# Eval("SalesReport_Crr_Trip")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty uptoDate">
                        <ItemTemplate>
                            <%# Eval("SalesReport_Crr_Qty")%>
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
