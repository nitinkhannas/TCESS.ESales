<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Charts.aspx.cs" Inherits="MarketDistribution_Charts" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PageNameContent" runat="Server">
    <h2>
        <label>
            Markets distribution
        </label>
    </h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <table>
        <tr>
            <td>
                <asp:Image ID="Image1" ImageUrl="~/Images/Copy of chart-of-coal-reservesxls.jpg"
                    runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
