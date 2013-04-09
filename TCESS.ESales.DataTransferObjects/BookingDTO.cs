using System;

namespace TCESS.ESales.DataTransferObjects
{
    public class BookingDTO : BaseDTO
    {
        #region Primitive Properties

        public int Booking_Id
        {
            get;
            set;
        }

        public int Booking_Cust_Id
        {
            get;
            set;
        }

        public string Booking_Cust_Code
        {
            get;
            set;
        }

        public string Booking_Cust_TradeName
        {
            get;
            set;
        }

        public string Booking_Cust_UnitName
        {
            get;
            set;
        }

        public string Booking_Cust_UnitAddress
        {
            get;
            set;
        }

        public string Booking_Cust_District_Name
        {
            get;
            set;
        }

        public string Booking_Cust_State_Name
        {
            get;
            set;
        }

        public Nullable<int> Booking_Agent_Id
        {
            get;
            set;
        }

        public string Booking_Agent_AgentShortName
        {
            get;
            set;
        }

        public string Booking_Agent_AgentName
        {
            get;
            set;
        }

        public DateTime Booking_Date
        {
            get;
            set;
        }

        public Nullable<int> Booking_Mode
        {
            get;
            set;
        }

        public int Booking_MaterialType_Id
        {
            get;
            set;
        }

        public string Booking_MaterialType_Code
        {
            get;
            set;
        }

        public string Booking_MaterialType_MaterialName
        {
            get;
            set;
        }

        public decimal Booking_MaterialType_TiscoRate
        {
            get;
            set;
        }

        public decimal Booking_MaterialType_CSTRate
        {
            get;
            set;
        }

        public decimal Booking_MaterialType_CFormRate
        {
            get;
            set;
        }

        public decimal Booking_MaterialType_HandlingRate
        {
            get;
            set;
        }

        public decimal Booking_MaterialType_ServiceTax
        {
            get;
            set;
        }

        public decimal Booking_MaterialType_EducationCess
        {
            get;
            set;
        }

        public decimal Booking_MaterialType_HigherEducationCess
        {
            get;
            set;
        }

        public bool Booking_TruckType
        {
            get;
            set;
        }

        public Nullable<int> Booking_Truck_Id
        {
            get;
            set;
        }

        public string Booking_Truck_RegNo
        {
            get;
            set;
        }

        public string Booking_Truck_OwnerName
        {
            get;
            set;
        }

        public string Booking_Truck_DriverName
        {
            get;
            set;
        }

        public string Booking_Truck_OwnerShortAdd
        {
            get;
            set;
        }

        public string Booking_Truck_DriverShortAdd
        {
            get;
            set;
        }

        public Nullable<int> Booking_StandAlone_Truck_Id
        {
            get;
            set;
        }

        public string Booking_StandaloneTruck_RegNo
        {
            get;
            set;
        }

        public string Booking_StandaloneTruck_OwnerName
        {
            get;
            set;
        }

        public string Booking_StandaloneTruck_DriverName
        {
            get;
            set;
        }

        public string Booking_StandaloneTruck_OwnerShortAdd
        {
            get;
            set;
        }

        public string Booking_StandaloneTruck_DriverShortAdd
        {
            get;
            set;
        }

        public Nullable<int> Booking_CounterId
        {
            get;
            set;
        }

        public Nullable<decimal> Booking_AdvanceAmount
        {
            get;
            set;
        }
        public Nullable<decimal> Booking_TotalAdvanceAmount
        {
            get;
            set;
        }

        public Nullable<decimal> Booking_BalanceAmount
        {
            get;
            set;
        }
        public int Booking_TotalIssuedQty
        {
            get;
            set;
        }

        public int Booking_Qty
        {
            get;
            set;
        }

        public string Booking_CounterName
        {
            get;
            set;
        }

        public Nullable<bool> Booking_Status
        {
            get;
            set;
        }

        public string Booking_RejectionReson
        {
            get;
            set;
        }

        public bool Booking_MoneyReceiptIssued
        {
            get;
            set;
        }

        public bool Booking_AccountSettled
        {
            get;
            set;
        }

        public bool Booking_IsAdvanced
        {
            get;
            set;
        }

        public Nullable<bool> Booking_IsFullAmount
        {
            get;
            set;
        }

        public int Booking_CreatedBy
        {
            get;
            set;
        }

        public Nullable<DateTime> Booking_CreatedDate
        {
            get;
            set;
        }

        public Nullable<DateTime> Booking_LastUpdatedDate
        {
            get;
            set;
        }

        public bool Booking_IsDeleted
        {
            get;
            set;
        }

        public string Booking_DriverName
        {
            get;
            set;
        }

        public decimal Booking_Account_Quantity
        {
            get;
            set;
        }

        public Nullable<DateTime> Booking_Cust_AMEVisitDate
        {
            get;
            set;
        }

        public int Booking_Cust_Mat_AnnualRequirement
        {
            get;
            set;
        }

        public string Booking_Account_InvoiceNumber
        {
            get;
            set;
        }

        public Nullable<bool> Booking_TruckIn
        {
            get;
            set;
        }

