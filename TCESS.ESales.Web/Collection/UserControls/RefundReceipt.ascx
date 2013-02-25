<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RefundReceipt.ascx.cs" 
Inherits="Collection_UserControls_RefundReceipt" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
    <div>
    <rsweb:reportviewer bordercolor="Black" borderwidth="1px" id="reportViewer"
        runat="server" font-names="Verdana" font-size="8pt" width="700px" height="750px">
        </rsweb:reportviewer>
</div>
<div>
    &nbsp;
</div>
<div>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnReturn" CssClass="button" Text="Back"
                    runat="server" OnClick="btnReturn_Click" />
            </td>
        </tr>
    </table>
</div>