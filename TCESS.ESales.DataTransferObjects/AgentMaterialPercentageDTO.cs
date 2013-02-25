using System;

namespace TCESS.ESales.DataTransferObjects
{
    public class AgentMaterialPercentageDTO : BaseDTO
    {
        #region Primitive Properties

        public int AMP_Id
        {
            get;
            set;
        }

        public int AMP_Agent_Id
        {
            get;
            set;
        }

        public int AMP_MaterialType_Id
        {
            get;
            set;
        }

        public decimal AMP_Percentage
        {
            get;
            set;
        }

        public bool AMP_IsActive
        {
            get;
            set;
        }

        public int AMP_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> AMP_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> AMP_LastUpdatedDate
        {
            get;
            set;
        }

        public bool AMP_IsDeleted
        {
            get;
            set;
        }

        public string AgentName
        {
            get;
            set;
        }

        public MaterialTypeDTO MatrialType
        {
            get;
            set;
        }

        #endregion

        #region Navigation Properties
        
        private AgentDTO _agentdetail;
        private MaterialTypeDTO _materialtype;

        public AgentDTO agentdetail
        {
            get { return _agentdetail; }
            set
            {
                if (!ReferenceEquals(_agentdetail, value))
                {
                    var previousValue = _agentdetail;
                    _agentdetail = value;
                    AgentName = _agentdetail.Agent_Name;
                }
            }
        }

        public MaterialTypeDTO materialtype
        {
            get { return _materialtype; }
            set
            {
                if (!ReferenceEquals(_materialtype, value))
                {
                    var previousValue = _materialtype;
                    _materialtype = value;
                    MatrialType = _materialtype;
                }
            }
        }
        
        #endregion
    }
}