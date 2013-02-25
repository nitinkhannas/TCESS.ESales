 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestSmsSend.aspx.cs" MasterPageFile="~/Site.master" Inherits="Bookings_Default" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <div>
 <table >
            <tr align="left">
                <td  >MobileNo</td>
                <td  >
                <asp:TextBox ID="txtMobile" runat="server"  ></asp:TextBox></td>
                <td ><asp:RequiredFieldValidator ID="requiredvalue" ControlToValidate="txtMobile" ValidationGroup="RequiredValue" ErrorMessage="Enter Value" SetFocusOnError="true" Text="*" runat="server"></asp:RequiredFieldValidator></td>
                </tr>
                <tr align="left">
                <td >Customer Code</td>
                <td ><asp:TextBox ID="txtCustCode" runat="server"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="requiredvalue1" ControlToValidate="txtCustCode" ValidationGroup="RequiredValue" ErrorMessage="Enter Value" SetFocusOnError="true" Text="*" runat="server"></asp:RequiredFieldValidator></td>
                
                </tr>
                <tr align="left">
                <td style="width: 10px">TruckNo</td>
                <td style="width: 20px"><asp:TextBox ID="txtTruck" runat="server"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="requiredvalue2" ControlToValidate="txtTruck" ValidationGroup="RequiredValue" ErrorMessage="Enter Value" SetFocusOnError="true" Text="*" runat="server"></asp:RequiredFieldValidator></td>
                <td ><asp:Button ID="btnvalidate" runat="server" Text="Validate" 
                        onclick="btnvalidate_Click" /></td>
                </tr>
                </table>
 </div>
 </asp:Content>
