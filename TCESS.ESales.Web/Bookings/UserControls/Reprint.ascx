<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Reprint.ascx.cs" Inherits="Bookings_UserControls_Reprint" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<table style="overflow: auto;" width="100%;">
    <tr>
        <td align="left" valign="top">
            <asp:TextBox ID="txtBookingID" Width="100px" runat="server"></asp:TextBox>
            <asp:Button ID="btnPopulateGrd" CssClass="button" runat="server" Text="Validate"
                OnClick="btnPopulateGrd_Click" />
        </td>
    </tr>
    <tr>
        <td align="center">
            <Custom:GridViewAlwaysShow ID="grdReprint" runat="server" AutoGenerateColumns="False"
                DataKeyNames="Booking_Id" AllowPaging="true" PageSize="10" BorderColor="#3366CC"
                BorderStyle="Solid" BorderWidth="1px" Font-Size="Small" Width="100%" HorizontalAlign="Center"
                CellPadding="5" OnRowCommand="grdReprint_RowCommand" 
                onrowdatabound="grdReprint_RowDataBound">
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
                    <asp:TemplateField HeaderText="Print Money Receipt">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButtonPrintMoney" CommandName="IssueMoneyReceipt" CommandArgument='<%#Bind("Booking_Id") %>' runat="server" Text="Print"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Handle Bill">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButtonHandleBill" CommandName="PrintBill" CommandArgument='<%#Bind("Booking_Id") %>' runat="server" Text="Print"></asp:LinkButton>
                        </ItemTemplate>
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
