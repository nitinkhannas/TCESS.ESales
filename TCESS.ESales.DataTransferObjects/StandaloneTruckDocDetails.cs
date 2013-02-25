using System;

namespace TCESS.ESales.DataTransferObjects
{
    public class StandaloneTruckDocDetails
    {
        public int StandaloneTruck_Doc_Id
        {
            get;
            set;
        }

        public int StandaloneTruck_Doc_TruckId
        {
            get;
            set;
        }

        public int StandaloneTruck_Doc_DocId
        {
            get;
            set;
        }

        public string StandaloneTruck_Doc_DocNo
        {
            get;
            set;
        }

        public Nullable<DateTime> StandaloneTruck_Doc_ExDate
        {
            get;
            set;
        }

        public byte[] StandaloneTruck_Doc_File
        {
            get;
            set;
        }

        public string StandaloneTruck_Doc_FileName
        {
            get;
            set;
        }

        public int StandaloneTruck_Doc_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> StandaloneTruck_Doc_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> StandaloneTruck_Doc_LastUpdatedDate
        {
            get;
            set;
        }

        public Boolean StandaloneTruck_Doc_IsDeleted
        {
            get;
            set;
        }
    }
}