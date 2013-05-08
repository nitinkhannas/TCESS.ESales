<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="InsertDailyDCAMaterial.aspx.cs" Inherits="Bookings_InsertDailyDCAMaterial" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageNameContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
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
            <table width="100%" cellspacing="10" cellpadding="5">
                <tr>
                    <td align="center">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" CssClass="button"
                            Text="Initialize DCA Percentage" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="RejectBooking" runat="server" CssClass="button" 
                             Text="Reject UnPaid Booking" onclick="RejectBooking_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <Custom:GridViewAlwaysShow AllowPaging="True" ID="grdManageCounter" runat="server"
                            AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                            Font-Size="Small" PageSize="12" Width="100%" HorizontalAlign="Center" CellPadding="5"
                        
                            ShowFooter="true">
                            <EmptyDataTemplate>
                                <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                      <%#Convert.ToDateTime(Eval("DCAMA_Date")).ToString("dd-MMM-yyyy")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Today Percentage">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOwnerName" runat="server" Text='<%# Bind("DCAMA_TodayPercentage") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Allocated Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDriverName" runat="server" Text='<%# Bind("DCAMA_AllocatedQty") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current Percentage">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDriverName" runat="server" Text='<%# Bind("DCAMA_CurrentPercentage") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current Variance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDriverName" runat="server" Text='<%# Bind("DCAMA_CurrentVariance") %>' />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
