<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageUserPaymentMode.aspx.cs" Inherits="Administrator_ManageUserPaymentMode" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, MANAGEUSERPAYMENTMODE%>"
        CssClass="pageNameContent" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="progressBar" runat="server" AssociatedUpdatePanelID="uplMainPanel"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="overlay">
                <div class="ajaxloader">
                    <img src='<%= ResolveClientUrl("~/Images/ajax-loader.gif")%>' style="vertical-align: middle"
                        alt="Processing" />Processing....
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="uplMainPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="overflow: auto;">
                <table align="left">
                    <tr>
                        <td>
                            <asp:Label ID="lblTotalAmount" runat="server" Text="<%$Resources:Labels, UserName%>" />
                            &nbsp;
                            <asp:DropDownList ID="ddlUsers" runat="server" CssClass="listmenu" OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged" AutoPostBack="true" />
                            <asp:RequiredFieldValidator ID="UserValidator" runat="server" ControlToValidate="ddlUsers"
                                CssClass="failureNotification" Display="Dynamic" ErrorMessage="<%$ Resources:ErrorMessages, RequiredUser%>"
                                InitialValue="0" SetFocusOnError="true" Text="*" />
                            <ajax:ValidatorCalloutExtender ID="UserValidatorCallOutExtender" runat="server" TargetControlID="UserValidator" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                &nbsp;
            </div>
            <div style="overflow: auto; width: 100%;">
                <Custom:GridViewAlwaysShow AllowPaging="true" ID="grdUserPaymentMode" runat="server"
                    AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                    Font-Size="Small" PageSize="13" Width="100%" HorizontalAlign="Center" CellPadding="5"
                    OnPageIndexChanging="grdUserPaymentMode_PageIndexChanging" OnMustAddARow="grdUserPaymentMode_MustAddARow"
                    DataKeyNames="PaymentMode_Id">
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="70px" HeaderText="<%$Resources:Labels, SELECT%>">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkSelectPaymentMode" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, PaymentMode%>">
                            <ItemTemplate>
                                <asp:Label ID="lblPaymentMode" runat="server" Text='<%#Bind("PaymentMode_Name") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlPaymentMode" runat="server" DataTextField="PaymentMode_Name"
                                    DataValueField="PaymentMode_Id" CssClass="listmenu" />
                            </EditItemTemplate>
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
            <div style="clear: both; height: 20px">
            </div>
            <div style="align: center;">
            <asp:CustomValidator ID="customValidator" Display="Dynamic" runat="server" CssClass="failureNotification" />
                <asp:Button ID="btnSave" CssClass="button" runat="server" Text="<%$Resources:Labels, Save%>"
                    ValidationGroup="SaveGroup" OnClick="btnSave_Click" />
            </div>
            <div style="clear: both; height: 10px">
            </div>
            <uc1:MessageBox ID="ucMessageBox" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>