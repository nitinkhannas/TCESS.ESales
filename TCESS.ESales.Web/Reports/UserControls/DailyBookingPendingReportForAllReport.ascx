<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DailyBookingPendingReportForAllReport.ascx.cs" Inherits="Reports_UserControls_DailyBookingPendingReportForAllReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<div>
    <rsweb:reportviewer bordercolor="Black" borderwidth="1px" id="reportViewer"
        runat="server" font-names="Verdana" font-size="8pt" width="820px" 
       PageCountMode="Actual" >
        </rsweb:reportviewer>
</div>
<div>
    &nbsp;
</div>
<div>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnReturn" CssClass="button" Text="Return"
                    runat="server" onclick="btnReturn_Click"/>
            </td>
        </tr>
    </table>
</div>