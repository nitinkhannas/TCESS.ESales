<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ConsolidatedCustomerCollectionReport.ascx.cs"
    Inherits="Reports_UserControls_ConsolidatedCustomerCollectionReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<div>
    <table width="100%">
        <tr>
            <td align="center">
               <asp:Button ID="btnGenerate" CssClass="button" Text="Generate Report" 
                    runat="server" onclick="btnGenerate_Click" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <rsweb:ReportViewer BorderColor="Black" BorderWidth="1px" ID="reportViewer" runat="server"
                    Font-Names="Verdana" Font-Size="8pt" Width="860px">
                </rsweb:ReportViewer>
            </td>
        </tr>
    </table>
</div>
<div>
    &nbsp;
</div>
<div>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnReturn" CssClass="button" Text="Return" runat="server" OnClick="btnReturn_Click" />
            </td>
        </tr>
    </table>
</div>
