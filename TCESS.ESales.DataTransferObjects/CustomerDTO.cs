#region Using directives

using System;
using System.Collections.Generic;

#endregion

namespace TCESS.ESales.DataTransferObjects
{
	public class CustomerDTO : BaseDTO
	{
		#region Primitive Properties

		public int Cust_Id
		{
			get;
			set;
		}

		public string Cust_Code
		{
			get;
			set;
		}

        public Nullable<int> Cust_AgentId
        {
            get;
            set;
        }

        public string Cust_AgentName
        {
            get;
            set;
        }

		public IList<TruckDetailsDTO> Cust_TruckList
		{
			get;
			set;
		}

		public string Cust_TradeName
		{
			get;
			set;
		}

		public string Cust_FirmName
		{
			get;
			set;
		}

		public string Cust_OwnerName
		{
			get;
			set;
		}

		public string Cust_FathersName
		{
			get;
			set;
		}

		public string Cust_RegisteredAddress
		{
			get;
			set;
		}

		public string Cust_UnitAddress
		{
			get;
			set;
		}
		public string Cust_Post
		{
			get;
			set;
		}

		public int Cust_State
		{
			get;
			set;
		}

		public string Cust_State_Name
		{
			get;
			set;
		}

		public int Cust_District
		{
			get;
			set;
		}

		public string Cust_District_Name
		{
			get;
			set;
		}

		public string Cust_Landmark
		{
			get;
			set;
		}

		public int Cust_Pincode
		{
			get;
			set;
		}

		public string Cust_MobileNo
		{
			get;
			set;
		}

		public string Cust_PhoneNo
		{
			get;
			set;
		}

		public int Cust_OwnershipStatus
		{
			get;
			set;
		}

		public string Cust_OwnershipName
		{
			get;
			set;
		}

		public int Cust_BusinessType
		{
			get;
			set;
		}

		public string Cust_Business_Name
		{
			get;
			set;
		}

		public int Cust_SalesType
		{
			get;
			set;
		}

		public bool Cust_RegCustType
		{
			get;
			set;
		}

		public string Cust_PartnerPhoneNo
		{
			get;
			set;
		}

		public int Cust_AMEBlockId
		{
			get;
			set;
		}

		public string AMEBlockOffice
		{
			get;
			set;
		}

		public Nullable<DateTime> Cust_AMEVisitDate
		{
			get;
			set;
		}

        public string Cust_TinNo
        {
            get;
            set;
        }

        public int Cust_Mat_AnnualRequirement
        {
            get;
            set;
        }

		public bool Cust_Status
		{
			get;
			set;
		}

        public int Cust_NoOfChimneys
        {
            get;
            set;
        }

        public int Cust_BrickCapacity
        {
            get;
            set;
        }
		

		public string Cust_Excise_Comm
		{
			get;
			set;
		}
		public  string Cust_Excise_Range
		{
			get;
			set;
		}

		public  string Cust_Excise_Div
		{
			get;
			set;
		}

		public bool Cust_IsBlacklisted
		{
			get;
			set;
		}

		public string Cust_BlacklistedBy
		{
			get;
			set;
		}

        public string Cust_FolderName
        {
            get;
            set;
        }

		public int Cust_CreatedBy
		{
			get;
			set;
		}

		public Nullable<DateTime> Cust_CreatedDate
		{
			get;
			set;
		}

		public Nullable<DateTime> Cust_LastUpdatedDate
		{
			get;
			set;
		}

		public bool Cust_IsDeleted
		{
			get;
			set;
		}
		public Nullable<bool> Cust_SmsSent
		{
			get;
			set;
		}
        public string Cust_BankName
        {
            get;
            set;
        }

        public string Cust_BankBranch
        {
            get;
            set;
        }

        public string Cust_BankAccountNo
        {
            get;
            set;
        }

        public Nullable<int> Cust_BankAccountType
        {
            get;
            set;
        }

        public string Cust_BankIFCICode
        {
            get;
            set;
        }

        public Nullable<int> Cust_BankChequeNo
        {
            get;
            set;
        }

