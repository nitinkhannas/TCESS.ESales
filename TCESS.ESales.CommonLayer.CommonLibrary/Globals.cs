#region Using directives

using System.Configuration;
using System.IO;
using System.Web;

#endregion

namespace TCESS.ESales.CommonLayer.CommonLibrary
{
    public sealed class Globals
    {
        public static byte[] _blankImageBytes;

        /// <summary>
        /// Button Event Arguments for dialog window
        /// </summary>
        public enum ButtonEventArguments
        {
            YES,
            NO,
            CANCEL,
            DELETE
        }

        /// <summary>
        /// Enum for Exception Types
        /// </summary>
        public enum ExceptionTypes
        {
            AssistingAdministrators,
            ExceptionShielding,
            LoggingException,
            NotifyingRethrow,
            ReplacingException
        }

        /// <summary>
        /// Enum for Payment modes
        /// </summary>
        public enum PaymentModes
        {
            CHEQUE = 1,
            DEMANDDRAFT = 2,
            RTGSTRANSFER = 3,
            CASH = 8
        }

        /// <summary>
        /// Enum for Collection Status
        /// </summary>
        public enum CollectionStatus
        {
            SENTTOCASHIER = 1,
            ACCEPTEDBYCASHIER = 2,
            CANCELLED = 3,
            REISSUED = 4
        }

        /// <summary>
        /// Enum for Collection Status
        /// </summary>
        public enum InstrumentStatus
        {
            ACTIVATED = 1,
            PENDING = 2,
            BOUNCED = 3,
        }

        /// <summary>
        /// Enum for Payment headers
        /// </summary>
        public enum PaymentHeader
        {
            FORREPORTSCREEN = 1,
            FORSUPERVISORSCREEN = 2
        }

        /// <summary>
        /// Enum for Payment headers
        /// </summary>
        public enum BatchIdentity
        {
            FROMCOLLECTIONSCREEN = 1,
            FORSUPERVISORSCREEN = 2
        }

        public enum ChequeStatus
        {
            ACCEPTED = 1,
            REJECTED = 3
        }

        /// <summary>
        /// Config variables to read from web.config files
        /// </summary>
        public struct ConfigVariables
        {
            public static readonly string MAXREQUESTLENGTH = ConfigurationManager.AppSettings["MaxRequestLength"];
            public static readonly string PRINTERNAME = ConfigurationManager.AppSettings["PrinterName"];
            public static readonly string LOGFILEPATH = Path.Combine(HttpContext.Current.Server.MapPath(""), ConfigurationManager.AppSettings["LogFilePath"]);
        }

        /// <summary>
        /// Constant for State management variables which are used on page
        /// </summary>
        public struct StateMgmtVariables
        {

