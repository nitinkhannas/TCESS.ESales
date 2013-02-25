using System;

namespace TCESS.ESales.DataTransferObjects
{
	public class CustomerDocDetailsDTO : BaseDTO
	{
		#region Primitive Properties

		public int Cust_Doc_Id
		{
			get;
			set;
		}

		public int Cust_Doc_CustId
		{
			get;
			set;
		}

		public CustomerDTO Cust_Doc_Customer
		{
			get;
			set;
		}

		public int Cust_Doc_DocId
		{
			get;
			set;
		}

		public string Cust_Doc_DocName
		{
			get;
			set;
		}

		public string Cust_Doc_DocAcroName
		{
			get;
			set;
		}

		public string Cust_Doc_No
		{
			get;
			set;
		}

		public Nullable<DateTime> Cust_Doc_ExDate
		{
			get;
			set;
		}

        public string Cust_Doc_FileName
        {
            get;
            set;
        }

		public int Cust_Doc_CreatedBy
		{
			get;
			set;
		}

		public Nullable<DateTime> Cust_Doc_CreatedDate
		{
			get;
			set;
		}

		public Nullable<DateTime> Cust_Doc_LastUpdatedDate
		{
			get;
			set;
		}

		public bool Cust_Doc_IsDeleted
		{
			get;
			set;
		}

		#endregion

        #region Navigation Properties

        private CustomerDTO _customer;
		private DocTypeDTO _documentDetail;

        public CustomerDTO Customer
		{
			get { return _customer; }
			set
			{
				if (!ReferenceEquals(_customer, value))
				{
					var previousValue = _customer;
                    _customer = null;
                    Cust_Doc_Customer = value;
				}
			}
        }

        public DocTypeDTO doctype
        {
            get { return _documentDetail; }
            set
            {
                if (!ReferenceEquals(_documentDetail, value))
                {
                    var previousValue = _documentDetail;
                    _documentDetail = null;
                    Cust_Doc_DocName = value.Doc_Name;
                    Cust_Doc_DocAcroName = value.Doc_Acronym;
                }
            }
        }

        #endregion
    }
}