        public string Cust_AMEName
        {
            get;
            set;
        }

        public Nullable<DateTime> Cust_VATFiledON
        {
            get;
            set;
        }

        public Nullable<int> Cust_UnitStatus
        {
            get;
            set;
        }
        public Nullable<DateTime> Cust_AMEReVisitDate
        {
            get;
            set;
        }

        public bool Cust_IsVarified
        {
            get;
            set;
        }

		#endregion

		#region Navigation Properties

		private IList<TruckDetailsDTO> _truckdetails;
        private IList<CustomerMaterialMapDTO> _customermaterialmaps;
		private AmeBlockDTO _ameblock;
		private DistrictDTO _district;
		private StateDTO _state;
        private BusinessTypeDTO _businesstype;
		private OwnershipStatusDTO _ownershipstatu;
        private AgentDTO _agentdetail;
        private IList<CustomerDocDetailsDTO> _customerdocdetails;

        public AgentDTO agentdetail
        {
            get { return _agentdetail; }
            set
            {
                if (!ReferenceEquals(_agentdetail, value))
                {
                    var previousValue = _agentdetail;
                    _agentdetail = null;
                    Cust_AgentName = value.Agent_Name;
                }
            }
        }

		public IList<TruckDetailsDTO> truckdetails
		{
			get { return _truckdetails; }
			set
			{
				if (!ReferenceEquals(_truckdetails, value))
				{
					var previousValue = _truckdetails as FixupCollection<TruckDetailsDTO>;
					_truckdetails = null;
					Cust_TruckList = value;
				}
			}
		}

        public IList<CustomerMaterialMapDTO> customermaterialmaps
        {
            get { return _customermaterialmaps; }
            set
            {
                if (!ReferenceEquals(_customermaterialmaps, value))
                {                    
                    _customermaterialmaps = null;
                    Cust_Mat_AnnualRequirement = value[0].Cust_Mat_AnnualRequirement;
                }
            }
        }

        public IList<CustomerDocDetailsDTO> customerdocdetails
        {
            get { return _customerdocdetails; }
            set
            {
                if (!ReferenceEquals(_customerdocdetails, value))
                {
                    string tinno = string.Empty;
                    _customerdocdetails = null;

                    //Loops through list of customer documents to find Tin Number
                    foreach (CustomerDocDetailsDTO customerDoc in value)
                    {
                        if (customerDoc.Cust_Doc_DocId == 3)
                        {
                            tinno = customerDoc.Cust_Doc_No;
                            break;
                        }
                    }
                    Cust_TinNo = tinno;
                }
            }
        }

        public AmeBlockDTO ameblock
        {
            get { return _ameblock; }
            set
            {
                if (!ReferenceEquals(_ameblock, value))
                {
                    var previousValue = _ameblock;
                    _ameblock = null;
                    AMEBlockOffice = value.Blocks_Name;
                }
            }
        }

        public DistrictDTO district
        {
            get { return _district; }
            set
            {
                if (!ReferenceEquals(_district, value))
                {
                    var previousValue = _district;
                    _district = null;
                    Cust_District_Name = value.Dist_Name;
                }
            }
        }

        public StateDTO state
		{
			get { return _state; }
			set
			{
				if (!ReferenceEquals(_district, value))
				{
					var previousValue = _state;
					_state = null;
					Cust_State_Name = value.State_Name;
				}
			}
		}

        public BusinessTypeDTO businesstype
        {
            get { return _businesstype; }
            set
            {
                if (!ReferenceEquals(_businesstype, value))
                {
                    var previousValue = _businesstype;
                    _businesstype = null;
                    Cust_Business_Name = value.BusinessType_Name;
                }
            }
        }

        public OwnershipStatusDTO ownershipstatu
        {
            get { return _ownershipstatu; }
            set
            {
                if (!ReferenceEquals(_ownershipstatu, value))
                {
                    var previousValue = _ownershipstatu;
                    _ownershipstatu = null;
                    Cust_OwnershipName = value.OwnershipStatus_Name;
                }
            }
        }

		#endregion
	}
}