        public Nullable<System.DateTime> Booking_TruckInTime
        {
            get;
            set;
        }

        public Nullable<bool> Booking_TruckMatLifted
        {
            get;
            set;
        }

        public Nullable<System.DateTime> Booking_TruckMatLiftedTime
        {
            get;
            set;
        }

        public string Booking_Cust_OwnerName
        {
            get;
            set;
        }

        public string Booking_Cust_FirmName
        {
            get;
            set;
        }

        public Nullable<System.DateTime> Booking_AccountSettledDate
        {
            get;
            set;
        }

        #endregion

        #region Navigation Properties
        private SettlementOfAccountsDTO _settlementOfAccounts;
        private CustomerDTO _customer;
        private MaterialTypeDTO _materialtype;
        private TruckDetailsDTO _truckdetail;
        private AgentDTO _agentdetail;
        private StandaloneTrucksDTO _standalonetruck;
        private CounterDTO _counter;

        public CustomerDTO customer
        {
            get { return _customer; }
            set
            {
                if (!ReferenceEquals(_customer, value))
                {
                    var previousValue = _customer;
                    _customer = null;
                    Booking_Cust_Code = value.Cust_Code;
                    Booking_Cust_UnitName = value.Cust_FirmName;
                    Booking_Cust_UnitAddress = value.Cust_UnitAddress;
                    Booking_Cust_District_Name = value.Cust_District_Name;
                    Booking_Cust_State_Name = value.Cust_State_Name;
                    Booking_Cust_TradeName = value.Cust_TradeName;
                    Booking_Cust_AMEVisitDate = value.Cust_AMEVisitDate;
                    Booking_Cust_Mat_AnnualRequirement = value.Cust_Mat_AnnualRequirement;
                    Booking_Cust_OwnerName = value.Cust_OwnerName;
                    Booking_Cust_FirmName = value.Cust_FirmName;
                }
            }
        }

        public virtual MaterialTypeDTO materialtype
        {
            get { return _materialtype; }
            set
            {
                if (!ReferenceEquals(_materialtype, value))
                {
                    var previousValue = _materialtype;
                    _materialtype = null;
                    Booking_MaterialType_Code = value.MaterialType_Code;
                    Booking_MaterialType_MaterialName = value.MaterialType_Name;
                    Booking_MaterialType_TiscoRate = value.MaterialType_TiscoRate;
                    Booking_MaterialType_CFormRate = value.MaterialType_CFormRate;
                    Booking_MaterialType_CSTRate = value.MaterialType_CSTRate;
                    Booking_MaterialType_ServiceTax = value.MaterialType_ServiceTax;
                    Booking_MaterialType_HandlingRate = value.MaterialType_HandlingRate;
                    Booking_MaterialType_EducationCess = value.MaterialType_EducationCess;
                    Booking_MaterialType_HigherEducationCess = value.MaterialType_HigherEducationCess;
                }
            }
        }

        public TruckDetailsDTO truckdetail
        {
            get { return _truckdetail; }
            set
            {
                if (!ReferenceEquals(_truckdetail, value))
                {
                    var previousValue = _truckdetail;
                    _truckdetail = null;
                    Booking_Truck_RegNo = value.Truck_RegNo;
                    Booking_Truck_OwnerName = value.Truck_OwnerName;
                    Booking_Truck_DriverName = value.Truck_DriverName;
                    Booking_Truck_OwnerShortAdd = value.Truck_OwnerShortAdd;
                    Booking_Truck_DriverShortAdd = value.Truck_DriverShortAdd;
                }
            }
        }

        public StandaloneTrucksDTO standalonetruck
        {
            get { return _standalonetruck; }
            set
            {
                if (!ReferenceEquals(_standalonetruck, value))
                {
                    var previousValue = _standalonetruck;
                    _standalonetruck = null;
                    Booking_StandaloneTruck_RegNo = value.StandaloneTruck_RegNo;
                    Booking_StandaloneTruck_OwnerName = value.StandaloneTruck_OwnerName;
                    Booking_StandaloneTruck_DriverName = value.StandaloneTruck_DriverName;
                    Booking_StandaloneTruck_OwnerShortAdd = value.StandaloneTruck_OwnerShortAdd;
                    Booking_StandaloneTruck_DriverShortAdd = value.StandaloneTruck_DriverShortAdd;
                }
            }
        }

        public AgentDTO agentdetail
        {
            get { return _agentdetail; }
            set
            {
                if (!ReferenceEquals(_agentdetail, value))
                {
                    var previousValue = _agentdetail;
                    _agentdetail = null;
                    Booking_Agent_AgentShortName = value.Agent_ShortName;
                    Booking_Agent_AgentName = value.Agent_Name;
                }
            }
        }

        public CounterDTO counter
        {
            get { return _counter; }
            set
            {
                if (!ReferenceEquals(_counter, value))
                {
                    var previousValue = _counter;
                    _counter = null;
                    Booking_CounterName = value.Counter_Name;
                    Booking_CounterId = value.Counter_Id;
                }
            }
        }

        #endregion
    }
}
