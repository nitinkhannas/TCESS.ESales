<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ConsolidatedCustomerCollectionData.ascx.cs"
    Inherits="Reports_UserControls_ConsolidatedCustomerCollectionData" %>
<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<table width="100%" cellspacing="5">
    <tr align="left">
        <td align="right">
            <asp:Button ID="btnPrint" runat="server" OnClientClick="javascript:return CompareDate();"
                Text="<%$Resources:Labels, Print%>" CssClass="button" OnClick="btnPrint_Click"
                Width="55px" ValidationGroup="LoadingAdvRpt" />
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <custom:gridviewalwaysshow id="grdConsolidatedCustomerCollection" runat="server"
                autogeneratecolumns="False" bordercolor="#3366CC" borderstyle="Solid" borderwidth="1px"
                font-size="Small" allowpaging="False" horizontalalign="Center" width="100%" cellpadding="5">
                <columns>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="<%$Resources:Labels, CustomerCode%>">
                        <ItemTemplate>
                            <%#Eval("CustomerCode")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, CustomerName%>">
                        <ItemTemplate>
                            <%# Eval("CustomerName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, District%>">
                        <ItemTemplate>
                            <%#Eval("CustomerDistrict")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, OpeningBalance%>">
                        <ItemTemplate>
                           <%#Eval("OpeningBalance")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, CollectionActive%>">
                        <ItemTemplate>
                            <%#Eval("CollectionActive")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, TotalBalAvailable%>">
                        <ItemTemplate>
                            <%#Eval("TotalBalAvailable")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, TotalLoadingAdviceIssued%>">
                         <ItemStyle HorizontalAlign ="left" />
                        <ItemTemplate>
                             <%#Eval("TotalLoadingAdviceIssued")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, TotalSettlement%>">
                        <ItemTemplate>
                            <%#Eval("TotalSettlement")%>
                        </ItemTemplate>
                    </asp:TemplateField>                                    
                    <asp:TemplateField HeaderText="<%$Resources:Labels, ClosingBalance%>">
                        <ItemTemplate>
                            <%#Eval("ClosingBalance")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="<%$Resources:Labels, HoldForActivation%>">
                        <ItemTemplate>
                            <%#Eval("HoldForActivation")%>
                        </ItemTemplate>
                        </asp:TemplateField>       
                </columns>
                <headerstyle backcolor="#397dbc" font-bold="True" forecolor="#FFFFFF" height="20px" />
                <pagerstyle backcolor="#397dbc" forecolor="#FFFFFF" horizontalalign="Left" />
                <rowstyle backcolor="White" forecolor="#003399" font-size="Small" height="20px" horizontalalign="Center" />
                <selectedrowstyle backcolor="#009999" font-bold="True" forecolor="#CCFF99" />
                <sortedascendingcellstyle backcolor="#EDF6F6" />
                <sortedascendingheaderstyle backcolor="#0D4AC4" />
                <sorteddescendingcellstyle backcolor="#D6DFDF" />
                <sorteddescendingheaderstyle backcolor="#002876" />
            </custom:gridviewalwaysshow>
        </td>
    </tr>
</table>
