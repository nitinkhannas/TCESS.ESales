<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TotalMoneyReciepts.ascx.cs"
    Inherits="Bookings_UserControls_TotalMoneyReciepts" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>


 <table >
 <tr >
        <td style =" width :95px;" >
         <asp:Label ID="lblBooking" runat="server" Text="<%$Resources:Labels, TotalBooking%>" Font-Bold ="true"/>
        </td>
        <td style =" width :95px;">
         <asp:Label ID="lblCount" runat="server"  Font-Bold ="true"/>
        </td>
        <td style =" width :140px;">
          <asp:Label ID="lbltotalcash" runat="server" Text="<%$Resources:Labels,TotalCashCollected%>" Font-Bold ="true"/>
        </td>
        <td>
        <asp:Label ID="lblTotalAmount" runat="server"  Font-Bold ="true"/>       
        </td>
        </tr> 

 </table>
<table style="overflow: auto;" width="100%;">   
    <tr>
        <td align="center" colspan ="5" >
            <Custom:GridViewAlwaysShow ID="grdTotalMoneyReceipts" runat="server" AutoGenerateColumns="False"
                DataKeyNames="Booking_Id" AllowPaging="false" PageSize="10" BorderColor="#3366CC"
                BorderStyle="Solid" BorderWidth="1px" Font-Size="Small" Width="100%" HorizontalAlign="Center"
                CellPadding="5" OnRowCommand="grdTotalMoneyReceipts_RowCommand" ShowFooter="true">
                <EmptyDataTemplate>
                    <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, BookingNo%>">
                        <ItemTemplate>
                            <%# Eval("Booking_Agent_AgentShortName")%>-<%# Eval("Booking_Id")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, TruckNo%>">
                        <ItemTemplate>
                            <%#Convert.ToInt32(Eval("Booking_TruckType")) == 1 ? Eval("Booking_StandaloneTruck_RegNo") : Eval("Booking_Truck_RegNo")%>
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
                    <asp:TemplateField HeaderText="<%$Resources:Labels, Qty%>">
                        <ItemTemplate>
                            <%#Eval("Booking_Qty")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, BookingAdvance%>">
                        <ItemTemplate>
                            <%#Eval("Booking_AdvanceAmount")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkGenerate" runat="server" CausesValidation="False" CommandName="IssueMoneyReceipt"
                                Text="<%$Resources:Labels, Generate%>" Font-Underline="False" CommandArgument='<%#Bind("Booking_Id") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="lnkRefresh" runat="server" CausesValidation="true" CommandName="Refresh"
                                Text="<%$Resources:Labels, Refresh%>" CssClass="button" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#397dbc" ForeColor="#FFFFFF" HorizontalAlign="Center" />
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
