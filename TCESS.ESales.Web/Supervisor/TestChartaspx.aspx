<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="TestChartaspx.aspx.cs" Inherits="Supervisor_TestChartaspx" ValidateRequest="false" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageNameContent" runat="Server">
    <div>
        <asp:Chart ID="SalesChart" Width="445px" runat="server" Font="Trebuchet MS, 8.25pt, style=Bold"
            Palette="BrightPastel" Height="296px" BorderlineDashStyle="Solid" MarkerStyle="Circle"
            BorderColor="64, 64, 64, 64" Color="180, 65, 140, 240" BackColor="LavenderBlush">
            <%-- <Legends>
                <asp:Legend Name="Default">
                </asp:Legend>
            </Legends>
            <Series>
                <asp:Series ChartType="Column" Name="Series1">
                </asp:Series>
             
            </Series>--%>
            <ChartAreas>
                <asp:ChartArea Name="Area1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:TextBox ID="txt" runat="server">
    </asp:TextBox>
    <asp:Button ID="btn" runat="server" Text="sdf" OnClick="btn_Click" />
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:DropDownList ID="ddl" runat="server" AutoPostBack="true" 
                onselectedindexchanged="ddl_SelectedIndexChanged">
                <asp:ListItem Value="0" Text="A"></asp:ListItem>
                <asp:ListItem Value="1" Text="B"></asp:ListItem>
            </asp:DropDownList>
            <asp:GridView ID="gv" runat="server">
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
