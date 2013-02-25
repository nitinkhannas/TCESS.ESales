using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCESS.ESales.CommonLayer.CommonLibrary;

public partial class Common_ViewImage : BaseUserControl
{
    public event OkMessageBoxEventHandler Event_OkButton;

    protected void Page_Load(object sender, EventArgs e)
    {
        btnOk.OnClientClick = String.Format("fnClickOK('{0}','{1}')", btnOk.UniqueID, "");
    }

    public void ShowMessage(string docId)
    {        
        imgBlob.ImageUrl = "~/ImageHandler/ShowImage.ashx?GetDocType=" + 1 + "&docId=" + docId;
        mdlMessageBox.Show();
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        mdlMessageBox.Hide();
        Event_OkButton(sender, e);
    }
}