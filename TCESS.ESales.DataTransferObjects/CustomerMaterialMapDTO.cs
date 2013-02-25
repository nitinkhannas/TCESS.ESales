#region Namespace

using System;

#endregion

namespace TCESS.ESales.DataTransferObjects
{
	public class CustomerMaterialMapDTO : BaseDTO
	{
		#region Primitive Properties

		public int Cust_Mat_Id
		{
			get;
			set;
		}

		public int Cust_Mat_CustId
		{
			get;
			set;
		}

        public string CustomerFirmName
        {
            get;
            set;
        }

        public bool CustomerRegType
        {
            get;
            set;
        }

		public int Cust_Mat_MaterialId
		{
			get;
			set;
		}        

		public string Cust_Mat_MaterialName
		{
			get;
			set;
		}

		public int Cust_Mat_AnnualRequirement
		{
			get;
			set;
		}

        public int Cust_Mat_AllotedQuantityId
        {
            get;
            set;
        }

        public string Cust_Mat_AllotedQuantity
        {
            get;
            set;
        }

        public int Cust_Mat_LiftingLimit
        {
            get;
            set;
        }

		public int Cust_Mat_CreatedBy
		{
			get;
			set;
		}

		public Nullable<DateTime> Cust_Mat_CreatedDate
		{
			get;
			set;
		}

		public Nullable<DateTime> Cust_Mat_LastUpdatedDate
		{
			get;
			set;
		}

		public bool Cust_Mat_IsDeleted
		{
			get;
			set;
		}

		public CustomerDTO Cust_Mat_Customer
		{
			get;
			set;
		}
        public  Nullable<int> Cust_Mat_Timeinterval
        {
            get;
            set;
        }
		#endregion

        #region Navigation Properties

        private CustomerDTO _customer;
        private AllotedQuantityDTO _allotedquantity;
		private MaterialTypeDTO _materialName;

		public CustomerDTO Customer
        {
            get { return _customer; }
            set
            {
                if (!ReferenceEquals(_customer, value))
                {
                    var previousValue = _customer;
                    _customer = null;
                    CustomerFirmName = value.Cust_FirmName;
                    CustomerRegType = value.Cust_RegCustType;
					Cust_Mat_Customer = value;
				}
            }
        }

		public MaterialTypeDTO materialtype
		{
			get { return _materialName; }
			set
			{
				if (!ReferenceEquals(_materialName, value))
				{
					var previousValue = _materialName;
					_materialName = null;
					Cust_Mat_MaterialName = value.MaterialType_Name;
				}
			}
		}

        public AllotedQuantityDTO allotedquantity
        {
            get { return _allotedquantity; }
            set
            {
                if (!ReferenceEquals(_allotedquantity, value))
                {
                    var previousValue = _allotedquantity;
                    _allotedquantity = null;
                    Cust_Mat_AllotedQuantity = value.Alloted_Quantity;
                    Cust_Mat_AllotedQuantityId = value.Alloted_Id;
                }
            }
        }

        #endregion
    }
}