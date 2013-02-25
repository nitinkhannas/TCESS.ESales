<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CustomerFeedback.aspx.cs" Inherits="CustomerDevelopment_CustomerFeedback" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PageNameContent" runat="Server">
    <h2>
        <label>
            Customer Feedback
        </label>
    </h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <table>
        <tr>
            <td colspan="3">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name"/>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:DropDownList ID="ddlCustomerName" runat="server" Width="200"/>
            </td>
        </tr>
        <tr>
            <td colspan="3">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblVisitDate" runat="server" Text="Visit Date"/>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtVisitDate" runat="server" Width="194px" ReadOnly="true"/>
                <cc1:CalendarExtender ID="VisitDate_CalendarExtender" runat="server" Enabled="True"
                    TargetControlID="txtVisitDate" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Label ID="lblFeedbackComments" runat="server" Text="Feedback Comments"/>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtFeedbackComments" Width="194px" Height="150px" TextMode="MultiLine"
                    runat="server"/>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <asp:Button ID="SearchButton" runat="server" Text="Save" />
                &nbsp;
                <asp:Button ID="Button1" runat="server" Text="Reset" />
            </td>
        </tr>
    </table>
</asp:Content>