<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageAllocatePercentage.aspx.cs" Inherits="Supervisor_ManageAllocatePercentage"
    ValidateRequest="false" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:UpdatePanel ID="updHeader" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManageAllocatePercentage%>"
                CssClass="pageNameContent" />
            <asp:Label ID="lblMaterialname" runat="server" CssClass="pageNameContent" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript">
        function ValidateTotalPercentage(CellNo) {
            var sum = 0;
            sum = parseFloat(sum);
            //this will not include the header row and footer row
            var rows = $("#<%=grdMaterialPercentage.ClientID%> tr:gt(0)").not("tr:last");

            rows.children("td:nth-child(4)").each(function () {
                //each time we add the cell to the total
                //If the text field accepts decimal values then use parseFloat to convert the number
                if ($(this).find("input").val() != "") {
                    var percentage = parseFloat($(this).find("input").val().toString()).toFixed(2);
                    sum += parseFloat(percentage);
                }

            });
            sum = parseInt(sum.toFixed(2));
            if (sum != 100 && sum != 0) {
                alert("Alloted material type percentage not equal to 100");
                return false;
            }
            else {
                return true;
            }
        }

    </script>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="overlay">
                <div class="ajaxloader">
                    <img src='<%= ResolveClientUrl("~/Images/ajax-loader.gif")%>' style="vertical-align: middle"
                        alt="Processing" />Processing ...
                   
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="overflow: auto; width: 100%;">
                <table width="100%">
                    <tr>
                        <td align="left" colspan="2" style="height: 40px">
                            <strong>Select Material Type:</strong> &nbsp;
                            <asp:DropDownList ID="ddlMaterialType" runat="server" DataValueField="MaterialType_Id"
                                DataTextField="MaterialType_Name" AutoPostBack="true" OnSelectedIndexChanged="ddlMaterialType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <Custom:GridViewAlwaysShow ID="grdMaterialPercentage" runat="server" BorderColor="#397dbc"
                                BorderStyle="Solid" BorderWidth="1px" Font-Size="Small" DataKeyNames="AMP_Id"
                                ShowFooter="true" AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center"
                                CellPadding="5" OnRowCommand="grdMaterialPercentage_RowCommand">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblNoRecordsFound" runat="server" Text="No records found" />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Agent Account No" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%#Eval("agentdetail")== null? " " : ((TCESS.ESales.DataTransferObjects.AgentDTO)Eval("agentdetail")).Agent_ShortName%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Agent Name" ItemStyle-Width="25%">
                                        <ItemTemplate>
                                            <%#Eval("AgentName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Percentage" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPercentage" runat="server" Text='<%#Bind("AMP_Percentage") %>'
                                                Width="35px" Height="15px" MaxLength="6" />
                                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtPercentage"
                                                FilterMode="ValidChars" ValidChars="." FilterType="Numbers,Custom" />
                                            <asp:RegularExpressionValidator ID="txtPercentageRegExpValidator" runat="server"
                                                ControlToValidate="txtPercentage" ValidationExpression="\d+\.?\d*" Display="Dynamic"
                                                ValidationGroup="EditMaterialTypePercentage" SetFocusOnError="true" Text="*"
                                                CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredInteger %>"></asp:RegularExpressionValidator>
                                            <ajax:ValidatorCalloutExtender ID="txtPercentageRegExpValidatorCallOut" runat="server"
                                                TargetControlID="txtPercentageRegExpValidator" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="lnkbtnAdd" runat="server" Text="Save" OnClientClick="return ValidateTotalPercentage(6);"
                                                CommandArgument="Save" CausesValidation="true" CssClass="button" ValidationGroup="EditMaterialTypePercentage" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#397dbc" ForeColor="#003399" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#397dbc" Font-Bold="True" ForeColor="#FFFFFF" Height="20px" />
                                <PagerStyle BackColor="#397dbc" ForeColor="#FFFFFF" HorizontalAlign="Left" />
                                <RowStyle BackColor="White" ForeColor="#003399" Font-Size="Small" Height="23px" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                <SortedDescendingHeaderStyle BackColor="#002876" />
                            </Custom:GridViewAlwaysShow>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:ValidationSummary ID="EditSummary" runat="server" CssClass="failureNotification"
                    ValidationGroup="EditMaterialTypePercentage" ShowMessageBox="true" ShowSummary="false" />
                     <uc1:MessageBox ID="ucMessageBox" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
