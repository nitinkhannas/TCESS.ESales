<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SMSConfirmation.aspx.cs" Inherits="Bookings_SMSConfirmation" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageNameContent" runat="Server">
    <table width="100%" cellspacing="10" cellpadding="5">
        <tr>
            <td>
                <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, SMSBookingConfirmation%>"
                    CssClass="pageNameContent" />
            </td>
            <td>
                <asp:Label ID="lblSmsR" runat="server" Text="<%$Resources:Labels, SMSReceived%>"
                    class="formlabel" />
                <asp:Label ID="lblSmsReceived" runat="server" />
            </td>
            <td>
                <asp:Label ID="lblSmsL" runat="server" Text="<%$Resources:Labels, SMSLimit%>" class="formlabel" />
                <asp:Label ID="lblSmsLimit" runat="server" />
            </td>
            <td>
                <asp:Label ID="lblSmsA" runat="server" Text="<%$Resources:Labels, SMSAccepted%>"
                    class="formlabel" />
                <asp:Label ID="lblSmsAccepted" runat="server" />
            </td>
            <td>
                <asp:Label ID="lblSmsB" runat="server" Text="<%$Resources:Labels, SMSBalance%>" class="formlabel" />
                <asp:Label ID="lblSmsBalance" runat="server" />
            </td>
        </tr>
    </table>
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
            <div style="overflow: auto; width: 100%;">
                <table width="100%">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <Custom:GridViewAlwaysShow ID="grdSMSReg" runat="server" AutoGenerateColumns="False"
                                BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                                AllowPaging="true" PageSize="10" HorizontalAlign="Center" Width="100%" CellPadding="5"
                                OnRowCommand="grdSMSReg_RowCommand" OnRowEditing="grdSMSReg_RowEditing" OnRowCancelingEdit="grdSMSReg_RowCancelingEdit"
                                OnRowUpdating="grdSMSReg_RowUpdating" DataKeyNames="SMSReg_Id" OnPageIndexChanging="grdSMSReg_PageIndexChanging"
                                OnRowDataBound="grdSMSReg_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAutoColumn" Text="<%# Container.DataItemIndex + 1 %>" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SMS Date">
                                        <ItemTemplate>
                                            <%#Convert.ToDateTime(Eval("SMSReg_CreatedDate")).ToString("dd-MMM-yyyy")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SMS Time">
                                        <ItemTemplate>
                                            <%#Convert.ToDateTime(Eval("SMSReg_CreatedDate")).ToString("T")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Code">
                                        <ItemTemplate>
                                            <%#Eval("SMSReg_Cust_Code")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <%#Eval("SMSReg_Cust_TradeName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemTemplate>
                                            <%#Eval("SMSReg_Cust_State_Name")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District">
                                        <ItemTemplate>
                                            <%#Eval("SMSReg_Cust_District_Name")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TruckNo">
                                        <ItemTemplate>
                                            <%#Eval("SMSReg_TruckNo")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$Resources:Labels, RejectionReason%>" Visible="false">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlRejectionReason" runat="server" CssClass="listmenu">
                                                <asp:ListItem Text="Select Reason" Selected="True" Value="0" />
                                                <asp:ListItem Text="Wrong Truck No" Value="1" />
                                                <asp:ListItem Text="More than One Truck in SMS" Value="2" />
                                                <asp:ListItem Text="Stopped by DCA" Value="3" />
                                                <asp:ListItem Text="Stopped by TSL" Value="4" />
                                                <asp:ListItem Text="Stopped by Customer" Value="5" />
                                                <asp:ListItem Text="Others" Value="6" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RejectionReasonValidator" ControlToValidate="ddlRejectionReason"
                                                Display="Dynamic" ValidationGroup="ValidateReasonGroup" SetFocusOnError="true"
                                                Text="*" InitialValue="0" CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredRejectionReason%>"
                                                runat="server" />
                                            <ajax:ValidatorCalloutExtender ID="RejectionReasonValidatorCallout" runat="server"
                                                TargetControlID="RejectionReasonValidator" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkAccept" runat="server" CausesValidation="False" CommandName="EditBooking"
                                                Text="<%$Resources:Labels, Accept%>" Font-Underline="False" CommandArgument='<%#Bind("SMSReg_Id")%>' />
                                            <asp:LinkButton ID="lnkReject" runat="server" CausesValidation="False" CommandName="Edit"
                                                Text="<%$Resources:Labels, Reject%>" Font-Underline="False" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" Visible="false">
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                Text="<%$Resources:Labels, Save%>" Font-Underline="False" ValidationGroup="ValidateReasonGroup" />
                                            <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                Text="<%$Resources:Labels, Cancel%>" Font-Underline="False" />
                                        </EditItemTemplate>
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
                            <table style="width: 656px">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnApproval" runat="server" CssClass="button" OnClick="btnApproval_Click"
                                            Text="Approve SMS" Visible="False" />
                                    </td>
                                    <td>
                                    <asp:Button ID="btnRejectAll" runat="server" CssClass="button" 
                                            Text="Reject SMS" Visible="False" OnClientClick="return confirm('Are you sure you want to reject all the SMS?');" onclick="btnRejectAll_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:HiddenField ID="hdnFlag" runat="server" />
                            <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
