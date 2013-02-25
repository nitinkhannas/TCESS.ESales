using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
	public class CounterDTO : BaseDTO
	{
		#region Primitive Properties

		public int Counter_Id
		{
			get;
			set;
		}

		public string Counter_Name
		{
			get;
			set;
		}

		public Nullable<int> Counter_Agent_Id
		{
			get;
			set;
		}

		public string Counter_Agent_Name
		{
			get;
			set;
		}

		public Nullable<int> Counter_User_Id
		{
			get;
			set;
		}

		public string Counter_MAC_Id
		{
			get;
			set;
		}

		public bool Counter_IsActive
		{
			get;
			set;
		}

		public int Counter_CreatedBy
		{
			get;
			set;
		}

		public Nullable<DateTime> Counter_CreatedDate
		{
			get;
			set;
		}

		public Nullable<DateTime> Counter_LastUpdatedDate
		{
			get;
			set;
		}

		public bool Counter_IsDeleted
		{
			get;
			set;
		}

		#endregion

		#region Navigation Properties

		private AgentDTO _agentdetail;

		public AgentDTO agentdetail
		{
			get { return _agentdetail; }
			set
			{
				if (!ReferenceEquals(_agentdetail, value))
				{
					var previousValue = _agentdetail;
					_agentdetail = null;
					Counter_Agent_Name = value.Agent_Name;
				}
			}
		}

		#endregion
	}
}