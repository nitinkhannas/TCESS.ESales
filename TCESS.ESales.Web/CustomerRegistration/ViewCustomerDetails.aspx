<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ViewCustomerDetails.aspx.cs" Inherits="CustomerRegistration_ViewCustomerDetails" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <label class="pageNameContent">
        View Customer
    </label>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="100%" cellspacing="10" cellpadding="5">
        <tr align="left">
            <td nowrap="nowrap">
                <label for="lblTradeName" class="formlabel">
                    Trade Name</label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblTradeName" />
            </td>
            <td nowrap="nowrap">
                <label for="lblFirmName" class="formlabel">
                    Firm Name</label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblFirmName" />
            </td>
        </tr>
        <tr align="left">
            <td nowrap="nowrap">
                <label for="lblOwnershipStatus" class="formlabel">
                    Ownership Status</label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblOwnershipStatus" />
            </td>
            <td nowrap="nowrap">
                <label for="lblOwnerName" class="formlabel">
                    Owner/Partner/Director Name</label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblOwnerName" />
            </td>
        </tr>
        <tr align="left">
            <td nowrap="nowrap">
                <label for="lblPartnerMobileNumber" class="formlabel">
                    Partner Mobile Number</label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblPartnerMobileNumber" />
            </td>
            <td nowrap="nowrap">
                <label for="lblFatherName" class="formlabel">
                    Father Name</label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblFatherName" />
            </td>
        </tr>
        <tr align="left">
            <td nowrap="nowrap" valign="top">
                <label for="lblRegisteredAddress" class="formlabel">
                    Registered Address</label>
            </td>
            <td class="wordbreak">
                <asp:Label runat="server" ID="lblRegisteredAddress" />
            </td>
            <td nowrap="nowrap" valign="top">
                <label for="lblUnitAddress" class="formlabel">
                    Unit Address</label>
            </td>
            <td class="wordbreak">
                <asp:Label runat="server" ID="lblUnitAddress" />
            </td>
        </tr>
        <tr align="left">
            <td nowrap="nowrap">
                <label for="lblLandmark" class="formlabel">
                    Landmark</label>
            </td>
            <td class="wordbreak">
                <asp:Label runat="server" ID="lblLandmark" />
            </td>
            <td nowrap="nowrap">
                <label for="lblAMEOffice" class="formlabel">
                    AME Office</label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblAMEOffice" />
            </td>
        </tr>
        <tr align="left">
            <td nowrap="nowrap">
                <label for="lblState" class="formlabel">
                    State</label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblState" />
            </td>
            <td nowrap="nowrap">
                <label for="lblDistrict" class="formlabel">
                    District</label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblDistrict" />
            </td>
        </tr>
        <tr align="left">
            <td nowrap="nowrap">
                <label for="lblPinCode" class="formlabel">
                    PIN Code</label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblPinCode" />
            </td>
            <td nowrap="nowrap">
                <label for="lblBusinessType" class="formlabel">
                    Business Type</label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblBusinessType" />
            </td>
        </tr>
        <tr align="left">
            <td nowrap="nowrap">
                <label for="lblMobileNo" class="formlabel">
                    Mobile Number</label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblMobileNo" />
            </td>
            <td nowrap="nowrap">
                <label for="lblPhoneNumber" class="formlabel">
                    Phone Number</label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblPhoneNumber" />
            </td>
        </tr>
        <tr align="left">
            <td nowrap="nowrap">
                <label for="lblSalesType" class="formlabel">
                    AME Visit Date</label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblVisitDate" />
            </td>
            <td nowrap="nowrap">
                <label for="lblSalesType" class="formlabel">
                    Sales Type</label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblSalesType" />
            </td>
        </tr>
        <tr align="left">
            <td colspan="2" align="left">
                <asp:GridView HorizontalAlign="Left" ID="grdCustomerMaterialMapping" runat="server"
                    AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                    Font-Size="Small" AllowPaging="true" PageSize="10" Width="348px" CellPadding="5">
                    <Columns>
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
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:GridView ID="grdDocument" runat="server" AutoGenerateColumns="False" BorderColor="#3366CC"
                    BorderStyle="Solid" BorderWidth="1px" CellPadding="5" Font-Size="Small" HorizontalAlign="Center"
                    Width="100%" OnRowCommand="grdDocument_RowCommand">
                    <EmptyDataTemplate>
                        No Record Found.
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Document Name">
                            <ItemTemplate>
                                <asp:Label ID="lblname" runat="server" Text='<%# Bind("Cust_Doc_DocName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acronym">
                            <ItemTemplate>
                                <asp:Label ID="lblAcronymName" runat="server" Text='<%# Bind("Cust_Doc_DocAcroName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Document Number">
                            <ItemTemplate>
                                <asp:Label ID="lblDocNumber" runat="server" Text='<%# Bind("Cust_Doc_No") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Document Expiry Date">
                            <ItemTemplate>
                                <asp:Label ID="lblExpireDate" runat="server" Text='<%# Bind("Cust_Doc_ExDate","{0:dd MMM yyyy}") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkViewDoc" runat="server" CommandArgument='<%#Bind("Cust_Doc_Id") %>'
                                    CausesValidation="true" Font-Underline="False" CommandName="ViewDoc" Text="View" />
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
    <table width="100%" cellspacing="5" cellpadding="5">
        <tr align="left">
            <td>
                <asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptTruck_ItemDataBound">
                    <HeaderTemplate>
                        <table width="100%" cellspacing="5" cellpadding="5">
                            <tr align="center">
                                <td>
                                    <label class=" pageNameContent">
                                        Truck Registration Details
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table width="100%" cellspacing="5" cellpadding="5">
                            <label class="heading">
                                Truck
                                <asp:Label ID="Label1" runat="server" Text='<%#Container.ItemIndex +1 %>' />
                                Details
                            </label>
                            <tr align="left">
                                <td>
                                    <label for="lblRegistrationNumber" class="formlabel">
                                        Registration Number</label>
                                </td>
                                <td>
                                    <asp:Label ID="txtTruck_Id" Visible="false" runat="server" Text='<%# Bind("Truck_Id") %>' />
                                    <asp:Label ID="txtTruckRegNo" runat="server" Text='<%# Bind("Truck_RegNo") %>' />
                                </td>
                                <td>
                                    <label for="lblOwnerName" class="formlabel">
                                        Owner Name</label>
                                </td>
                                <td>
                                    <asp:Label ID="txtOwnerName" runat="server" Text='<%# Bind("Truck_OwnerName") %>' />
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <label for="lblDriverName" class="formlabel">
                                        Driver Name</label>
                                </td>
                                <td>
                                    <asp:Label ID="txtDriverName" runat="server" Text='<%# Bind("Truck_DriverName") %>' />
                                </td>
                                <td>
                                    <label for="lblTruckMake" class="formlabel">
                                        Truck Make</label>
                                </td>
                                <td>
                                    <asp:Label ID="txtTruckMake" runat="server" Text='<%# Bind("TruckMake_Name") %>' />
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <label for="lblWheeler" class="formlabel">
                                        Number of Wheels</label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWheeler" runat="server" Text='<%# Bind("TruckWheeler_Type") %>' />
                                </td>
                                <td>
                                    <label for="lblState" class="formlabel">
                                        Carry Capacity
                                        <br />
                                        (In Tonnes)</label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCarryCapacity" runat="server" Text='<%# Bind("TruckCarryCapacity_Type") %>' />
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <label for="lblRegisteredAddress" class="formlabel">
                                        Registered Address</label>
                                </td>
                                <td>
                                    <asp:Label ID="txtRegAddress" runat="server" Text='<%# Bind("Truck_Address") %>' />
                                </td>
                                <td>
                                    <label for="lblState" class="formlabel">
                                        State</label>
                                </td>
                                <td>
                                    <asp:Label ID="ddlStates" runat="server" Text='<%# Bind("Truck_State_Name") %>' />
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <label for="lblPhoneNumber" class="formlabel">
                                        Mobile Number</label>
                                </td>
                                <td>
                                    <asp:Label ID="txtMobileNo" runat="server" Text='<%# Bind("Truck_MobileNo") %>' />
                                </td>
                                <td>
                                    <label for="lblPhoneNo" class="formlabel">
                                        Phone Number</label>
                                </td>
                                <td>
                                    <asp:Label ID="txtPhoneNo" runat="server" Text='<%# Bind("Truck_PhoneNo") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:GridView ID="grdTruckDocument" runat="server" AutoGenerateColumns="False" BorderColor="#3366CC"
                                        BorderStyle="Solid" BorderWidth="1px" Font-Size="Small" AllowPaging="true" PageSize="10"
                                        HorizontalAlign="Center" Width="100%" CellPadding="5" OnRowCommand="grdTruckDocument_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Document Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblname" runat="server" Text='<%# Bind("Truck_Doc_DocName") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Acronym Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAcronymName" runat="server" Text='<%# Bind("Truck_Doc_DocAcroName") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Document Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtDocNo" runat="server" Text='<%# Bind("Truck_Doc_DocNo") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Document Expiry Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtDocExDate" runat="server" Text='<%# Bind("Truck_Doc_ExDate","{0:dd MMM yyyy}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkViewDoc" runat="server" CommandArgument='<%#Bind("Truck_Doc_Id") %>'
                                                        CausesValidation="true" Font-Underline="False" CommandName="ViewDoc" Text="View" />
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
                    </ItemTemplate>
                </asp:Repeater>
            </td>
        </tr>
    </table>
    <table width="100%" cellspacing="5" cellpadding="5">
        <tr align="left">
            <td>
                <asp:Repeater ID="rptAuthRep" runat="server" OnItemDataBound="rptAuthRep_ItemDataBound">
                    <HeaderTemplate>
                        <table width="100%" cellspacing="5" cellpadding="5">
                            <tr align="center">
                                <td>
                                    <label class=" pageNameContent">
                                        Authorized Representative Details
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <label class="heading">
                            Representative
                            <asp:Label ID="Label1" runat="server" Text='<%#Container.ItemIndex +1 %>' />
                            Detail
                        </label>
                        <table width="100%" cellspacing="5" cellpadding="5" class="formtext">
                            <tr align="left">
                                <td>
                                    <asp:Label ID="lblAuthName" runat="server" Text='Name' class="formlabel" />
                                </td>
                                <td>
                                    <asp:Label ID="txtAuthID" runat="server" Visible="false" Text='<%# Bind("AuthRep_Id") %>' />
                                    <asp:Label ID="txtAuthName" runat="server" Text='<%# Bind("AuthRep_Name") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="lblAuthFatherName" runat="server" class="formlabel" Text='Father’s Name' />
                                </td>
                                <td>
                                    <asp:Label ID="txtAuthFatherName" runat="server" Text='<%# Bind("AuthRep_FatherName") %>' />
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <asp:Label ID="lblAddress" runat="server" class="formlabel" Text='Address' />
                                </td>
                                <td>
                                    <asp:Label ID="txtAddress" runat="server" Text='<%# Bind("AuthRep_Address") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="lblMobileNumber" runat="server" class="formlabel" Text='Mobile Number' />
                                </td>
                                <td>
                                    <asp:Label ID="txtMobileNumber" runat="server" Text='<%# Bind("AuthRep_Mobile") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <asp:GridView ID="grdAuthRepDocument" runat="server" AutoGenerateColumns="False"
                                        BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                                        AllowPaging="true" PageSize="10" HorizontalAlign="Center" Width="100%" CellPadding="5"
                                        OnRowCommand="grdAuthRepDocument_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Document Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblname" runat="server" Text='<%# Bind("AuthRep_Doc_DocName") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Acronym Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAcronymName" runat="server" Text='<%# Bind("AuthRep_Doc_DocAcroName") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Document Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtDocNo" Text='<%# Bind("AuthRep_Doc_DocNo") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Document Expiry Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtDocExDate" runat="server" Text='<%#Bind("AuthRep_Doc_ExDate","{0:dd MMM yyyy}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkViewDoc" runat="server" CommandArgument='<%#Bind("AuthRep_Doc_Id") %>'
                                                        CausesValidation="true" Font-Underline="False" CommandName="ViewDoc" Text="View" />
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
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
            </td>
        </tr>
    </table>
    <table width="100%" cellspacing="10" cellpadding="5">
        <tr>
            <td align="left" runat="server" id="UploadFileArea">
                <label for="lblTradeName" class="formlabel">
                    Upload File</label>
                &nbsp;
                <asp:FileUpload ID="filAuthDoc" runat="server" />
            </td>
            <td align="right" runat="server" id="ButtonArea">
                <asp:Button ID="btnSaveAndUpload" CssClass="button" runat="server" Text="Save and Upload"
                    OnClick="btnSaveAndUpload_Click" />
                <asp:Button ID="btnAuthCertificate" Visible="false" CssClass="button" runat="server" Text="View Authorization Certificate"
                    OnClick="btnAuthCertificate_Click" />
                &nbsp;<asp:Button ID="btnClose" CssClass="button" runat="server" Text="Close" OnClick="btnClose_Click" />
            </td>
        </tr>
    </table>
    <div>
        <asp:CustomValidator ID="customValidator" Display="Dynamic" runat="server" CssClass="failureNotification" />
        <uc1:MessageBox ID="ucMessageBox" runat="server" />
        <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
    </div>
</asp:Content>
