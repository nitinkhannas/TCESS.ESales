#region Using directives

using System.ServiceModel;
using TCESS.ESales.DataTransferObjects;
using System;

#endregion

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface ISMSService
{
    /// <summary>
    /// Will return the message to be sent to calling number
    /// </summary>
    /// <param name="phoneNumber">Mobile number</param>
    /// <param name="message">SMS message</param>
    /// <returns>Reply SMS</returns>
    [OperationContract]
	string RespondSms(string phoneNumber, string message, string messageTruck);
	/// <summary>
	/// Will update DCA percentage
	/// </summary>
	/// <returns>Status</returns>
	[OperationContract]
	string UpdateDCAPercentage();
	[OperationContract]
	BookingDTO UpdateGateInformation(int gateLocation, int bookingId);
	/// <summary>
	/// Will Give the total count of truck in or out on particular date
	/// </summary>
	/// <returns>int count</returns>
	[OperationContract]
	int GetTruckCountForDateBarcode(System.DateTime currentDate, int truckStatus);
    [OperationContract]
    string RespondPaymentSms(string phoneNumber, string customerCode, Decimal amount);
    
}