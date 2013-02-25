<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageStandaloneTrucks.aspx.cs" Inherits="Supervisor_ManageStandaloneTrucks" %>

<%@ Register Src="../CustomerRegistration/UserControls/AddEditStandaloneTrucks.ascx"
    TagName="AddEditStandaloneTrucks" TagPrefix="uc1" %>
<%@ Register Src="UserControls/ManageStandaloneTrucks.ascx" TagName="ManageStandaloneTrucks"
    TagPrefix="uc2" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageNameContent" runat="Server">
    <asp:Label ID="lblPageName" runat="server" Text="<%$Resources:Labels, ManageTransporterTrucks%>"
        CssClass="pageNameContent" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <script language="javascript" type="text/javascript">
        function createfolder(filepath) {
            var x = new ActiveXObject("AxControls.HelloWorld");
            x.CreateFolder(filepath);
        }

        function ReadFiles(obj, hdnBytes, lblFilename, hdnFileName, lblAcronym) {
            try {

                if (document.getElementById(obj).checked) {

                    if (document.getElementById('MainContent_ucAddEditStandaloneTrucks_txtTruckRegNo').value == "") {
                        ValidatorEnable(document.getElementById('MainContent_ucAddEditStandaloneTrucks_TruckRegNoValidator'), true)
                        return false;
                    }
                    else {
                        var acronym = document.getElementById(lblAcronym).innerHTML;
                        var foldobj = new ActiveXObject("AxControls.HelloWorld");
                        var found = 0;
                        var fileName = "";

                        var filePath = "C:\\CustomerGeneration\\TruckInfo\\" + document.getElementById('MainContent_ucAddEditStandaloneTrucks_txtTruckRegNo').value;
                        createfolder(filePath);

                        var ObjMyFolder = new ActiveXObject("Scripting.FileSystemObject");
                        var myfolder = ObjMyFolder.GetFolder(filePath);
                        var fil_col = myfolder.Files;
                        var flag = 0;
                        var en = new Enumerator(fil_col); 0.
                        for (; !en.atEnd(); en.moveNext()) {
                            var objFileIO = ObjMyFolder.GetFile(filePath + "\\" + en.item().Name);

                            if (objFileIO.Path.indexOf(acronym) > -1) {
                                fileName = objFileIO.Name.toString();
                                flag = 1;
                                break;
                            }
                        }

                        if (flag == 1) {
                            document.getElementById(hdnBytes).value = foldobj.ConvertImageToByteArray(filePath + "\\" + fileName);
                            document.getElementById(lblFilename).innerHTML = fileName;
                            document.getElementById(hdnFileName).value = fileName
                            return true;
                        }
                        else {
                            document.getElementById(hdnFileName).value = "";
                            return true;
                        }
                    }
                }
                else {

                    document.getElementById(lblFilename).innerHTML = "";
                    document.getElementById(hdnBytes).value = "";
                }
            }
            catch (e) {
                __doPostBack('"+document.getElementById(obj)+"', '');

            }
        }
    </script>
    <asp:UpdateProgress ID="progressBar" runat="server" AssociatedUpdatePanelID="uplMainPanel"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="overlay">
                <div class="ajaxloader">
                    <img src='<%= ResolveClientUrl("~/Images/ajax-loader.gif")%>' style="vertical-align: middle"
                        alt="Processing" />Processing....
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="uplMainPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlAddEditStandaloneTrucks" runat="server">
                <uc1:AddEditStandaloneTrucks ID="ucAddEditStandaloneTrucks" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlManageStandaloneTrucks" runat="server">
                <uc2:ManageStandaloneTrucks ID="ucManageStandaloneTrucks" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
