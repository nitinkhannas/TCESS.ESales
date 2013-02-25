<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DistrictWiseReportofInactiveCustomersData.ascx.cs" Inherits="Reports_UserControls_DistrictWiseReportofInactiveCustomersData" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<table width="100%" cellspacing="5">    
    <tr>
        <td align="right">           
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
            <Custom:GridViewAlwaysShow ID="grdDistrictWiseReportofInactiveCustomers" runat="server" AutoGenerateColumns="False"
                BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                AllowPaging="false" HorizontalAlign="Center" Width="100%" CellPadding="5">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, CustomerCode%>">
                        <ItemTemplate>
                            <%#Eval("Booking_Cust_Code")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, CustomerName%>">
                        <ItemTemplate>
                            <%#Eval("Booking_Cust_UnitName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, District%>">
                        <ItemTemplate>
                            <%#Eval("Booking_Cust_District_Name")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, State%>">
                        <ItemTemplate>
                            <%#Eval("Booking_Cust_State_Name")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, QtyLimits%>">
                        <ItemTemplate>
                            <%#Eval("Booking_Cust_Mat_AnnualRequirement")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, QtyLifted%>">
                        <ItemTemplate>
                            <%#Eval("Booking_Qty")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, BalanceQty%>">
                        <ItemTemplate>                        
                            <%#Convert.ToInt32(Eval("Booking_Cust_Mat_AnnualRequirement")) - Convert.ToInt32(Eval("Booking_Qty"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date of Last Lifting">
                        <ItemTemplate>
                            <%#Eval("Booking_Date", "{0:d}")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Date of AME Visit">
                        <ItemTemplate>
                            <%#Eval("Booking_Cust_AMEVisitDate","{0:d}")%>
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
