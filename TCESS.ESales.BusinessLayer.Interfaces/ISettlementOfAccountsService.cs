#region Using directives

using TCESS.ESales.DataTransferObjects;
using System.Collections.Generic;
using System;

#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces
{
	public interface ISettlementOfAccountsService
	{
		int SaveSettlementOfAccounts(SettlementOfAccountsDTO settlementOfAccountsDetail);
		SettlementOfAccountsDTO GetSettlementOfAccountsById(int accountId);
        IList<SettlementOfAccountsDTO> GetSettlementDetailsForDay(DateTime fromDate);
		SettlementOfAccountsDTO GetSettlementOfAccountsByBookingId(int bookingID);
        IList<object> GetSettlementOfAccountsCount(int userID);
        SettlementOfAccountsDTO GetLastSettlementOfAccountsByTruckNo(string truckNo);
        decimal GetQtySum(int customerID);
        decimal GetMaterialAmountLiftedByCustomer(int customerID, DateTime fromDate, DateTime toDate);
	}
}