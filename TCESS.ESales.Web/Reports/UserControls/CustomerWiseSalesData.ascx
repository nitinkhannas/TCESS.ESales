<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerWiseSalesData.ascx.cs" Inherits="Reports_UserControls_CustomerWiseSalesData" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<script type ="text/javascript" language ="javascript">
    function ValidateDropdown() {
        var date = new Date()
        var yearControl = document.getElementById('<%=ddlyear.ClientID%>');
        var monthControl = document.getElementById('<%=ddlMonth.ClientID%>');
        var monthData = monthControl.options[monthControl.selectedIndex].value;
        var yearData = yearControl.options[yearControl.selectedIndex].value;
        if (yearData > date.getFullYear()) {
            alert("Year Cannot be greater than current year");
            return false;
        }
        else if (yearData == date.getFullYear()) {
        var currentMonth = date.getMonth() + 1;
        if (monthData > currentMonth) {
                alert("Month cannot be greater than current month");
                return false;
            }
            else {
                return true;
            }
        }
        else {
            return true;
        }
    }
 
</script>

<table width="100%" cellspacing="5" cellpadding="5">    
    <tr align="left">
        <td>
            Month
        </td>
        <td>
            <asp:DropDownList ID="ddlMonth"  runat="server" ClientIDMode ="Static">
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
          <td>
            Year
        </td>
          <td>
            <asp:DropDownList ID="ddlyear" runat="server" ClientIDMode ="Static" >
                <asp:ListItem Text="2011" Value="2011" Selected="true"></asp:ListItem>
                <asp:ListItem Text="2012" Value="2012" Selected="false"></asp:ListItem>
                <asp:ListItem Text="2013" Value="2013" Selected="false"></asp:ListItem>
                <asp:ListItem Text="2014" Value="2014" Selected="false"></asp:ListItem>
                <asp:ListItem Text="2015" Value="2015" Selected="false"></asp:ListItem>
                <asp:ListItem Text="2016" Value="2016" Selected="false"></asp:ListItem>
                <asp:ListItem Text="2017" Value="2017" Selected="false"></asp:ListItem>
                <asp:ListItem Text="2018" Value="2018" Selected="false"></asp:ListItem>
                <asp:ListItem Text="2019" Value="2019" Selected="false"></asp:ListItem>
                <asp:ListItem Text="2020" Value="2020" Selected="false"></asp:ListItem>
                <asp:ListItem Text="2021" Value="2021" Selected="false"></asp:ListItem>
                <asp:ListItem Text="2022" Value="2022" Selected="false"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Button ID="btnGenerate" runat="server"  Text="<%$Resources:Labels, Generate%>"
                CssClass="button"  OnClientClick = "javascript:return ValidateDropdown();" OnClick="btnGenerate_Click" />
            &nbsp;
            <asp:Button ID="btnPrint" runat="server"  Text="<%$Resources:Labels, Print%>" CssClass="button"
                OnClientClick = "javascript:return ValidateDropdown();" OnClick="btnPrint_Click" Width="55px" />
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <Custom:GridViewAlwaysShow ID="grdDistrictWiseSales" runat="server" AutoGenerateColumns="false"
                BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                AllowPaging="False" HorizontalAlign="Center" Width="100%" CellPadding="5">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer Code">
                        <ItemTemplate>
                            <asp:HyperLink runat="server" Text='<%# Eval("CustomerCode")%>' NavigateUrl='<%# "~/Reports/DispatchCustomerwiseReport.aspx?custcode="+ Eval("CustomerCode")%>'>
                            </asp:HyperLink>
                            <%--<%# Eval("CustomerCode")%>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer Name">
                        <ItemTemplate>
                            <%# Eval("CustomerName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name of District">
                        <ItemTemplate>
                            <%# Eval("NameofDistrict")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Annual Qty Requirement">
                        <ItemTemplate>
                            <%# Eval("AnnualQtyRequirement")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty Limits">
                        <ItemTemplate>
                            <%# Eval("QtyLimits")%>
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
                    <asp:TemplateField HeaderText="Balance Qty">
                        <ItemTemplate>                            
                            <%#Convert.ToInt32(Eval("QtyLimits")) - Convert.ToInt32(Eval("SalesReport_Crr_Qty"))%>
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