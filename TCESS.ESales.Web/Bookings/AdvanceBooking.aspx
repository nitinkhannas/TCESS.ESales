<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AdvanceBooking.aspx.cs" Inherits="Bookings_AdvanceBooking" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, AdvanceBookings%>"
        CssClass="pageNameContent" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="progressBar" runat="server" AssociatedUpdatePanelID="uplMainPanel"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="overlay">
                <div class="ajaxloader">
                    <img src='<%= ResolveClientUrl("~/Images/ajax-loader.gif")%>' style="vertical-align: middle"
                        alt="Processing" />Processing ...
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="uplMainPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td align="center">
                        <Custom:GridViewAlwaysShow ID="grdBooking" runat="server" AutoGenerateColumns="False"
                            BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                            AllowPaging="true" PageSize="10" HorizontalAlign="Center" Width="100%" CellPadding="5"
                            OnRowCommand="grdBooking_RowCommand">
                            <EmptyDataTemplate>
                                <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, BookingDate%>">
                                    <ItemTemplate>
                                        <%#Convert.ToDateTime(Eval("Booking_Date")).ToString("dd-MMM-yyyy")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, DCAShortName%>">
                                    <ItemTemplate>
                                        <%#Eval("Booking_Agent_AgentShortName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, CustomerCode%>">
                                    <ItemTemplate>
                                        <%#Eval("Booking_Cust_Code")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, CustomerName%>">
                                    <ItemTemplate>
                                        <%#Eval("Booking_Cust_TradeName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, State%>">
                                    <ItemTemplate>
                                        <%#Eval("Booking_Cust_State_Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, District%>">
                                    <ItemTemplate>
                                        <%#Eval("Booking_Cust_District_Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, Quantity%>">
                                    <ItemTemplate>
                                        <%#Eval("Booking_Qty")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, BookingAdvance%>">
                                    <ItemTemplate>
                                        <%#Math.Round(Convert.ToDecimal(Eval("Booking_AdvanceAmount")),2)%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, TruckNo%>">
                                    <ItemTemplate>
                                        <%#Convert.ToInt32(Eval("Booking_TruckType")) == 1 ? Eval("Booking_StandaloneTruck_RegNo") : Eval("Booking_Truck_RegNo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkAccept" runat="server" CausesValidation="False" CommandName="EditBooking"
                                            Text="<%$Resources:Labels, Accept%>" Font-Underline="False" CommandArgument='<%#Bind("Booking_Id")%>' />
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
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCounterNo" runat="server" CssClass="pageNameContent" Visible="false" />
                        <uc1:MessageBox ID="ucMessageBox" runat="server" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