            public const string VERIFICATIONMODE = "VerificationMode";
            public const string REFUNDID = "RefundId";
            public const string COLLECTIONID = "CollectionId";
            public const string NEWCOLLECTIONID = "NewCollectionId";
            public const string PAYMENTHEADERFOR = "PaymentHeaderFor";
            public const string SETACTIONMODE = "SetActionMode";
            public const string ACTIONMODEFORMANAGEPAYMENTS = "ActionModeForManagePayments";
            public const string PAYMENTMODE = "PaymentMode";
            public const string PAYMENTID = "PaymentId";
            public const string BATCHID = "BatchId";
            public const string FILEPATH = "FilePath";
            public const string ISMANAGECUSTOMERS = "IsManageCustomers";
            public const string CUSTOMERID = "CustId";
            public const string CUSTOMERCODE = "CustomerCode";
            public const string CUSTOMERNAME = "CustomerName";
            public const string VIEWCUSTOMERSOURCE = "ViewCustomerSource";
            public const string EDITCUSTOMER = "EditCustomer";
            public const string CHECKEDITEMS = "CHECKED_ITEMS";
            public const string AGENTID = "AgentId";
            public const string CUSTOMERBUSINESSTYPE = "CustomerBusinessType";
            public const string CUSTOMERTRUCKTYPE = "CustomerTruckType";
            public const string CUSTOMERTRUCKTYPENAME = "CustomerTruckTypeName";
            public const string TRADENAME = "TradeName";
            public const string BOOKINGID = "BookingId";
            public const string BookingCusotmerID = "BookingCusotmerID";
            public const string MONEYRECEIPTID = "MoneyReceiptId";
            public const string ACCOUNTID = "AccountId";
            public const string CUSTFOLDERNAME = "CustFolderName";
            public const string BLOCKID = "BlockId";
            public const string ROLEID = "RoleId";
            public const string ROLENAME = "RoleName";
            public const string TRUCKID = "TruckId";
            public const string COUNTERID = "CounterId";
            public const string DOCID = "DocId";
            public const string DOCTYPE = "DocType";
            public const string GETDOCTYPE = "GetDocType";
            public const string STATEID = "StateId";
            public const string AUTHREPID = "AuthRepId";
            public const string MANDATORYDOCTYPE = "MandatoryDocType";
            public const string MANDATORYDOCNO = "MandatoryDocNo";
            public const string SERVICETAX = "ServiceTax";
            public const string EDUCATIONCESS = "EducationCess";
            public const string HEDUCATIONCESS = "HigherEducationCess";
            public const string CUSTOMEREGTYPE = "CustRegType";
            public const string BOOKINGMODEID = "BookingModeId";
            public const string BOOKINGGROUP = "BookingGroup";
            public const string ADVANCEAMOUNT = "AdvanceAmount";
            public const string MOBILENO = "MobileNo";
            public const string TISCORATE = "TiscoRate";
            public const string CFORMRATE = "CFormRate";
            public const string CSTRATE = "CSTRate";
            public const string INSTRUMENTTYPE = "InstrumentType";
            public const string SELECTEDAMOUNT = "SelectedAmount";
        }

        /// <summary>
        /// Constant for Grid command events
        /// </summary>
        public struct GridCommandEvents
        {
            public const string EDITDOCUMENT = "EditDocument";
            public const string EDIT = "Edit";
            public const string PRINTCUSTOMER = "PrintCustomer";
            public const string ADDNEW = "AddNew";
            public const string ISSUEMONEYRECEIPT = "IssueMoneyReceipt";
            public const string REFRESH = "Refresh";
            public const string CANCEL = "CancelRequest";
            public const string EDITBOOKING = "EditBooking";
            public const string EDITDCA = "EditDCA";
            public const string EDITTRUCK = "EditTruck";
            public const string SHOWCUSTOMER = "ShowCustomer";
            public const string EDITCOUNTER = "EditCounter";
            public const string VIEW = "View";
            public const string VIEWDOC = "ViewDoc";
            public const string SHOWDISTRICT = "ShowDistrict";
            public const string EDITAUTHREP = "EditAuthRep";
            public const string EDITCUSTOMER = "EditCustomer";
            public const string PRINBILL = "PrintBill";
            public const string PRINT = "Print";
            public const string REJECTBOOKING = "RejectBooking";
            public const string REISSUE = "ReIssue";
        }

        /// <summary>
        /// Folder path details to be used in customer registration and
        /// Standalone truck registration
        /// </summary>
        public struct FolderDetails
        {
            public readonly static string FOLDERPATH = ConfigurationManager.AppSettings["FolderPath"];
            public readonly static string STANDALONETRUCKFOLDER = ConfigurationManager.AppSettings["StandaloneTruckFolder"];
        }

        /// <summary>
        /// Constant for Acronyms used in grid
        /// </summary>
        public struct AcronymType
        {
            public const string REGN = "REGN";
            public const string PUC = "PUC";
            public const string PAN = "PAN";
            public const string TIN = "TIN";
        }
    }
}