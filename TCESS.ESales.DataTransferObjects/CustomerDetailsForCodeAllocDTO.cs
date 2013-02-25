using System;


namespace TCESS.ESales.DataTransferObjects
{
	public class CustomerDetailsForCodeAllocDTO:BaseDTO
	{
		#region Primitive Properties

		public  int Cust_Id
		{
			get;
			set;
		}

		public  string Cust_TradeName
		{
			get;
			set;
		}

		public  string Cust_OwnerName
		{
			get;
			set;
		}

		public  string Cust_FirmName
		{
			get;
			set;
		}

		public  string Cust_RegisteredAddress
		{
			get;
			set;
		}

		public  string Cust_UnitAddress
		{
			get;
			set;
		}

		public  int Cust_Pincode
		{
			get;
			set;
		}

		public  string state
		{
			get;
			set;
		}

		public  string district
		{
			get;
			set;
		}

		public  string businesstype
		{
			get;
			set;
		}

		public  string TIN
		{
			get;
			set;
		}

		public  string PAN
		{
			get;
			set;
		}

		public  Nullable<long> AnnualRequirement
		{
			get;
			set;
		}

		public  string Cust_MobileNo
		{
			get;
			set;
		}

        public virtual string Cust_Code
        {
            get;
            set;
        }

		#endregion

	}
}
