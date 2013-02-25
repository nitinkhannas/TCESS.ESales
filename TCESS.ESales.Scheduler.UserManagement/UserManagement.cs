#region Using directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.CommonLibrary;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.Logging;
using TCESS.ESales.CommonLayer.UnityExtension;
using TCESS.ESales.CommonLayer.Mapper;

#endregion

namespace TCESS.ESales.Scheduler.UserManagement
{
    public class UserManagement
    {
        // Singleton instance
		private static volatile UserManagement uniqueInstance;
        
		#region Private Constructor

		private UserManagement()
		{
			try
			{
                //Initializes unity container and registers interface with service classes 
                ESalesUnityContainerExtension.InitializeUnityContainer();

                //Creates mapping between Data transfer objects and persistence layer
                InitializeMapper();

                //Initializes Log Manager
                CustomLogger.InitializeLogManager();
                
                //Save user details in database with counter number
				SaveUserDetails();
			}
			catch (Exception exception)
			{
				// Logs the error
                CustomLogger.WriteLog(exception.Message);
			}
		}

		#endregion

        /// <summary>
        /// Creates mapping between Data transfer objects and persistence layer
        /// </summary>
        private void InitializeMapper()
        {
            ESalesUnityContainer.Container.Resolve<IMapObject>().CreateMap();
        }

		/// <summary>
		/// GetInstance Constructor Will Check For Nullability of existing Instance, If Exists,
		/// Instantiate it with New Or Else Return the existing Instance
		/// </summary>
		public static UserManagement GetInstance()
		{
			try
			{
				if (uniqueInstance == null)
				{
					uniqueInstance = new UserManagement();
				}
			}
			catch (Exception ex)
			{
				//Logs the error
                CustomLogger.WriteLog(ex.Message);
			}
			return uniqueInstance;
		}

        private void SaveUserDetails()
        {
            //gets the mac address of the first operation nic found.
            string macAddress = GetMacAddress();

            //Gets counter details from MAC address
            CounterDTO counter = ESalesUnityContainer.Container.Resolve<ICounterService>()
                .GetCounterDetailsByMacId(macAddress, 0, 0);

            if (counter.Counter_Id > 0)
            {
                //Gets list of agent and material details from database
                IList<DcaMaterialAllocationDTO> lstAgentMaterialDetails = ESalesUnityContainer.Container
                    .Resolve<IDcaMaterialAllocationService>()
                    .GetMaterialAgentAllocationDetails(Convert.ToInt32(counter.Counter_Agent_Id), DateTime.Now.Date);

                //Update list of agent and mark agent material mapping as active
                (from agentMaterial in lstAgentMaterialDetails select agentMaterial)
                    .Update(agentDetail => agentDetail.DCAMA_IsAgentActive = true);

                ESalesUnityContainer.Container.Resolve<ICounterService>()
                    .SaveCounterDailyDetails(InitializeCounterDetails(counter), lstAgentMaterialDetails);
            }
        }

        /// <summary>
        /// Initialize counter details to be updated on daily basis
        /// </summary>
        /// <param name="counterDetail">Counter which is to be updated for current day</param>
        /// <returns>returns counter details</returns>
        private CounterDetailsDTO InitializeCounterDetails(CounterDTO counter)
        {
            CounterDetailsDTO counterDetails = new CounterDetailsDTO();
            counterDetails.CounterDetail_Counter_ID = counter.Counter_Id;
            counterDetails.CounterDetail_Agent_Id = Convert.ToInt32(counter.Counter_Agent_Id);
            counterDetails.CounterDetail_Date = DateTime.Now.Date;
            counterDetails.CounterDetail_Count = 0;
            counterDetails.CounterDetail_CreatedDate = DateTime.Now;            
            return counterDetails;
        }

        /// <summary>
        /// returns the mac address of the first operation nic found.
        /// </summary>
        /// <returns></returns>
        private static string GetMacAddress()
        {
            string macAddress = string.Empty;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddress += nic.GetPhysicalAddress().ToString().Replace("-", "");
                    EventLog.WriteEntry("MACAddress", macAddress);
                    break;
                }
            }
            return macAddress;
        }
    }
}