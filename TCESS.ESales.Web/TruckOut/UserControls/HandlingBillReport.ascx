<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HandlingBillReport.ascx.cs"
    Inherits="TruckOut_UserControls_HandlingBillReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<div>
    <rsweb:ReportViewer BorderColor="Black" BorderWidth="1px" ID="reportViewer" runat="server"
        Font-Names="Verdana" Font-Size="8pt" Width="745px" Height="800px" />
</div>
<div>
    &nbsp;
</div>
<div>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnReturn" CssClass="button" Text="<%$Resources:Labels, SettlementOfAccount%>"
                    runat="server" onclick="btnReturn_Click" />
            </td>
        </tr>
    </table>
</div>
