#region Using directives

using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.BusinessLayer.Interfaces.GhatoCollection;
using TCESS.ESales.BusinessLayer.Interfaces.Masters;
using TCESS.ESales.BusinessLayer.Interfaces.Users;
using TCESS.ESales.BusinessLayer.Services;
using TCESS.ESales.BusinessLayer.Services.GhatoCollection;
using TCESS.ESales.BusinessLayer.Services.Masters;
using TCESS.ESales.BusinessLayer.Services.Users;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Exception;
using TCESS.ESales.CommonLayer.Mapper;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.Logging;
using TCESS.ESales.PersistenceLayer.Interfaces;
using TCESS.ESales.PersistenceLayer.Repository;

#endregion

namespace TCESS.ESales.CommonLayer.UnityExtension
{
    public class ESalesUnityContainerExtension
    {
        public static void InitializeContainer()
        {
            //Initializes unity container and registers interface with service classes 
            InitializeUnityContainer();

            //Initializes exception manager with details from config file
            ExceptionHandler.InitializeExceptionManager();

            //Initializes Log Manager
            CustomLogger.InitializeLogManager(Globals.ConfigVariables.LOGFILEPATH);
        }

        public static void InitializeUnityContainer()
        {
            ESalesUnityContainer.InitializeContainer();

            #region Registration of Classes

            ESalesUnityContainer.Container.RegisterType<IMapObject, MapObject>();
            ESalesUnityContainer.Container.RegisterType<IAmeBlockService, AmeBlockService>();
            ESalesUnityContainer.Container.RegisterType<ICustomerMastersService, CustomerMastersService>();
            ESalesUnityContainer.Container.RegisterType<ICustomerService, CustomerService>();
            ESalesUnityContainer.Container.RegisterType<ITruckService, TruckService>();
            ESalesUnityContainer.Container.RegisterType<IAuthRepService, AuthRepService>();
            ESalesUnityContainer.Container.RegisterType<IDocumentTypeService, DocumentTypeService>();
            ESalesUnityContainer.Container.RegisterType<ILocationService, LocationService>();
            ESalesUnityContainer.Container.RegisterType<IAgentService, AgentService>();
            ESalesUnityContainer.Container.RegisterType<IMaterialTypeService, MaterialTypeService>();
            ESalesUnityContainer.Container.RegisterType<ICautionListService, CautionListService>();
            ESalesUnityContainer.Container.RegisterType<IMembershipService, MembershipService>();
            ESalesUnityContainer.Container.RegisterType<IUserAgentService, UserAgentService>();
            ESalesUnityContainer.Container.RegisterType<ICustomerMaterialService, CustomerMaterialService>();
            ESalesUnityContainer.Container.RegisterType<IBookingService, BookingService>();
            ESalesUnityContainer.Container.RegisterType<IMoneyReceiptService, MoneyReceiptService>();
            ESalesUnityContainer.Container.RegisterType<ISettlementOfAccountsService, SettlementOfAccountsService>();
            ESalesUnityContainer.Container.RegisterType<IStandaloneTruckService, StandaloneTruckService>();
            ESalesUnityContainer.Container.RegisterType<IStandaloneTruckService, StandaloneTruckService>();
            ESalesUnityContainer.Container.RegisterType<IDcaMaterialAllocationService, DcaMaterialAllocationService>();
            ESalesUnityContainer.Container.RegisterType<IAgentMaterialPercentageService, AgentMaterialPercentageService>();
            ESalesUnityContainer.Container.RegisterType<IBookingModeService, BookingModeService>();
            ESalesUnityContainer.Container.RegisterType<IReportService, ReportService>();
            ESalesUnityContainer.Container.RegisterType<ISMSService, SMSService>();
            ESalesUnityContainer.Container.RegisterType<ICustomerRepository, CustomerRepository>();
            ESalesUnityContainer.Container.RegisterType<ICustomerMaterialMapRepository, CustomerMaterialMapRepository>();
            ESalesUnityContainer.Container.RegisterType<ICounterService, CounterService>();
            ESalesUnityContainer.Container.RegisterType<ICounterRepository, CounterRepository>();
            ESalesUnityContainer.Container.RegisterType<IBookingModeRepository, BookingModeRepository>();
            ESalesUnityContainer.Container.RegisterType<ITruckMakeService, TruckMakeService>();
            ESalesUnityContainer.Container.RegisterType<ITruckDocService, TruckDocService>();
            ESalesUnityContainer.Container.RegisterType<ICustomerDocService, CustomerDocService>();
            ESalesUnityContainer.Container.RegisterType<ICustAuthorizationService, CustAuthorizationService>();
            ESalesUnityContainer.Container.RegisterType<ISMSLimitService, SMSLimitService>();
            ESalesUnityContainer.Container.RegisterType<ILiftingLimit, LiftingLimitService>();
            ESalesUnityContainer.Container.RegisterType<IForm27CService, Form27CService>();
            ESalesUnityContainer.Container.RegisterType<IAffidavitDetails, AffidavitDetailsService>();
            ESalesUnityContainer.Container.RegisterType<IForm27C_HistoryService, Form27C_HistoryService>();
            ESalesUnityContainer.Container.RegisterType<IPaymentService, PaymentService>();
            ESalesUnityContainer.Container.RegisterType<IUserPaymentModeService, UserPaymentModeService>();
            ESalesUnityContainer.Container.RegisterType<IMasterService, MasterService>();
            ESalesUnityContainer.Container.RegisterType(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            #endregion
        }
    }
}