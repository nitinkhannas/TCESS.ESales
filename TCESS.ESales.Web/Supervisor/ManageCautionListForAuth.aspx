<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageCautionListForAuth.aspx.cs" Inherits="Supervisor_ManageCautionListForAuth" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, CautionListForAuthorizedRep%>"
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
            <div style="overflow: auto; width: 100%;">
                <Custom:GridViewAlwaysShow AllowPaging="true" ID="grdAuthRepCuationLst" runat="server"
                    AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                    Font-Size="Small" PageSize="10" Width="100%" HorizontalAlign="Center" CellPadding="5"
                    ShowFooter="True" OnPageIndexChanging="grdAuthRepCuationLst_PageIndexChanging"
                    DataKeyNames="AuthRep_Id" OnRowCommand="grdAuthRepCuationLst_RowCommand" OnRowDataBound="grdAuthRepCuationLst_RowDataBound"
                    OnRowDeleting="grdAuthRepCuationLst_RowDeleting" OnMustAddARow="grdAuthRepCuationLst_MustAddARow">
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecordsFound" runat="server" HeaderText="<%$Resources:Labels, NoRecordsFound%>" />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, AuthorizedRepresentative%>">
                            <ItemTemplate>
                                <asp:Label ID="lblAuthRepName" runat="server" Text='<%# Bind("AuthRep_Name") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlAuthRep" runat="server" DataTextField="AuthRep_Name" DataValueField="AuthRep_Id">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="ddlAuthRepValidator" ControlToValidate="ddlAuthRep"
                                    InitialValue="0" Display="Dynamic" ValidationGroup="AddAuthREpCautionLSt" SetFocusOnError="true"
                                    Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredAuthorizedRep %>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="ddlAuthRepValidatorCalloutExtender" runat="server"
                                    TargetControlID="ddlAuthRepValidator" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, BlacklistedBy%>">
                            <ItemTemplate>
                                <asp:Label ID="lblAuthRepBlacklistedBy" runat="server" Text='<%# Bind("AuthRep_BlacklistedBy") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlBlackListedBy" runat="server" Width="110px">
                                    <asp:ListItem Text="<%$Resources:Messages,SelectUser%>" Value="0" />
                                    <asp:ListItem Text="DCA" Value="DCA" />
                                    <asp:ListItem Text="TSL" Value="TSL" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="ddlBlackListedByValidator" ControlToValidate="ddlBlackListedBy"
                                    InitialValue="0" Display="Dynamic" ValidationGroup="AddAuthREpCautionLSt" SetFocusOnError="true"
                                    Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredUser %>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="ddlBlackListedByValidatorCalloutExtender" runat="server"
                                    TargetControlID="ddlBlackListedByValidator" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, CustomerName%>">
                            <ItemTemplate>
                                <asp:Label ID="lblCustomerName" runat="server" Text='<%#(Eval("AuthRep_Customer")==null) ? "" : ((TCESS.ESales.DataTransferObjects.CustomerDTO)Eval("AuthRep_Customer")).Cust_FirmName%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                            <FooterTemplate>
                                <asp:Button ID="lnkAdd" runat="server" CausesValidation="true" CommandName="AddNew"
                                    Text="<%$Resources:Labels, Add%>" CssClass="button" ValidationGroup="AddAuthREpCautionLSt" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                    CommandArgument='<%#Bind("AuthRep_Id")%>' Text="<%$Resources:Labels, Delete%>"
                                    OnClientClick="return confirm('Are you sure you want to delete this item?');"
                                    Font-Underline="False" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#397dbc" ForeColor="#336600" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#397dbc" Font-Bold="True" ForeColor="#FFFFFF" Height="20px" />
                    <PagerStyle BackColor="#397dbc" ForeColor="#FFFFFF" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" Font-Size="Small" Height="23px" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
                </Custom:GridViewAlwaysShow>
            </div>
            <div><uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" /></div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
