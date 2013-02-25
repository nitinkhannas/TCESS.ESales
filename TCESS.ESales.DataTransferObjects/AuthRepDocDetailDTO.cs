using System;

namespace TCESS.ESales.DataTransferObjects
{
	public class AuthRepDocDetailDTO
	{
		#region Primitive Properties

		public int AuthRep_Doc_Id
		{
			get;
			set;
		}

		public int AuthRep_Doc_AuthId
		{
			get;
			set;
		}

		public int AuthRep_Doc_DocId
		{
			get;
			set;
		}

		public string AuthRep_Doc_DocName
		{
			get;
			set;
		}

		public string AuthRep_Doc_DocAcroName
		{
			get;
			set;
		}

		public string AuthRep_Doc_DocNo
		{
			get;
			set;
		}

		public Nullable<DateTime> AuthRep_Doc_ExDate
		{
			get;
			set;
		}

		public string AuthRep_Doc_FileName
		{
			get;
			set;
		}

		public int AuthRep_Doc_CreatedBy
		{
			get;
			set;
		}

		public Nullable<DateTime> AuthRep_Doc_CreatedDate
		{
			get;
			set;
		}

		public Nullable<DateTime> AuthRep_Doc_LastUpdatedDate
		{
			get;
			set;
		}

		public bool AuthRep_Doc_IsDeleted
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
					AuthRep_Doc_DocName = value.Doc_Name;
					AuthRep_Doc_DocAcroName = value.Doc_Acronym;
				}
			}
		}

		#endregion
	}
}