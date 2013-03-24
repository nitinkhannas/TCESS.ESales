#region Namespace
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.PersistenceLayer.Entity;
using TCESS.ESales.DataTransferObjects.GhatoCollection;
using TCESS.ESales.DataTransferObjects.Users;
using TCESS.ESales.DataTransferObjects.Masters;


#endregion

namespace TCESS.ESales.CommonLayer.Mapper
{
    public class MapObject : IMapObject
    {
        #region IMapObject Members

        public void CreateMap()
        {
            //Mapper for State
            AutoMapper.Mapper.CreateMap<StateDTO, state>();
            AutoMapper.Mapper.CreateMap<state, StateDTO>();

            //Mapper for District
            AutoMapper.Mapper.CreateMap<DistrictDTO, district>();
            AutoMapper.Mapper.CreateMap<district, DistrictDTO>();

            //Mapper for OwnershipStatus
            AutoMapper.Mapper.CreateMap<OwnershipStatusDTO, ownershipstatu>();
            AutoMapper.Mapper.CreateMap<ownershipstatu, OwnershipStatusDTO>();

            //Mapper for CustDocTypeInfo
            AutoMapper.Mapper.CreateMap<DocTypeDTO, doctype>();
            AutoMapper.Mapper.CreateMap<doctype, DocTypeDTO>();

            //Mapper for AMeBlock
            AutoMapper.Mapper.CreateMap<AmeBlockDTO, ameblock>();
            AutoMapper.Mapper.CreateMap<ameblock, AmeBlockDTO>();

            //Mapper for Businesstype
            AutoMapper.Mapper.CreateMap<BusinessTypeDTO, businesstype>();
            AutoMapper.Mapper.CreateMap<businesstype, BusinessTypeDTO>();

            //Mapper for Materialtype
            AutoMapper.Mapper.CreateMap<MaterialTypeDTO, materialtype>();
            AutoMapper.Mapper.CreateMap<materialtype, MaterialTypeDTO>();

            //Mapper for CustomerTruckDetails
            AutoMapper.Mapper.CreateMap<TruckDetailsDTO, truckdetail>();
            AutoMapper.Mapper.CreateMap<truckdetail, TruckDetailsDTO>();

            //Mapper for TruckDocumentTypeDetails
            AutoMapper.Mapper.CreateMap<TruckDocDetailsDTO, truckdocdetail>();
            AutoMapper.Mapper.CreateMap<truckdocdetail, TruckDocDetailsDTO>();

            //Mapper for Customer
            AutoMapper.Mapper.CreateMap<CustomerDTO, customer>();
            AutoMapper.Mapper.CreateMap<customer, CustomerDTO>();

            //Mapper for CustomerDocuments
            AutoMapper.Mapper.CreateMap<CustomerDocDetailsDTO, customerdocdetail>();
            AutoMapper.Mapper.CreateMap<customerdocdetail, CustomerDocDetailsDTO>();

            //Mapper for agent
            AutoMapper.Mapper.CreateMap<AgentDTO, agentdetail>();
            AutoMapper.Mapper.CreateMap<agentdetail, AgentDTO>();

            //Mapper for authorized representative 
            AutoMapper.Mapper.CreateMap<AuthRepDTO, authrepdetail>();
            AutoMapper.Mapper.CreateMap<authrepdetail, AuthRepDTO>();

            //Mapper for alloted qunatity
            AutoMapper.Mapper.CreateMap<AllotedQuantityDTO, allotedquantity>();
            AutoMapper.Mapper.CreateMap<allotedquantity, AllotedQuantityDTO>();

            //Mapper for agent material percentage
            AutoMapper.Mapper.CreateMap<agentmaterialpercentage, AgentMaterialPercentageDTO>();
            AutoMapper.Mapper.CreateMap<AgentMaterialPercentageDTO, agentmaterialpercentage>();

            //Mapper for User in Roles
            AutoMapper.Mapper.CreateMap<UsersInRolesDTO, my_aspnet_usersinroles>();
            AutoMapper.Mapper.CreateMap<my_aspnet_usersinroles, UsersInRolesDTO>();

            //Mapper for User in Pages in Roles
            AutoMapper.Mapper.CreateMap<PagesInRoleDTO, pagesinrole>();
            AutoMapper.Mapper.CreateMap<pagesinrole, PagesInRoleDTO>();

            //Mapper for User in DCA
            AutoMapper.Mapper.CreateMap<UserAgentMappingDTO, useragentmapping>();
            AutoMapper.Mapper.CreateMap<useragentmapping, UserAgentMappingDTO>();

            //Mapper for Page Info
            AutoMapper.Mapper.CreateMap<PageInfoDTO, pageinfo>();
            AutoMapper.Mapper.CreateMap<pageinfo, PageInfoDTO>();

            //Mapper for Customer Material Mapping
            AutoMapper.Mapper.CreateMap<CustomerMaterialMapDTO, customermaterialmap>();
            AutoMapper.Mapper.CreateMap<customermaterialmap, CustomerMaterialMapDTO>();

            //Mapper for Agent Document Mapping
            AutoMapper.Mapper.CreateMap<AuthRepDocDetailDTO, authrepdocdetail>();
            AutoMapper.Mapper.CreateMap<authrepdocdetail, AuthRepDocDetailDTO>();

            //Mapper for Roles
            AutoMapper.Mapper.CreateMap<RolesDTO, my_aspnet_roles>();
            AutoMapper.Mapper.CreateMap<my_aspnet_roles, RolesDTO>();

            //Mapper for Roles
            AutoMapper.Mapper.CreateMap<IList<customermaterialmap>, IList<CustomerMaterialMapDTO>>();
            AutoMapper.Mapper.CreateMap<IList<CustomerMaterialMapDTO>, IList<customermaterialmap>>();

            //Mapper for Cust Authorization Detail
            AutoMapper.Mapper.CreateMap<CustAuthorizationDetailDTO, custauthorizationdetail>();
            AutoMapper.Mapper.CreateMap<custauthorizationdetail, CustAuthorizationDetailDTO>();

            //Mapper for Truck Make Detail
            AutoMapper.Mapper.CreateMap<TruckMakeDTO, truckmake>();
            AutoMapper.Mapper.CreateMap<truckmake, TruckMakeDTO>();

            //Mapper for Truck Make Detail
            AutoMapper.Mapper.CreateMap<TruckCarryCapacityDTO, truckcarrycapacity>();
            AutoMapper.Mapper.CreateMap<truckcarrycapacity, TruckCarryCapacityDTO>();

            //Mapper for Truck Make Detail
            AutoMapper.Mapper.CreateMap<TruckWheelerDTO, truckwheeler>();
            AutoMapper.Mapper.CreateMap<truckwheeler, TruckWheelerDTO>();

            //Mapper for Booking
            AutoMapper.Mapper.CreateMap<BookingDTO, booking>();
            AutoMapper.Mapper.CreateMap<booking, BookingDTO>();

            //Mapper for Money Receipt
            AutoMapper.Mapper.CreateMap<MoneyReceiptDTO, moneyreceipt>();
            AutoMapper.Mapper.CreateMap<moneyreceipt, MoneyReceiptDTO>();

            //Mapper for Settlement Of Accounts
            AutoMapper.Mapper.CreateMap<SettlementOfAccountsDTO, settlementofaccount>();
            AutoMapper.Mapper.CreateMap<settlementofaccount, SettlementOfAccountsDTO>();

            //Mapper for DCA Material Allocation 
            AutoMapper.Mapper.CreateMap<DcaMaterialAllocationDTO, dcamaterialallocation>();
            AutoMapper.Mapper.CreateMap<dcamaterialallocation, DcaMaterialAllocationDTO>();

            //Mapper for Standalone Trucks
            AutoMapper.Mapper.CreateMap<standalonetruck, StandaloneTrucksDTO>();
            AutoMapper.Mapper.CreateMap<StandaloneTrucksDTO, standalonetruck>();

            //Mapper for Standalone Truck Document Details
            AutoMapper.Mapper.CreateMap<standalonetruckdocdetail, StandaloneTruckDocDetails>();
            AutoMapper.Mapper.CreateMap<StandaloneTruckDocDetails, standalonetruckdocdetail>();

            //Mapper for Counter
            AutoMapper.Mapper.CreateMap<CounterDTO, counter>();
            AutoMapper.Mapper.CreateMap<counter, CounterDTO>();

            //Mapper for Counter Details
            AutoMapper.Mapper.CreateMap<CounterDetailsDTO, counterdetail>();
            AutoMapper.Mapper.CreateMap<counterdetail, CounterDetailsDTO>();

            //Mapper for Customer Documents
            AutoMapper.Mapper.CreateMap<CustomerDocumentsDTO, customerdocument>();
            AutoMapper.Mapper.CreateMap<customerdocument, CustomerDocumentsDTO>();

            //Mapper for Truck Documents
            AutoMapper.Mapper.CreateMap<TruckDocumentsDTO, truckdocument>();
            AutoMapper.Mapper.CreateMap<truckdocument, TruckDocumentsDTO>();

            //Mapper for Auth Rep Documents
            AutoMapper.Mapper.CreateMap<AuthRepDocumentsDTO, authrepdocument>();
            AutoMapper.Mapper.CreateMap<authrepdocument, AuthRepDocumentsDTO>();

            //Mapper for Auth Rep Documents
            AutoMapper.Mapper.CreateMap<BookingModeDTO, bookingmode>();
            AutoMapper.Mapper.CreateMap<bookingmode, BookingModeDTO>();

            //Mapper for Auth Rep Documents
            AutoMapper.Mapper.CreateMap<BookingModeDetailDTO, bookingmodedetail>();
            AutoMapper.Mapper.CreateMap<bookingmodedetail, BookingModeDetailDTO>();

            //Mapper for Materialtype History
            AutoMapper.Mapper.CreateMap<MaterialTypeDTO, materialtype_history>();
            AutoMapper.Mapper.CreateMap<materialtype_history, MaterialTypeDTO>();

            //Mapper for SMS Registration
            AutoMapper.Mapper.CreateMap<SMSRegistrationDTO, smsregistration>();
            AutoMapper.Mapper.CreateMap<smsregistration, SMSRegistrationDTO>();

            //Mapper for Truck Verification 
            AutoMapper.Mapper.CreateMap<TruckVerificationDTO, truckverification>();
            AutoMapper.Mapper.CreateMap<truckverification, TruckVerificationDTO>();

            //Mapper for Dispatch Report
            AutoMapper.Mapper.CreateMap<DispatchReportDTO, dispatchreport>();
            AutoMapper.Mapper.CreateMap<dispatchreport, DispatchReportDTO>()
                  .ForMember(d => d.QtyLiftedMts, o => o.MapFrom(s => Convert.ToDecimal(s.QtyLiftedMts)))
                  .ForMember(d => d.TSLAmount, o => o.MapFrom(s => Convert.ToDecimal(s.TSLAmount)))
                  .ForMember(d => d.DCABillHandling, o => o.MapFrom(s => Convert.ToDecimal(s.DCABillHandling)))
                  .ForMember(d => d.ServiceTax, o => o.MapFrom(s => Convert.ToDecimal(s.ServiceTax)))
                  .ForMember(d => d.ECess2, o => o.MapFrom(s => Convert.ToDecimal(s.ECess2)))
                  .ForMember(d => d.HECess1, o => o.MapFrom(s => Convert.ToDecimal(s.HECess1)))
                  .ForMember(d => d.AdvanceReceived, o => o.MapFrom(s => Convert.ToDecimal(s.AdvanceReceived)));

            //Mapper for Payment mode
            AutoMapper.Mapper.CreateMap<PaymentModeDTO, paymentmode>();
            AutoMapper.Mapper.CreateMap<paymentmode, PaymentModeDTO>();

            //Mapper for Code Alloc 
            AutoMapper.Mapper.CreateMap<CustomerDetailsForCodeAllocDTO, customerdetailsforcodealloc>();
            AutoMapper.Mapper.CreateMap<customerdetailsforcodealloc, CustomerDetailsForCodeAllocDTO>();
            //Mapper for Customer Document View 
            AutoMapper.Mapper.CreateMap<CustomerDocumentsViewDTO, customerdocumentsview>();
            AutoMapper.Mapper.CreateMap<customerdocumentsview, CustomerDocumentsViewDTO>();
            //Mapper for Customer Document View and Customer Document
            AutoMapper.Mapper.CreateMap<CustomerDocumentsViewDTO, CustomerDocumentsDTO>();
            AutoMapper.Mapper.CreateMap<CustomerDocumentsDTO, CustomerDocumentsViewDTO>();
            //Mapper for SMS Limit
            AutoMapper.Mapper.CreateMap<SMSLimitDTO, smsbookinglimit>();
            AutoMapper.Mapper.CreateMap<smsbookinglimit, SMSLimitDTO>();

            AutoMapper.Mapper.CreateMap<truckregtype, TruckRegTypeDTO>();
            AutoMapper.Mapper.CreateMap<TruckRegTypeDTO, truckregtype>();

            AutoMapper.Mapper.CreateMap<dformreport, DFormReportDTO>();
            AutoMapper.Mapper.CreateMap<DFormReportDTO, dformreport>();

            AutoMapper.Mapper.CreateMap<roadpermitreport, RoadPermitReportDTO>();
            AutoMapper.Mapper.CreateMap<RoadPermitReportDTO, roadpermitreport>();

            AutoMapper.Mapper.CreateMap<CustomerwiseSalesReportDTO, loadingadivcerpt>();
            AutoMapper.Mapper.CreateMap<loadingadivcerpt, CustomerwiseSalesReportDTO>();

            AutoMapper.Mapper.CreateMap<LiftingLimitDTO, liftinglimit_history>();
            AutoMapper.Mapper.CreateMap<liftinglimit_history, LiftingLimitDTO>();

            AutoMapper.Mapper.CreateMap<LiftingLimitDTO, liftinglimit>();
            AutoMapper.Mapper.CreateMap<liftinglimit, LiftingLimitDTO>();

            AutoMapper.Mapper.CreateMap<Form27CDTO, form27c>();
            AutoMapper.Mapper.CreateMap<form27c, Form27CDTO>();

            AutoMapper.Mapper.CreateMap<Form27C_HistoryDTO, form27c_history>();
            AutoMapper.Mapper.CreateMap<form27c_history, Form27C_HistoryDTO>();

            AutoMapper.Mapper.CreateMap<Form27CDTO, form27c_history>();
            AutoMapper.Mapper.CreateMap<form27c_history, Form27CDTO>();

            AutoMapper.Mapper.CreateMap<AffidavitDetailsDTO, affidavitdetail>();
            AutoMapper.Mapper.CreateMap<affidavitdetail, AffidavitDetailsDTO>();

            AutoMapper.Mapper.CreateMap<PeriodTypeDTO, periodtype>();
            AutoMapper.Mapper.CreateMap<periodtype, PeriodTypeDTO>();

            AutoMapper.Mapper.CreateMap<Form27PeriodTypeDTO, form27cperiodtype>();
            AutoMapper.Mapper.CreateMap<form27cperiodtype, Form27PeriodTypeDTO>();

            AutoMapper.Mapper.CreateMap<MonthsDTO, month>();
            AutoMapper.Mapper.CreateMap<month, MonthsDTO>();

            AutoMapper.Mapper.CreateMap<LiftingIntervalDTO, liftinginterval>();
            AutoMapper.Mapper.CreateMap<liftinginterval, LiftingIntervalDTO>();

            AutoMapper.Mapper.CreateMap<Form27CViewDTO, form27cview>();
            AutoMapper.Mapper.CreateMap<form27cview, Form27CViewDTO>();

            AutoMapper.Mapper.CreateMap<pervioussettlementDTO, pervioussettlement>();
            AutoMapper.Mapper.CreateMap<pervioussettlement, pervioussettlementDTO>();

            AutoMapper.Mapper.CreateMap<PaymentCollectionDTO, paymentcollection>();
            AutoMapper.Mapper.CreateMap<paymentcollection, PaymentCollectionDTO>();

            AutoMapper.Mapper.CreateMap<UserPaymentModeMappingDTO, userpaymentmodemapping>();
            AutoMapper.Mapper.CreateMap<userpaymentmodemapping, UserPaymentModeMappingDTO>();

            AutoMapper.Mapper.CreateMap<BankDTO, bank>();
            AutoMapper.Mapper.CreateMap<bank, BankDTO>();

            AutoMapper.Mapper.CreateMap<BatchTransferDTO, batchtransfer>();
            AutoMapper.Mapper.CreateMap<batchtransfer, BatchTransferDTO>();

            AutoMapper.Mapper.CreateMap<PaymentTransitDTO, paymenttransit>();
            AutoMapper.Mapper.CreateMap<paymenttransit, PaymentTransitDTO>();

            AutoMapper.Mapper.CreateMap<RejectionReasonDTO, rejectionreason>();
            AutoMapper.Mapper.CreateMap<rejectionreason, RejectionReasonDTO>();

            AutoMapper.Mapper.CreateMap<PaymentRefundDTO, paymentrefund>();
            AutoMapper.Mapper.CreateMap<paymentrefund, PaymentRefundDTO>();

            AutoMapper.Mapper.CreateMap<vwcollectionsummary, CollectionSummaryDTO>();
            AutoMapper.Mapper.CreateMap<CollectionSummaryDTO, vwcollectionsummary>();

            AutoMapper.Mapper.CreateMap<smspaymentregistration, SMSPaymentRegistrationDTO>();
            AutoMapper.Mapper.CreateMap<SMSPaymentRegistrationDTO, smspaymentregistration>();

        }

        #endregion
    }
}