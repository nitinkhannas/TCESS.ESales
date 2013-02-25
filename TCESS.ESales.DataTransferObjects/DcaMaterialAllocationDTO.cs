#region Namespace

using System;

#endregion

namespace TCESS.ESales.DataTransferObjects
{
	public class DcaMaterialAllocationDTO : BaseDTO
	{
		public int DCAMA_ID
		{
			get;
			set;
		}

		public DateTime DCAMA_Date
		{
			get;
			set;
		}

		public int DCAMA_Agent_Id
		{
			get;
			set;
		}


		public int DCAMA_MaterialType_Id
		{
			get;
			set;
		}

		public decimal DCAMA_TodayPercentage
		{
			get;
			set;
		}

		public int DCAMA_AllocatedQty
		{
			get;
			set;
		}

		public int DCAMA_LastQty
		{
			get;
			set;
		}

		public decimal DCAMA_CurrentPercentage
		{
			get;
			set;
		}
        public decimal DCAMA_CurrentVariance
		{
			get;
			set;
		}

        public virtual decimal DCAMA_ActualPercentage
        {
            get;
            set;
        }

        public virtual decimal DCAMA_ActualVariance
        {
            get;
            set;
        }

        public bool DCAMA_IsAgentActive
		{
			get;
			set;
		}

		public int DCAMA_CreatedBy
		{
			get;
			set;
		}

		public Nullable<DateTime> DCAMA_CreatedDate
		{
			get;
			set;
		}

		public Nullable<DateTime> DCAMA_LastUpdatedDate
		{
			get;
			set;
		}
	}
}
