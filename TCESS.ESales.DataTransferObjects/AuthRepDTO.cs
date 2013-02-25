using System;

namespace TCESS.ESales.DataTransferObjects
{
	public class AuthRepDTO:BaseDTO 
	{
		#region Primitive Properties

		public int AuthRep_Id
		{
			get;
			set;
		}

		public int AuthRep_CustomerId
		{
			get;
			set;
		}

        public CustomerDTO AuthRep_Customer
        {
            get;
            set;
        }

		public string AuthRep_Name
		{
			get;
			set;
		}

		public string AuthRep_FatherName
		{
			get;
			set;
		}

		public string AuthRep_Mobile
		{
			get;
			set;
		}

		public bool AuthRep_IsBlacklisted
		{
			get;
			set;
		}

		public string AuthRep_BlacklistedBy
		{
			get;
			set;
		}

		public string AuthRep_Address
		{
			get;
			set;
		}

		public int AuthRep_CreatedBy
		{
			get;
			set;
		}

		public Nullable<DateTime> AuthRep_CreatedDate
		{
			get;
			set;
		}

		public Nullable<DateTime> AuthRep_LastUpdatedDate
		{
			get;
			set;
		}

		public bool AuthRep_IsDeleted
		{
			get;
			set;
		}

		#endregion

		#region Navigate Properties

		private CustomerDTO _customer;

		public CustomerDTO customer
		{
			get { return _customer; }
			set
			{
				if (!ReferenceEquals(_customer, value))
				{
					var previousvalue = _customer;
					_customer = value;
					AuthRep_Customer = _customer;
				}
			}
		}

		#endregion
	}
}