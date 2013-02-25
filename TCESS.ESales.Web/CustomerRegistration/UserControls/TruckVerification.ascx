<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TruckVerification.ascx.cs"
	Inherits="CustomerRegistration_UserControls_TruckVerification" %>
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
	<Custom:GridViewAlwaysShow AllowPaging="True" ID="grdViewTruck" runat="server" AutoGenerateColumns="False"
		BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
		PageSize="12" Width="100%" HorizontalAlign="Center" CellPadding="5" OnMustAddARow="grdViewTruck_MustAddARow">
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
					<%# GetSMSDate(Eval("Truck_RegNo") == null ? null : Eval("Truck_RegNo").ToString())%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="<%$Resources:Labels, TruckNo%>">
				<ItemTemplate>
					<asp:Label ID="lblDefaultFees" runat="server" Text='<%# Bind("Truck_RegNo") %>' />
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="<%$Resources:Labels, OwnerName%>">
				<ItemTemplate>
					<asp:Label ID="lblFirmName" runat="server" Text='<%# Bind("Truck_OwnerName") %>' />
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="<%$Resources:Labels, DriverName%>">
				<ItemTemplate>
					<asp:Label ID="lblOwnerName" runat="server" Text='<%# Bind("Truck_DriverName") %>' />
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="<%$Resources:Labels, RegisteredAddress%>">
				<ItemTemplate>
					<asp:Label ID="lblBusinessType" runat="server" Text='<%# Bind("Truck_Address") %>' />
				</ItemTemplate>
			</asp:TemplateField>
            	<asp:TemplateField HeaderText="Customer Code">
				<ItemTemplate>
					<%# GetCustomerCode(Eval("Truck_RegNo") == null ? null : Eval("Truck_RegNo").ToString())%>
				</ItemTemplate>
			</asp:TemplateField>
            	<asp:TemplateField HeaderText="Customer Mobile Number">
				<ItemTemplate>
					<%# GetCustomerPhonenumber(Eval("Truck_RegNo") == null ? null : Eval("Truck_RegNo").ToString())%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="<%$Resources:Labels, SmsOrderNo%>">
				<ItemTemplate>
					<%# GetSMSID(Eval("Truck_RegNo") == null ? null : Eval("Truck_RegNo").ToString())%>
				</ItemTemplate>
			</asp:TemplateField>
            <asp:TemplateField  HeaderText= "<%$Resources:Labels, Action%>">
            <ItemTemplate>
            <asp:Button ID ="btnPrint" runat ="server" text ="Print" CssClass ="button" CausesValidation="false"  OnCommand= "btnPrint_Click" CommandArgument = '<%#Bind("Truck_RegNo") %>' />
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

       
</div>
<div>
	<uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
</div>
<div>
<uc1:MessageBox ID ="ucMessageBox" runat ="server" />
</div>
