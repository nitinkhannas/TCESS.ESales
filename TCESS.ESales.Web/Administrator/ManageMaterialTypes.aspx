<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageMaterialTypes.aspx.cs" Inherits="Administrator_ManageMaterialTypes"
    ValidateRequest="false" %>

<%@ Register TagPrefix="Custom" Namespace="AlwaysShowHeaderFooter" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManageMaterialInformation%>"
        CssClass="pageNameContent" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="progressBar" runat="server" AssociatedUpdatePanelID="uplMainPanel"
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
    <asp:UpdatePanel runat="server" ID="uplMainPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="overflow: auto; width: 100%;">
                <Custom:GridViewAlwaysShow AllowPaging="true" ID="grdMaterialType" runat="server"
                    AutoGenerateColumns="False" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                    Font-Size="Small" PageSize="10" Width="100%" HorizontalAlign="Center" CellPadding="5"
                    ShowFooter="True" OnRowEditing="grdMaterialType_RowEditing" DataKeyNames="MaterialType_Id"
                    OnRowUpdating="grdMaterialType_RowUpdating" OnRowCommand="grdMaterialType_RowCommand"
                    OnRowCancelingEdit="grdMaterialType_RowCancelingEdit" OnRowDeleting="grdMaterialType_RowDeleting"
                    OnMustAddARow="grdMaterialType_MustAddARow" OnPageIndexChanging="grdMaterialType_PageIndexChanging">
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoRecordsFound" runat="server" Text="<%$Resources:Labels, NoRecordsFound%>" />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, MaterialCode%>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtProductCode" runat="server" Text='<%# Bind("MaterialType_Code") %>'
                                    MaxLength="5" Width="50px" />
                                <asp:RequiredFieldValidator ID="txtProductCodeValidator" ControlToValidate="txtProductCode"
                                    Display="Dynamic" ValidationGroup="EditMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredMaterialTypeCode%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtProductCodeValidatorCallOut" runat="server"
                                    TargetControlID="txtProductCodeValidator" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewProductCode" runat="server" Width="50px" MaxLength="5" />
                                <asp:RequiredFieldValidator ID="txtNewProductCodeValidator" ControlToValidate="txtNewProductCode"
                                    Display="Dynamic" ValidationGroup="AddMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredMaterialTypeCode%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtNewProductCodeValidatorCallOut" runat="server"
                                    TargetControlID="txtNewProductCodeValidator" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# Eval("MaterialType_Code") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="<%$Resources:Labels, MaterialName%>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtProductName" MaxLength="100" Width="100px" runat="server" Text='<%# Bind("MaterialType_Name") %>' />
                                <asp:RequiredFieldValidator ID="txtProductNameValidator" ControlToValidate="txtProductName"
                                    Display="Dynamic" ValidationGroup="EditMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredMaterialTypeName%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtProductNameValidatorCallOut" runat="server"
                                    TargetControlID="txtProductNameValidator" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewProductName" MaxLength="100" Width="100px" runat="server" />
                                <asp:RequiredFieldValidator ID="txtNewProductNameValidator" ControlToValidate="txtNewProductName"
                                    Display="Dynamic" ValidationGroup="AddMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredMaterialTypeName%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtNewProductNameValidatorCallOut" runat="server"
                                    TargetControlID="txtNewProductNameValidator" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# Eval("MaterialType_Name") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, TiscoRate%>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtTiscoRate" Width="60px" runat="server" Text='<%# Bind("MaterialType_TiscoRate") %>'
                                    MaxLength="7" />
                                <asp:RequiredFieldValidator ID="TiscoRateValidator" ControlToValidate="txtTiscoRate"
                                    Display="Dynamic" ValidationGroup="EditMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredTiscoRate%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="TiscoRateValidatorCallOut" runat="server" TargetControlID="TiscoRateValidator" />
                                <asp:RegularExpressionValidator ID="TiscoRateRegExpValidator" runat="server" ControlToValidate="txtTiscoRate"
                                    ValidationExpression="\d+\.?\d*" Display="Dynamic" ValidationGroup="EditMaterialType"
                                    SetFocusOnError="true" Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredDecimalNumbers %>" />
                                <ajax:ValidatorCalloutExtender ID="TiscoRateRegExpValidatorCallOut" runat="server"
                                    TargetControlID="TiscoRateRegExpValidator" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewTiscoRate" Width="60px" runat="server" MaxLength="7" />
                                <asp:RequiredFieldValidator ID="NewVATRateValidator" ControlToValidate="txtNewTiscoRate"
                                    Display="Dynamic" ValidationGroup="AddMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredTiscoRate%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="NewTiscoRateValidatorCallOut" runat="server" TargetControlID="NewVATRateValidator" />
                                <asp:RegularExpressionValidator ID="NewTiscoRateRegExpValidator" runat="server" ControlToValidate="txtNewTiscoRate"
                                    ValidationExpression="\d+\.?\d*" Display="Dynamic" ValidationGroup="AddMaterialType"
                                    SetFocusOnError="true" Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredDecimalNumbers %>" />
                                <ajax:ValidatorCalloutExtender ID="NewTiscoRateRegExpValidatorCallOut" runat="server"
                                    TargetControlID="NewTiscoRateRegExpValidator" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# Math.Round(Convert.ToDecimal(Eval("MaterialType_TiscoRate")),2) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, CSTRate%>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCSTRate" runat="server" Text='<%# Bind("MaterialType_CSTRate") %>'
                                    Width="60px" MaxLength="7" />
                                <asp:RequiredFieldValidator ID="txtCSTRateValidator" ControlToValidate="txtCSTRate"
                                    Display="Dynamic" ValidationGroup="EditMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredCSTRate%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtCSTRateValidatorCallOut" runat="server" TargetControlID="txtCSTRateValidator" />
                                <asp:RegularExpressionValidator ID="txtCSTRateRegExpValidator" runat="server" ControlToValidate="txtCSTRate"
                                    ValidationExpression="\d+\.?\d*" Display="Dynamic" ValidationGroup="EditMaterialType"
                                    SetFocusOnError="true" Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredDecimalNumbers %>"></asp:RegularExpressionValidator>
                                <ajax:ValidatorCalloutExtender ID="txtCSTRateRegExpValidatorCallOut" runat="server"
                                    TargetControlID="txtCSTRateRegExpValidator" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewCSTRate" Width="60px" runat="server" MaxLength="7" />
                                <asp:RequiredFieldValidator ID="txtNewCSTRateValidator" ControlToValidate="txtNewCSTRate"
                                    Display="Dynamic" ValidationGroup="AddMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredCSTRate%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtNewCSTRateValidatorCallOut" runat="server"
                                    TargetControlID="txtNewCSTRateValidator" />
                                <asp:RegularExpressionValidator ID="txtNewCSTRateRegExpValidator" runat="server"
                                    ControlToValidate="txtNewCSTRate" ValidationExpression="\d+\.?\d*" Display="Dynamic"
                                    ValidationGroup="AddMaterialType" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                                    ErrorMessage="<%$ Resources:ErrorMessages, RequiredDecimalNumbers %>"></asp:RegularExpressionValidator>
                                <ajax:ValidatorCalloutExtender ID="txtNewCSTRateRegExpValidatorCallOut" runat="server"
                                    TargetControlID="txtNewCSTRateRegExpValidator" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# Math.Round(Convert.ToDecimal(Eval("MaterialType_CSTRate")),2) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, CFormRate%>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCFormRate" Width="60px" runat="server" Text='<%#Bind("MaterialType_CFormRate")%>'
                                    MaxLength="7" />
                                <asp:RequiredFieldValidator ID="txtNewCSTRateValidatorValidator" ControlToValidate="txtCFormRate"
                                    Display="Dynamic" ValidationGroup="EditMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredCFormTRate%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtNewCSTRateValidatorCallOut" runat="server"
                                    TargetControlID="txtNewCSTRateValidatorValidator" />
                                <asp:RegularExpressionValidator ID="txtCFormRateRegExpValidator" runat="server" ControlToValidate="txtCFormRate"
                                    ValidationExpression="\d+\.?\d*" Display="Dynamic" ValidationGroup="EditMaterialType"
                                    SetFocusOnError="true" Text="*" CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredDecimalNumbers %>"></asp:RegularExpressionValidator>
                                <ajax:ValidatorCalloutExtender ID="txtCFormRateRegExpValidatorCallOut" runat="server"
                                    TargetControlID="txtCFormRateRegExpValidator" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewCFormRate" Width="60px" runat="server" MaxLength="7" />
                                <asp:RequiredFieldValidator ID="txtNewCFormRateValidator" ControlToValidate="txtNewCFormRate"
                                    Display="Dynamic" ValidationGroup="AddMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredCFormTRate%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtNewCFormRateValidatorCallOut" runat="server"
                                    TargetControlID="txtNewCFormRateValidator" />
                                <asp:RegularExpressionValidator ID="txtNewCFormRateRegExpValidator" runat="server"
                                    ControlToValidate="txtNewCFormRate" ValidationExpression="\d+\.?\d*" Display="Dynamic"
                                    ValidationGroup="AddMaterialType" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                                    ErrorMessage="<%$ Resources:ErrorMessages, RequiredDecimalNumbers %>"></asp:RegularExpressionValidator>
                                <ajax:ValidatorCalloutExtender ID="txtNewCFormRateRegExpValidatorCallOut" runat="server"
                                    TargetControlID="txtNewCFormRateRegExpValidator" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%#Math.Round(Convert.ToDecimal(Eval("MaterialType_CFormRate")),2)%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, HandlingRate%>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtHandlingRate" runat="server" Text='<%# Bind("MaterialType_HandlingRate") %>'
                                    Width="60px" MaxLength="7" />
                                <asp:RequiredFieldValidator ID="txtHandlingRateValidator" ControlToValidate="txtHandlingRate"
                                    Display="Dynamic" ValidationGroup="EditMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredHandlingRate%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtHandlingRateValidatorCallOut" runat="server"
                                    TargetControlID="txtHandlingRateValidator" />
                                <asp:RegularExpressionValidator ID="txtHandlingRateRegExpValidator" runat="server"
                                    ControlToValidate="txtHandlingRate" ValidationExpression="\d+\.?\d*" Display="Dynamic"
                                    ValidationGroup="EditMaterialType" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                                    ErrorMessage="<%$ Resources:ErrorMessages, RequiredDecimalNumbers %>"></asp:RegularExpressionValidator>
                                <ajax:ValidatorCalloutExtender ID="txtHandlingRateRegExpValidatorCallOut" runat="server"
                                    TargetControlID="txtHandlingRateRegExpValidator" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewHandlingRate" runat="server" Width="60px" MaxLength="7" />
                                <asp:RequiredFieldValidator ID="txtNewHandlingRateValidator" ControlToValidate="txtNewHandlingRate"
                                    Display="Dynamic" ValidationGroup="AddMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredHandlingRate%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtNewHandlingRateValidatorCallOut" runat="server"
                                    TargetControlID="txtNewHandlingRateValidator" />
                                <asp:RegularExpressionValidator ID="txtNewHandlingRateRegExpValidator" runat="server"
                                    ControlToValidate="txtNewHandlingRate" ValidationExpression="\d+\.?\d*" Display="Dynamic"
                                    ValidationGroup="AddMaterialType" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                                    ErrorMessage="<%$ Resources:ErrorMessages, RequiredDecimalNumbers %>"></asp:RegularExpressionValidator>
                                <ajax:ValidatorCalloutExtender ID="txtNewHandlingRateRegExpValidatorCallOut" runat="server"
                                    TargetControlID="txtNewHandlingRateRegExpValidator" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%#Math.Round(Convert.ToDecimal(Eval("MaterialType_HandlingRate")),2) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, ServiceTax%>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtServiceTax" runat="server" Text='<%# Bind("MaterialType_ServiceTax") %>'
                                    Width="60px" MaxLength="7" />
                                <asp:RequiredFieldValidator ID="txtServiceTaxValidator" ControlToValidate="txtServiceTax"
                                    Display="Dynamic" ValidationGroup="EditMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredServiceTax%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtServiceTaxValidatorCallOut" runat="server"
                                    TargetControlID="txtServiceTaxValidator" />
                                <asp:RegularExpressionValidator ID="txtServiceTaxRegExpValidator" runat="server"
                                    ControlToValidate="txtServiceTax" ValidationExpression="\d+\.?\d*" Display="Dynamic"
                                    ValidationGroup="EditMaterialType" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                                    ErrorMessage="<%$ Resources:ErrorMessages, RequiredDecimalNumbers %>"></asp:RegularExpressionValidator>
                                <ajax:ValidatorCalloutExtender ID="txtServiceTaxRegExpValidatorCallOut" runat="server"
                                    TargetControlID="txtServiceTaxRegExpValidator" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewServiceTax" runat="server" Width="60px" MaxLength="7" />
                                <asp:RequiredFieldValidator ID="txtNewServiceTaxValidator" ControlToValidate="txtNewServiceTax"
                                    Display="Dynamic" ValidationGroup="AddMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredServiceTax%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtNewServiceTaxValidatorCallOut" runat="server"
                                    TargetControlID="txtNewServiceTaxValidator" />
                                <asp:RegularExpressionValidator ID="txtNewServiceTaxRegExpValidator" runat="server"
                                    ControlToValidate="txtNewServiceTax" ValidationExpression="\d+\.?\d*" Display="Dynamic"
                                    ValidationGroup="AddMaterialType" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                                    ErrorMessage="<%$ Resources:ErrorMessages, RequiredDecimalNumbers %>"></asp:RegularExpressionValidator>
                                <ajax:ValidatorCalloutExtender ID="txtNewServiceTaxRegExpValidatorCallOut" runat="server"
                                    TargetControlID="txtNewServiceTaxRegExpValidator" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# Math.Round(Convert.ToDecimal(Eval("MaterialType_ServiceTax")),2) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, EducationCess%>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEducationCess" runat="server" Text='<%# Bind("MaterialType_EducationCess") %>'
                                    Width="60px" MaxLength="7" />
                                <asp:RequiredFieldValidator ID="txtEducationCessValidator" ControlToValidate="txtEducationCess"
                                    Display="Dynamic" ValidationGroup="EditMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredEducationCess%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtEducationCessValidatorCallOut" runat="server"
                                    TargetControlID="txtEducationCessValidator" />
                                <asp:RegularExpressionValidator ID="txtEducationCessRegExpValidator" runat="server"
                                    ControlToValidate="txtEducationCess" ValidationExpression="\d+\.?\d*" Display="Dynamic"
                                    ValidationGroup="EditMaterialType" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                                    ErrorMessage="<%$ Resources:ErrorMessages, RequiredDecimalNumbers %>"></asp:RegularExpressionValidator>
                                <ajax:ValidatorCalloutExtender ID="txtEducationCessRegExpValidatorCallOut" runat="server"
                                    TargetControlID="txtEducationCessRegExpValidator" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewEducationCess" runat="server" Width="60px" MaxLength="7" />
                                <asp:RequiredFieldValidator ID="txtNewEducationCessValidator" ControlToValidate="txtNewEducationCess"
                                    Display="Dynamic" ValidationGroup="AddMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredEducationCess%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtNewEducationCessValidatorCallOut" runat="server"
                                    TargetControlID="txtNewEducationCessValidator" />
                                <asp:RegularExpressionValidator ID="txtNewEducationCessRegExpValidator" runat="server"
                                    ControlToValidate="txtNewEducationCess" ValidationExpression="\d+\.?\d*" Display="Dynamic"
                                    ValidationGroup="AddMaterialType" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                                    ErrorMessage="<%$ Resources:ErrorMessages, RequiredDecimalNumbers %>"></asp:RegularExpressionValidator>
                                <ajax:ValidatorCalloutExtender ID="txtNewEducationCessRegExpValidatorCallOut" runat="server"
                                    TargetControlID="txtNewEducationCessRegExpValidator" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%#Math.Round(Convert.ToDecimal(Eval("MaterialType_EducationCess")),2) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, HigherEducationCess%>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtHigherEducationCess" runat="server" Text='<%# Bind("MaterialType_HigherEducationCess") %>'
                                    Width="60px" MaxLength="7" />
                                <asp:RequiredFieldValidator ID="txtHigherEducationCessValidator" ControlToValidate="txtHigherEducationCess"
                                    Display="Dynamic" ValidationGroup="EditMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredHigherEducationCess%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtHigherEducationCessValidatorCallOut" runat="server"
                                    TargetControlID="txtHigherEducationCessValidator" />
                                <asp:RegularExpressionValidator ID="txtHeigherEducationCessRegExpValidator" runat="server"
                                    ControlToValidate="txtHigherEducationCess" ValidationExpression="\d+\.?\d*" Display="Dynamic"
                                    ValidationGroup="EditMaterialType" SetFocusOnError="true" Text="*" CssClass="failureNotification"
                                    ErrorMessage="<%$ Resources:ErrorMessages, RequiredDecimalNumbers %>"></asp:RegularExpressionValidator>
                                <ajax:ValidatorCalloutExtender ID="txtHeigherEducationCessRegExpValidatorCallOut"
                                    runat="server" TargetControlID="txtHeigherEducationCessRegExpValidator" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewHigherEducationCess" runat="server" Width="60px" MaxLength="7" />
                                <asp:RequiredFieldValidator ID="txtNewHigherEducationCessValidator" ControlToValidate="txtNewHigherEducationCess"
                                    Display="Dynamic" ValidationGroup="AddMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$Resources:ErrorMessages, RequiredHigherEducationCess%>"
                                    runat="server" />
                                <ajax:ValidatorCalloutExtender ID="txtNewHigherEducationCessValidatorCallOut" runat="server"
                                    TargetControlID="txtNewHigherEducationCessValidator" />
                                <asp:RegularExpressionValidator ID="txtNewHigherEducationCessRegExpValidator" runat="server"
                                    ControlToValidate="txtNewHigherEducationCess" ValidationExpression="\d+\.?\d*"
                                    Display="Dynamic" ValidationGroup="AddMaterialType" SetFocusOnError="true" Text="*"
                                    CssClass="failureNotification" ErrorMessage="<%$ Resources:ErrorMessages, RequiredDecimalNumbers %>"></asp:RegularExpressionValidator>
                                <ajax:ValidatorCalloutExtender ID="txtNewHigherEducationCessRegExpValidatorCallOut"
                                    runat="server" TargetControlID="txtNewHigherEducationCessRegExpValidator" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%#Math.Round(Convert.ToDecimal(Eval("MaterialType_HigherEducationCess")),2) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Active%>">
                            <EditItemTemplate>
                                <asp:CheckBox ID="chkActive" runat="server" Checked='<%#Bind("MaterialType_IsActive")%>' />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:CheckBox ID="chkNewActive" runat="server" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <%#Convert.ToBoolean(Eval("MaterialType_IsActive")) == true ? "Yes" : "No" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, Action%>">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="<%$Resources:Labels, Update%>" Font-Underline="False" ValidationGroup="EditMaterialType" />
                                <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="<%$Resources:Labels, Cancel%>" Font-Underline="False" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="lnkAdd" runat="server" CausesValidation="true" CommandName="AddNew"
                                    CssClass="button" Text="Add" ValidationGroup="AddMaterialType" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="<%$Resources:Labels, Edit%>" Font-Underline="False" CommandArgument='<%#Bind("MaterialType_Id")%>' />
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text="<%$Resources:Labels, Delete%>" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                                    Font-Underline="False" CommandArgument='<%#Bind("MaterialType_Id")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#397dbc" ForeColor="#003399" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#397dbc" Font-Bold="True" ForeColor="#FFFFFF" Height="20px" />
                    <PagerStyle BackColor="#397dbc" ForeColor="#FFFFFF" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" Font-Size="Small" Height="20px" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
                </Custom:GridViewAlwaysShow>
            </div>
            <div>
                &nbsp;
                <uc2:MessageBoxForGrid ID="ucMessageBoxForGrid" runat="server" />
            </div>
            <div runat="server" id="divHistory">
                <asp:Label ID="lblHistory" runat="server" Text="<%$Resources:Labels, History%>" CssClass="pageNameContent" />
                <Custom:GridViewAlwaysShow AllowPaging="true" ID="gridHistory" runat="server" AutoGenerateColumns="False"
                    BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small"
                    PageSize="10" Width="100%" HorizontalAlign="Center" CellPadding="5" ShowFooter="True"
                    DataKeyNames="MaterialType_Id">
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, SNo%>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, MaterialCode%>">
                            <ItemTemplate>
                                <%# Eval("MaterialType_Code") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="<%$Resources:Labels, MaterialName%>">
                            <ItemTemplate>
                                <%# Eval("MaterialType_Name") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, TiscoRate%>">
                            <ItemTemplate>
                                <%# Math.Round(Convert.ToDecimal(Eval("MaterialType_TiscoRate")),2) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, CSTRate%>">
                            <ItemTemplate>
                                <%# Math.Round(Convert.ToDecimal(Eval("MaterialType_CSTRate")),2) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, CFormRate%>">
                            <ItemTemplate>
                                <%#Math.Round(Convert.ToDecimal(Eval("MaterialType_CFormRate")),2)%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, HandlingRate%>">
                            <ItemTemplate>
                                <%#Math.Round(Convert.ToDecimal(Eval("MaterialType_HandlingRate")),2) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, ServiceTax%>">
                            <ItemTemplate>
                                <%# Math.Round(Convert.ToDecimal(Eval("MaterialType_ServiceTax")),2) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, EducationCess%>">
                            <ItemTemplate>
                                <%#Math.Round(Convert.ToDecimal(Eval("MaterialType_EducationCess")),2) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, HigherEducationCess%>">
                            <ItemTemplate>
                                <%#Math.Round(Convert.ToDecimal(Eval("MaterialType_HigherEducationCess")),2) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Labels, ChangedDate%>">
                            <ItemTemplate>
                                <%# Convert.ToDateTime(Eval("MaterialType_LastUpdatedDate")).ToString("dd MMM yyyy") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#397dbc" ForeColor="#003399" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#397dbc" Font-Bold="True" ForeColor="#FFFFFF" Height="20px" />
                    <PagerStyle BackColor="#397dbc" ForeColor="#FFFFFF" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" Font-Size="Small" Height="20px" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
                </Custom:GridViewAlwaysShow>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
