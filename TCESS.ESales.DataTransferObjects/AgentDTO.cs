using System;

namespace TCESS.ESales.DataTransferObjects
{
    public class AgentDTO : BaseDTO
    {
        #region Primitive Properties

        public int Agent_Id
        {
            get;
            set;
        }

        public string Agent_Name
        {
            get;
            set;
        }

        public string Agent_ShortName
        {
            get;
            set;
        }
        public DateTime Agent_StartDate
        {
            get;
            set;
        }

        public Nullable<DateTime> Agent_ClosureDate
        {
            get;
            set;
        }

        public string Agent_PanNumber
        {
            get;
            set;
        }

        public string Agent_SalesTaxNumber
        {
            get;
            set;
        }    

        public string Agent_TSLCode
        {
            get;
            set;
        }

        public string Agent_RegContactPerson
        {
            get;
            set;
        }

        public string Agent_RegAddress
        {
            get;
            set;
        }

        public int Agent_RegState
        {
            get;
            set;
        }

        public int Agent_RegDistrict
        {
            get;
            set;
        }

        public Nullable<int> Agent_RegPinCode
        {
            get;
            set;
        }

        public string Agent_RegMobileNo
        {
            get;
            set;
        }

        public string Agent_RegPhoneNo
        {
            get;
            set;
        }

        public string Agent_RegEmail
        {
            get;
            set;
        }

        public string Agent_LocalContactPerson
        {
            get;
            set;
        }

        public  string Agent_LocalAddress
        {
            get;
            set;
        }

        public Nullable<int> Agent_LocalState
        {
            get;
            set;
        }

        public Nullable<int> Agent_LocalDistrict
        {
            get;
            set;
        }

        public Nullable<int> Agent_LocalPinCode
        {
            get;
            set;
        }

        public string Agent_LocalMobileNo
        {
            get;
            set;
        }

        public  string Agent_LocalPhoneNo
        {
            get;
            set;
        }

        public string Agent_LocalEmail
        {
            get;
            set;
        }

        public string Agent_ComContactPerson
        {
            get;
            set;
        }

        public string Agent_ComAddress
        {
            get;
            set;
        }

        public Nullable<int> Agent_ComState
        {
            get;
            set;
        }

        public Nullable<int> Agent_ComDistrict
        {
            get;
            set;
        }

        public Nullable<int> Agent_ComPinCode
        {
            get;
            set;
        }

        public string Agent_ComMobileNo
        {
            get;
            set;
        }

        public string Agent_ComPhoneNo
        {
            get;
            set;
        }

        public string Agent_ComEmail
        {
            get;
            set;
        }

        public int Agent_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> Agent_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> Agent_LastUpdatedDate
        {
            get;
            set;
        }

        public bool Agent_IsDeleted
        {
            get;
            set;
        }

        #endregion
    }
}