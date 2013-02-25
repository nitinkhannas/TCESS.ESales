<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManageLoadingAdvice.aspx.cs" Inherits="Bookings_ManageLoadingAdvice" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" Runat="Server">
<asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManageAuthorizedBookings%>"
        CssClass="pageNameContent" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<table width="100%">
    <tr>
        <td align="center">
            <Custom:GridViewAlwaysShow ID="grdManageLoadingAdvice" runat="server" AutoGenerateColumns="False"
                DataKeyNames="Booking_Id" AllowPaging="true" PageSize="10" BorderColor="#397dbc"
                BorderStyle="Solid" BorderWidth="1px" Font-Size="Small" Width="100%" HorizontalAlign="Center"
                CellPadding="5" OnRowCommand="grdManageLoadingAdvice_RowCommand" OnPageIndexChanging="grdManageLoadingAdvice_PageIndexChanging">
                <EmptyDataTemplate>
                    <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, Date%>">
                        <ItemTemplate>
                             <%#Convert.ToDateTime(Eval("Booking_CreatedDate")).ToString("dd-MM-yyyy")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, BookingNo%>">
                        <ItemTemplate>
                            <%# Eval("Booking_Id")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, CustomerName%>">
                        <ItemTemplate>
                            <%#Eval("Booking_Cust_UnitName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, MaterialType%>">
                        <ItemTemplate>
                            <%#Eval("Booking_MaterialType_MaterialName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, TruckNo%>">
                        <ItemTemplate>
                            <%#Convert.ToInt32(Eval("Booking_TruckType")) == 1 ? Eval("Booking_StandaloneTruck_RegNo") : Eval("Booking_Truck_RegNo")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, BookingQuantity%>">
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
                            <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="EditBooking"
                                Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%#Bind("Booking_Id") %>' />
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
</asp:Content>