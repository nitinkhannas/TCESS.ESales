<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RoadPermitsUtilisationStatementForTheMonthData.ascx.cs" Inherits="Reports_UserControls_RoadPermitsUtilisationStatementForTheMonthData" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<table width="100%" cellspacing="5">
    
    <tr align="left">
        <td>Month</td>
        <td>            
            <asp:DropDownList ID="ddlMonth" runat="server" 
                onselectedindexchanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true">   
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
        <td>Year</td>
        <td>
            <asp:DropDownList ID="ddlYear" runat="server" 
                onselectedindexchanged="ddlYear_SelectedIndexChanged" AutoPostBack="true">              
            </asp:DropDownList>            
        </td>
        <td align="right">
            <asp:Button ID="btnGenerate" runat="server" Text="<%$Resources:Labels, Generate%>"
                CssClass="button" ValidationGroup="LoadingAdvRpt" OnClick="btnGenerate_Click" />
            &nbsp;
            <asp:Button ID="btnPrint" runat="server" Text="<%$Resources:Labels, Print%>" CssClass="button"
                OnClick="btnPrint_Click" Width="55px" ValidationGroup="LoadingAdvRpt" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <Custom:GridViewAlwaysShow ID="grdRoadPermitsUtilisationStatementForTheMonth" runat="server" AutoGenerateColumns="False"
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
                            <%# Eval("Account_CreatedDate")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, RoadPermittNo%>">
                        <ItemTemplate>
                            <%# Eval("Account_RoadPermitNo")%>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="<%$Resources:Labels, CustomerName%>">
                        <ItemTemplate>
                            <%#Eval("Account_Booking_Cust_UnitName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, Address%>">
                        <ItemTemplate>
                            <%#Eval("Account_Booking_Cust_UnitAddress")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, District%>">
                        <ItemTemplate>
                            <%#Eval("Account_Booking_Cust_District_Name")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, TSLInvoiceNo%>">
                        <ItemTemplate>
                            <%#Eval("Account_InvoiceNumber")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, TSLInvoiceAmount%>">
                        <ItemTemplate>                            
                            <%#Math.Round(Convert.ToDecimal(Eval("Account_TotalAmount")), 2)%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, QtyLifted%>">
                        <ItemTemplate>
                            <%#Eval("Account_Quantity")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, TruckNo%>">
                        <ItemTemplate>
                            <%#Eval("Account_Booking_Truck_RegNo")%><%#Eval("Account_Booking_StandaloneTruck_RegNo")%>
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
