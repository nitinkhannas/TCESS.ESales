using System;
using TCESS.ESales.CommonLayer.CommonLibrary;
using System.IO;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using Microsoft.Practices.Unity;

public partial class ImageHandler_ImagePopUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (Session[Globals.StateMgmtVariables.DOCID] != null)
		{
            if (Convert.ToInt32(Session[Globals.StateMgmtVariables.DOCTYPE]) != 4)
            {
                imgBlob.ImageUrl = "~/ImageHandler/ShowImage.ashx?GetDocType=" + Session[Globals.StateMgmtVariables.DOCTYPE]
                    + "&docId=" + Session[Globals.StateMgmtVariables.DOCID];
            }
            else
            {
                byte[] imgDocument = GetCustomerAuthorizationDetails(Convert.ToInt32(Session[Globals.StateMgmtVariables.DOCID]));

                Response.Clear();

                // Clear the content of the response
                Response.ClearContent();
                Response.ClearHeaders();

                // Buffer response so that page is sent
                // after processing is complete.
                Response.BufferOutput = true;

                // Add the file name and attachment,
                // which will force the open/cance/save dialog to show, to the header
                Response.AddHeader("Content-Disposition", "inline;filename=AuthorizationCertificate.pdf");
                byte[] pdf = imgDocument;

                // Add the file size into the response header
                Response.AddHeader("Content-Length", imgDocument.Length.ToString());

                // Set the ContentType
                Response.ContentType = "Application/pdf";

                using (MemoryStream ms = new MemoryStream(pdf))
                {
                    ms.WriteTo(Response.OutputStream);
                }

                Response.Flush();
                Response.Close();
                Response.End();
            }
		}
    }

    private byte[] GetCustomerAuthorizationDetails(int customerId)
    {
        CustAuthorizationDetailDTO custAuthDetails = ESalesUnityContainer.Container.Resolve<ICustAuthorizationService>()
            .GetCustomerAuthorizationDetails(customerId);
        return custAuthDetails.CustAuth_File;
    }
}