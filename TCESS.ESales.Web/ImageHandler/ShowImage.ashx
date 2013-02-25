<%@ WebHandler Language="C#" Class="ShowImage" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.IO;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

public class ShowImage : IHttpHandler, IRequiresSessionState, IReadOnlySessionState
{
    public void ProcessRequest(HttpContext context)
    {
        byte[] imgDocument = null;
        int documentType = Convert.ToInt32(context.Request.QueryString[Globals.StateMgmtVariables.GETDOCTYPE]);

        switch (documentType)
        {
            case 1:
                imgDocument = GetCustomerDocumentDetails(Convert.ToInt32(context.Request.QueryString[Globals.StateMgmtVariables.DOCID]));
                break;
            case 2:
                imgDocument = GetTruckDocDetails(Convert.ToInt32(context.Request.QueryString[Globals.StateMgmtVariables.DOCID]));
                break;
            case 3:
                imgDocument = GetAuthRepDocDetails(Convert.ToInt32(context.Request.QueryString[Globals.StateMgmtVariables.DOCID]));
                break;
            default:
                break;
        }

        context.Response.Clear();

        // Clear the content of the response
        context.Response.ClearContent();
        context.Response.ClearHeaders();

        context.Response.Buffer = true;
        context.Response.ContentType = "Image/JPEG";
        context.Response.BinaryWrite(imgDocument);

        context.Response.Flush();
        context.Response.Close();
        context.Response.End();
    }

	public bool IsReusable
	{
		get
		{
			return false;
		}
	}
    
	private byte[] GetCustomerDocumentDetails(int documentId)
	{
        CustomerDocumentsDTO doctype = ESalesUnityContainer.Container.Resolve<ICustomerDocService>()
                    .GetCustomerDocumentDetailsByCustDocId(documentId);
        return doctype.CustDoc_File;
	}

    private byte[] GetAuthRepDocDetails(int documentId)
	{
		AuthRepDocumentsDTO doctype = ESalesUnityContainer.Container.Resolve<IAuthRepService>()
                    .GetAuthRepDocDetailsByDocId(documentId);
        return doctype.AuthRepDoc_File;
	}

    private byte[] GetTruckDocDetails(int documentId)
	{
		TruckDocumentsDTO doctype = ESalesUnityContainer.Container.Resolve<ITruckDocService>()
                    .GetTruckDocDetailsByTruckDocId(documentId);
        return doctype.TruckDoc_File;
	}
}