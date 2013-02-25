<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Form27CPeriodType.aspx.cs"
    Inherits="Masters_Form27CPeriodType" ValidateRequest="false" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="Form 27C Period Type" CssClass="pageNameContent" />
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
            <table width="100%" cellspa cing="5" cellpadding="5">
                <tr align="left">
                    <td>
                        <asp:Label ID="lblValidMonth" runat="server" Text="Form 27C Period Type" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPeriodType" runat="server" AutoPostBack="false" CssClass="listmenu" />
                        <asp:RequiredFieldValidator ID="MonthValidator" runat="server" ControlToValidate="ddlPeriodType"
                            CssClass="failureNotification" Display="Dynamic" ErrorMessage="Enter Month" InitialValue="0"
                            SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup" />
                        <ajax:ValidatorCalloutExtender ID="MonthValidatorCallOutExtender" runat="server"
                            TargetControlID="MonthValidator" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5" align="center">
                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="<%$Resources:Labels, Accept%>"
                            OnClick="btnSave_Click" ValidationGroup="SaveGroup" />
                    </td>
                </tr>
            </table>
            <div>
                <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
            </div>
            <div runat="server" id="divHistory">
                <asp:Label ID="lblHistory" Visible="false" runat="server" Text="<%$Resources:Labels, History%>"
                    CssClass="pageNameContent" />
                <Custom:GridViewAlwaysShow AllowPaging="true" ID="gridHistory" runat="server" AutoGenerateColumns="False"
                    BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                    PageSize="10" Width="100%" HorizontalAlign="Center" CellPadding="5" ShowFooter="True">
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Created Date">
                            <ItemTemplate>
                                <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Modified Date">
                            <ItemTemplate>
                                <asp:Label ID="lblModifiedDate" runat="server" Text='<%#Eval("ModifiedDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%#Status(Eval("PeriodType").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Period Type">
                            <ItemTemplate>
                                <asp:Label ID="lblRace" runat="server" Text='<%#PeriodType(Eval("PeriodTypeId").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#397dbc" ForeColor="#003399" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#397dbc" Font-Bold="True" ForeColor="#FFFFFF" Height="20px" />
                    <PagerStyle BackColor="#397dbc" ForeColor="#FFFFFF" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" Font-Size="Small" Height="20px" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
                </Custom:GridViewAlwaysShow>
            </div>
            <uc1:MessageBox ID="ucMessageBox" runat="server" />
            <uc3:ViewImage ID="ucViewImage" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
