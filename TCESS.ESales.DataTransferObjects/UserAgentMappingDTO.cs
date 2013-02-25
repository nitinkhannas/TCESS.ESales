#region Using directives

using System;

#endregion

namespace TCESS.ESales.DataTransferObjects
{
	public class UserAgentMappingDTO : BaseDTO
	{
		#region Primitive Properties

		public int UAM_Id
		{
			get;
			set;
		}

		public int UAM_Agent_Id
		{
			get;
			set;
		}        

        public string UAM_Agent_Name
        {
            get;
            set;
        }

		public int UAM_User_Id
		{
			get;
			set;
		}

        public string UAM_FirstName
        {
            get;
            set;
        }

        public string UAM_LastName
        {
            get;
            set;
        }

        public int UPM_PaymentModeId
        {
            get;
            set;
        }

        public string UPM_PaymentMode
        {
            get;
            set;
        }

		public int UAM_CreatedBy
		{
			get;
			set;
		}

		public Nullable<DateTime> UAM_CreatedDate
		{
			get;
			set;
		}

		public Nullable<DateTime> UAM_LastUpdatedDate
		{
			get;
			set;
		}

		public bool UAM_IsDeleted
		{
			get;
			set;
		}        

		#endregion

        #region Navigation Properties

        private AgentDTO _agentdetail;

        public virtual AgentDTO agentdetail
        {
            get { return _agentdetail; }
            set
            {
                if (!ReferenceEquals(_agentdetail, value))
                {
                    var previousValue = _agentdetail;
                    _agentdetail = null;
                    UAM_Agent_Name = value.Agent_Name;
                }
            }
        }

        #endregion
    }
}