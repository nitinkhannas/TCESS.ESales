#region Namespace

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class Masters_ManageDocumentType : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Get document type from database depending upon the group selected from dropdown
            GetDocumentTypes();
        }
    }

    /// <summary>
    /// Get document type from database depending upon the group selected from dropdown
    /// </summary>
    private void GetDocumentTypes()
    {
        if (ddldocGroup.SelectedIndex > 0)
        {
            grdDocType.ShowFooter = true;
        }
        else
        {
            grdDocType.ShowFooter = false;
        }

        IList<DocTypeDTO> lstDocTypeDTO = ESalesUnityContainer.Container.Resolve<IDocumentTypeService>()
            .GetDocumentTypeListByDocGroupId(Convert.ToInt32(ddldocGroup.SelectedValue));
        
        if (lstDocTypeDTO.Count > 0)
        {
            grdDocType.DataSource = lstDocTypeDTO;
            grdDocType.DataBind();
        }
        else
        {
            ShowBlankRowInGrid<DocTypeDTO>(grdDocType);
        }
    }

    protected void ddldocGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Get document type from database depending upon the group selected from dropdown
        GetDocumentTypes();
    }

    protected void grdDocType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals(Globals.GridCommandEvents.ADDNEW))
        {
            if (Page.IsValid)
            {
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                DocTypeDTO docTypeDetails = new DocTypeDTO();
                docTypeDetails.Doc_Name = ((TextBox)row.FindControl("txtNewDocType")).Text;
                docTypeDetails.Doc_Group = Convert.ToInt32(ddldocGroup.SelectedValue);
                docTypeDetails.Doc_Acronym = ((TextBox)row.FindControl("txtNewDocAcronym")).Text;
                docTypeDetails.Doc_Mandatory = ((CheckBox)row.FindControl("chkNewMandatory")).Checked ? true : false;
                docTypeDetails.Doc_IsUnique = ((CheckBox)row.FindControl("chkNewUnique")).Checked ? true : false;
                docTypeDetails.Doc_CreatedDate = DateTime.Now;
                docTypeDetails.Doc_LastUpdatedDate = DateTime.Now;
                docTypeDetails.Doc_CreatedBy = GetCurrentUserId();

                int docId = ESalesUnityContainer.Container.Resolve<IDocumentTypeService>().SaveCustDocumentTypeInfo(docTypeDetails);
                ucMessageBoxForGrid.ShowMessage(Messages.DocumentTypeSavedSuccessfully);
            }
        }
    }

    protected void grdDocType_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdDocType.EditIndex = e.NewEditIndex;

        //Get document type from database depending upon the group selected from dropdown
        GetDocumentTypes();
    }

    protected void grdDocType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int documentId = Convert.ToInt32(grdDocType.DataKeys[e.RowIndex].Value);
        
        DocTypeDTO docTypeDetails = ESalesUnityContainer.Container.Resolve<IDocumentTypeService>()
            .GetDocumentTypeListByDocId(documentId);
        docTypeDetails.Doc_IsDeleted = true;
        
        ESalesUnityContainer.Container.Resolve<IDocumentTypeService>().UpdateCustomerDocumentTypeInfo(docTypeDetails);
        ucMessageBoxForGrid.ShowMessage(Messages.DocumentTypeDeletedSuccessfully);
    }

    /// <summary>
    /// Row edit/update/cancel function for grid view
    /// </summary>
    /// <param name="rowIndex"></param>
    private void GridViewRowUpdateFunctions(int rowIndex)
    {
        grdDocType.EditIndex = rowIndex;

        //Get document type from database depending upon the group selected from dropdown
        GetDocumentTypes();
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        //Row edit/update/cancel function for grid view
        GridViewRowUpdateFunctions(-1);
    }

    protected void grdDocType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDocType.PageIndex = e.NewPageIndex;

        //Get document type from database depending upon the group selected from dropdown
        GetDocumentTypes();
    }

    protected IEnumerable grdDocType_MustAddARow(IEnumerable data)
    {
        return base.AddBlankRowInGrid<DocTypeDTO>();
    }

    protected void grdDocType_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (Page.IsValid)
        {            
            DocTypeDTO docTypeDetails = new DocTypeDTO();
            docTypeDetails = ESalesUnityContainer.Container.Resolve<IDocumentTypeService>()
                .GetDocumentTypeListByDocId(Convert.ToInt32(grdDocType.DataKeys[e.RowIndex].Value));
            docTypeDetails.Doc_Name = ((TextBox)grdDocType.Rows[e.RowIndex].FindControl("txtDocType")).Text;
            docTypeDetails.Doc_Acronym = ((TextBox)grdDocType.Rows[e.RowIndex].FindControl("txtDocAcronym")).Text;
            docTypeDetails.Doc_Mandatory = ((CheckBox)grdDocType.Rows[e.RowIndex].FindControl("chkMandatory")).Checked ? true : false;
            docTypeDetails.Doc_IsUnique = ((CheckBox)grdDocType.Rows[e.RowIndex].FindControl("chkUnique")).Checked ? true : false;
            docTypeDetails.Doc_CreatedDate = DateTime.Now;
            docTypeDetails.Doc_LastUpdatedDate = DateTime.Now;
            docTypeDetails.Doc_CreatedBy = GetCurrentUserId();
            
            //To update document type
            int docId = ESalesUnityContainer.Container.Resolve<IDocumentTypeService>()
                .UpdateCustomerDocumentTypeInfo(docTypeDetails);
            ucMessageBoxForGrid.ShowMessage(Messages.DocumentTypeUpdatedSuccessfully);
        }
    }

    protected void grdDocType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdDocType.EditIndex = -1;
        GetDocumentTypes();
    }

    protected void EditDocType_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        CustomValidator customval = (CustomValidator)sender;
        GridViewRow r = (GridViewRow)customval.NamingContainer;

        string DocTypeName = ((TextBox)r.FindControl("txtDocType")).Text.Trim();
        int docTypeId = Convert.ToInt32(grdDocType.DataKeys[r.RowIndex].Value);

        if (ESalesUnityContainer.Container.Resolve<IDocumentTypeService>()
            .DocTypeExists(Convert.ToInt32(ddldocGroup.SelectedValue), docTypeId, DocTypeName))
        {
            args.IsValid = false;
        }
    }

    protected void AddDocType_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        TextBox txtNewDocType = (TextBox)grdDocType.FooterRow.FindControl("txtNewDocType");

        if (ESalesUnityContainer.Container.Resolve<IDocumentTypeService>()
            .DocTypeExists(Convert.ToInt32(ddldocGroup.SelectedValue), 0, txtNewDocType.Text.Trim()))
        {
            args.IsValid = false;
        }
    }
}