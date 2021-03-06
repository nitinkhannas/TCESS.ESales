﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DailyHandlingIncomeAndServiceTaxStatementData.ascx.cs" Inherits="Reports_UserControls_DailyHandlingIncomeAndServiceTaxStatementData" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<table width="100%" cellspacing="5" cellpadding="5">
	<tr align="left">
		<td>
			<asp:Label ID="lblDCAName" runat="server" Text="<%$Resources:Labels, DCAName%>" />
		</td>
		<td colspan="4">
			<asp:Label ID="lblAgentName" runat="server" Text="Label"></asp:Label>
		</td>
	</tr>
	<tr align="left">
		<td colspan="5">
			<asp:CheckBox ID="chkDateRange" runat="server" Text="<%$Resources:Labels, EnableMultiDateSelection%>"
				OnCheckedChanged="chkDateRange_CheckedChanged" AutoPostBack="true" />
		</td>
	</tr>
	<tr align="left">
		<td>
			<asp:Label ID="lblFromDate" runat="server" Text="<%$Resources:Labels, FromDate%>" />
		</td>
		<td>
			<asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" Enabled="false" />
			<ajax:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFromDate"
				Format="dd-MMM-yyyy" />
			<ajax:TextBoxWatermarkExtender ID="FromDate_TextBoxWatermarkExtender" runat="server"
				Enabled="True" TargetControlID="txtFromDate" WatermarkCssClass="watermark" WatermarkText="<%$Resources:Labels, SelectDate%>" />
			<asp:RequiredFieldValidator ID="txtFromDateValidator" ControlToValidate="txtFromDate"
				Display="Dynamic" ValidationGroup="GenerateGroup" SetFocusOnError="true" Text="*"
				CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredFromDate %>"
				runat="server" />
			<ajax:ValidatorCalloutExtender ID="txtFromDateValidatorCalloutExtender" runat="server"
				TargetControlID="txtFromDateValidator" />
		</td>
		<td>
			<asp:Label ID="lblToDate" runat="server" Text="<%$Resources:Labels, ToDate%>" />
		</td>
		<td>
			<asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" Enabled="false" />
			<ajax:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtToDate"
				Format="dd-MMM-yyyy" />
			<ajax:TextBoxWatermarkExtender ID="ToDate_TextBoxWatermarkExtender" runat="server"
				Enabled="True" TargetControlID="txtToDate" WatermarkCssClass="watermark" WatermarkText="<%$Resources:Labels, SelectDate%>" />
			<asp:RequiredFieldValidator ID="txtToDateValidator" ControlToValidate="txtToDate"
				Display="Dynamic" ValidationGroup="GenerateGroup" SetFocusOnError="true" Text="*"
				CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredToDate %>"
				runat="server" />
			<ajax:ValidatorCalloutExtender ID="txtToDateValidatorCalloutExtender" runat="server"
				TargetControlID="txtToDateValidator" />
		</td>
		<td align="right">
			<asp:Button ID="btnGenerate" runat="server" Text="<%$Resources:Labels, Generate%>"
				CssClass="button" ValidationGroup="GenerateGroup" OnClick="btnGenerate_Click" />
			&nbsp;
			<asp:Button ID="btnPrint" runat="server" Text="<%$Resources:Labels, Print%>" CssClass="button"
				OnClick="btnPrint_Click" ValidationGroup="GenerateGroup" />
		</td>
	</tr>
</table>
<table width="100%">
	<tr>
		<td align="center">
			<Custom:GridViewAlwaysShow ID="grdDailyHandlingIncomeAndServiceTaxStatement" runat="server" AutoGenerateColumns="False"
				BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
				AllowPaging="false" HorizontalAlign="Center" Width="100%" CellPadding="5">
				<Columns>
					<asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
						<ItemTemplate>
							<%# Container.DataItemIndex + 1 %>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Loading Adv No">
						<ItemTemplate>
							<%# Eval("LoadingAdvNo")%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="TSL Inv No">
						<ItemTemplate>
							<%# Eval("TSLInvNo")%>
						</ItemTemplate>
					</asp:TemplateField>					
                    <asp:TemplateField HeaderText="Qty LiftedMts">
						<ItemTemplate>
							<%# Eval("QtyLiftedMts")%>
						</ItemTemplate>
					</asp:TemplateField>                    
					<asp:TemplateField HeaderText="TSL Amount">
						<ItemTemplate>
							<%# Math.Round(Convert.ToDouble(Eval("TSLAmount")),2) %>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="DCA Bill Handling">
						<ItemTemplate>
							<%#  Math.Round(Convert.ToDouble(Eval("DCABillHandling")),2)%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Service Tax">
						<ItemTemplate>
							<%#  Math.Round(Convert.ToDouble(Eval("ServiceTax")),2)%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="E Cess 2 %">
						<ItemTemplate>
							<%#  Math.Round(Convert.ToDouble(Eval("ECess2")),2)%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="HE Cess 1%">
						<ItemTemplate>
							<%#  Math.Round(Convert.ToDouble(Eval("HECess1")),2)%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Total Stax">
						<ItemTemplate>
							<%# Math.Round(Convert.ToDouble(Eval("ServiceTax")), 2) + Math.Round(Convert.ToDouble(Eval("ECess2")), 2) + Math.Round(Convert.ToDouble(Eval("HECess1")), 2)%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Total Amt Due">
						<ItemTemplate>
							<%# Math.Round(Convert.ToDouble(Eval("ServiceTax")), 2) + Math.Round(Convert.ToDouble(Eval("ECess2")), 2) + Math.Round(Convert.ToDouble(Eval("HECess1")), 2) + Math.Round(Convert.ToDouble(Eval("DCABillHandling")), 2) + Math.Round(Convert.ToDouble(Eval("TSLAmount")), 2)%>
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
</table>