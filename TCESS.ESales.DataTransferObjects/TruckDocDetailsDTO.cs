using System;

namespace TCESS.ESales.DataTransferObjects
{
    public class TruckDocDetailsDTO
    {
        #region Primitive Properties

        public int Truck_Doc_Id
        {
            get;
            set;
        }

        public int Truck_Doc_TruckId
        {
            get;
            set;
        }

        public int Truck_Doc_DocId
        {
            get;
            set;
        }

        public string Truck_Doc_DocName
        {
            get;
            set;
        }

        public string Truck_Doc_DocAcroName
        {
            get;
            set;
        }

        public string Truck_Doc_DocNo
        {
            get;
            set;
        }

        public Nullable<DateTime> Truck_Doc_ExDate
        {
            get;
            set;
        }

        public string Truck_Doc_FileName
        {
            get;
            set;
        }

        public int Truck_Doc_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> Truck_Doc_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> Truck_Doc_LastUpdatedDate
        {
            get;
            set;
        }

        public bool Truck_Doc_IsDeleted
        {
            get;
            set;
        }

        #endregion

        #region Navigation Properties

        private DocTypeDTO _documentDet;

        public DocTypeDTO doctype
        {
            get { return _documentDet; }
            set
            {
                if (!ReferenceEquals(_documentDet, value))
                {
                    var previousValue = _documentDet;
                    _documentDet = null;
                    Truck_Doc_DocName = value.Doc_Name;
                    Truck_Doc_DocAcroName = value.Doc_Acronym;
                }
            }
        }

        #endregion
    }
}