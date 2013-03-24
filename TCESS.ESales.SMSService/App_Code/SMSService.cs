#region Using directives

using System.ServiceModel;
using System.ServiceModel.Activation;
using Microsoft.Practices.Unity;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Mapper;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.CommonLayer.UnityExtension;
using TCESS.ESales.DataTransferObjects;

#endregion

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, InstanceContextMode = InstanceContextMode.PerSession)]
[AspNetCompatibilityRequirements(RequirementsMode =AspNetCompatibilityRequirementsMode.Required)]
public class SMSService : ISMSService
{
    /// <summary>
    /// Will return the message to be sent to calling number
    /// </summary>
    /// <param name="phoneNumber">Mobile number</param>
    /// <param name="message">SMS message</param>
    /// <returns>Reply SMS</returns>
    public string RespondSms(string phoneNumber, string message, string messageTruck)
    {
		ESalesUnityContainerExtension.InitializeContainer();
		ESalesUnityContainer.Container.Resolve<IMapObject>().CreateMap();
		return SmsUtility.RespondSms(phoneNumber, message, messageTruck);
    }



	public string UpdateDCAPercentage()
	{
		ESalesUnityContainerExtension.InitializeContainer();
		ESalesUnityContainer.Container.Resolve<IMapObject>().CreateMap();
		return SmsUtility.UpdateDCAPercentage();
	}

	/// <summary>
	/// Will update booking infomration based on the truck
	/// </summary>
	/// <returns></returns>
	public BookingDTO UpdateGateInformation(int gateLocation, int bookingId)
	{
		ESalesUnityContainerExtension.InitializeContainer();
		ESalesUnityContainer.Container.Resolve<IMapObject>().CreateMap();
		BookingDTO BookingData = SmsUtility.UpdateGateInformation(gateLocation, bookingId);
		return BookingData;

	}
	/// <summary>
	/// Will Give the total count of truck in or out on particular date
	/// </summary>
	/// <returns>int count</returns>
	public int GetTruckCountForDateBarcode(System.DateTime currentDate, int truckStatus)
	{
		ESalesUnityContainerExtension.InitializeContainer();
		ESalesUnityContainer.Container.Resolve<IMapObject>().CreateMap();
		int truckCount = SmsUtility.GetTruckCountForDateBarcode(currentDate, truckStatus);
		return truckCount;

	}


    public string RespondPaymentSms(string phoneNumber, string customerCode, decimal amount)
    {
        ESalesUnityContainerExtension.InitializeContainer();
        ESalesUnityContainer.Container.Resolve<IMapObject>().CreateMap();
        return SmsUtility.AcceptPayment(phoneNumber, customerCode, amount);
    }
}