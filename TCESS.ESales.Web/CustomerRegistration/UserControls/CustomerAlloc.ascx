<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerAlloc.ascx.cs" 
Inherits="CustomerRegistration_UserControls_CustomerAlloc" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<div>
    <rsweb:ReportViewer BorderColor="Black" BorderWidth="1px" ID="ReportViewer" runat="server"
        Font-Names="Verdana" Font-Size="8pt" Width="745px" Height="810px" ShowExportControls="false" />
</div>
<div>
    &nbsp;
</div>
<div>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnReturn" CssClass="button" Text="<%$Resources:Labels, Back%>"
                    runat="server" OnClick="btnReturn_Click" />
            </td>
        </tr>
    </table>
</div>

