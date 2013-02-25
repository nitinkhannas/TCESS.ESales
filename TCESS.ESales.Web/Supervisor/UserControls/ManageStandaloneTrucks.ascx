<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManageStandaloneTrucks.ascx.cs"
    Inherits="Bookings_UserControls_ManageStandaloneTrucks" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<div style="text-align: left;">
	<asp:Label ID="lblTruckNumber" runat="server" Text="<%$Resources:Labels, TruckNo%>" />
	<asp:TextBox ID="txtTruckNumber" runat="server" CssClass="textbox" Wrap="False" MaxLength="15"
		onkeypress="return runScript(event)" />
	<asp:RequiredFieldValidator ID="TruckNumberValidator" ControlToValidate="txtTruckNumber"
		Display="Dynamic" ValidationGroup="ValidateGroup" SetFocusOnError="true" Text="*"
		CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredTruckRegNo %>"
		runat="server" />
	<ajax:ValidatorCalloutExtender ID="TruckNumberValidatorCallout" runat="server" TargetControlID="TruckNumberValidator" />
	<asp:Button ID="btnValidate" runat="server" ValidationGroup="ValidateGroup" Text="Validate"
		OnClick="btnValidate_Click" CssClass="button" />
</div>
<div class="clear">
	&nbsp;
</div>
<div style="overflow: auto; width: 100%;">
    <Custom:GridViewAlwaysShow AllowPaging="True" ID="grdManageStandaloneTrucks" runat="server"
        AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
        Font-Size="Small" PageSize="10" Width="100%" HorizontalAlign="Center" CellPadding="5"
        OnMustAddARow="grdManageStandaloneTrucks_MustAddARow" DataKeyNames="StandaloneTruck_Id"
        OnRowCommand="grdManageStandaloneTrucks_RowCommand" ShowFooter="true" OnRowDeleting="grdManageStandaloneTrucks_RowDeleting"
        OnPageIndexChanging="grdManageStandaloneTrucks_PageIndexChanging">
        <EmptyDataTemplate>
            <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, TruckNo%>">
                <ItemTemplate>
                    <asp:Label ID="lblRegNo" runat="server" Text='<%# Bind("StandaloneTruck_RegNo") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, OwnerName%>">
                <ItemTemplate>
                    <asp:Label ID="lblOwnerName" runat="server" Text='<%# Bind("StandaloneTruck_OwnerName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, DriverName%>">
                <ItemTemplate>
                    <asp:Label ID="lblDriverName" runat="server" Text='<%# Bind("StandaloneTruck_DriverName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="EditTruck"
                        Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%#Bind("StandaloneTruck_Id") %>' />
                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="<%$Resources:Labels, Delete%>" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                        Font-Underline="False" CommandArgument='<%#Bind("StandaloneTruck_Id") %>' />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Button ID="lnkAddTruck" runat="server" CausesValidation="true" CommandName="AddNew"
                        Text="<%$Resources:Labels, Add%>" CssClass="button" ValidationGroup="VGAddTruck" />
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
    <div>
        <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
    </div>
</div>
