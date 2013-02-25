<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="SMSBookingLimit.aspx.cs"
    Inherits="Bookings_SMSBookingLimit" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <table width="100%" cellspacing="10" cellpadding="5">
        <tr>
            <td>
                <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, SMSBookingLimit%>"
                    CssClass="pageNameContent" />
            </td>
            <td>
                <asp:Label ID="lblCDate" runat="server" Text="Current Date: " class="formlabel"/>            
                <asp:Label ID="lblCurrentDate" runat="server"/>
            </td>
            <td>
                <asp:Label ID="lblNDate" runat="server" Text="Valid For: " class="formlabel"/>            
                <asp:Label ID="lblNextDate" runat="server"/>
            </td>
            <td>
                <asp:Label ID="lblSmsRec" runat="server" Text="<%$Resources:Labels, SMSReceived%>" class="formlabel"/>
                <asp:Label ID="lblSmsReceived" runat="server"/>
            </td>
            <td>
                <asp:Label ID="lblSmsAcc" runat="server" Text="<%$Resources:Labels, SMSAccepted%>" class="formlabel"/>
                <asp:Label ID="lblSmsAccepted" runat="server"/>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="smslimitcontent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="smslimitprogressBar" runat="server" AssociatedUpdatePanelID="smslimitUpdatePanel"
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
    <asp:UpdatePanel runat="server" ID="smslimitUpdatePanel" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="overflow: auto; width: 100%;">
                <Custom:GridViewAlwaysShow AllowPaging="false" ID="grdSMSLimit" runat="server" AutoGenerateColumns="False"
                    BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                    PageSize="10" Width="100%" HorizontalAlign="Center" CellPadding="5" ShowFooter="True"
                    OnRowCommand="grdSMSLimit_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SMSLimit%>">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewSMSLimit" runat="server" MaxLength="5" />
                                <asp:RequiredFieldValidator ID="smsLimitValidator" ControlToValidate="txtNewSMSLimit"
		                            Display="Dynamic" ValidationGroup="ValidateGroup" SetFocusOnError="true" Text="*"
		                            CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredSMSLimit %>"
		                            runat="server" />
	                            <ajax:ValidatorCalloutExtender ID="smsLimitValidatorCallout1" runat="server" TargetControlID="smsLimitValidator" />
                                <ajax:FilteredTextBoxExtender ID="smsLimitTextBoxExtender" runat="server" TargetControlID="txtNewSMSLimit"
                                    FilterMode="ValidChars" FilterType="Numbers" />
                                <asp:RegularExpressionValidator ID="smsLimitCodeValidator" runat="server" ControlToValidate="txtNewSMSLimit"
                                    Display="Dynamic" SetFocusOnError="true" Text="*" ValidationGroup="SaveGroup"
                                    ErrorMessage="<%$ Resources:ErrorMessages, InvalidSMSLimit%>" CssClass="failureNotification"
                                    ValidationExpression="^\d{1,5}$" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# Eval("SMSLimit_Limit")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, AuthorizedBy%>">
                            <FooterTemplate>
                                <asp:TextBox ID="txtAuthorizedBy" runat="server" MaxLength="45" />
                                <asp:RequiredFieldValidator ID="authorizedByValidator" ControlToValidate="txtAuthorizedBy"
		                            Display="Dynamic" ValidationGroup="ValidateGroup" SetFocusOnError="true" Text="*"
		                            CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredAuthorizedBy %>"
		                            runat="server" />
	                            <ajax:ValidatorCalloutExtender ID="authorizedByValidatorCallout" runat="server" TargetControlID="authorizedByValidator" />
                                <ajax:FilteredTextBoxExtender ValidChars=" ,.,&,(,),-" ID="authorizedByFilteredTextBoxExtender"
                                    runat="server" TargetControlID="txtAuthorizedBy" FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# Eval("SMSLimit_AuthorizedBy")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SMSLimitLastUpdatedTime%>">
                            <ItemTemplate>
                                <%#String.Format("{0:t}", Eval("SMSLimit_LastUpdatedDate"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                            <FooterTemplate>
                                <asp:Button ID="lnkAdd" runat="server" CausesValidation="True" CommandName="AddNew"
                                    CssClass="button" Text="<%$Resources:Labels, Add%>" ValidationGroup="ValidateGroup" />
                            </FooterTemplate>
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
            <div>
                <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
