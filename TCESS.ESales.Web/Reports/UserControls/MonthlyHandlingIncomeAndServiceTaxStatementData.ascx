<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MonthlyHandlingIncomeAndServiceTaxStatementData.ascx.cs" Inherits="Reports_UserControls_MonthlyHandlingIncomeAndServiceTaxStatementData" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<table width="100%" cellspacing="5">
    <tr align="left">
		<td>
			<asp:Label ID="lblDCAName" runat="server" Text="<%$Resources:Labels, DCAName%>" />
		</td>
		<td colspan="4">
			<asp:Label ID="lblAgentName" runat="server" Text="Label"></asp:Label>
		</td>
	</tr>
    <tr align="left">
        <td>Month</td>
        <td>            
            <asp:DropDownList ID="ddlMonth" runat="server" 
                onselectedindexchanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true">   
                <asp:ListItem Text="January" Value="1" Selected="true"></asp:ListItem>
            <asp:ListItem Text="February" Value="2" Selected="false"></asp:ListItem>
            <asp:ListItem Text="March" Value="3" Selected="false"></asp:ListItem>
            <asp:ListItem Text="April" Value="4" Selected="false"></asp:ListItem>
            <asp:ListItem Text="May" Value="5" Selected="false"></asp:ListItem>
            <asp:ListItem Text="June" Value="6" Selected="false"></asp:ListItem>
            <asp:ListItem Text="July" Value="7" Selected="false"></asp:ListItem>
            <asp:ListItem Text="August" Value="8" Selected="false"></asp:ListItem>
            <asp:ListItem Text="September" Value="9" Selected="false"></asp:ListItem>
            <asp:ListItem Text="October" Value="10" Selected="false"></asp:ListItem>
            <asp:ListItem Text="November" Value="11" Selected="false"></asp:ListItem>
            <asp:ListItem Text="December" Value="12" Selected="false"></asp:ListItem>             
            </asp:DropDownList>            
        </td>
        <td>Year</td>
        <td>
            <asp:DropDownList ID="ddlYear" runat="server" 
                onselectedindexchanged="ddlYear_SelectedIndexChanged" AutoPostBack="true">              
            </asp:DropDownList>            
        </td>
        <td align="right">
            <asp:Button ID="btnGenerate" runat="server" Text="<%$Resources:Labels, Generate%>"
                CssClass="button" ValidationGroup="LoadingAdvRpt" OnClick="btnGenerate_Click" />
            &nbsp;
            <asp:Button ID="btnPrint" runat="server" Text="<%$Resources:Labels, Print%>" CssClass="button"
                OnClick="btnPrint_Click" Width="55px" ValidationGroup="LoadingAdvRpt" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
<table width="100%">
	<tr>
		<td align="center">
			<Custom:GridViewAlwaysShow ID="grdMontlyHandlingIncomeAndServiceTaxStatement" runat="server" AutoGenerateColumns="False"
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