#region Namespace

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Resources;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;

#endregion

public partial class CustomerRegistration_EditTruckDocuments : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ucMessageBoxForGrid.Event_OkButton += ucMessageBoxForGrid_Event_OkButton;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateTruckDocuments();
        }
    }

    private void PopulateTruckDocuments()
    {
        ddlTruckDoc.DataSource = ESalesUnityContainer.Container.Resolve<IDocumentTypeService>().GetTruckDocumentTypeList();
        ddlTruckDoc.DataBind();
        ddlTruckDoc.Items.Insert(0, new ListItem("Select", "0"));
        ddlTruckDoc.Items.Insert(1, new ListItem("Driver Details", "1"));
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            FillGridWithTruckDetails(txtTruckNo.Text.Trim(), ddlTruckDoc.SelectedItem.Text);
        }
    }

    private void FillGridWithTruckDetails(string truckNo,string docName)
    {
        TruckVerificationDTO truckDetails = new TruckVerificationDTO();
        truckDetails = ESalesUnityContainer.Container.Resolve<ITruckService>().GetAllTruckDetails(truckNo);

        if (truckDetails.type > 0)
        {
            IList<TruckVerificationDTO> lstTruckDetails = new List<TruckVerificationDTO>();
            lstTruckDetails.Add(truckDetails);
            grdManageTrucks.DataSource = lstTruckDetails;
            grdManageTrucks.DataBind();
        }
        else
        {
            //ucMessageBoxForGrid.ShowMessage(Resources.Messages.TruckDetailsDoesNotExist);
        }

        if (Convert.ToInt32(ddlTruckDoc.SelectedItem.Value) == 1)
        {
            grdManageTrucks.Columns[6].Visible = true;
            grdDocument.Visible = false;
        }
        else
        {
            grdManageTrucks.Columns[6].Visible = false;
            grdDocument.Visible = true;
            if (truckDetails.type == 1)
            {
                TruckDocDetailsDTO truckDocDetails = new TruckDocDetailsDTO();
                truckDocDetails = ESalesUnityContainer.Container.Resolve<ITruckDocService>().GetTruckDocDetailsByTruckIdAndDocId(truckDetails.Truck_Id, Convert.ToInt32(ddlTruckDoc.SelectedItem.Value));

                if (truckDocDetails.Truck_Doc_Id > 0)
                {
                    LoadDocumentGrid();
                    ViewState[Globals.StateMgmtVariables.TRUCKID] = truckDocDetails.Truck_Doc_TruckId;
                    foreach (GridViewRow row in grdDocument.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            if (truckDocDetails != null)
                            {
                                ((TextBox)row.FindControl("txtDocNo")).Text = truckDocDetails.Truck_Doc_DocNo;
                                //((HiddenField)row.FindControl("hdnCustDocId")).Value = truckDocDetails.Cust_Doc_Id.ToString();
                                ((TextBox)row.FindControl("txtDocExDate")).Text = string.IsNullOrEmpty(Convert.ToString(truckDocDetails.Truck_Doc_ExDate)) ? string.Empty :
                                    Convert.ToDateTime(truckDocDetails.Truck_Doc_ExDate).ToString("dd MMM yyyy");
                            }
                        }
                    }
                }
                else
                {
                    base.ShowBlankRowInGrid<DocTypeDTO>(grdDocument);
                }
            }
            else if (truckDetails.type == 2)
            {
                StandaloneTruckDocDetails standaloneTruckDocDetails = ESalesUnityContainer.Container.Resolve<IStandaloneTruckService>().GetStandaloneTruckDocDetailsByTruckIdAndDocId(truckDetails.Truck_Id, Convert.ToInt32(ddlTruckDoc.SelectedItem.Value));

                if (standaloneTruckDocDetails.StandaloneTruck_Doc_Id > 0)
                {
                    LoadDocumentGrid();
                    ViewState[Globals.StateMgmtVariables.TRUCKID] = standaloneTruckDocDetails.StandaloneTruck_Doc_TruckId;
                    foreach (GridViewRow row in grdDocument.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            if (standaloneTruckDocDetails != null)
                            {
                                ((TextBox)row.FindControl("txtDocNo")).Text = standaloneTruckDocDetails.StandaloneTruck_Doc_DocNo;
                                //((HiddenField)row.FindControl("hdnCustDocId")).Value = truckDocDetails.Cust_Doc_Id.ToString();
                                ((TextBox)row.FindControl("txtDocExDate")).Text = string.IsNullOrEmpty(Convert.ToString(standaloneTruckDocDetails.StandaloneTruck_Doc_ExDate)) ? string.Empty :
                                    Convert.ToDateTime(standaloneTruckDocDetails.StandaloneTruck_Doc_ExDate).ToString("dd MMM yyyy");
                            }
                        }
                    }
                }
                else
                {
                    base.ShowBlankRowInGrid<DocTypeDTO>(grdDocument);
                }
            }
        }
    }
    
    /// <summary>
    /// Loads Truck Document Details from database
    /// </summary>
    private void LoadDocumentGrid()
    {
        grdDocument.DataSource = ESalesUnityContainer.Container.Resolve<IDocumentTypeService>().GetDocumentTypeListForTrucks().Where(f => f.Doc_Id == Convert.ToInt32(ddlTruckDoc.SelectedItem.Value));
        grdDocument.DataBind();
    }

    private void SaveDocumentListForTruck(string truckNo)
    {
        TruckVerificationDTO truckDetails = new TruckVerificationDTO();
        truckDetails = ESalesUnityContainer.Container.Resolve<ITruckService>().GetAllTruckDetails(truckNo);

        IList<TruckDocDetailsDTO> listTruckDocDetail = new List<TruckDocDetailsDTO>();
        IList<TruckDocumentsDTO> listTruckDocument = new List<TruckDocumentsDTO>();
        IList<StandaloneTruckDocDetails> listStandaloneTruckDocDetail = new List<StandaloneTruckDocDetails>();

        foreach (GridViewRow row in grdDocument.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                if (((TextBox)(row.Cells[2].Controls[1])).Text != string.Empty)
                {
                    if (truckDetails.type == 1)
                    {
                        TruckDocDetailsDTO truckDocDetails = new TruckDocDetailsDTO();
                        truckDocDetails.Truck_Doc_TruckId = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.TRUCKID]);
                        truckDocDetails.Truck_Doc_DocId = Convert.ToInt32(grdDocument.DataKeys[row.RowIndex].Value);
                        truckDocDetails.Truck_Doc_DocNo = ((TextBox)(row.Cells[2].Controls[1])).Text;

                        DateTimeFormatInfo dateTimeFormatterProvider = DateTimeFormatInfo.CurrentInfo.Clone() as DateTimeFormatInfo;
                        dateTimeFormatterProvider.ShortDatePattern = "dd/MM/yyyy";
                        if (((TextBox)(row.Cells[3].Controls[1])).Text != string.Empty)
                        {
                            truckDocDetails.Truck_Doc_ExDate = DateTime.Parse(((TextBox)(row.Cells[3].Controls[1])).Text, dateTimeFormatterProvider);
                        }

                        TruckDocumentsDTO truckDocument = new TruckDocumentsDTO();
                        truckDocument.TruckDoc_CreatedBy = base.GetCurrentUserId();
                        truckDocument.TruckDoc_CreatedDate = DateTime.Now;
                        truckDocument.TruckDoc_LastUpdatedDate = DateTime.Now;

                        //If fileupload control has file
                        if (filAuthDoc.HasFile)
                        {
                            string uploadFilePath = Path.Combine(Server.MapPath("../CustomerAuthImages"), filAuthDoc.FileName);
                            filAuthDoc.SaveAs(uploadFilePath);

                            truckDocument.TruckDoc_File = ImageToBlob.ConvertImageToByteArray(uploadFilePath);
                            truckDocDetails.Truck_Doc_FileName = filAuthDoc.FileName;
                            //Delete the file from application folder after converting into byte array
                            File.Delete(uploadFilePath);
                        }
                        else
                        {
                            truckDocument.TruckDoc_File = null;
                        }

                        listTruckDocument.Add(truckDocument);

                        truckDocDetails.Truck_Doc_CreatedBy = GetCurrentUserId();
                        truckDocDetails.Truck_Doc_CreatedDate = DateTime.Now;
                        truckDocDetails.Truck_Doc_LastUpdatedDate = DateTime.Now;

                        listTruckDocDetail.Add(truckDocDetails);

                        ESalesUnityContainer.Container.Resolve<ITruckService>().SaveAndUpdateTruckDocumentDetails(listTruckDocDetail, listTruckDocument);
                    }
                    else if (truckDetails.type == 2)
                    {
                        StandaloneTruckDocDetails standaloneTruckDocDetails = new StandaloneTruckDocDetails();
                        standaloneTruckDocDetails.StandaloneTruck_Doc_TruckId = Convert.ToInt32(ViewState[Globals.StateMgmtVariables.TRUCKID]);
                        standaloneTruckDocDetails.StandaloneTruck_Doc_DocId = Convert.ToInt32(grdDocument.DataKeys[row.RowIndex].Value);
                        standaloneTruckDocDetails.StandaloneTruck_Doc_DocNo = ((TextBox)(row.Cells[2].Controls[1])).Text;

                        if (((TextBox)(row.Cells[3].Controls[1])).Text != string.Empty)
                        {
                            standaloneTruckDocDetails.StandaloneTruck_Doc_ExDate = DateTime.Parse(((TextBox)(row.Cells[3].Controls[1])).Text.ToString());
                        }

                        if (filAuthDoc.HasFile)
                        {
                            string uploadFilePath = Path.Combine(Server.MapPath("../CustomerAuthImages"), filAuthDoc.FileName);
                            filAuthDoc.SaveAs(uploadFilePath);

                            standaloneTruckDocDetails.StandaloneTruck_Doc_File = ImageToBlob.ConvertImageToByteArray(uploadFilePath);
                            standaloneTruckDocDetails.StandaloneTruck_Doc_FileName = filAuthDoc.FileName;
                            File.Delete(uploadFilePath);
                        }
                        else
                        {
                            standaloneTruckDocDetails.StandaloneTruck_Doc_File = null;
                        }
                        standaloneTruckDocDetails.StandaloneTruck_Doc_CreatedBy = GetCurrentUserId();
                        standaloneTruckDocDetails.StandaloneTruck_Doc_CreatedDate = DateTime.Now;
                        standaloneTruckDocDetails.StandaloneTruck_Doc_LastUpdatedDate = DateTime.Now;

                        listStandaloneTruckDocDetail.Add(standaloneTruckDocDetails);

                        ESalesUnityContainer.Container.Resolve<IStandaloneTruckService>().SaveAndUpdateStandaloneTruckDocumentDetails(listStandaloneTruckDocDetail);
                    }
                }
            }
        }
    }

    protected void btnSaveAndUpload_Click(object sender, EventArgs e)
    {
        if (!filAuthDoc.HasFile && Convert.ToInt32(ddlTruckDoc.SelectedItem.Value)!=1)
        {
            hdnFlag.Value = "0";
            ucMessageBoxForGrid.ShowMessage("Please select the document to upload");
        }
        else if (!filAuthDoc.HasFile && Convert.ToInt32(ddlTruckDoc.SelectedItem.Value) == 1)
        {
            // save method for driver details
        }
        else if (filAuthDoc.HasFile && Convert.ToInt32(ddlTruckDoc.SelectedItem.Value) != 1)
        {
            SaveDocumentListForTruck(txtTruckNo.Text.Trim());
            hdnFlag.Value = "1";
            ucMessageBoxForGrid.ShowMessage("Details have been updated successfully");
        }
        //Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    private void ucMessageBoxForGrid_Event_OkButton(object sender, EventArgs args)
    {
        if (hdnFlag.Value == "1")
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }

    protected void grdManageTrucks_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewRowUpdateFunctions(e.NewEditIndex);
    }

    private void GridViewRowUpdateFunctions(int rowIndex)
    {
        grdManageTrucks.EditIndex = rowIndex;
        FillGridWithTruckDetails(txtTruckNo.Text.Trim(), ddlTruckDoc.SelectedItem.Text);
    }
    
    protected void grdManageTrucks_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridViewRowUpdateFunctions(-1);
    }
    
    protected void grdManageTrucks_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (Page.IsValid)
        {
            GridViewRow row = grdManageTrucks.Rows[e.RowIndex];
            int truckId = 0;

            TruckVerificationDTO truck = new TruckVerificationDTO();
            truck = ESalesUnityContainer.Container.Resolve<ITruckService>().GetAllTruckDetails(txtTruckNo.Text.Trim());

            if (truck.type == 1)
            {
                TruckDetailsDTO truckDetails = ESalesUnityContainer.Container.Resolve<ITruckService>().GetTruckDetailsByTruckId(truck.Truck_Id);

                truckDetails.Truck_OwnerName = ((TextBox)row.FindControl("txtOwnerName")).Text;
                truckDetails.Truck_DriverName = ((TextBox)row.FindControl("txtDriverName")).Text;
                truckDetails.Truck_Address = ((TextBox)row.FindControl("txtRegisteredAddress")).Text;
                truckDetails.Truck_MobileNo = ((TextBox)row.FindControl("txtMobileNo")).Text;
                truckDetails.Truck_LastUpdatedDate = DateTime.Now;
                truckId = ESalesUnityContainer.Container.Resolve<ITruckService>().SaveAndUpdateTruckDetailsForCustomer(truckDetails);
            }
            else if (truck.type == 2)
            {
                StandaloneTrucksDTO standaloneTruckDetails = ESalesUnityContainer.Container.Resolve<IStandaloneTruckService>().GetStandaloneTruckByTruckId(truck.Truck_Id);

                standaloneTruckDetails.StandaloneTruck_OwnerName = ((TextBox)row.FindControl("txtOwnerName")).Text;
                standaloneTruckDetails.StandaloneTruck_DriverName = ((TextBox)row.FindControl("txtDriverName")).Text;
                standaloneTruckDetails.StandaloneTruck_Address = ((TextBox)row.FindControl("txtRegisteredAddress")).Text;
                standaloneTruckDetails.StandaloneTruck_MobileNo = ((TextBox)row.FindControl("txtMobileNo")).Text;
                standaloneTruckDetails.StandaloneTruck_LastUpdatedDate = DateTime.Now;
                truckId = ESalesUnityContainer.Container.Resolve<IStandaloneTruckService>().SaveAndUpdateStandaloneTrucks(standaloneTruckDetails);
            }
            hdnFlag.Value = "1";
            ucMessageBoxForGrid.ShowMessage("Details have been updated successfully");
        }
    }
}