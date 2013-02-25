<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerDocumentReValidate.ascx.cs" 

Inherits="CustomerRegistration_UserControls_CustomerDocumentReValidate" %>
<style type="text/css">
    .style1
    {
        height: 38px;
    }
    .style2
    {
        height: 35px;
    }
    .button
    {
        margin-bottom: 0px;
    }
</style>
<table width="100%" cellspacing="10" cellpadding="5">
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="Label1" runat="server" Text="<%$Resources:Labels, TradeName%>" />
        </td>
        <td>
            <asp:Label runat="server" ID="lblTradeName" />
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="Label2" runat="server" Text="<%$Resources:Labels, FirmName%>" />
        </td>
        <td>
            <asp:Label runat="server" ID="lblFirmName" />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="Label3" runat="server" Text="<%$Resources:Labels, OwnershipStatus%>" />
        </td>
        <td>
            <asp:Label runat="server" ID="lblOwnershipStatus" />
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="Label4" runat="server" Text="<%$Resources:Labels, OwnerPartnerName%>" />
        </td>
        <td>
            <asp:Label runat="server" ID="lblOwnerName" />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="Label5" runat="server" Text="<%$Resources:Labels, OwnerPartnerMobileNo%>" />
        </td>
        <td>
            <asp:Label runat="server" ID="lblPartnerMobileNumber" />
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="Label6" runat="server" Text="<%$Resources:Labels, FathersName%>" />
        </td>
        <td>
            <asp:Label runat="server" ID="lblFatherName" />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap" valign="top" >
            <asp:Label ID="Label7" runat="server" Text="<%$Resources:Labels, RegisteredAddress%>" />
        </td>
        <td class="wordbreak" width="25%">
            <asp:Label runat="server" ID="lblRegisteredAddress" />
        </td>
        <td nowrap="nowrap" valign="top">
            <asp:Label ID="Label8" runat="server" Text="<%$Resources:Labels, UnitAddress%>" />
        </td>
        <td class="wordbreak" width="25%">
            <asp:Label runat="server" ID="lblUnitAddress" />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="Label9" runat="server" Text="<%$Resources:Labels, Landmark%>" />
        </td>
        <td class="wordbreak" width="25%">
            <asp:Label runat="server" ID="lblLandmark" />
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="Label10" runat="server" Text="<%$Resources:Labels, AMEOffice%>" />
        </td>
        <td>
            <asp:Label runat="server" ID="lblAMEOffice" />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="Label11" runat="server" Text="<%$Resources:Labels, State%>" />
        </td>
        <td>
            <asp:Label runat="server" ID="lblState" />
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="Label12" runat="server" Text="<%$Resources:Labels, District%>" />
        </td>
        <td>
            <asp:Label runat="server" ID="lblDistrict" />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="Label13" runat="server" Text="<%$Resources:Labels, PINCode%>" />
        </td>
        <td>
            <asp:Label runat="server" ID="lblPinCode" />
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="Label14" runat="server" Text="<%$Resources:Labels, BusinessType%>" />
        </td>
        <td>
            <asp:Label runat="server" ID="lblBusinessType" />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="Label15" runat="server" Text="<%$Resources:Labels, MobileNumberforSMS%>" />
        </td>
        <td>
            <asp:Label runat="server" ID="lblMobileNo" />
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblPhoneNo" runat="server" Text="<%$Resources:Labels, PhoneNumberwithSTDcode%>" />
        </td>
        <td>
            <asp:Label runat="server" ID="lblPhoneNumber" />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="lblNoOfChimney1" runat="server" Text="<%$Resources:Labels, NoOfChimneys%>" />
        </td>
        <td>
            <asp:Label ID="lblNoOfChimney" runat="server" 
				 />
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblBrickCapacity1" runat="server" Text="<%$Resources:Labels, BrickCapacity%>" />
        </td>
        <td>
            <asp:Label ID="lblBrickCapacity" runat="server" 
				/>
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="Label16" runat="server" Text="<%$Resources:Labels, SalesType%>" />
        </td>
        <td>
            <asp:Label runat="server" ID="lblSalesType" />
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblExciseRange0" runat="server" Text="<%$Resources:Labels, ExciseRange%>" />
        </td>
        <td>
            <asp:Label ID="lblExciseRange" runat="server" 
				 />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="lblExciseDiv1" runat="server" Text="<%$Resources:Labels, ExciseDiv%>" />
        </td>
        <td>
            <asp:Label ID="lblExciseDiv" runat="server" 
				/>
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblExciseComm1" runat="server" 
				Text="<%$Resources:Labels, ExciseComm%>" />
        </td>
        <td>
            <asp:Label ID="lblExciseComm" runat="server" 
				 />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap" class="style1">
            <asp:Label ID="lblBankName0" runat="server" Text="Name of Customer Bank" />
        </td>
        <td class="style1">
            <asp:Label ID="lblBankName" runat="server" />
        </td>
        <td nowrap="nowrap" class="style1">
            <asp:Label ID="lblAccountNo1" runat="server" Text="Account No" />
        </td>
        <td class="style1">
            <asp:Label ID="lblAccountNo" runat="server" />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="lblBankBranch0" runat="server" Text="Branch" />
        </td>
        <td>
            <asp:Label ID="lblBankBranch" runat="server" />
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblChequeNo0" runat="server" Text="Cancelled Cheque No" />
        </td>
        <td>
            <asp:Label ID="lblChequeNo" runat="server" />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="lblAccountType0" runat="server" Text="Type of Account" />
        </td>
        <td>
            <asp:Label ID="lblAccountType" runat="server" />
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblIFSCCode0" runat="server" Text="IFSC Code" />
        </td>
        <td>
            <asp:Label ID="lblIFSCCode" runat="server" />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap">
            <asp:Label ID="lblAMEVisitDate3" runat="server" 
                Text="Annual VAT Return Filed  on" />
        </td>
        <td>
            <asp:Label ID="lblVATReturn" runat="server" />
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="lblAMEVisitDate4" runat="server" 
                Text="Unit Status" />
        </td>
        <td>
            <asp:Label ID="lblUnitStatus" runat="server" />
        </td>
    </tr>
    <tr align="left">
        <td nowrap="nowrap" class="style2">
            <asp:Label ID="lblAMEVisitDate" runat="server" Text="<%$Resources:Labels, AMEVisitDate%>" />
        </td>
        <td class="style2">
            <asp:Label runat="server" ID="lblVisitDate" />
        </td>
        <td nowrap="nowrap" class="style2">
            <asp:Label ID="lblAMEVisitDate5" runat="server" 
                Text="Name of AME/Visiting Executive" />
        </td>
        <td class="style2">
            <asp:Label ID="lblAMEVisitName" runat="server" />
        </td>
    </tr>
    <tr align="left">
        <td colspan="2" align="left">
            <asp:GridView HorizontalAlign="Left" ID="grdCustomerMaterialMapping" runat="server"
                AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                Font-Size="Small" AllowPaging="true" PageSize="10" Width="348px" CellPadding="5">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material Type">
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("Cust_Mat_MaterialName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Annual Coal Required per year<br />(In Tonnes)" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("Cust_Mat_AnnualRequirement") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#397dbc" HorizontalAlign="Center" Font-Bold="True" ForeColor="#FFFFFF"
                    Height="20px" />
                <PagerStyle BackColor="#397dbc" ForeColor="#FFFFFF" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Font-Size="Small" Height="20px" HorizontalAlign="Center" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <strong>
                <asp:Label runat="server" ID="lblFolderPath" />
                <asp:Label runat="server" ID="lnkOpenFolder" />
            </strong>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <asp:GridView ID="grdDocument" runat="server" DataKeyNames="Doc_Id" AutoGenerateColumns="False"
                BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" CellPadding="5" Font-Size="Small"
                HorizontalAlign="Center" Width="100%" OnRowDataBound="grdDocument_RowDataBound">
                <EmptyDataTemplate>
                    No Record Found.
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkDocID" runat="server" Checked='<%# Bind("Doc_Mandatory") %>'
                                Enabled="False" />
                                <asp:HiddenField ID="hdnCustDocId"  runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, DocumentName%>">
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("Doc_Name") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, Acronym%>">
                        <ItemTemplate>
                            <asp:Label ID="lblAcronymName" runat="server" Text='<%# Bind("Doc_Acronym") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, DocumentNumber%>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDocNo" MaxLength="20" runat="server"/>
                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtDocNo"
                                FilterMode="ValidChars" ValidChars="-" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" />
                            <asp:CustomValidator ControlToValidate="txtDocNo" ID="DocNoValidator" Display="Dynamic"
                                ValidateEmptyText="true" OnServerValidate="DocNo_ServerValidate" runat="server"
                                ValidationGroup="SaveGroup" ErrorMessage="<%$ Resources:ErrorMessages, RequiredDocNo %>"
                                Text="*" CssClass="failureNotification" />
                            <ajax:ValidatorCalloutExtender ID="DocNoValidatorCalloutExtender" runat="server"
                                TargetControlID="DocNoValidator" />
                           
                            <asp:CustomValidator ControlToValidate="txtDocNo" ID="txtDocNoCustomValidator" Display="Dynamic"
                                ValidateEmptyText="true" OnServerValidate="DocNoExist_ServerValidate" runat="server"
                                ValidationGroup="SaveGroup" ErrorMessage="<%$ Resources:ErrorMessages, DuplicateDocumentNumber %>"
                                Text="*" CssClass="failureNotification" />
                            <ajax:ValidatorCalloutExtender ID="txtDocNoCustomValidatorCalloutExtender" runat="server" TargetControlID="txtDocNoCustomValidator" />
                            
                            <asp:CustomValidator ControlToValidate="txtDocNo" ID="CustomValidator1" Display="Dynamic"
                                ValidateEmptyText="true" OnServerValidate="PAN_ServerValidate" runat="server"
                                ValidationGroup="SaveGroup" ErrorMessage="Input correct PAN Number"
                                Text="*" CssClass="failureNotification" />
                            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="CustomValidator1" />

                            <asp:CustomValidator ControlToValidate="txtDocNo" ID="CustomValidator2" Display="Dynamic"
                                ValidateEmptyText="true" OnServerValidate="TIN_ServerValidate" runat="server"
                                ValidationGroup="SaveGroup" ErrorMessage="TIN Number Must be of 11 digits"
                                Text="*" CssClass="failureNotification" />
                            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="CustomValidator2" />


                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, DocumentExpiryDate%>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDocExDate" runat="server" />
                            <ajax:CalendarExtender ID="CalendarExtender1" Format="dd MMM yyyy" runat="server" TargetControlID="txtDocExDate" />
                            <asp:CustomValidator ControlToValidate="txtDocExDate" ID="DocExDateValidator" OnServerValidate="DocExDate_ServerValidate"
                                Text="*" Display="Dynamic" ValidateEmptyText="true" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, RequiredDocExpiryDate %>"
                                ValidationGroup="SaveGroup" CssClass="failureNotification" />
                            <ajax:ValidatorCalloutExtender ID="DocExDateValidatorCalloutExtender" runat="server"
                                TargetControlID="DocExDateValidator" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, ScanAndSave%>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkScanComplete" AutoPostBack="true" runat="server" OnCheckedChanged="chkScanComplete_Checked"  />
                            <asp:CustomValidator ID="ScanCompleteValidator" Display="Dynamic" ValidationGroup="CheckBoxGroup"
                                runat="server" Text="*" CssClass="failureNotification" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Labels, FileName%>" HeaderStyle-Width="160px">
                        <ItemTemplate>
                            <asp:Label ID="lblFileName" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#397dbc" Font-Bold="True" ForeColor="#FFFFFF" Height="20px" />
                <PagerStyle BackColor="#397dbc" ForeColor="#FFFFFF" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Font-Size="Small" Height="20px" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
<div>
    <asp:CustomValidator ID="gridValidator" Display="Dynamic" runat="server" CssClass="failureNotification" />
    <uc1:MessageBox ID="ucMessageBox" runat="server" />
</div>
<div>
    <table width="100%">
        <tr>
            <td align="left">
                <asp:Button ID="btnNewRegistration" runat="server" CssClass="button" Text="New Registration"
                    OnClick="btnNewRegistration_Click" Visible="False" />
            </td>
            <td align="center" id="btnCenterArea" runat="server">
                <asp:Button ID="btnAddPatInfo" runat="server" CssClass="button" Text="Add Partners Information"
                    OnClick="btnAddAuthRep_Click" Visible="False" />
            </td>
            <td align="right" id="btnRightArea" runat="server">
                <asp:Button ID="btnSave" runat="server" CssClass="button" OnClick="btnSave_Click"
                    Text="Save and Upload" ValidationGroup="SaveGroup" />
                <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" OnClick="btnPrint_Click" />
                <asp:Button ID="btnCancel" Visible="false" CssClass="button" runat="server" Text="Cancel"
                    OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</div>
<div>
    &nbsp;
</div>
